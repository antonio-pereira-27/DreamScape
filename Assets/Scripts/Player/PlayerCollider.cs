using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour, IDataPersistance
{
    // references
    public HealthBar healthBar;

    private InventoryManager inventoryManager;
    [SerializeField]private GameObject gameOverPanel;

    // variables
    public float maxHealth = 100f;
    [HideInInspector]public float currentHealth;

    private float timer;

    public bool hited = false;

    private void Awake() {
        
    }

    private void Start() {
        inventoryManager = FindObjectOfType<InventoryManager>();
        currentHealth = inventoryManager.gameData.health;
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0f && gameOverPanel != null)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gameOverPanel.SetActive(true);
        }

        GameObject fireplace = GameObject.FindGameObjectWithTag("Fireplace");
        if (fireplace != null && Vector3.Distance(fireplace.transform.position, gameObject.transform.position) <= 3f && currentHealth < maxHealth)
        {
            if (timer >= 2f)
            {
                timer = 0f;
                currentHealth += 2f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HallwayDoor")
            SceneManager.LoadScene("Hallway");
        
        if(other.tag == "BedroomDoor")
            SceneManager.LoadScene("Bedroom");
                
        if(other.tag == "BigBathroomDoor")
            SceneManager.LoadScene("BigBathroom");
        
        if(other.tag == "KitchenDoor")
            SceneManager.LoadScene("Kitchen");
        
        if(other.tag == "LivingRoomDoor")
            SceneManager.LoadScene("LivingRoom");
        
        if(other.tag == "SmallBathroomDoor")
            SceneManager.LoadScene("SmallBathroom");
        
        if(other.tag == "StudyDoor")
            SceneManager.LoadScene("Study");
        
        if(other.tag == "CampingDoor")
            SceneManager.LoadScene("CampingMap");
        
        if(other.tag == "DiningRoomDoor")
            SceneManager.LoadScene("DiningRoom");

        DataPersistanceManager.instance.SaveGame();
        
    }

    public void TakeDamage(float amount)
    {
        hited = true;
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
    }

    public void LoadData(GameData data)
    {
        this.currentHealth = data.health;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.currentHealth;
        
    }
}
