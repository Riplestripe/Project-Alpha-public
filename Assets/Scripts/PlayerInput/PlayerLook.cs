using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public bool isLocked = false;

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
