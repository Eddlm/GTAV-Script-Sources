using GTA;
using GTA.Native;
using System;
using System.IO;
using System.Windows.Forms;



public class ScriptTest : Script
{
    string ScriptName = "Disarm";
    string ScriptVer = "";

    bool DisarmPlayer = true;
    bool StunDisarms = true;
    int WritheProb = 20;

    public ScriptTest()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        Interval = 500;
        LoadSettings();
    }

    void OnTick(object sender, EventArgs e)
    {
        foreach (Ped victim in World.GetNearbyPeds(Game.Player.Character, 50f))
        {
            Script.Wait(0);
            if(victim.IsAlive && !Function.Call<bool>(Hash.IS_PED_IN_WRITHE, victim) && victim.Weapons.Current.Hash != WeaponHash.Unarmed)
            {
                if ((victim.IsPlayer == DisarmPlayer || !victim.IsPlayer) && LastDamagedBone(victim) == 18905 || LastDamagedBone(victim) == 57005 || (victim.IsBeingStunned && StunDisarms))
                {
                    PlayAmbientSpeech(victim, "GENERIC_CURSE_MED");                    
                    victim.Weapons.Drop();
                    Function.Call(Hash.CLEAR_PED_LAST_DAMAGE_BONE, victim);
                    if(!victim.IsPlayer && RandomInt(0,100)<WritheProb) Function.Call(Hash.TASK_WRITHE, victim, Game.Player.Character,25,0);

                }
            }
        }
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
    public static void PlayAmbientSpeech(Ped ped, string speech)
    {
        Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, ped.Handle, speech, "SPEECH_PARAMS_FORCE");
    }

    int LastDamagedBone(Ped ped)
    {
        OutputArgument outputArgument = new OutputArgument();
        Function.Call<bool>(Hash._0xD75960F6BD9EA49C, (InputArgument)ped, (InputArgument)outputArgument);
        return outputArgument.GetResult<int>();
    }


    /// TOOLS ///
    void LoadSettings()
    {
        if (File.Exists(@"scripts\\DisarmExtended.ini"))
        {
            ScriptSettings config = ScriptSettings.Load(@"scripts\DisarmExtended.ini");
            DisarmPlayer = config.GetValue<bool>("SETTINGS", "DisarmPlayer", true);
            StunDisarms = config.GetValue<bool>("SETTINGS", "StunDisarms", true);
            WritheProb = config.GetValue<int>("SETTINGS", "WritheProb", 20);
            
        }
        else
        {
            WarnPlayer(ScriptName + " " + ScriptVer, "NO CONFIG FOUND", "~o~No config file has been found for "+ScriptName+".");
        }
    }
    public static Random rnd = new Random();
    public static int RandomInt(int min, int max)
    {
        return rnd.Next(min, max);
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
