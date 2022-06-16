using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    // player health
    public float health;

    // instance enemys
    public bool enemysDiningRoom;
    public bool enemysLivingRoom;
    public bool enemysKitchen;
    public bool enemysStudy;
    public bool enemysBigBathroom;
    public bool enemysSmallBathroom;

    // instantiate buttons
    public bool fireButton;
    public bool waterButton;
    public bool earthButton;
    public bool energyButton;

    // inventory items
    public List<Item> items;

    // attacks 
    public int attack1;
    public int attack2;

    // toy
    public bool toy;


    // scene to save
    public int scene;

    public GameData()
    {
        
        health = 100f;

        items = new List<Item>();

        attack1 = 0;
        attack2 = 0;

        enemysDiningRoom = true;
        enemysLivingRoom = true;
        enemysKitchen = true;
        enemysStudy = true;
        enemysBigBathroom = true;
        enemysSmallBathroom = true;

        fireButton = true;
        waterButton = true;
        earthButton = true;
        energyButton = true;
        
        toy = false;

        scene = 1;
    }
}
