using BepInEx;
using Gameplay;
using HarmonyLib;
using Platforms;
using Platforms.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using UI;
using UnityEngine;
namespace ItemsMakeFast
{
    [BepInPlugin("com.bestcreeper.speeditems", "ItemsMakeFast", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static Main Instance;
        public float Speed = 1.0f;
        readonly string configFilePath = Path.Combine(Paths.PluginPath,"ItemsMakeFast", "config.txt");
        public float speedup = 1f;
        public float SpeedPerItem = 0.05f;
        public Dictionary<string, string> settings = new Dictionary<string, string>();
        public KeyCode speedKey = KeyCode.None;
        public ValueChooser itsbtn;
        void Awake()
        {
            Instance = this;
            var harmony = new Harmony("com.bestcreeper.speeditems");
            Logger.LogInfo("plugin loaded");
            harmony.PatchAll();
            LoadConfig();
            
        }

        public void SaveConfig()
        {
            
            var text = Res.configTemplate
                .Replace("{addedspeed}", speedup.ToString())
                .Replace("{speedperitem}", SpeedPerItem.ToString())
                .Replace("{speedkey}", speedKey.ToString());
            File.WriteAllText(configFilePath, text);
            Logger.LogMessage("Config Saved");
        }

        private void LoadConfig()
        {
            
            string[] file = File.ReadAllLines(configFilePath);

            settings.Clear();
            foreach (string line in file)
            {
                if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                {
                    Logger.LogMessage(line);
                    string cleanline = line.Replace(" ", "");
                    string[] parts = cleanline.Split('=');
                    
                    settings.Add(parts[0], parts[1]);
                }
            }
            List<string> missing = new List<string>();
            if (settings.ContainsKey("speedkey") && Enum.TryParse((string)settings["speedkey"], out KeyCode parsedKey))
            {
                speedKey = parsedKey;
            } else missing.Add("speedkey");

            if (settings.ContainsKey("addedspeed") && float.TryParse(Res.LocaFloat((string)settings["addedspeed"]), out float parsedSpeed))
            {
                speedup = parsedSpeed;
            } else missing.Add("addedspeed");

            
            if (settings.ContainsKey("speedperitem") && float.TryParse(Res.LocaFloat((string)settings["speedperitem"]), out float parsedSpeedPerItem))
            {
                SpeedPerItem = parsedSpeedPerItem;
            } else missing.Add("speedperitem"); 

            if (missing.Count > 0) {Missingconfig(missing);}
            Logger.LogMessage("Config Loaded");
        }

        private void Missingconfig(List<string> keys)
        {
            foreach (string key in keys)
            {
                Logger.LogError($"{key} is missing from config.txt");
            }

        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(speedKey))
            {
                Runtime.Instance.NotificationScreen.AddNotification($"Speed:{Speed}",Runtime.Configuration.CooldownSprite);
                
            }
            
            Speed = speedup  + (Game.Instance.Data.Items.Count * SpeedPerItem);
        }



    }
    [HarmonyPatch(typeof(Game), "Update")]
    public class PatchUpdate
    {
        public static void Prefix()
        {
            Main.Instance?.OnUpdate();
        }
    }

    [HarmonyPatch(typeof(Game), "CalculateGameSpeed")]
    public class Patchspeed
    {
        public static bool Prefix(ref float __result)
        {
            Console.WriteLine(Main.Instance.Speed);
            __result = Main.Instance.Speed;
            return false;
        }
    }
    [HarmonyPatch(typeof(OptionsMenu), "Init")]
    public class Patch_OptionsMenu_Init
    {
        static void Postfix(OptionsMenu __instance)
        {
            var list = new List<float>
            {
                0.0f,0.25f,0.5f,0.75f,1.0f,1.25f,1.5f,1.75f,2.0f,2.25f,2.5f,2.75f,3.0f,3.25f,3.5f,
            };
            __instance.GameSpeedChooser.Init<float>($"*Added Speed(adds on top of speed per item):\n(0 to 3.5  1 = normal)\n press {Main.Instance.speedKey} to show current speed", list);
            var list2 = new List<float>
            {
                0.05f, 0.10f, 0.15f, 0.20f, 0.25f, 0.30f, 0.35f, 0.40f, 0.45f, 0.50f

            };
            __instance.SkipIntroChooser.Init<float>($"*Speed added per item", list2);
        }
    }

    [HarmonyPatch(typeof(I2.Loc.LocalizationManager), "GetTermTranslation")]
    public class PatchLoca
    {
        static bool Prefix(ref string __result, string Term, bool FixForRTL, int maxLineLengthForRTL, bool ignoreRTLnumbers, bool applyParameters, GameObject localParametersRoot, string overrideLanguage, bool allowLocalizedParameters)
        {
            if (Term.StartsWith("*"))
            {
                __result = Term.Substring(1);
                return false;
            }
            return true;
        }

    }
    [HarmonyPatch(typeof(OptionsMenu), "ApplySettings")]
    public class PatchApplySettings
    {
        static void Prefix(OptionsMenu __instance)
        {
            float speed = __instance.GameSpeedChooser.GetSelectedValue<float>();
            float itemspeed = __instance.SkipIntroChooser.GetSelectedValue<float>();
            Main.Instance.speedup = speed;
            Main.Instance.SpeedPerItem = itemspeed;
            Runtime.Instance.NotificationScreen.AddNotification($"New Speed:{Main.Instance.speedup + (Game.Instance.Data.Items.Count * Main.Instance.SpeedPerItem)}", Runtime.Configuration.CooldownSprite);
            Runtime.Instance.NotificationScreen.AddNotification($"New Speed per item:{Main.Instance.SpeedPerItem} ", Runtime.Configuration.CooldownSprite);
            __instance.GameSpeedChooser.SetSelectedValue(GameSpeed.Normal);
            __instance.SkipIntroChooser.SetSelectedValue(OnOff.On);
            Main.Instance.SaveConfig();           
        }
        
    }

}
