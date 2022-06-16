using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // References
    private Transform playerTransform;
    public Transform rightHand;
    public Transform leftHand;
    
    [HideInInspector]public NavMeshAgent agent;
    private Animator animator;
    public InstatiateEnemys instatiateEnemys;
    
    [HideInInspector] 
    public List<Transform> patrolPoints;

    [SerializeField]
    private GameObject grenadeProjectile;

    [SerializeField]
    private HealthBar healthBar;

  


    // Variables
    private float waitTime;
    private float startWaitTime = 3.0f;
    public int randomSpot = 0;
    
    private bool sawEnemy;

    private float maxHealth = 100f;
    private float currentHealth;

    private float timeToRangeAttack = 4f;
    private float timeToFistAttack = 2f;

    private float dieTimer = 10f;

    private bool idle;

    // DecisionTree 
    private DTNode tree;

    private Action fistAttack;
    private Action rangeAttack;
    private Action followPlayer;
    private Action patrol;

    private Func<bool> inRange;
    private Func<bool> isClose;
    private Func<bool> caught;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        agent = transform.GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
       

        animator = gameObject.GetComponent<Animator> ();
        

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        // Decision Tree Actions
        this.fistAttack = FistAttack;
        this.rangeAttack = RangeAttack;
        this.followPlayer = FollowPlayer;
        this.patrol = Patrol;
        
        // Decision Tree Conditions
        this.inRange = InRange;
        this.isClose = IsClose;
        caught = SawEnemy;
        
        // Decision Tree Nodes
        DTNode fistAttack = new DTAction("Fist Attack", this.fistAttack);
        DTNode rangeAttack = new DTAction("Range Attack", this.rangeAttack);
        DTNode followPlayer = new DTAction("Follow Player", this.followPlayer);
        DTNode patrol = new DTAction("Patrol", this.patrol);

        DTNode isClose = new DTCondition("Is Close", this.isClose, fistAttack, rangeAttack);
        DTNode inRange = new DTCondition("In Range", this.inRange, isClose, followPlayer);
        tree = new DTCondition("Saw Player", caught, inRange, patrol);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0f)
            Die();
        else
        {
            tree.Run();
            animator.SetBool("Idle", idle);
            
        }

    }


    private bool SawEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
        {
            if (hit.transform.CompareTag("Player"))
            {
                sawEnemy = true;
                transform.LookAt(playerTransform);
            }
                
        }

        if (currentHealth < 100)
            sawEnemy = true;

        float maxDistance = 6f;
        Vector3 vectorDistance = playerTransform.position - transform.position;
        float floatDistance = vectorDistance.magnitude;

        if (floatDistance <= maxDistance)
            sawEnemy = true;

        return sawEnemy ;
    }

    private bool InRange()
    {
        transform.LookAt(playerTransform);
        float maxDistance = 15f;
        Vector3 vectorDistance = playerTransform.position - transform.position;
        float floatDistance = vectorDistance.magnitude;
         
        return floatDistance < maxDistance;
    }
    
    private bool IsClose()
    {
        transform.LookAt(playerTransform);
        float maxDistance = 2f;
        Vector3 vectorDistance = playerTransform.position - transform.position;
        float floatDistance = vectorDistance.magnitude;
       
        return floatDistance < maxDistance;
    }
    
    private void FollowPlayer()
    {
        
        if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
        {
            idle = true;
            agent.isStopped = true;
            agent.speed = 0f;
            agent.acceleration = 0f;
        }
        else
        {
            idle = false;
            agent.stoppingDistance = 1.5f;
            agent.SetDestination(playerTransform.position);
            agent.speed = 5f;
            agent.acceleration = 10f;
        }

        transform.LookAt(playerTransform);
        
    }

    private void Patrol()
    {
        agent.SetDestination(patrolPoints[randomSpot].position);
        agent.speed = 2f;
        agent.acceleration = 3f;


        
        if (Vector3.Distance(transform.position, patrolPoints[randomSpot].position) <= 2f)
        {
            if (waitTime <= 0)
            {
                if (randomSpot == patrolPoints.Count - 1)
                    randomSpot = 0;
                else
                    randomSpot += 1;
                
                waitTime = startWaitTime;
            }
            else
            {
                idle = true;
                waitTime -= Time.deltaTime;
                if (randomSpot == patrolPoints.Count - 1)
                    transform.LookAt(patrolPoints[0]);
                else
                    transform.LookAt(patrolPoints[randomSpot + 1]);
            }
        }
        else
            idle = false;
    }

    private void FistAttack()
    {
        
        if (timeToFistAttack > 2f)
        {
            idle = true;
            StartCoroutine(CloseRangeAttack());
            timeToFistAttack = 0f;
            
        }
        else
        {
            timeToFistAttack += Time.deltaTime;  
        }
            
    }
    
    private void RangeAttack()
    {
        
        if (timeToRangeAttack > 4f)
        {
            idle = true;
            StartCoroutine(LongRangeAttack());
            timeToRangeAttack = 0f;
            
        }
        else
        {
            timeToRangeAttack += Time.deltaTime;
            
        }
    }

    
    private void Die()
    {
        instatiateEnemys.EnemyEliminated();
        Destroy(gameObject);
    }

   


    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator CloseRangeAttack()
    {
       // animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("CloseAttack");
        
        yield return new WaitForSeconds(4f);
        //animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }

    IEnumerator LongRangeAttack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("RangeAttack");
        Instantiate(grenadeProjectile, leftHand.position, transform.localRotation);
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }

}
