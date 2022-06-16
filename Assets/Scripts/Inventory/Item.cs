using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Create New Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public bool active;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType
    {
        Button,
        Pin
    }
}
