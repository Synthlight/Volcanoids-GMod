using System.Reflection;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainCoreAccessUseComponentPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccessUseComponent).GetMethod("IsEnabledForUse", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float ___m_claimHealthLevel) {
            ___m_claimHealthLevel = 0.9f;
            return true;
        }
    }
}