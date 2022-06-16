using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushChair : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    private Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null && hit.gameObject.transform.position.y >= transform.position.y)
        {
            Vector3 forceDirection = transform.forward;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
            StartCoroutine(PushAnimation());
        }
           
    }

    IEnumerator PushAnimation()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("Push");

        yield return new WaitForSeconds(3f);

        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }
    
}
