using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Util
    {

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


        int zModifier = 0;
        int Tries = 0;
        while (finalpos == Vector3.Zero)
        {
            int NodeID = Function.Call<int>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, desiredPos.X, desiredPos.Y, desiredPos.Z + zModifier, NodeNumber, type, 300f, 300f);
            if (ForceOffroad)
            {
                while (!Function.Call<bool>(Hash._GET_IS_SLOW_ROAD_FLAG, NodeID) && NodeNumber < 500)
                {
                    NodeNumber++;
                    NodeID = Function.Call<int>(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_ID, desiredPos.X, desiredPos.Y, desiredPos.Z + zModifier, NodeNumber + 5, type, 300f, 300f);
                }
            }
            Function.Call(Hash.GET_VEHICLE_NODE_POSITION, NodeID, outArgA);
            finalpos = outArgA.GetResult<Vector3>();


            if (sidewalk && finalpos.DistanceTo(World.GetNextPositionOnSidewalk(finalpos))<100) finalpos = World.GetNextPositionOnSidewalk(finalpos);
            Tries++;


            zModifier = GetRandomInt(-Tries, Tries);
            if (Tries > 1000)
            {
                //UI.Notify("~r~Error generating position(" + Tries + " tries)");
                break;
            }
        }

        return finalpos;
        }
    public static int GetRandomInt(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }



    public static void WarnPlayer(string script_name, string title, string message)
    {
        Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
        Function.Call(Hash._SET_NOTIFICATION_MESSAGE, "CHAR_SOCIAL_CLUB", "CHAR_SOCIAL_CLUB", true, 0, title, "~b~" + script_name);
    }

    public static bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    public static void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, false, -1);
    }
    //Notifications system

    public static List<String> MessageQueue = new List<String>();
    public static int MessageQueueInterval = 8000;
    public static int MessageQueueReferenceTime = 0;
    public static void HandleMessages()
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
    public static void AddQueuedHelpText(string text)
    {
        if (!MessageQueue.Contains(text)) MessageQueue.Add(text);
    }

    public static void ClearAllHelpText(string text)
    {
        MessageQueue.Clear();
    }


    public static List<String> NotificationQueueText = new List<String>();
    public static List<String> NotificationQueueAvatar = new List<String>();
    public static List<String> NotificationQueueAuthor = new List<String>();
    public static List<String> NotificationQueueTitle = new List<String>();

    public static int NotificationQueueInterval = 8000;
    public static int NotificationQueueReferenceTime = 0;
    public static void HandleNotifications()
    {
        if (Game.GameTime > NotificationQueueReferenceTime)
        {

            if (NotificationQueueAvatar.Count > 0 && NotificationQueueText.Count > 0 && NotificationQueueAuthor.Count > 0 && NotificationQueueTitle.Count > 0)
            {
                NotificationQueueReferenceTime = Game.GameTime + ((NotificationQueueText[0].Length / 10) * 1000);
                Notify(NotificationQueueAvatar[0], NotificationQueueAuthor[0], NotificationQueueTitle[0], NotificationQueueText[0]);
                NotificationQueueText.RemoveAt(0);
                NotificationQueueAvatar.RemoveAt(0);
                NotificationQueueAuthor.RemoveAt(0);
                NotificationQueueTitle.RemoveAt(0);
            }
        }
    }

    public static void AddNotification(string avatar, string author, string title, string text)
    {
        NotificationQueueText.Add(text);
        NotificationQueueAvatar.Add(avatar);
        NotificationQueueAuthor.Add(author);
        NotificationQueueTitle.Add(title);
    }
    public static void CleanNotifications()
    {
        NotificationQueueText.Clear();
        NotificationQueueAvatar.Clear();
        NotificationQueueAuthor.Clear();
        NotificationQueueTitle.Clear();
        NotificationQueueReferenceTime = Game.GameTime;
        Function.Call(Hash._REMOVE_NOTIFICATION, CurrentNotification);
    }

    public static int CurrentNotification;
    public static void Notify(string avatar, string author, string title, string message)
    {
        if (avatar != "" && author != "" && title != "")
        {
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
            CurrentNotification = Function.Call<int>(Hash._SET_NOTIFICATION_MESSAGE, avatar, avatar, true, 0, title, author);
        }
        else
        {
            UI.Notify(message);
        }
    }

}
