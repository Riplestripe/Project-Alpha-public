using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    public float rotAmount = 2f;
    public Quaternion initialRot;
    public float smoothRotAmount = 0.25f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        CameraRot();

        float mousX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mousY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        xRotation += mousX;
        yRotation -= mousY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Vector3 v = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, v.y);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
    void CameraRot()
    {
        float rotZ = -Input.GetAxis("Horizontal") * rotAmount;
        Quaternion finalRot = Quaternion.Euler(xRotation, 0, rotZ);
        transform.localRotation = Quaternion.Lerp(initialRot, finalRot, smoothRotAmount);

    }
}
