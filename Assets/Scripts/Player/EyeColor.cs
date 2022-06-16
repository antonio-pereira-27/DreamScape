using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeColor : MonoBehaviour
{
    // References
    [SerializeField]
    List<Material> materials = new List<Material>();

    [SerializeField]
    private GameObject leftEye;
    [SerializeField]
    private GameObject rightEye;

    private AttackSystem attackSystem;

    private void Start()
    {
        attackSystem = this.gameObject.GetComponent<AttackSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        leftEye.GetComponent<MeshRenderer>().material = materials[attackSystem.selectedAttack1];
        rightEye.GetComponent<MeshRenderer>().material = materials[attackSystem.selectedAttack2];
    }
}
