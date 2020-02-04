using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod {
    [BepInPlugin(UUID, "Greg's Mod", "1.0.0.0")]
    public class GMod : BaseUnityPlugin {
        private const string UUID = "com.gmod";
        private static ManualLogSource logSource;

        public static readonly Dictionary<string, int> itemSortOrders = new Dictionary<string, int>();

        static GMod() {
            var i = 0;
            itemSortOrders.Add("Revolver", i++);
            itemSortOrders.Add("Revolver ammo", i++);
            itemSortOrders.Add("Revolver high-power ammo", i++);
            itemSortOrders.Add("Explosive revolver ammo", i++);

            itemSortOrders.Add("Shotgun", i++);
            itemSortOrders.Add("Buckshot", i++);
            itemSortOrders.Add("Slugs", i++);
            itemSortOrders.Add("AP Slugs", i++);

            itemSortOrders.Add("Hand mortar", i++);
            itemSortOrders.Add("Mortar grenade", i++);
            itemSortOrders.Add("High-explosive mortar grenade", i++);

            itemSortOrders.Add("Hacking tool", i++);

            itemSortOrders.Add("Gatling gun", i++);
            itemSortOrders.Add("Gatling ammo", i++);

            itemSortOrders.Add("Detonator", i++);
            itemSortOrders.Add("Explosive", i++);

            itemSortOrders.Add("Grenade", i++);

            itemSortOrders.Add("Medkit", i++);
            itemSortOrders.Add("Module repair kit", i++);
            itemSortOrders.Add("Breach replacement", i++);

            itemSortOrders.Add("Destroyed production module T1", i++);
            itemSortOrders.Add("Destroyed production module T2", i++);
            itemSortOrders.Add("Destroyed production module T3", i++);
            itemSortOrders.Add("Destroyed refinery module T1", i++);
            itemSortOrders.Add("Destroyed refinery module T2", i++);
            itemSortOrders.Add("Destroyed refinery module T3", i++);
            itemSortOrders.Add("Destroyed research module T1", i++);
            itemSortOrders.Add("Destroyed research module T2", i++);
            itemSortOrders.Add("Destroyed research module T3", i++);

            itemSortOrders.Add("Builder head", i++);
            itemSortOrders.Add("Technician head", i++);
            itemSortOrders.Add("Engineer head", i++);
            itemSortOrders.Add("Miner head", i++);
            itemSortOrders.Add("Foundryman head", i++);
            itemSortOrders.Add("Metallurgist head", i++);
            itemSortOrders.Add("Explorer head", i++);
            itemSortOrders.Add("Chemist head", i++);
            itemSortOrders.Add("Scientist head", i++);
            itemSortOrders.Add("Guardian head", i++);
            itemSortOrders.Add("Protector head", i++);
            itemSortOrders.Add("Sentry head", i++);

            itemSortOrders.Add("Intel production T1", i++);
            itemSortOrders.Add("Intel production T2", i++);
            itemSortOrders.Add("Intel production T3", i++);
            itemSortOrders.Add("Intel refinery T1", i++);
            itemSortOrders.Add("Intel refinery T2", i++);
            itemSortOrders.Add("Intel refinery T3", i++);
            itemSortOrders.Add("Intel research T1", i++);
            itemSortOrders.Add("Intel research T2", i++);
            itemSortOrders.Add("Intel research T3", i++);

            itemSortOrders.Add("Drillship core 1", i++);
            itemSortOrders.Add("Drillship core 2", i++);
            itemSortOrders.Add("Drillship core 3", i++);

            itemSortOrders.Add("Engine 1", i++);
            itemSortOrders.Add("Engine 2", i++);
            itemSortOrders.Add("Engine 3", i++);

            itemSortOrders.Add("Drillship segment", i++);

            itemSortOrders.Add("Copper drill", i++);
            itemSortOrders.Add("Hardened drill parts", i++);
            itemSortOrders.Add("Hardened drill", i++);
            itemSortOrders.Add("Corundum drill parts", i++);
            itemSortOrders.Add("Corundum drill", i++);
            itemSortOrders.Add("Diamond drill parts", i++);
            itemSortOrders.Add("Diamond drill", i++);

            itemSortOrders.Add("Basic tracks", i++);
            itemSortOrders.Add("Modified tracks parts", i++);
            itemSortOrders.Add("Modified tracks", i++);
            itemSortOrders.Add("Improved tracks parts", i++);
            itemSortOrders.Add("Improved tracks", i++);
            itemSortOrders.Add("Advanced tracks parts", i++);
            itemSortOrders.Add("Advanced tracks", i++);

            itemSortOrders.Add("Copper hull", i++);
            itemSortOrders.Add("Reinforced hull parts", i++);
            itemSortOrders.Add("Reinforced hull", i++);
            itemSortOrders.Add("Steel hull parts", i++);
            itemSortOrders.Add("Steel hull", i++);
            itemSortOrders.Add("Titanium hull parts", i++);
            itemSortOrders.Add("Titanium hull", i++);

            itemSortOrders.Add("Upgrade starter production", i++);
            itemSortOrders.Add("Upgrade basic production", i++);
            itemSortOrders.Add("Upgrade advanced production", i++);

            itemSortOrders.Add("Upgrade starter refinery", i++);
            itemSortOrders.Add("Upgrade basic refinery", i++);
            itemSortOrders.Add("Upgrade advanced refinery", i++);

            itemSortOrders.Add("Upgrade starter research", i++);
            itemSortOrders.Add("Upgrade basic research", i++);
            itemSortOrders.Add("Upgrade advanced research", i++);

            itemSortOrders.Add("Coal", i++);

            itemSortOrders.Add("Copper ore", i++);
            itemSortOrders.Add("Copper scrap metal", i++);
            itemSortOrders.Add("Copper ingot", i++);
            itemSortOrders.Add("Iron ore", i++);
            itemSortOrders.Add("Iron scrap metal", i++);
            itemSortOrders.Add("Iron ingot", i++);
            itemSortOrders.Add("Titanium Ore", i++);
            itemSortOrders.Add("Titanium Scrap Metal", i++);
            itemSortOrders.Add("Titanium ingot", i++);

            itemSortOrders.Add("SulfurOre", i++);
            itemSortOrders.Add("Sulfur powder", i++);
            itemSortOrders.Add("Raw Crystals", i++);
            itemSortOrders.Add("Crystal", i++);
            itemSortOrders.Add("Raw Diamond", i++);
            itemSortOrders.Add("Diamond", i++);

            itemSortOrders.Add("Copper bolts", i++);
            itemSortOrders.Add("Copper plates", i++);
            itemSortOrders.Add("Copper tubes", i++);
            itemSortOrders.Add("Copper radiator", i++);
            itemSortOrders.Add("Copper wire", i++);

            itemSortOrders.Add("Iron bolts", i++);
            itemSortOrders.Add("Iron plates", i++);
            itemSortOrders.Add("Iron tubes", i++);
            itemSortOrders.Add("Iron frame", i++);
            itemSortOrders.Add("Iron gear", i++);

            itemSortOrders.Add("Titan bolts", i++);
            itemSortOrders.Add("Titan plates", i++);
            itemSortOrders.Add("Titan tubes", i++);

            itemSortOrders.Add("Turret ammo", i++);

            itemSortOrders.Add("Pump", i++);
            itemSortOrders.Add("Hydraulics", i++);
            itemSortOrders.Add("Transmission", i++);

            itemSortOrders.Add("Shredder", i++);
            itemSortOrders.Add("Boiler", i++);
            itemSortOrders.Add("Furnace", i++);

            itemSortOrders.Add("Converter", i++);
            itemSortOrders.Add("Sensors", i++);
            itemSortOrders.Add("Analyzer", i++);

            itemSortOrders.Add("Coal Powerplant Tier 1", i++);
            itemSortOrders.Add("Coal Powerplant Tier 2", i++);
            itemSortOrders.Add("Coal Powerplant Tier 3", i++);
            itemSortOrders.Add("Production Tier 1", i++);
            itemSortOrders.Add("Production Tier 2", i++);
            itemSortOrders.Add("Production Tier 3", i++);
            itemSortOrders.Add("Refinery Tier 1", i++);
            itemSortOrders.Add("Refinery Tier 2", i++);
            itemSortOrders.Add("Refinery Tier 3", i++);
            itemSortOrders.Add("Research Tier 1", i++);
            itemSortOrders.Add("Research Tier 2", i++);
            itemSortOrders.Add("Research Tier 3", i++);
            itemSortOrders.Add("Storage Tier 1", i++);
            itemSortOrders.Add("Storage Tier 2", i++);
            itemSortOrders.Add("Storage Tier 3", i++);

            itemSortOrders.Add("Turret Module", i++);

            itemSortOrders.Add("Door module", i++);
            itemSortOrders.Add("Window module", i++);

            itemSortOrders.Add("BannerLarge", i++);
            itemSortOrders.Add("BannerSmall", i++);
            itemSortOrders.Add("BoardingDrone", i++);
            itemSortOrders.Add("Chest", i++);
            itemSortOrders.Add("Controller custom", i++);
            itemSortOrders.Add("Controller Manufacture Medium", i++);
            itemSortOrders.Add("Controller manufacture small", i++);
            itemSortOrders.Add("Controller shutdown", i++);
            itemSortOrders.Add("Controller System Medium", i++);
            itemSortOrders.Add("Controller system small", i++);
            itemSortOrders.Add("Copper door", i++);
            itemSortOrders.Add("Copper floor hole", i++);
            itemSortOrders.Add("Copper floor", i++);
            itemSortOrders.Add("Copper ladder", i++);
            itemSortOrders.Add("Copper wall hole", i++);
            itemSortOrders.Add("Copper wall", i++);
            itemSortOrders.Add("Copper window", i++);
            itemSortOrders.Add("Display large", i++);
            itemSortOrders.Add("Display medium", i++);
            itemSortOrders.Add("Display small", i++);
            itemSortOrders.Add("Hub production", i++);
            itemSortOrders.Add("Hub refinery", i++);
            itemSortOrders.Add("Hub research", i++);
            itemSortOrders.Add("Hub scrap", i++);
            itemSortOrders.Add("Lever stand", i++);
            itemSortOrders.Add("Light", i++);
            itemSortOrders.Add("Long distance radio", i++);
            itemSortOrders.Add("MapStand", i++);
            itemSortOrders.Add("Panel core small", i++);
            itemSortOrders.Add("Panel large", i++);
            itemSortOrders.Add("Panel manufacture medium", i++);
            itemSortOrders.Add("Panel manufacture small", i++);
            itemSortOrders.Add("Panel system medium", i++);
            itemSortOrders.Add("Panel system small", i++);
            itemSortOrders.Add("Periscope", i++);
            itemSortOrders.Add("Pilot seat", i++);
            itemSortOrders.Add("Production station", i++);
            itemSortOrders.Add("Refinery station", i++);
            itemSortOrders.Add("Research station", i++);
            itemSortOrders.Add("Scrap station", i++);
            itemSortOrders.Add("Spawn tube", i++);
            itemSortOrders.Add("Stats panel", i++);
            itemSortOrders.Add("Storage door", i++);
            itemSortOrders.Add("Worktable", i++);

            itemSortOrders.Add("Wrench", i++);
            itemSortOrders.Add("Pickaxe", i++);
        }

        public static int GetItemSortOrder(string item) {
            if (itemSortOrders.ContainsKey(item)) {
                return itemSortOrders[item];
            } else {
                return 9999;
            }
        }

        public void Awake() {
            logSource = Logger;

            Log(LogLevel.Info, "GMod loaded.");

            var harmony = new Harmony(UUID);
            harmony.PatchAll();

            foreach (var patchedMethod in harmony.GetPatchedMethods()) {
                Log(LogLevel.Info, $"Patched: {patchedMethod.DeclaringType?.FullName}:{patchedMethod}");
            }
        }

        public static void Log(LogLevel level, string msg) {
            logSource.Log(level, msg);
        }
    }
}