using System.IO;
using UnityEngine;
using System.Xml.Serialization;



// Save/Load should take and return generic type T (include string param) for datapath
// Should be MonoBehaviour so there cant be new DataManager class generated;
// SetData from events, so only specific classes can access methods.
public static class DataManager{


    public static void SaveData(AchievementContainer data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AchievementContainer));
        FileStream filestream = File.Create(Application.persistentDataPath + "/gamedata.dat"); // replace hardcoded file to string.

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    public static AchievementContainer LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/gamedata.dat"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AchievementContainer));
            FileStream filestream = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            AchievementContainer data = serializer.Deserialize(filestream) as AchievementContainer;
            filestream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveCardInfo(CardInfoContainer data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CardInfoContainer));
        FileStream filestream = File.Create(Application.persistentDataPath + "/cardinfo.dat"); // replace hardcoded file to string.

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    public static CardInfoContainer LoadCardInfo()
    {
        if (File.Exists(Application.persistentDataPath + "/cardinfo.dat"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CardInfoContainer));
            FileStream filestream = File.Open(Application.persistentDataPath + "/cardinfo.dat", FileMode.Open);
            CardInfoContainer data = serializer.Deserialize(filestream) as CardInfoContainer;
            filestream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
