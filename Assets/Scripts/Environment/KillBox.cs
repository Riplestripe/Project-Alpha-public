using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public Transform point;
    public bool teleport, kill, toPoligon;
    private void OnTriggerEnter(Collider other)
    {
        if (toPoligon)
        {
            SceneManager.LoadScene("Poligon");
        }
        if (teleport)
        {
            if (other.CompareTag("Player") && other.TryGetComponent<PlayerMovement>(out var player))
            {
                Debug.Log("ping");
                player.Teleport(point.position, point.rotation);
                player.transform.eulerAngles = new Vector3(0, 0, 0);
            }

        }

        if(kill)
        {
            other.gameObject.SetActive(false);
        }
    }

}
