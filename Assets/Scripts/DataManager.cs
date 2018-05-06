using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {


    public void SaveData(GameData inputData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/gameData.dat"); // replace hardcoded file to string.

        GameData data = new GameData();
        data.relaxModeWins = inputData.relaxModeWins;
        data.turnModeWins = inputData.turnModeWins;
        data.highestTurnsLeft = inputData.highestTurnsLeft;
        data.highestTimeLeft = inputData.highestTimeLeft;
        data.highestTimeSurvived = inputData.highestTimeSurvived;


        bf.Serialize(fs, data);
        fs.Close();

    }

    public GameData LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        return null;
    }
}
