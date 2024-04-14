using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactble : MonoBehaviour
{
    // ��������� ������� ����� ��������� ������, ����� �� ����� �������� �� ������
    public string promtMessage;
    public Material defaultMaterial = null;
    public Material selectedMaterial = null;

   
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // ������ ����� ������� ����� ����������� ����������� �����
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
