using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistanceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    public GameData gameData;
    private List<IDataPersistance> dataPersistancesObjects;
    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance {get; set;}

    private void Awake() {
       if (instance != null)
       {
            Debug.LogError("Found More Than one Data Persistance Manager in scene");
       }
       instance = this;
    }

    private void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistancesObjects = FindAllDataPersistancesObjects();
        LoadGame();
    }

    
    public void NewGame()
    {
        print("initializing new game");
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        print("loading game");
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data found. Initializing data to default values");
            NewGame();
        }

        foreach (IDataPersistance dataPersistenceObjs in dataPersistancesObjects)
        {
            dataPersistenceObjs.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistenceObjs in dataPersistancesObjects)
        {
            dataPersistenceObjs.SaveData( ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

   private List<IDataPersistance> FindAllDataPersistancesObjects()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistances);
    }

}
