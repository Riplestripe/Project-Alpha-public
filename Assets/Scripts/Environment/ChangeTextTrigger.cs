using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextTrigger : MonoBehaviour
{
    public TextMeshPro textField;
    public string newText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) textField.text = newText;
    }
}
