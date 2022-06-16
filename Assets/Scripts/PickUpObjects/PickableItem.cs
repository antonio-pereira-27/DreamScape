using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    // References 
    private Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody;
    private void Awake()
    {
        // rigidbody component
        rigidbody = GetComponent<Rigidbody>();
    }
}
