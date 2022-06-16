using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public int scene;
    public bool instantiateEnemy;
    public void LoadData(GameData data)
    {
        scene = data.scene;
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Hallway")
            instantiateEnemy = false;
        else if (activeScene.name == "Kitchen")
            instantiateEnemy = data.enemysKitchen;
        else if (activeScene.name == "LivingRoom")
            instantiateEnemy = data.enemysLivingRoom;
        else if (activeScene.name == "DiningRoom")
            instantiateEnemy = data.enemysDiningRoom;
        else if (activeScene.name == "BigBathroom")
            instantiateEnemy = data.enemysBigBathroom;
        else if (activeScene.name == "Study")
            instantiateEnemy = data.enemysStudy;
        else if (activeScene.name == "SmallBathroom")
            instantiateEnemy = data.enemysSmallBathroom;
        else if (activeScene.name == "FirstRoom")
            instantiateEnemy = true;
    }

    public void SaveData(ref GameData data)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        data.scene = scene;
        if (activeScene.name == "Kitchen")
            data.enemysKitchen = instantiateEnemy;
        else if (activeScene.name == "LivingRoom")
            data.enemysLivingRoom = instantiateEnemy;
        else if (activeScene.name == "DiningRoom")
            data.enemysDiningRoom = instantiateEnemy;
        else if (activeScene.name == "BigBathroom")
            data.enemysBigBathroom = instantiateEnemy;
        else if (activeScene.name == "Study")
            data.enemysStudy = instantiateEnemy;
        else if (activeScene.name == "SmallBathroom")
            data.enemysSmallBathroom = instantiateEnemy;
    }
}
