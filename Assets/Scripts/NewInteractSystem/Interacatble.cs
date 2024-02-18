using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactble : MonoBehaviour
{
    // ��������� ������� ����� ��������� ������, ����� �� ����� �������� �� ������
    public string promtMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // ������ ����� ������� ����� ����������� ����������� �����
    }
}
