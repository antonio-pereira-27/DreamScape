using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandCheck : MonoBehaviour
{
    float damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position -= other.transform.forward * 2;
            other.gameObject.GetComponent<PlayerCollider>().TakeDamage(damage);
        }
    }
}
