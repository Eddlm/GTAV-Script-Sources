using GTA;
using GTA.Native;
using System;
using System.IO;
using System.Windows.Forms;



public class ScriptTest : Script
{
    string ScriptName = "NPC Tweaks";
    string ScriptVer = "1.0";

    public ScriptTest()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;

    }

    void OnTick(object sender, EventArgs e)
    {

    }
    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }
    void OnKeyUp(object sender, KeyEventArgs e)
    {

    }
    protected override void Dispose(bool dispose)
    {


        base.Dispose(dispose);
    }




    /// TOOLS ///
    void LoadSettings()
    {
        if (File.Exists(@"scripts\\NPCTweaks.ini"))
        {

            ScriptSettings config = ScriptSettings.Load(@"scripts\NPCTweaks.ini");
            // = config.GetValue<bool>("GENERAL_SETTINGS", "NAME", true);
        }
        else
        {
            WarnPlayer(ScriptName + " " + ScriptVer, "SCRIPT RESET", "");
        }
    }

    void WarnPlayer(string script_name, string title, string message)
    {
        Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
        Function.Call(Hash._SET_NOTIFICATION_MESSAGE, "CHAR_SOCIAL_CLUB", "CHAR_SOCIAL_CLUB", true, 0, title, "~b~" + script_name);
    }

    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, true, -1);
    }
}
