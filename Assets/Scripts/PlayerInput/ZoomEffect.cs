using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    Camera m_Camera;
    int normal = 60;
    int zoom = 40;
    float smooth = 5f;
    public bool zooming;
    public GameObject player;
    public InputManager inputManager;
    public GameObject hands;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<InputManager>();
        m_Camera = GetComponent<Camera>();
        zooming = inputManager.player.ZoomAim.IsPressed();
    }
    void Update()
    {
        zooming = inputManager.player.ZoomAim.IsPressed();

        if (zooming)
        {
            m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else
        {

            m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, normal, Time.deltaTime * smooth);

        }
    }
}
