using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

enum WeatherType
{
    EXTRA_SUNNY = -1750463879,
    CLEAR = 916995460,
    NEUTRAL = -1530260698,
    SMOG = 282916021,
    FOGGY = -1368164796,
    OVERCAST = -1148613331,
    CLOUDS = 821931868,
    CLEARING = 1840358669,
    RAIN = 1420204096,
    THUNDER = -1233681761,
    SNOW = -273223690,
    BLIZZARD = 669657108,
    LIGHT_SNOW = 603685163,
    X_MAS = -1429616491
};

enum VehicleClass
{
    COMPACT=0,
    SEDAN,
    SUV,
    COUPE,
    MUSCLE,
    SPORTCLASSIC,
    SPORT,
    SUPER,
    MOTORCYCLE,
    OFFROAD,
    INDUSTRIAL,
    UTILITY,
    VAN_OR_PICKUP,
    BICYCLE,
    BOAT,
    HELICOPTER,
    AIRPLANE,
    SERVICE,
    EMERGENCY,
    MILITARY,
    COMMERTIAL,
}


public class HollywoodRollover : Script
{
    bool Debug = false;

    //Rollovers
    int ReferenceTime=Game.GameTime;
    int NPCBaseProb = 0;
    int PlayerBaseProb =0;
    int ExplodeChance = 1;
    int RolloverCooldownTime = Game.GameTime;
    bool ExtendRollover = true;
    float RolloverForceMult = 1.0f;
    Vehicle Roller = null;
    List<Vehicle> Candidates = new List<Vehicle>();


    //Shfters

    public HollywoodRollover()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        LoadSettings();
    }


    void HandleRoll()
    {
        foreach (Vehicle veh in Candidates)
        {
           if (veh.IsOnAllWheels && Math.Abs(Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, veh, true).X) > 15f && Math.Abs(Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION_VELOCITY, veh, true).Z) < 1f)
            {
                if(GetRolloverProb(veh) > (RandomInt(50,100))) Roller = veh;
                return;
            }
        }

    }

    //Dirtroad: -1885547121, 510490462,-1907520769
    public static List<int> Dirt = new List<int> { 1144315879       , -1286696947, -700658213, 2128369009,
       -1595148316,-765206029,509508168,1333033863,-461750719,1913209870,1619704960};
    public static int GetGroundHash(Vehicle v)
    {
        Vector3 pos = v.Position + (v.UpVector * 4f); ;
        Vector3 endpos = v.Position + (v.UpVector * -1f);

        int shape = Function.Call<int>(Hash._0x28579D1B8F8AAC80, pos.X, pos.Y, pos.Z, endpos.X, endpos.Y, endpos.Z, 1f, 1, v, 7);

        OutputArgument didhit = new OutputArgument();
        OutputArgument hitpos = new OutputArgument();
        OutputArgument snormal = new OutputArgument();
        OutputArgument materialhash = new OutputArgument();

        OutputArgument entity = new OutputArgument();

        Function.Call(Hash._0x65287525D951F6BE, shape, didhit, hitpos, snormal, materialhash, entity);
        return materialhash.GetResult<int>();
    }


    void PerpetuateRollover(Vehicle v)
    {
        if (CanWeUse(v) && !v.IsOnAllWheels && Math.Abs(Function.Call<float>(Hash.GET_ENTITY_SPEED, v)) > 10f && Math.Abs( Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION_VELOCITY, v).Y) > 3f)
        {
            //DisplayHelpTextThisFrame("perp");
            Vector3 vel = Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, v, false).Normalized*0.2f;// new Vector3(playerveh.Velocity.X,playerveh.Velocity.Y, 0);
            Vector3 dir = Vector3.WorldUp*2;


            if (Math.Abs(v.Rotation.X) > 90f)
            {
                //DisplayHelpTextThisFrame("upside down");
                dir = Vector3.WorldDown*2;
                vel = new Vector3(Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, v, false).X , Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, v, false).Y, Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, v, false).Z * -1).Normalized*0.2f;

            }
           if (Math.Abs(Function.Call<float>(Hash.GET_ENTITY_SPEED, v)) < 15f)
            {
                v.ApplyForce(vel*0.3f);
            }

            if ( v.HasCollidedWithAnything)
            {
                dir = dir * 1.2f;
            }
            //Function.Call(Hash.APPLY_FORCE_TO_ENTITY, v, 3, vel.X,vel.Y,vel.Z, dir.X,dir.Y, dir.Z, false, true, true, true, true);

          v.ApplyForce(vel , dir , ForceType.MaxForceRot2);

        }
    }
    public static void DrawLine(Vector3 from, Vector3 to)
    {
        Function.Call(Hash.DRAW_LINE, from.X, from.Y, from.Z, to.X, to.Y, to.Z, 255, 255, 0, 255);
    }

    int CollideCheck = 0;
    void OnTick(object sender, EventArgs e)
    {

        Vehicle v = Game.Player.Character.CurrentVehicle;


        float pitch = (float) Math.Round(Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION_VELOCITY, v).X,1);
        float yaw = (float)Math.Round(Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION_VELOCITY, v).Y, 1);
        float roll = (float)Math.Round(Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION_VELOCITY, v).Z, 1);


        //DisplayHelpTextThisFrame(pitch+" - "+yaw + " - " + roll);
        /*
        Vehicle v = Game.Player.Character.CurrentVehicle;
        if (CanWeUse(v))
        {
            string f = "false";
            if (Dirt.Contains(GetGroundHash(v))) f = "true";
            DisplayHelpTextThisFrame(GetGroundHash(v).ToString() + " " + f);
        }
        */
        //Rollovers
        if (Game.GameTime> RolloverCooldownTime) HandleRoll();

        if (Game.GameTime > ReferenceTime + 500)
        {
            ReferenceTime = Game.GameTime;
            foreach(Vehicle veh in World.GetNearbyVehicles(Game.Player.Character.Position, 30f))
            {
                if (!Candidates.Contains(veh)) Candidates.Add(veh);
            }

            for (int i = 0; i < (Candidates.Count - 1); i++)
            {
                if (!CanWeUse(Candidates[i]) || !Candidates[i].IsInRangeOf(Game.Player.Character.Position, 200f)) Candidates.RemoveAt(i);
            }
        }

        if (CanWeUse(Roller))
        {

            if(Roller.Velocity.Length()<3f)
            {
                Roller = null;
                return;
            }
            else
            {


                    if(Roller.HasCollidedWithAnything &&CollideCheck < Game.GameTime)
                    {
                        int Chance = RandomInt(0, 100);
                        if (Debug) UI.Notify((Chance).ToString() + "% of exploding, >" +(100- ExplodeChance) + " required");
                        if ((100 - Chance) < ExplodeChance && Roller.IsAlive)
                        {
                            Roller.Explode();
                        }
                        CollideCheck = Game.GameTime + 1000;
                    }


              if(ExtendRollover)  PerpetuateRollover(Roller);
            }

            if (Math.Abs(Roller.Rotation.X) > 80f || Math.Abs(Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, Roller, true).X)<2f)
            {
                if (!ExtendRollover) Roller = null;
                return;
            }
            else
            {
                float Side = (Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, Roller, true).X < 0 ? 0.1f : -0.1f);
                Side = Side * RolloverForceMult;
                Roller.ApplyForceRelative(new Vector3(Side, 0, 0), new Vector3(0, 0, Roller.Model.GetDimensions().Z * -2), ForceType.MaxForceRot2);
                if(RolloverCooldownTime<Game.GameTime) RolloverCooldownTime = Game.GameTime + 5000;
            }

        }
        else if (RolloverCooldownTime > Game.GameTime)
        {
            RolloverCooldownTime = 0;
        }
        //Rollovers




    }

    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }

    void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            LoadSettings();
        }
    }

    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    int RandomInt(int min, int max)
    {
        max++;
        return Function.Call<int>(GTA.Native.Hash.GET_RANDOM_INT_IN_RANGE, min, max);
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


    void LoadSettings()
    {
        ScriptSettings config = ScriptSettings.Load(@"scripts\HollywoodRollover.ini");

        PlayerBaseProb = config.GetValue<int>("SETTINGS", "PlayerBaseProb", 0);
        NPCBaseProb = config.GetValue<int>("SETTINGS", "NPCBaseProb", 0);
        Debug = config.GetValue<bool>("SETTINGS", "Debug", true);
        RolloverForceMult = config.GetValue<float>("SETTINGS", "RolloverForceMult", 1.0f);
        ExplodeChance = config.GetValue<int>("SETTINGS", "ExplodeChance", 1);
        ExtendRollover = config.GetValue<bool>("SETTINGS", "ExtendRollover", true);

    }

    bool IsRaining()
    {
        int weather = Function.Call<int>(GTA.Native.Hash._0x564B884A05EC45A3); //get current weather hash
        switch (weather)
        {
            case (int)WeatherType.BLIZZARD:
                {
                    return true;
                }
            case (int)WeatherType.CLEARING:
                {
                    return true;
                }
            case (int)WeatherType.FOGGY:
                {
                    return true;
                }
            case (int)WeatherType.RAIN:
                {
                    return true;
                }
            case (int)WeatherType.NEUTRAL:
                {
                    return true;
                }
            case (int)WeatherType.THUNDER:
                {
                    return true;
                }
            case (int)WeatherType.LIGHT_SNOW:
                {
                    return true;
                }
            case (int)WeatherType.SNOW:
                {
                    return true;
                }
            case (int)WeatherType.X_MAS:
                {
                    return true;
                }
        }
        return false;
    }


    int GetRolloverProb(Vehicle veh)
    {

        string Debugt = "";
        int prob = 0;
        if (veh.Handle == GetLastVehicle(Game.Player.Character).Handle)
        {
             prob = PlayerBaseProb;
        }
        else
        {
             prob = NPCBaseProb;
        }

        Debugt += "Base: " + prob;

        /*
        if (veh.Model.GetDimensions().X > 2.5f) prob += (int)((veh.Model.GetDimensions().X - 2.5f)*2);
        if (veh.Model.GetDimensions().Y > 4f) prob += (int)((veh.Model.GetDimensions().Y - 4f)*2);
        if (veh.Model.GetDimensions().Z > 2.5f) prob += (int)((veh.Model.GetDimensions().Z - 2.5f)*2);
        */

        float heightmult = (veh.Model.GetDimensions().Z+1f - veh.Model.GetDimensions().X);
        if (heightmult < 0) heightmult = 0;
        prob += (int)(heightmult * 10);


        Debugt += "~n~After Dimensions: " + prob;

        prob += (int)(1000f - veh.BodyHealth) / 5;
        Debugt += "~n~After BodyHealth: " + prob;

        /*
        if (!Function.Call<bool>(Hash.IS_POINT_ON_ROAD, veh.Position.X, veh.Position.Y, veh.Position.Z, veh))
        {
            prob += 20;
        }
        */
        
        float diff = 1 + (veh.Model.GetDimensions().Z - veh.Model.GetDimensions().Y);
        if (diff < 1) diff = 1;
        if (Dirt.Contains(GetGroundHash(veh)))
        {
            prob += (int)(20 * diff);
            if (IsRaining()) prob +=(int) (20*diff);
        }
        else
        {
            if (IsRaining()) prob = prob / 2;
        }
        Debugt += "~n~After Terrain: " + prob;



        if (Debug) DisplayHelpTextThisFrame(Debugt);
        return prob;
    }


    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
    }

}