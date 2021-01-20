using System;
using System.IO;
using Base_Mod;
using GMod.Models;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMod {
    [UsedImplicitly]
    public class Plugin : BaseGameMod {
        protected override string ModName    { get; } = "GMod";
        protected override bool   UseHarmony { get; } = true;
        public static      Config config = new Config();

        protected override void Init() {
            try {
                if (File.Exists(ConfigFile)) {
                    var json = File.ReadAllText(ConfigFile);
                    config = JsonConvert.DeserializeObject<Config>(json);
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }

            try {
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigFile, json);
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            if (scene.name != "Island") return;
            SceneManager.sceneLoaded -= OnSceneLoaded;

            DoAllIslandSceneLoadedPatches();
        }
    }
}