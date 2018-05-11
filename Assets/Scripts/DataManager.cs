using System.IO;
using UnityEngine;
using System.Xml.Serialization;


// Make MonoBehaviour and set data from event.
// Make it so that the functions can take generic type T
public class DataManager{


    public void SaveData(AchievementContainer data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AchievementContainer));
        FileStream filestream = File.Create(Application.persistentDataPath + "/gamedata.dat"); // replace hardcoded file to string.

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    public AchievementContainer LoadData()
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
}
