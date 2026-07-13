using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using Elements.Core;
using FrooxEngine;
using HarmonyLib;

namespace ImportCompactor;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BasePlugin
{
#nullable disable
    internal static new ManualLogSource Log;
    internal static ConfigEntry<float> ScaleFactor;
#nullable enable

    public override void Load()
    {
        Log = base.Log;

        ScaleFactor = Config.Bind("General", "ScaleFactor", 0.75f,
            "Multiplies the position of each import by this value.");
        
        HarmonyInstance.PatchAll();
    }
    
    [HarmonyPatch(typeof(UniversalImporter), nameof(UniversalImporter.GridOffset))]
    class ImportCompactorPatch
    {
        public static void Postfix(ref float3 __result)
        {
            __result *= ScaleFactor.Value;
        }
    }
}
