using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MonoBehaviour,IDataPersistance
{
    [SerializeField]private GameObject toy;
    public bool active;
    
    // Update is called once per frame
    void Update()
    {
        toy.SetActive(active);
    }

    public void LoadData(GameData data)
    {
       active = data.toy;
    }

    public void SaveData(ref GameData data)
    {
        data.toy = active;
    }
}
