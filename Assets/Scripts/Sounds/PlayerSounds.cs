using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _footsteps;
    public FMOD.Studio.EventInstance footsteps;
    float footStepsTimer = 0;
    public float offsetTimer;
    [SerializeField] GameObject player;
    
    [SerializeField] PlayerMovement mvmnt;
    [SerializeField] InputManager inpt;


    private void Awake()
    {

        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);

        }
    }
    public void PlayFootsteps()
    {
        if (footsteps.isValid())
        {
            
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            //footsteps.setParameterByName("Footstep", 0);
            footsteps.start();
            footsteps.release();
        }
    }

    void Update()
    {
        GroundSwtich();
    }

    private void GroundSwtich()
    {
        if (!mvmnt.grounded) return;

        footStepsTimer -= Time.deltaTime;
        if (footStepsTimer <= 0 && inpt.walkPressed)
        {
            Debug.Log("yes");
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "ground":
                        Debug.Log("ground!");
                        footsteps.setParameterByName("Footstep", 0);
                        break;
                    case "conrete":
                        Debug.Log("concrete!");
                        footsteps.setParameterByName("Footstep", 2);
                        break;
                }



            }
            footStepsTimer = offsetTimer;

        }
    }
}
