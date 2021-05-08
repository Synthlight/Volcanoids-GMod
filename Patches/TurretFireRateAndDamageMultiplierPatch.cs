using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Base_Mod;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class TurretFireRateAndDamageMultiplierPatch {
        private static readonly FieldInfo TURRET_M_WEAPON                            = typeof(Turret).GetField("m_weapon", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo WEAPON_RELOADER_ONLINE_CARGO_M_RELOAD_TIME = typeof(WeaponReloaderOnlineCargo).GetField("m_reloadTime", BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly List<GUID> TURRET_AMMO_ITEMS = new List<GUID> {
            GUID.Parse("f46cdfb659dd5f3428f5a1c9c1fe7d32"), // Pistol Turret Ammo
            GUID.Parse("cdd3ca2c103709d4395f67d08bef56f9"), // Shotgun Turret Ammo
            GUID.Parse("3bd794833a3c801498d50cb745f51734"), // SMG Turret Ammo
            GUID.Parse("66a6ec9baa4d140438c316b93c639a72"), // Sniper Turret Ammo
            GUID.Parse("6af41273ef98e88499f8a7d4e71430c6"), // Mortar Turret Grenade
            GUID.Parse("68125db51cd1b714d8cd8f1dd1e1d374") // Gatling Turret Ammo
        };

        private static readonly List<GUID> TURRET_MODULES = new List<GUID> {
            GUID.Parse("3bf96763d5dfb564aad936a087cb614e"), // Pistol Turret Module
            GUID.Parse("6dcf6c5ca3b8f1741af35a4011cdaa1e"), // Shotgun Turret Module
            GUID.Parse("4912009609358ea46bc2ceac554045e6"), // SMG Turret Module
            GUID.Parse("b40d3f9773a3a014c9ce1801b2696538"), // Sniper Turret Module
            GUID.Parse("a2a0d7a9fab7dee4f8b2f7a6d0912b5a"), // Mortar Turret Module
            GUID.Parse("4b17c7bc9d6c2ee46b7807292f26b432") // Gatling Turret Module
        };

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var ammoDef in GameResources.Instance.Items.Where(def => TURRET_AMMO_ITEMS.Contains(def.AssetId)).Cast<AmmoDefinition>()) {
                var stats = ammoDef.AmmoStats;
                stats.RateOfFire  *= Plugin.config.turretFireRateMultiplier;
                stats.Damage      *= Plugin.config.turretDamageMultiplier;
                ammoDef.AmmoStats =  stats;
            }

            foreach (var turret in GameResources.Instance.Modules.Where(def => TURRET_MODULES.Contains(def.AssetId)).WithComponent<Turret>()) {
                var reloader = (WeaponReloaderOnlineCargo) turret.GetPrivateField<Weapon>(TURRET_M_WEAPON).Reloader;

                var reloadTime = reloader.GetPrivateField<float>(WEAPON_RELOADER_ONLINE_CARGO_M_RELOAD_TIME);
                reloader.SetPrivateField(WEAPON_RELOADER_ONLINE_CARGO_M_RELOAD_TIME, reloadTime / Plugin.config.turretFireRateMultiplier);
            }
        }
    }
}