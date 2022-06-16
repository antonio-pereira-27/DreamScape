
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //references
    
    
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
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            
        }

        if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
