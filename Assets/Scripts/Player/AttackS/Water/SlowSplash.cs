using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSplash : MonoBehaviour
{
    private float lifetime = 3f;

    private Enemy enemy;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            if(enemy != null)
                enemy.agent.speed -= 3f;
            else 
                Debug.Log("No enemy");
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().agent.speed = 0f;
            other.GetComponent<Enemy>().agent.acceleration = -2f;
            
        }

        if (other.tag == "Boss")
        {
            other.GetComponent<Boss>().agent.speed = 0f;
            other.GetComponent<Boss>().agent.acceleration = -2f;

        }
    }
}
