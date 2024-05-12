using sc.terrain.proceduralpainter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VisualEffwct : MonoBehaviour
{
    public Volume Volume;
    float Max = 5f;
    float Min = 0.0f;
    private void Update()
    {   if (Volume.profile.TryGet(out Crt crt))
        {
            if(crt.noiseWeight.value == 0f)
            {
                crt.noiseWeight.value = Mathf.Lerp(0, 50, 0.5f);
            }
           
        }

       // if(Volume.GetComponent<Crt>().noiseWeight.value >= 5f)
       // {
       //     Volume.GetComponent<Crt>().noiseWeight.value -= Time.deltaTime;

      // }
    }
}
