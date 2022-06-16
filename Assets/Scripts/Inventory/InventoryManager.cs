using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDataPersistance
{
    // references
    public GameData gameData;
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public Transform Attack1;
    public Transform Attack2;
    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;

    // variables
    public int position;

    private void Awake()
    {
        Instance = this;
        gameData = DataPersistanceManager.instance.gameData;
    }


    private void Update() {
        if (Attack1.GetComponent<Image>().sprite == null){
            Attack1.gameObject.SetActive(false);
        }else
        {
            Attack1.gameObject.SetActive(true);
        }

        if (Attack2.GetComponent<Image>().sprite == null){
            Attack2.gameObject.SetActive(false);
        }else
        {
            Attack2.gameObject.SetActive(true);
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItem()
    {
        
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    public bool CheckButtons()
    {
        position = 0;
        if (Attack1.GetComponent<Image>().sprite == null)
        {
            position = 1;
            return true;
        } 
        else if (Attack2.GetComponent<Image>().sprite == null)
        {
            position = 2;
            return true;
        }          
        else
            return false;

        
    }

    public void CleanInventory()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        this.Items = data.items;
        
    }

    public void SaveData(ref GameData data)
    {
        data.items = this.Items;
    }
}
