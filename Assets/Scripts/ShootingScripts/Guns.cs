using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Guns : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject bulletPoint;
    public float bulletSpeed;
    [SerializeField] private bool pistol;
    [SerializeField] private bool shootGun;
    public float damage;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (pistol)
        {
            GameObject bullet = Instantiate(bulletPref, bulletPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Destroy(bullet, 1f);
        }
    }
}
