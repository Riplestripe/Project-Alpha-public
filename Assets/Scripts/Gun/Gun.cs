using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [Header("Gun stats")]
    public int damage;
    public float timeBetweenShooting, spreadX, spreadY, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, rangeWeapon, meleeWeapon;
    int bulletsLeft, bulletsShots;

    //bools
    bool shooting, readytoShot, reloading;

    [Header("References")]
    public Camera cam;
    public Transform muzzle = null;
    public RaycastHit hit;
    public LayerMask whatsEnemy, playerMask;
    public GameObject muzzleFlash, bulletHole;
    InputManager inputManager;
    public GameObject player;
    Rigidbody rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<InputManager>();
        rb = player.GetComponent<Rigidbody>();
        bulletsLeft = magazineSize;
        readytoShot = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = inputManager.player.ShootBurst.IsPressed();
        }
        else shooting = inputManager.player.Shoot.triggered;

        if(inputManager.player.Reload.triggered && bulletsLeft < magazineSize && !reloading && rangeWeapon )
        {
            Reload();
        }

        if(readytoShot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShots = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readytoShot = false;

        //Spread
        
        float x = Random.Range(-spreadX, spreadX);
        float y = Random.Range(-spreadY, spreadY);
        //Calculate Direction with spreading
        Vector3 direction = cam.transform.forward + new Vector3(x,y,0);
        //Raycast
        if (Physics.Raycast(cam.transform.position, direction, out hit, range, ~playerMask))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        //Graphics
        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
        if(rangeWeapon) Instantiate(muzzleFlash, muzzle.position, Quaternion.identity);
        if (rangeWeapon)
        {
            bulletsLeft--;
        }
            bulletsShots--;
        
        Invoke("ResetShot", timeBetweenShooting);
        if(bulletsShots > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);


    }
    private void ResetShot()
    {
        readytoShot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinish", reloadTime);
    }

    private void ReloadFinish()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
