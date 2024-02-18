using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField]
    private PlayerGunSelector GunSelector;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.player.Shoot.triggered)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }
}
