using HarmonyLib;
using NeosModLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrooxEngine;
using FrooxEngine.LogiX.Math.Time;
using BaseX;

namespace ImportCompactor
{
    public class ImportCompactor : NeosMod
    {
        public override string Name => "ImportCompactor";
        public override string Author => "art0007i";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/art0007i/ImportCompactor/";

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<float> KEY_SCALE_FACTOR = new("scale_factor", "Multiplies the position of each import by this value.", () => 0.25f, false, (v)=> v!=0);
        public static ModConfiguration config;
        public override void OnEngineInit()
        {
            config = GetConfiguration();
            Harmony harmony = new Harmony("me.art0007i.ImportCompactor");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(UniversalImporter), nameof(UniversalImporter.GridOffset))]
        class ImportCompactorPatch
        {
            public static void Postfix(ref float3 __result)
            {
                __result = __result * config.GetValue(KEY_SCALE_FACTOR);
            }
        }
    }
}