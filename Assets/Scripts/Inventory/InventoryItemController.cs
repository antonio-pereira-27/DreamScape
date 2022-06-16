using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IDataPersistance
{
    Item item;
    int instance1;
    int instance2;

    private void Awake()
    {
        //instance1 = InventoryManager.Instance.gameData.attack1;
        //instance2 = InventoryManager.Instance.gameData.attack2;
    }
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Button:

                if (item.active)
                {
                    
                    item.active = false;
                    if (instance1 == item.id)
                    {
                        InventoryManager.Instance.gameData.attack1 = 0;
                        instance1 = 0;
                        InventoryManager.Instance.Attack1.GetComponent<Image>().sprite = null;
                        
                        AttackSystem.Instance.selectedAttack1 = 0;
                    }
                    else if(instance2 == item.id)
                    {
                        InventoryManager.Instance.gameData.attack2 = 0;
                        instance2 = 0;
                        InventoryManager.Instance.Attack2.GetComponent<Image>().sprite = null;
                        
                        AttackSystem.Instance.selectedAttack2 = 0;
                    }
                }
                else
                {
                    
                    if (InventoryManager.Instance.CheckButtons())
                    {
                        item.active = true;
                        if (InventoryManager.Instance.position == 1)
                        {
                            instance1 = item.id;
                            InventoryManager.Instance.gameData.attack1 = item.id;
                            InventoryManager.Instance.Attack1.GetComponent<Image>().sprite = item.icon;
                            
                            AttackSystem.Instance.selectedAttack1 = instance1;
                        }  
                        else if(InventoryManager.Instance.position == 2)
                        {
                            instance2 = item.id;
                            InventoryManager.Instance.gameData.attack2 = item.id;
                            InventoryManager.Instance.Attack2.GetComponent<Image>().sprite = item.icon;
                            
                            AttackSystem.Instance.selectedAttack2 = instance2;
                        }
                            
                    }
                    else
                    {
                        Debug.Log("Noo positions availables");
                    }
                }
                break;
            case Item.ItemType.Pin:
                Debug.Log("Used : " + item.name);
                break;
            
        }
        
    }

    public void LoadData(GameData data)
    {
        instance1 = data.attack1;
        instance2 = data.attack2;
    }

    public void SaveData(ref GameData data)
    {
        // no need to save data
    }
}
