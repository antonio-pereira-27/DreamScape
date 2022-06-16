using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private List<Transform> positions;

    // variables
    private float timer = 0f;
    private float startTimer = 5f;
    private int position = 0;

    private int state = 1;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(positions[position].position);
        navMeshAgent.speed = 3f;
        navMeshAgent.acceleration = 4f;

        if (Vector3.Distance(transform.position, positions[position].position) <= 2f)
        {
            if (timer <= 0f)
            {
                if(position == positions.Count - 1)
                    position = 0;
                else
                    position++;
                
                timer = startTimer;
            }else
            {
                state = 0;
                timer -= Time.deltaTime;
                if (position == positions.Count - 1)
                    transform.LookAt(positions[0]);
                else
                    transform.LookAt(positions[position + 1]);
            }
        }
        else
            state = Random.Range(1,2);

        animator.SetInteger("State", state);
    }
}
