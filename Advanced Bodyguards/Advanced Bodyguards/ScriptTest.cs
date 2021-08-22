using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Xml;

public class BodyguardSquads : Script
{


    //NativeUI test
    private MenuPool _menuPool;

    //Main Menu
    private UIMenu mainMenu;

    private UIMenu Squad1Menu;
    private UIMenu Squad2Menu;
    private UIMenu Squad3Menu;
    private UIMenu Squad4Menu;

    private UIMenuItem Squad1MenuItem;
    private UIMenuItem Squad2MenuItem;
    private UIMenuItem Squad3MenuItem;
    private UIMenuItem Squad4MenuItem;



    // SQUAD SETUP
    private UIMenuCheckboxItem Squad1ReactToEvents;
    private UIMenuCheckboxItem Squad1Followplayer;

    private UIMenuCheckboxItem Squad2ReactToEvents;
    private UIMenuCheckboxItem Squad2Followplayer;

    private UIMenuCheckboxItem Squad3ReactToEvents;
    private UIMenuCheckboxItem Squad3Followplayer;

    private UIMenuCheckboxItem Squad4ReactToEvents;
    private UIMenuCheckboxItem Squad4Followplayer;

    private UIMenuCheckboxItem Squad1GodMode;
    private UIMenuCheckboxItem Squad2GodMode;
    private UIMenuCheckboxItem Squad3GodMode;
    private UIMenuCheckboxItem Squad4GodMode;

    private UIMenuCheckboxItem ShowBlips;
    private UIMenuCheckboxItem ShowStatus;

    private UIMenuListItem Squad1StyleItem;
    private UIMenuListItem Squad2StyleItem;
    private UIMenuListItem Squad3StyleItem;
    private UIMenuListItem Squad4StyleItem;


    private UIMenuListItem MainWeaponSquad1List;
    private UIMenuListItem MainWeaponSquad2List;
    private UIMenuListItem MainWeaponSquad3List;
    private UIMenuListItem MainWeaponSquad4List;

    private UIMenuListItem SecondaryWeaponSquad1List;
    private UIMenuListItem SecondaryWeaponSquad2List;
    private UIMenuListItem SecondaryWeaponSquad3List;
    private UIMenuListItem SecondaryWeaponSquad4List;

    private UIMenuCheckboxItem Squad1DispatchToWaypoint;
    private UIMenuCheckboxItem Squad2DispatchToWaypoint;
    private UIMenuCheckboxItem Squad3DispatchToWaypoint;
    private UIMenuCheckboxItem Squad4DispatchToWaypoint;

    private UIMenuCheckboxItem Squad1Autocall;
    private UIMenuCheckboxItem Squad2Autocall;
    private UIMenuCheckboxItem Squad3Autocall;
    private UIMenuCheckboxItem Squad4Autocall;


    private UIMenuItem Squad1RecruitNear;
    private UIMenuItem Squad2RecruitNear;
    private UIMenuItem Squad3RecruitNear;
    private UIMenuItem Squad4RecruitNear;


    private UIMenuItem DeleteSquad1;
    private UIMenuItem DeleteSquad2;
    private UIMenuItem DeleteSquad3;
    private UIMenuItem DeleteSquad4;

    private UIMenuItem CallSquad1;
    private UIMenuItem CallSquad2;
    private UIMenuItem CallSquad3;
    private UIMenuItem CallSquad4;

    //RELATIONSHIPS MENU
    private UIMenuItem Squad1Hate;
    private UIMenuItem Squad1Like;

    private UIMenuItem Squad2Hate;
    private UIMenuItem Squad2Like;

    private UIMenuItem Squad3Hate;
    private UIMenuItem Squad3Like;

    private UIMenuItem Squad4Hate;
    private UIMenuItem Squad4Like;

    private UIMenuCheckboxItem Squad1HateSquad2;
    private UIMenuCheckboxItem Squad1HateSquad3;
    private UIMenuCheckboxItem Squad1HateSquad4;

    private UIMenuCheckboxItem Squad2HateSquad1;
    private UIMenuCheckboxItem Squad2HateSquad3;
    private UIMenuCheckboxItem Squad2HateSquad4;

    private UIMenuCheckboxItem Squad3HateSquad1;
    private UIMenuCheckboxItem Squad3HateSquad2;
    private UIMenuCheckboxItem Squad3HateSquad4;

    private UIMenuCheckboxItem Squad4HateSquad1;
    private UIMenuCheckboxItem Squad4HateSquad2;
    private UIMenuCheckboxItem Squad4HateSquad3;

    private UIMenuCheckboxItem Squad1HatePlayer;
    private UIMenuCheckboxItem Squad2HatePlayer;
    private UIMenuCheckboxItem Squad3HatePlayer;
    private UIMenuCheckboxItem Squad4HatePlayer;


    // COMMON ORDERS
    private UIMenuItem EscortMeSquad1;
    private UIMenuItem EscortMeSquad2;
    private UIMenuItem EscortMeSquad3;
    private UIMenuItem EscortMeSquad4;

    private UIMenuItem GoToWaypointSquad1;
    private UIMenuItem GoToWaypointSquad2;
    private UIMenuItem GoToWaypointSquad3;
    private UIMenuItem GoToWaypointSquad4;

    private UIMenuItem EnterLeaveVehicleSquad1;
    private UIMenuItem EnterLeaveVehicleSquad2;
    private UIMenuItem EnterLeaveVehicleSquad3;
    private UIMenuItem EnterLeaveVehicleSquad4;

    private UIMenuItem GetBackToMeSquad1;
    private UIMenuItem GetBackToMeSquad2;
    private UIMenuItem GetBackToMeSquad3;
    private UIMenuItem GetBackToMeSquad4;

    // SPECIAL ORDERS
    private UIMenuItem RappelSquad1;
    private UIMenuItem RappelSquad2;
    private UIMenuItem RappelSquad3;
    private UIMenuItem RappelSquad4;

    private UIMenuItem SwitchSirensSquad1;
    private UIMenuItem SwitchSirensSquad2;
    private UIMenuItem SwitchSirensSquad3;
    private UIMenuItem SwitchSirensSquad4;

    private UIMenuItem AttachVehiclesSquad1;
    private UIMenuItem AttachVehiclesSquad2;
    private UIMenuItem AttachVehiclesSquad3;
    private UIMenuItem AttachVehiclesSquad4;

    private UIMenuItem ParkNearbySquad1;
    private UIMenuItem ParkNearbySquad2;
    private UIMenuItem ParkNearbySquad3;
    private UIMenuItem ParkNearbySquad4;

    private UIMenuItem FollowmeOffroadSquad1;
    private UIMenuItem FollowmeOffroadSquad2;
    private UIMenuItem FollowmeOffroadSquad3;
    private UIMenuItem FollowmeOffroadSquad4;



    private UIMenuItem GuardThisAreaSquad1;
    private UIMenuItem GuardThisAreaSquad2;
    private UIMenuItem GuardThisAreaSquad3;
    private UIMenuItem GuardThisAreaSquad4;

    private UIMenuItem FleeSquad1;
    private UIMenuItem FleeSquad2;
    private UIMenuItem FleeSquad3;
    private UIMenuItem FleeSquad4;


    //INTERNAL VARIABLES
    private UIMenuListItem DrivingStyleSquad1;
    private UIMenuListItem DrivingStyleSquad2;
    private UIMenuListItem DrivingStyleSquad3;
    private UIMenuListItem DrivingStyleSquad4;

    private UIMenuListItem Squad1VehicleSelection;
    private UIMenuListItem Squad1NumberItem;
    private UIMenuListItem Squad2VehicleSelection;
    private UIMenuListItem Squad2NumberItem;
    private UIMenuListItem Squad3VehicleSelection;
    private UIMenuListItem Squad3NumberItem;
    private UIMenuListItem Squad4VehicleSelection;
    private UIMenuListItem Squad4NumberItem;

    //ALL SQUADS MENU
    private UIMenu AllSquadsMenu;
    private UIMenuItem AllSquadsMenuItem;
    private UIMenuItem AllGoToWaypoint;
    private UIMenuItem AllGetBackToMe;
    private UIMenuItem AllCallSquad;
    private UIMenuItem AllDismissSquad;
    private UIMenuItem AllEnterLeave;

    //RACE MENU
    private UIMenu AllSquadsRaceMenu;
    private UIMenuItem AllParkAtMySide;
    private UIMenuItem AllSquadsRaceMenuItem;
    private UIMenuItem AllSquadsAddWaypoint;
    private UIMenuItem AllSquadsClearWaypoints;
    private UIMenuItem AllSquadsStartRace;

    //GLOBAL VARIABLES
    List<Vector3> RaceWaypoints = new List<Vector3>();
    List<Blip> RaceWaypointBlips = new List<Blip>();

    List<Ped> Squad1 = new List<Ped>();
    List<Ped> Squad2 = new List<Ped>();
    List<Ped> Squad3 = new List<Ped>();
    List<Ped> Squad4 = new List<Ped>();

    List<Ped> DrivingCareful = new List<Ped>();
    List<Ped> DrivingNormal = new List<Ped>();
    List<Ped> DrivingFurious = new List<Ped>();


    Vehicle Squad1Car;
    Vehicle Squad2Car;
    Vehicle Squad3Car;
    Vehicle Squad4Car;
    
    
    private int interval2sec = Game.GameTime; //        interval2sec = DateTime.Now;
    private int interval1sec = Game.GameTime; //        interval1sec = DateTime.Now;

    //SQUAD PEDS
    private Ped Squad1Leader;
    private Ped Squad1Member;
    private Ped Squad2Leader;
    private Ped Squad2Member;
    private Ped Squad3Leader;
    private Ped Squad3Member;
    private Ped Squad4Leader;
    private Ped Squad4Member;


    ///RAYCAST INFO
    private Vector3 hitcoord;
    private Entity entitihit;

    //DEFAULT VALUES FOR CONFIG
    private string Squad1Vehicle = "OnFoot";
    private string Squad2Vehicle = "OnFoot";
    private string Squad3Vehicle = "OnFoot";
    private string Squad4Vehicle = "OnFoot";

    private string Squad1StylePed = "Army";
    private string Squad2StylePed = "Army";
    private string Squad3StylePed = "Army";
    private string Squad4StylePed = "Army";

    private WeaponHash MainWeaponSquad1 = WeaponHash.Unarmed;
    private WeaponHash SecondaryWeaponSquad1 = WeaponHash.Unarmed;
    private WeaponHash MainWeaponSquad2 = WeaponHash.Unarmed;
    private WeaponHash SecondaryWeaponSquad2 = WeaponHash.Unarmed;
    private WeaponHash MainWeaponSquad3 = WeaponHash.Unarmed;
    private WeaponHash SecondaryWeaponSquad3 = WeaponHash.Unarmed;
    private WeaponHash MainWeaponSquad4 = WeaponHash.Unarmed;
    private WeaponHash SecondaryWeaponSquad4 = WeaponHash.Unarmed;

    private int Squad1Number = 8;
    private int Squad2Number = 8;
    private int Squad3Number = 8;
    private int Squad4Number = 8;


    private string CustomStyle1Name = "Custom1";
    private string CustomStyle2Name = "Custom2";
    private Color Squad1Color = Color.FromArgb(255, 15, 170, 255);
    private Color Squad2Color = Color.FromArgb(255, 249, 255, 79);
    private Color Squad3Color = Color.FromArgb(255, 12, 173, 0);
    private Color Squad4Color = Color.FromArgb(255, 255, 0, 0);


    ///.INI CONFIG
    private bool EnterAsPassengerEnabled = true;
    private bool SquadStatusOnScreen = true;
    private int SquadSpawnDistance = 300;

    private Keys MenuKey = Keys.X;
    private Keys OrdersModeKey = Keys.C;
    private Keys EnterAsPassengerKey = Keys.Space;
    private float TimeScale;


    ///SLOWTIME CONFIG
    private bool WantedSlowsTime;
    private bool HalfHealthSlowsTime;
    private bool CombatSlowsTime;
    private bool SlowTimeWhenManaging;


    ///MORE VALUES
    private int ordersmode = 0;
    int Squad1RelationshipGroup = World.AddRelationshipGroup("Squad1RelationshipGroup");
    int Squad2RelationshipGroup = World.AddRelationshipGroup("Squad2RelationshipGroup");
    int Squad3RelationshipGroup = World.AddRelationshipGroup("Squad3RelationshipGroup");
    int Squad4RelationshipGroup = World.AddRelationshipGroup("Squad4RelationshipGroup");


    List<String> SquadTransportHashes = new List<String>    {
        "OnFoot",
        "Schafter2",
        "Schafter5",
        "Cognoscenti2",
        "Stretch",
                "Limo2",
        "Burrito3",
        "GBurrito2",
        "Huntley",
        "Cavalcade2",
        "Baller5",
        "Granger",
        "Bison",
        "Mesa",
        "Mesa3",
        "Buffalo2",
        "BType",
        "Ztype",
        "Kuruma",
        "Kuruma2",
        "Patriot",
        "Rebel",
        "Technical",
        "Dubsta",
        "Dubsta3",
        "Sandking",
        "Guardian",
        "Insurgent2",
        "Insurgent",
        "Barracks",
        "Rhino",
        "Akuma",
        "Enduro",
        "Bati",
        "Daemon",
        "Innovation",
        "Swift",
        "Supervolito",
        "Buzzard2",
        "Buzzard",
        "Annihilator",
        "Valkyrie",
        "Savage",
        "Cargobob",
        "Police",
        "Police2",
        "Police3",
        "Polmav",
        "FBI2",
        "Riot",
    };

    List<WeaponHash> MainWeaponHashes= new List<WeaponHash>
    {
                WeaponHash.Unarmed,
                WeaponHash.HeavyPistol,
                WeaponHash.PumpShotgun,
                WeaponHash.SMG,
                WeaponHash.Gusenberg,
                WeaponHash.AssaultRifle,
                WeaponHash.AdvancedRifle,
                WeaponHash.CarbineRifle,
                WeaponHash.SpecialCarbine,
                WeaponHash.HeavySniper,
                WeaponHash.CombatMG,
                WeaponHash.Railgun,
                WeaponHash.Minigun,
                WeaponHash.RPG,
    };

    List<WeaponHash> SecondaryWeaponHashes = new List<WeaponHash>
{
                WeaponHash.Unarmed,
                WeaponHash.Nightstick,
                WeaponHash.StunGun,
                WeaponHash.Bat,
                WeaponHash.Knife,
                WeaponHash.Pistol,
                WeaponHash.SawnOffShotgun,
                WeaponHash.MicroSMG,
};



    Random rnd = new Random();
    protected int GetRandomInt(int min, int max)
    {
        return rnd.Next(min, max);
    }

    List<String> Squad1Models = new List<String>
{
        "s_m_y_marine_03",
        "s_m_y_marine_02",
        "s_m_y_marine_01",
        "s_m_m_marine_01",
        "s_m_y_blackops_01",
        "s_m_y_blackops_02",
        "s_m_y_blackops_03"
};

    List<String> Squad2Models = new List<String>
{
            "s_m_y_armymech_01",
        "s_m_y_marine_03",
        "s_m_y_marine_02",
        "s_m_y_marine_01",
        "s_m_m_marine_01",
        "s_m_y_blackops_01",
        "s_m_y_blackops_02",
        "s_m_y_blackops_03"

};

    List<String> Squad3Models = new List<String>
{
            "s_m_y_armymech_01",
        "s_m_y_marine_03",
        "s_m_y_marine_02",
        "s_m_y_marine_01",
        "s_m_m_marine_01",
        "s_m_y_blackops_01",
        "s_m_y_blackops_02",
        "s_m_y_blackops_03"

};
    List<String> Squad4Models = new List<String>
{
            "s_m_y_armymech_01",
        "s_m_y_marine_03",
        "s_m_y_marine_02",
        "s_m_y_marine_01",
        "s_m_m_marine_01",
        "s_m_y_blackops_01",
        "s_m_y_blackops_02",
        "s_m_y_blackops_03"

};
    List<String> ArmyModels = new List<String>
    {

        "s_m_y_armymech_01",
        "s_m_y_marine_03",
        "s_m_y_marine_02",
        "s_m_y_marine_01",
        "s_m_m_marine_01",
        "s_m_y_blackops_01",
        "s_m_y_blackops_02",
        "s_m_y_blackops_03"
    };

    List<String> PrivateSecurityModels = new List<String>
{

    "s_m_m_highsec_01",
    "s_m_m_highsec_02",
    "s_m_y_doorman_01",
    "s_m_y_devinsec_01"
};
    List<String> BallasModels = new List<String>
{
        "g_f_y_ballas_01",
        "g_m_y_ballaeast_01",
        "g_m_y_ballaorig_01",
        "g_m_y_ballasout_01",
};


    List<String> FamiliesModels = new List<String>
{
        "mp_m_famdd_01",
        "g_f_y_families_01",
        "g_m_y_famca_01",
        "g_m_y_famdnf_01",
        "g_m_y_famfor_01"
};
    List<String> LostModels = new List<String>
{
        "g_m_y_lost_01",
        "g_m_y_lost_02",
        "g_m_y_lost_02",
        "g_f_y_lost_01",
};

    List<String> RedneckModels = new List<String>
{
        "a_m_m_hillbilly_01",
        "a_m_m_hillbilly_02"
};

    List<String> SWATModels = new List<String>
{
        "s_m_y_swat_01",

};

    List<String> PoliceModels = new List<String>
{

        "s_f_y_cop_01",
        "s_m_y_cop_01"
};

    ///EMPTY CUSTOM MODEL LISTS TO FILL AT START
    List<String> CustomModels1 = new List<String>
    {

    };
    List<String> CustomModels2 = new List<String>
    {

    };

    List<Model> VehiclesWithSiren = new List<Model>
        {
            VehicleHash.Police,
            VehicleHash.Police2,
            VehicleHash.Police3,
            VehicleHash.Police4,
            VehicleHash.Policeb,
            VehicleHash.PoliceOld1,
            VehicleHash.PoliceOld2,
            VehicleHash.Riot,
            VehicleHash.FBI2,
            VehicleHash.FBI,
            VehicleHash.FireTruck,
            VehicleHash.Ambulance,
        };



    List<dynamic> SquadVehicles = new List<dynamic>
        {
            "On foot",
            "[Sedan] Schafter",
            "[Sedan] Armored Schafter",
            "[Sedan] Armored Cognoscenti",
            "[Limousine] Stretch",
            "[Limousine] Turreted Stretch",
            "[Vans] Burrito",
            "[Vans] Gang Burrito",
            "[SUV] Huntley S",
            "[SUV] Cavalcade",
            "[SUV] Armored Baller LE",
            "[SUV] Granger",
            "[SUV] Bison",
            "[4X4] Mesa",
            "[Military] Army Mesa",
            "[Sports] Buffalo",
            "[Sports Classic] Roosewelt",
            "[Sports Classic] ZType",
            "[4X4] Kuruma",
            "[4X4] Armored Kuruma",
            "[4X4] Patriot",
            "[4X4] Rebel",
            "[4X4] Technical",
            "[4X4] Dubsta",
            "[4X4] Dubsta 6x6",
            "[4X4] Sandking",
            "[4X4] Guardian",
            "[4X4] Insurgent",
            "[4X4] Armed Insurgent",
            "[Truck] Barracks",
            "[Tank] Rhino",
            "[Bikes] Akuma",
            "[Bikes] Enduro",
            "[Bikes] Bati 801",
            "[Bikes] Daemon",
            "[Bikes] Innovation",
            "[Helis] Swift",
            "[Helis] Supervolito",
            "[Helis] Buzzard",
            "[Helis] Attack Buzzard",
            "[Helis] Annihilator",
            "[Helis] Valkyrie",
            "[Helis] Savage",
            "[Helis] Cargobob",
            "[Police] Cruiser",
            "[Police] Buffalo",
            "[Police] Interceptor",
            "[Police] Maverick",
            "[FIB] Granger",
            "[SWAT] Riot",

        };

    List<dynamic> MainWeaponsSquad = new List<dynamic>
        {
            "Unarmed",
            "Heavy Pistol",
            "Shotgun",
            "SMG",
            "Gusenberg Sweeper",
            "Assault Rifle",
            "Advanced Rifle",
            "Carbine",
            "Special Carbine",
            "Heavy Sniper Rifle",
            "Combat MG",
            "Railgun",
            "Minigun",
            "RPG",
        };


    List<dynamic> SecondaryWeaponsSquad = new List<dynamic>
        {
            "Unarmed",
            "Nightstick",
            "Stun Gun",
            "Baseball Bat",
            "Knife",
            "Pistol",
            "Sawn Off Shotgun",
            "MicroSMG",
        };
    public void LoadSettings()
    {
        ScriptSettings config = ScriptSettings.Load(@"scripts\advanced_bodyguards.ini");
        while (config == null) Script.Wait(200);
//        UI.Notify("Loading config");
        foreach (string value in config.GetAllValues("MODELS", "CustomVehicle"))
        {
            Model vehmodel = value;
            if ((vehmodel).IsVehicle)
            {
                //  UI.Notify("Added " + value);
                SquadTransportHashes.Add(value);
                SquadVehicles.Add("[CUSTOM]" + Function.Call<string>(GTA.Native.Hash.GET_DISPLAY_NAME_FROM_VEHICLE_MODEL, Game.GenerateHash(value)));
                //[" + ((VehicleClass)Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_CLASS, value)).ToString() + "] 
            }
        }

        foreach (string value in config.GetAllValues("MODELS", "MainWeaponModel"))
        {
            Model weapon = value;
            if (1 == 1)
            {
                //  UI.Notify("Added " + value);
                WeaponHash w = (WeaponHash)Game.GenerateHash(value);
                MainWeaponHashes.Add(w);
                MainWeaponsSquad.Add(w);

            }
        }

        foreach (string value in config.GetAllValues("MODELS", "SecondaryWeaponModel"))
        {
            Model weapon = value;
            // UI.Notify("Added " + value);
            if (1 == 1)
            {
                WeaponHash w = (WeaponHash)Game.GenerateHash(value);

                SecondaryWeaponHashes.Add(w);
                SecondaryWeaponsSquad.Add(w);
            }
        }

        EnterAsPassengerEnabled = config.GetValue<bool>("SETTINGS", "EnterAsPassengerEnabled", true);
        SquadStatusOnScreen = config.GetValue<bool>("SETTINGS", "SquadStatusOnScreen", true);
        SquadSpawnDistance = config.GetValue<int>("SETTINGS", "SquadSpawnDistance", 300);
        MenuKey = config.GetValue<Keys>("SETTINGS", "MenuKey", Keys.X);
        OrdersModeKey = config.GetValue<Keys>("SETTINGS", "OrdersModeKey", Keys.C);
        TimeScale = config.GetValue<float>("SETTINGS", "TimeScale", 0);
        EnterAsPassengerKey = config.GetValue<Keys>("SETTINGS", "EnterAsPassengerKey", Keys.Space);

        SlowTimeWhenManaging = config.GetValue<bool>("SETTINGS", "SlowTimeWhenManaging", false);
        WantedSlowsTime = config.GetValue<bool>("SETTINGS", "WantedSlowsTime", false);
        HalfHealthSlowsTime = config.GetValue<bool>("SETTINGS", "HalfHealthSlowsTime", true);
        CombatSlowsTime = config.GetValue<bool>("SETTINGS", "CombatSlowsTime", true);
        CustomStyle1Name = config.GetValue<string>("MODELS", "CustomStyle1Name", "Custom1");
        CustomStyle2Name = config.GetValue<string>("MODELS", "CustomStyle2Name", "Custom2");

        var Custommodel = config.GetValue<string>("MODELS", "CustomStyle1Model1", "u_m_y_mani");
        CustomModels1.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle1Model2", "u_m_y_mani");
        CustomModels1.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle1Model3", "u_m_y_mani");
        CustomModels1.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle1Model4", "u_m_y_mani");
        CustomModels1.Add(Custommodel);

        Custommodel = config.GetValue<string>("MODELS", "CustomStyle2Model1", "u_m_y_mani");
        CustomModels2.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle2Model2", "u_m_y_mani");
        CustomModels2.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle2Model3", "u_m_y_mani");
        CustomModels2.Add(Custommodel);
        Custommodel = config.GetValue<string>("MODELS", "CustomStyle2Model4", "u_m_y_mani");
        CustomModels2.Add(Custommodel);
        Custommodel = null;

        var SquadStyle = new List<dynamic>
        {
            "Army",
            "Private Security",
            "Rednecks",
            "Families",
            "Ballas",
            "The Lost",
            CustomStyle1Name,
            CustomStyle2Name,
            "SWAT",
            "Police",
        };

        List<dynamic> NumberOfPeople = new List<dynamic>
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
        };



        var DrivingStyle = new List<dynamic>
        {
            "Careful",
            "Normal",
            "Fast & Furious",
        };

        _menuPool = new MenuPool();

        mainMenu = new UIMenu("Guard Commands", "");
        _menuPool.Add(mainMenu);

        //SQUAD 1 MAIN MENU
        Squad1MenuItem = new UIMenuItem("~b~Squad 1");
        mainMenu.AddItem(Squad1MenuItem);
        Squad1Menu = new UIMenu("~b~Squad 1", "");
        _menuPool.Add(Squad1Menu);

        //SQUAD 1 SETUP
        var Squad1SetupItem = new UIMenuItem("Squad Setup", "Set up this squad and configure their behavior.");
        Squad1SetupItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        Squad1Menu.AddItem(Squad1SetupItem);
        var Squad1Setup = new UIMenu("~b~Squad Setup", "~b~Configure the first Squad from there.");
        _menuPool.Add(Squad1Setup);
        Squad1Menu.BindMenuToItem(Squad1Setup, Squad1SetupItem);
        Squad1Setup.AddItem(Squad1StyleItem = new UIMenuListItem("Unit Style", SquadStyle, 0));
        Squad1Setup.AddItem(Squad1NumberItem = new UIMenuListItem("Units", NumberOfPeople, 7));
        Squad1Setup.AddItem(Squad1VehicleSelection = new UIMenuListItem("Vehicle", SquadVehicles, 0, "The vehicle they will come in. Be aware of how many passengers the vehicle will fit in."));
        Squad1Setup.AddItem(MainWeaponSquad1List = new UIMenuListItem("Main weapon", MainWeaponsSquad, 0, "This list includes heavy weapons."));
        Squad1Setup.AddItem(SecondaryWeaponSquad1List = new UIMenuListItem("Secondary weapon", SecondaryWeaponsSquad, 0, "This list includes melee and light weapons."));
        Squad1Setup.AddItem(Squad1Autocall = new UIMenuCheckboxItem("Re-call when defeated", false));
        Squad1Setup.AddItem(Squad1DispatchToWaypoint = new UIMenuCheckboxItem("Dispatch to Waypoint", false));
        Squad1DispatchToWaypoint.Description = "If checked, the squad will be dispatched to your Waypoint instead of your actual position.";
        Squad1Setup.AddItem(CallSquad1 = new UIMenuItem("Call Squad", "Spawn this squad with the above defined paramet."));
        Squad1Setup.AddItem(DeleteSquad1 = new UIMenuItem("Dismiss Squad", "Make the Squad leave."));

        //SQUAD 1 CONFIG BEHAVIOR
        var Squad1ConfigItem = new UIMenuItem("Squad Behaviour", "");
        Squad1ConfigItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad1Config = new UIMenu("~b~Squad Behaviour", "~b~These options affect the squad in real time.");
        _menuPool.Add(Squad1Config);
        Squad1Menu.AddItem(Squad1ConfigItem);
        Squad1Menu.BindMenuToItem(Squad1Config, Squad1ConfigItem);
        Squad1Config.AddItem(DrivingStyleSquad1 = new UIMenuListItem("Driving style", DrivingStyle, 1, "How the leader will drive. Careful: Slow, overtaking with care. Normal: Somewhat fast, overtaking with care. Fast: Fastest, careless overtaking."));
        Squad1Config.AddItem(Squad1ReactToEvents = new UIMenuCheckboxItem("React to enemies", true, "When enabled, the squad leader will only obey when is not in combat. Squad members will ALWAYS priorize combat over orders, this allows them to do drivebys while the leader (driver) focuses on whatever you told him."));
        Squad1Config.AddItem(Squad1Followplayer = new UIMenuCheckboxItem("Follow Player", false, "The Squad will follow you automatically. They will hop in your vehicles, too."));
        Squad1Config.AddItem(Squad1GodMode = new UIMenuCheckboxItem("God Mode", false, "People kept asking for this."));


        //SQUAD 1 SPECIAL ORDERS
        var Squad1SpecialOrdersItem = new UIMenuItem("Special Orders", "This submenu contains orders that can only be issued under certain conditions.");
        Squad1Menu.AddItem(Squad1SpecialOrdersItem);
        Squad1SpecialOrdersItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad1SpecialOrdersMenu = new UIMenu("~b~Special Orders", "~b~Not so common orders.");
        _menuPool.Add(Squad1SpecialOrdersMenu);

        Squad1Menu.BindMenuToItem(Squad1SpecialOrdersMenu, Squad1SpecialOrdersItem);
        Squad1SpecialOrdersMenu.AddItem(SwitchSirensSquad1 = new UIMenuItem("Siren on/off", "If the squad leader is in an Emergency vehicle, it will switch the vehicle's sirens on or off."));
        Squad1SpecialOrdersMenu.AddItem(RappelSquad1 = new UIMenuItem("Rappel From Heli", "If the squad is in an Helicopter that allows rappeling, the passengers will rappel to the ground."));
        Squad1SpecialOrdersMenu.AddItem(AttachVehiclesSquad1 = new UIMenuItem("Attach/Detach vehicle", "If the squad is driving a Cargobob, Truck or Towtruck, they will attach nearby vehicles to them."));
        Squad1SpecialOrdersMenu.AddItem(ParkNearbySquad1 = new UIMenuItem("Park Nearby", "The Squad leader will try to park his vehicle nearby."));
        Squad1SpecialOrdersMenu.AddItem(FollowmeOffroadSquad1 = new UIMenuItem("Follow me, we're going offroad", "The Squad leader will try follow you offroad."));
        Squad1SpecialOrdersMenu.AddItem(GuardThisAreaSquad1 = new UIMenuItem("Watch out for enemies", "The Squad leader will try to watch out for enemies."));
        Squad1SpecialOrdersMenu.AddItem(FleeSquad1 = new UIMenuItem("Flee in this vehicle", "The Squad leader will try to flee in the current vehicle"));
        Squad1SpecialOrdersMenu.AddItem(Squad1RecruitNear = new UIMenuItem("Recruit Nearby peds", "Any ped close to the Squad Leader will become part of their squad."));

        //SQUAD 1 RELATIONSHIPS
        var Squad1RelationshipsItem = new UIMenuItem("Relationships Menu", "Define the faction relationships.");
        Squad1Config.AddItem(Squad1RelationshipsItem);

        Squad1RelationshipsItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad1RelationshipsMenu = new UIMenu("~b~Relationships Menu", "~b~Define the faction relationships.");
        _menuPool.Add(Squad1RelationshipsMenu);
        Squad1Config.BindMenuToItem(Squad1RelationshipsMenu, Squad1RelationshipsItem);

        Squad1RelationshipsMenu.AddItem(Squad1HateSquad2 = new UIMenuCheckboxItem("Hate Squad 2", false, "While checked, Squad 1 will attack Squad 2 on sight."));
        Squad1RelationshipsMenu.AddItem(Squad1HateSquad3 = new UIMenuCheckboxItem("Hate Squad 3", false, "While checked, Squad 1 will attack Squad 2 on sight."));
        Squad1RelationshipsMenu.AddItem(Squad1HateSquad4 = new UIMenuCheckboxItem("Hate Squad 4", false, "While checked, Squad 1 will attack Squad 2 on sight."));
        Squad1RelationshipsMenu.AddItem(Squad1HatePlayer = new UIMenuCheckboxItem("Hate Player", false, "While checked, Squad 1 will attack you on sight."));

        Squad1RelationshipsMenu.AddItem(Squad1Hate = new UIMenuItem("Open Hate Input", "Any RelationshipGroup you type here will be hated by this Squad. If the squad already hates that RelationShipGroup, they will stop hating it."));
        Squad1RelationshipsMenu.AddItem(Squad1Like = new UIMenuItem("Open Allies Input", "Any RelationshipGroup you type here will be liked by this Squad. If the squad already likes RelationShipGroup, they will stop liking it."));

        //SQUAD 1 MAIN MENU PART 2
        Squad1Menu.AddItem(EscortMeSquad1 = new UIMenuItem("Escort my vehicle", "The leader will follow your vehicle. If you're on foot, Helicopters can follow you too."));
        Squad1Menu.AddItem(GetBackToMeSquad1 = new UIMenuItem("Get back to me", "The leader will try to run/drive to your position, or the nearest point possible. They will get to their vehicles if you're too far."));
        Squad1Menu.AddItem(GoToWaypointSquad1 = new UIMenuItem("Go to waypoint", "The leader will try to run/drive to the waypoint, or the nearest point possible."));
        Squad1Menu.AddItem(EnterLeaveVehicleSquad1 = new UIMenuItem("Enter/exit/land vehicle", "The squad will leave their current vehicle or go back to their last vehicle, if they are on foot. Also counts as 'landing' when in an Helicopter."));
        mainMenu.BindMenuToItem(Squad1Menu, Squad1MenuItem);

        /*
        var banner = new UIResRectangle();
        banner.Color = Color.FromArgb(255, 0, 85, 150);
        Squad1Menu.SetBannerType(banner);
        Squad1Config.SetBannerType(banner);
        Squad1Setup.SetBannerType(banner);
        Squad1SpecialOrdersMenu.SetBannerType(banner);
        Squad1RelationshipsMenu.SetBannerType(banner);
        */
        //SQUAD 2 MAIN MENU
        Squad2MenuItem = new UIMenuItem("~y~Squad 2");
        mainMenu.AddItem(Squad2MenuItem);
        Squad2Menu = new UIMenu("~y~Squad 2", "");
        _menuPool.Add(Squad2Menu);

        //SQUAD 2 SETUP
        var Squad2SetupItem = new UIMenuItem("Squad Setup", "Set up this squad and configure their behavior.");
        Squad2SetupItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        Squad2Menu.AddItem(Squad2SetupItem);
        var Squad2Setup = new UIMenu("~y~Squad Setup", "~y~Configure the second Squad from there.");
        _menuPool.Add(Squad2Setup);
        Squad2Menu.BindMenuToItem(Squad2Setup, Squad2SetupItem);
        Squad2Setup.AddItem(Squad2StyleItem = new UIMenuListItem("Unit Style", SquadStyle, 0));
        Squad2Setup.AddItem(Squad2NumberItem = new UIMenuListItem("Units", NumberOfPeople, 7));
        Squad2Setup.AddItem(Squad2VehicleSelection = new UIMenuListItem("Vehicle", SquadVehicles, 0, "The vehicle they will come in. Be aware of how many passengers the vehicle will fit in."));
        Squad2Setup.AddItem(MainWeaponSquad2List = new UIMenuListItem("Main weapon", MainWeaponsSquad, 0));
        Squad2Setup.AddItem(SecondaryWeaponSquad2List = new UIMenuListItem("Secondary weapon", SecondaryWeaponsSquad, 0, "This list includes melee and light weapons."));

        Squad2Setup.AddItem(Squad2Autocall = new UIMenuCheckboxItem("Re-call when defeated", false));
        Squad2Setup.AddItem(Squad2DispatchToWaypoint = new UIMenuCheckboxItem("Dispatch to Waypoint", false));
        Squad2DispatchToWaypoint.Description = "If checked, the squad will be dispatched to your Waypoint instead of your actual position.";

        Squad2Setup.AddItem(CallSquad2 = new UIMenuItem("Call Squad", "Spawn this squad with the above defined parameters."));
        Squad2Setup.AddItem(DeleteSquad2 = new UIMenuItem("Dismiss Squad", "Make the Squad leave."));

        //SQUAD 2 CONFIG BEHAVIOR
        var Squad2ConfigItem = new UIMenuItem("Squad Behaviour", "");
        Squad2ConfigItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad2Config = new UIMenu("~y~Squad Behaviour", "~y~These options affect the squad in real time.");
        _menuPool.Add(Squad2Config);
        Squad2Menu.AddItem(Squad2ConfigItem);
        Squad2Menu.BindMenuToItem(Squad2Config, Squad2ConfigItem);

        Squad2Config.AddItem(DrivingStyleSquad2 = new UIMenuListItem("Driving style", DrivingStyle, 1, "How the leader will drive. Careful: Slow, overtaking with care. Normal: Somewhat fast, overtaking with care. Fast: Fastest, careless overtaking."));
        Squad2Config.AddItem(Squad2ReactToEvents = new UIMenuCheckboxItem("React to enemies", true, "When enabled, the squad will only obey when is not in combat."));
        Squad2Config.AddItem(Squad2Followplayer = new UIMenuCheckboxItem("Follow Player", false, "The Squad will follow you automatically. They will hop in your vehicles, too."));
        Squad2Config.AddItem(Squad2GodMode = new UIMenuCheckboxItem("God Mode", false, "People kept asking for this."));

        //SQUAD 2 SPECIAL ORDERS
        var Squad2SpecialOrdersItem = new UIMenuItem("Special Orders", "This submenu contains orders that can only be issued under certain conditions.");
        Squad2SpecialOrdersItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);

        Squad2Menu.AddItem(Squad2SpecialOrdersItem);
        var Squad2SpecialOrdersMenu = new UIMenu("~y~Special Orders", "~y~Not so common orders.");
        _menuPool.Add(Squad2SpecialOrdersMenu);
        Squad2Menu.BindMenuToItem(Squad2SpecialOrdersMenu, Squad2SpecialOrdersItem);
        Squad2SpecialOrdersMenu.AddItem(SwitchSirensSquad2 = new UIMenuItem("Siren on/off", "If the squad leader is in an Emergency vehicle, it will switch the vehicle's sirens on or off."));
        Squad2Menu.BindMenuToItem(Squad2SpecialOrdersMenu, Squad2SpecialOrdersItem);
        Squad2SpecialOrdersMenu.AddItem(RappelSquad2 = new UIMenuItem("Rappel From Heli", "If the squad is in an Helicopter that allows rappeling, the passengers will rappel to the ground."));
        Squad2SpecialOrdersMenu.AddItem(AttachVehiclesSquad2 = new UIMenuItem("Attach/Detach vehicle", "If the squad is driving a Cargobob, Truck or Towtruck, they will attach nearby vehicles to them."));
        Squad2SpecialOrdersMenu.AddItem(ParkNearbySquad2 = new UIMenuItem("Park Nearby", "The Squad leader will try to park his vehicle nearby."));
        Squad2SpecialOrdersMenu.AddItem(FollowmeOffroadSquad2 = new UIMenuItem("Follow me, we're going offroad", "The Squad leader will try follow you offroad."));
        Squad2SpecialOrdersMenu.AddItem(GuardThisAreaSquad2 = new UIMenuItem("Watch out for enemies", "The Squad leader will try to watch out for enemies."));
        Squad2SpecialOrdersMenu.AddItem(FleeSquad2 = new UIMenuItem("Flee in this vehicle", "The Squad leader will try to flee in the current vehicle"));
        Squad2SpecialOrdersMenu.AddItem(Squad2RecruitNear = new UIMenuItem("Recruit Nearby peds", "Any ped close to the Squad Leader will become part of their squad."));

        //SQUAD 2 RELATIONSHIPS
        var Squad2RelationshipsItem = new UIMenuItem("Relationships Menu", "Define the faction relationships.");
        Squad2Config.AddItem(Squad2RelationshipsItem);

        Squad2RelationshipsItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad2RelationshipsMenu = new UIMenu("~y~Relationships Menu", "~y~Define the faction relationships.");
        _menuPool.Add(Squad2RelationshipsMenu);
        Squad2Config.BindMenuToItem(Squad2RelationshipsMenu, Squad2RelationshipsItem);
        Squad2RelationshipsMenu.AddItem(Squad2HateSquad1 = new UIMenuCheckboxItem("Hate Squad 1", false, "While checked, Squad 2 will attack Squad 1 on sight."));
        Squad2RelationshipsMenu.AddItem(Squad2HateSquad3 = new UIMenuCheckboxItem("Hate Squad 3", false, "While checked, Squad 2 will attack Squad 1 on sight."));
        Squad2RelationshipsMenu.AddItem(Squad2HateSquad4 = new UIMenuCheckboxItem("Hate Squad 4", false, "While checked, Squad 2 will attack Squad 1 on sight."));
        Squad2RelationshipsMenu.AddItem(Squad2HatePlayer = new UIMenuCheckboxItem("Hate Player", false, "While checked, Squad 2 will attack you on sight."));
        Squad2RelationshipsMenu.AddItem(Squad2Hate = new UIMenuItem("Open Hate Input", "Any RelationshipGroup you type here will be hated by this Squad. If the squad already hates that RelationshipGroup, they will stop hating it."));
        Squad2RelationshipsMenu.AddItem(Squad2Like = new UIMenuItem("Open Allies Input", "Any RelationshipGroup you type here will be liked by this Squad. If the squad already likes RelationshipGroup, they will stop liking it."));

        //SQUAD 2 MAIN MENU PART 2
        Squad2Menu.AddItem(EscortMeSquad2 = new UIMenuItem("Escort my vehicle", "The leader will follow your vehicle. If you're on foot, Helicopters can follow you too."));
        Squad2Menu.AddItem(GetBackToMeSquad2 = new UIMenuItem("Get back to me", "The leader will try to run/drive to your position, or the nearest point possible. They will get to their vehicles if you're too far."));

        Squad2Menu.AddItem(GoToWaypointSquad2 = new UIMenuItem("Go to waypoint", "The leader will try to run/drive to the waypoint, or the nearest point possible."));
        Squad2Menu.AddItem(EnterLeaveVehicleSquad2 = new UIMenuItem("Enter/exit/land vehicle", "The squad will leave their current vehicle or go back to their last vehicle, if they are on foot. Also counts as 'landing' when in an Helicopter."));
        mainMenu.BindMenuToItem(Squad2Menu, Squad2MenuItem);

        /*
        UIResRectangle banner2 = new UIResRectangle();
        banner2.Color = Color.FromArgb(255, 143, 124, 0);
        */



        //SQUAD 3 MAIN MENU
        Squad3MenuItem = new UIMenuItem("~g~Squad 3");
        mainMenu.AddItem(Squad3MenuItem);
        Squad3Menu = new UIMenu("~g~Squad 3", "");
        _menuPool.Add(Squad3Menu);

        //SQUAD 3 SETUP
        var Squad3SetupItem = new UIMenuItem("Squad Setup", "Set up this squad and configure their behavior.");
        Squad3SetupItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        Squad3Menu.AddItem(Squad3SetupItem);
        var Squad3Setup = new UIMenu("~g~Squad Setup", "~g~Configure the first Squad from there.");
        _menuPool.Add(Squad3Setup);
        Squad3Menu.BindMenuToItem(Squad3Setup, Squad3SetupItem);
        Squad3Setup.AddItem(Squad3StyleItem = new UIMenuListItem("Unit Style", SquadStyle, 0));
        Squad3Setup.AddItem(Squad3NumberItem = new UIMenuListItem("Units", NumberOfPeople, 7));
        Squad3Setup.AddItem(Squad3VehicleSelection = new UIMenuListItem("Vehicle", SquadVehicles, 0, "The vehicle they will come in. Be aware of how many passengers the vehicle will fit in."));
        Squad3Setup.AddItem(MainWeaponSquad3List = new UIMenuListItem("Main weapon", MainWeaponsSquad, 0, "This list includes heavy weapons."));
        Squad3Setup.AddItem(SecondaryWeaponSquad3List = new UIMenuListItem("Secondary weapon", SecondaryWeaponsSquad, 0, "This list includes melee and light weapons."));
        Squad3Setup.AddItem(Squad3Autocall = new UIMenuCheckboxItem("Re-call when defeated", false));
        Squad3Setup.AddItem(Squad3DispatchToWaypoint = new UIMenuCheckboxItem("Dispatch to Waypoint", false));
        Squad3DispatchToWaypoint.Description = "If checked, the squad will be dispatched to your Waypoint instead of your actual position.";
        Squad3Setup.AddItem(CallSquad3 = new UIMenuItem("Call Squad", "Spawn this squad with the above defined paramet."));
        Squad3Setup.AddItem(DeleteSquad3 = new UIMenuItem("Dismiss Squad", "Make the Squad leave."));

        //SQUAD 3 CONFIG BEHAVIOR
        var Squad3ConfigItem = new UIMenuItem("Squad Behaviour", "");
        Squad3ConfigItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad3Config = new UIMenu("~g~Squad Behaviour", "~g~These options affect the squad in real time.");
        _menuPool.Add(Squad3Config);
        Squad3Menu.AddItem(Squad3ConfigItem);
        Squad3Menu.BindMenuToItem(Squad3Config, Squad3ConfigItem);
        Squad3Config.AddItem(DrivingStyleSquad3 = new UIMenuListItem("Driving style", DrivingStyle, 1, "How the leader will drive. Careful: Slow, overtaking with care. Normal: Somewhat fast, overtaking with care. Fast: Fastest, careless overtaking."));
        Squad3Config.AddItem(Squad3ReactToEvents = new UIMenuCheckboxItem("React to enemies", true, "When enabled, the squad will only obey when is not in combat."));
        Squad3Config.AddItem(Squad3Followplayer = new UIMenuCheckboxItem("Follow Player", false, "The Squad will follow you automatically. They will hop in your vehicles, too."));
        Squad3Config.AddItem(Squad3GodMode = new UIMenuCheckboxItem("God Mode", false, "People kept asking for this."));

        //SQUAD 3 SPECIAL ORDERS
        var Squad3SpecialOrdersItem = new UIMenuItem("Special Orders", "This submenu contains orders that can only be issued under certain conditions.");
        Squad3Menu.AddItem(Squad3SpecialOrdersItem);
        Squad3SpecialOrdersItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad3SpecialOrdersMenu = new UIMenu("~g~Special Orders", "~g~Not so common orders.");
        _menuPool.Add(Squad3SpecialOrdersMenu);

        Squad3Menu.BindMenuToItem(Squad3SpecialOrdersMenu, Squad3SpecialOrdersItem);
        Squad3SpecialOrdersMenu.AddItem(SwitchSirensSquad3 = new UIMenuItem("Siren on/off", "If the squad leader is in an Emergency vehicle, it will switch the vehicle's sirens on or off."));
        Squad3SpecialOrdersMenu.AddItem(RappelSquad3 = new UIMenuItem("Rappel From Heli", "If the squad is in an Helicopter that allows rappeling, the passengers will rappel to the ground."));
        Squad3SpecialOrdersMenu.AddItem(AttachVehiclesSquad3 = new UIMenuItem("Attach/Detach vehicle", "If the squad is driving a Cargobob, Truck or Towtruck, they will attach nearby vehicles to them."));
        Squad3SpecialOrdersMenu.AddItem(ParkNearbySquad3 = new UIMenuItem("Park Nearby", "The Squad leader will try to park his vehicle nearby."));
        Squad3SpecialOrdersMenu.AddItem(FollowmeOffroadSquad3 = new UIMenuItem("Follow me, we're going offroad", "The Squad leader will try follow you offroad."));
        Squad3SpecialOrdersMenu.AddItem(GuardThisAreaSquad3 = new UIMenuItem("Watch out for enemies", "The Squad leader will try to watch out for enemies."));
        Squad3SpecialOrdersMenu.AddItem(FleeSquad3 = new UIMenuItem("Flee in this vehicle", "The Squad leader will try to flee in the current vehicle"));
        Squad3SpecialOrdersMenu.AddItem(Squad3RecruitNear = new UIMenuItem("Recruit Nearby peds", "Any ped close to the Squad Leader will become part of their squad."));

        //SQUAD 3 RELATIONSHIPS
        var Squad3RelationshipsItem = new UIMenuItem("Relationships Menu", "Define the faction relationships.");
        Squad3Config.AddItem(Squad3RelationshipsItem);

        Squad3RelationshipsItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad3RelationshipsMenu = new UIMenu("~g~Relationships Menu", "~g~Define the faction relationships.");
        _menuPool.Add(Squad3RelationshipsMenu);
        Squad3Config.BindMenuToItem(Squad3RelationshipsMenu, Squad3RelationshipsItem);

        Squad3RelationshipsMenu.AddItem(Squad3HateSquad1 = new UIMenuCheckboxItem("Hate Squad 1", false, "While checked, SQUAD 3 will attack Squad 2 on sight."));
        Squad3RelationshipsMenu.AddItem(Squad3HateSquad2 = new UIMenuCheckboxItem("Hate Squad 2", false, "While checked, SQUAD 3 will attack Squad 2 on sight."));
        Squad3RelationshipsMenu.AddItem(Squad3HateSquad4 = new UIMenuCheckboxItem("Hate Squad 4", false, "While checked, SQUAD 3 will attack Squad 2 on sight."));
        Squad3RelationshipsMenu.AddItem(Squad3HatePlayer = new UIMenuCheckboxItem("Hate Player", false, "While checked, SQUAD 3 will attack you on sight."));

        Squad3RelationshipsMenu.AddItem(Squad3Hate = new UIMenuItem("Open Hate Input", "Any RelationshipGroup you type here will be hated by this Squad. If the squad already hates that RelationShipGroup, they will stop hating it."));
        Squad3RelationshipsMenu.AddItem(Squad3Like = new UIMenuItem("Open Allies Input", "Any RelationshipGroup you type here will be liked by this Squad. If the squad already likes RelationShipGroup, they will stop liking it."));

        //SQUAD 3 MAIN MENU PART 2
        Squad3Menu.AddItem(EscortMeSquad3 = new UIMenuItem("Escort my vehicle", "The leader will follow your vehicle. If you're on foot, Helicopters can follow you too."));
        Squad3Menu.AddItem(GetBackToMeSquad3 = new UIMenuItem("Get back to me", "The leader will try to run/drive to your position, or the nearest point possible. They will get to their vehicles if you're too far."));
        Squad3Menu.AddItem(GoToWaypointSquad3 = new UIMenuItem("Go to waypoint", "The leader will try to run/drive to the waypoint, or the nearest point possible."));
        Squad3Menu.AddItem(EnterLeaveVehicleSquad3 = new UIMenuItem("Enter/exit/land vehicle", "The squad will leave their current vehicle or go back to their last vehicle, if they are on foot. Also counts as 'landing' when in an Helicopter."));
        mainMenu.BindMenuToItem(Squad3Menu, Squad3MenuItem);

        /*
        var banner3 = new UIResRectangle();
        banner3.Color = Color.FromArgb(255, 12, 173, 0);
        Squad3Menu.SetBannerType(banner3);
        Squad3Config.SetBannerType(banner3);
        Squad3Setup.SetBannerType(banner3);
        Squad3SpecialOrdersMenu.SetBannerType(banner3);
        Squad3RelationshipsMenu.SetBannerType(banner3);

        */


        //SQUAD 4 MAIN MENU
        Squad4MenuItem = new UIMenuItem("~o~Squad 4");
        mainMenu.AddItem(Squad4MenuItem);
        Squad4Menu = new UIMenu("~o~Squad 4", "");
        _menuPool.Add(Squad4Menu);

        //SQUAD 4 SETUP
        var Squad4SetupItem = new UIMenuItem("Squad Setup", "Set up this squad and configure their behavior.");
        Squad4SetupItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        Squad4Menu.AddItem(Squad4SetupItem);
        var Squad4Setup = new UIMenu("~o~Squad Setup", "~o~Configure the first Squad from there.");
        _menuPool.Add(Squad4Setup);
        Squad4Menu.BindMenuToItem(Squad4Setup, Squad4SetupItem);
        Squad4Setup.AddItem(Squad4StyleItem = new UIMenuListItem("Unit Style", SquadStyle, 0));
        Squad4Setup.AddItem(Squad4NumberItem = new UIMenuListItem("Units", NumberOfPeople, 7));
        Squad4Setup.AddItem(Squad4VehicleSelection = new UIMenuListItem("Vehicle", SquadVehicles, 0, "The vehicle they will come in. Be aware of how many passengers the vehicle will fit in."));
        Squad4Setup.AddItem(MainWeaponSquad4List = new UIMenuListItem("Main weapon", MainWeaponsSquad, 0, "This list includes heavy weapons."));
        Squad4Setup.AddItem(SecondaryWeaponSquad4List = new UIMenuListItem("Secondary weapon", SecondaryWeaponsSquad, 0, "This list includes melee and light weapons."));
        Squad4Setup.AddItem(Squad4Autocall = new UIMenuCheckboxItem("Re-call when defeated", false));
        Squad4Setup.AddItem(Squad4DispatchToWaypoint = new UIMenuCheckboxItem("Dispatch to Waypoint", false));
        Squad4DispatchToWaypoint.Description = "If checked, the squad will be dispatched to your Waypoint instead of your actual position.";
        Squad4Setup.AddItem(CallSquad4 = new UIMenuItem("Call Squad", "Spawn this squad with the above defined paramet."));
        Squad4Setup.AddItem(DeleteSquad4 = new UIMenuItem("Dismiss Squad", "Make the Squad leave."));

        //SQUAD 4 CONFIG BEHAVIOR
        var Squad4ConfigItem = new UIMenuItem("Squad Behaviour", "");
        Squad4ConfigItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad4Config = new UIMenu("~o~Squad Behaviour", "~o~These options affect the squad in real time.");
        _menuPool.Add(Squad4Config);
        Squad4Menu.AddItem(Squad4ConfigItem);
        Squad4Menu.BindMenuToItem(Squad4Config, Squad4ConfigItem);
        Squad4Config.AddItem(DrivingStyleSquad4 = new UIMenuListItem("Driving style", DrivingStyle, 1, "How the leader will drive. Careful: Slow, overtaking with care. Normal: Somewhat fast, overtaking with care. Fast: Fastest, careless overtaking."));
        Squad4Config.AddItem(Squad4ReactToEvents = new UIMenuCheckboxItem("React to enemies", true, "When enabled, the squad will only obey when is not in combat."));
        Squad4Config.AddItem(Squad4Followplayer = new UIMenuCheckboxItem("Follow Player", false, "The Squad will follow you automatically. They will hop in your vehicles, too."));
        Squad4Config.AddItem(Squad4GodMode = new UIMenuCheckboxItem("God Mode", false, "People kept asking for this."));

        //SQUAD 4 SPECIAL ORDERS
        var Squad4SpecialOrdersItem = new UIMenuItem("Special Orders", "This submenu contains orders that can only be issued under certain conditions.");
        Squad4Menu.AddItem(Squad4SpecialOrdersItem);
        Squad4SpecialOrdersItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad4SpecialOrdersMenu = new UIMenu("~o~Special Orders", "~o~Not so common orders.");
        _menuPool.Add(Squad4SpecialOrdersMenu);

        Squad4Menu.BindMenuToItem(Squad4SpecialOrdersMenu, Squad4SpecialOrdersItem);
        Squad4SpecialOrdersMenu.AddItem(SwitchSirensSquad4 = new UIMenuItem("Siren on/off", "If the squad leader is in an Emergency vehicle, it will switch the vehicle's sirens on or off."));
        Squad4SpecialOrdersMenu.AddItem(RappelSquad4 = new UIMenuItem("Rappel From Heli", "If the squad is in an Helicopter that allows rappeling, the passengers will rappel to the ground."));
        Squad4SpecialOrdersMenu.AddItem(AttachVehiclesSquad4 = new UIMenuItem("Attach/Detach vehicle", "If the squad is driving a Cargobob, Truck or Towtruck, they will attach nearby vehicles to them."));
        Squad4SpecialOrdersMenu.AddItem(ParkNearbySquad4 = new UIMenuItem("Park Nearby", "The Squad leader will try to park his vehicle nearby."));
        Squad4SpecialOrdersMenu.AddItem(FollowmeOffroadSquad4 = new UIMenuItem("Follow me, we're going offroad", "The Squad leader will try follow you offroad."));
        Squad4SpecialOrdersMenu.AddItem(GuardThisAreaSquad4 = new UIMenuItem("Watch out for enemies", "The Squad leader will try to watch out for enemies."));
        Squad4SpecialOrdersMenu.AddItem(FleeSquad4 = new UIMenuItem("Flee in this vehicle", "The Squad leader will try to flee in the current vehicle"));
        Squad4SpecialOrdersMenu.AddItem(Squad4RecruitNear = new UIMenuItem("Recruit Nearby peds", "Any ped close to the Squad Leader will become part of their squad."));

        //SQUAD 4 RELATIONSHIPS
        var Squad4RelationshipsItem = new UIMenuItem("Relationships Menu", "Define the faction relationships.");
        Squad4Config.AddItem(Squad4RelationshipsItem);

        Squad4RelationshipsItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        var Squad4RelationshipsMenu = new UIMenu("~o~Relationships Menu", "~o~Define the faction relationships.");
        _menuPool.Add(Squad4RelationshipsMenu);
        Squad4Config.BindMenuToItem(Squad4RelationshipsMenu, Squad4RelationshipsItem);

        Squad4RelationshipsMenu.AddItem(Squad4HateSquad1 = new UIMenuCheckboxItem("Hate Squad 1", false, "While checked, SQUAD 4 will attack Squad 2 on sight."));
        Squad4RelationshipsMenu.AddItem(Squad4HateSquad2 = new UIMenuCheckboxItem("Hate Squad 2", false, "While checked, SQUAD 4 will attack Squad 2 on sight."));
        Squad4RelationshipsMenu.AddItem(Squad4HateSquad3 = new UIMenuCheckboxItem("Hate Squad 3", false, "While checked, SQUAD 4 will attack Squad 2 on sight."));
        Squad4RelationshipsMenu.AddItem(Squad4HatePlayer = new UIMenuCheckboxItem("Hate Player", false, "While checked, SQUAD 4 will attack you on sight."));

        Squad4RelationshipsMenu.AddItem(Squad4Hate = new UIMenuItem("Open Hate Input", "Any RelationshipGroup you type here will be hated by this Squad. If the squad already hates that RelationShipGroup, they will stop hating it."));
        Squad4RelationshipsMenu.AddItem(Squad4Like = new UIMenuItem("Open Allies Input", "Any RelationshipGroup you type here will be liked by this Squad. If the squad already likes RelationShipGroup, they will stop liking it."));

        //SQUAD 4 MAIN MENU PART 2
        Squad4Menu.AddItem(EscortMeSquad4 = new UIMenuItem("Escort my vehicle", "The leader will follow your vehicle. If you're on foot, Helicopters can follow you too."));
        Squad4Menu.AddItem(GetBackToMeSquad4 = new UIMenuItem("Get back to me", "The leader will try to run/drive to your position, or the nearest point possible. They will get to their vehicles if you're too far."));
        Squad4Menu.AddItem(GoToWaypointSquad4 = new UIMenuItem("Go to waypoint", "The leader will try to run/drive to the waypoint, or the nearest point possible."));
        Squad4Menu.AddItem(EnterLeaveVehicleSquad4 = new UIMenuItem("Enter/exit/land vehicle", "The squad will leave their current vehicle or go back to their last vehicle, if they are on foot. Also counts as 'landing' when in an Helicopter."));
        mainMenu.BindMenuToItem(Squad4Menu, Squad4MenuItem);

        /*
        UIResRectangle banner4 = new UIResRectangle();
        banner4.Color = Squad4Color;
        Squad4Menu.SetBannerType(banner4);
        Squad4Config.SetBannerType(banner4);
        Squad4Setup.SetBannerType(banner4);
        Squad4SpecialOrdersMenu.SetBannerType(banner4);
        Squad4RelationshipsMenu.SetBannerType(banner4);
        */


        //ALL SQUADS MAIN MENU
        mainMenu.AddItem(AllSquadsMenuItem = new UIMenuItem("All Squads", "Apply orders to all the squads at once."));
        _menuPool.Add(AllSquadsMenu = new UIMenu("All Squads", "Apply orders to all the squads at once."));
        mainMenu.BindMenuToItem(AllSquadsMenu, AllSquadsMenuItem);

        AllSquadsMenu.AddItem(AllGoToWaypoint = new UIMenuItem("Go to waypoint", "The leader will try to run/drive to the waypoint, or the nearest point possible."));
        AllSquadsMenu.AddItem(AllGetBackToMe = new UIMenuItem("Get back to me", "The leader will try to run/drive to your position, or the nearest point possible. They will get to their vehicles if you're too far."));
        AllSquadsMenu.AddItem(AllEnterLeave = new UIMenuItem("Enter/Leave/Land vehicle", "The squads will leave their current vehicles or go back to their last vehicles, if they are on foot. Also counts as 'landing' when in an Helicopter."));
        AllSquadsMenu.AddItem(AllCallSquad = new UIMenuItem("Call", "All the Squads will be spawned with their current setup."));
        AllSquadsMenu.AddItem(AllDismissSquad = new UIMenuItem("Dimiss", "All the Squads will be dismissed."));
        AllSquadsMenu.AddItem(ShowStatus = new UIMenuCheckboxItem("ShowStatus", true, "Show the status HUD?"));
        AllSquadsMenu.AddItem(ShowBlips = new UIMenuCheckboxItem("ShowBlips", true, "Show blips?"));

        //ALL SQUADS RACE
        AllSquadsMenu.AddItem(AllSquadsRaceMenuItem = new UIMenuItem("Race commands", "Some commands to set up races with the squads."));
        _menuPool.Add(AllSquadsRaceMenu = new UIMenu("Race Commands", "Smoke 'em."));
        AllSquadsRaceMenuItem.SetLeftBadge(UIMenuItem.BadgeStyle.Michael);
        AllSquadsMenu.BindMenuToItem(AllSquadsRaceMenu, AllSquadsRaceMenuItem);

        AllSquadsRaceMenu.AddItem(AllSquadsAddWaypoint = new UIMenuItem("Add Waypoint", "The current map waypoint position will be added as a waypoint for the race. If there is no map waypoint set, your own position will be used."));
        AllSquadsRaceMenu.AddItem(AllSquadsClearWaypoints = new UIMenuItem("Clear All Waypoints", "Remove all waypoints so you can start from scratch."));
        AllSquadsRaceMenu.AddItem(AllParkAtMySide = new UIMenuItem("Park at my sides", "The Squad leaders will park at your sides. Think of it as an improvised starting line."));
        AllSquadsRaceMenu.AddItem(AllSquadsStartRace = new UIMenuItem("Start Race", "Makes all squad leaders drive their vehicles through the waypoints you defined. They will stop at the last one."));
        /*
        UIResRectangle banner5 = new UIResRectangle();
        banner5.Color = Color.FromArgb(255, 118, 118, 118);
        
        AllSquadsRaceMenu.SetBannerType(banner5);
        AllSquadsMenu.SetBannerType(banner5);
        */

        mainMenu.MouseControlsEnabled = false;
        foreach (UIMenu menu in _menuPool.ToList())
        {
            menu.RefreshIndex();
            menu.OnItemSelect += OnItemSelect;
            menu.OnListChange += OnListChange;
            menu.OnCheckboxChange += OnCheckboxChange;
            menu.OnIndexChange += OnItemChange;
            menu.OnMenuClose += OnMenuClose;
        }


        //SQUADS LOVE U
        World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Game.Player.Character.RelationshipGroup);
        World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Game.Player.Character.RelationshipGroup);
        World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Game.Player.Character.RelationshipGroup);
        World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Game.Player.Character.RelationshipGroup);

        //NOoSE Mod compatiblity
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "MOD_NOOSE"), Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "MOD_NOOSE"), Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "MOD_NOOSE"), Squad3RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "MOD_NOOSE"), Squad4RelationshipGroup, Relationship.Companion);

        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "noose_targets"), Squad1RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "noose_targets"), Squad2RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "noose_targets"), Squad3RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "noose_targets"), Squad4RelationshipGroup, Relationship.Hate);

        //Arms Trading compatiblity        
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "Enemy Gang"), Squad1RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "Enemy Gang"), Squad2RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "Enemy Gang"), Squad3RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "Enemy Gang"), Squad4RelationshipGroup, Relationship.Hate);

        //Portable Warzones compatiblity
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "BlueTeamRLGroup"), Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "BlueTeamRLGroup"), Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "BlueTeamRLGroup"), Squad3RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "BlueTeamRLGroup"), Squad4RelationshipGroup, Relationship.Companion);

        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "RedTeamRLGroup"), Squad1RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "RedTeamRLGroup"), Squad2RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "RedTeamRLGroup"), Squad3RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "RedTeamRLGroup"), Squad4RelationshipGroup, Relationship.Hate);

        //General compatiblity
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "HATES_PLAYER"), Squad1RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "HATES_PLAYER"), Squad2RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "HATES_PLAYER"), Squad3RelationshipGroup, Relationship.Hate);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "HATES_PLAYER"), Squad4RelationshipGroup, Relationship.Hate);


        /*
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "PLAYER"), Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "PLAYER"), Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "PLAYER"), Squad3RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "PLAYER"), Squad4RelationshipGroup, Relationship.Companion);
        */



        //SQUADS LOVE EACH OTHER BY DEFAULT
        SetRelationshipGroup(Squad1RelationshipGroup, Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad1RelationshipGroup, Squad3RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad1RelationshipGroup, Squad4RelationshipGroup, Relationship.Companion);

        SetRelationshipGroup(Squad2RelationshipGroup, Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad2RelationshipGroup, Squad3RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad2RelationshipGroup, Squad4RelationshipGroup, Relationship.Companion);

        SetRelationshipGroup(Squad3RelationshipGroup, Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad3RelationshipGroup, Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad3RelationshipGroup, Squad4RelationshipGroup, Relationship.Companion);

        SetRelationshipGroup(Squad4RelationshipGroup, Squad1RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad4RelationshipGroup, Squad2RelationshipGroup, Relationship.Companion);
        SetRelationshipGroup(Squad4RelationshipGroup, Squad3RelationshipGroup, Relationship.Companion);

    }
    bool SettingsLoaded = false;
    public BodyguardSquads()
    {

        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;


    }
    //SQUAD 1 LOAD CONFIG, NOT FINISHED

    /*
        void LoadSquadSettings()
        {
            XmlDocument driversdoc = new XmlDocument();
            driversdoc.Load(@"scripts\AdvancedBodyguards.xml");
            if(driversdoc != null)
            {
                Squad1StyleItem.Index = int.Parse(driversdoc.SelectSingleNode("//Squad1/Model").InnerText);
                Squad1VehicleSelection.Index = int.Parse(driversdoc.SelectSingleNode("//Squad1/Vehicle").InnerText);
                Squad1Vehicle = SquadTransportHashes[Squad1VehicleSelection.Index];
            }

            Squad1StylePed = Squad1StyleItem.IndexToItem(Squad1StyleItem.Index);

                if (Squad1StylePed == "Army")
                {
                    Squad1Models = ArmyModels;
                }
                if (Squad1StylePed == "Police")
                {
                    Squad1Models = PoliceModels;
                }
                if (Squad1StylePed == "Rednecks")
                {
                    Squad1Models = RedneckModels;
                }
                if (Squad1StylePed == "Private Security")
                {
                    Squad1Models = PrivateSecurityModels;
                }
                if (Squad1StylePed == "Ballas")
                {
                    Squad1Models = BallasModels;
                }
                if (Squad1StylePed == "Families")
                {
                    Squad1Models = FamiliesModels;
                }
                if (Squad1StylePed == "SWAT")
                {
                    Squad1Models = SWATModels;
                }
                if (Squad1StylePed == "The Lost")
                {
                    Squad1Models = LostModels;
                }
                if (Squad1StylePed == CustomStyle1Name + "")
                {
                    Squad1Models = CustomModels1;
                }
                if (Squad1StylePed == CustomStyle2Name + "")
                {
                    Squad1Models = CustomModels2;
                }
        }
        */

    //TEST, NOT USED
    public void RefreshSquadDesc()
    {
        Squad1MenuItem.Description = IsSquadActive(1);
        Squad2MenuItem.Description = IsSquadActive(2);
    }
    public string IsSquadActive(int Squad)
    {
        switch (Squad)
        {
            case 1:
                {
                    if (CanWeUse(Squad1Leader))
                    {
                        if (Squad1Leader.IsSittingInVehicle())
                        {
                            return "In "+Squad1Leader.CurrentVehicle.FriendlyName;
                        }
                        else
                        {
                            return "On foot";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            case 2:
                {
                    if (CanWeUse(Squad2Leader))
                    {
                        if (Squad2Leader.IsSittingInVehicle())
                        {
                            return "In "+ Squad2Leader.CurrentVehicle.FriendlyName;
                        }
                        else
                        {
                            return "On Foot";

                        }
                    }
                    else
                    {
                        return "";
                    }
                }
        }

        return "";
    }



    public void OnMenuClose(UIMenu sender)
    {
        if (sender == mainMenu)
        {
            Game.TimeScale = 1;
        }
    }
    public void OnItemChange(UIMenu sender, int index)
    {

    }

    public void OnCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
    {
        if (checkbox == Squad1ReactToEvents)
        {
            if (CanWeUse(Squad1Leader))
            {

                Squad1Leader.BlockPermanentEvents = !Checked;
            }
            else
            {
                UI.Notify("~b~Squad 1 ~w~does not exist.");
            }
        }

        if (checkbox == Squad2ReactToEvents)
        {
            if (CanWeUse(Squad2Leader))
            {

                Squad2Leader.BlockPermanentEvents = !Checked;
            }
            else
            {
                UI.Notify("~y~Squad 2~w~ does not exist.");
            }
        }

        if (checkbox == Squad3ReactToEvents)
        {
            if (CanWeUse(Squad3Leader))
            {

                Squad3Leader.BlockPermanentEvents = !Checked;
            }
            else
            {
                UI.Notify("~b~Squad 1 ~w~does not exist.");
            }
        }
        if (checkbox == Squad4ReactToEvents)
        {
            if (CanWeUse(Squad4Leader))
            {

                Squad4Leader.BlockPermanentEvents = !Checked;
            }
            else
            {
                UI.Notify("~b~Squad 1 ~w~does not exist.");
            }
        }
        if (checkbox == Squad1HateSquad2)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Squad2RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Squad2RelationshipGroup);
            }
        }
        if (checkbox == Squad1HateSquad3)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Squad3RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Squad3RelationshipGroup);
            }
        }
        if (checkbox == Squad1HateSquad2)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Squad4RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Squad4RelationshipGroup);
            }
        }

        if (checkbox == Squad1HatePlayer)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
        }

        if (checkbox == Squad2HatePlayer)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
        }

        if (checkbox == Squad3HatePlayer)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad3RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
        }

        if (checkbox == Squad4HatePlayer)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad4RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Game.Player.Character.RelationshipGroup);
            }
        }

        if (checkbox == Squad2HateSquad1)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Squad1RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Squad1RelationshipGroup);
            }
        }
        if (checkbox == Squad2HateSquad3)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Squad3RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Squad3RelationshipGroup);
            }
        }
        if (checkbox == Squad2HateSquad4)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Squad4RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Squad4RelationshipGroup);
            }
        }
        if (checkbox == Squad3HateSquad1)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad3RelationshipGroup, Squad1RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Squad1RelationshipGroup);
            }
        }
        if (checkbox == Squad3HateSquad2)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad3RelationshipGroup, Squad2RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Squad2RelationshipGroup);
            }
        }
        if (checkbox == Squad3HateSquad4)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad3RelationshipGroup, Squad4RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Squad4RelationshipGroup);
            }
        }
        if (checkbox == Squad4HateSquad1)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad4RelationshipGroup, Squad1RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Squad1RelationshipGroup);
            }
        }
        if (checkbox == Squad4HateSquad2)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad4RelationshipGroup, Squad2RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Squad2RelationshipGroup);
            }
        }
        if (checkbox == Squad4HateSquad3)
        {
            if (Checked)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad4RelationshipGroup, Squad3RelationshipGroup);
            }
            else
            {
                World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Squad3RelationshipGroup);
            }
        }
        if (checkbox == Squad2GodMode)
        {
            foreach (Ped ped in Squad2)
            {
                ped.IsInvincible = Checked;
            }
            if (CanWeUse(GetLastVehicle(Squad2Leader)))
            {
                GetLastVehicle(Squad2Leader).IsInvincible = Checked;
            }
        }

        if (checkbox == Squad3GodMode)
        {
            foreach (Ped ped in Squad3)
            {
                ped.IsInvincible = Checked;
            }
            if (CanWeUse(GetLastVehicle(Squad3Leader)))
            {
                GetLastVehicle(Squad3Leader).IsInvincible = Checked;
            }
        }
        if (checkbox == Squad4GodMode)
        {
            foreach (Ped ped in Squad4)
            {
                ped.IsInvincible = Checked;
            }
            if (CanWeUse(GetLastVehicle(Squad4Leader)))
            {
                GetLastVehicle(Squad4Leader).IsInvincible = Checked;
            }
        }

        if (checkbox == Squad1GodMode)
        {
            foreach (Ped ped in Squad1)
            {
                ped.IsInvincible = Checked;
            }

        }
        if (checkbox == Squad1Followplayer)
        {
            if (CanWeUse(Squad1Leader))
            {
                if (Squad1Followplayer.Checked)
                {
                    UI.ShowSubtitle("~b~Squad 1~w~ will follow you from now on.");
                    foreach (Ped ped in Squad1)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle));
                    }
                }
                else
                {
                    UI.ShowSubtitle("~b~Squad 1 ~w~won't follow you from now on.");

                    int Squad1Group = Function.Call<int>(Hash.CREATE_GROUP);

                    foreach (Ped ped in Squad1)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        if (ped == Squad1Leader)
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad1Group);
                        }
                        else
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Squad1Group);
                            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, 1);
                        }
                    }
                }
            }
            else
            {
                UI.Notify("~b~Squad 1 ~w~does not exist.");
            }
        }


        if (checkbox == Squad2Followplayer)
        {
            if (CanWeUse(Squad2Leader))
            {
                UI.ShowSubtitle("~y~Squad 2 ~w~will follow you from now on.");

                if (Squad2Followplayer.Checked)
                {
                    foreach (Ped ped in Squad2)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle));
                    }
                }
                else
                {
                    UI.ShowSubtitle("~y~Squad 2~w~ won't follow you from now on.");

                    int Squad2Group = Function.Call<int>(Hash.CREATE_GROUP);

                    foreach (Ped ped in Squad2)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        if (ped == Squad2Leader)
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad2Group);
                        }
                        else
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Squad2Group);
                            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, 1);
                        }
                    }
                }
            }
            else
            {
                UI.Notify("~y~Squad 2~w~ does not exist.");
            }
        }

        if (checkbox == Squad3Followplayer)
        {
            if (CanWeUse(Squad3Leader))
            {
                if (Squad3Followplayer.Checked)
                {
                    UI.ShowSubtitle("~b~Squad 3~w~ will follow you from now on.");
                    foreach (Ped ped in Squad3)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle));
                    }
                }
                else
                {
                    UI.ShowSubtitle("~b~Squad 3 ~w~won't follow you from now on.");

                    int Squad3Group = Function.Call<int>(Hash.CREATE_GROUP);

                    foreach (Ped ped in Squad3)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        if (ped == Squad3Leader)
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad3Group);
                        }
                        else
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Squad3Group);
                            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, 1);
                        }
                    }
                }
            }
            else
            {
                UI.Notify("~b~Squad 3 ~w~does not exist.");
            }
        }

        if (checkbox == Squad4Followplayer)
        {
            if (CanWeUse(Squad4Leader))
            {
                if (Squad4Followplayer.Checked)
                {
                    UI.ShowSubtitle("~b~Squad 3~w~ will follow you from now on.");
                    foreach (Ped ped in Squad4)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle));
                    }
                }
                else
                {
                    UI.ShowSubtitle("~b~Squad 3 ~w~won't follow you from now on.");

                    int Squad4Group = Function.Call<int>(Hash.CREATE_GROUP);

                    foreach (Ped ped in Squad4)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
                        if (ped == Squad4Leader)
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad4Group);
                        }
                        else
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Squad4Group);
                            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, 1);
                        }
                    }
                }
            }
            else
            {
                UI.Notify("~b~Squad 3 ~w~does not exist.");
            }
        }
    }

    public void OnListChange(UIMenu sender, UIMenuListItem list, int index)
    {
        if (list == Squad1NumberItem)
        {
            Squad1Number = list.IndexToItem(index);
        }
        if (list == Squad1VehicleSelection)
        {
            Squad1Vehicle = SquadTransportHashes[index];
        }
        if (list == Squad2NumberItem)
        {
            Squad2Number = list.IndexToItem(index);
        }
        if (list == Squad2VehicleSelection)
        {
            Squad2Vehicle = SquadTransportHashes[index];
        }


        if (list == Squad3NumberItem)
        {
            Squad3Number = list.IndexToItem(index);
        }
        if (list == Squad3VehicleSelection)
        {
            Squad3Vehicle = SquadTransportHashes[index];
        }
        if (list == Squad4NumberItem)
        {
            Squad4Number = list.IndexToItem(index);
        }
        if (list == Squad4VehicleSelection)
        {
            Squad4Vehicle = SquadTransportHashes[index];
        }

        if (list == MainWeaponSquad1List)
        {
            MainWeaponSquad1 = MainWeaponHashes[index];
        }
        if (list == SecondaryWeaponSquad1List)
        {
            SecondaryWeaponSquad1 = SecondaryWeaponHashes[index];
        }
        if (list == MainWeaponSquad2List)
        {
            MainWeaponSquad2 = MainWeaponHashes[index];
        }
        if (list == SecondaryWeaponSquad2List)
        {
            SecondaryWeaponSquad2 = SecondaryWeaponHashes[index];
        }

        if (list == MainWeaponSquad3List)
        {
            MainWeaponSquad3 = MainWeaponHashes[index];
        }
        if (list == SecondaryWeaponSquad3List)
        {
            SecondaryWeaponSquad3 = SecondaryWeaponHashes[index];
        }
        if (list == MainWeaponSquad4List)
        {
            MainWeaponSquad4 = MainWeaponHashes[index];
        }
        if (list == SecondaryWeaponSquad4List)
        {
            SecondaryWeaponSquad4 = SecondaryWeaponHashes[index];
        }
        if (list == Squad1StyleItem)
        {
            Squad1StylePed = list.IndexToItem(index);
            if (Squad1StylePed == "Army")
            {
                Squad1Models = ArmyModels;
            }
            if (Squad1StylePed == "Police")
            {
                Squad1Models = PoliceModels;
            }
            if (Squad1StylePed == "Rednecks")
            {
                Squad1Models = RedneckModels;
            }
            if (Squad1StylePed == "Private Security")
            {
                Squad1Models = PrivateSecurityModels;
            }           
            if (Squad1StylePed == "Ballas")
            {
                Squad1Models = BallasModels;
            }
            if (Squad1StylePed == "Families")
            {
                Squad1Models = FamiliesModels;
            }
            if (Squad1StylePed == "SWAT")
            {
                Squad1Models = SWATModels;
            }
            if (Squad1StylePed == "The Lost")
            {
                Squad1Models = LostModels;
            }
            if (Squad1StylePed == CustomStyle1Name + "")
            {
                Squad1Models = CustomModels1;
            }
            if (Squad1StylePed == CustomStyle2Name + "")
            {
                Squad1Models = CustomModels2;
            }
        }
        if (list == Squad2StyleItem)
        {
            Squad2StylePed = list.IndexToItem(index);

            if (Squad2StylePed == "Army")
            {
                Squad2Models = ArmyModels;

            }
            if (Squad2StylePed == "Police")
            {
                Squad2Models = PoliceModels;
            }
            if (Squad2StylePed == "Rednecks")
            {
                Squad2Models = RedneckModels;
            }
            if (Squad2StylePed == "Private Security")
            {
                Squad2Models = PrivateSecurityModels;
            }

            if (Squad2StylePed == "Families")
            {
                Squad2Models = FamiliesModels;
            }
            if (Squad2StylePed == "SWAT")
            {
                Squad2Models = SWATModels;
            }
            if (Squad2StylePed == "The Lost")
            {
                Squad2Models = LostModels;
            }
            if (Squad2StylePed == CustomStyle1Name+"")
            {
                Squad2Models = CustomModels1;
            }
            if (Squad2StylePed == CustomStyle2Name + "")
            {
                Squad2Models = CustomModels2;
            }
        }

        if (list == Squad3StyleItem)
        {
            Squad3StylePed = list.IndexToItem(index);

            if (Squad3StylePed == "Army")
            {
                Squad3Models = ArmyModels;
            }
            if (Squad3StylePed == "Police")
            {
                Squad3Models = PoliceModels;
            }
            if (Squad3StylePed == "Rednecks")
            {
                Squad3Models = RedneckModels;
            }
            if (Squad3StylePed == "Private Security")
            {
                Squad3Models = PrivateSecurityModels;
            }
            if (Squad3StylePed == "Ballas")
            {
                Squad3Models = BallasModels;
            }
            if (Squad3StylePed == "Families")
            {
                Squad3Models = FamiliesModels;
            }
            if (Squad3StylePed == "SWAT")
            {
                Squad3Models = SWATModels;
            }
            if (Squad3StylePed == "The Lost")
            {
                Squad3Models = LostModels;
            }
            if (Squad3StylePed == CustomStyle1Name + "")
            {
                Squad3Models = CustomModels1;
            }
            if (Squad3StylePed == CustomStyle2Name + "")
            {
                Squad3Models = CustomModels2;
            }
        }
        if (list == Squad4StyleItem)
        {
            Squad4StylePed = list.IndexToItem(index);

            if (Squad4StylePed == "Army")
            {
                Squad4Models = ArmyModels;
            }
            if (Squad4StylePed == "Police")
            {
                Squad4Models = PoliceModels;
            }
            if (Squad4StylePed == "Rednecks")
            {
                Squad4Models = RedneckModels;
            }
            if (Squad4StylePed == "Private Security")
            {
                Squad4Models = PrivateSecurityModels;
            }
            if (Squad4StylePed == "Ballas")
            {
                Squad4Models = BallasModels;
            }
            if (Squad4StylePed == "Families")
            {
                Squad4Models = FamiliesModels;
            }
            if (Squad4StylePed == "SWAT")
            {
                Squad4Models = SWATModels;
            }
            if (Squad4StylePed == "The Lost")
            {
                Squad4Models = LostModels;
            }
            if (Squad4StylePed == CustomStyle1Name + "")
            {
                Squad4Models = CustomModels1;
            }
            if (Squad4StylePed == CustomStyle2Name + "")
            {
                Squad4Models = CustomModels2;
            }
        }
        if (list == DrivingStyleSquad1)
        {

            if (DrivingCareful.Contains(Squad1Leader)) { DrivingCareful.RemoveAt(DrivingCareful.IndexOf(Squad1Leader)); }
            if (DrivingNormal.Contains(Squad1Leader)) { DrivingNormal.RemoveAt(DrivingNormal.IndexOf(Squad1Leader)); }
            if (DrivingFurious.Contains(Squad1Leader)) { DrivingFurious.RemoveAt(DrivingFurious.IndexOf(Squad1Leader)); }

            string Style = list.IndexToItem(index).ToString();
            if (Style == "Careful" && CanWeUse(Squad1Leader))
            {
                DrivingCareful.Add(Squad1Leader);
                SetDrivingStyleAndCruiseSpeed(Squad1Leader,1|2|4|16|32,20f);
            }
            if (Style == "Normal" && CanWeUse(Squad1Leader))
            {
                DrivingNormal.Add(Squad1Leader);
                SetDrivingStyleAndCruiseSpeed(Squad1Leader, 1 | 2 | 4 | 16 | 32, 30f);

            }
            if (Style == "Fast & Furious" && CanWeUse(Squad1Leader))
            {
                DrivingFurious.Add(Squad1Leader);
                SetDrivingStyleAndCruiseSpeed(Squad1Leader, 4 | 16 | 32, 70f);

            }
        }
        if (list == DrivingStyleSquad2)
        {
            if (DrivingCareful.Contains(Squad2Leader)) { DrivingCareful.RemoveAt(DrivingCareful.IndexOf(Squad2Leader)); }
            if (DrivingNormal.Contains(Squad2Leader)) { DrivingNormal.RemoveAt(DrivingNormal.IndexOf(Squad2Leader)); }
            if (DrivingFurious.Contains(Squad2Leader)) { DrivingFurious.RemoveAt(DrivingFurious.IndexOf(Squad2Leader)); }

            string Style = list.IndexToItem(index).ToString();
            if (Style == "Careful" && CanWeUse(Squad2Leader))
            {
                DrivingCareful.Add(Squad2Leader);
                SetDrivingStyleAndCruiseSpeed(Squad2Leader, 1 | 2 | 4 | 16 | 32, 20f);

            }
            if (Style == "Normal" && CanWeUse(Squad2Leader))
            {
                DrivingNormal.Add(Squad2Leader);
                SetDrivingStyleAndCruiseSpeed(Squad2Leader, 1 | 2 | 4 | 16 | 32, 30f);

            }
            if (Style == "Fast & Furious" && CanWeUse(Squad2Leader))
            {
                DrivingFurious.Add(Squad2Leader);
                SetDrivingStyleAndCruiseSpeed(Squad2Leader,  4 | 16 | 32, 70f);

            }
        }
        if (list == DrivingStyleSquad3)
        {
            if (DrivingCareful.Contains(Squad3Leader)) { DrivingCareful.RemoveAt(DrivingCareful.IndexOf(Squad3Leader)); }
            if (DrivingNormal.Contains(Squad3Leader)) { DrivingNormal.RemoveAt(DrivingNormal.IndexOf(Squad3Leader)); }
            if (DrivingFurious.Contains(Squad3Leader)) { DrivingFurious.RemoveAt(DrivingFurious.IndexOf(Squad3Leader)); }

            string Style = list.IndexToItem(index).ToString();
            if (Style == "Careful" && CanWeUse(Squad3Leader))
            {
                DrivingCareful.Add(Squad3Leader);
                SetDrivingStyleAndCruiseSpeed(Squad3Leader, 1 | 2 | 4 | 16 | 32, 20f);

            }
            if (Style == "Normal" && CanWeUse(Squad3Leader))
            {
                DrivingNormal.Add(Squad3Leader);
                SetDrivingStyleAndCruiseSpeed(Squad3Leader, 1 | 2 | 4 | 16 | 32, 30f);

            }
            if (Style == "Fast & Furious" && CanWeUse(Squad3Leader))
            {
                DrivingFurious.Add(Squad3Leader);
                SetDrivingStyleAndCruiseSpeed(Squad3Leader, 4 | 16 | 32, 70f);
            }
        }

        if (list == DrivingStyleSquad4)
        {
            if (DrivingCareful.Contains(Squad4Leader)) { DrivingCareful.RemoveAt(DrivingCareful.IndexOf(Squad4Leader)); }
            if (DrivingNormal.Contains(Squad4Leader)) { DrivingNormal.RemoveAt(DrivingNormal.IndexOf(Squad4Leader)); }
            if (DrivingFurious.Contains(Squad4Leader)) { DrivingFurious.RemoveAt(DrivingFurious.IndexOf(Squad4Leader)); }

            string Style = list.IndexToItem(index).ToString();
            if (Style == "Careful" && CanWeUse(Squad4Leader))
            {
                DrivingCareful.Add(Squad4Leader);
                SetDrivingStyleAndCruiseSpeed(Squad4Leader, 1 | 2 | 4 | 16 | 32, 20f);

            }
            if (Style == "Normal" && CanWeUse(Squad4Leader))
            {
                DrivingNormal.Add(Squad4Leader);
                SetDrivingStyleAndCruiseSpeed(Squad4Leader, 1 | 2 | 4 | 16 | 32, 30f);

            }
            if (Style == "Fast & Furious" && CanWeUse(Squad4Leader))
            {
                DrivingFurious.Add(Squad4Leader);
                SetDrivingStyleAndCruiseSpeed(Squad4Leader, 4 | 16 | 32, 70f);
            }
        }
    }


    void SwitchSirens(Ped RecieveOrder)
    {

        if (CanWeUse(RecieveOrder) && RecieveOrder.IsSittingInVehicle())
        {
            Model veh = RecieveOrder.CurrentVehicle.Model;
            if (VehiclesWithSiren.Contains(veh))
            {
                RecieveOrder.CurrentVehicle.SirenActive = !RecieveOrder.CurrentVehicle.SirenActive;
            }
            else
            {
                UI.ShowSubtitle("This vehicle doesn't have siren.");
            }
        }
    }

    void AttachVehicle(Ped SquadLeader)
    {
        if (CanWeUse(SquadLeader) && SquadLeader.IsSittingInVehicle())
        {
            var veh = SquadLeader.CurrentVehicle;

            if (veh.Model == new Model(VehicleHash.Cargobob))
            {
                if (Function.Call<bool>(GTA.Native.Hash._0x1821D91AD4B56108, veh) == false) //_IS_CARGOBOB_HOOK_ACTIVE
                {
                    Function.Call(GTA.Native.Hash._0x7BEB0C7A235F6F3B, veh);
                }
                else
                {
                }

                if (CanWeUse(Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_ATTACHED_TO_CARGOBOB, veh)))
                {
                    Vehicle vehicle = Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_ATTACHED_TO_CARGOBOB, veh);
                    if (!vehicle.IsStopped)
                    {
                        //vehicle.Position = veh.Position + veh.UpVector * -3;
                        Function.Call<Vehicle>(GTA.Native.Hash.DETACH_VEHICLE_FROM_ANY_CARGOBOB, vehicle);
                    }
                }
                else
                {
                    var nearbyvehs = World.GetNearbyVehicles(veh.Position + new Vector3(0, 0, -5), 6);
                    foreach (Vehicle vehicle in nearbyvehs)
                    {
                        if (CanWeUse(vehicle) && vehicle != veh)
                        {
                            Function.Call(GTA.Native.Hash.ATTACH_VEHICLE_TO_CARGOBOB, veh, vehicle, 1, vehicle.Position.X, vehicle.Position.Y, vehicle.Position.Z);
                            break;
                        }
                    }
                }
            }
            if (veh.Model == new Model(VehicleHash.Phantom) || veh.Model == new Model(VehicleHash.Packer) || veh.Model == new Model(VehicleHash.Hauler))
            {
                var nearbyvehs = World.GetNearbyVehicles(veh.Position + veh.ForwardVector * -5, 10);
                foreach (Vehicle vehicle in nearbyvehs)
                {
                    if (CanWeUse(vehicle) && vehicle != veh)
                    {
                        if (Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_ATTACHED_TO_TRAILER, veh) == false)
                        {
                            Function.Call(GTA.Native.Hash.ATTACH_VEHICLE_TO_TRAILER, veh, vehicle, 10);
                            break;
                        }
                        else
                        {
                            Function.Call(GTA.Native.Hash.DETACH_VEHICLE_FROM_TRAILER, veh);
                            break;
                        }
                    }
                }
            }
            if (veh.Model == new Model(VehicleHash.TowTruck) || veh.Model == new Model(VehicleHash.TowTruck2))
            {
                var nearbyvehs = World.GetNearbyVehicles(veh.Position + veh.ForwardVector * -3, 10);
                foreach (Vehicle vehicle in nearbyvehs)
                {
                    if (CanWeUse(vehicle) && vehicle != veh)
                    {
                        //Squad1Leader.CurrentVehicle.SoundHorn(500);
                        if (Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_ATTACHED_TO_TOW_TRUCK, veh, vehicle) == false)
                        {
                            Function.Call(GTA.Native.Hash.ATTACH_VEHICLE_TO_TOW_TRUCK, veh, vehicle, 10, veh.Position.X, veh.Position.Y, veh.Position.Z);
                            Function.Call(GTA.Native.Hash._SET_TOW_TRUCK_CRANE_RAISED, 0.0);
                            break;
                        }
                        else
                        {
                            Function.Call(GTA.Native.Hash.DETACH_VEHICLE_FROM_TOW_TRUCK, veh, vehicle);
                            break;
                        }
                    }
                }
            }
        }
    }





    public void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {

        if (selectedItem == AttachVehiclesSquad1)
        {
            AttachVehicle(Squad1Leader);
        }
        if (selectedItem == AttachVehiclesSquad2)
        {
            AttachVehicle(Squad2Leader);
        }
        if (selectedItem == AttachVehiclesSquad3)
        {
            AttachVehicle(Squad3Leader);
        }
        if (selectedItem == AttachVehiclesSquad4)
        {
            AttachVehicle(Squad4Leader);
        }
        if (selectedItem == SwitchSirensSquad1)
        {
            SwitchSirens(Squad1Leader);
        }
        if (selectedItem == SwitchSirensSquad2)
        {
            SwitchSirens(Squad2Leader);
        }
        if (selectedItem == SwitchSirensSquad3)
        {
            SwitchSirens(Squad3Leader);
        }
        if (selectedItem == SwitchSirensSquad4)
        {
            SwitchSirens(Squad4Leader);
        }
        if (selectedItem == RappelSquad1)
        {
            RappelFunction(Squad1Leader);
        }
        if (selectedItem == RappelSquad2)
        {
            RappelFunction(Squad2Leader);
        }
        if (selectedItem == RappelSquad3)
        {
            RappelFunction(Squad3Leader);
        }
        if (selectedItem == RappelSquad4)
        {
            RappelFunction(Squad4Leader);
        }
        if (selectedItem == ParkNearbySquad1)
        {
            ParkNearby(Squad1Leader);
        }
        if (selectedItem == ParkNearbySquad2)
        {
            ParkNearby(Squad2Leader);
        }
        if (selectedItem == ParkNearbySquad3)
        {
            ParkNearby(Squad3Leader);
        }
        if (selectedItem == ParkNearbySquad4)
        {
            ParkNearby(Squad4Leader);
        }
        if (selectedItem == FollowmeOffroadSquad1)
        {
            FollowmeOffroad(Squad1Leader);
        }
        if (selectedItem == GuardThisAreaSquad1)
        {
            GuardArea(Squad1Leader);
        }
        if (selectedItem == GuardThisAreaSquad2)
        {
            GuardArea(Squad2Leader);
        }
        if (selectedItem == GuardThisAreaSquad3)
        {
            GuardArea(Squad3Leader);
        }
        if (selectedItem == GuardThisAreaSquad4)
        {
            GuardArea(Squad4Leader);
        }
        if (selectedItem == FleeSquad1)
        {
            VehicleFlee(Squad1Leader);
        }
        if (selectedItem == FleeSquad2)
        {
            VehicleFlee(Squad2Leader);
        }
        if (selectedItem == FleeSquad3)
        {
            VehicleFlee(Squad4Leader);
        }
        if (selectedItem == FleeSquad4)
        {
            VehicleFlee(Squad4Leader);
        }
        if (selectedItem == FollowmeOffroadSquad2)
        {
            FollowmeOffroad(Squad2Leader);
        }
        if (selectedItem == FollowmeOffroadSquad3)
        {
            FollowmeOffroad(Squad3Leader);
        }
        if (selectedItem == FollowmeOffroadSquad4)
        {
            FollowmeOffroad(Squad4Leader);
        }
        if (selectedItem == Squad1RecruitNear)
        {
            RecruitNear(Squad1Leader);
        }
        if (selectedItem == Squad2RecruitNear)
        {
            RecruitNear(Squad2Leader);
        }
        if (selectedItem == Squad3RecruitNear)
        {
            RecruitNear(Squad3Leader);
        }

        if (selectedItem == GetBackToMeSquad1)
        {
            UI.ShowSubtitle("Get back to me.");
            GoThereSmart(Squad1Leader, Game.Player.Character.Position);
        }

        if (selectedItem == GetBackToMeSquad2)
        {
            UI.ShowSubtitle("Get back to me.");
            GoThereSmart(Squad2Leader, Game.Player.Character.Position);
        }
        if (selectedItem == GetBackToMeSquad3)
        {
            UI.ShowSubtitle("Get back to me.");
            GoThereSmart(Squad3Leader, Game.Player.Character.Position);
        }
        if (selectedItem == GetBackToMeSquad4)
        {
            UI.ShowSubtitle("Get back to me.");
            GoThereSmart(Squad4Leader, Game.Player.Character.Position);
        }

        if (selectedItem == DeleteSquad1)
        {
            RemoveSquad1();
        }

        if (selectedItem == DeleteSquad2)
        {
            RemoveSquad2();
        }
        if (selectedItem == DeleteSquad3)
        {
            RemoveSquad3();
        }
        if (selectedItem == DeleteSquad4)
        {
            RemoveSquad4();
        }
        if (selectedItem == EscortMeSquad1)
        {
            NonSituationalEscortMe(Squad1Leader);
        }

        if (selectedItem == EscortMeSquad2)
        {
            NonSituationalEscortMe(Squad2Leader);
        }
        if (selectedItem == EscortMeSquad3)
        {
            NonSituationalEscortMe(Squad3Leader);
        }
        if (selectedItem == EscortMeSquad4)
        {
            NonSituationalEscortMe(Squad4Leader);
        }
        if (selectedItem == GoToWaypointSquad1)
        {
            NonSituationalGoToWaypoint(Squad1Leader);
        }
        if (selectedItem == GoToWaypointSquad2)
        {
            NonSituationalGoToWaypoint(Squad2Leader);
        }
        if (selectedItem == GoToWaypointSquad3)
        {
            NonSituationalGoToWaypoint(Squad3Leader);
        }
        if (selectedItem == GoToWaypointSquad4)
        {
            NonSituationalGoToWaypoint(Squad4Leader);
        }
        if (selectedItem == EnterLeaveVehicleSquad1)
        {
            NonSituationalEnterLeaveVehicle(Squad1Leader);
        }
        if (selectedItem == EnterLeaveVehicleSquad2)
        {
            NonSituationalEnterLeaveVehicle(Squad2Leader);
        }
        if (selectedItem == EnterLeaveVehicleSquad3)
        {
            NonSituationalEnterLeaveVehicle(Squad3Leader);
        }

        if (selectedItem == EnterLeaveVehicleSquad4)
        {
            NonSituationalEnterLeaveVehicle(Squad4Leader);
        }
        if (selectedItem == CallSquad1 && Squad1.Count == 0)
        {
            CreateSquad(1);//CreateSquad1();
        }
        if (selectedItem == CallSquad2 && Squad2.Count == 0)
        {

            CreateSquad(2); //CreateSquad2();
        }
        if (selectedItem == CallSquad3 && Squad3.Count == 0)
        {

            CreateSquad(3); //CreateSquad3();
        }
        if (selectedItem == CallSquad4 && Squad4.Count == 0)
        {

            CreateSquad(4);// CreateSquad4();
        }
        if (selectedItem == Squad1Hate)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Hate)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now hates " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped hating " + pregunta + " faction.");
                }
            }
        }
        if (selectedItem == Squad1Like)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Companion)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Companion, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now likes " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped liking " + pregunta + " faction.");
                }
            }
        }


        if (selectedItem == Squad2Hate)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null || pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Hate)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 2 now hates " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 2 stopped hating " + pregunta + " faction.");
                }
            }
        }
        if (selectedItem == Squad2Like)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null || pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Companion)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Companion, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 2 now likes " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 2 stopped liking " + pregunta + " faction.");
                }
            }
        }
        if (selectedItem == Squad3Hate)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Hate)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Hate, Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now hates " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped hating " + pregunta + " faction.");
                }
            }
        }
        if (selectedItem == Squad3Like)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Companion)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Companion, Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now likes " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad3RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped liking " + pregunta + " faction.");
                }
            }
        }



        if (selectedItem == Squad4Hate)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Hate)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Hate, Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now hates " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped hating " + pregunta + " faction.");
                }
            }
        }
        if (selectedItem == Squad4Like)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "")
            {
                if (World.GetRelationshipBetweenGroups(Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta)) != Relationship.Companion)
                {
                    World.SetRelationshipBetweenGroups(Relationship.Companion, Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 now likes " + pregunta + " faction.");
                }
                else
                {
                    World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad4RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, pregunta));
                    UI.Notify("Squad 1 stopped liking " + pregunta + " faction.");
                }
            }
        }

        if (selectedItem == AllGoToWaypoint)
        {
            if (CanWeUse(Squad1Leader))
            {
                NonSituationalGoToWaypoint(Squad1Leader);
            }
            if (CanWeUse(Squad2Leader))
            {
                NonSituationalGoToWaypoint(Squad2Leader);
            }
            if (CanWeUse(Squad3Leader))
            {
                NonSituationalGoToWaypoint(Squad3Leader);
            }
            if (CanWeUse(Squad4Leader))
            {
                NonSituationalGoToWaypoint(Squad4Leader);
            }
        }
        if (selectedItem == AllGetBackToMe)
        {
            UI.ShowSubtitle("All, get back to me.");

            if (CanWeUse(Squad1Leader))
            {
                GoThereSmart(Squad1Leader,Game.Player.Character.Position);
            }
            if (CanWeUse(Squad2Leader))
            {
                GoThereSmart(Squad2Leader, Game.Player.Character.Position);
            }
            if (CanWeUse(Squad3Leader))
            {
                GoThereSmart(Squad3Leader, Game.Player.Character.Position);
            }
            if (CanWeUse(Squad4Leader))
            {
                GoThereSmart(Squad4Leader, Game.Player.Character.Position);
            }
        }
        if (selectedItem == AllCallSquad)
        {
            if (Squad1.Count == 0)
            {

                CreateSquad(1);
            }

            if (Squad2.Count == 0)
            {

                CreateSquad(2);
            }
            if (Squad3.Count == 0)
            {

                CreateSquad(3);
            }
            if (Squad4.Count == 0)
            {

                CreateSquad(4);
            }
        }
        if (selectedItem == AllDismissSquad)
        {
            RemoveSquad1();
            RemoveSquad2();
            RemoveSquad3();
            RemoveSquad4();

        }
        if (selectedItem == AllEnterLeave)
        {
            NonSituationalEnterLeaveVehicle(Squad1Leader);
            NonSituationalEnterLeaveVehicle(Squad2Leader);
            NonSituationalEnterLeaveVehicle(Squad3Leader);
            NonSituationalEnterLeaveVehicle(Squad4Leader);

        }

        if (selectedItem == AllParkAtMySide)
        {
            if (CanWeUse(Squad1Leader))
            {
                Vector3 ParkingPos = Game.Player.Character.Position+ Game.Player.Character.RightVector*-3;
                Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, Squad1Leader, GetLastVehicle(Squad1Leader), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Game.Player.Character.Heading, 1, 30f, false);
            }
            if (CanWeUse(Squad2Leader))
            {
                Vector3 ParkingPos = Game.Player.Character.Position + Game.Player.Character.RightVector * 3;
                Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, Squad2Leader, GetLastVehicle(Squad2Leader), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Game.Player.Character.Heading, 1, 30f, false);
            }
            if (CanWeUse(Squad3Leader))
            {
                Vector3 ParkingPos = Game.Player.Character.Position + Game.Player.Character.RightVector * -6;
                Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, Squad3Leader, GetLastVehicle(Squad3Leader), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Game.Player.Character.Heading, 1, 30f, false);
            }
            if (CanWeUse(Squad4Leader))
            {
                Vector3 ParkingPos = Game.Player.Character.Position + Game.Player.Character.RightVector * 6;
                Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, Squad4Leader, GetLastVehicle(Squad4Leader), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Game.Player.Character.Heading, 1, 30f, false);
            }
        }


        if (selectedItem == AllSquadsAddWaypoint)
        {
            if (RaceWaypoints.Count <8)
            {
                if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE))
                {
                    if (GetWaypointVector() != Vector3.Zero)
                    {
                        if (RaceWaypoints.Contains(GetWaypointVector()))
                        {
                            UI.Notify("This waypoint is already set.");
                        }
                        else
                        {
                            RaceWaypoints.Add(GetWaypointVector());
                            Blip blop = World.CreateBlip(GetWaypointVector());
                            blop.Color = BlipColor.White;
                            RaceWaypointBlips.Add(blop);
                            blop.ShowNumber(RaceWaypointBlips.Count);
                            UI.Notify("Waypoint added (" + World.GetStreetName(GetWaypointVector()) + ")");                            
                        }
                    }
                    else
                    {
                        UI.Notify("Waypoint coords not valid.");
                    }
                }
                else
                {
                    RaceWaypoints.Add(Game.Player.Character.Position);
                    UI.Notify(Game.Player.Character.Position.ToString());
                    Blip blop = World.CreateBlip(Game.Player.Character.Position);
                    blop.Color = BlipColor.White;
                    RaceWaypointBlips.Add(blop);
                    blop.ShowNumber(RaceWaypointBlips.Count);
                }
            }
            else
            {
                UI.Notify("You can't place more waypoints, sorry.\nEngine limits.");
            }
        }

        if (selectedItem == AllSquadsClearWaypoints)
        {
            UI.Notify("Waypoints cleared.");
            RaceWaypoints.Clear();
            foreach (Blip blop in RaceWaypointBlips)
            {
                blop.Remove();
            }
            RaceWaypointBlips.Clear();
        }

        if (selectedItem == AllSquadsStartRace)
        {
            if (CanWeUse(Squad1Leader))
            {
                TaskSequence RaceSequence = new TaskSequence();
                foreach ( Vector3 pos in RaceWaypoints)
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, GetLastVehicle(Squad1Leader), pos.X, pos.Y, pos.Z, 70f, 4 | 16 | 32| 262144, 10f);
                    Function.Call(Hash.TASK_PAUSE, 0, 5);
                }
                RaceSequence.Close();
                Squad1Leader.Task.PerformSequence(RaceSequence);
            }
            if (CanWeUse(Squad2Leader))
            {
                TaskSequence RaceSequence = new TaskSequence();
                foreach (Vector3 pos in RaceWaypoints)
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, GetLastVehicle(Squad2Leader), pos.X, pos.Y, pos.Z, 70f, 4 | 16 | 32| 262144, 10f);
                    Function.Call(Hash.TASK_PAUSE, 0, 5);
                }
                RaceSequence.Close();
                Squad2Leader.Task.PerformSequence(RaceSequence);
            }


            if (CanWeUse(Squad3Leader))
            {
                TaskSequence RaceSequence = new TaskSequence();
                foreach (Vector3 pos in RaceWaypoints)
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, GetLastVehicle(Squad3Leader), pos.X, pos.Y, pos.Z, 70f, 4 | 16 | 32 | 262144, 10f);
                    Function.Call(Hash.TASK_PAUSE, 0, 5);
                }
                RaceSequence.Close();
                Squad3Leader.Task.PerformSequence(RaceSequence);
            }

            if (CanWeUse(Squad4Leader))
            {
                TaskSequence RaceSequence = new TaskSequence();
                foreach (Vector3 pos in RaceWaypoints)
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, GetLastVehicle(Squad4Leader), pos.X, pos.Y, pos.Z, 70f, 4 | 16 | 32 | 262144, 10f);
                    Function.Call(Hash.TASK_PAUSE, 0, 5);
                }
                RaceSequence.Close();
                Squad4Leader.Task.PerformSequence(RaceSequence);
            }
        }
    }



    void FollowmeOffroad(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder))
        {
            if (DrivingCareful.Contains(RecieveOrder))
            {
                GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 10.0, 1 | 4 | 16 | 32 | 4194304, 5f, 10f, 20f);
            }
            if (DrivingNormal.Contains(RecieveOrder))
            {
                GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 20.0, 1 | 4 | 16 | 32 | 4194304, 5f, 10f, 20f);
            }
            if (DrivingFurious.Contains(RecieveOrder))
            {
                GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 40.0, 1 | 4 | 16 | 32 | 4194304, 5f, 10f, 20f);
            }
            UI.ShowSubtitle("We goin' offroad m8", 1000);
        }
    }

    Vehicle GetLastVehicle(Ped RecieveOrder)
    {
        Vehicle vehicle = null;
        if (GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, true) != null)
        {
            vehicle = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, true);
            if (vehicle.IsAlive)
            {
                return vehicle;
            }
        }
        else
        {
            if (GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, false) != null)
            {
                vehicle = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, false);
                if (vehicle.IsAlive)
                {
                    return vehicle;
                }
            }
        }
        return vehicle;
    }

    bool IsAnyVehicleThere(Vector3 pos, Entity ent)
    {
        foreach (Vehicle veh in World.GetNearbyVehicles(pos, 5f))
        {
            if (veh != ent)
            {
                return true;
            }
        }
        return false;
    }
    void RecruitNear(Ped RecieveOrder)
    {
        foreach(Ped p in World.GetNearbyPeds(RecieveOrder, 10f))
        {
            if (RecieveOrder.CurrentPedGroup.MemberCount <= 8)
            {
                UI.Notify("~y~8 Team Members limit reached. ~w~This is a game hardcoded limit, sorry.");
            }
            if(p.CurrentPedGroup != RecieveOrder.CurrentPedGroup && !p.IsPlayer && p.IsHuman)
            {
                p.IsPersistent = true;
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, p, RecieveOrder.CurrentPedGroup);
                if (RecieveOrder == Squad1Leader) Squad1.Add(p);
                if (RecieveOrder == Squad2Leader) Squad2.Add(p);
                if (RecieveOrder == Squad3Leader) Squad3.Add(p);
                if (RecieveOrder == Squad4Leader) Squad4.Add(p);
            }
        }
    }
    void ParkNearby(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && CanWeUse(GetLastVehicle(RecieveOrder)))
        {
            bool ParkingatSides = false;
            bool CanPark = false;
            Vector3 ParkingPos = RecieveOrder.Position;
            Vector3 PreParkingPos = RecieveOrder.Position;

            float Heading = RecieveOrder.Heading;

            var nearbyvehs = World.GetNearbyVehicles(RecieveOrder.Position, 60);
            foreach (Vehicle nearbyvehicle in nearbyvehs)
            {
                if (nearbyvehicle != GetLastVehicle(RecieveOrder) && !CanWeUse(nearbyvehicle.GetPedOnSeat(VehicleSeat.Driver)))
                {
                    if (!IsAnyVehicleThere(nearbyvehicle.Position + (nearbyvehicle.ForwardVector * -6), nearbyvehicle))
                    {
                        if (Function.Call<bool>(GTA.Native.Hash.IS_BIG_VEHICLE, nearbyvehicle))
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.ForwardVector * -10);
                        }
                        else
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.ForwardVector * -6);
                        }
                        PreParkingPos = nearbyvehicle.Position + (nearbyvehicle.ForwardVector * -20);
                        UI.Notify("No obstacles behind vehicle");
                        Heading = nearbyvehicle.Heading;
                        CanPark = true;
                        break;
                    }
                    else if (!IsAnyVehicleThere(nearbyvehicle.Position + (nearbyvehicle.RightVector * -6), nearbyvehicle))
                    {
                        if (Function.Call<bool>(GTA.Native.Hash.IS_BIG_VEHICLE, nearbyvehicle))
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * -6);
                        }
                        else
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * -3);
                        }
                        PreParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * -3) + (nearbyvehicle.ForwardVector * -20);

                        UI.Notify("No obstacles at the left side");
                        Heading = nearbyvehicle.Heading;
                        CanPark = true;
                        ParkingatSides = true;
                        break;
                    }
                    else if (!IsAnyVehicleThere(nearbyvehicle.Position + (nearbyvehicle.RightVector * 6), nearbyvehicle))
                    {
                        if (Function.Call<bool>(GTA.Native.Hash.IS_BIG_VEHICLE, nearbyvehicle))
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * 6);
                        }
                        else
                        {
                            ParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * 3);
                        }
                        PreParkingPos = nearbyvehicle.Position + (nearbyvehicle.RightVector * 3) +(nearbyvehicle.ForwardVector * -20);

                        UI.Notify("No obstacles at the right side");
                        Heading = nearbyvehicle.Heading;
                        CanPark = true;
                        ParkingatSides = true;
                        break;
                    }
                }
            }
            if (CanPark)
            {
                if (ParkingatSides)
                {
                    Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, RecieveOrder, GetLastVehicle(RecieveOrder), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Heading, 1, 30f, false);
                }
                else
                {
                    TaskSequence DriverSequence = new TaskSequence();
                    Function.Call(Hash.TASK_VEHICLE_GOTO_NAVMESH, 0, GetLastVehicle(RecieveOrder), PreParkingPos.X, PreParkingPos.Y, PreParkingPos.Z, 6f, 4194304 | 1 | 2 | 4 | 16 | 32, 3f);
                    Function.Call(Hash.TASK_PAUSE, 0, 1);
                    Function.Call(GTA.Native.Hash.TASK_VEHICLE_PARK, 0, GetLastVehicle(RecieveOrder), ParkingPos.X, ParkingPos.Y, ParkingPos.Z, Heading, 1, 30f, false);
                    DriverSequence.Close();
                    RecieveOrder.Task.PerformSequence(DriverSequence);
                }
            }
            else
            {
                UI.Notify("Didn't find suitable parking");
            }
        }        
    }


    void PreparePed(Ped ped, WeaponHash weapon, WeaponHash weapon2)
    {
        Function.Call(GTA.Native.Hash.SET_PED_TO_INFORM_RESPECTED_FRIENDS, ped, 20f,20);

        Function.Call(GTA.Native.Hash.SET_PED_COMBAT_MOVEMENT, ped, 2);

        Function.Call(GTA.Native.Hash.SET_DRIVER_ABILITY, ped,1f);
        //Function.Call(GTA.Native.Hash.SET_PED_HEARING_RANGE, ped, 50f);

        /*
        for (int i = 0; i < 200; i++)
        {
            //UI.ShowSubtitle((Function.Call<float>(GTA.Native.Hash.GET_COMBAT_FLOAT, ped.Handle, i).ToString()));
            Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 3,false);

        }*/
        //Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 2, false);

        ped.AlwaysDiesOnLowHealth = false;
        ped.Accuracy = 90;
        ped.Armor = 100;
        ped.AlwaysKeepTask = true;
        ped.CanSwitchWeapons = true;
        ped.Weapons.Give(weapon, 900, false, true);
        ped.Weapons.Give(weapon2, 900, false, true);
    }

    public Vector3 RotToDir(Vector3 Rot)
    {
        float z = Rot.Z;
        float retz = z * 0.0174532924F;//degree to radian
        float x = Rot.X;
        float retx = x * 0.0174532924F;
        float absx = (float)System.Math.Abs(System.Math.Cos(retx));
        return new Vector3((float)-System.Math.Sin(retz) * absx, (float)System.Math.Cos(retz) * absx, (float)System.Math.Sin(retx));
    }

    public RaycastResult Cam_Raycast_Forward()
    {
        Vector3 camrot = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_ROT, 0);
        Vector3 campos = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);

        Vector3 multiplied = new Vector3(RotToDir(camrot).X * 1000.0f, RotToDir(camrot).Y * 1000.0f, RotToDir(camrot).Z * 1000.0f);
        RaycastResult ray = World.Raycast(campos+Game.Player.Character.ForwardVector, campos + multiplied * 5000, IntersectOptions.Everything);
        return ray;
    }

    public RaycastResult RaycastDrive(Vector3 pos1, Vector3 pos2)
    {
        RaycastResult ray = World.Raycast(pos1, pos2 + new Vector3(0, 0, 1), IntersectOptions.Map);
        return ray;
    }

    void DriveTo(Ped RecieveOrder, Vector3 coord, float stopdistance)
    {
        if (RecieveOrder.IsSittingInVehicle())
        {
            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model) || RecieveOrder.CurrentVehicle.Model.IsPlane)
            {
                if (RecieveOrder.CurrentVehicle.Model == new Model(VehicleHash.Cargobob))
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, coord.X, coord.Y, coord.Z + 30, 30.0, 0, RecieveOrder.CurrentVehicle.Model, 6, 60, 30.0);
                }
                else
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, coord.X, coord.Y, coord.Z + 30, 60.0, 0, RecieveOrder.CurrentVehicle.Model, 6, 200, 20.0);
                }
            }

            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, RecieveOrder.CurrentVehicle.Model))
            {
                if (DrivingCareful.Contains(RecieveOrder))
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, coord.X, coord.Y, coord.Z, 20f, 1 | 2 | 32, stopdistance);
                }
                if (DrivingNormal.Contains(RecieveOrder))
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, coord.X, coord.Y, coord.Z, 30f, 1 | 2 | 4 | 16 | 32 , stopdistance);
                }
                if (DrivingFurious.Contains(RecieveOrder))
                {
                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, coord.X, coord.Y, coord.Z, 70f, 4 | 16 | 32 | 512 | 262144, stopdistance); //| 16384 | 8192 | 4096 | 2048 // | 65536 | 32768 < superpathing //  2097152| 1048576 | 524288 likes dirtroads   // | 131072
                }
            }
        }
        else
        {
            UI.Notify("~r~ Bodyguards Squads Error:\nTried to make a ped drive, but the ped wasn't in a vehicle.");
        }
    }
    void PlayCombatAnim()
    {

        WeaponGroup weapon = Game.Player.Character.Weapons.Current.Group;

        if(new List<WeaponGroup> { WeaponGroup.AssaultRifle, WeaponGroup.SMG, WeaponGroup.Sniper, WeaponGroup.Heavy }.Contains(weapon))
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, "combat@gestures@rifle@overthere");

            int d = GetRandomInt(0, 4);
            switch (d)
            {
                case 0: Game.Player.Character.Task.PlayAnimation("combat@gestures@rifle@overthere", "0", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 1: Game.Player.Character.Task.PlayAnimation("combat@gestures@rifle@overthere", "90", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 2: Game.Player.Character.Task.PlayAnimation("combat@gestures@rifle@overthere", "-90", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 3: Game.Player.Character.Task.PlayAnimation("combat@gestures@rifle@overthere", "180", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 4: Game.Player.Character.Task.PlayAnimation("combat@gestures@rifle@overthere", "-180", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
            }
        };


        if (new List<WeaponGroup> { WeaponGroup.Pistol }.Contains(weapon))
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, "combat@gestures@pistol@overthere");

            int d = GetRandomInt(0, 4);
            switch (d)
            {
                case 0: Game.Player.Character.Task.PlayAnimation("combat@gestures@pistol@overthere", "0", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 1: Game.Player.Character.Task.PlayAnimation("combat@gestures@pistol@overthere", "90", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 2: Game.Player.Character.Task.PlayAnimation("combat@gestures@pistol@overthere", "-90", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 3: Game.Player.Character.Task.PlayAnimation("combat@gestures@pistol@overthere", "180", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
                case 4: Game.Player.Character.Task.PlayAnimation("combat@gestures@pistol@overthere", "-180", 1f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32); break;
            }
        };


    }
    void GoThereSmart(Ped RecieveOrder, Vector3 coord)
    {

        PlayCombatAnim();
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive && coord != null)
        {
            if (RecieveOrder.IsSittingInVehicle())
            {
                DriveTo(RecieveOrder, coord,20);
            }
            if (RecieveOrder.IsOnFoot)
            {
                Vehicle lastveh = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, true);
                if (RecieveOrder.Position.DistanceTo(coord) > 200 && CanWeUse(lastveh) && lastveh.IsAlive)
                {
                    if (Squad1.Contains(RecieveOrder))
                    {
                        GetSquadIntoVehicle(Squad1, lastveh);
                    }
                    if (Squad2.Contains(RecieveOrder))
                    {
                        GetSquadIntoVehicle(Squad2, lastveh);
                    }
                    Script.Wait(0);
                    DriveTo(RecieveOrder, coord,20);
                }
                else
                {
                    RecieveOrder.Task.RunTo(coord, false);
                }
            }
        }
    }

    private Vector3 ToGround(Vector3 position)
    {
        position.Z = World.GetGroundHeight(new Vector2(position.X, position.Y));
        return new Vector3(position.X, position.Y, position.Z);
    }


    Vector3 GetWaypointCoords()
    {
           Vector3 pos = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, 8));

        if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE) && pos != null || pos != new Vector3(0, 0, 0))
        {
            Vector3 WayPos = ToGround(pos);
            if (WayPos.Z == 0 || WayPos.Z == 1)
            {
                WayPos = World.GetNextPositionOnStreet(WayPos);
                return WayPos;
            }
        }
        return Vector3.Zero;
    }

    Vector3 GetWaypointVector()
    {
        Vector3 WayPos = Vector3.Zero;

        Vector3 nonsituationalwaypointcoords = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, 8));
        if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE) && nonsituationalwaypointcoords != null || nonsituationalwaypointcoords != new Vector3(0, 0, 0))
        {
            if (nonsituationalwaypointcoords.DistanceTo(Game.Player.Character.Position) < 200f)
            {
                WayPos = World.GetNextPositionOnStreet(nonsituationalwaypointcoords);
            }
            else
            {
                WayPos = World.GetSafeCoordForPed(nonsituationalwaypointcoords);
            }
            if (WayPos.Z == 0 || WayPos.Z == 1)
            {
                WayPos = World.GetNextPositionOnStreet(nonsituationalwaypointcoords);
            }
        }

        return WayPos;
    }

    void NonSituationalGoToWaypoint(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            Vector3 nonsituationalwaypointcoords = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, 8));
            if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE) && nonsituationalwaypointcoords != null || nonsituationalwaypointcoords != new Vector3(0,0,0))
            {
                Vector3 WayPos=Vector3.Zero;
                if (nonsituationalwaypointcoords.DistanceTo(Game.Player.Character.Position) < 200f)
                {
                    WayPos = World.GetNextPositionOnStreet(nonsituationalwaypointcoords);
                }
                else
                {
                     WayPos = World.GetSafeCoordForPed(nonsituationalwaypointcoords);
                }
                UI.ShowSubtitle("Go to the marked position.");
                if (WayPos.Z == 0 || WayPos.Z == 1)
                {
                    WayPos = World.GetNextPositionOnStreet(nonsituationalwaypointcoords);
                }

                if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
                {
                    if (WayPos != new Vector3(0, 0, 0))
                    {
                        GoThereSmart(RecieveOrder, WayPos);
                    }
                    else
                    {
                        UI.ShowSubtitle("~r~Invalid Waypoint coords (" + WayPos.ToString() + ") \n~w~Place a new one.");
                    }
                }
            }
            else
            {
                UI.ShowSubtitle("There is no waypoint set.", 2000);
            }
        }
        else
        {
            UI.ShowSubtitle("There is no Squad Leader to give orders.", 2000);
        }
    }


    void DispatchToWaypoint(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            Vector3 nonsituationalwaypointcoords = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, 8));
            if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE) && nonsituationalwaypointcoords != null || nonsituationalwaypointcoords != new Vector3(0, 0, 0))
            {
                Vector3 WayPos = ToGround(nonsituationalwaypointcoords);
                if (WayPos.Z == 0 || WayPos.Z == 1)
                {
                    WayPos = World.GetNextPositionOnStreet(WayPos);
                    //UI.ShowSubtitle("Waypoint Z coord not valid, nearest valid place will be used (" + World.GetStreetName(WayPos).ToString() + ").");
                }

                if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
                {
                    if (WayPos != Vector3.Zero)
                    {
                        GoThereSmart(RecieveOrder, WayPos);
                    }
                    else
                    {
                        GoThereSmart(RecieveOrder, Game.Player.Character.Position);
                        UI.ShowSubtitle("~r~Invalid Waypoint coords (" + WayPos.ToString() + "), the Squad will go to you.");
                    }
                }
            }
            else
            {
                GoThereSmart(RecieveOrder, Game.Player.Character.Position);
                UI.ShowSubtitle("Waypoint not set, Squad will go to you.", 2000);
            }
        }
        else
        {
            UI.ShowSubtitle("There is no Squad Leader to give orders.", 2000);
        }
    }


    void NonSituationalEscortMe(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            if (RecieveOrder.IsSittingInVehicle())
            {
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model))
                {
                    UI.ShowSubtitle("Protect me (Heli)", 1000);
                    Function.Call(Hash.TASK_VEHICLE_HELI_PROTECT, RecieveOrder, RecieveOrder.CurrentVehicle,Game.Player.Character, 50f, 32, 50f, 35, 1); //32 o 0 no cambia
                }
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, RecieveOrder.CurrentVehicle.Model))
                {
                    if (Game.Player.Character.IsSittingInVehicle())
                    {
                        if (DrivingCareful.Contains(RecieveOrder))
                        {
                            UI.ShowSubtitle("Escort my " + Game.Player.Character.CurrentVehicle.FriendlyName + ". Careful.", 1000);
                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 40.0, 1 | 2 | 4 | 32, 20f, 1f, 5f);
                        }
                        if (DrivingNormal.Contains(RecieveOrder))
                        {
                            GTA.Native.Function.Call(GTA.Native.Hash.SET_DRIVE_TASK_DRIVING_STYLE, RecieveOrder, 6);

                            UI.ShowSubtitle("Escort my " + Game.Player.Character.CurrentVehicle.FriendlyName + ".", 1000);
                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 60f, 1 | 2 | 4 | 16 | 32, 0f, 0f, 10f);
                        }
                        if (DrivingFurious.Contains(RecieveOrder))
                        {
                            UI.ShowSubtitle("Escort my " + Game.Player.Character.CurrentVehicle.FriendlyName + ". Be with me no matter what.", 1000);
                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, Game.Player.Character.CurrentVehicle, -1, 200f, 1 | 4 | 16 | 32, 0f, 0f, 20f);
                        }
                    }
                }
            }
            else
            {
                UI.ShowSubtitle("The Squad Leader needs to be in a vehicle to escort you.");
            }
        }
        else
        {
            UI.ShowSubtitle("There is no Squad Leader to give orders.", 2000);
        }
    }

    void NonSituationalEnterLeaveVehicle(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            if (RecieveOrder.IsSittingInVehicle())
            {
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model))
                {
                    if (RecieveOrder.CurrentVehicle.IsStopped)
                    {
                        RecieveOrder.Task.LeaveVehicle();
                        UI.ShowSubtitle("Leave the " + RecieveOrder.CurrentVehicle.FriendlyName + ".", 1000);
                    }
                    else
                    {
                        UI.ShowSubtitle("Land the " + RecieveOrder.CurrentVehicle.FriendlyName + ".", 1000);
                        Vector3 safepos = World.GetSafeCoordForPed(RecieveOrder.Position);
                        if (safepos.DistanceTo(RecieveOrder.Position)<50f)
                        {
                            Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, safepos.X, safepos.Y, safepos.Z, 20, 40f, 1f, 36f, 15, 15, -1f, 1);
                        }
                        else
                        {
                            Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, RecieveOrder.Position.X, RecieveOrder.Position.Y, RecieveOrder.Position.Z, 20, 40f, 1f, 36f, 15, 15, -1f, 1);
                        }
                    }
                }
                else
                {
                    Function.Call<bool>(GTA.Native.Hash.TASK_LEAVE_VEHICLE, RecieveOrder, RecieveOrder.CurrentVehicle, 0);
                    UI.ShowSubtitle("Leave the " + RecieveOrder.CurrentVehicle.FriendlyName + ".", 1000);

                }
            }
            else
            {
                Vehicle lastveh = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, true);
                if (CanWeUse(lastveh))
                {
                    if (lastveh.IsAlive)
                    {
                        GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, RecieveOrder, lastveh, 10000, -1, 2.0, 1, 0);
                        UI.ShowSubtitle("Get back to the " + lastveh.FriendlyName + ".", 1000);
                    }
                    else
                    {
                        UI.ShowSubtitle("This squad can't get in their " + lastveh.FriendlyName + ", it is destroyed.", 1000);
                    }
                }
            }
        }
        else
        {
            UI.ShowSubtitle("There is no Squad Leader to give orders.", 2000);
        }
    }

    void NonSituationalLeaveVehicle(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            if (!RecieveOrder.IsOnFoot && CanWeUse(RecieveOrder.CurrentVehicle))
            {
                RecieveOrder.Task.LeaveVehicle(RecieveOrder.CurrentVehicle, true);
            }
        }
    }

    void RappelFunction(Ped RecieveOrder)
    {
        if (CanWeUse(RecieveOrder))
        {
            if (CanWeUse(RecieveOrder.CurrentVehicle))
            {
                if (RecieveOrder.CurrentVehicle.Model == new Model(VehicleHash.Polmav) || RecieveOrder.CurrentVehicle.Model == new Model(VehicleHash.Maverick))
                {
                    UI.ShowSubtitle("Make your passengers rappel from the " + RecieveOrder.CurrentVehicle.FriendlyName + ".");
                    if (CanWeUse(RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.LeftRear)))
                    {
                        Function.Call(Hash.TASK_RAPPEL_FROM_HELI, RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.LeftRear), 1f);
                    }
                    if (CanWeUse(RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightRear)))
                    {
                        Function.Call(Hash.TASK_RAPPEL_FROM_HELI, RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightRear), 1f);
                    }
                    if (!CanWeUse(RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightRear)) && !CanWeUse(RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.LeftRear)) && CanWeUse(RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightFront)))
                    {
                        RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightFront).SetIntoVehicle(RecieveOrder.CurrentVehicle, VehicleSeat.RightRear);
                        Function.Call(Hash.TASK_RAPPEL_FROM_HELI, RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.RightRear), 1f);
                    }

                }
                else
                {
                    UI.ShowSubtitle("Squad Leader's vehicle does not allow rappeling.");
                }
            }
            else
            {
                UI.ShowSubtitle("Squad Leader is not in a vehicle.");
            }
        }
        else
        {
            UI.ShowSubtitle("Squad Leader not found.");
        }
    }

    void ApplyOrder(Ped RecieveOrder)
    {
        PlayCombatAnim();
        //UI.Notify(hitcoord.ToString());
        if (CanWeUse(RecieveOrder) && RecieveOrder.IsAlive)
        {
            if (!RecieveOrder.IsSittingInVehicle())
            {
                if (CanWeUse(entitihit))
                {
                    if (entitihit is Vehicle && entitihit.IsAlive)
                    {
                        Vehicle entityveh = entitihit as Vehicle;
                        if (CanWeUse(entityveh.GetPedOnSeat(VehicleSeat.Driver)) && (entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad1Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad2Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad3Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad4Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Game.Player.Character))
                        {
                            UI.ShowSubtitle("Enter that " + entityveh.FriendlyName + " as passenger", 1000);
                            int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, entityveh);

                            for (int i = -5; i < max_seats; i++)
                            {
                                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, entityveh, i))
                                {
                                    GTA.Native.Function.Call(GTA.Native.Hash.TASK_ENTER_VEHICLE, RecieveOrder, entityveh, 10000, i, 2.0, 1, 0);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, RecieveOrder, entityveh, 10000, -1, 2.0, 1, 0);

                            //RecieveOrder.Task.DriveTo(entityveh, entityveh.Position, 4, 200f);
                            UI.ShowSubtitle("Enter that " + entityveh.FriendlyName, 1000);
                        }

                    }
                    if (entitihit is Ped)
                    {
                        if(Game.IsControlPressed(2, GTA.Control.Context))
                        {

                            Ped entityped = entitihit as Ped;
                            if (entityped != RecieveOrder)
                            {
                                UI.ShowSubtitle("Attack that ped", 1000);
                                RecieveOrder.Task.FightAgainst(entityped, -1);
                            }
                        }
                        else
                        {

                            Ped entityped = entitihit as Ped;
                            if (entityped != RecieveOrder)
                            {
                                UI.ShowSubtitle("Follow that ped", 1000);
                                // RecieveOrder.Task.FightAgainst(entityped, -1);
                                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, RecieveOrder, entitihit, 1,1,1, 2f, -1, 3f, true);

                                //RecieveOrder.Task.FollowToOffsetFromEntity(entityped, new Vector3(1,1,1), 2f, -1, 4f, true);
                            }
                        }
                    }
                }
                else
                if (hitcoord != new Vector3(0, 0, 0))
                {
                    if (Game.IsKeyPressed(Keys.ShiftKey))
                    {
                        foreach (Ped enemy in World.GetNearbyPeds(hitcoord, 40f))
                        {
                            Function.Call(Hash.REGISTER_TARGET, RecieveOrder, enemy);
                        }
                        TaskSequence RaceSequence = new TaskSequence();
                        Function.Call(Hash.TASK_FOLLOW_NAV_MESH_TO_COORD, 0, hitcoord.X, hitcoord.Y, hitcoord.Z, 3.0f, -1, 2.0f, 0, 2.0f);
                        Function.Call(Hash.TASK_COMBAT_HATED_TARGETS_AROUND_PED, 0, 50f,0);
                        if (CanWeUse(Squad1Leader) && RecieveOrder.Handle == Squad1Leader.Handle) Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, 0, Squad1ReactToEvents.Checked);
                        if (CanWeUse(Squad2Leader) && RecieveOrder.Handle == Squad2Leader.Handle) Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, 0, Squad2ReactToEvents.Checked);
                        if (CanWeUse(Squad3Leader) && RecieveOrder.Handle == Squad3Leader.Handle) Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, 0, Squad3ReactToEvents.Checked);
                        if (CanWeUse(Squad4Leader) && RecieveOrder.Handle == Squad4Leader.Handle) Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, 0, Squad4ReactToEvents.Checked);

                        RaceSequence.Close();
                        RecieveOrder.Task.PerformSequence(RaceSequence);
                        RaceSequence.Dispose();

                        UI.ShowSubtitle("Move there", 1000);
                    }
                    else
                    {


                        Vehicle v = RecieveOrder.GetVehicleIsTryingToEnter();


                        if (CanWeUse(v))
                        {
                            UI.ShowSubtitle("Drive there", 1000);
                            Vector3 pos = hitcoord - RecieveOrder.Position;

                            TaskSequence RaceSequence = new TaskSequence();
                            
                            //Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_ COORD, RecieveOrder, v, hitcoord.X, hitcoord.Y, hitcoord.Z, 20.0, 0, v.Model, 4194304, 60, 15.0);
                            Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder,v, hitcoord.X, hitcoord.Y, hitcoord.Z, 10f, 1,v.Model, 1 | 2 | 4 | 16 | 32| 4194304, 4.0f, 5.0f);

                            //     Function.Call(Hash.TASK_TURN_PED_TO_FACE_COORD, 0, pos.X, pos.Y, pos.Z);
                            RaceSequence.Close();
                            RecieveOrder.Task.PerformSequence(RaceSequence);
                            RaceSequence.Dispose();
                        }
                        else
                        {
                            UI.ShowSubtitle("Run there", 1000);
                            Vector3 pos = hitcoord - RecieveOrder.Position;

                            TaskSequence RaceSequence = new TaskSequence();
                            Function.Call(Hash.TASK_FOLLOW_NAV_MESH_TO_COORD, 0, hitcoord.X, hitcoord.Y, hitcoord.Z, 3.0f, -1, 2.0f, (hitcoord - RecieveOrder.Position).ToHeading(), 2.0f);
                            //     Function.Call(Hash.TASK_TURN_PED_TO_FACE_COORD, 0, pos.X, pos.Y, pos.Z);
                            RaceSequence.Close();
                            RecieveOrder.Task.PerformSequence(RaceSequence);
                            RaceSequence.Dispose();
                        }
                        //RecieveOrder.Task.RunTo(hitcoord, false);

                    }
                }
            }
            if (RecieveOrder.IsSittingInVehicle())
            {
                if (CanWeUse(entitihit))
                {
                    if (entitihit is Vehicle)
                    {
                        Vehicle entityveh = entitihit as Vehicle;
                        if (entityveh == RecieveOrder.CurrentVehicle)
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model))
                            {
                                if (RecieveOrder.CurrentVehicle.IsStopped)
                                {
                                    UI.ShowSubtitle("Leave the " + entityveh.FriendlyName, 1000);
                                    RecieveOrder.Task.LeaveVehicle();
                                }
                                else
                                {
                                    UI.ShowSubtitle("Land the " + entityveh.FriendlyName, 1000);
                                    Vector3 safepos = World.GetSafeCoordForPed(RecieveOrder.Position);
                                    if (safepos.DistanceTo(RecieveOrder.Position) < 50f)
                                    {
                                        Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, safepos.X, safepos.Y, safepos.Z, 20, 40f, 1f, 36f, 15, 15, -1f, 1);
                                    }
                                    else
                                    {
                                        Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, RecieveOrder.Position.X, RecieveOrder.Position.Y, RecieveOrder.Position.Z, 20, 40f, 1f, 36f, 15, 15, -1f, 1);
                                    }
                                }
                            }
                            else
                            {
                                UI.ShowSubtitle("Leave the " + entityveh.FriendlyName, 1000);
                                RecieveOrder.Task.LeaveVehicle();
                            }
                        }
                        if (entityveh != RecieveOrder.CurrentVehicle)
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model))
                            {
                                if (CanWeUse(entityveh.GetPedOnSeat(VehicleSeat.Driver)))
                                {
                                    if (entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad1Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad2Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Game.Player.Character || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad3Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad4Leader)
                                    {
                                        UI.ShowSubtitle("Protect that " + entityveh.FriendlyName, 1000);
                                        Function.Call(Hash.TASK_VEHICLE_HELI_PROTECT, RecieveOrder, RecieveOrder.CurrentVehicle, entityveh.GetPedOnSeat(VehicleSeat.Driver), 50f, 32, 25f, 35,1);
                                    }
                                    else
                                    {
                                        UI.ShowSubtitle("Chase that " + entityveh.FriendlyName, 1000);
                                        Function.Call(Hash.TASK_HELI_CHASE, RecieveOrder, entityveh, 20f, 20f, 80f);
                                    }
                                }
                                else
                                {
                                    UI.ShowSubtitle("Protect that empty " + entityveh.FriendlyName, 1000);
                                    Function.Call(Hash.TASK_VEHICLE_HELI_PROTECT, RecieveOrder, RecieveOrder.CurrentVehicle, entityveh, 50f, 32, 25f, 35, 1);

                                }

                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, RecieveOrder.CurrentVehicle.Model))
                            {
                                if (CanWeUse(entityveh.GetPedOnSeat(VehicleSeat.Driver)))
                                {
                                    
                                    if (!Game.IsControlPressed(2, GTA.Control.Context)) //&& !CanWeUse(entityveh.GetPedOnSeat(VehicleSeat.Driver)) || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad1Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad2Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Game.Player.Character || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad3Leader || entityveh.GetPedOnSeat(VehicleSeat.Driver) == Squad4Leader
                                    {
                                        if (DrivingCareful.Contains(RecieveOrder))
                                        {
                                            UI.ShowSubtitle("Escort that " + entityveh.FriendlyName + ". Careful.", 1000);
                                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityveh, -1, 40.0, 1 | 4 | 16 | 32, 20f, 1f, 5f);
                                        }
                                        if (DrivingNormal.Contains(RecieveOrder))
                                        {
                                            UI.ShowSubtitle("Escort that " + entityveh.FriendlyName + ".", 1000);
                                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityveh, -1, 60.0, 1 | 4 | 16 | 32, 3f, 1f, 10f);
                                        }
                                        if (DrivingFurious.Contains(RecieveOrder))
                                        {
                                            UI.ShowSubtitle("Escort that " + entityveh.FriendlyName + ". Be with them no matter what.", 1000);
                                            GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityveh, -1, 90.0, 1 | 4 | 16 | 32 , 1f, 1f, 30f);
                                        }
                                    }
                                    else
                                    {
                                        RecieveOrder.Task.FightAgainst(entityveh.GetPedOnSeat(VehicleSeat.Driver), -1);
                                        UI.ShowSubtitle("Chase that" + entityveh.FriendlyName, 1000);
                                    }
                                }
                            }
                        }
                    }
                    if (entitihit is Ped)
                    {
                        if(Game.IsControlPressed(2, GTA.Control.Context))
                        {

                            Ped entityped = entitihit as Ped;
                            if (entityped != RecieveOrder)
                            {

                                UI.ShowSubtitle("Attack that ped from your vehicle", 1000);
                                RecieveOrder.Task.FightAgainst(entityped, -1);
                            }
                        }
                        else
                        {
                            Ped entityped = entitihit as Ped;
                            if (entityped != RecieveOrder)
                            {

                                if (DrivingCareful.Contains(RecieveOrder))
                                {
                                    UI.ShowSubtitle("Escort that person. Careful.", 1000);
                                    GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityped, -1, 40.0, 1 | 4 | 16 | 32, 20f, 1f, 5f);
                                }
                                if (DrivingNormal.Contains(RecieveOrder))
                                {
                                    UI.ShowSubtitle("Escort that  person.", 1000);
                                    GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityped, -1, 60.0, 1 | 4 | 16 | 32, 3f, 1f, 10f);
                                }
                                if (DrivingFurious.Contains(RecieveOrder))
                                {
                                    UI.ShowSubtitle("Escort that person. Be with them no matter what.", 1000);
                                    GTA.Native.Function.Call(GTA.Native.Hash.TASK_VEHICLE_ESCORT, RecieveOrder, RecieveOrder.CurrentVehicle, entityped, -1, 90.0, 1 | 4 | 16 | 32, 1f, 1f, 30f);
                                }
                            }
                        }
                    }

                }
                if (!CanWeUse(entitihit) && hitcoord != new Vector3(0, 0, 0))
                {
                    if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, RecieveOrder.CurrentVehicle.Model))
                    {
                        if (RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == RecieveOrder)
                        {
                            if (RecieveOrder.CurrentVehicle.Model == new Model(VehicleHash.Cargobob))
                            {
                                if (RecieveOrder.Position.DistanceTo(hitcoord) < 60)
                                {
                                    UI.ShowSubtitle("Fly the cargobob there (near).", 1000);
                                    Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, hitcoord.X, hitcoord.Y, hitcoord.Z, 6, 20f, 1f, 1f, 7, 8, -1f, 1);
                                }
                                else
                                {
                                    UI.ShowSubtitle("Fly the cargobob there.", 1000);
                                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z + 30, 10.0, 0, RecieveOrder.CurrentVehicle.Model, 6, 60, 10.0);
                                }
                            }
                            else
                            {
                                UI.ShowSubtitle("Fly there.", 1000);
                                //Function.Call(Hash.TASK_HELI_MISSION, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, 0, 0, hitcoord.X, hitcoord.Y, hitcoord.Z, 6, 40f, 1f, 36f, 15, 15, -1f, 1); //makes armed helis shoot randomly
                                //Function.Call(Hash.TASK_VEHICLE_SHOOT_AT_COORD, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z, 5000f);
                                //Function.Call(Hash.ADD_VEHICLE_SUBTASK_ATTACK_COORD, RecieveOrder, hitcoord.X, hitcoord.Y, hitcoord.Z);

                                Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z + 20, 20.0, 0, RecieveOrder.CurrentVehicle.Model, 6, 60, 15.0);
                            }
                        }
                    }
                    if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, RecieveOrder.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, RecieveOrder.CurrentVehicle.Model))
                    {
                        if (RecieveOrder.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == RecieveOrder)
                        {
                            if (RecieveOrder.Position.DistanceTo(hitcoord) < 200)
                            {
                                if (RecieveOrder.Position.DistanceTo(hitcoord) < 20)
                                {
                                    UI.ShowSubtitle("Drive directly there", 1000);
                                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle, hitcoord.X, hitcoord.Y, hitcoord.Z, 10f, 1, RecieveOrder.CurrentVehicle.Model,1|2|4|16|32, 1.0, 40.0);
                                }
                                else
                                {
                                    UI.ShowSubtitle("Drive there (near)", 1000);
                                    if (DrivingCareful.Contains(RecieveOrder))
                                    {
                                       Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z, 3f, 4194304 | 1 | 2 | 32, 1f);
                                    }
                                    if (DrivingNormal.Contains(RecieveOrder))
                                    {
                                        Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z, 6f, 4194304 | 1 | 2 | 4 | 16 | 32, 1f);
                                    }
                                    if (DrivingFurious.Contains(RecieveOrder))
                                    {
                                        Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, RecieveOrder.Handle, RecieveOrder.CurrentVehicle.Handle, hitcoord.X, hitcoord.Y, hitcoord.Z, 90f, 16777264 | 1 | 2 | 4 | 16 | 32, 1f);

                                        //RecieveOrder.Task.ParkVehicle(RecieveOrder.CurrentVehicle, hitcoord, Game.Player.Character.Heading);
                                        //Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle, hitcoord.X, hitcoord.Y, hitcoord.Z, 10f, 1, RecieveOrder.CurrentVehicle.Model, 4194304 | 4 | 16 | 321, 0.5, 0.5);
                                    }


                                    //Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, RecieveOrder.Handle, RecieveOrder.CurrentVehicle, hitcoord.X, hitcoord.Y, hitcoord.Z, 10f, 1, RecieveOrder.CurrentVehicle.Model, 4194304| 32|2|1, 1.0, 2.0);
                                }
                            }
                            else
                            {
                                UI.ShowSubtitle("Drive there", 1000);
                                DriveTo(RecieveOrder, hitcoord, 5);
                            }
                        }
                        else
                        {
                            RecieveOrder.Task.RunTo(hitcoord, false);
                            UI.ShowSubtitle("Get out of that " + RecieveOrder.CurrentVehicle.FriendlyName + " and run there", 1000);
                        }
                    }
                }
            }
        }
        else
        {
            UI.ShowSubtitle("Squad Leader not found.");
        }
    }
    

    void FixSquadLeaders()
    {
        foreach (Ped ped in Squad2)
        {
            if (CanWeUse(ped))
            {
                if (!ped.IsAlive && ped.CurrentBlip.Exists())
                {
                    ped.CurrentBlip.Remove();
                }
            }
        }
        foreach (Ped ped in Squad1)
        {
            if (CanWeUse(ped))
            {
                if (!ped.IsAlive && ped.CurrentBlip.Exists())
                {
                    ped.CurrentBlip.Remove();
                }
            }
        }
        foreach (Ped ped in Squad3)
        {
            if (CanWeUse(ped))
            {
                if (!ped.IsAlive && ped.CurrentBlip.Exists())
                {
                    ped.CurrentBlip.Remove();
                }
            }
        }
        foreach (Ped ped in Squad4)
        {
            if (CanWeUse(ped))
            {
                if (!ped.IsAlive && ped.CurrentBlip.Exists())
                {
                    ped.CurrentBlip.Remove();
                }
            }
        }
        if (CanWeUse(Squad1Leader) && !Squad1Leader.IsAlive && Squad1.Count>0)
        {
       
            var alldead = 0;
            foreach (Ped ped in Squad1)
            {
                if (CanWeUse(ped) && ped.IsAlive)
                {
                    Squad1Leader = ped;
                    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Squad1Leader.Handle, Squad1Leader.CurrentPedGroup);
                    PrepareSquadLeader(Squad1Leader,Squad1);
                    break;
                }
                else
                {
                    alldead++;
                }
            }
            if (alldead >= Squad1.Count)
            {
                RemoveSquad1();
                UI.Notify("~r~Squad 1 has been defeated.");
                if (Squad1Autocall.Checked)
                {
                    CreateSquad(1);
                }
            }
        }

        if (CanWeUse(Squad2Leader) && !Squad2Leader.IsAlive && Squad2.Count > 0)
        {
            var alldead = 0;
            foreach (Ped ped in Squad2)
            {
                if (CanWeUse(ped) && ped.IsAlive)
                {

                    Squad2Leader = ped;
                    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Squad2Leader.Handle, Squad2Leader.CurrentPedGroup);
                    PrepareSquadLeader(Squad2Leader, Squad2);
                    break;
                }
                else
                {
                    alldead++;
                }
            }
            if (alldead >= Squad2.Count)
            {
                RemoveSquad2();
                UI.Notify("~r~Squad 2 has been defeated.");
                if (Squad2Autocall.Checked)
                {
                    CreateSquad(2);
                }
            }
        }


        if (CanWeUse(Squad3Leader) && !Squad3Leader.IsAlive && Squad3.Count > 0)
        {
            var alldead = 0;
            foreach (Ped ped in Squad3)
            {
                if (CanWeUse(ped) && ped.IsAlive)
                {

                    Squad3Leader = ped;
                    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Squad3Leader.Handle, Squad3Leader.CurrentPedGroup);
                    PrepareSquadLeader(Squad3Leader, Squad3);
                    break;
                }
                else
                {
                    alldead++;
                }
            }
            if (alldead >= Squad3.Count)
            {
                RemoveSquad3();
                UI.Notify("~r~Squad 1 has been defeated.");
                if (Squad3Autocall.Checked)
                {
                    CreateSquad(3);
                }
            }
        }
        if (CanWeUse(Squad4Leader) && !Squad4Leader.IsAlive && Squad4.Count > 0)
        {
            var alldead = 0;
            foreach (Ped ped in Squad4)
            {
                if (CanWeUse(ped) && ped.IsAlive)
                {

                    Squad4Leader = ped;
                    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Squad4Leader.Handle, Squad4Leader.CurrentPedGroup);
                    PrepareSquadLeader(Squad4Leader, Squad4);
                    break;
                }
                else
                {
                    alldead++;
                }
            }
            if (alldead >= Squad4.Count)
            {
                RemoveSquad4();
                UI.Notify("~r~Squad 1 has been defeated.");
                if (Squad4Autocall.Checked)
                {
                    CreateSquad(4);
                }
            }
        }
    }

    public static bool WasCheatStringJustEntered(string cheat)
    {
        return Function.Call<bool>(Hash._0x557E43C447E700A8, Game.GenerateHash(cheat));
    }

    bool DeliveredHint = false;
    void OnTick(object sender, EventArgs e)
    {


        if (!SettingsLoaded)
        {
            if (Game.Player.Character.Velocity.Length() > 1.5f)
            {
                LoadSettings();
                SettingsLoaded = true;
                UI.Notify("~b~Bodyguard Squads~y~ 1.1~w~ loaded.");                

            }

            return;
        }
        
       if(CanWeUse(Squad1Leader)) HandleAirCombat(Squad1Leader);
        if (CanWeUse(Squad2Leader)) HandleAirCombat(Squad2Leader);
        if (CanWeUse(Squad3Leader)) HandleAirCombat(Squad3Leader);
        if (CanWeUse(Squad4Leader)) HandleAirCombat(Squad4Leader);

        //DONE EVERY 2 SECONDS
        if (Game.GameTime> interval2sec +2000)
        {
            interval2sec = Game.GameTime;
        }

        //DONE EVERY SECOND
        if (Game.GameTime > interval1sec+1000)
        {
            FixSquadLeaders();
            interval1sec = Game.GameTime;

            if (CanWeUse(entitihit))
            {
                if(Game.IsControlPressed(2, GTA.Control.Context))
                {
                    UI.ShowSubtitle("Kill that person");
                }
                else
                {
                    UI.ShowSubtitle("Follow that person");
                }
            }
        }

        var res = UIMenu.GetScreenResolutionMantainRatio();
        var safe = UIMenu.GetSafezoneBounds();
        _menuPool.ProcessMenus();


        if (ShowStatus.Checked)
        {
            int pluszone = 0;
            if (CanWeUse(Squad1Leader) && Squad1.Count > 0)
            {
                pluszone = pluszone + 35;
                new Sprite("timerbars", "all_black_bg", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364), new Size(34, 250), 270f, Squad1Color).Draw();
                if (Squad1Leader.IsInCombat)
                {
                    new Sprite("mpinventory", "shooting_range", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 397), new Size(32, 32), 0f, Color.FromArgb(255, 255, 0, 0)).Draw();
                }
                int size = 32;
                var number = 0;
                foreach (Ped ped in Squad1)
                {
                    if (ped.IsAlive)
                    {
                        if (ped.IsSittingInVehicle())
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_car", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(size, size), 0f, Squad1Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, ped.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_bike", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(size, size), 0f, Squad1Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_heli", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(size, size), 0f, Squad1Color).Draw();
                            }
                            if (ped.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == ped)
                            {
                                new Sprite("mpinventory", "mp_specitem_steer_wheel", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number + size/2), new Size(16, 16), 0f, Squad1Color).Draw();
                            }
                        }
                        else
                        {
                            new Sprite("mpinventory", "mp_specitem_ped", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(size, size), 0f, Squad1Color).Draw();
                        }

                        if (ped.Health < 50)
                        {
                            new Sprite("helicopterhud", "targetlost", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone + 16, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Color.Red).Draw();
                        }
                    }
                    else
                    {
                        new Sprite("mpinventory", "survival", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad1Color).Draw();
                    }
                    number = number + 32;
                }
            }

            if (CanWeUse(Squad2Leader) && Squad2.Count > 0)
            {
                pluszone = pluszone + 35;

                new Sprite("timerbars", "all_black_bg", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364), new Size(34, 250), 270f, Squad2Color).Draw();
                if (Squad2Leader.IsInCombat)
                {
                    new Sprite("mpinventory", "shooting_range", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 397), new Size(32, 32), 0f, Color.FromArgb(255, 255, 0, 0)).Draw();
                }
                var number = 0;
                foreach (Ped ped in Squad2)
                {

                    if (ped.IsAlive)
                    {
                        if (ped.IsSittingInVehicle())
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_car", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad2Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, ped.CurrentVehicle.Model)|| GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_bike", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad2Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_heli", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad2Color).Draw();
                            }
                            if (ped.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == ped)
                            {
                                new Sprite("mpinventory", "mp_specitem_steer_wheel", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Squad2Color).Draw();
                            }
                        }
                        else
                        {
                            new Sprite("mpinventory", "mp_specitem_ped", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad2Color).Draw();
                        }
                        if (ped.Health < 50)
                        {
                            new Sprite("helicopterhud", "targetlost", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone + 16, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Color.Red).Draw();
                        }
                    }
                    else
                    {
                        new Sprite("mpinventory", "survival", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad2Color).Draw();
                    }
                    number = number + 32;
                }
            }

            if (CanWeUse(Squad3Leader) && Squad3.Count > 0)
            {
                pluszone = pluszone + 35;

                new Sprite("timerbars", "all_black_bg", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364), new Size(34, 250), 270f, Squad3Color).Draw();
                if (Squad3Leader.IsInCombat)
                {
                    new Sprite("mpinventory", "shooting_range", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 397), new Size(32, 32), 0f, Color.FromArgb(255, 255, 0, 0)).Draw();
                }
                var number = 0;
                foreach (Ped ped in Squad3)
                {

                    if (ped.IsAlive)
                    {
                        if (ped.IsSittingInVehicle())
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_car", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad3Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, ped.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_bike", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad3Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_heli", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad3Color).Draw();
                            }
                            if (ped.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == ped)
                            {
                                new Sprite("mpinventory", "mp_specitem_steer_wheel", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Squad3Color).Draw();
                            }
                        }
                        else
                        {
                            new Sprite("mpinventory", "mp_specitem_ped", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad3Color).Draw();
                        }
                        if (ped.Health < 50)
                        {
                            new Sprite("helicopterhud", "targetlost", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone + 16, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Color.Red).Draw();
                        }
                    }
                    else
                    {
                        new Sprite("mpinventory", "survival", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad3Color).Draw();
                    }
                    number = number + 32;
                }
            }

            if (CanWeUse(Squad4Leader) && Squad4.Count > 0)
            {
                pluszone = pluszone + 35;

                new Sprite("timerbars", "all_black_bg", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364), new Size(34, 250), 270f, Squad4Color).Draw();
                if (Squad4Leader.IsInCombat)
                {
                    new Sprite("mpinventory", "shooting_range", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 397), new Size(32, 32), 0f, Color.FromArgb(255, 255, 0, 0)).Draw();
                }
                var number = 0;
                foreach (Ped ped in Squad4)
                {

                    if (ped.IsAlive)
                    {
                        if (ped.IsSittingInVehicle())
                        {
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_CAR, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_car", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad4Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BIKE, ped.CurrentVehicle.Model) || GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_QUADBIKE, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_bike", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad4Color).Draw();
                            }
                            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, ped.CurrentVehicle.Model))
                            {
                                new Sprite("mpinventory", "mp_specitem_heli", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad4Color).Draw();
                            }
                            if (ped.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == ped)
                            {
                                new Sprite("mpinventory", "mp_specitem_steer_wheel", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Squad4Color).Draw();
                            }
                        }
                        else
                        {
                            new Sprite("mpinventory", "mp_specitem_ped", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad4Color).Draw();
                        }
                        if (ped.Health < 50)
                        {
                            new Sprite("helicopterhud", "targetlost", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone + 16, Convert.ToInt32(res.Height) - safe.Y - 364 + number + 16), new Size(16, 16), 0f, Color.Red).Draw();
                        }
                    }
                    else
                    {
                        new Sprite("mpinventory", "survival", new Point(Convert.ToInt32(res.Width) - safe.X - pluszone, Convert.ToInt32(res.Height) - safe.Y - 364 + number), new Size(32, 32), 0f, Squad4Color).Draw();
                    }
                    number = number + 32;
                }
            }
        }

        if (ordersmode > 0)
        {
            if (ordersmode == 1 && Squad1.Count > 0)
            {
                World.DrawMarker(MarkerType.UpsideDownCone, (hitcoord + new Vector3(0f, 0f, 0.4f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0.5f, 0.5f), Color.Blue, false, false, 0, false, "", "", false);
                if (Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack) || Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack2))
                {
                    if (CanWeUse(Squad1Leader))
                    {
                        ApplyOrder(Squad1Leader);
                    }
                    else
                    {
                        UI.Notify("Squad 1 does not exist.");
                    }
                }
                foreach (Ped ped in Squad1)
                {
                    if (CanWeUse(ped) && ped.IsAlive)
                    {
                        World.DrawMarker(MarkerType.UpsideDownCone, (ped.Position + new Vector3(0f, 0f, 1f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.2f, 0.2f, 0.2f), Color.Blue, false, false, 0, false, "", "", false);
                    }
                }
            }
            if (ordersmode == 2 && Squad2.Count > 0)
            {
                World.DrawMarker(MarkerType.UpsideDownCone, (hitcoord + new Vector3(0f, 0f, 0.4f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0.5f, 0.5f), Color.Yellow, false, false, 0, false, "", "", false);
                if (Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack) || Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack2))
                {
                    if (CanWeUse(Squad2Leader))
                    {
                        ApplyOrder(Squad2Leader);
                    }
                    else
                    {
                        UI.Notify("Squad 2 does not exist.");
                    }
                }
                foreach (Ped ped in Squad2)
                {
                    if (CanWeUse(ped) && ped.IsAlive)
                    {
                        World.DrawMarker(MarkerType.UpsideDownCone, (ped.Position + new Vector3(0f, 0f, 1f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.2f, 0.2f, 0.2f), Color.Yellow, false, false, 0, false, "", "", false);
                    }
                }
            }
            if (ordersmode == 3 && Squad3.Count > 0)
            {
                World.DrawMarker(MarkerType.UpsideDownCone, (hitcoord + new Vector3(0f, 0f, 0.4f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0.5f, 0.5f), Color.Green, false, false, 0, false, "", "", false);
                if (Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack) || Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack2))
                {
                    if (CanWeUse(Squad3Leader))
                    {
                        ApplyOrder(Squad3Leader);
                    }
                    else
                    {
                        UI.Notify("Squad 3 does not exist.");
                    }
                }
                foreach (Ped ped in Squad3)
                {
                    if (CanWeUse(ped) && ped.IsAlive)
                    {
                        World.DrawMarker(MarkerType.UpsideDownCone, (ped.Position + new Vector3(0f, 0f, 1f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.2f, 0.2f, 0.2f), Color.Green, false, false, 0, false, "", "", false);
                    }
                }
            }

            if (ordersmode == 4 && Squad4.Count > 0)
            {
                World.DrawMarker(MarkerType.UpsideDownCone, (hitcoord + new Vector3(0f, 0f, 0.4f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0.5f, 0.5f), Squad4Color, false, false, 0, false, "", "", false);
                if (Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack) || Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)GTA.Control.Attack2))
                {
                    if (CanWeUse(Squad4Leader))
                    {
                        ApplyOrder(Squad4Leader);
                    }
                    else
                    {
                        UI.Notify("Squad 3 does not exist.");
                    }
                }
                foreach (Ped ped in Squad4)
                {
                    if (CanWeUse(ped) && ped.IsAlive)
                    {
                        World.DrawMarker(MarkerType.UpsideDownCone, (ped.Position + new Vector3(0f, 0f, 1f)), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.2f, 0.2f, 0.2f), Squad4Color, false, false, 0, false, "", "", false);
                    }
                }
            }
            Raycastshit();
            Game.Player.DisableFiringThisFrame();
            /*
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)GTA.Control.Attack2, true);
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)GTA.Control.Attack, true);
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)GTA.Control.VehicleAttack, true);
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)GTA.Control.VehicleAttack2, true);
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)GTA.Control.VehiclePassengerAttack, true);
            */
        }
        else if (CanWeUse(entitihit)) entitihit = null;
    }



    void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == MenuKey && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch
        {
            if (SlowTimeWhenManaging || (WantedSlowsTime && Game.Player.WantedLevel !=0) || (CombatSlowsTime && (IsInCombat(Game.Player.Character) || IsAnySquadMemberInCombat())) || (HalfHealthSlowsTime && Game.Player.Character.Health < 50))
            {
                Game.TimeScale = TimeScale;
            }
            mainMenu.Visible = !mainMenu.Visible;

            if(mainMenu.Visible && !DeliveredHint)
            {
                UI.Notify("Remember that you can now add custom vehicles and weapons in ~b~GTAV/Scripts/advanced_bodyguads.ini~w~.");
                UI.Notify("Also remember this is an old script being revived these days. Check the 5mods page regularly for updates and hotfixes.");
                DeliveredHint = true;
            }
        }
    }

    void OnKeyUp(object sender, KeyEventArgs e)
    {/*
        if (e.KeyCode == Keys.NumPad1)
        {
            min = int.Parse(Game.GetUserInput(20));

        }
        if (e.KeyCode == Keys.NumPad2)
        {
            max = int.Parse(Game.GetUserInput(20));
        }
        */
        /*
        if (e.KeyCode == Keys.ControlKey)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "" && CanWeUse(Squad1Leader))
            {
                GTA.Native.Function.Call<bool>(GTA.Native.Hash.SET_DRIVER_AGGRESSIVENESS, Squad1Leader, float.Parse(pregunta));
                UI.Notify(pregunta + " AGGRESIVE");
            }
        }
        if (e.KeyCode == Keys.E)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "" && CanWeUse(Squad1Leader))
            {
                GTA.Native.Function.Call<bool>(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, Squad1Leader, int.Parse(pregunta),true);
                UI.Notify(pregunta + " ENABLED");
            }
        }
        if (e.KeyCode == Keys.Q)
        {
            string pregunta = Game.GetUserInput(20);
            if (pregunta != null && pregunta != "" && CanWeUse(Squad1Leader))
            {
                GTA.Native.Function.Call<bool>(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, Squad1Leader, int.Parse(pregunta), false);
                UI.Notify(pregunta+" DISABLED");
            }
        }
        */

        //ENTER AS PASSENGER
        if (e.KeyCode == EnterAsPassengerKey && Game.Player.Character.Velocity.Length() < 1f && EnterAsPassengerEnabled)
        {
            var nearbymounts = World.GetClosestVehicle(Game.Player.Character.Position, 10);
            if (CanWeUse(nearbymounts) && (nearbymounts.GetPedOnSeat(VehicleSeat.Driver) == Squad1Leader || nearbymounts.GetPedOnSeat(VehicleSeat.Driver) == Squad2Leader || nearbymounts.GetPedOnSeat(VehicleSeat.Driver) == Squad3Leader || nearbymounts.GetPedOnSeat(VehicleSeat.Driver) == Squad4Leader))
            {
                int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, nearbymounts);
                for (int i = -1; i < max_seats; i++)
                {
                    if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, nearbymounts, i))
                    {
                        GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, Game.Player.Character, nearbymounts, 5000, i, 2.0, 1, 0);
                        break;
                    }
                }
            }
        }
        
        //HANDLE ORDERS MODE
        if (e.KeyCode == OrdersModeKey)
        {
            if (SlowTimeWhenManaging || (WantedSlowsTime && Game.Player.WantedLevel>0 ) || (CombatSlowsTime && (IsInCombat(Game.Player.Character) || IsAnySquadMemberInCombat())) || (HalfHealthSlowsTime && Game.Player.Character.Health<50))
            {
                Game.TimeScale = TimeScale;
            }

            ordersmode++;
            if ((ordersmode == 1 && Squad1.Count == 0))
            {
                ordersmode++;
            }
            if ((ordersmode == 2 && Squad2.Count == 0))
            {
                ordersmode++;
            }
            if ((ordersmode == 3 && Squad3.Count == 0))
            {
                ordersmode++;
            }
            if ((ordersmode == 4 && Squad4.Count == 0))
            {
                ordersmode++;
            }
            if ((!CanWeUse(Squad2Leader) && !CanWeUse(Squad1Leader) && !CanWeUse(Squad3Leader) && !CanWeUse(Squad4Leader)))
            {
                Game.TimeScale = 1;
                ordersmode = 0;
            }
            if (ordersmode > 4)
            {
                Game.TimeScale = 1;
                ordersmode = 0;
            }
        }
    }


    
    ////////////////////////////// UTILITY FUNCTIONS //////////////////////////////

    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }
    void SetDrivingStyleAndCruiseSpeed(Ped ped, int drivingstyle, float cruisespeed)
    {
        Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, ped, cruisespeed);
        Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, ped, drivingstyle);
    }
    bool IsAnySquadMemberInCombat()
    {
        if ((CanWeUse(Squad1Leader) && Squad1Leader.IsInCombat) || (CanWeUse(Squad2Leader) && Squad2Leader.IsInCombat) || (CanWeUse(Squad3Leader) && Squad3Leader.IsInCombat) || (CanWeUse(Squad4Leader) && Squad4Leader.IsInCombat))
        {
            return true;
        }
        return false;
    }


    bool IsInCombat(Ped ped)
    {
        foreach (Ped target in World.GetAllPeds())
        {
            if (target.IsAlive && GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_COMBAT, target, ped))
            {
                return true;
            }
        }
        return false;
    }
    
    void HandleAirCombat(Ped RecieveOrder)
    {
        /*
        if (CanWeUse(RecieveOrder) && Util.IsInVehicle(RecieveOrder,VehicleSeat.Driver) && Function.Call<bool>(GTA.Native.Hash.DOES_VEHICLE_HAVE_WEAPONS, RecieveOrder.CurrentVehicle))
        {
            int Damagetype = Function.Call<int>(Hash.GET_WEAPON_DAMAGE_TYPE, Util.GetPedVehicleWeapon(RecieveOrder));
            int probability = 100;
            //Util.DisplayHelpTextThisFrame(((Util.WeaponDamageType)Damagetype).ToString());
            if ((Util.WeaponDamageType)Damagetype == Util.WeaponDamageType.BULLET) probability = 60; else if ((Util.WeaponDamageType)Damagetype == Util.WeaponDamageType.EXPLOSIVE) probability = 5;
            if (1==1)
            {
                if (RecieveOrder.IsInCombat && Util.RandomInt(1, 100) <= probability)
                {
                    foreach (Ped target in World.GetAllPeds())
                    {
                        if (target.IsAlive && Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_COMBAT, RecieveOrder, target) && Util.CanPedSeePed(RecieveOrder,target,false) && RecieveOrder.IsInRangeOf(target.Position,150f) && GTA.Native.Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, RecieveOrder, target.Position.X, target.Position.Y, target.Position.Z).Y > -10)
                        {
                            Vector3 Velocity = new Vector3(GetRandomInt(-2, 2), GetRandomInt(-2, 2), 0);
                            if (target.Velocity.Length() > 10f)
                            {
                                Velocity = Velocity + RecieveOrder.Velocity - target.Velocity;
                            }
                            Function.Call(Hash.SET_VEHICLE_SHOOT_AT_TARGET, RecieveOrder, 0, target.Position.X + Velocity.X, target.Position.Y + Velocity.Y, target.Position.Z);
                        }
                    }
                }
            }
        }
        */
    }


    void Squad1GroupFix()
    {
        int Squad1Group = Function.Call<int>(Hash.CREATE_GROUP);
        foreach (Ped ped in Squad1)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped.Handle, 1);
            if (ped == Squad1Leader)
            {
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad1Group);
            }
            else
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Squad1Group);
            }
        }
    }

    void Squad2GroupFix()
    {
    int    Squad2Group = Function.Call<int>(Hash.CREATE_GROUP);
        foreach (Ped ped in Squad2)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped.Handle, 1);
            if (ped == Squad2Leader)
            {
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad2Group);
            }
            else
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Squad2Group);
            }
        }
    }


    void Squad3GroupFix()
    {
        int Squad3Group = Function.Call<int>(Hash.CREATE_GROUP);
        foreach (Ped ped in Squad3)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped.Handle, 1);
            if (ped == Squad3Leader)
            {
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad3Group);
            }
            else
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Squad3Group);
            }
        }
    }

    void Squad4GroupFix()
    {
        int Squad4Group = Function.Call<int>(Hash.CREATE_GROUP);
        foreach (Ped ped in Squad4)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped.Handle, 1);
            if (ped == Squad4Leader)
            {
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, Squad4Group);
            }
            else
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Squad4Group);
            }
        }
    }
    void PrepareSquadLeader(Ped Squadleader, List<Ped> Squad)
    {
        string Style = "";

        if (Squad == Squad1)
        {
            Style = DrivingStyleSquad1.IndexToItem(DrivingStyleSquad1.Index).ToString();
        }
        if (Squad == Squad2)
        {
            Style = DrivingStyleSquad2.IndexToItem(DrivingStyleSquad2.Index).ToString();
        }
        if (Squad == Squad3)
        {
            Style = DrivingStyleSquad3.IndexToItem(DrivingStyleSquad3.Index).ToString();
        }
        if (Squad == Squad4)
        {
            Style = DrivingStyleSquad4.IndexToItem(DrivingStyleSquad4.Index).ToString();
        }
        if (Style == "Careful")
        {
            DrivingCareful.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 30f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 1 | 2 | 4 | 16 | 32);

        }
        if (Style == "Normal")
        {

            DrivingNormal.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 40f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 1 | 2 | 4 | 16 | 32);
        }
        if (Style == "Fast & Furious")
        {
            DrivingFurious.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 80f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 4 | 16 | 32);
        }
    }

    void PrepareSquadLeader(Ped Squadleader, int squadNumber)
    {
        string Style = "";
        
        if (squadNumber == 1)
        {
            Style = DrivingStyleSquad1.IndexToItem(DrivingStyleSquad1.Index).ToString();
        }
        if (squadNumber == 2)
        {
            Style = DrivingStyleSquad2.IndexToItem(DrivingStyleSquad2.Index).ToString();
        }
        if (squadNumber == 3)
        {
            Style = DrivingStyleSquad3.IndexToItem(DrivingStyleSquad3.Index).ToString();
        }
        if (squadNumber == 4)
        {
            Style = DrivingStyleSquad4.IndexToItem(DrivingStyleSquad4.Index).ToString();
        }
        if (Style == "Careful")
        {
            DrivingCareful.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 30f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 1 | 2 | 4 | 16 | 32);

        }
        if (Style == "Normal")
        {

            DrivingNormal.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 40f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 1 | 2 | 4 | 16 | 32);
        }
        if (Style == "Fast & Furious")
        {
            DrivingFurious.Add(Squadleader);
            Function.Call(Hash.SET_DRIVE_TASK_CRUISE_SPEED, Squadleader, 80f);
            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, Squadleader, 4 | 16 | 32);
        }
    }
    void Raycastshit()
    {
        {
            RaycastResult RayCast;

            RayCast = this.Cam_Raycast_Forward();
            if (RayCast.DitHitAnything)
            {
                hitcoord = RayCast.HitCoords;
                entitihit = RayCast.HitEntity;

                
            }
            else
            {
                Vector3 camrot = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_ROT, 0);
                Vector3 campos = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);
                hitcoord = camrot + new Vector3(RotToDir(camrot).X * 1000.0f, RotToDir(camrot).Y * 1000.0f, RotToDir(camrot).Z * 1000.0f);
            }

            if (hitcoord != new Vector3())
            {
                if (entitihit != null)
                {
                    //UI.ShowSubtitle(entitihit.ToString(), 5);
                }
            }
        }
    }

    void SetBlipName(Blip blip, string text)
    {
        GTA.Native.Function.Call(GTA.Native.Hash._0xF9113A30DE5C6670, "STRING");
        GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, text);
        GTA.Native.Function.Call(GTA.Native.Hash._0xBC38B49BCB83BC9B, blip);
    }

    void GetSquadIntoVehicle(List<Ped> Squad, Vehicle Vehicle)
    {
        int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Vehicle);
        for (int i = -1; i < max_seats; i++)
        {
            if (i == Squad.Count - 1)
            {
                break;
            }
            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, Vehicle, i) && (CanWeUse(Squad[i + 1])))
            {
                GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, Squad[i + 1], Vehicle, 10000, i, 2.0, 16, 0);
            }
        }
    }


    void HandleSquadCopRelationship()
    {

        if (Game.Player.WantedLevel > 0)
        {
            if (CanWeUse(Squad1Leader) && World.GetRelationshipBetweenGroups(Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP")) != Relationship.Hate)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP"));
                UI.Notify("Changed relationship to hate");
            }
            if (CanWeUse(Squad2Leader) && World.GetRelationshipBetweenGroups(Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP")) != Relationship.Hate)
            {
                World.SetRelationshipBetweenGroups(Relationship.Hate, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP"));
            }
        }
        else
        {
            if (CanWeUse(Squad1Leader) && World.GetRelationshipBetweenGroups(Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP")) != Relationship.Neutral)
            {
                World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad1RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP"));
                UI.Notify("Changed relationship to neutral");
            }
            if (CanWeUse(Squad2Leader) && World.GetRelationshipBetweenGroups(Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP")) != Relationship.Neutral)
            {
                World.SetRelationshipBetweenGroups(Relationship.Neutral, Squad2RelationshipGroup, Function.Call<int>(GTA.Native.Hash.GET_HASH_KEY, "COP"));
            }
        }
    }

   void GuardArea(Ped RecieveOrder)
    {
        Function.Call(Hash.TASK_WANDER_IN_AREA, RecieveOrder, RecieveOrder.Position.X, RecieveOrder.Position.Y, RecieveOrder.Position.Z,20f,2f,2f);
    }


    void VehicleFlee(Ped RecieveOrder)
    {
        if (RecieveOrder.IsSittingInVehicle())
        {
            if (DrivingCareful.Contains(RecieveOrder))
            {
                Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, RecieveOrder, RecieveOrder.CurrentVehicle, 20f, 1+2+4+16+32);
                UI.ShowSubtitle("Flee!");
            }
            if (DrivingNormal.Contains(RecieveOrder))
            {
                Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, RecieveOrder, RecieveOrder.CurrentVehicle, 30f, 1 + 2 + 4 + 16 + 32+ 262144);
                UI.ShowSubtitle("Flee!");
            }
            if (DrivingFurious.Contains(RecieveOrder))
            {
                Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, RecieveOrder, RecieveOrder.CurrentVehicle, 200f, 4 + 16 + 32+ 262144);
                UI.ShowSubtitle("Flee!");
            }
        }
        else UI.Notify("~r~Squad Leader is not in a vehicle");
    }


    void SetRelationshipGroup(int Group1, int Group2, Relationship relationship)
    {
        World.SetRelationshipBetweenGroups(relationship, Group1, Group2);
        World.SetRelationshipBetweenGroups(relationship, Group2, Group1);
    }

    //////// ORDERS ////////





    //////////////////// CREATE SQUADS
    /*
    void CreateSquad1()
    {
        Function.Call(GTA.Native.Hash.SET_GROUP_FORMATION, Squad1Group, 1);
        Function.Call(GTA.Native.Hash.SET_GROUP_FORMATION_SPACING, Squad1Group, 1.0f, 1.0f, 1.0f);

        if (Squad1Vehicle != "OnFoot")
        {
            var pasengerz = Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)Squad1Vehicle);
            if (pasengerz < Squad1Number) { Squad1Number = pasengerz; }
        }

        if (Squad1Followplayer.Checked) { Squad1Followplayer.Checked = false; }
        Squad1.Clear();
        Vector3 pos = Game.Player.Character.Position;
        if (Squad1DispatchToWaypoint.Checked && GetWaypointVector() != Vector3.Zero) { pos = GetWaypointVector(); }

        if (Squad1Vehicle != "OnFoot")
        {
            Squad1Car = World.CreateVehicle(Squad1Vehicle, World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)), 0);
            Squad1Leader = GTA.World.CreatePed(Squad1Models[GetRandomInt(0, Squad1Models.Count)], Squad1Car.Position.Around(5));
        }
        else
        {
            Squad1Leader = GTA.World.CreatePed(Squad1Models[GetRandomInt(0, Squad1Models.Count)], pos.Around(SquadSpawnDistance));
        }

        PreparePed(Squad1Leader, MainWeaponSquad1, SecondaryWeaponSquad1);
        PrepareSquadLeader(Squad1Leader, Squad1);

        Squad1.Add(Squad1Leader);
        Squad1Leader.BlockPermanentEvents = !Squad1ReactToEvents.Checked;

        if (Squad1Vehicle != "OnFoot")
        {
            Squad1Leader.SetIntoVehicle(Squad1Car, VehicleSeat.Driver);
        }

        for (int i = 1; i < Squad1Number; i++)
        {
            Squad1Member = GTA.World.CreatePed(Squad1Models[GetRandomInt(0, Squad1Models.Count)], Squad1Leader.Position.Around(4));
            PreparePed(Squad1Member, MainWeaponSquad1, SecondaryWeaponSquad1);
            Squad1.Add(Squad1Member);
        }

        foreach (Ped ped in Squad1)
        {
            if (ShowBlips.Checked)
            {
                ped.AddBlip();
                ped.CurrentBlip.Color = BlipColor.Blue;
                ped.CurrentBlip.Scale = 0.7f;
            }

            ped.AlwaysKeepTask = true;
            ped.RelationshipGroup = Squad1RelationshipGroup;
            Function.Call(Hash.SET_PED_SHOOT_RATE, ped, 1000);
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.IsInvincible = Squad1GodMode.Checked;
            SetBlipName(ped.CurrentBlip, "Squad Member");

            if (Squad1Vehicle != "OnFoot")
            {
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad1Car.Model))
                {
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, ped, 150f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MIN_ELEVATION_ANGLE, ped, -90f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MAX_ELEVATION_ANGLE, ped, 180f);
                }
                Squad1Car.IsInvincible = Squad1GodMode.Checked;
            }

        }


        if (Squad1Vehicle != "OnFoot")
        {
            int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Squad1Car);
            for (int i = -1; i < max_seats; i++)
            {
                if (i == Squad1.Count - 1)
                {
                    break;
                }
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, Squad1Car, i) && (CanWeUse(Squad1[i + 1])))
                {
                    GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, Squad1[i + 1], Squad1Car, 10000, i, 2.0, 16, 0);
                }
            }

            if (Squad1DispatchToWaypoint.Checked)
            {
                DriveTo(Squad1Leader, pos, 30);
            }
            else
            {
                DriveTo(Squad1Leader, Game.Player.Character.Position, 30);
            }
            if (Squad1Car.Model == (Model)VehicleHash.Polmav)
            {
                Squad1Car.Livery = 0;
            }
            if (Squad1Car.Model == (Model)VehicleHash.Swift)
            {
                Squad1Car.Livery = 0;
            }
            GetSquadIntoVehicle(Squad1, Squad1Car);

            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad1Car.Model))
            {
                Squad1Car.Position = Squad1Car.Position + new Vector3(0, 0, 50f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_HELI_BLADES_FULL_SPEED, Squad1Car);
            }
        }
        else
        {
            Squad1Leader.Task.RunTo(pos);
        }
        Squad1GroupFix();

        if (Squad1Vehicle != "OnFoot")
        {
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, Squad1Car.FriendlyName + " ready, " + MainWeaponSquad1.ToString() + "s loaded, we are on our way to " + World.GetStreetName(pos) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~b~Leader", "~c~" + Squad1StyleItem.IndexToItem(Squad1StyleItem.Index).ToString());
        }
    }


    void CreateSquad2()
    {
        if (Squad2Followplayer.Checked) { Squad2Followplayer.Checked = false; }
        if (Squad2Vehicle != "OnFoot")
        {
            var pasengerz = Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)Squad2Vehicle);
            if (pasengerz < Squad2Number) { Squad2Number = pasengerz; }
        }


        Vector3 pos = Game.Player.Character.Position;
        if (Squad2DispatchToWaypoint.Checked && GetWaypointVector() != Vector3.Zero) { pos = GetWaypointVector(); }
        if (Squad2Vehicle != "OnFoot")
        {
            Squad2Car = World.CreateVehicle(Squad2Vehicle, World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)), 0);
            Squad2Leader = GTA.World.CreatePed(Squad2Models[GetRandomInt(0, Squad2Models.Count)], Squad2Car.Position.Around(5));
            Squad2Leader.SetIntoVehicle(Squad2Car, VehicleSeat.Driver);
            Squad2Car.IsInvincible = Squad2GodMode.Checked;

        }
        else
        {
            Squad2Leader = GTA.World.CreatePed(Squad2Models[GetRandomInt(0, Squad2Models.Count)], pos.Around(SquadSpawnDistance));
        }

        PreparePed(Squad2Leader, MainWeaponSquad2, SecondaryWeaponSquad2);
        PrepareSquadLeader(Squad2Leader, Squad2);
        Squad2.Add(Squad2Leader);
        Squad2Leader.BlockPermanentEvents = !Squad2ReactToEvents.Checked;
        for (int i = 1; i < Squad2Number; i++)
        {
            Squad2Member = GTA.World.CreatePed(Squad2Models[GetRandomInt(0, Squad2Models.Count)], Squad2Leader.Position.Around(4));
            PreparePed(Squad2Member, MainWeaponSquad2, SecondaryWeaponSquad2);
            Squad2.Add(Squad2Member);
        }

        foreach (Ped ped in Squad2)
        {
            if (ShowBlips.Checked)
            {
                ped.AddBlip();
                ped.CurrentBlip.Color = BlipColor.Yellow;
                ped.CurrentBlip.Scale = 0.7f;
            }
            ped.AlwaysKeepTask = true;
            ped.RelationshipGroup = Squad2RelationshipGroup;
            Function.Call(Hash.SET_PED_SHOOT_RATE, ped, 1000);
            ped.IsInvincible = Squad2GodMode.Checked;
            SetBlipName(ped.CurrentBlip, "Squad Member");

            if (CanWeUse(Squad2Car) && GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad2Car.Model))
            {
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, ped, 150f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MIN_ELEVATION_ANGLE, ped, -90f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MAX_ELEVATION_ANGLE, ped, 180f);
            }
        }
        Squad2GroupFix();

        if (Squad2Vehicle != "OnFoot")
        {
            GetSquadIntoVehicle(Squad2, Squad2Car);

            if (Squad2DispatchToWaypoint.Checked)
            {
                DriveTo(Squad2Leader, pos, 30);
            }
            else
            {
                DriveTo(Squad2Leader, Game.Player.Character.Position, 30);
            }
            if (Squad2Car.GetHashCode() == (int)VehicleHash.Polmav)
            {
                Squad2Car.Livery = 0;
            }
            if (Squad2Car.Model == (Model)VehicleHash.Swift)
            {
                Squad2Car.Livery = 0;
            }
        }
        else
        {
            Squad2Leader.Task.RunTo(pos);
        }
        if (Squad2Vehicle != "OnFoot")
        {
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, Squad2Car.FriendlyName + " ready, " + MainWeaponSquad2.ToString() + "s loaded, we are on our way to " + World.GetStreetName(pos) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~y~Leader", "~c~" + Squad2StyleItem.IndexToItem(Squad2StyleItem.Index).ToString());
            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad2Car.Model))
            {
                Squad2Car.Position = Squad2Car.Position + new Vector3(0, 0, 50f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_HELI_BLADES_FULL_SPEED, Squad2Car);
            }
        }
    }
    void CreateSquad3()
    {
        if (Squad3Vehicle != "OnFoot")
        {
            var pasengerz = Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)Squad3Vehicle);
            if (pasengerz < Squad3Number) { Squad3Number = pasengerz; }
        }


        if (Squad3Followplayer.Checked) { Squad3Followplayer.Checked = false; }
        Squad3.Clear();
        Vector3 pos = Game.Player.Character.Position;
        if (Squad3DispatchToWaypoint.Checked && GetWaypointVector() != Vector3.Zero) { pos = GetWaypointVector(); }
        if (Squad3Vehicle != "OnFoot")
        {
            Squad3Car = World.CreateVehicle(Squad3Vehicle, World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)), 0);
            Squad3Leader = GTA.World.CreatePed(Squad3Models[GetRandomInt(0, Squad3Models.Count)], Squad3Car.Position.Around(5));
        }
        else
        {
            Squad3Leader = GTA.World.CreatePed(Squad3Models[GetRandomInt(0, Squad3Models.Count)], World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)));
        }

        PreparePed(Squad3Leader, MainWeaponSquad3, SecondaryWeaponSquad3);
        PrepareSquadLeader(Squad3Leader, Squad3);

        Squad3.Add(Squad3Leader);
        if (Squad3Vehicle != "OnFoot")
        {
            Squad3Leader.SetIntoVehicle(Squad3Car, VehicleSeat.Driver);
        }
        Squad3Leader.BlockPermanentEvents = !Squad3ReactToEvents.Checked;

        for (int i = 1; i < Squad3Number; i++)
        {
            Squad3Member = GTA.World.CreatePed(Squad3Models[GetRandomInt(0, Squad3Models.Count)], Squad3Leader.Position.Around(4));
            PreparePed(Squad3Member, MainWeaponSquad3, SecondaryWeaponSquad3);
            Squad3.Add(Squad3Member);
        }

        foreach (Ped ped in Squad3)
        {
            if (ShowBlips.Checked)
            {
                ped.AddBlip();
                ped.CurrentBlip.Color = BlipColor.Green;
                ped.CurrentBlip.Scale = 0.7f;
            }
            ped.AlwaysKeepTask = true;
            ped.RelationshipGroup = Squad3RelationshipGroup;
            Function.Call(Hash.SET_PED_SHOOT_RATE, ped, 1000);
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.IsInvincible = Squad3GodMode.Checked;
            SetBlipName(ped.CurrentBlip, "Squad Member");
            if (Squad3Vehicle != "OnFoot")
            {
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad3Car.Model))
                {
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, ped, 150f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MIN_ELEVATION_ANGLE, ped, -90f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MAX_ELEVATION_ANGLE, ped, 180f);
                }
            }
        }
        Squad3GroupFix();

        if (Squad3Vehicle != "OnFoot")
        {
            Squad3Car.IsInvincible = Squad3GodMode.Checked;
            int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Squad3Car);
            for (int i = -1; i < max_seats; i++)
            {
                if (i == Squad3.Count - 1)
                {
                    break;
                }
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, Squad3Car, i) && (CanWeUse(Squad3[i + 1])))
                {
                    GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, Squad3[i + 1], Squad3Car, 10000, i, 2.0, 16, 0);
                }
            }

            if (Squad3DispatchToWaypoint.Checked)
            {
                DriveTo(Squad3Leader, pos, 30);
            }
            else
            {
                DriveTo(Squad3Leader, Game.Player.Character.Position, 30);
            }
            if (Squad3Car.Model == (Model)VehicleHash.Polmav)
            {
                Squad3Car.Livery = 0;
            }
            if (Squad3Car.Model == (Model)VehicleHash.Swift)
            {
                Squad3Car.Livery = 0;
            }
            GetSquadIntoVehicle(Squad3, Squad3Car);

            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad3Car.Model))
            {
                Squad3Car.Position = Squad3Car.Position + new Vector3(0, 0, 50f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_HELI_BLADES_FULL_SPEED, Squad3Car);
            }

            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, Squad3Car.FriendlyName + " ready, " + MainWeaponSquad3.ToString() + "s loaded, we are on our way to " + World.GetStreetName(pos) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~g~Leader", "~c~" + Squad3StyleItem.IndexToItem(Squad3StyleItem.Index).ToString());
        }
        else
        {
            Squad3Leader.Task.RunTo(pos);
        }
    }



    void CreateSquad4()
    {
        if (Squad4Vehicle != "OnFoot")
        {
            var pasengerz = Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)Squad4Vehicle);
            if (pasengerz < Squad4Number) { Squad4Number = pasengerz; }
        }

        if (Squad4Followplayer.Checked) { Squad4Followplayer.Checked = false; }
        Squad4.Clear();
        Vector3 pos = Game.Player.Character.Position;
        if (Squad4DispatchToWaypoint.Checked && GetWaypointVector() != Vector3.Zero) { pos = GetWaypointVector(); }

        if (Squad4Vehicle != "OnFoot")
        {
            Squad4Car = World.CreateVehicle(Squad4Vehicle, World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)), 0);
            Squad4Leader = GTA.World.CreatePed(Squad4Models[GetRandomInt(0, Squad4Models.Count)], Squad4Car.Position.Around(5));
            Squad4Leader.SetIntoVehicle(Squad4Car, VehicleSeat.Driver);
            Squad4Car.IsInvincible = Squad4GodMode.Checked;
        }
        else
        {
            Squad4Leader = GTA.World.CreatePed(Squad4Models[GetRandomInt(0, Squad4Models.Count)], World.GetNextPositionOnStreet(pos.Around(SquadSpawnDistance)));

        }

        PreparePed(Squad4Leader, MainWeaponSquad4, SecondaryWeaponSquad4);
        PrepareSquadLeader(Squad4Leader, Squad4);

        Squad4.Add(Squad4Leader);

        Squad4Leader.BlockPermanentEvents = !Squad4ReactToEvents.Checked;

        for (int i = 1; i < Squad4Number; i++)
        {
            Squad4Member = GTA.World.CreatePed(Squad4Models[GetRandomInt(0, Squad4Models.Count)], Squad4Leader.Position.Around(4));
            PreparePed(Squad4Member, MainWeaponSquad4, SecondaryWeaponSquad4);
            Squad4.Add(Squad4Member);
        }

        foreach (Ped ped in Squad4)
        {
            if (ShowBlips.Checked)
            {
                ped.AddBlip();
                ped.CurrentBlip.Color = BlipColor.Red;
                ped.CurrentBlip.Scale = 0.7f;
                ped.AlwaysKeepTask = true;
            }
            ped.RelationshipGroup = Squad4RelationshipGroup;
            Function.Call(Hash.SET_PED_SHOOT_RATE, ped, 1000);
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.IsInvincible = Squad4GodMode.Checked;
            SetBlipName(ped.CurrentBlip, "Squad Member");
            if (Squad4Vehicle != "OnFoot")
            {
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad4Car.Model))
                {
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, ped, 150f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MIN_ELEVATION_ANGLE, ped, -90f);
                    GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MAX_ELEVATION_ANGLE, ped, 180f);
                }
            }
        }
        Squad4GroupFix();

        if (Squad4Vehicle != "OnFoot")
        {
            int max_seats = GTA.Native.Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Squad4Car);
            for (int i = -1; i < max_seats; i++)
            {
                if (i == Squad4.Count - 1)
                {
                    break;
                }
                if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_SEAT_FREE, Squad4Car, i) && (CanWeUse(Squad4[i + 1])))
                {
                    GTA.Native.Function.Call<bool>(GTA.Native.Hash.TASK_ENTER_VEHICLE, Squad4[i + 1], Squad4Car, 10000, i, 2.0, 16, 0);
                }
            }

            if (Squad4DispatchToWaypoint.Checked)
            {
                DriveTo(Squad4Leader, pos, 30);
            }
            else
            {
                DriveTo(Squad4Leader, Game.Player.Character.Position, 30);
            }
            if (Squad4Car.Model == (Model)VehicleHash.Polmav)
            {
                Squad4Car.Livery = 0;
            }
            if (Squad4Car.Model == (Model)VehicleHash.Swift)
            {
                Squad4Car.Livery = 0;
            }
            GetSquadIntoVehicle(Squad4, Squad4Car);

            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_HELI, Squad4Car.Model))
            {
                Squad4Car.Position = Squad4Car.Position + new Vector3(0, 0, 50f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_HELI_BLADES_FULL_SPEED, Squad4Car);
            }

            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, Squad4Car.FriendlyName + " ready, " + MainWeaponSquad4.ToString() + "s loaded, we are on our way to " + World.GetStreetName(pos) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~o~Leader", "~c~" + Squad4StyleItem.IndexToItem(Squad4StyleItem.Index).ToString());
        }
        else
        {
            Squad4Leader.Task.RunTo(pos);
        }
    }
    */
    protected override void Dispose(bool dispose)
    {

        RemoveSquad1(); RemoveSquad2(); RemoveSquad3(); RemoveSquad4();
        base.Dispose(dispose);
    }


    void CreateSquad(int squadNumber)
    {
      //  UI.Notify("Creating Squad");
        bool ReactToEvents = true;
        bool FollowPlayer = false;

        List<string> pedmodels = new List<string>();
        Model pedmodel = "player_zero";
        Model vehiclemodel = "dubsta";
        BlipColor BColor = BlipColor.White;
        bool DispatchToWaypoint = false;
        int passengers = 8;
        int RLGroup = 0;
        int SquadGroup = Function.Call<int>(Hash.CREATE_GROUP);

        for(int i=0; i<10; i++)
        {
            if(!Function.Call<bool>(Hash.DOES_GROUP_EXIST, SquadGroup))
            {
                SquadGroup = Function.Call<int>(Hash.CREATE_GROUP);
                Script.Wait(1000);
            }
        }


        Function.Call(GTA.Native.Hash.SET_GROUP_FORMATION, SquadGroup,0);
        Function.Call(GTA.Native.Hash.SET_GROUP_FORMATION_SPACING, SquadGroup, 2f, 2f,5f);
        WeaponHash MainWeapon = WeaponHash.Unarmed;
        WeaponHash SecondaryWeapon = WeaponHash.Unarmed;
        string SquadName = "Squad";
        switch (squadNumber)
        {
            case 1: FollowPlayer = Squad1Followplayer.Checked; ReactToEvents = Squad1ReactToEvents.Checked; vehiclemodel = Squad1Vehicle; pedmodels = Squad1Models; SquadName = Squad1StyleItem.IndexToItem(Squad1StyleItem.Index).ToString(); passengers = Squad1Number; BColor = BlipColor.Blue; DispatchToWaypoint = Squad1DispatchToWaypoint.Checked; RLGroup = Squad1RelationshipGroup; MainWeapon = MainWeaponSquad1; SecondaryWeapon = SecondaryWeaponSquad1; break;
            case 2: FollowPlayer = Squad2Followplayer.Checked; ReactToEvents = Squad2ReactToEvents.Checked; vehiclemodel = Squad2Vehicle; pedmodels = Squad2Models; SquadName = Squad2StyleItem.IndexToItem(Squad2StyleItem.Index).ToString(); passengers = Squad2Number; BColor = BlipColor.Yellow; DispatchToWaypoint = Squad2DispatchToWaypoint.Checked; RLGroup = Squad2RelationshipGroup; MainWeapon = MainWeaponSquad2; SecondaryWeapon = SecondaryWeaponSquad2; break;
            case 3: FollowPlayer = Squad3Followplayer.Checked; ReactToEvents = Squad3ReactToEvents.Checked; vehiclemodel = Squad3Vehicle; pedmodels = Squad3Models; SquadName = Squad3StyleItem.IndexToItem(Squad3StyleItem.Index).ToString(); passengers = Squad3Number; BColor = BlipColor.Green; DispatchToWaypoint = Squad3DispatchToWaypoint.Checked; RLGroup = Squad3RelationshipGroup; MainWeapon = MainWeaponSquad3; SecondaryWeapon = SecondaryWeaponSquad3; break;
            case 4: FollowPlayer = Squad4Followplayer.Checked; ReactToEvents = Squad4ReactToEvents.Checked; vehiclemodel = Squad4Vehicle; pedmodels = Squad4Models; SquadName = Squad4StyleItem.IndexToItem(Squad4StyleItem.Index).ToString(); passengers = Squad4Number; BColor = BlipColor.Red; DispatchToWaypoint = Squad4DispatchToWaypoint.Checked; RLGroup = Squad4RelationshipGroup; MainWeapon = MainWeaponSquad4; SecondaryWeapon = SecondaryWeaponSquad4; break;
        }

        Vehicle veh = null;
        if (vehiclemodel.IsVehicle)
        {

            Vector3 pos = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(SquadSpawnDistance));
            if (DispatchToWaypoint) pos = World.GetNextPositionOnStreet(World.GetWaypointPosition());
            veh = World.CreateVehicle(vehiclemodel, pos);
        }

        if (CanWeUse(veh))
        {
            if (Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)vehiclemodel) < passengers) passengers = Function.Call<int>(GTA.Native.Hash._GET_VEHICLE_MODEL_MAX_NUMBER_OF_PASSENGERS, (Model)vehiclemodel);
        }
        Vector3 pedpos = Game.Player.Character.Position.Around(5);
        if (CanWeUse(veh)) pedpos = veh.Position + new Vector3(0, 0, 5f);

        List<Ped> PedTeam = new List<Ped>();
        for (int i = 0; i < passengers; i++)
        {
            Ped p = World.CreatePed(pedmodels[GetRandomInt(0, pedmodels.Count - 1)], pedpos);
            p.AddBlip();
            p.CurrentBlip.Scale = 0.7f;
            p.CurrentBlip.Name = "Squad Member";
            p.CurrentBlip.Color = BColor;
            Function.Call(Hash.SET_PED_SHOOT_RATE, p, 1000);
            p.RelationshipGroup = RLGroup;
            PreparePed(p, MainWeapon, SecondaryWeapon);
            if (CanWeUse(veh) && veh.Model.IsHelicopter)
            {
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, p, 150f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MIN_ELEVATION_ANGLE, p, -90f);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_VISUAL_FIELD_MAX_ELEVATION_ANGLE, p, 180f);
            }


            PedTeam.Add(p);
        }


        foreach (Ped ped in PedTeam)
        {
            if (ped == PedTeam[0])
            {

                if (CanWeUse(veh)) ped.SetIntoVehicle(veh, VehicleSeat.Driver);

            }
            else
            {

                if (CanWeUse(veh)) ped.SetIntoVehicle(veh, VehicleSeat.Any);
            }


        }

        if (CanWeUse(veh) && veh.Model.IsHelicopter)
        {
            veh.Position = veh.Position + new Vector3(0, 0, 50f);
            GTA.Native.Function.Call(GTA.Native.Hash.SET_HELI_BLADES_FULL_SPEED, veh);
        }
        Vector3 Dest = Game.Player.Character.Position;


        PrepareSquadLeader(PedTeam[0], squadNumber);

        PedTeam[0].BlockPermanentEvents = !ReactToEvents;
        switch (squadNumber)
        {
            case 1:
                {

                    foreach (Ped p in Squad1) if (CanWeUse(p)) p.Delete();
                    if (CanWeUse(veh)) Squad1Car = veh;
                    Squad1 = PedTeam;
                    Squad1Leader = Squad1[0];
                    break;
                }

            case 2:
                {
                    foreach (Ped p in Squad2) if (CanWeUse(p)) p.Delete();
                    if (CanWeUse(veh)) Squad2Car = veh;
                    Squad2 = PedTeam;
                    Squad2Leader = Squad2[0];
                    break;
                }
            case 3:
                {
                    foreach (Ped p in Squad3) if (CanWeUse(p)) p.Delete();
                    if (CanWeUse(veh)) Squad3Car = veh;
                    Squad3 = PedTeam;
                    Squad3Leader = Squad3[0];
                    break;
                }
            case 4:
                {
                    foreach (Ped p in Squad4) if (CanWeUse(p)) p.Delete();
                    if (CanWeUse(veh)) Squad4Car = veh;
                    Squad4 = PedTeam;
                    Squad4Leader = Squad4[0];
                    break;
                }
        }

        if (!DispatchToWaypoint)
        {
            if (CanWeUse(veh)) DriveTo(PedTeam[0], Dest, 30f); else PedTeam[0].Task.RunTo(Game.Player.Character.Position, true, -1);
        }
        else Dest = World.GetWaypointPosition();


        if (CanWeUse(veh))
        {
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, veh.FriendlyName + " ready, " + MainWeapon.ToString() + "s loaded, we are on our way to " + World.GetStreetName(Dest) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~g~Leader", "~c~" + SquadName);
        }
        else
        {
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            GTA.Native.Function.Call(GTA.Native.Hash._ADD_TEXT_COMPONENT_STRING, MainWeapon.ToString() + "s loaded, we are on our way to " + World.GetStreetName(Dest) + ".");
            GTA.Native.Function.Call(GTA.Native.Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 2, "~g~Leader", "~c~" + SquadName);
        }




/*
        foreach (Ped ped in PedTeam)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped.Handle,true);
            if (FollowPlayer)
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, Game.Player.Character.CurrentPedGroup);
            }
            else
            {

                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, SquadGroup);

            }
        }
        if (!FollowPlayer)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedTeam[0].Handle);

            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, PedTeam[0].Handle, SquadGroup);
        }
        */
        foreach (Ped ped in PedTeam)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, true);

            if (ped == PedTeam[0])
            {
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped.Handle, SquadGroup);
            }
            else
            {
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped.Handle, SquadGroup);
            }
        }
    
    //UI.Notify("Squad " + squadNumber + " created");
    }

    //// REMOVE SQUADS ////
    void RemoveSquad1()
    {
        foreach (Ped ped in Squad1)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.CurrentBlip.Remove();
            ped.BlockPermanentEvents = false;
            ped.Task.ClearAll();
            ped.MarkAsNoLongerNeeded();
        }

        Squad1.Clear();
        if (CanWeUse(Squad1Car))
        {
            Squad1Car.MarkAsNoLongerNeeded();
        }
    }

    void RemoveSquad2()
    {
        foreach (Ped ped in Squad2)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.CurrentBlip.Remove();
            ped.BlockPermanentEvents = false;
            ped.Task.ClearAll();
            ped.MarkAsNoLongerNeeded();
        }

        Squad2.Clear();
        if (CanWeUse(Squad2Car))
        {
            Squad2Car.MarkAsNoLongerNeeded();
        }
    }

    void RemoveSquad3()
    {
        foreach (Ped ped in Squad3)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.CurrentBlip.Remove();
            ped.BlockPermanentEvents = false;
            ped.Task.ClearAll();
            ped.MarkAsNoLongerNeeded();
        }

        Squad3.Clear();
        if (CanWeUse(Squad3Car))
        {
            Squad3Car.MarkAsNoLongerNeeded();
        }
    }

    void RemoveSquad4()
    {
        foreach (Ped ped in Squad4)
        {
            Function.Call(Hash.REMOVE_PED_FROM_GROUP, ped.Handle);
            ped.CurrentBlip.Remove();
            ped.BlockPermanentEvents = false;
            ped.Task.ClearAll();
            ped.MarkAsNoLongerNeeded();
        }

        Squad4.Clear();
        if (CanWeUse(Squad4Car))
        {
            Squad4Car.MarkAsNoLongerNeeded();
        }
    }


}


