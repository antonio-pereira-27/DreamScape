using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(){
        // path combine to avoid errors with differents OS
        string fullPath = Path.Combine(dataDirPath, dataFileName);    

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load serialized data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)){
                using(StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }

                //  deserialized data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            }
            catch (System.Exception ex)
            {
                 Debug.Log("Error ocurred whent trying to load data from file : " + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // path combine to avoid errors with differents OS
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // create a new directory where the file will be written to if it doenst already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the c# game data to json
            string dataToStore = JsonUtility.ToJson(data, true);
            // write to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
             Debug.Log("Error ocurred whent trying to save data to file : " + fullPath + "\n" + ex);
        }
    }
}
