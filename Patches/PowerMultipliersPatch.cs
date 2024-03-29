using Base_Mod;
using Base_Mod.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches;

public static class PowerMultipliersPatch {
    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var powerPlant in RuntimeAssetDatabase.Get<ItemDefinition>().WithComponent<PowerPlant>()) {
            powerPlant.EnergyPerSecond *= Plugin.config.powerPerSecondMultiplier;
            powerPlant.FuelEfficiency  *= Plugin.config.powerEfficiencyMultiplier;

            if (Plugin.config.disablePowerPlantStopThreshold) {
                powerPlant.StartThreshold = 0;
                powerPlant.StopThreshold  = 0;
            }
        }
    }
}