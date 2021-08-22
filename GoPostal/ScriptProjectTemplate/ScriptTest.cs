using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class GoPostalDelivery : Script
{
    List<Vector3> GoPostalOffices = new List<Vector3>
    {
        new Vector3(-259,-841,31)

    };
    List<Vector3> DeliveryPoints = new List<Vector3>
    {

    };
    List<Blip> DeliveryPointBlips = new List<Blip>
    {

    };
    public enum MissionState { NoStarted, PickingDelivery, Delivering, MissionFailed, Ending }

    string ScriptName = "Go Postal Delivery";
    string ScriptVer = "1.0";
    Vehicle DeliveryVeh;

    MissionState MissionPhase = MissionState.NoStarted;
    int DayLimit = Function.Call<int>(GTA.Native.Hash.GET_CLOCK_DAY_OF_MONTH);
    Vector3 Start;
    Vector3 End;
    Blip postalblip;

    int GameTimeRef = 0;
    int Interval = 500;
    int Reward = 0;
    int Packages = 0;
    int TimeLimit = 0;
    int VehicleHealthRef;
    public GoPostalDelivery()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        postalblip = World.CreateBlip(GoPostalOffices[0]);
        postalblip.Sprite = BlipSprite.DollarSign;
        postalblip.Color = BlipColor.Green;
        postalblip.Name = "Go Postal delivery";
    }

    void CleanEverything()
    {

        if(DeliveryPoints.Count>0)
        {
            for (int i = 0; i < DeliveryPoints.Count; i++)
            {
                DeliveryPointBlips[i].Remove();
            }
        }

        MissionPhase = MissionState.NoStarted;
    }
    void OnTick(object sender, EventArgs e)
    {
        Util.HandleMessages();


        if (MissionPhase != MissionState.NoStarted)
        {
            foreach (Vector3 pos in DeliveryPoints)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, pos + new Vector3(0, 0, -1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(5, 5, 2), Color.Yellow);
            }
            if (!Util.CanWeUse(DeliveryVeh) || DeliveryVeh.IsDead || Game.Player.IsDead)
            {
                UI.Notify("Delivery Mission cancelled.");
                CleanEverything();
                return;
            }
            
            Util.DisplayHelpTextThisFrame("Packages: "+ Packages +"~n~Reward: ~g~$~w~"+Reward +" (x"+Packages+")~n~Time Limit: "+((TimeLimit-Game.GameTime)/1000+"s ~n~Destinations: "+DeliveryPoints.Count+""));
        }
        else
        {
            //Show triggers
            foreach (Vector3 pos in GoPostalOffices)
            {
                World.DrawMarker(MarkerType.UpsideDownCone, pos + new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.Yellow);
            }

            //Trigger to start the mission
            if (Game.Player.Character.Position.DistanceTo(GoPostalOffices[0]) < 30 && !Game.Player.Character.IsOnFoot)
            {
                Util.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to start this Go Postal delivery mission.");
                if(Util.CanWeUse(DeliveryVeh) && Game.IsControlJustPressed(2, GTA.Control.Context))
                {
                Start = Util.GenerateSpawnPos(Game.Player.Character.Position.Around(100), Util.Nodetype.AnyRoad, true);
                UI.Notify("Deliver the goods to all the marked locations. ~r~Don't damage them.");

                MissionPhase = MissionState.PickingDelivery;

                
                //Calculate how much packages can the current vehicle hold
                int Length = (int)DeliveryVeh.Model.GetDimensions().Y;
                int Width = (int)DeliveryVeh.Model.GetDimensions().X;
                int Height = (int)DeliveryVeh.Model.GetDimensions().Z;

                Packages = Length + Height + Width;
                if (Packages < 1) Packages = 1;

                int Dist = Packages * 200;


                int Number = Util.GetRandomInt(3, 7);
                int MaxDistance = 600;
                if (DeliveryVeh.Model.IsBicycle)
                {
                    Number = Util.GetRandomInt(4, 10);
                    MaxDistance = 200;
                }

                for (int _ = 0; _ < Number; _++)
                {
                        Script.Wait(500);
                    Vector3 Position = Vector3.Zero;
                    int Tries = 0;
                    while (Position == Vector3.Zero && Tries < 10) { Position = Util.GenerateSpawnPos(Game.Player.Character.Position.Around(Util.GetRandomInt(MaxDistance/2, MaxDistance)), Util.Nodetype.AnyRoad, true); Tries++; }

                    if (Position == Vector3.Zero)
                    {
                        CleanEverything();
                        Util.WarnPlayer(ScriptName, "Error", "Error getting the delivery points. Try again.");
                    }
                    else
                    {
                        Blip Destination = World.CreateBlip(Start);
                            Destination.Sprite = BlipSprite.Waypoint;
                        Destination.Color = BlipColor.Yellow;
                        Destination.Position = Position;
                        DeliveryPointBlips.Add(Destination);
                        DeliveryPoints.Add(Position);
                    }
                    int TimeAdded = (((int)Game.Player.Character.Position.DistanceTo(GetCloserPos(DeliveryPoints, Game.Player.Character.Position))) * 104); // 1,4% of the closer DeliveryPoint's distance
                    TimeLimit = Game.GameTime + TimeAdded;
                }
                Reward = 50;
                }
            }
        }

        //if (MissionPhase == MissionState.PickingDelivery) World.DrawMarker(MarkerType.UpsideDownCone, Start + new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.Yellow);
        //if (MissionPhase == MissionState.Delivering) World.DrawMarker(MarkerType.UpsideDownCone, End + new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.Yellow);

        if (GameTimeRef < Game.GameTime)
        {
            GameTimeRef = Game.GameTime + Interval;
            if (Util.CanWeUse(Game.Player.Character.CurrentVehicle)) DeliveryVeh = Game.Player.Character.CurrentVehicle;
            

            switch (MissionPhase)
            {
                case MissionState.NoStarted:
                    {
                        break;
                    }
                case MissionState.PickingDelivery:
                    {

                        if (DeliveryVeh.Speed>2f)
                        {
                            MissionPhase = MissionState.Delivering;                            
                            VehicleHealthRef = (int)DeliveryVeh.BodyHealth;
                        }
                        break;
                    }
                case MissionState.Delivering:
                    {
                        
                        if(TimeLimit<Game.GameTime || Reward == 0)
                        {
                            UI.Notify("~r~Mission failed.");
                            if (DeliveryVeh.Speed > 0.1f) DeliveryVeh.HandbrakeOn = true; else MissionPhase = MissionState.MissionFailed;
                            break;
                        }
                        for (int i = 0; i < DeliveryPoints.Count; i++)
                        {
                            if (DeliveryVeh.Position.DistanceTo(DeliveryPoints[i]) < 10f && DeliveryVeh.Speed < 0.1)
                            {
                                DeliveryPointBlips[i].Remove();
                                DeliveryPointBlips.RemoveAt(i);
                                DeliveryPoints.RemoveAt(i);
                                UI.Notify("One delivery done!");
                                int TimeAdded = ((int)Game.Player.Character.Position.DistanceTo(GetCloserPos(DeliveryPoints, Game.Player.Character.Position))) * 100; // 10% of the closer DeliveryPoint's distance
                                TimeLimit +=  TimeAdded;
                            }
                        }

                        if (VehicleHealthRef > DeliveryVeh.BodyHealth) { Reward -= (VehicleHealthRef - (int)DeliveryVeh.BodyHealth); VehicleHealthRef = (int)DeliveryVeh.BodyHealth;}

                        if (Reward < 1) Reward = 1;

                        if (DeliveryPoints.Count == 0) MissionPhase = MissionState.Ending;
                        break;
                    }
                case MissionState.Ending:
                    {
                        Game.Player.Money += (Reward*Packages);
                        UI.Notify("All deliveries done.");
                        MissionPhase = MissionState.NoStarted;

                        Start = Vector3.Zero;
                        End = Vector3.Zero;
                        CleanEverything();

                        DeliveryVeh.Position = World.GetNextPositionOnStreet(GoPostalOffices[0]);
                        break;
                    }
                case MissionState.MissionFailed:
                    {
                        DeliveryVeh.HandbrakeOn = false;
                        DeliveryVeh.Position = World.GetNextPositionOnStreet(GoPostalOffices[0]);
                        
                        if (Game.Player.Character.IsOnFoot) Game.Player.Character.SetIntoVehicle(DeliveryVeh, VehicleSeat.Driver);
                        CleanEverything();
                        break;
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
    Vector3 GetCloserPos(List<Vector3>posList, Vector3 pos)
    {
        Vector3 finalpos=Vector3.Zero;
        foreach (Vector3 candidate in posList)
        {
            if (candidate.DistanceTo(pos) < finalpos.DistanceTo(pos)) finalpos = candidate;
        }
        return finalpos;
    }


    protected override void Dispose(bool dispose)
    {
        postalblip.Remove();

        CleanEverything();
            base.Dispose(dispose);
    }


}
