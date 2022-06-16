using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateToy : MonoBehaviour, IDataPersistance
{
    private bool instantiate;

    private void Update() {
        gameObject.SetActive(instantiate);
    }
    public void LoadData(GameData data)
    {
        instantiate = !data.toy;
    }

    public void SaveData(ref GameData data)
    {
       // no need to save data
    }
}
