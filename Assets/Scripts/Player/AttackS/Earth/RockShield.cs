using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShield : MonoBehaviour
{
    // variables
    public float armor = 25f;
    private float lifeTime = 2f;
    
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
