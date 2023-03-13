using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MaxLevelMistlands
{
    [BepInPlugin("MaxLevelMistlands", "Max Level Mistlands", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ValheimMod : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("MaxLevelMistlands");
        private static ManualLogSource Logs;

        private void Awake()
        {
            harmony.PatchAll();
            Logs = Logger;
            Logs.LogInfo("Successfully patched Galdr Table/Black Forge workaround!");
        }

        [HarmonyPatch(typeof(CraftingStation), nameof(CraftingStation.GetLevel))]
        class Mistlands_Patch
        {
            [HarmonyPostfix]
            static void Patch(ref int __result, ref string ___m_name)
            {
                if (___m_name.ToLower().Equals("$piece_magetable") || ___m_name.ToLower().Equals("$piece_blackforge"))
                {
                    __result = __result + 2;
                }
            }
        }
    }
}
