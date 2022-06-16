using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoss : MonoBehaviour
{

    // variables
    private float damage = 15f;
    private float lifeTime = 2f;
    private float speed = 7f;
    

    // Update is called once per frame
    void Update()
    {
        // destroy fireball if dont hit nothing
        lifeTime-= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        else
        {
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
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

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
