using System.Reflection;
using AC_TranslationHelper;
using AC.UI;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Character;
using HarmonyLib;
using XUnity.AutoTranslator.Plugin.Core;

[assembly: AssemblyTitle(TranslationHelperPlugin.DisplayName)]
[assembly: AssemblyProduct(TranslationHelperPlugin.DisplayName)]
[assembly: AssemblyDescription("Workaround for some things not getting translated by AutoTranslator.")]
[assembly: AssemblyVersion(TranslationHelperPlugin.Version)]

namespace AC_TranslationHelper
{
    [BepInPlugin(GUID, DisplayName, Version)]
    [BepInDependency("gravydevsupreme.xunity.autotranslator", "5.4")]
    public class TranslationHelperPlugin : BasePlugin
    {
        public const string Version = "0.1";
        public const string GUID = "TranslationHelper";
        internal const string DisplayName = "Translation Helper";
        internal static ManualLogSource Logger;

        public override void Load()
        {
            Logger = Log;
            Harmony.CreateAndPatchAll(typeof(Hooks));
        }

        private static class Hooks
        {
            /// <summary>
            /// Translate full character name before it is used by the game. Takes care of most of the UI.
            /// </summary>
            [HarmonyPostfix]
            [HarmonyPatch(typeof(HumanDataParameter), nameof(HumanDataParameter.fullname), MethodType.Getter)]
            public static void Postfix_fullname_get(HumanDataParameter __instance, ref string __result)
            {
                string? first = null;
                string? last = null;
                if ((string.IsNullOrWhiteSpace(__instance.firstname) || AutoTranslator.Default.TryTranslate(__instance.firstname, out first)) &&
                    (string.IsNullOrWhiteSpace(__instance.lastname) || AutoTranslator.Default.TryTranslate(__instance.lastname, out last)))
                {
                    var original = __result;

                    __result = first ?? "";
                    if (!string.IsNullOrWhiteSpace(last))
                    {
                        if (!string.IsNullOrWhiteSpace(__result))
                            __result += " ";
                        __result += last;
                    }

                    Logger.LogDebug($"Fullname translated: {original} -> {__result}");
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(Passport), nameof(Passport.SetParameter))]
            public static void Postfix_fullname_get(Passport __instance, AC.User.ActorData data)
            {
                // Workaround for names in roster screen not getting translated.
                // Seems like AT misses the text update, this makes AT re-check the text
                if (__instance._txtName != null)
                    __instance._txtName.text = __instance._txtName.text;
            }
        }
    }
}
