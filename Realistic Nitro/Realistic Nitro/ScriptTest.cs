using GTA;
using GTA.Math;
using GTA.Native;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
//using NativeUI;


public class RealisticNitro : Script
{

    List<string> Exhausts = new List<string>
    {
        "exhaust","exhaust_2","exhaust_3","exhaust_4","exhaust_5","exhaust_6","exhaust_7"
    };
    Keys NitroKey = Keys.ShiftKey;
    GTA.Control PadNitroKey = GTA.Control.ScriptSelect;
    GTA.Control SecondPadNitroKey = GTA.Control.ScriptSelect;

    Color BoostUp = Color.FromArgb(200, 85, 255, 0);
    Color BoostDown = Color.FromArgb(200, 255, 0, 0);
    Color Boost = Color.FromArgb(200, 0, 21, 255);
    bool NitroSystemEnabled = true;
    bool NitroSound = false;
    bool SoundPlaying = false;
    bool NitroScreenEffect = false;
    bool AutoRecharge = true;
    bool OnlyLandVehicles = true;
    float NitroScore = 0;
    float NitroUpMultiplier = 1;
    float NitroDownMuliplier = 5;
    float MaxNitro = 1f;
    int NitroBarWide = 100;
    float NitroBarYPos = 500;
    float EngineMultiplier = 10;
    bool EmptyNitroCooldown = true;
    int EmptyNitroCooldownRef = 0;
    bool CurrentlyNitroing = false;
    int TankSize = 1;
    int BoostForce = 100;

    float Variation = 0.015f;

    bool DisplayInfo = false;

    float Damage = 0;
    public RealisticNitro()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        LoadSettings();
        NitroScore = MaxNitro;

        NitroBarWide = (int)MaxNitro * 100;
    }

    void LoadSettings()
    {
        ScriptSettings config = ScriptSettings.Load(@"scripts\RealisticNitro.ini");
        NitroSystemEnabled = config.GetValue<bool>("SETTINGS", "EnabledByDefault", true);

        NitroSound = config.GetValue<bool>("SETTINGS", "NitroSound", false);
        NitroScreenEffect = config.GetValue<bool>("SETTINGS", "NitroScreenEffect", true);
        AutoRecharge = config.GetValue<bool>("SETTINGS", "AutoRecharge", false);
        OnlyLandVehicles = !config.GetValue<bool>("SETTINGS", "AllowBoatsAndPlanes", true);

        NitroUpMultiplier = config.GetValue<float>("MULTIPLIERS", "NitroGainMultiplier", 1);
        NitroDownMuliplier = config.GetValue<float>("MULTIPLIERS", "NitroWasteMultiplier", 1);
        NitroBarYPos = config.GetValue<float>("SETTINGS", "NitroBarPos", 0.95f);
        EngineMultiplier = config.GetValue<float>("SETTINGS", "EngineMultiplier", 5);

        PadNitroKey = config.GetValue<GTA.Control>("KEYS", "PadNitroKey", GTA.Control.ScriptRDown);
        SecondPadNitroKey = config.GetValue<GTA.Control>("KEYS", "SecondPadNitroKey", GTA.Control.ScriptPadRight);

        NitroKey = config.GetValue<Keys>("KEYS", "NitroKey", Keys.ShiftKey);
    }
    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }
    int NitroWasteRefTime = Game.GameTime;
    int NitroFillRefTime = Game.GameTime;

    void OnTick(object sender, EventArgs e)
    {
        //NitroScore = (float)Math.Round(NitroScore, 4);
        //UI.ShowSubtitle(NitroScore.ToString());
        CurrentlyNitroing = false;
        if (NitroSystemEnabled)
        {
            float referencescore = NitroScore;
            // var res = UIMenu.GetScreenResolutionMantainRatio();
            //var safe = UIMenu.GetSafezoneBounds();

            Vehicle v = Game.Player.Character.CurrentVehicle;
            if (CanWeUse(v))
            {
                if ((OnlyLandVehicles && !IsBoat(v) && !IsPlane(v)) || !OnlyLandVehicles)
                {
                    HandleNitroing();
                    if (NitroFillRefTime < Game.GameTime)
                    {
                        NitroFillRefTime = Game.GameTime + 40;
                        HandleNitroScore();
                    }

                    Color Color = Boost;
                    if (NitroScore > referencescore)
                    {
                        Color = BoostUp;
                    }
                    if (NitroScore < referencescore)
                    {
                        Color = BoostDown;
                    }

                    if(!Function.Call<bool>(Hash.DECOR_EXIST_ON, v, "ikt_speedo_nos"))
                    {
                        Function.Call(Hash.DRAW_RECT, 0.5f, NitroBarYPos, MaxNitro * 0.501f, 0.012f, 0, 0, 0, 200);
                        Function.Call(Hash.DRAW_RECT, 0.5f, NitroBarYPos, NitroScore * 0.5f, 0.01f, Color.R, Color.G, Color.B, Color.A);
                    }

                    //void DRAW_RECT(float x, float y, float width, float height, int r, int g, int b, int a) // 0x3A618A217E5154F0 0xDD2BFC77
                    //new UIResRectangle(new Point(, new Size(, Color.FromArgb(200, 0, 0, 0)).Draw();
                    //new UIResRectangle(new Point(, , new Size(), Color).Draw();
                }
            }
        }

    }
    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, false, -1);
    }



    void HandleNitroing()
    {
        Vehicle veh = GetLastVehicle(Game.Player.Character);
        if (CanWeUse(veh))
        {
            if (DisplayInfo)
            {
                DisplayHelpTextThisFrame("Tank capacity: ~b~"+TankSize+ "L~w~~n~Boost: ~b~" + BoostForce+ "%~w~~n~E. Damage/Tick: ~o~"+Damage+"~w~~n~Remaining: ~b~" +Math.Round(NitroScore*100).ToString()+"%");
            }
            Damage = 0;

            if (WasCheatStringJustEntered("nitro info"))
            {
                DisplayInfo = !DisplayInfo;
            }

            if (WasCheatStringJustEntered("nitro boost"))
            {
                UI.Notify("~b~[Realistic Nitro]~w~~n~Enter the boost force (%)");

                int.TryParse(Game.GetUserInput(90), out BoostForce);
            }

            if (WasCheatStringJustEntered("nitro tank"))
            {
                UI.Notify("~b~[Realistic Nitro]~w~~n~Enter the tank size (1-1000)");

                int.TryParse(Game.GetUserInput(90),out TankSize);

                UI.Notify(veh.FriendlyName+"'s tank size is now "+TankSize+" Litters.");
                NitroScore = 0;
            }
            if(WasCheatStringJustEntered("nitro fill"))
            {
                int cost = (25 * TankSize);
                if (Game.Player.Money==0 || Game.Player.Money > cost)
                {
                    Game.Player.Money -= cost;
                    NitroScore = MaxNitro;
                    UI.Notify("~b~Nitro tank ("+TankSize+"L) for " + veh.FriendlyName + " refilled for ~g~$"+cost+"~w~.");
                }
                else
                {
                    UI.Notify("You don't have ~g~$"+ cost + "~w~ to refill your nitro tank.");
                }
            }
            bool IsPressingNitroKey = (Game.IsKeyPressed(NitroKey) || (Function.Call<bool>(Hash._GET_LAST_INPUT_METHOD, 2) == false && Game.IsControlPressed(2, PadNitroKey) && Game.IsControlPressed(2, SecondPadNitroKey)));
            if (EmptyNitroCooldown && Game.GameTime > EmptyNitroCooldownRef) EmptyNitroCooldown = false;
            if (!EmptyNitroCooldown && Game.Player.Character.IsSittingInVehicle() && NitroScore > 0)
            {
                if (IsPressingNitroKey)
                {
                    if (NitroWasteRefTime < Game.GameTime)
                    {
                        NitroScore -= (((Variation * NitroDownMuliplier) / TankSize) * BoostForce) / 100;
                        NitroWasteRefTime = Game.GameTime + 40;
                    }
                    CurrentlyNitroing = true;
                    if (NitroScore <= 0)
                    {
                        EmptyNitroCooldown = true;
                        EmptyNitroCooldownRef = Game.GameTime + 2000;
                        NitroScore = 0;
                    }

                    if (NitroScore > 0 && !EmptyNitroCooldown)
                    {
                        if (NitroSound)
                        {
                            Function.Call(Hash.SET_VEHICLE_BOOST_ACTIVE, veh, true);
                            SoundPlaying = true;
                        }
                        if (NitroScreenEffect)
                        {
                            Function.Call(Hash._START_SCREEN_EFFECT, "RaceTurbo", 0, 0);
                        }
                        Function.Call(Hash._SET_VEHICLE_ENGINE_TORQUE_MULTIPLIER, veh, (EngineMultiplier*BoostForce)/100);
                        if (veh.GetMod(VehicleMod.Engine) < 1)
                        {
                            float basedamage = 0.1f;
                            float difference = ((basedamage * BoostForce) / 100);
                            Damage = 0.1f;  // % of the usual damage, 0.05f
                            if (Damage != difference)
                            {
                                if (difference > basedamage) Damage = 0;
                                for (int i = 1; i < 2; i++)
                                {
                                    if (difference > basedamage) Damage += difference;
                                    if (difference < basedamage) Damage -= difference;
                                }
                            }                           
                            if (Damage > 0) veh.EngineHealth -= Damage;
                        }
                    }

                    if (Function.Call<bool>(Hash._0x8702416E512EC454, "core"))
                    {
                        float pitch = Function.Call<float>(Hash.GET_ENTITY_PITCH, veh);
                        
                        foreach (string exhaust in Exhausts)
                        {
                            if (veh.HasBone(exhaust))
                            {
                                float scale = (0.5f * BoostForce) / 100;
                                if (scale > 2) scale = 2;
                                if (scale < 0.3) scale = 0.3f;
                                Vector3 offset =  veh.GetBoneCoord(exhaust);
                                offset = Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, veh, offset.X, offset.Y, offset.Z);
                                Function.Call(Hash._0x6C38AF3693A69A91, "core");

                                //BOOL START_PARTICLE_FX_NON_LOOPED_ON_ENTITY(char *effectName, Entity entity, float offsetX, float offsetY, float offsetZ, float rotX, float rotY, float rotZ, float scale, BOOL axisX, BOOL axisY,BOOL axisZ) // 0x0D53A3B8DA0809D2 0x9604DAD4
                                //Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_AT_COORD, "scr_carsteal5_car_muzzle_flash", offset.X, offset.Y, offset.Z, 0f, pitch, direction - 90f, 1.0f, false, false, false);
                                Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "veh_backfire", veh, offset.X, offset.Y, offset.Z, 0f, pitch,  0f,scale, false, false, false);

                            }

                        }
                    }
                    else
                    {
                        Function.Call(Hash._0xB80D8756B4668AB6, "scr_carsteal4");
                    }
                }

                    if ((!IsPressingNitroKey && SoundPlaying) || EmptyNitroCooldown)
                    {
                        Function.Call(Hash.SET_VEHICLE_BOOST_ACTIVE, veh, false);
                    Function.Call(Hash.SET_VEHICLE_BOOST_ACTIVE, veh, true);
                    Function.Call(Hash.SET_VEHICLE_BOOST_ACTIVE, veh, false);

                    SoundPlaying = false;
                    }
                
            }
        }

    }
    void HandleNitroScore()
    {



        Vehicle veh = GetLastVehicle(Game.Player.Character);
        if (CanWeUse(veh))
        {
            if (IsDecorRegistered("ikt_speedo_nos", 3) && !Function.Call<bool>(Hash.DECOR_EXIST_ON, veh, "ikt_speedo_nos"))
            {
                //RegisterDecorator("ikt_speedo_nos", 3);
                //UI.Notify("~g~Realistic Nitro~w~~n~Decorator ikt_speedo_nos does not exist for " + veh.FriendlyName + ", creating and setting it to 1");
                Function.Call(Hash.DECOR_SET_INT, veh, "ikt_speedo_nos", 1);
            }

           
            if(IsDecorRegistered("ikt_speedo_nos_level" , 1) && IsDecorRegistered("ikt_speedo_nos", 3) && DecorExistsOn("ikt_speedo_nos", veh)) Function.Call(Hash._DECOR_SET_FLOAT, veh, "ikt_speedo_nos_level", NitroScore);
            //RegisterDecorator("ikt_speedo_nos_level", 1);


            if (AutoRecharge && !CurrentlyNitroing)
            {
                if (NitroScore < MaxNitro)
                {
                    NitroScore += ((Variation * NitroUpMultiplier) / TankSize);
                }
            }
            if (!veh.IsStopped)
            {

                if((veh.IsInAir && veh.Velocity.Length() > 10f)
                    ||(Function.Call<bool>(Hash.IS_POINT_ON_ROAD, veh.Position.X, veh.Position.Y, veh.Position.Z, veh) && Function.Call<int>(Hash.GET_TIME_SINCE_PLAYER_DROVE_AGAINST_TRAFFIC) < 1 && Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, veh, true).Y > 20)
                    ||(veh.IsOnAllWheels && Math.Abs(Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, veh, true).X) > 8 && Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, veh, true).Y > 4))
                {
                    if (1==2 && NitroScore < MaxNitro && !CurrentlyNitroing)
                    {
                        NitroScore += ((Variation * NitroUpMultiplier)/TankSize);
                    }
                }

            }

        }

        if (NitroScore > MaxNitro) NitroScore = MaxNitro;
    }

    bool IsPlane(Vehicle veh)
    {
        return Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_PLANE, veh.Model);
    }

    bool IsBoat(Vehicle veh)
    {
        return Function.Call<bool>(GTA.Native.Hash.IS_THIS_MODEL_A_BOAT, veh.Model);
    }

    public static bool WasCheatStringJustEntered(string cheat)
    {
        return Function.Call<bool>(Hash._0x557E43C447E700A8, Game.GenerateHash(cheat));
    }
    bool IsSuperCar(Vehicle veh)
    {
        switch (Function.Call<int>(GTA.Native.Hash.GET_VEHICLE_CLASS, veh))
        {
            case 6:
                {
                    return true;
                }
            case 7:
                {
                    return true;
                }

        }
        return false;
    }

    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }
    void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && Game.IsKeyPressed(Keys.ShiftKey))
        {
            LoadSettings();
            UI.Notify("~y~Realistic Nitro~w~ settings reloaded.");
        }
        if ((e.KeyCode == Keys.ShiftKey && Game.IsKeyPressed(Keys.N)) || (e.KeyCode == Keys.N && Game.IsKeyPressed(Keys.ShiftKey)))
        {
            NitroSystemEnabled = !NitroSystemEnabled;
            if (NitroSystemEnabled == true)
            {
                UI.Notify("Realistic Nitro ~g~enabled");
            }
            else
            {
                UI.Notify("Realistic Nitro ~r~disabled");
            }
        }
    }

    int GetBoneIndex(Entity entity, string value)
    {
        return GTA.Native.Function.Call<int>(Hash._0xFB71170B7E76ACBA, entity, value);
    }

    Vector3 GetBoneCoords(Entity entity, int boneIndex)
    {
        return GTA.Native.Function.Call<Vector3>(Hash._0x44A8FCB8ED227738, entity, boneIndex);
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
    void RegisterDecorator(string decor, int type)
    {
        UnlockDecorator();
        Script.Wait(10);
        if (!Function.Call<bool>(Hash.DECOR_IS_REGISTERED_AS_TYPE, decor, type)) Function.Call(Hash.DECOR_REGISTER, decor, type);

        Script.Wait(10);

        LockDecotator();
    }
    bool DecorExistsOn(string decor, Entity e)
    {
        if (!CanWeUse(e)) return false;
        return Function.Call<bool>(Hash.DECOR_EXIST_ON, e, decor);
    }
    bool IsDecorRegistered(string decor, int type)
    {
        return Function.Call<bool>(Hash.DECOR_IS_REGISTERED_AS_TYPE, decor, type);
    }
    float GetDecorFloat(string decor, Entity e)
    {
        if (!CanWeUse(e) || !DecorExistsOn(decor, e)) return -1;
        return Function.Call<float>(Hash._DECOR_GET_FLOAT, e, decor);
    }

    int GetDecorInt(string decor, Entity e)
    {
        if (!CanWeUse(e) || !DecorExistsOn(decor, e)) return -1;
        return Function.Call<int>(Hash.DECOR_GET_INT, e, decor);
    }
    bool GetDecorBool(string decor, Entity e)
    {
        if (!CanWeUse(e) || !DecorExistsOn(decor, e)) return false;
        return Function.Call<bool>(Hash.DECOR_GET_BOOL, e, decor);
    }

    void SetDecorBool(string decor, Entity e, bool i)
    {
        if (!CanWeUse(e)) return;
        Function.Call(Hash.DECOR_SET_BOOL, e, decor, i);
    }
    void SetDecorInt(string decor, Entity e, int i)
    {
        if (!CanWeUse(e)) return;
        Function.Call(Hash.DECOR_SET_INT, e, decor, i);
    }

    void SetDecorFloat(string decor, Entity e, float i)
    {
        if (!CanWeUse(e)) return;
        Function.Call(Hash._DECOR_SET_FLOAT, e, decor, i);
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
}