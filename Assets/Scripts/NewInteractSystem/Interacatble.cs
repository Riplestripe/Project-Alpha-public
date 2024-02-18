using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactble : MonoBehaviour
{
    // Сообщение которое будет выводится игроку, когда он будет смотреть на объект
    public string promtMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // Пустой метод который будет перезаписан подклассами далее
    }
}
