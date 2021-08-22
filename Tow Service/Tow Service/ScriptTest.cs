using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Tow_Service
{
    enum PlayerTowingPhase
    {
        NotStarted,
        TowApproachingArea,
        TowDriverServicing,
        TowDriverFinishedJob,
    }
    public enum AmbientTowingPhase
    {
        TowApproachingArea,
        TowApproachingVehicle,
        VehicleTowed,
        TowFinishedJob,
    }
    enum FixingPhase
    {
        NotStarted,
        PlayingAnim,
        Fixing,
        Done,
    }
    public class TowingService : Script
    {

        public static  List<string> NormalTowtrucks = new List<string> { "towtruck", "towtruck2", "ramptruck2", "mule5" }; //
        public static List<string> BigTowtrucks = new List<string> { "sturdy2", "flatbed","mule5" };
        public static List<string> Helicopters = new List<string> { "skylift", "skylift", };

        List<String> MessageQueue = new List<String>();
        List<Vector3> LSCustomsLocations = new List<Vector3>();
        Dictionary<Vector3, float> LSCustomsVectorToHeading = new Dictionary<Vector3, float>();
        public static List<Rope> Ropes = new List<Rope>();

        int MessageQueueInterval = 8000;
        int MessageQueueReferenceTime = 0;
        void HandleMessages()
        {
            if (MessageQueue.Count > 0)
            {
                Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, MessageQueue[0]);
                Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
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

        PlayerTowingPhase PlayerTowingPhase = PlayerTowingPhase.NotStarted; //"NotStarted","TowApproachingArea","TowDriverServicing","TowDriverFinishedJob"
        FixingPhase FixingPhase = FixingPhase.NotStarted; //,"PlayingAnim","Fixing","Done";
        
        int interval2sec;
        string TeleportingPhase = "none"; // fadingout, teleporting, fadingin


        //Settings

        public static bool Debug = false;

        bool TutorialMessages = true;
        public static float SpawnDistance = 100;

        public static bool TeleportToTowBack = true;
        public static bool DisableWhileInCombat = true;
        public static bool ImmersiveStuckFix = true;
        public static bool AllowCargobob = true;

        public static bool ReverseWhenTowing = true;
        public static bool SafeDriving = true;
        public static bool BurningVehicles = true;
        public static bool BurstTires = true;
        public static bool SmokingEngine = true;
        public static bool DeadEngine = true;
        public static bool PersistentVehicles = true;
        public static bool NotPersistentVehicles = true;
        public static bool DestroyedVehicles = true;
        public static bool VehiclesWithAliveDriver = true;
        public static bool EmergencyVehicles = true;
        public static bool VehiclesWithAliveOwner = true;
        public static bool VehiclesWithDeadDriver = true;
        public static bool VehiclesWithDeadOwner = true;

        public  List<Tow> Dispatched = new List<Tow>();


        int Traffic = 0;

        //Ambient tow
        Vehicle TowVeh;
        Ped TowDriver;
        Vehicle VehicleToTow;

        //Player Tow
        Vehicle PlayerTowVeh;
        Ped PlayerTowDriver;
        Vehicle PlayerVehicle;
        bool IsPlayerBeingServiced = false;
        bool IsDriverFixingVehicle = false;
        int interval = 1000;
        bool InCombat = false;

        string ScriptVersion = "2.2";
        public TowingService()
        {
            LSCustomsVectorToHeading.Add(new Vector3(-1153f, -2001f, 13f), 135f);
            LSCustomsVectorToHeading.Add(new Vector3(1175f, 2641f, 37f), 179f);
            LSCustomsVectorToHeading.Add(new Vector3(-350f, -136f, 38f), 261f);
            LSCustomsVectorToHeading.Add(new Vector3(111f, 6625f, 31f), 44f);

            LSCustomsLocations.Add(new Vector3(-1153f, -2001f, 13f));
            LSCustomsLocations.Add(new Vector3(1175f, 2641f, 37f));
            LSCustomsLocations.Add(new Vector3(-350f, -136f, 38f));
            LSCustomsLocations.Add(new Vector3(111f, 6625f, 31f));

            LoadSettings();

            Tick += OnTick;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            foreach (Vehicle veh in World.GetAllVehicles())
            {
                if (veh.CurrentBlip != null && veh.CurrentBlip.Sprite == BlipSprite.TowTruck)
                {
                    veh.MarkAsNoLongerNeeded();
                }
            }


            CheckDecors();
            SetDecorBool(TSActiveSearch, Game.Player.Character, true);
        }
        void WarnPlayer(string script_name, string title, string message)
        {
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
            Function.Call(Hash._SET_NOTIFICATION_MESSAGE, "CHAR_SOCIAL_CLUB", "CHAR_SOCIAL_CLUB", true, 0, title, script_name);
        }

        void LoadSettings()
        {
            ScriptSettings config = ScriptSettings.Load(@"scripts\Tow Service.ini");



            ActiveSearch = config.GetValue<bool>("GENERAL_SETTINGS", "ActiveSearch", true);
            DisableASWhenCop = config.GetValue<bool>("GENERAL_SETTINGS", "DisableASWhenCop", true);

            SpawnDistance = config.GetValue<float>("GENERAL_SETTINGS", "SpawnDistance", 100);
            TutorialMessages = config.GetValue<bool>("GENERAL_SETTINGS", "TutorialMessages", true);
            TeleportToTowBack = config.GetValue<bool>("GENERAL_SETTINGS", "TeleportToTowBack", true);
            DisableWhileInCombat = config.GetValue<bool>("GENERAL_SETTINGS", "DisableWhileInCombat", false);
            ImmersiveStuckFix = config.GetValue<bool>("GENERAL_SETTINGS", "ImmersiveStuckFix", false);
            ReverseWhenTowing = config.GetValue<bool>("GENERAL_SETTINGS", "ReverseWhenTowing", false);
            SafeDriving = config.GetValue<bool>("GENERAL_SETTINGS", "SafeDriving", false);
            AllowCargobob = config.GetValue<bool>("GENERAL_SETTINGS", "AllowCargobob", false);
            EmergencyVehicles = config.GetValue<bool>("VEHICLE_REQUIREMENTS", "EmergencyVehicles", false);

            PersistentVehicles = config.GetValue<bool>("VEHICLE_REQUIREMENTS", "PersistentVehicles", false);
            NotPersistentVehicles = config.GetValue<bool>("VEHICLE_REQUIREMENTS", "NotPersistentVehicles", false);
            BurningVehicles = config.GetValue<bool>("VEHICLE_REQUIREMENTS", "BurningVehicles", false);

            VehiclesWithAliveDriver = config.GetValue<bool>("VEHICLE_OWNER_STATUS", "VehiclesWithAliveDriver", false);
            VehiclesWithAliveOwner = config.GetValue<bool>("VEHICLE_OWNER_STATUS", "VehiclesWithAliveOwner", false);
            VehiclesWithDeadDriver = config.GetValue<bool>("VEHICLE_OWNER_STATUS", "VehiclesWithDeadDriver", false);
            VehiclesWithDeadOwner = config.GetValue<bool>("VEHICLE_OWNER_STATUS", "VehiclesWithDeadOwner", false);

            BurstTires = config.GetValue<bool>("VEHICLE_DAMAGE_RULES", "BurstTires", false);
            SmokingEngine = config.GetValue<bool>("VEHICLE_DAMAGE_RULES", "SmokingEngine", false);
            DeadEngine = config.GetValue<bool>("VEHICLE_DAMAGE_RULES", "DeadEngine", false);
            DestroyedVehicles = config.GetValue<bool>("VEHICLE_DAMAGE_RULES", "DestroyedVehicles", false);

        }
        public static bool WasCheatStringJustEntered(string cheat)
        {
            return Function.Call<bool>(Hash._0x557E43C447E700A8, Game.GenerateHash(cheat));
        }

        void HandleRopeRemoval()
        {
            if (Ropes.Count > 2)
            {
                Ropes[0].Delete();
                Ropes[1].Delete();
            }
        }

        bool ActiveSearch = true;
        bool DisableASWhenCop = true;

        bool ActiveSearchWasActive = true;
        void OnTick(object sender, EventArgs e)
        {

            if(Traffic>10 && Dispatched.Count>0)
            {
                Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0.2f);

            }
            if (WasCheatStringJustEntered("TSActiveSearch"))
            {
                SetDecorBool(TSActiveSearch, Game.Player.Character, !GetDecorBool(TSActiveSearch, Game.Player.Character));

                if(GetDecorBool(TSActiveSearch, Game.Player.Character)) UI.Notify("Active Search ~g~enabled~w~."); else UI.Notify("Active Search ~o~disabled~w~.");
            }
            if (IsCop(Game.Player.Character))
            {
                if(ActiveSearchWasActive && GetDecorBool(TSActiveSearch, Game.Player.Character))
                {
                    SetDecorBool(TSActiveSearch, Game.Player.Character, false);
                    ActiveSearchWasActive = false;
                    UI.Notify("Towing Service ActiveSearch ~o~disabled~w~.");
                    UI.Notify("As a cop, you'll be the one calling for towtrucks when neccesary.");
                    UI.Notify("Just use the cheat 'call tow' near the target vehicle.");
                }
            }
            else
            {
                if (!ActiveSearchWasActive && !GetDecorBool(TSActiveSearch, Game.Player.Character))
                {
                    SetDecorBool(TSActiveSearch, Game.Player.Character, true);
                    ActiveSearchWasActive = true;
                    UI.Notify("Towing Service ActiveSearch ~g~enabled~w~.");
                }
            }

            if (WasCheatStringJustEntered("call tow"))
            {
                Vehicle v = World.GetClosestVehicle(Game.Player.Character.Position, 5f);

                if (CanWeUse(v))
                {
                    UI.Notify("A tow has been dispatched for the ~y~"+v.FriendlyName+"~w~.");
                    Dispatched.Add(new Tow(v));
                }
            }

            HandleMessages();
            foreach (Tow tow in Dispatched) if (CanWeUse(tow.TowVeh) && tow.AmbientTowingPhase == AmbientTowingPhase.TowFinishedJob && tow.TowVeh.Model.IsHelicopter && tow.TowVeh.HeightAboveGround < 60f) tow.TowVeh.ApplyForce(Vector3.WorldUp*0.7f);
            

                if (Game.GameTime > interval2sec + interval)
            {
                interval2sec = Game.GameTime;

                if (DisableWhileInCombat)
                {
                    InCombat = false;
                    foreach (Ped p in World.GetAllPeds())
                    {
                        if (p.IsInCombat)
                        {
                            InCombat = true;
                        }
                    }
                }


                if (Dispatched.Count > 0)
                {
                    for (int i = 0; i <= Dispatched.Count - 1; i++)
                    {
                        if (!CanWeUse(Dispatched[i].VehicleToTow))
                        {
                            Dispatched[i].CleanTowService();
                            Dispatched.RemoveAt(i);
                            break;
                        }
                        else Dispatched[i].HandleTowing();
                    }
                }

                if((!DisableWhileInCombat || !InCombat) && Dispatched.Count<3)
                {
                    Traffic = 0;
                    foreach (Vehicle veh in World.GetNearbyVehicles(Game.Player.Character.Position, 70f))
                    {
                        Traffic++;
                        if ((DecorExistsOn(TSActiveSearch, Game.Player.Character) && GetDecorBool(TSActiveSearch, Game.Player.Character) && TowingService.IsVehAbandoned(veh)) || (DecorExistsOn(HandledByTow, veh)))
                        {
                            bool CanTow = true;


                            if (GetDecorBool(HandledByTow, veh) || veh == Game.Player.LastVehicle || DecorExistsOn("DontInfluence", veh)) CanTow = false;
                            else
                            {
                                foreach (Tow t in Dispatched)
                                {
                                    if (CanWeUse(t.VehicleToTow) && t.VehicleToTow.Handle == veh.Handle)
                                    {
                                        CanTow = false;
                                        break;
                                    }
                                }
                            }


                            if (CanTow)
                            {
                              if(TowingService.Debug)  UI.Notify("New tow dispatched for " + veh.FriendlyName);
                                Dispatched.Add(new Tow(veh));

                                SetDecorInt(HandledByTow, veh, 1);
                                break;

                            }
                        }
                    }
                }



                if (IsPlayerBeingServiced)
                {
                    if (AnythingWrong())
                    {
                        MessageQueue.Add("~r~Player Towtruck Service cancelled.");
                        CleanPlayerTowService();
                    }
                    else
                    {
                        HandlePlayerTowService();
                    }
                }
            }

            if (!IsPlayerBeingServiced && IsCarBrokenDown(GetLastVehicle(PlayerPed())) && GetLastVehicle(PlayerPed()).IsAlive && PlayerPed().IsSittingInVehicle())
            {
                DisplayHelpTextThisFrame("Hold the Handbrake and press ~INPUT_CONTEXT~ to call the Tow service for ~g~$50~w~.");
                if (Game.IsControlPressed(2, GTA.Control.VehicleHandbrake) && Game.IsControlJustPressed(2, GTA.Control.Context) && !AnythingWrong())
                {
                    if (Game.Player.Money >= 50)
                    {
                        Game.Player.Money = Game.Player.Money - 50;
                        IsPlayerBeingServiced = true;
                        MessageQueue.Add("The Tow service has sent a Tow truck in to your position.");
                        if (TutorialMessages)
                        {
                            MessageQueue.Add("You will be towed to the nearest LS Customs when the Truck arrives.");
                            MessageQueue.Add("Alternatively, you can step out of the car and let the Driver fix it in situ.");
                            MessageQueue.Add("You can cancel the service by holding ~INPUT_JUMP~ and pressing ~INPUT_CONTEXT~ again.");
                        }
                    }
                    else
                    {
                        UI.ShowSubtitle("~r~You don't have enough money.");
                    }
                }
            }
            else
            if (IsPlayerBeingServiced)
            {
                if (Game.IsControlPressed(2, GTA.Control.VehicleHandbrake) && Game.IsControlJustPressed(2, GTA.Control.Context) && Game.Player.Money > 150 && !AnythingWrong())
                {
                    CleanPlayerTowService();
                    MessageQueue.Add("~r~Player Towtruck Service cancelled by user.");
                }
            }
        }

        public static float RoadTravelDistance(Vector3 pos, Vector3 destination)
        {
            return Function.Call<float>(Hash.CALCULATE_TRAVEL_DISTANCE_BETWEEN_POINTS, pos.X, pos.Y, pos.Z, destination.X, destination.Y, destination.Z);

        }
        void OnKeyDown(object sender, KeyEventArgs e)
        {

        }
        void OnKeyUp(object sender, KeyEventArgs e)
        {

        }

        public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
        {
            Vector3 P = x * Vector3.Normalize(B - A) + A;
            return P;
        }
        public enum Nodetype { AnyRoad, Road, Offroad, Water }
        public static Vector3 GenerateSpawnPos(Vector3 desiredPos, Nodetype roadtype, bool sidewalk)
        {

            Vector3 finalpos = Vector3.Zero;
            bool ForceOffroad = false;


            OutputArgument outArgA = new OutputArgument();
            int NodeNumber = 1;
            int type = 0;

            if (roadtype == Nodetype.AnyRoad) type = 1;
            if (roadtype == Nodetype.Road) type = 0;
            if (roadtype == Nodetype.Offroad) { type = 1; ForceOffroad = true; }
            if (roadtype == Nodetype.Water) type = 3;


            int NodeID = Function.Call<int>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, desiredPos.X, desiredPos.Y, desiredPos.Z, NodeNumber, type, 300f, 300f);
            if (ForceOffroad)
            {
                while (!Function.Call<bool>(Hash._GET_IS_SLOW_ROAD_FLAG, NodeID) && NodeNumber < 500)
                {
                    Script.Wait(1);
                    NodeNumber++;
                    Vector3 v = desiredPos.Around(NodeNumber);
                    NodeID = Function.Call<int>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, v.X, v.Y, v.Z, NodeNumber, type, 300f, 300f);
                }
            }
            Function.Call(Hash.GET_VEHICLE_NODE_POSITION, NodeID, outArgA);
            finalpos = outArgA.GetResult<Vector3>();

            if (sidewalk) finalpos = World.GetNextPositionOnSidewalk(finalpos);
            //UI.Notify("Final:" +NodeNumber.ToString());
            return finalpos;
        }
        bool AnythingWrong()
        {
            if (PlayerPed().Health < 1)
            {
                return true;
            }
            if ((CanWeUse(PlayerTowDriver) && !PlayerTowDriver.IsAlive) || ((CanWeUse(PlayerTowVeh) && !PlayerTowVeh.IsAlive)))
            {
                return true;
            }
            if (CanWeUse(PlayerVehicle) && (!PlayerVehicle.IsAlive || !PlayerPed().IsInRangeOf(PlayerVehicle.Position, 50f)))
            {
                return true;
            }
            return false;
        }

        void HandlePlayerTowService()
        {
            switch (PlayerTowingPhase)
            {
                case PlayerTowingPhase.NotStarted:
                    {
                        bool CanContinue = true;
                        if (!CanWeUse(PlayerTowVeh))
                        {
                            CanContinue = false;
                            if (RandomInt(1, 2) == 1)
                            {
                                PlayerTowVeh = World.CreateVehicle("towtruck2", World.GetNextPositionOnStreet(PlayerPed().Position.Around(SpawnDistance), true), 0);
                            }
                            else
                            {
                                PlayerTowVeh = World.CreateVehicle("towtruck", World.GetNextPositionOnStreet(PlayerPed().Position.Around(SpawnDistance), true), 0);
                            }
                            PlayerTowVeh.SirenActive = true;
                            Vector3 playerpos = PlayerPed().Position;
                            Vector3 towpos = PlayerTowVeh.Position;
                            PlayerTowVeh.Heading = Function.Call<float>(Hash.GET_ANGLE_BETWEEN_2D_VECTORS, playerpos.X, playerpos.Y, towpos.X, towpos.Y);
                            SetEntityHeading(PlayerTowVeh);
                            Function.Call(Hash.SET_VEHICLE_FORWARD_SPEED, PlayerTowVeh, 5f);
                            

                        }
                        if (!CanWeUse(PlayerTowDriver))
                        {
                            CanContinue = false;
                            PlayerTowDriver = Function.Call<Ped>(Hash.CREATE_RANDOM_PED_AS_DRIVER, PlayerTowVeh, true);
                            PreparePed(PlayerTowDriver);

                            PlayerTowDriver.AddBlip();
                            PlayerTowDriver.CurrentBlip.Sprite = BlipSprite.TowTruck;

                        }
                        if (!CanWeUse(PlayerVehicle))
                        {
                            CanContinue = false;
                            PlayerVehicle = GetLastVehicle(PlayerPed());
                            PlayerVehicle.LeftIndicatorLightOn = true;
                            PlayerVehicle.RightIndicatorLightOn = true;

                            //Function.Call(Hash.SET_VEHICLE_INDICATOR_LIGHTS, 2, true);
                        }
                        if (CanContinue)
                        {
                            PlayerTowingPhase = PlayerTowingPhase.TowApproachingArea;
                        }
                        return;
                    }
                case PlayerTowingPhase.TowApproachingArea:
                    {
                        if (!PlayerTowVeh.IsInRangeOf(PlayerVehicle.Position, 20f))
                        {
                            if (PlayerTowVeh.IsStopped)
                            {
                                int drivingstyle = 4 + 16 + 32 + 262144;
                                if (SafeDriving)
                                {
                                    drivingstyle = drivingstyle + 1 + 2;
                                }
                                PlayerTowDriver.Task.DriveTo(PlayerTowVeh, PlayerVehicle.Position, 15f, 15f, drivingstyle);
                            }
                        }
                        else
                        {
                            PlayerTowingPhase = PlayerTowingPhase.TowDriverServicing;
                        }
                        return;
                    }
                case PlayerTowingPhase.TowDriverServicing:
                    {
                        if ((PlayerVehicle.GetPedOnSeat(VehicleSeat.Driver).Handle == PlayerPed().Handle) && HandleTeleporting()) // && HandleFixing()
                        {
                            PlayerTowingPhase = PlayerTowingPhase.TowDriverFinishedJob;
                        }
                        else if (HandleFixing())
                        {
                            PlayerTowingPhase = PlayerTowingPhase.TowDriverFinishedJob;
                        }
                        return;
                    }
                case PlayerTowingPhase.TowDriverFinishedJob:
                    {

                        CleanPlayerTowService();
                        return;
                    }
            }
        }

        bool HandleTeleporting()
        {
            switch (TeleportingPhase)
            {
                case "none":
                    {
                        Game.FadeScreenOut(500);
                        TeleportingPhase = "fadingout";
                        return false;
                    }
                case "fadingout":
                    {
                        float dist = 9999f;
                        Vector3 finalvec = Vector3.Zero;
                        foreach (Vector3 vec in LSCustomsLocations)
                        {
                            if (PlayerVehicle.Position.DistanceTo(vec) < dist)
                            {
                                dist = PlayerVehicle.Position.DistanceTo(vec);
                                finalvec = vec;
                            }
                        }

                        PlayerVehicle.Position = finalvec;
                        PlayerVehicle.Heading = LSCustomsVectorToHeading[finalvec];
                        TeleportingPhase = "fadingin";
                        return false;
                    }
                case "fadingin":
                    {
                        Game.FadeScreenIn(500);
                        TeleportingPhase = "none";
                        return true;
                    }
            }

            return true;
        }

        public static Vehicle GetAttachedVehicle(Vehicle carrier, bool DetachIfFound)
        {
            Vehicle Carried = null;
            Vehicle OriginalCarrier = carrier;


            if (carrier.Model.IsCar && carrier.HasBone("attach_female") && carrier.Model != "yosemitexl" && carrier.Model != "ramptruck")
            {
                //ui.notify("Carrier " + carrier.FriendlyName + " has an 'attach_female' bone, looking for trailers");

                if (Function.Call<bool>(Hash.IS_VEHICLE_ATTACHED_TO_TRAILER, carrier))
                {
                    //ui.notify("This carrier has a trailer, getting trailer");
                    Vehicle trailer = null; // GetTrailer(ToCarry);
                    if (trailer == null)
                    {
                        foreach (Vehicle t in World.GetNearbyVehicles(carrier.Position, 30f))
                            if (t.HasBone("attach_male"))
                            {
                                trailer = t;
                                break;
                            }
                    }

                    if (trailer != null)
                    {
                        carrier = trailer;
                        //ui.notify("Trailer found, " + carrier.FriendlyName + "(" + carrier.DisplayName + ")");

                        foreach (Vehicle veh in World.GetNearbyVehicles(carrier.Position, 10f))
                            if (veh != OriginalCarrier && veh != carrier && veh.IsAttachedTo(carrier))
                            {
                                if (DetachIfFound) Detach(carrier, veh);
                                return veh;
                                Carried = veh;
                                //ui.notify("Found ToCarry, " + ToCarry.FriendlyName);
                                break;
                            }
                    }
                    else
                    {
                        //ui.notify("Trailer not found, aborting");
                        return null;
                    }
                }
                else
                {
                    //ui.notify("This carrier doesn't have trailer, aborting");
                    return null;
                }

            }
            else
            {
                //ui.notify("Carrier " + carrier.FriendlyName + " does ~o~NOT~w~ have an 'attach_female' bone, must be a normal car");
                foreach (Vehicle v in World.GetNearbyVehicles(carrier.Position, carrier.Model.GetDimensions().Y))
                {
                    if (v.IsAttachedTo(carrier))
                    {

                        if (DetachIfFound) Detach(carrier, v);

                        return v;
                    }
                }
            }
            return null;
        }

        public static void Detach(Vehicle carrier, Vehicle cargo)
        {
            cargo.Detach();
            UI.Notify("Detaching " + cargo.FriendlyName + " from " + carrier.FriendlyName);

            if (CanWeUse(Game.Player.Character.CurrentVehicle) && carrier == Game.Player.Character.CurrentVehicle)
                if (Game.IsControlPressed(2, GTA.Control.ParachuteTurnLeftOnly))
                {
                    //ui.notify("~o~Left");

                    cargo.Position = carrier.Position - (carrier.RightVector * carrier.Model.GetDimensions().X);
                }
            if (Game.IsControlPressed(2, GTA.Control.ParachuteTurnRightOnly))
            {
                //ui.notify("~o~Right");
                cargo.Position = carrier.Position + (carrier.RightVector * carrier.Model.GetDimensions().X);
            }
            if (Game.IsControlPressed(2, GTA.Control.ParachutePitchDownOnly))
            {
                //ui.notify("~o~Back");

                cargo.Position = carrier.Position + -(carrier.ForwardVector * carrier.Model.GetDimensions().Y);
                // ToCarry.Position = carrier.Position + (carrier.RightVector * carrier.Model.GetDimensions().X);
            }
        }
       public static void Attach(Vehicle carrier, Vehicle ToCarry)
        {
            //Vehicle ToCarry = null; // Game.Player.Character.CurrentVehicle;
            if (!CanWeUse(carrier)) return;
            Vehicle OriginalCarrier = carrier;

            if (!CanWeUse(ToCarry))
            {
                if (carrier.Model.IsCar && carrier.HasBone("attach_female") && carrier.Model != "yosemitexl" && carrier.Model != "ramptruck")
                {
                    //ui.notify("Carrier " + carrier.FriendlyName + " has an 'attach_female' bone, looking for trailers");

                    if (Function.Call<bool>(Hash.IS_VEHICLE_ATTACHED_TO_TRAILER, carrier))
                    {
                        //ui.notify("This carrier has a trailer, getting trailer");
                        Vehicle trailer = null; // GetTrailer(ToCarry);
                        if (trailer == null)
                        {
                            foreach (Vehicle t in World.GetNearbyVehicles(carrier.Position, 30f))
                                if (t.HasBone("attach_male"))
                                {
                                    trailer = t;
                                    break;
                                }
                        }

                        if (trailer != null)
                        {
                            carrier = trailer;
                            //ui.notify("Trailer found, " + carrier.FriendlyName + "(" + carrier.DisplayName + ")");

                            foreach (Vehicle veh in World.GetNearbyVehicles(carrier.Position, 10f))
                                if (veh != OriginalCarrier && veh != carrier)
                                {
                                    ToCarry = veh;
                                    //ui.notify("Found ToCarry, " + ToCarry.FriendlyName);
                                    break;
                                }
                        }
                        else
                        {
                            //ui.notify("Trailer not found, aborting");
                            return;
                        }
                    }
                    else
                    {
                        //ui.notify("This carrier doesn't have trailer, aborting");
                        return;
                    }

                }
                else
                {
                    //ui.notify("Carrier " + carrier.FriendlyName + " does ~o~NOT~w~ have an 'attach_female' bone, must be a normal car");
                    foreach (Vehicle v in World.GetNearbyVehicles(carrier.Position, carrier.Model.GetDimensions().Y))
                    {
                        if (v.IsAttachedTo(carrier))
                        {

                            Detach(carrier, v);

                            //ui.notify("~o~ToCarry already attached, aborting");

                            return;
                        }
                    }

                    if (carrier.Model.IsHelicopter)
                    {
                        foreach (Vehicle veh in World.GetNearbyVehicles(carrier.Position, 30f))
                            if (veh != OriginalCarrier && veh != carrier)
                            {
                                ToCarry = veh;
                                //ui.notify("Found ToCarry, " + ToCarry.FriendlyName);
                                break;
                            }
                    }
                    else
                    {
                        Vector3 back = -(carrier.ForwardVector * carrier.Model.GetDimensions().Y);

                        if (carrier.Model.IsHelicopter) back = -(carrier.UpVector * 30);
                        RaycastResult ray = World.Raycast(carrier.Position, back, 30f, IntersectOptions.Everything, carrier);


                        if (!ray.DitHitEntity) ray = World.Raycast(carrier.Position - carrier.UpVector, back, 30f, IntersectOptions.Everything, carrier);

                        if (ray.DitHitEntity && ray.HitEntity.Model.IsVehicle)
                        {
                            ToCarry = ray.HitEntity as Vehicle;
                            //ui.notify("Carrier: " + carrier.FriendlyName);
                            //ui.notify("ToCarry: " + ToCarry.FriendlyName);

                        }
                        else
                        {
                            //ui.notify("No vehicle found behind yours.");
                            return;
                        }
                    }
                }


            }


            if (!CanWeUse(ToCarry))
            {
                //ui.notify("ToCarry not found, aborting");
                return;
            }

          if(Debug)  UI.Notify("Tow attaching " + ToCarry.FriendlyName + " to " + OriginalCarrier.FriendlyName);
            if (ToCarry.IsAttached())
            {

                return;
            }

            Vector3 CarrierOffset = new Vector3(0, -(carrier.Model.GetDimensions().Y / 2f), 0f);// new Vector3(0, -1.4f, 3f + (ToCarry.Model.GetDimensions().Z * 0.35f));
            Vector3 truckoffset = new Vector3(0f, (ToCarry.Model.GetDimensions().Y / 2f), -ToCarry.HeightAboveGround);
            if (!ToCarry.IsOnAllWheels)
            {
                truckoffset = new Vector3(0f, (ToCarry.Model.GetDimensions().Y / 2f), -(ToCarry.Model.GetDimensions().Z * 0.35f));
            }

            float pitch = 0f;
            bool Collision = true;

            bool NotMadeToCarry = true;
            if (carrier.Model == "mule4" || carrier.Model == "mule5")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, 1.5f, -0.05f);
            }
            if (carrier.Model == "flatbed")
            {
                NotMadeToCarry = false;
                float farback = 0f;

                CarrierOffset = new Vector3(0, 0.5f + farback, 0.4f); // v.Model.GetDimensions().Z * 0.5f  //
            }

            if (carrier.Model == "barracks4" || carrier.Model == "sturdy2")
            {
                NotMadeToCarry = false;

                CarrierOffset = new Vector3(0, 0.9f, 0.88f); // v.Model.GetDimensions().Z * 0.5f  //
            }
            if (carrier.Model == "ramptruck2" || carrier.Model == "ramptruck")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, -1f, 1f); // v.Model.GetDimensions().Z * 0.5f
                pitch = 5;
            }
            if (carrier.Model == "wastelander")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, 1.5f, 1f); // v.Model.GetDimensions().Z * 0.5f
                                                          //pitch = 5;
            }
            if (carrier.Model == "SKYLIFT")
            {
                Collision = false;
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, -2f, -(ToCarry.Model.GetDimensions().Z / 2) + 0.5f); // v.Model.GetDimensions().Z * 0.5f
                truckoffset = new Vector3(0f, 0f, (ToCarry.Model.GetDimensions().Z * 0.4f));
            }
            //ui.notify("Calculated offsets");
            //ui.notify("Is NOT normal vehicle, attaching");


            if (carrier.Model == "wastelander")
            {
                Collision = false;
                NotMadeToCarry = false;
            }
            if (carrier.Model == "freighttrailer")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, 8f, -1.2f);
            }
            if (carrier.Model == "trflat")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(0, 3, 0.5f);
            }
            if (carrier.Model == "armytrailer")
            {
                CarrierOffset = new Vector3(0, 0, -1.2f);
                NotMadeToCarry = false;
            }
            if (carrier.Model == "cartrailer" || carrier.Model == "cartrailer2")
            {
                NotMadeToCarry = false;
                CarrierOffset = new Vector3(2.3f, -2.5f, -0.4f);
            }


            if (NotMadeToCarry) truckoffset = truckoffset = new Vector3(0f, (ToCarry.Model.GetDimensions().Y / 2f), 0f);
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY_PHYSICALLY, ToCarry, carrier, 0, 0, CarrierOffset.X, CarrierOffset.Y, CarrierOffset.Z, truckoffset.X, truckoffset.Y, truckoffset.Z, pitch, 0f, 0f, 5000f, true, true, Collision, false, 2); //+ (v.Model.GetDimensions().Y/2f)


            /* Rope system
         v.Detach();

         Script.Wait(1000);
         Vector3 dynamicoffset = v.GetOffsetFromWorldCoords(carrier.Position);
         //Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY_PHYSICALLY, v, carrier, 0, 0, 0f, 2f, 0f, dynamicoffset.X, dynamicoffset.Y+2f, dynamicoffset.Z, v.Rotation.X, 0f, 0f, 1000f, false, true, true, true, 2); //+ (v.Model.GetDimensions().Y/2f)

         float yoffset = v.Model.GetDimensions().Y/2;
         Rope rope = World.AddRope(RopeType.Normal, v.Position, v.Rotation, v.Position.DistanceTo(carrier.Position), 1f, false);
         rope.ActivatePhysics();
         rope.AttachEntities(v, v.Position+(v.ForwardVector* yoffset), carrier, v.Position + (v.ForwardVector * yoffset) - v.UpVector, (v.Position + (v.ForwardVector * yoffset)).DistanceTo(v.Position + (v.ForwardVector * yoffset) - v.UpVector));
         TrailerRopes.Add(rope);

          yoffset = v.Model.GetDimensions().Y / 2;
          rope = World.AddRope(RopeType.Normal, v.Position, v.Rotation, v.Position.DistanceTo(carrier.Position), 1f, false);
         rope.ActivatePhysics();
         rope.AttachEntities(v, v.Position - (v.ForwardVector * yoffset), carrier, v.Position - (v.ForwardVector * yoffset) - v.UpVector, (v.Position - (v.ForwardVector * yoffset)).DistanceTo(v.Position - (v.ForwardVector * yoffset) - v.UpVector));
         TrailerRopes.Add(rope);
         */
        }
        protected override void Dispose(bool dispose)
        {
            WarnPlayer("~b~Towing Service " + ScriptVersion, "SCRIPT RESET", "~g~Towing Service has been cleaned and reset succesfully.");
            //UI.Notify("Tow script ~g~cleaned~w~.");
            foreach (Rope rope in Ropes) rope.Delete();

            foreach (Tow t in Dispatched) t.CleanTowService();
            if (CanWeUse(TowVeh))
            {
                TowVeh.IsPersistent = false;
                if (TowVeh.CurrentBlip != null)
                {
                    TowVeh.CurrentBlip.Remove();
                }
            }
            if (CanWeUse(VehicleToTow))
            {
                if (VehicleToTow.CurrentBlip != null)
                {
                    VehicleToTow.CurrentBlip.Remove();
                }
                VehicleToTow.IsPersistent = false;
                VehicleToTow.LeftIndicatorLightOn = false;
                VehicleToTow.RightIndicatorLightOn = false;
            }
            if (CanWeUse(TowDriver))
            {
                TowDriver.IsPersistent = false;
                TowDriver.Task.ClearAll();
            }
            base.Dispose(dispose);
        }
        bool HandleFixing()
        {

            IsDriverFixingVehicle = true;
            switch (FixingPhase)
            {
                case FixingPhase.NotStarted:
                    {

                        if (!PlayerTowDriver.IsInRangeOf(PlayerVehicle.Position, 3f))
                        {
                            Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, PlayerTowDriver.Handle, "GENERIC_HI", "SPEECH_PARAMS_STANDARD");

                            Vector3 pos = PlayerVehicle.Position;
                            Function.Call(Hash.TASK_FOLLOW_NAV_MESH_TO_COORD, PlayerTowDriver, pos.X, pos.Y, pos.Z, 1.0f, -1, 0.0f, 0, 0.0f);
                        }
                        else
                        {
                            FixingPhase = FixingPhase.PlayingAnim;
                        }
                        return false;
                    }
                case FixingPhase.PlayingAnim:
                    {
                        if (IsCarBrokenDown(PlayerVehicle))
                        {
                            int cost = 50;
                            PlayerTowDriver.Task.ClearAll();
                            Vector3 faceto = PlayerVehicle.Position;
                            TaskSequence RaceSequence = new TaskSequence();
                            RaceSequence.AddTask.TurnTo(faceto, 2000);
                            RaceSequence.AddTask.PlayAnimation("amb@prop_human_parking_meter@female@idle_a", "idle_b_female", 1.0f, 2000, true, 1.0f);
                            RaceSequence.Close();
                            PlayerTowDriver.Task.PerformSequence(RaceSequence);
                            RaceSequence.Dispose();
                            if (PlayerVehicle.EngineHealth < 1000f)
                            {
                                cost = cost + 200;
                            }
                            for (int i = 0; i <= 10; i++)
                            {
                                if (PlayerVehicle.IsTireBurst(i))
                                {
                                    cost = cost + 50;
                                }
                            }
                            if (PlayerVehicle.PetrolTankHealth < 500)
                            {
                                cost = cost + 100;
                            }

                            if (Game.Player.Money > cost)
                            {
                                Game.Player.Money = Game.Player.Money - cost;

                                if (PlayerVehicle.EngineHealth < 1000f)
                                {
                                    PlayerVehicle.EngineHealth = 1000f;
                                }
                                for (int i = 0; i <= 10; i++)
                                {
                                    if (PlayerVehicle.IsTireBurst(i))
                                    {
                                        PlayerVehicle.FixTire(i);
                                    }
                                }
                                if (PlayerVehicle.PetrolTankHealth < 500)
                                {
                                    PlayerVehicle.PetrolTankHealth = 1000f;
                                }
                                Function.Call(GTA.Native.Hash.SET_VEHICLE_DEFORMATION_FIXED, PlayerVehicle);
                                MessageQueue.Add("Total cost: ~g~$" + cost + "~w~.");
                            }
                            else
                            {
                                MessageQueue.Add("~r~You don't have enough money to fix your car. ~w~(Cost ~g~$" + cost + "~w~.)");
                                FixingPhase = FixingPhase.NotStarted;
                                IsDriverFixingVehicle = false;

                                CleanPlayerTowService();
                            }
                        }
                        else
                        {
                            FixingPhase = FixingPhase.Fixing;
                        }
                        return false;
                    }
                case FixingPhase.Fixing:
                    {
                        FixingPhase = FixingPhase.Done;
                        return false;
                    }
                case FixingPhase.Done:
                    {
                        Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, PlayerTowDriver.Handle, "GENERIC_bye", "SPEECH_PARAMS_STANDARD");
                        IsDriverFixingVehicle = false;
                        FixingPhase = FixingPhase.NotStarted;
                        return true;
                    }
            }
            return true;
        }


        public static int RandomInt(int min, int max)
        {
            max++;
            return Function.Call<int>(GTA.Native.Hash.GET_RANDOM_INT_IN_RANGE, min, max);
        }

        bool IsInnacessibleByRoad(Vector3 pos)
        {
            return (World.GetNextPositionOnStreet(pos, false).DistanceTo(pos) < 50f && Math.Abs(World.GetNextPositionOnStreet(pos, false).Z - pos.Z) < 5f);
        }
        public static bool TooManyVehiclesAround(Vector3 pos, int max)
        {
            int num = 0;
            foreach (Vehicle veh in World.GetNearbyVehicles(pos, 20f))
            {
                num++;
            }
            if (num >= max)
            {
                return true;
            }
            return false;
        }
        public static void TempAction(Ped ped, Vehicle veh, int action, int time)
        {
            Vector3 pos = veh.Position;


            //Util.DisplayHelpTextThisFrame("attempting burnout");


            //ped.Task.DriveTo(veh, pos, 10, 30f);

            TaskSequence TempSequence = new TaskSequence();

            Function.Call(Hash.TASK_VEHICLE_TEMP_ACTION, 0, veh, action, time);
            Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, veh, pos.X, pos.Y, pos.Z, 200f, 4194304, 250f);

            TempSequence.Close();
            ped.Task.PerformSequence(TempSequence);
            TempSequence.Dispose();
            /*
*/

        }
        public static  bool IsTooBig(Vehicle veh, float maxlength, float maxwidth)
        {

            return (veh.Model.GetDimensions().X > maxwidth || veh.Model.GetDimensions().Y > maxlength);
        }
        public static void PreparePed(Ped ped)
        {
            ped.IsPersistent = true;
            ped.AlwaysKeepTask = true;
            ped.BlockPermanentEvents = true;
            Function.Call(GTA.Native.Hash.SET_DRIVER_ABILITY, ped, 100f);
            Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
            ped.Weapons.Give(WeaponHash.Bat, -1, false, true);
        }

        void CleanPlayerTowService()
        {
            IsPlayerBeingServiced = false;
            FixingPhase = FixingPhase.NotStarted;
            PlayerTowVeh.SirenActive = false;

            PlayerVehicle.LeftIndicatorLightOn = false;
            PlayerVehicle.RightIndicatorLightOn = false;
            PlayerTowVeh.MarkAsNoLongerNeeded();
            PlayerTowVeh = null;
            PlayerTowDriver.MarkAsNoLongerNeeded();
            PlayerTowDriver = null;
            PlayerVehicle = null;
            PlayerTowingPhase = PlayerTowingPhase.NotStarted;
        }
        //Utility functions

        public static bool CanWeUse(Entity entity)
        {
            return entity != null && entity.Exists();
        }
        public static bool IsTireBurst(Vehicle veh, int i)
        {
            return Function.Call<bool>(GTA.Native.Hash.IS_VEHICLE_TYRE_BURST, veh, i, false);
        }
        public static bool IsCopVehicle(Vehicle veh)
        {
            Vector3 Location = veh.Position;
            float Range = 1f;
            return Function.Call<bool>(Hash.IS_COP_VEHICLE_IN_AREA_3D, Location.X - Range, Location.Y - Range, Location.Z - Range, Location.X + Range, Location.Y + Range, Location.Z + Range);
        }
        public static bool IsVehAbandoned(Vehicle veh)
        {
            if (!veh.IsStopped) return false;
            if (!EmergencyVehicles && (veh.ClassType == VehicleClass.Emergency || IsCopVehicle(veh))) return false;
            if (veh.IsStopped && !veh.IsAttached() && !GetLastDriver(veh).IsPlayer && GetLastVehicle(PlayerPed()).Handle != veh.Handle)
            {

                bool IsSuitable = false;

                if (!BurningVehicles && veh.IsOnFire)
                {
                    return false;
                }
                if (PersistentVehicles && !veh.IsPersistent)
                {
                    IsSuitable = true;
                }
                if (NotPersistentVehicles && !veh.IsPersistent)
                {
                    IsSuitable = true;
                }
                if (IsSuitable)
                {
                    bool CanCheck = false;

                    ///OWNER LIFES
                    if (CanWeUse(GetLastDriver(veh)))
                    {
                        Ped owner = GetLastDriver(veh);
                        if ((VehiclesWithAliveOwner && owner.IsAlive) || (VehiclesWithDeadOwner && !owner.IsAlive))
                        {
                            if (Debug) UI.Notify("DEAD/ALIVE OWNER ALLOWED");
                            CanCheck = true;
                            if (VehiclesWithDeadOwner && !owner.IsAlive) return true;
                        }
                    }
                    else
                    {
                        CanCheck = true;
                    }
                    ///DRIVER LIFES
                    if (CanWeUse(veh.GetPedOnSeat(VehicleSeat.Driver)))
                    {
                        Ped driver = veh.GetPedOnSeat(VehicleSeat.Driver);
                        if ((VehiclesWithAliveDriver && driver.IsAlive) || (VehiclesWithDeadDriver && !driver.IsAlive))
                        {
                            if (Debug) UI.Notify("DEAD/ALIVE DRIVER ALLOWED");
                            CanCheck = true;
                            if (VehiclesWithDeadDriver && !driver.IsAlive) return true;
                        }
                    }
                    else
                    {
                        CanCheck = true;
                    }

                    if (CanCheck)
                    {
                        if (SmokingEngine && veh.EngineHealth < 300 && veh.EngineHealth > 0)
                        {
                            return true;
                        }
                        if (DeadEngine && veh.EngineHealth < 1)
                        {
                            return true;
                        }
                        if (DestroyedVehicles && !veh.IsAlive)
                        {
                            return true;
                        }
                        if (BurstTires)
                        {
                            for (int i = 0; i <= 10; i++)
                            {
                                if (IsTireBurst(veh, i))
                                {
                                    return true;
                                }
                            }
                        }
                        //if(veh.LeftHeadLightBroken || veh.RightHeadLightBroken) return true;
                    }
                }
            }
            return false;
        }


       static public bool  IsCarBrokenDown(Vehicle veh)
        {
            if (veh.IsStopped && !veh.IsAttached())
            {
                if (veh.EngineHealth < 300)
                {
                    return true;
                }
                for (int i = 0; i <= 10; i++)
                {
                    if (IsTireBurst(veh, i))
                    {
                        return true;
                    }
                }
                /*
                if (veh.BodyHealth < 700 && veh.BodyHealth > 10)
                {
                    return true;
                }*/
                if (veh.PetrolTankHealth < 650)
                {
                    return true;
                }

            }
            return false;
        }

        static public Ped GetLastDriver(Vehicle veh)
        {
            return Function.Call<Ped>(GTA.Native.Hash.GET_LAST_PED_IN_VEHICLE_SEAT, veh, -1);
        }

        static public Vehicle GetLastVehicle(Ped ped)
        {
            Vehicle vehicle = null;
            vehicle = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, ped, true);
            if (vehicle == null)
            {
                vehicle = GTA.Native.Function.Call<Vehicle>(GTA.Native.Hash.GET_VEHICLE_PED_IS_IN, ped, false);
            }
            return vehicle;
        }



        static public Ped PlayerPed()
        {
            return Game.Player.Character;
        }
        static public void DisplayHelpTextThisFrame(string text)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
        }

        static public void SetEntityHeading(Entity E)
        {
            OutputArgument outArgA = new OutputArgument();
            OutputArgument outArgB = new OutputArgument();


            if (Function.Call<bool>(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, E.Position.X, E.Position.Y, E.Position.Z, outArgA, outArgB, 1, 1077936128, 0))
            {
                E.Heading = outArgB.GetResult<float>(); //getting heading if the native returns true
            }
        }


        static public void MoveEntitytoNearestRoad(Entity E)
        {
            OutputArgument outArgA = new OutputArgument();
            OutputArgument outArgB = new OutputArgument();


            if (Function.Call<bool>(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, E.Position.X, E.Position.Y, E.Position.Z, outArgA, outArgB, 1, 1077936128, 0))
            {
                E.Position = outArgA.GetResult<Vector3>(); //Get position
                E.Heading = outArgB.GetResult<float>(); //getting heading
            }
        }



        public unsafe static byte* FindPattern(string pattern, string mask)
        {
            ProcessModule module = Process.GetCurrentProcess().MainModule;

            ulong address = (ulong)module.BaseAddress.ToInt64();
            ulong endAddress = address + (ulong)module.ModuleMemorySize;

            for (; address < endAddress; address++)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (mask[i] != '?' && ((byte*)address)[i] != pattern[i])
                    {
                        break;
                    }
                    else if (i + 1 == pattern.Length)
                    {
                        return (byte*)address;
                    }
                }
            }

            return null;
        }
        void UnlockDecorator()
        {

            unsafe
            {
                IntPtr addr = (IntPtr)FindPattern("\x40\x53\x48\x83\xEC\x20\x80\x3D\x00\x00\x00\x00\x00\x8B\xDA\x75\x29",
                                "xxxxxxxx????xxxxx");
                if (addr != IntPtr.Zero)
                {
                    byte* g_bIsDecorRegisterLockedPtr = (byte*)(addr + *(int*)(addr + 8) + 13);
                    *g_bIsDecorRegisterLockedPtr = 0;
                }

            }
        }
        void LockDecotator()
        {
            unsafe
            {
                IntPtr addr = (IntPtr)FindPattern("\x40\x53\x48\x83\xEC\x20\x80\x3D\x00\x00\x00\x00\x00\x8B\xDA\x75\x29",
                                "xxxxxxxx????xxxxx");
                if (addr != IntPtr.Zero)
                {
                    byte* g_bIsDecorRegisterLockedPtr = (byte*)(addr + *(int*)(addr + 8) + 13);
                    *g_bIsDecorRegisterLockedPtr = 1;
                }

            }
        }


        public static bool DecorExistsOn(string decor, Entity e)
        {
            if (!CanWeUse(e)) return false;
            return Function.Call<bool>(Hash.DECOR_EXIST_ON, e, decor);
        }

        public static float GetDecorFloat(string decor, Entity e)
        {
            if (!CanWeUse(e)) return -2;
            return Function.Call<float>(Hash._DECOR_GET_FLOAT, e, decor);
        }

        public static int GetDecorInt(string decor, Entity e)
        {
            if (!CanWeUse(e)) return -2;
            return Function.Call<int>(Hash.DECOR_GET_INT, e, decor);
        }
        public static bool GetDecorBool(string decor, Entity e)
        {
            if (!CanWeUse(e)) return false;
            return Function.Call<bool>(Hash.DECOR_GET_BOOL, e, decor);
        }

        public static void SetDecorBool(string decor, Entity e, bool i)
        {
            if (!CanWeUse(e)) return;
            Function.Call(Hash.DECOR_SET_BOOL, e, decor, i);
        }
        public static void SetDecorInt(string decor, Entity e, int i)
        {
            if (!CanWeUse(e)) return;
            Function.Call(Hash.DECOR_SET_INT, e, decor, i);
        }

       public static void SetDecorFloat(string decor, Entity e, float i)
        {
            if (!CanWeUse(e)) return;
            Function.Call(Hash._DECOR_SET_FLOAT, e, decor, i);
        }


        public static bool IsCop(Ped ped)
        {
            if ((Function.Call<int>(Hash.GET_PED_TYPE, ped) == 6 || Function.Call<int>(Hash.GET_PED_TYPE, ped) == 27 || Function.Call<int>(Hash.GET_PED_TYPE, ped) == 29)) return true;
            return false;
        }

        public static string TSActiveSearch = "TSActiveSearch";
        public static string HandledByTow = "HandledByTow";
        public static string DontInfluence = "DontInfluence";
        bool CheckDecors()
        {
            bool AllGood = true;

            if (!Function.Call<bool>(Hash.DECOR_IS_REGISTERED_AS_TYPE, TSActiveSearch, 2))
            {
                AllGood = false;
                UnlockDecorator();
                // Script.Wait(20);
                Function.Call(Hash.DECOR_REGISTER, TSActiveSearch, 2);
            }
            if (!Function.Call<bool>(Hash.DECOR_IS_REGISTERED_AS_TYPE, DontInfluence, 2))
            {
                AllGood = false;
                UnlockDecorator();
               // Script.Wait(20);
                Function.Call(Hash.DECOR_REGISTER, DontInfluence, 2);
            }
            if (!Function.Call<bool>(Hash.DECOR_IS_REGISTERED_AS_TYPE, HandledByTow, 3))
            {
                AllGood = false;
                UnlockDecorator();
              //  Script.Wait(20);
                Function.Call(Hash.DECOR_REGISTER, HandledByTow, 3);               
            }


            if (!AllGood) LockDecotator();
            return AllGood;
        }
    }
}