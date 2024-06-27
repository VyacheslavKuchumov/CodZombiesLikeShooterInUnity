using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    [SerializeField]
    public GameObject gun1;
    [SerializeField]
    public GameObject gun2;
    [SerializeField]
    public GameObject weaponHolder;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        Gun primaryGun = gun1.GetComponent<Gun>();
        Gun secondaryGun = gun2.GetComponent<Gun>();
        WeaponSwitching weaponSwitcher = weaponHolder.GetComponent<WeaponSwitching>();

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Sprint.canceled += ctx => motor.Sprint();
        onFoot.Fire.performed += ctx => primaryGun.Shoot();
        onFoot.Fire.canceled += ctx => primaryGun.NotShoot();

        onFoot.Interact.performed += ctx => primaryGun.Reload();

        onFoot.Fire.performed += ctx => secondaryGun.Shoot();
        onFoot.Fire.canceled += ctx => secondaryGun.NotShoot();

        onFoot.Interact.performed += ctx => secondaryGun.Reload();


        onFoot.ChangeWeapon.performed += ctx => weaponSwitcher.ChangeWeapon();

        onFoot.Aim.performed += ctx => look.Aim();
        onFoot.Aim.performed += ctx => primaryGun.AimingGun();
        onFoot.Aim.performed += ctx => secondaryGun.AimingGun();
        onFoot.Aim.canceled += ctx => look.StopAim();
        onFoot.Aim.canceled += ctx => primaryGun.NotAimingGun();
        onFoot.Aim.canceled += ctx => secondaryGun.NotAimingGun();
    }

    // Update is called once per frame
    private void Update()
    {
        motor.ProccesMove(onFoot.Movement.ReadValue<Vector2>());
        look.ProccesLook(onFoot.Look.ReadValue<Vector2>());
    }

/*    void FixedUpdate()
    {
        motor.ProccesMove(onFoot.Movement.ReadValue<Vector2>());   
    }

    private void LateUpdate()
    {
        look.ProccesLook(onFoot.Look.ReadValue<Vector2>());
    }*/

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
