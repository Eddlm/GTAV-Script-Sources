using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

enum WheelmanMissionStatus { NotStarted,RecievingCall,WaitingForCorrectTime,PickingUpPartners,GoingToStore,Robbing,Fleeing,Finished};

public class WheelmanMissions : Script
{
    string ScriptName = "Wheelman";
    string ScriptVer = "1.0";
    bool Debug = true;
    WheelmanMissionStatus MissionState = WheelmanMissionStatus.NotStarted;
    List<Ped> HelperPeds = new List<Ped>();
    int RLGroup = World.AddRelationshipGroup("StoreRobbers");
    
    int Money;
    Blip Destination;
    int RefTime = Game.GameTime;
    Vector3 SelectedShop;
    Vector3 PedPickup=Game.Player.Character.Position;
    int Time = Game.GameTime;
    Ped Clerk = null;
    List<Vector3> ShopstoRob = new List<Vector3>
    {
        new Vector3(1720,6410,35),
        new Vector3(2564,385,107),
        new Vector3(545,2676,41),
        new Vector3(-3036,589,7),
        new Vector3(1966,3737,32),
        new Vector3(2684,3282,55),

        //new Vector3(-54,-1757,29),
        new Vector3(376,321,103),
    };

    Blip Shop = null;
    List<Blip> ShopBlips = new List<Blip>();

    int AimCount = 0;
    public WheelmanMissions()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        //Interval = 100;
        foreach (Vector3 pos in ShopstoRob) { Blip blip = World.CreateBlip(pos); ShopBlips.Add(blip); blip.IsShortRange = true; blip.Sprite = BlipSprite.DollarSignSquared; blip.Color = BlipColor.Green; blip.Name = "24/7"; }
        World.SetRelationshipBetweenGroups(Relationship.Neutral, RLGroup, Game.GenerateHash("PLAYER"));
    }

    void ClearMission()
    {
        foreach (Ped ped in HelperPeds)
        {
            //Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped, 7,true);
            if (ped.CurrentBlip.Exists()) ped.CurrentBlip.Remove();
            ped.MarkAsNoLongerNeeded();
        }
        HelperPeds.Clear();
        Money = 0;
        if (Destination != null) Destination.Remove();
        MissionState = WheelmanMissionStatus.NotStarted;
    }
    public static bool HasPlayerBeenRecentlyArrested(int MS_Threshold)
    {
        if (Function.Call<int>(Hash.GET_TIME_SINCE_LAST_ARREST) == -1) return false;
        return Function.Call<int>(Hash.GET_TIME_SINCE_LAST_ARREST) < MS_Threshold;
    }
    void HandleWheelmanMission()
    {
        HandleSubtitles();
        HandleMessages();
        //DisplayHelpTextThisFrame(MissionState.ToString());
        if (Game.Player.IsDead || HasPlayerBeenRecentlyArrested(0)) ClearMission();
        foreach (Ped ped in HelperPeds) if (ped.IsDead) { DisplayHelpTextThisFrame("~r~Wheelman Mission failed: ~w~One of the partners has died!"); ClearMission(); }

        switch (MissionState)
        {
            case WheelmanMissionStatus.NotStarted:
                {
                    foreach (Vector3 pos in ShopstoRob)
                    {
                        if (Game.Player.Character.IsInRangeOf(pos, 20f) && Game.Player.Character.Velocity.Length()<0.5f)
                        {
                            if (!Game.IsControlPressed(2, GTA.Control.Context))
                            {
                                DisplayHelpTextThisFrame("Bring up your phone and press ~INPUT_CONTEXT~ to set up a Wheelman mission for this store.");
                            }
                            else if(Function.Call<bool>(Hash.IS_PED_RUNNING_MOBILE_PHONE_TASK, Game.Player.Character))
                            {
                                if (CanWeUse(GetClosestClerk(Game.Player.Character.Position)))
                                {
                                    SelectedShop = pos;

                                    WarnPlayer("Partner", "Store Holdup", "Hey, we're interested in your business. We're two people.");
                                    UI.Notify("We get the money, you drive us out of there. Then we distribute what we got between the three.");
                                    UI.Notify("You okay with that? (Y/N)");

                                    MissionState = WheelmanMissionStatus.RecievingCall;
                                }
                                else
                                {
                                    DisplayHelpTextThisFrame("Looks like there isn't any clerk for this store.");

                                }
                                //Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character.Handle, UnitGroup);
                                //PedPickup = World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(130f));
                                //Time = Game.GameTime + 6000;


                            }
                            else
                            {
                                DisplayHelpTextThisFrame("Bring up your phone.");
                            }
                            break;
                        }
                    }
                    break;
                }
            case WheelmanMissionStatus.RecievingCall:
                {
                    if (Game.IsKeyPressed(Keys.Y))
                    {
                        UI.Notify("Great. Get a car with enough seats and come pick us up.");
                        MissionState = WheelmanMissionStatus.WaitingForCorrectTime;
                    }
                    if (Game.IsKeyPressed(Keys.N))
                    {
                        UI.Notify("No bussiness to do then.");
                        ClearMission();
                    }
                    break;
                }
            case WheelmanMissionStatus.WaitingForCorrectTime:
                {
                    if (Game.GameTime>Time)
                    {
                        Destination = World.CreateBlip(PedPickup);
                        Destination.ShowRoute = true;

                        Ped ped = World.CreateRandomPed(PedPickup);
                        ped.Weapons.Give(WeaponHash.Pistol, -1, true, true);
                        ped.Heading = RandomInt(0, 180);
                        ped.Task.WanderAround(ped.Position, 3f);

                        ped.AddBlip();
                        ped.CurrentBlip.Color = BlipColor.Green;
                        ped.CurrentBlip.Scale = 0.5f;
                        ped.RelationshipGroup = RLGroup;
                        Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, true);
                        HelperPeds.Add(ped);


                        ped = World.CreateRandomPed(PedPickup.Around(3f));
                        ped.Weapons.Give(WeaponHash.Pistol, -1, true, true);
                        Function.Call(Hash.SET_PED_NEVER_LEAVES_GROUP, ped, true);
                        ped.Heading = RandomInt(0, 180);
                        ped.Task.WanderAround(ped.Position, 3f);

                        ped.AddBlip();
                        ped.CurrentBlip.Color = BlipColor.Green;
                        ped.CurrentBlip.Scale = 0.5f;
                        ped.RelationshipGroup = RLGroup;

                        HelperPeds.Add(ped);
                        Destination.Position = ped.Position;
                        //AddHelpText("Pick up your ~y~partners~w~.");

                        MissionState = WheelmanMissionStatus.PickingUpPartners;
                    }
                    break;
                }
            case WheelmanMissionStatus.PickingUpPartners:
                {
                    if (Game.Player.Character.IsInRangeOf(HelperPeds[0].Position, 10f) && !HelperPeds[0].IsInGroup)
                    {
                        if(Game.Player.WantedLevel > 0)
                        {
                            DisplayHelpTextThisFrame("Lose the cops before picking up your partners.");
                        }
                        else
                        {
                            foreach (Ped ped in HelperPeds)
                            {
                                ped.BlockPermanentEvents = true;
                                ped.Task.ClearAll();
                                if (!ped.IsInGroup) Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character));
                            }
                            if (Destination != null) Destination.Remove();
                            AddSubtitle("Here you are.");
                            if (!CanWeUse(Game.Player.Character.CurrentVehicle)) AddSubtitle("Is that the car?");
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, HelperPeds[0].Handle, "Generic_Hi", "Speech_Params_Force");

                        }
                    }

                    if (AreAllPartnersInTheCar())
                    {
                        //AddHelpText("Go to the ~y~Store~w~.");
                        foreach (Ped ped in HelperPeds)
                        {
                            ped.BlockPermanentEvents = false;
                        }
                        MissionState = WheelmanMissionStatus.GoingToStore;
                        AddSubtitle("Lets go.");
                        AddSubtitle("The plan is simple, you park near the store, I get in, get the money...");
                        AddSubtitle("... and then you drive us to a safe place. Then we'll distribute the money.");
                        Destination = World.CreateBlip(SelectedShop);
                        Destination.ShowRoute = true;
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, HelperPeds[1].Handle, "CHAT_STATE", "Speech_Params_Force");
                    }
                    break;
                }
            case WheelmanMissionStatus.GoingToStore:
                {
                    bool CanRob = false;

                    if (Game.Player.Character.IsInRangeOf(SelectedShop, 40f) && Game.Player.Character.Velocity.Length()<0.5f)
                    {
                        Ped clerk = GetClosestClerk(Game.Player.Character.Position);
                        Ped ped = HelperPeds[0];

                        if (CanWeUse(clerk))
                        {
                            if (Destination != null) Destination.Remove();

                            Vector3 pos = clerk.Position + (clerk.ForwardVector * 2);

                            if (Destination.Exists()) Destination.Remove();
                            if (ped.IsStopped && !ped.IsInRangeOf(clerk.Position, 5f))
                            {
                                ped.LeaveGroup();
                                Function.Call(Hash.TASK_FOLLOW_NAV_MESH_TO_COORD, ped, pos.X, pos.Y, pos.Z, 1f, -1, 0f, 0, 0f);
                                AddSubtitle("Keep the engine on.");
                            }
                            if (ped.IsInRangeOf(pos, 1f))
                            {
                                if (!Function.Call<bool>(Hash.GET_IS_TASK_ACTIVE, ped, 4) && ped.IsStopped)
                                {
                                    ped.Task.AimAt(clerk, -1);
                                }
                                if(Function.Call<bool>(Hash.GET_IS_TASK_ACTIVE, ped, 4))
                                {
                                    AimCount++;
                                }
                            }

                            switch (AimCount)
                            {
                                case 5:
                                    {
                                        clerk.AlwaysKeepTask = true;
                                        Function.Call<int>(Hash.TASK_HANDS_UP, clerk, -1, ped, false);
                                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped.Handle, "Generic_Insult_High", "Speech_Params_Force");

                                        break;
                                    }
                                case 150:
                                    {
                                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped.Handle, "GENERIC_THANKS", "Speech_Params_Force");

                                        //AddSubtitle("Almost!");
                                        break;
                                    }
                            }
                            if (AimCount > 200) { AimCount = 0; CanRob = true; }
                        }
                        else
                        {
                            DisplayHelpTextThisFrame("Looks like the Clerk isn't around. Drive far and come back to let it spawn.");
                        }
                        if (CanRob)
                        {
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character));

                            Function.Call(Hash.TASK_ENTER_VEHICLE, HelperPeds[0], HelperPeds[0].LastVehicle, 5000, 0, 10f, 1, 0);
                            //HelperPeds[0].Task.EnterVehicle(HelperPeds[0].LastVehicle, VehicleSeat.Passenger,5);
                            //DisplayHelpTextThisFrame("Robbed!");
                            Money = RandomInt(1000, 5000);
                            Function.Call(Hash.SET_MAX_WANTED_LEVEL, 5);
                            Game.Player.WantedLevel = 2;
                            MissionState = WheelmanMissionStatus.Fleeing;
                            clerk.Task.FleeFrom(clerk.Position);
                            clerk.MarkAsNoLongerNeeded();
                            AddSubtitle("Here we go! ~g~$" + Money + "~w~, to divide between the three of us!");
                            AddSubtitle("Now get us out of here!");
                            AddHelpText("Store succesfully robbed! Lose the cops to get your reward.");

                        }
                    }
                    break;
                }
            case WheelmanMissionStatus.Fleeing:
                {
                    if (Game.Player.WantedLevel == 0)
                    {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, HelperPeds[0].Handle, "GENERIC_FRIGHTENED_HIGH", "Speech_Params_Force");

                        if (Game.Player.Character.Velocity.Length() > 0.5f) AddSubtitle("Everything according to the plan! Park nearby and we'll distribute the money.");
                        MissionState = WheelmanMissionStatus.Finished;
                    }
                    break;
                }
            case WheelmanMissionStatus.Finished:
                {
                    if (Game.Player.Character.Velocity.Length() < 0.5f)
                    {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character.Handle, "GENERIC_BYE", "Speech_Params_Force");

                        Vehicle veh = GetLastVehicle(Game.Player.Character);
                        int MoneyEach = Money / (HelperPeds.Count + 1);
                        Game.Player.Money += MoneyEach;
                        AddSubtitle("That's ~g~$" + MoneyEach + "~w~ for our favourite wheelman.");

                        foreach (Ped ped in HelperPeds) { ped.Money += MoneyEach; Function.Call(Hash.RESET_PED_LAST_VEHICLE, ped); }
                        AddSubtitle("A pleasure doing business with ya.");
                        if (CanWeUse(veh) && (veh.Health < 700 || !Function.Call<bool>(Hash._ARE_ALL_VEHICLE_WINDOWS_INTACT, veh)) ) { AddSubtitle("And sorry for the vehicle beat up."); }
                        
                        ClearMission();
                        MissionState = WheelmanMissionStatus.NotStarted;
                    }
                    break;
                }
        }
    }
    public  Vehicle GetLastVehicle(Ped RecieveOrder)
    {
        Vehicle vehicle = Function.Call<Vehicle>(Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, false);
        if (CanWeUse(vehicle)) return vehicle;

        vehicle = Function.Call<Vehicle>(Hash.GET_VEHICLE_PED_IS_IN, RecieveOrder, true);
        if (CanWeUse(vehicle)) return vehicle;
        return null;
    }
    public static int RandomInt(int min, int max)
    {
        max++;
        return Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, min, max);
    }

    void OnTick(object sender, EventArgs e)
    {
        HandleWheelmanMission();
        PartnersSpawnPosGetter();
    }
    public Ped GetClosestClerk(Vector3 pos)
    {
        foreach (Ped ped in World.GetAllPeds()) if (ped.Model == PedHash.ShopKeep01 && ped.IsInRangeOf(Game.Player.Character.Position,30f)) return ped;
        return null;
    }

    public static bool IsRaining()
    {
        int weather = Function.Call<int>(GTA.Native.Hash._0x564B884A05EC45A3); //get current weather hash
        switch (weather)
        {
            case (int)Weather.Blizzard:
                {
                    return true;
                }
            case (int)Weather.Clearing:
                {
                    return true;
                }
            case (int)Weather.Foggy:
                {
                    return true;
                }
            case (int)Weather.Raining:
                {
                    return true;
                }
            case (int)Weather.Neutral:
                {
                    return true;
                }
            case (int)Weather.ThunderStorm:
                {
                    return true;
                }
            case (int)Weather.Snowlight:
                {
                    return true;
                }
            case (int)Weather.Snowing:
                {
                    return true;
                }
            case (int)Weather.Christmas:
                {
                    return true;
                }
        }
        return false;
    }
    public static bool IsNightTime()
    {
        int hour = Function.Call<int>(Hash.GET_CLOCK_HOURS);
        return (hour > 20 || hour < 7);
    }
    public bool AreAllPartnersInTheCar()
    {
        int partners = 0;
        foreach (Ped partner in HelperPeds)
        {
            if (CanWeUse(partner.CurrentVehicle)) partners++;
        }
        if (partners == HelperPeds.Count) return true;
        return false;
    }
    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }
    void OnKeyUp(object sender, KeyEventArgs e)
    {

    }
    protected override void Dispose(bool dispose)
    {
        foreach (Blip blip in ShopBlips) { blip.Remove(); }
        ShopBlips.Clear();
        ClearMission();
        //base.Dispose(dispose);
    }
    void PartnersSpawnPosGetter()
    {
        if (PedPickup.DistanceTo(Game.Player.Character.Position) > 200f) PedPickup = World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(150f));
    }
    List<Blip> GetActiveBlips(BlipSprite sprite)
    {
        List<Blip> blips = new List<Blip>();
        for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_ACTIVE_BLIPS); i++)
        {
            Blip blip = Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, i);
            if (blip.Sprite == sprite)
            {
                while (blip.Exists())
                {
                    blips.Add(blip);
                    blip = Function.Call<Blip>(Hash.GET_NEXT_BLIP_INFO_ID, i);
                }
            }
        }

        return blips;
    }


    /// TOOLS ///
    void LoadSettings()
    {
        if (File.Exists(@"scripts\\SCRIPTNAME.ini"))
        {

            ScriptSettings config = ScriptSettings.Load(@"scripts\SCRIPTNAME.ini");
            // = config.GetValue<bool>("GENERAL_SETTINGS", "NAME", true);
        }
        else
        {
            WarnPlayer(ScriptName + " " + ScriptVer, "SCRIPT RESET", "~g~Towing Service has been cleaned and reset succesfully.");
        }
    }

    void WarnPlayer(string script_name, string title, string message)
    {
        Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
        Function.Call(Hash._SET_NOTIFICATION_MESSAGE, "CHAR_DEFAULT", "CHAR_DEFAULT", true, 0, title, "~b~" + script_name);
    }

    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, false, -1);
    }

    public static List<String> MessageQueue = new List<String>();
    public static int MessageQueueInterval = 8000;
    public static int MessageQueueReferenceTime = 0;
    public void HandleMessages()
    {
        if (MessageQueue.Count > 0)
        {
            DisplayHelpTextThisFrame(MessageQueue[0]);
        }
        else
        {
            MessageQueueReferenceTime = Game.GameTime;
        }
        if (Game.GameTime > MessageQueueReferenceTime + MessageQueueInterval)
        {
            if (MessageQueue.Count > 0)
            {
                MessageQueue.RemoveAt(0);
            }
            MessageQueueReferenceTime = Game.GameTime;
        }
    }
    public void AddHelpText(string text)
    {
        if (!MessageQueue.Contains(text)) MessageQueue.Add(text);
    }
    public void AddSubtitle(string text)
    {
        if (!SubtitleQueue.Contains(text)) SubtitleQueue.Add(text);
    }
    public static List<String> SubtitleQueue = new List<String>();
    public static int SubtitleQueueInterval = 4000;
    public static int SubtitleQueueReferenceTime = 0;
    public void HandleSubtitles()
    {
        if (SubtitleQueue.Count > 0)
        {
            UI.ShowSubtitle(SubtitleQueue[0]);
        }
        else
        {
            SubtitleQueueReferenceTime = Game.GameTime;
        }
        if (Game.GameTime > SubtitleQueueReferenceTime + SubtitleQueueInterval)
        {
            if (SubtitleQueue.Count > 0)
            {
                SubtitleQueue.RemoveAt(0);
            }
            SubtitleQueueReferenceTime = Game.GameTime;
        }
    }
}