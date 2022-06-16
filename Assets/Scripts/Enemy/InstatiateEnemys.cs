using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateEnemys : MonoBehaviour
{
    // References
    [SerializeField] 
    private List<GameObject> enemyList;
    [SerializeField]
    private List<Transform> enemyTransformList;
    [SerializeField]
    private List<Transform> patrolPoints;

    private GameManager gameManager;

    // Variables
    private float enemysEliminated = 0f;
    
    private int enemysCount = 0;
    private int totalEnemys = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        totalEnemys = enemyList.Count;
        
    }

    private void Update()
    {
        if (gameManager.instantiateEnemy)
        {
            while (totalEnemys != enemysCount)
            {
                
                var enemy = Instantiate(enemyList[enemysCount], enemyTransformList[enemysCount].position, Quaternion.identity);
                enemy.GetComponent<Enemy>().patrolPoints = this.patrolPoints;
                enemy.GetComponent<Enemy>().instatiateEnemys = this;
                enemysCount++;
            }
           
        }

        if (enemysEliminated == enemyList.Count)
        {
            gameManager.instantiateEnemy = false;
        }

    }


    public void EnemyEliminated()
    {
        enemysEliminated++;
    }
}
