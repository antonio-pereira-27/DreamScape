using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // References
    [SerializeField] private GameObject explosion;

    // variables
    private float damage = 10f;
    private float lifeTime = 1.3f;
    public float speed = 5f;

    private Collider[] hited;
    // Update is called once per frame
    void Update()
    {
        lifeTime-= Time.deltaTime;
        if (lifeTime <= 0)
        {
            SpawnExplosion();
            VerifyColisions();
            Destroy(gameObject);
            
        }
        else
        {
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.velocity = transform.forward*speed;
           
        }
       
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Obstacles"))
        {
            SpawnExplosion();
            VerifyColisions();
            Destroy (gameObject);
        }
        
    }

    private void SpawnExplosion()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        var explosionVar = Instantiate(explosion, position, Quaternion.identity);
        Destroy(explosionVar, 2f);
    }

    private void VerifyColisions()
    {
        hited = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hit in hited)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player");
                hit.GetComponent<PlayerCollider>().TakeDamage(damage);
            }
            else if (hit.CompareTag("Shield"))
            {
                Debug.Log("Shield");
                hit.GetComponent<RockShield>().TakeDamage(damage * 2.5f);
            }
            else
                Debug.Log("No Hit");
        }
    }

}
