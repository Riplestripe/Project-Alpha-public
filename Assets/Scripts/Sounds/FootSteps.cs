


using UnityEngine;
using System.Collections;


public class Footsteps : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference 
    m_EventPath;
    public float m_StepDistance = 2.0f;
    float m_StepRand;
    Vector3 m_PrevPos;
    float m_DistanceTravelled;


    void Start()
    {
        //Initialise random, set seed
        Random.InitState(System.DateTime.Now.Second);

        //Initialise member variables
        m_StepRand = Random.Range(0.0f, 0.5f);
        m_PrevPos = transform.position;
    
    }

    void Update()
    {
        m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;
        if (m_DistanceTravelled >= m_StepDistance + m_StepRand)//TODO: Play footstep sound based on position from headbob script
        {
            PlayFootstepSound();
            m_StepRand = Random.Range(0.0f, 0.5f);//Adding subtle random variation to the distance required before a step is taken - Re-randomise after each step.
            m_DistanceTravelled = 0.0f;
        }

        m_PrevPos = transform.position;

    }

    void PlayFootstepSound()
    {
        FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(m_EventPath);
        e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000.0f))
        {

            switch (hit.collider.tag)
            {
                case "ground":
                    Debug.Log("ground!");
                    e.setParameterByName("All_step", 0);
                    break;
                case "glass":
                    Debug.Log("concrete!");
                    e.setParameterByName("All_step", 2);
                    break;
            }

        }

        e.start();
        e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
        
    }


  
}