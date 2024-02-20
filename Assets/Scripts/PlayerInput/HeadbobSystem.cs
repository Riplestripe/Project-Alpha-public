using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbobSystem : MonoBehaviour
{
    [Range(0.01f, 0.1f)] 
    public float amout = 0.02f;
    [Range(1f, 30f)]
    public float frequency = 10f;
    [Range(10f, 100f)]
    public float smooth = 10.0f;
    Vector3 StartPos;

    private void Start()
    {
        StartPos = transform.localPosition;
    }

    private void Update()
    {
        CheckForHeadbobTrigger();
        StopHeadBob();
    }

    private Vector3 StartHeadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amout * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amout * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }
    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0  )
        {
            StartHeadbob();
        }
    }
}
