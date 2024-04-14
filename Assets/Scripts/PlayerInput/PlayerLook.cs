using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public GameObject weaponHolder;
    private float xRotation = 0f;
    InputManager inputManager;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public bool isLocked;
    public float rotAmount = 2f;
    public Quaternion initialRot;
    public float smoothRotAmount = 0.25f;
    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        CameraRot();
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseY = input.y;
        float mouseX = input.x;
        if (!isLocked)
        {
            Vector3 v = cam.transform.rotation.eulerAngles;

            // Вычисления поворота персонажа вверх вниз
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            // Применение к cam transform
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, v.z);
            //Поворот модели в сторону камеры
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
    }
    void CameraRot()
    {
        float rotZ = inputManager.moveDirection.x * rotAmount;
        Quaternion finalRot = Quaternion.Euler(xRotation, 0, rotZ);
        cam.transform.localRotation = Quaternion.Lerp(initialRot, finalRot, smoothRotAmount);

    }
}
