using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour, IDataPersistance
{
    // References
    private Transform playerTransform;
    public Transform rightHand;
    public Transform leftHand;

    [HideInInspector]public NavMeshAgent agent;
    [SerializeField]private HealthBar healthBar;
    private Animator animator;

    [SerializeField]private GameObject fireballProjectile;
    [SerializeField]private GameObject shieldProjectile;
    [SerializeField]private GameObject waterProjectile;
    [SerializeField]private GameObject lightning;
    // Variables
    private float maxHealth = 100f;
    private float currentHealth;

    private int attack1, attack2;

    private int[] attackArray;

    private bool inCooldown = false;
    
    // DecisionTree 
    private DTNode tree;

    private Action fistAttack;
    private Action rangeAttack;
    private Action followPlayer;

    private Func<bool> inRange;
    private Func<bool> isClose;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = transform.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        attackArray = new int[4]{1,2,3,4};

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


        this.fistAttack = FistAttack;
        this.rangeAttack = RangeAttack;
        this.followPlayer = FollowPlayer;

        this.inRange = InRange;
        this.isClose = IsClose;

        DTNode fistAttack = new DTAction("Fist Attack", this.fistAttack);
        DTNode rangeAttack = new DTAction("Range Attack", this.rangeAttack);
        DTNode followPlayer = new DTAction("Follow Player", this.followPlayer);

        DTNode isClose = new DTCondition("Is Close", this.isClose, fistAttack, rangeAttack);

        tree = new DTCondition("In Range", this.inRange, isClose, followPlayer);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
            Die();
        else
        {
            tree.Run();

            
        }
    }

#region Decision Tree
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
        float distance = 5f;
        Vector3 vectorDistance = playerTransform.position - transform.position;
        float floatDistance = vectorDistance.magnitude;

        return floatDistance < distance;
    }

    private void FollowPlayer()
    {
        animator.SetInteger("Speed", 1);
        if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
        {
            agent.isStopped = true;
            agent.speed = 0f;
            agent.acceleration = 0f;
        }
        else
        {
            agent.stoppingDistance = 1.5f;
            agent.SetDestination(playerTransform.position);
            //agent.speed = 5f;
            agent.acceleration = 10f;
        }

        transform.LookAt(playerTransform);
    }

    private void FistAttack()
    {
        animator.SetInteger("Speed", 0);
        StartCoroutine(CloseAttack());
    }

    private IEnumerator CloseAttack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("0");

        yield return new WaitForSeconds(2f);

        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }

    private void RangeAttack()
    {
        animator.SetInteger("Speed", 0);
        StartCoroutine(MagicAttack());
    }

    private IEnumerator MagicAttack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);

        int random = Random.Range(1, 2);
        if (random == 1 && inCooldown == false){
            FilterAttack(attack1);
            inCooldown = true;
            animator.SetTrigger("1");
        }
        if(random == 2 && inCooldown == false)
        {
            FilterAttack(attack2);
            inCooldown = true;
            animator.SetTrigger("2");
        }
        
        yield return new WaitForSeconds(4f);
        inCooldown = false;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }
#endregion
    private void Die()
    { 
        Destroy(gameObject);
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void LoadData(GameData data)
    {
        ChooseAttack(data.attack1, data.attack2);
    }

    public void SaveData(ref GameData data)
    {
        // no need to save data
    }

    private void ChooseAttack(int dataAttack1, int dataAttack2)
    {
        for (var i = 0; i < attackArray.Length; i++)
        {
            if (dataAttack1 != i && attack1 == 0)
                attack1 = i;
            else if(dataAttack2 != i && attack2 == 0)
                attack2 = i;
        }
    }

    private void InstantiateFireball()
    {
        Instantiate(fireballProjectile, rightHand.position, transform.rotation);
        
    }

    private void InstantiateShield()
    {
        GameObject shield = Instantiate(shieldProjectile, leftHand.position + new Vector3(-0.2f, 0.1f, 0.7f), leftHand.rotation);
        shield.transform.SetParent(leftHand);
        
    }

    private void InstantiateWaterball()
    {
        Instantiate(waterProjectile, rightHand.position, transform.rotation);
       
    }

    private void InstantiateEnergy()
    {
        var lightningInstantiate = Instantiate(lightning, leftHand.position, leftHand.rotation);
        lightningInstantiate.transform.SetParent(leftHand);
    }

     public void FilterAttack(int attack)
    {
        switch (attack)
        {
            case 1:
                InstantiateFireball();
                break;
            case 2:
                InstantiateWaterball();
                break;
            case 3:
                InstantiateShield();
                break;
            case 4: 
                InstantiateEnergy();
                break;
            default:
                Debug.Log("No AttackSelected");
                break;
        }
    }
}
