using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss : MonoBehaviour
{
    // variables
    public float armor = 20f;
    private float lifeTime = 3f;
    
    // Update is called once per frame
    void Update()
    {
        lifeTime-= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        
        if(armor <= 0f)
            Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        armor -= damage;
    }
}
