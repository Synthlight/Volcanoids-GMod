using Base_Mod.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    public static class RevealMapPatch {
        private static PlayerCallback playerCallback;

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            if (playerCallback != null) {
                UnPatch();
            }

            playerCallback = new PlayerCallback();
            TrackingHandler<Player>.Subscribe(playerCallback, true);
        }

        [OnUnloaded]
        [UsedImplicitly]
        public static void UnPatch() {
            TrackingHandler<Player>.Unsubscribe(playerCallback, true);
            playerCallback = null;
        }

        private class PlayerCallback : ITrackingHandlerCallback<Player> {
            public void OnAdded(Player player) {
                if (Plugin.config.revealFullMap) {
                    Island.Current.TryGetComponentSafe<MapMemory>()?.RevealAll();
                }
            }

            public void OnRemoved(Player instance) {
            }
        }
    }
}