using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace GMod {
    [UsedImplicitly]
    public class Plugin : GameMod {
        public static           Config config      = new Config();
        private static readonly string CONFIG_FILE = Path.Combine(AssemblyDirectory, "GMod.json");

        public override void Load() {
            Debug.Log("GMod loaded.");

            Assembly.LoadFrom(Path.Combine(AssemblyDirectory, "0Harmony.dll"));

            try {
                if (File.Exists(CONFIG_FILE)) {
                    var json = File.ReadAllText(CONFIG_FILE);
                    config = JsonConvert.DeserializeObject<Config>(json);
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }

            try {
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }

            var harmony = new Harmony(GUID.Create().ToString());
            harmony.PatchAll();

            foreach (var patchedMethod in harmony.GetPatchedMethods()) {
                Debug.Log($"Patched: {patchedMethod.DeclaringType?.FullName}:{patchedMethod}");
            }
        }

        public override void Unload() {
        }

        private static string AssemblyDirectory {
            get {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri      = new UriBuilder(codeBase);
                var path     = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}