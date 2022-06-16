using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour
{
    // variables
    private float damage = 20f;
    private float lifeTime = 1f;
    private float speed = 25f;
    
    
    private void Update()
    {
        // Time to destroy fireball if doesnt hit
        lifeTime-= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        else
        {
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerCollider>().TakeDamage(damage);
            Destroy(gameObject);
            
        }

        
    }
}
