using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public float adsMultiplier = 1;

    private float defaultFOV;
    private void Start()
    {
        defaultFOV = cam.fieldOfView;
    }

    public void ProccesLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void Aim()
    {
        if (cam != null)
        {
            cam.fieldOfView = 30;
            xSensitivity = xSensitivity / adsMultiplier;
            ySensitivity = ySensitivity / adsMultiplier;
        }
    }

    public void StopAim()
    {

        if (cam != null)
        {
            cam.fieldOfView = defaultFOV;
            
            xSensitivity = xSensitivity * adsMultiplier;
            ySensitivity = ySensitivity * adsMultiplier;
        }
    }
}
