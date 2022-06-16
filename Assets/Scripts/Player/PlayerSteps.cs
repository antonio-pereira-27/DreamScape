using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    // references
    private PlayerMovement characterController;

    private SoundPlayer soundPlayer;

    private FMOD.Studio.EventInstance foostepsInstance;
    [SerializeField]private EventReference footsteps;

    private enum CURRENT_TERRAIN { GRAVEL, WOOD_FLOOR, METAL };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance foosteps;

    // variables
    private float timer = 0.0f;
    private float walkSpeed = 0.75f;
    private float runSpeed = 0.35f;

    private void Awake() {
        characterController = gameObject.GetComponent<PlayerMovement>();
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
    }


    // Update is called once per frame
    void Update()
    {

        DetermineTerrain();
        if (characterController.moveDirection != Vector3.zero && characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && timer >= runSpeed)
            {
                SelectAndPlayFootstep();
                timer = 0.0f;
            }else if (timer >= walkSpeed)
            {
                SelectAndPlayFootstep();
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }
    }

    

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Gravel"))
            {
                currentTerrain = CURRENT_TERRAIN.GRAVEL;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                currentTerrain = CURRENT_TERRAIN.WOOD_FLOOR;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Metal"))
            {
                currentTerrain = CURRENT_TERRAIN.METAL;
            }
            
        }
    }

    public void SelectAndPlayFootstep()
    {     
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.WOOD_FLOOR:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.METAL:
                PlayFootstep(2);
                break;

            default:
                PlayFootstep(0);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance(footsteps);
        foosteps.setParameterByName("Tipo de Passos", terrain);
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }
}
