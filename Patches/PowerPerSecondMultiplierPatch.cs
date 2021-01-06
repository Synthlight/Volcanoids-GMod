using Base_Mod;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class PowerPerSecondMultiplierPatch {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var powerPlant in GameResources.Instance.Items.WithComponent<PowerPlant>()) {
                powerPlant.EnergyPerSecond *= Plugin.config.powerPerSecondMultiplier;

                if (Plugin.config.disablePowerPlantStopThreshold) {
                    powerPlant.StartThreshold = 0;
                    powerPlant.StopThreshold  = 0;
                }
            }
        }
    }
}