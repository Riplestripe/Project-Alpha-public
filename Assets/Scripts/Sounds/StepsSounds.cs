using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StepsSounds : MonoBehaviour
{
    
    public EventReference footSteps;
    public float walkSpeed;
    private InputManager mv;
    private void Start()
    {
        mv = GetComponent<InputManager>();
    }
    private void Update()
    {
        if (mv.moveDirection.y != 0 || mv.moveDirection.x !=0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(footSteps , this.transform.position);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "ground":
                        Debug.Log("ground!");
                        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(footSteps);
                        Footstep.setParameterByName("Concrete", 0);
                        Footstep.start();
                        break;
                    case "glass":
                        Debug.Log("glass!");
                        FMOD.Studio.EventInstance Footstep2 = FMODUnity.RuntimeManager.CreateInstance(footSteps);
                        Footstep2.setParameterByName("Glass", 1);
                        Footstep2.start();
                        break;
                }



            }
        }
        
            

             
                


            
        
    }

}
