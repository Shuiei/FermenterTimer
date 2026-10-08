using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace FermenterTimer;

// Client-only: adds the time left until the fermenter is ready to its hover text.
// Works on any server, since it only reads the start time the fermenter already stores.
[BepInPlugin(Guid, "FermenterTimer", Version)]
public sealed class FermenterTimerPlugin : BaseUnityPlugin
{
	public const string Guid = "Tie.FermenterTimer";

	// The id before 1.1.0, which named the settings file.
	private const string OldGuid = "local.fermentertimer";

	public const string Version = "1.1.1";

	private static ConfigEntry<bool> _showSeconds;

	private static readonly AccessTools.FieldRef<Fermenter, ZNetView> NView = AccessTools.FieldRefAccess<Fermenter, ZNetView>("m_nview");

	private static readonly Func<Fermenter, double> FermentationTime = AccessTools.MethodDelegate<Func<Fermenter, double>>(AccessTools.Method(typeof(Fermenter), "GetFermentationTime"));

	private void Awake()
	{
		// Settings made under the old id move to the new file, so they are kept.
		string old = System.IO.Path.Combine(Paths.ConfigPath, OldGuid + ".cfg");
		if (System.IO.File.Exists(old) && !System.IO.File.Exists(Config.ConfigFilePath))
		{
			try
			{
				System.IO.File.Move(old, Config.ConfigFilePath);
				Config.Reload();
			}
			catch (System.IO.IOException)
			{
			}
		}
		_showSeconds = Config.Bind("General", "ShowSeconds", true, "Show seconds in the remaining time.");
		new Harmony(Guid).PatchAll(typeof(FermenterTimerPlugin));
	}

	[HarmonyPostfix]
	[HarmonyPatch(typeof(Fermenter), nameof(Fermenter.GetHoverText))]
	private static void GetHoverText(Fermenter __instance, ref string __result)
	{
		ZNetView nview = NView(__instance);
		if (nview == null || !nview.IsValid() || nview.GetZDO().GetInt(ZDOVars.s_content) == 0 || !PrivateArea.CheckAccess(__instance.transform.position, 0f, flash: false))
		{
			return;
		}
		double elapsed = FermentationTime(__instance);
		if (elapsed < 0.0 || elapsed > __instance.m_fermentationDuration)
		{
			return;
		}
		TimeSpan left = TimeSpan.FromSeconds(Math.Ceiling(__instance.m_fermentationDuration - elapsed));
		__result += "\n<color=orange>Ready in " + Format(left) + "</color>";
	}

	private static string Format(TimeSpan t)
	{
		if (t.TotalHours >= 1.0)
		{
			return $"{(int)t.TotalHours}h {t.Minutes:00}m";
		}
		return _showSeconds.Value ? $"{t.Minutes}m {t.Seconds:00}s" : $"{Math.Max(1, (int)Math.Ceiling(t.TotalMinutes))}m";
	}
}
