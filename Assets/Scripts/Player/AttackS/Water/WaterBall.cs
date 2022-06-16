using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    // References
    [SerializeField] private GameObject pWaterSplash;

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
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            other.GetComponent<Enemy>().agent.speed -= 4f;
            Destroy(gameObject);
        }

        if (other.tag == "Boss")
        {
            other.GetComponent<Boss>().TakeDamage(damage);
            other.GetComponent<Boss>().agent.speed -= 4f;
            Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            var waterSplash = Instantiate(pWaterSplash, position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(waterSplash, 3f);
        }
    }
}
