using System.Collections;

using UnityEngine;
using FMODUnity;

public class AttackSystem : MonoBehaviour, IDataPersistance
{
    public static AttackSystem Instance;

    // References
    public GameObject fireballProjectile;
    public GameObject shieldProjectile;
    public GameObject waterProjectile;
    public GameObject lightning;
    
    public Transform playerHead;
    public Transform lefthand;
    public Transform rightHand;

    private GrabSystem grabSystem;
    private ExamineObjects examine;
    private Animator animator;

    private SoundPlayer soundPlayer;
    [SerializeField]private EventReference[] attacks; // 0 nao tem ataques | 1 fogo | 2 agua | 3 terra | 4 energia


    // Variables
    private float timeToAttack = 2f;
    [HideInInspector] public bool attack1Available = true;
    [HideInInspector] public bool attack2Available = true;
    private bool inCooldown = false;

    public int selectedAttack1, selectedAttack2;

    
    private bool examineBool;
    private bool inventoryBool;

    private void Awake()
    {
        Instance = this;
        grabSystem = FindObjectOfType<GrabSystem>();
        examine = gameObject.GetComponent<ExamineObjects>();
        animator = gameObject.GetComponent<Animator>();
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryBool = grabSystem.enable;
        examineBool = examine.examineMode;

        if (examineBool == true || inCooldown == true || inventoryBool == true || PauseMenu.gamesIsPaused == true)
        {
            attack1Available = false;
            attack2Available = false;
        }
        else if (examineBool == false && inCooldown == false && inventoryBool == false || PauseMenu.gamesIsPaused == false)
        {
            attack1Available = true;
            attack2Available = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && attack1Available)
            StartCoroutine(Attack1(timeToAttack, selectedAttack1));

        if (Input.GetKeyDown(KeyCode.Mouse1) && attack2Available)
            StartCoroutine(Attack2(timeToAttack, selectedAttack2));

       
    }

    IEnumerator Attack1(float _timeToAttack, int selectedAttack1)
    {
        FilterAttack(selectedAttack1);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("RightAttack");
        soundPlayer.PlayOneShot(attacks[selectedAttack1]);
        inCooldown = true;
        
        yield return new WaitForSeconds(_timeToAttack);

        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
        inCooldown = false;
    }

    IEnumerator Attack2(float _timeToAttack , int selectedAttack2)
    {
        FilterAttack(selectedAttack2);
        soundPlayer.PlayOneShot(attacks[selectedAttack2]);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("LeftAttack");

        inCooldown = true;

        yield return new WaitForSeconds(_timeToAttack);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
        inCooldown = false;
    }
    

    private void InstantiateFireball()
    {
        Instantiate(fireballProjectile, rightHand.position, transform.rotation);
        
    }

    private void InstantiateShield()
    {
        GameObject shield = Instantiate(shieldProjectile, lefthand.position + Vector3.forward, lefthand.rotation);
        shield.transform.SetParent(lefthand);
        
    }

    private void InstantiateWaterball()
    {
        Instantiate(waterProjectile, rightHand.position, transform.rotation);
       
    }

    private void InstantiateEnergy()
    {
        var lightningInstantiate = Instantiate(lightning, lefthand.position, lefthand.rotation);
        lightningInstantiate.transform.SetParent(lefthand);
        
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

    public void LoadData(GameData data)
    {
        selectedAttack1 = data.attack1;
        selectedAttack2 = data.attack2;
    }

    public void SaveData(ref GameData data)
    {
        data.attack1 = selectedAttack1;
        data.attack2 = selectedAttack2;
    }
}
