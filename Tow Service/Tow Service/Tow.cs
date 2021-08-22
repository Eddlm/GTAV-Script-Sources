using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;

namespace Tow_Service
{
   public  class Tow
    {
        public AmbientTowingPhase AmbientTowingPhase = AmbientTowingPhase.TowApproachingArea;
        public  Vehicle VehicleToTow = null;
        public Ped TowDriver = null;
        public Vehicle TowVeh = null;
        int TowType = 0;
        int StuckPatience = 0;
        public Tow(Vehicle target)
        {
            
            VehicleToTow = target;

            Vector3 spawnpos =TowingService.GenerateSpawnPos(VehicleToTow.Position.Around(TowingService.SpawnDistance), TowingService.Nodetype.Road, false);

            int p = 0;
            while (p < 20 && TowingService.RoadTravelDistance(spawnpos, VehicleToTow.Position) < TowingService.SpawnDistance/2)
            {
                p++;
                spawnpos= TowingService.GenerateSpawnPos(VehicleToTow.Position.Around(TowingService.SpawnDistance), TowingService.Nodetype.Road, false);
                Script.Wait(0);

            }

            Model towmodel = "flatbed";
            if (!TowingService.IsTooBig(VehicleToTow, 7f, 5f) && !TowingService.TooManyVehiclesAround(VehicleToTow.Position, 5) && (World.GetNextPositionOnStreet(VehicleToTow.Position, false).DistanceTo(VehicleToTow.Position) < 50f && Math.Abs(World.GetNextPositionOnStreet(VehicleToTow.Position, false).Z - VehicleToTow.Position.Z) < 5f))//TowingService.TooManyVehiclesAround(VehicleToTow.Position, 100) || !Function.Call<bool>(Hash.IS_BIG_VEHICLE, TowVeh)
            {
                 towmodel = TowingService.NormalTowtrucks[TowingService.RandomInt(0, TowingService.NormalTowtrucks.Count - 1)];
                while (!towmodel.IsValid)towmodel = TowingService.NormalTowtrucks[TowingService.RandomInt(0, TowingService.NormalTowtrucks.Count - 1)];


                if (!VehicleToTow.IsAlive || VehicleToTow.Model.IsBicycle || VehicleToTow.Model.IsBike || VehicleToTow.Model.IsBoat || VehicleToTow.Model.IsHelicopter || VehicleToTow.Model.IsPlane)
                {
                    TowType = 1;
                    towmodel = TowingService.BigTowtrucks[TowingService.RandomInt(0, TowingService.BigTowtrucks.Count - 1)];
                    while (!towmodel.IsValid) towmodel = TowingService.BigTowtrucks[TowingService.RandomInt(0, TowingService.BigTowtrucks.Count - 1)];
                }


                    TowVeh = World.CreateVehicle(towmodel, spawnpos, 0);

            }
            else
            {
                if (TowingService.AllowCargobob)
                {
                    TowType = 2;
                    towmodel = TowingService.Helicopters[TowingService.RandomInt(0, TowingService.Helicopters.Count - 1)];
                    while (!towmodel.IsValid) towmodel = TowingService.Helicopters[TowingService.RandomInt(0, TowingService.Helicopters.Count - 1)];

                    if (TowingService.Debug) UI.Notify("USING CARGOBOB");
                    TowVeh = World.CreateVehicle(towmodel, World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(TowingService.SpawnDistance*2), true), 0);
                    TowVeh.Position = TowVeh.Position + (TowVeh.UpVector * 50);
                    Function.Call(Hash._0x7BEB0C7A235F6F3B, TowVeh);
                }
                else
                {
                    if (TowingService.Debug) UI.Notify("BOB NOT ALLOWED USING TOW");
                    TowVeh = World.CreateVehicle(towmodel, spawnpos, 0);
                }

            }

            TowDriver = Function.Call<Ped>(Hash.CREATE_RANDOM_PED_AS_DRIVER, TowVeh, true);
            Vector3 playerpos = Game.Player.Character.Position;
            Vector3 towpos = TowVeh.Position;
            TowingService.SetEntityHeading(TowVeh);
            //TowVeh.Heading= Function.Call<float>(Hash.GET_ANGLE_BETWEEN_2D_VECTORS, towpos.X, towpos.Y, playerpos.X, playerpos.Y)-90;
            TowingService.PreparePed(TowDriver);

            TowVeh.AddBlip();
            TowVeh.CurrentBlip.Sprite = BlipSprite.TowTruck;


            TowVeh.SirenActive = true;
            VehicleToTow.LeftIndicatorLightOn = true;
            VehicleToTow.RightIndicatorLightOn = true;
            if (!VehicleToTow.CurrentBlip.Exists())
            {
                VehicleToTow.AddBlip();
                VehicleToTow.CurrentBlip.Scale = 0.5f;
                VehicleToTow.CurrentBlip.Color = BlipColor.Yellow;
            }

            Function.Call(Hash.SET_VEHICLE_FORWARD_SPEED, TowVeh, 10f);
            Function.Call(GTA.Native.Hash._0x0DC7CABAB1E9B67E, TowVeh, true); //Load Collision
            Function.Call(GTA.Native.Hash._0x0DC7CABAB1E9B67E, VehicleToTow, true); //Load Collision


            if (TowType==2)
            {
                Function.Call(Hash.TASK_HELI_MISSION, TowDriver.Handle, TowDriver.CurrentVehicle.Handle, 0, 0, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 6, 20f, 1f, 1f, 7, 20, -1f, 1);
                TowDriver.DrivingStyle = (DrivingStyle)0;
            }
            else
            {
                int drivingstyle = 4 + 16 + 32 + 262144;
                if (TowingService.SafeDriving)
                {
                    drivingstyle = drivingstyle + 1 + 2;
                }
                TowDriver.Task.DriveTo(TowVeh, VehicleToTow.Position, 15f, 15f, drivingstyle);
            }

            if ((TowVeh.Model == "ramptruck" || TowVeh.Model == "ramptruck2") && TowVeh.IsExtraOn(3)) TowVeh.ToggleExtra(3, false);
            if (TowVeh.Model == "mule5") TowVeh.Livery = 1;

            TowingService.SetDecorBool(TowingService.DontInfluence, VehicleToTow, true);
            TowingService.SetDecorBool(TowingService.DontInfluence, TowVeh, true);

            TowingService.SetDecorBool(TowingService.HandledByTow, VehicleToTow, true);

            // if (!TowingService.DecorExistsOn("DontInfluence", VehicleToTow)) UI.Notify("~r~Nope");
        }

        public void HandleTowing()
        {
            if ((TowingService.CanWeUse(TowDriver) && !TowDriver.IsAlive))
            {
                CleanTowService();
                return;
            }
            if ((TowingService.CanWeUse(TowVeh) && !TowDriver.IsAlive))
            {
                CleanTowService();
                return;
            }
            if ((!TowingService.CanWeUse(VehicleToTow)) || (TowingService.CanWeUse(VehicleToTow) && !Game.Player.Character.IsInRangeOf(VehicleToTow.Position, 300f)))
            {
                CleanTowService();
                return;
            }
            if (TowVeh.IsStopped && !TowVeh.IsStoppedAtTrafficLights) StuckPatience++;

            if (StuckPatience>5)
            {
                
                StuckPatience = 0;
                if (TowingService.ImmersiveStuckFix)
                {
                    if(TowingService.ReverseWhenTowing) TowingService.TempAction(TowDriver, TowVeh, 23, 3000); else TowingService.TempAction(TowDriver, TowVeh, 3, 3000);
                }
                        else TowVeh.Position = World.GetNextPositionOnStreet(TowVeh.Position, true);
                return;
            }

            if (TowDriver.TaskSequenceProgress != -1 && !TowVeh.Model.IsHelicopter) return;
            switch (AmbientTowingPhase)
            {
                case AmbientTowingPhase.TowApproachingArea:
                    {

                        if (TowType == 2 && TowVeh.Speed < 2f)
                        {
                            if (!TowVeh.IsInRangeOf(VehicleToTow.Position, 40f))
                            {
                                TowDriver.DrivingStyle = (DrivingStyle)0;
                                TaskSequence BobSequence = new TaskSequence();
                                Function.Call(Hash.TASK_HELI_MISSION, 0, TowDriver.CurrentVehicle.Handle, 0, 0, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 4, 10f, 1f, VehicleToTow.Heading, 7, 15, -1f, 1);

                                BobSequence.Close();
                                TowDriver.Task.PerformSequence(BobSequence);
                                BobSequence.Dispose();
                            }
                            else
                            {
                                AmbientTowingPhase = AmbientTowingPhase.TowApproachingVehicle;
                            }
                        }
                        else
                        {
                            if (!TowVeh.IsInRangeOf(VehicleToTow.Position, 20f))
                            {
                                if (TowVeh.Speed < 1f && TowingService.RandomInt(1, 4) == 1)
                                {
                                    int drivingstyle = 4 + 16 + 32 + 262144;
                                    if (TowingService.SafeDriving)
                                    {
                                        drivingstyle = drivingstyle + 1 + 2;
                                    }
                                    TaskSequence BobSequence = new TaskSequence();
                                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 15f, drivingstyle, 15f);
                                    Function.Call(Hash.TASK_VEHICLE_TEMP_ACTION, 0, TowVeh,1,-1);
                                    BobSequence.Close();
                                    TowDriver.Task.PerformSequence(BobSequence);
                                    BobSequence.Dispose();
                                }
                            }
                            else
                            {
                                AmbientTowingPhase = AmbientTowingPhase.TowApproachingVehicle;

                            }
                        }
                        break;
                    }
                case AmbientTowingPhase.TowApproachingVehicle:
                    {
                        bool CanContinue = false;

                        if (TowType == 1 && TowVeh.Speed < 1f)
                        {

                            if (TowVeh.IsInRangeOf(VehicleToTow.Position, 10f))
                            {
                                CanContinue = true;
                            }
                            else
                            {

                                int drivingstyle = 4 + 16 + 32 + 262144;
                                if (TowingService.SafeDriving)
                                {
                                    drivingstyle = drivingstyle + 1 + 2;
                                }
                                TaskSequence BobSequence = new TaskSequence();
                                Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 15f, drivingstyle, 4f);

                                BobSequence.Close();
                                TowDriver.Task.PerformSequence(BobSequence);
                                BobSequence.Dispose();
                            }

                        }
                         if (TowType == 2 && TowVeh.Speed < 5f)
                        {

                            if (TowVeh.IsInRangeOf(VehicleToTow.Position, 10f))
                            {
                                CanContinue = true;
                            }
                            else
                            {
                                TaskSequence BobSequence = new TaskSequence();

                                Function.Call(Hash.TASK_HELI_MISSION, 0, TowDriver.CurrentVehicle.Handle, 0, 0, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 4, 10f, 1f, VehicleToTow.Heading, 7, 2, -1f, 1);
                                BobSequence.Close();
                                TowDriver.Task.PerformSequence(BobSequence);
                                BobSequence.Dispose();
                            }
                        }
                         if (TowType == 0 && TowVeh.Speed < 1f)
                        {

                            if (TowVeh.IsInRangeOf(VehicleToTow.Position, 10f))
                            {
                                CanContinue = true;
                            }
                            else
                            {
                                Function.Call(Hash._SET_TOW_TRUCK_CRANE_RAISED, TowVeh, 0f);


                                if (TowingService.ReverseWhenTowing)
                                {


                                    Vector3 park = Vector3.Zero;

                                    /*
                                    Vector3 temp = VehicleToTow.Position;
                                    Vector3 offset = Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, TowVeh, temp.X, temp.Y, temp.Z);


                                    if (offset.X > 0)
                                    {
                                        park = TowVeh.Position + (TowVeh.ForwardVector * 3)+(TowVeh.RightVector * 15);
                                    }
                                    else
                                    {
                                        park = TowVeh.Position + (TowVeh.ForwardVector * 3) + (TowVeh.RightVector * -15);
                                    }

                                    */
                                    park = TowVeh.Position + (TowVeh.ForwardVector * -10);

                                    if (TowingService.TooManyVehiclesAround(TowVeh.Position, 5))
                                    {
                                        int drivingstyle = 4 + 16 + 32 + 262144;

                                        TaskSequence BobSequence = new TaskSequence();
                                        //Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, park.X, park.Y, park.Z, 5f, drivingstyle, 4f);

                                        Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 5f, drivingstyle+ 4194304, 4f);
                                        BobSequence.Close();
                                        TowDriver.Task.PerformSequence(BobSequence);
                                        BobSequence.Dispose();
                                    }
                                    else
                                    {
                                        int drivingstyle = 4 + 16 + 32 + 262144;
                                        if (TowingService.SafeDriving)
                                        {
                                            drivingstyle = drivingstyle + 1 + 2;
                                        }
                                        TaskSequence BobSequence = new TaskSequence();

                                        Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, park.X, park.Y, park.Z, 5f, drivingstyle+ 1024, 4f);

                                        Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 5f, drivingstyle + 1024+ 4194304, 4f);
                                        BobSequence.Close();
                                        TowDriver.Task.PerformSequence(BobSequence);
                                        BobSequence.Dispose();
                                    }
                                }
                                else
                                {
                                    int drivingstyle = 4 + 16 + 32 + 262144;
                                    if (TowingService.SafeDriving)
                                    {
                                        drivingstyle = drivingstyle + 1 + 2;
                                    }
                                    TaskSequence BobSequence = new TaskSequence();
                                    Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, 0, TowVeh, VehicleToTow.Position.X, VehicleToTow.Position.Y, VehicleToTow.Position.Z, 5f, drivingstyle  + 4194304, 4f);

                                    //TowDriver.Task.DriveTo(TowVeh, VehicleToTow.Position, 4f, 5f, drivingstyle + 4194304);
                                    BobSequence.Close();
                                    TowDriver.Task.PerformSequence(BobSequence);
                                    BobSequence.Dispose();

                                }
                            }
                        }


                        if (CanContinue)
                        {
                            AmbientTowingPhase = AmbientTowingPhase.VehicleTowed;

                        }
                        break;
                    }
                case AmbientTowingPhase.VehicleTowed:
                    {
                        bool CanContinue = false;
                        if (TowType == 1)
                        {
                            if (TowVeh.IsStopped)
                            {

                                //VehicleToTow.HandbrakeOn = true;
                                //VehicleToTow.Heading = TowVeh.Heading;
                                //VehicleToTow.Position = TowVeh.Position + (TowVeh.ForwardVector * -2) + (TowVeh.UpVector * 4);
                                CanContinue = true;

                                TowingService.Attach(TowVeh, VehicleToTow);
                            }
                        }
                        else if (TowType == 2)
                        {
                            //   VehicleToTow.Position = TowVeh.Position + (TowVeh.UpVector * -8);
                            // Function.Call(Hash.ATTACH_VEHICLE_TO_CARGOBOB, TowVeh, VehicleToTow, false, 0, 0, 0);
                            TowingService.Attach(TowVeh, VehicleToTow);

                            /*
                            Vector3 vehtotowpos = VehicleToTow.Position + (new Vector3(0,0,(VehicleToTow.Model.GetDimensions().Z /2f)));
                            Vector3 towpos = TowVeh.Position + (TowVeh.UpVector * -1);

                            Rope rope;
                            rope = World.AddRope(RopeType.Normal, towpos, vehtotowpos, towpos.DistanceTo(vehtotowpos), 0f, false);
                            rope.ActivatePhysics();
                            rope.AttachEntities(TowVeh, towpos, VehicleToTow, vehtotowpos, towpos.DistanceTo(vehtotowpos));
                            Ropes.Add(rope);
                            */
                            CanContinue = true;
                        }
                        else if(TowType == 0)
                        {
                            if (TowVeh.IsStopped)
                            {

                                if(!TowVeh.HasTowArm) TowingService.Attach(TowVeh, VehicleToTow); // TowVeh.Model=="ramptruck" || TowVeh.Model == "ramptruck2" || TowVeh.Model =="mule5"
                                else
                                {

                                if (TowingService.TeleportToTowBack)
                                {
                                    VehicleToTow.Position = (TowVeh.Position + (TowVeh.ForwardVector * -8) + (TowVeh.UpVector * 2));
                                    VehicleToTow.Heading = TowVeh.Heading; //+180f
                                    Function.Call(Hash.SET_VEHICLE_FORWARD_SPEED, VehicleToTow, 4f);
                                }
                                Function.Call(Hash.ATTACH_VEHICLE_TO_TOW_TRUCK, TowVeh, VehicleToTow, false, 0, 0, 0);
                                Function.Call(Hash._SET_TOW_TRUCK_CRANE_RAISED, TowVeh, 1f);
                                }

                                CanContinue = true;
                            }

                        }
                        if (CanContinue)
                        {
                            AmbientTowingPhase = AmbientTowingPhase.TowFinishedJob;
                        }

                        break;
                    }
                case AmbientTowingPhase.TowFinishedJob:
                    {


                            /*
                            if (TowVeh.Model == "flatbed")
                            {
                                Rope rope;
                                rope = World.AddRope(RopeType.Normal, TowVeh.Position, VehicleToTow.Position, VehicleToTow.Position.DistanceTo(TowVeh.Position), 0f, true);
                                rope.ActivatePhysics();
                                rope.AttachEntities(TowVeh, TowVeh.Position, VehicleToTow, VehicleToTow.Position, VehicleToTow.Position.DistanceTo(TowVeh.Position));
                                TowingService.Ropes.Add(rope);

                            }
                            */

                                if (Function.Call<bool>(Hash.IS_THIS_MODEL_A_HELI, TowVeh.Model))
                                {
                                    Function.Call(Hash._SET_VEHICLE_ENGINE_POWER_MULTIPLIER, TowVeh, 5000f);
                                    Vector3 position = TowDriver.CurrentVehicle.Position;


                                    TaskSequence BobSequence = new TaskSequence();
                                    //Function.Call(Hash.TASK_HELI_MISSION, 0, TowVeh, 0, 0, position.X, position.Y, position.Z+30f,4, 15f, 30f, 1f, 7, 8, -1f, 1);
                                    Function.Call(Hash.TASK_HELI_MISSION, 0, TowVeh, 0, 0, 400f, -1632f, 29f, 6, 15f, 1f, 1f, 50, 2, -1f, 1);
                                    BobSequence.Close();
                                    TowDriver.Task.PerformSequence(BobSequence);
                                    BobSequence.Dispose();
                                }
                                else
                                {
                                    if (TowVeh.Model == "flatbed")
                                    {
                                        Function.Call(GTA.Native.Hash.TASK_VEHICLE_DRIVE_WANDER, TowDriver.Handle, TowVeh.Handle, 10f, (1 + 2 + 4 + 16 + 32 + 262144));
                                    }
                                    else
                                    {
                                        Function.Call(GTA.Native.Hash.TASK_VEHICLE_DRIVE_WANDER, TowDriver.Handle, TowVeh.Handle, 10f, (1 + 2 + 4 + 16 + 32 + 262144));
                                    }
                                }
                            if (!TowVeh.IsInRangeOf(Game.Player.Character.Position, 300f))  CleanTowService();                        
                        break;
                    }
            }
        }
       public void CleanTowService()
        {
            if (TowingService.CanWeUse(TowDriver))
            {

                if (TowingService.CanWeUse(TowVeh))    Function.Call(GTA.Native.Hash.TASK_VEHICLE_DRIVE_WANDER, TowDriver, TowVeh, 15f, (1 + 2 + 4 + 16 + 32 + 262144));

                if (TowDriver.IsInRangeOf(Game.Player.Character.Position, 100f)) TowDriver.IsPersistent = false;
                else TowDriver.Delete();
            }

            if (TowingService.CanWeUse(TowVeh))
            {
                if (TowVeh.CurrentBlip != null)
                {
                    TowVeh.CurrentBlip.Remove();
                }


                if (TowVeh.IsInRangeOf(Game.Player.Character.Position, 100f)) TowVeh.IsPersistent = false;
                else TowVeh.Delete();
            }
            if (TowingService.CanWeUse(VehicleToTow))
            {
                if (VehicleToTow.CurrentBlip != null)
                {
                    VehicleToTow.CurrentBlip.Remove();
                }

                if (VehicleToTow.IsInRangeOf(Game.Player.Character.Position, 100f)) VehicleToTow.IsPersistent = false;
                else VehicleToTow.Delete();
            }


            VehicleToTow = null;
            TowVeh = null;
            TowDriver = null;

        }
    }
}
