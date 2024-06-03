using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactble : MonoBehaviour
{
    // Сообщение которое будет выводится игроку, когда он будет смотреть на объект
    public string promtMessage;
    public Material defaultMaterial = null;
    public Material selectedMaterial = null;
    public bool looking;

    private void Update()
    {
        if (looking)
        {
            SelectObject();

        }
        else
        {
            DeselectObject();
        }
        looking = false;
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // Пустой метод который будет перезаписан подклассами далее
    }

    public void SelectObject()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;
    }

    public void DeselectObject()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
    }
   

}
