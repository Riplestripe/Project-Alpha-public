using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public GameObject weaponHolder;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public bool isLocked = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  
    public void ProcessLook(Vector2 input)
    {
        float mouseY = input.y;
        float mouseX = input.x;
        if (!isLocked)
        {
            // ���������� �������� ��������� ����� ����
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            // ���������� � cam transform
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //������� ������ � ������� ������
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
    }

}
