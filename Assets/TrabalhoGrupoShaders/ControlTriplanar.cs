using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTriplanar : MonoBehaviour
{
    // references
    [SerializeField] private Material material;
    private Transform player;

    // variables
    private float distance;

    private void Awake() {
        material = GetComponent<Renderer>().material;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update() {
        
        distance = Vector3.Distance(gameObject.transform.position, player.position) - 4f;
        material.SetFloat("_MossRange", distance);
    }
}
