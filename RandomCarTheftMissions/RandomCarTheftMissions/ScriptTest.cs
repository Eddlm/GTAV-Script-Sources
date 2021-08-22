using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;



public class ScriptTest : Script
{
    string ScriptName = "CarTheftMissions";
    string ScriptVer = "0.1";

    Vector3 DropoffPoint = Vector3.Zero;
    Model TargetedModel = null;
    Ped Contractor = null;
    Blip destination = null;
    public ScriptTest()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;

    }

    void OnTick(object sender, EventArgs e)
    {
        if (WasCheatStringJustEntered("cartheft"))
        {
            List<Vehicle> cars = new List<Vehicle>();

            foreach (Vehicle v in World.GetAllVehicles()) if (!cars.Contains(v)) cars.Add(v);

            int patience = 0;

            while (DropoffPoint == Vector3.Zero == patience < 50)
            {
                DropoffPoint = World.GetSafeCoordForPed(Game.Player.Character.Position.Around(150), true);
                patience++;
            }
            if (DropoffPoint == Vector3.Zero)
            {
                UI.Notify("Coudln't get a suitable place for dropoff, please retry.");
            }
            else
            {
                Vehicle veh = cars[RandomInt(0, cars.Count)];

                TargetedModel = veh.Model;

                destination = World.CreateBlip(DropoffPoint);
                destination.Sprite = BlipSprite.PersonalVehicleCar;
                destination.Color = BlipColor.Green;
                destination.Name = veh.FriendlyName + " dropoff";
                if (CanWeUse(Contractor)) Contractor.MarkAsNoLongerNeeded();
                Contractor = World.CreateRandomPed(DropoffPoint);
                Contractor.AlwaysKeepTask = true;
                Contractor.Weapons.Give(WeaponHash.Pistol, 900, true, true);
                Function.Call(GTA.Native.Hash.SET_DRIVER_ABILITY, Contractor, 1.0f);
                Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Contractor, "WORLD_HUMAN_SMOKING", 5000, true);

                UI.Notify("Someone wants a ~b~" + veh.FriendlyName + "~w~ delivered to ~y~" + World.GetStreetName(DropoffPoint) + "~w~. Find one and park it there.");

            }

        }


        if (DropoffPoint!= Vector3.Zero && TargetedModel!= null && CanWeUse(Contractor))
        {
            if (Contractor.IsInCombat)
            {
                UI.Notify("~r~Mission failed, the ~g~ contact~r~ got spooked!");
                DropoffPoint = Vector3.Zero;
                TargetedModel = null;
                return;
            }
            if (Game.Player.Character.IsInRangeOf(DropoffPoint, 50f))
            {

                Vehicle[] vehs = World.GetNearbyVehicles(DropoffPoint, 20f, TargetedModel);
                if(vehs.Length > 0)
                {

                    Vehicle car = vehs[0];
                    if (car.IsStopped && Game.Player.Character.IsOnFoot)
                    {
                        int money = (int)car.BodyHealth * car.HighGear;
                        UI.Notify(car.FriendlyName+" sold for ~g~"+money+"~w~.");
                        Game.Player.Money += money;
                        car.LockStatus = VehicleLockStatus.LockedForPlayer;
                        DropoffPoint = Vector3.Zero;
                        TargetedModel = null;
                        if (destination.Exists()) destination.Remove();
                        Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, Contractor, car, 10f,1+2+ 4 + 8 + 16 + 32);
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
    protected override void Dispose(bool dispose)
    {
        if (CanWeUse(Contractor)) Contractor.MarkAsNoLongerNeeded();
        if(destination.Exists()) destination.Remove();
        UI.Notify(ScriptName+" reset.");
        base.Dispose(dispose);
    }
    public static Random rnd = new Random();
    public static int RandomInt(int min, int max)
    {
        return rnd.Next(min, max);
    }
    public static bool WasCheatStringJustEntered(string cheat)
    {
        return Function.Call<bool>(Hash._0x557E43C447E700A8, Game.GenerateHash(cheat));
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
