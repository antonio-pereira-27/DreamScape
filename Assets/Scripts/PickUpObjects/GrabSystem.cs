using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{
   
    //References
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform grabSlot;
    [SerializeField] private Transform camera;


    
    [HideInInspector]public PickableItem pickedItem;
    private NoteAppear noteAppear;

    public GameObject inventoryOBJ;
    public InventoryManager inventoryManager;
    
    // variables
    [HideInInspector] public bool elegible;
    [HideInInspector] public bool enable;

    private bool toyActive;
    
    
    // Update is called once per frame
    void Update()
    {

        // Show Note
        if (Input.GetKeyDown(KeyCode.F))
        {
            // if already picked drop it
            if (noteAppear)
                CloseNote(noteAppear);
            else
            {
                // check by raycast hit with a 
                RaycastHit ray;
                if (Physics.Raycast(playerHead.position, camera.forward, out ray, 5f))
                {
                    var revealable = ray.transform.GetComponent<NoteAppear>();

                    if (revealable)
                        RevealNote(revealable);
                }
            }
        }

        // Pickup Itens
        if (Input.GetKeyDown(KeyCode.F))
        {
            // if already picked drop it
            if (pickedItem)
            {
                if (pickedItem.gameObject.CompareTag("Item"))
                {
                    
                     pickedItem.gameObject.GetComponent<ItemPickup>().Pickup();
                     
                }
                else
                    DropItem(pickedItem);
                
            }
            else
            {
                // check by raycast hit with a 
                RaycastHit ray;
                if (Physics.Raycast(playerHead.position, camera.forward, out ray , 5f))
                {
                    // check if gets the component "pickable"
                    var pickable = ray.transform.GetComponent<PickableItem>();
                    
                    // if it is grab it
                    if (pickable)
                    {
                        if (pickable.gameObject.tag == "Boneco")
                        {
                            //pickable.GetComponent<TriggerCutscene3>().picked = true;
                            toyActive = true;
                           StartCoroutine(triggerCutSceneToy(pickable));
                        }
                        else
                        {
                            PickItem(pickable);
                        }
                    }
                        
                }

            }
        }


        if (Input.GetKeyDown(KeyCode.I))
        { 
            if (inventoryOBJ.active == true)
            {
                inventoryOBJ.SetActive(false);
                inventoryManager.CleanInventory();
                enable = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                inventoryManager.ListItem();
                inventoryOBJ.SetActive(true);
                enable = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }    
    }
    
    private void PickItem(PickableItem item)
    {
        // reference
        pickedItem = item;
        elegible = true;
        // rigidbody
        item.Rigidbody.isKinematic = true;
        item.Rigidbody.velocity = Vector3.zero;
        item.Rigidbody.angularVelocity = Vector3.zero;
        
       
        item.transform.SetParent(grabSlot);
    
        
        // reset the transform
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;

    }


    private void DropItem(PickableItem item)
    {
        // null the item
        pickedItem = null;
        elegible = false;
        // remove from parent
        item.transform.SetParent(null);

        // dont affect the physics from rigdbody
        item.Rigidbody.isKinematic = false;
        
        
        // add a force to throw it
        item.Rigidbody.AddForce(item.transform.forward * 10, ForceMode.VelocityChange);
    }

    
    
    private void CloseNote(NoteAppear note)
    {
        noteAppear = null;
        note.reveal = false;
    }

    private void RevealNote(NoteAppear note)
    {
        noteAppear = note;

        note.reveal = true;
    }

    private IEnumerator triggerCutSceneToy(PickableItem p)
    {
        p.gameObject.GetComponent<TriggerCutscene3>().picked = true;
        yield return new WaitForSeconds(10.0f);
        p.gameObject.GetComponent<TriggerCutscene3>().picked = false;
        gameObject.GetComponent<ToyController>().active = toyActive;
        yield return new WaitForSeconds(1.0f);
        Destroy(p.gameObject);
    }

}
