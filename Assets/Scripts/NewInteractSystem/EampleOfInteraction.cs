using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EampleOfInteraction : Interactble
{
    

    // ���� ����� ����� ��������� �������������� � ��������
    protected override void Interact()
    {
        Destroy(gameObject);
    }
}
