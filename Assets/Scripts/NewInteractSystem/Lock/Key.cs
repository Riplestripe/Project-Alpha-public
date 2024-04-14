using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour
{
    public bool hovered;
    private RaycastHit hit;
   

    private void Update()
    {
        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -20f);
        Vector3 oldPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(EventSystem.current.IsPointerOverGameObject() || Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("key"))
            {
                this.hovered = true;
            }
            else hovered = false;

        }

        if (hovered)
        {
            this.transform.localPosition = newPos;
        }
        else this.transform.localPosition = oldPos;
    }


}
