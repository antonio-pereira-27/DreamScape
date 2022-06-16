using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBossCreator : MonoBehaviour
{
    // References
    [SerializeField]private EnergyBoss lightning;

    // variables
    private float lifeTime = 0.5f;


    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
        else       
            Instantiate(lightning, transform.position, transform.rotation);
            
            
        
    }
}
