using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Make MonoBehaviour and set data from event.
// Make it so that the functions can take generic type T
public class DataManager{


    public void SaveData(GameData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/gamedata.dat"); // replace hardcoded file to string.

        bf.Serialize(fs, data);
        fs.Close();

    }

    public GameData LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/gamedata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
