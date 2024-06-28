using System.Collections.Generic;
using System.Linq;
using Base_Mod;
using Base_Mod.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches;

public static class TurretFireRateAndDamageMultiplierPatch {
    private static readonly List<GUID> TURRET_AMMO_ITEMS = [
        GUID.Parse("f46cdfb659dd5f3428f5a1c9c1fe7d32"), // Pistol Turret Ammo
        GUID.Parse("cdd3ca2c103709d4395f67d08bef56f9"), // Shotgun Turret Ammo
        GUID.Parse("3bd794833a3c801498d50cb745f51734"), // SMG Turret Ammo
        GUID.Parse("66a6ec9baa4d140438c316b93c639a72"), // Sniper Turret Ammo
        GUID.Parse("6af41273ef98e88499f8a7d4e71430c6"), // Mortar Turret Grenade
        GUID.Parse("68125db51cd1b714d8cd8f1dd1e1d374")
    ];

    private static readonly List<GUID> TURRET_MODULES = [
        GUID.Parse("3bf96763d5dfb564aad936a087cb614e"), // Pistol Turret Module
        GUID.Parse("6dcf6c5ca3b8f1741af35a4011cdaa1e"), // Shotgun Turret Module
        GUID.Parse("4912009609358ea46bc2ceac554045e6"), // SMG Turret Module
        GUID.Parse("b40d3f9773a3a014c9ce1801b2696538"), // Sniper Turret Module
        GUID.Parse("a2a0d7a9fab7dee4f8b2f7a6d0912b5a"), // Mortar Turret Module
        GUID.Parse("4b17c7bc9d6c2ee46b7807292f26b432")
    ];

    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var ammoDef in RuntimeAssetDatabase.Get<ItemDefinition>().Where(def => TURRET_AMMO_ITEMS.Contains(def.AssetId)).Cast<AmmoDefinition>()) {
            var stats = ammoDef.AmmoStats;
            stats.RateOfFire  *= Plugin.config.turretFireRateMultiplier;
            stats.Damage      *= Plugin.config.turretDamageMultiplier;
            ammoDef.AmmoStats =  stats;
        }

        foreach (var turret in RuntimeAssetDatabase.Get<ItemDefinition>().Where(def => TURRET_MODULES.Contains(def.AssetId)).WithComponent<Turret>()) {
            switch (turret.Weapon?.Reloader) {
                case WeaponReloaderAmmo reloaderAmmo:
                    reloaderAmmo.Definition.ReloadTime = reloaderAmmo.ReloadDuration / Plugin.config.turretFireRateMultiplier;
                    break;
                default:
                    Debug.LogError($"Unable to cast reloader of type `{turret.Weapon?.Reloader?.GetType()}` for turret: {turret.name}");
                    break;
            }
        }
    }
}