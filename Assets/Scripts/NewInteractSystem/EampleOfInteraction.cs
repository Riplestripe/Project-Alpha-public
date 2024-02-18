using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EampleOfInteraction : Interactble
{
    

    // Этот метод будет описывать взаимодействие с объектом
    protected override void Interact()
    {
        Destroy(gameObject);
    }
}
