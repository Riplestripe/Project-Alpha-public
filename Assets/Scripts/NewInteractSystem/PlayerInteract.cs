using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    public GameObject firstObj;
    public GameObject secondObj;
    public Key key;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }
    
   
    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // Луч идущий из центра камеры вперед
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo; // Хранит информацию об объекте который попадает луч
        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactble>() != null)
            {
                Interactble interactble = hitInfo.collider.GetComponent<Interactble>();
                playerUI.UpdateText(interactble.promtMessage);
                interactble.SelectObject();
                if (inputManager.player.Interact.triggered)
                {
                    interactble.BaseInteract();
                }
            }
            
        }
        
    }
}
