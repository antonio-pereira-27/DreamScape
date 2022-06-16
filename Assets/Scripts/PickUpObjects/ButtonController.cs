using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour,IDataPersistance
{
    public bool instantiate = true;
    public void LoadData(GameData data)
    {
        load(data);
    }

    public void SaveData(ref GameData data)
    {
        save(data);
    }

   
    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(instantiate);
    }

    private void load(GameData data)
    {
        switch (gameObject.name)
        {
            case "Botao_Agua":
                instantiate = data.waterButton;
                break;
            case "Botao_Fogo":
                instantiate = data.fireButton;
                break;
            case "Botao_Terra":
                instantiate = data.earthButton;
                break;
            case "Botao_Eletricidade":
                instantiate = data.energyButton;
                break;
            default:
                Debug.Log("Error on name");
                break;
        }
        
    }
    private void save(GameData data)
    {
        switch (gameObject.name)
        {
            case "Botao_Agua":
                data.waterButton = instantiate;
                break;
            case "Botao_Fogo":
                data.fireButton = instantiate;
                break;
            case "Botao_Terra":
                data.earthButton = instantiate;
                break;
            case "Botao_Eletricidade":
                data.energyButton = instantiate;
                break;
            default:
                Debug.Log("Error on name");
                break;
        }
        
    }
}
