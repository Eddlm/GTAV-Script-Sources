using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



public class Wheelies : Script
{
    int wheeliepoints;
    float forcelimit = 1f;
    float plusforce = 0f;
    float level = 15f;
    bool AllowAllVehicles = false;
    //List<string> stabilizers =new List<string>();


    string[] candowheelie = new string[]
{
    "nightshade",
    "FBI",
    "FBI2",
    "AMBULAN",
        "LGUARD",
"POLICE",
"POLICE2",
"POLICE3",
"POLICE4",
    "CHINO",
        "CHINO2",

    "DUKES",
        "DUKES2",
    "HOTKNIFE",
    "BRAWLER",
    "PANTO",
    "RHAPSODY",
    "BUCCANEE",
        "BUCCANEE2",

    "COQUETTE3",
    "DOMINATO",
    "DOMINATO2",
    "FACTION",
    "FACTION2",
    "GAUNTLET",
    "GAUNTLET2",
    "LURCHER",
    "PHOENIX",
    "RLOADER2",
    "RUINER",
    "SABREGT",
    "SLAMVAN",
    "SLAMVAN2",
    "STALION",
    "STALION2",
    "VIGERO",
    "VIRGO",
    "BFINJECT",
    "BIFTA",

    "BODHI2",
    "DLOADER",

    "DUBSTA3",
    "DUNE",

    "DUNE2",
    "INSURGENT",
    "INSURGENT2",
    "MARSHALL",
    "MESA",
    "MONSTER",
    "RANCHERX",
    "REBEL01",
    "REBEL02",
    "TECHNICAL",
    "ASTROPE",
    "FUGITIVE",
    "GLENDALE",
    "INTRUDER",
    "PRIMO",
    "PRIMO2",
    "REGINA",
    "ROMERO",
    "SCHAFTER",
    "SCHAFTER2",
    "SCHAFTER3",
    "SCHAFTER4",
    "SCHAFTER5",
    "SCHAFTER6",
    "verlierer2",
    "STANIER",
    "STRATUM",
    "STRETCH",
    "SUPERD",
    "TAILGATE",
    "WARRENER",
    "WASHINGT",
    "TAXI",
    "ALPHA",
    "BANSHEE",
    "BUFFALO",
    "BUFFALO02",
    "BUFFALO3",
    "CARBONIZ",
    "COMET2",
    "COQUETTE",
    "ELEGY2",
    "FURORE",
    "FUSILADE",
    "FUTO",
    "BLISTA2",
    "BLISTA3",
    "JESTER",
    "JESTER2",
    "KHAMEL",
    "KURUMA",
    "KURUMA2",
    "MASSACRO",
    "MASSACRO2",
    "NINEF",
    "NINEF2",
    "PENUMBRA",
    "RAPIDGT",
    "RAPIDGT2",
    "SCHWARZE",
    "SULTAN",
    "SURANO",
    "ROOSEVELT",
    "BTYPE2",
    "CASCO",
    "COQUETTE2",
    "JB700",
    "MANANA",
    "MONROE",
    "STINGER",
    "STINGERG",
    "FELTZER3",
    "TORNADO",
    "TORNADO2",
    "TORNADO3",
    "BALLER",
    "BALLER2",
    "BJXL",
    "CAVCADE",
    "DUBSTA",
    "FQ2",
    "GRANGER",
    "GRESLEY",
    "HUNTLEY",
    "LANDSTAL",
    "MESA",
    "PATRIOT",
    "RADI",
    "rocoto",
    "SEMINOLE",
    "SERRANO",
    "BISON",
    "BOBCATXL",
    "BURRITO",
    "GBURRITO",
    "GBURRITO2",
    "MINIVAN",
    "PARADISE",
    "PONY",
    "RUMPO",
    "SPEEDO",
    "SPEEDO2",
    "SURFER",
    "SURFER2",
    "YOUGA",
    "MOONBEAM",
        "MOONBEAM2",
        "PICADOR",
        "COGCABRI",
        "EXEMPLAR",
        "F620",
        "FELON",
        "FELON2",
        "JACKAL",
        "ORACLE",
        "ORACLE2",
        "SENTINEL",
        "SENTINEL2",
        "WINDSOW",
        "ZION",
        "ZION2",
};

    string[] stabilizers = new string[]
{
    "CHINO",
    "DUKES",
        "LURCHER",
                "VIGERO",

};


    string[] noupgradesneeded = new string[]
{
    "DUKES",
        "LURCHER",
                    "HOTKNIFE",
                        "BUCCANEE",
                        "RLOADER2",
        "SABREGT",
            "BTYPE2",

            "MONSTER",
    "MARSHALL",
};
    string[] stabilizers3 = new string[]
{
    "BUCCANEE",

};

    string[] stabilizers2 = new string[]
{
"VIGERO",
        "SABREGT",

};
    string[] loweforce = new string[]
{
    "HOTKNIFE",
            "RLOADER2",

};


    string[] monsters = new string[]
{

            "MONSTER",
    "MARSHALL",
};
    string[] higherlimit = new string[]
{
        "BRAWLER",
            "LURCHER",
                        "BISON",
        "PARADISE",
                "RUMPO",
                    "BODHI2",
"DUBSTA3",
"DUNE",
"INSURGENT",
"INSURGENT2",
                "MONSTER",
    "MARSHALL",


};
    string[] lowerlimit = new string[]
{
    "SULTAN",


};

    string[] lowclearance = new string[]
{
    "FACTION",
        "FACTION2",

            "AMBULAN",

        "BUCCANEE2",
    "CHINO2",
    "LURCHER",
        "MOONBEAM2",
        "RAPIDGT",
        "FELTZER3",
            "SCHAFTER",
    "SCHAFTER2",
    "SCHAFTER3",
    "SCHAFTER4",
    "SCHAFTER5",
    "SCHAFTER6",

};


    string[] highclearance = new string[]
{
    "DUNE",

    "BIFTA",
    "BFINJECT",
        "MONSTER",

};

    public Wheelies()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        LoadSettings();

    }


    void LoadSettings()
    {
        ScriptSettings config = ScriptSettings.Load(@"scripts\BurnoutWheelie.ini");
        AllowAllVehicles = config.GetValue<bool>("SETTINGS", "AllowAllVehicles", true);

    }
    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
    }
    int GetHash(string thing)
    {
        return Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, thing);
    }
    void OnTick(object sender, EventArgs e)
    {


        if (wheeliepoints > 10000)
        {
            wheeliepoints = 9000;
        }


        if (CanWeUse(Game.Player.Character.CurrentVehicle) && Game.Player.Character.CurrentVehicle.IsInBurnout() && Game.Player.Character.CurrentVehicle.Speed < 1f)
        {
            wheeliepoints++;
        }
        else if (wheeliepoints > 0)
        {
            if ((CanWeUse(Game.Player.Character.CurrentVehicle) && Game.Player.Character.CurrentVehicle.Speed > 2f && !Game.IsControlPressed(2, GTA.Control.VehicleAccelerate)) || (Game.Player.Character.IsOnFoot && wheeliepoints > 0))
            {
                wheeliepoints = 0;
            }
            else
            if (CanWeUse(Game.Player.Character.CurrentVehicle) && Game.IsControlPressed(2, GTA.Control.VehicleAccelerate) && Function.Call<float>(Hash.GET_ENTITY_HEIGHT_ABOVE_GROUND, Game.Player.Character.CurrentVehicle) < 2f && Function.Call<bool>(GTA.Native.Hash.IS_ENTITY_UPRIGHT, Game.Player.Character.CurrentVehicle, 40f))
            {
                forcelimit = 1f;
                plusforce = 0f;
                level = 15f;

                if (AllowAllVehicles || ((candowheelie.Contains(Game.Player.Character.CurrentVehicle.DisplayName) && Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 11) != -1) || (noupgradesneeded.Contains(Game.Player.Character.CurrentVehicle.DisplayName))) && Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 15) != 3)
                {
                    if (loweforce.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                    {
                        plusforce = -1f;
                    }
                    if (higherlimit.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                    {
                        forcelimit = 1.4f;
                    }
                    if (lowerlimit.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                    {
                        forcelimit = 0.9f;
                    }
                    if (highclearance.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                    {
                        level = 20f;
                    }
                    if (lowclearance.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                    {
                        level = 7f;
                    }
                    if (Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 2) == 0)
                    {
                        if (stabilizers.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                        {
                            level = 5f;
                        }
                    }
                    if (Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 10) == 0)
                    {
                        if (stabilizers2.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                        {
                            level = 5f;
                        }
                    }
                    if (Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 2) == 1)
                    {
                        if (stabilizers3.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                        {
                            level = 5f;
                        }
                    }
                    switch (Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 11))
                    {
                        default:
                            {
                                float force = 0;
                                float result = Function.Call<bool>(GTA.Native.Hash.IS_ENTITY_UPRIGHT, Game.Player.Character.CurrentVehicle, level) ? 1f : 0f;

                                if (monsters.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                                {
                                    plusforce = 200f;
                                }
                                if (noupgradesneeded.Contains(Game.Player.Character.CurrentVehicle.DisplayName))
                                {
                                    //force = 0.2f;
                                }
                                force = force + ((float)(Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 11) - Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MOD, Game.Player.Character.CurrentVehicle, 15) + 1 + plusforce));
                                force = force * result;


                                if (force > forcelimit) { force = forcelimit; } else if (force < 0f) { force = 0f; }
                                if (force != 0f)
                                {
                                    Function.Call(Hash.APPLY_FORCE_TO_ENTITY, Game.Player.Character.CurrentVehicle, 3, 0f, force / 2, 0f, 0f, 0f, force * -1f, 0, true, true, true, true);
                                    wheeliepoints--;
                                }


                                break;
                            }
                    }
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

}