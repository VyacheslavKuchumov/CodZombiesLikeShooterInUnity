using System;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public GameObject player;
    [Header("Sway Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityMultiplier;

    private Quaternion refRotation;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        
    }

    private void Update()
    {
        // get mouse input
        Vector2 playerInput = player.GetComponent<InputManager>().onFoot.Look.ReadValue<Vector2>() * sensitivityMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-playerInput.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(playerInput.x, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }
}