using System;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Newtonsoft.Json;

namespace GMod {
    [BepInPlugin(UUID, "Greg's Mod", "1.0.0.0")]
    public class Plugin : BaseUnityPlugin {
        private const  string          UUID = "com.gmod";
        private static ManualLogSource logSource;
        public static  Config          config = new Config();

        public void Awake() {
            logSource = Logger;

            Log(LogLevel.Info, "GMod loaded.");

            var configFile = Path.Combine(Paths.ConfigPath, "gmod.json");

            try {
                if (File.Exists(configFile)) {
                    var json = File.ReadAllText(configFile);
                    config = JsonConvert.DeserializeObject<Config>(json);
                }
            } catch (Exception e) {
                Log(LogLevel.Error, e.ToString());
            }

            try {
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(configFile, json);
            } catch (Exception e) {
                Log(LogLevel.Error, e.ToString());
            }

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