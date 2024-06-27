using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private Transform muzzle;
    private bool isShooting;
    private float timeSinceLastShot;
    private float timeSinceReloadingStart;

    private bool isAiming;

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Reload();
    }

    private void Update()
    {
        gunData.currentAmmo = Mathf.Clamp(gunData.currentAmmo, 0, gunData.magSize);
        timeSinceLastShot += Time.deltaTime;

        if (isShooting && CanShoot())
        {
            ShootGun();
        }

        if (!gunData.reloading && gunData.currentAmmo == 0)
        {
            Reload();
        }

        if (gunData.reloading)
        {
            HandleReloading();
        }
    }

    private void ShootGun()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
        {
            Debug.Log(hitInfo.transform.tag);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(gunData.damage);
                    Debug.Log("Hitting enemy: " + hitInfo.collider.gameObject.name);
                }
            }
        }

        gunData.currentAmmo--;
        Debug.Log(gunData.currentAmmo);
        timeSinceLastShot = 0;
        OnGunShot();
    }

    private void HandleReloading()
    {
        timeSinceReloadingStart += Time.deltaTime;
        Debug.Log("Reloading....");
        if (timeSinceReloadingStart >= gunData.reloadTime)
        {
            Debug.Log("Reloading finished!");
            gunData.currentAmmo = gunData.magSize;
            timeSinceReloadingStart = 0;
            gunData.reloading = false;
        }
    }

    private void OnGunShot()
    {
        Transform gunBarrel = muzzle;
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, transform.rotation);
        Vector3 shootDirection = gunBarrel.transform.forward;
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(UnityEngine.Random.Range(-3f, 3f), Vector3.up) * shootDirection * 180;
    }

    private bool CanShoot()
    {
        return !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && gunData.currentAmmo > 0;
    }

    private bool CanReload()
    {
        return !gunData.reloading && gunData.currentAmmo < gunData.magSize;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            isShooting = true;
        }
        
    }

    public void NotShoot()
    {
        
        isShooting = false;
        

    }

    public void Reload()
    {
        if (CanReload())
        {
            gunData.currentAmmo = 0;
            isShooting = false;
            gunData.reloading = true;
            timeSinceReloadingStart = 0;
            Debug.Log("Initiating reloading");
        }
    }

    public void AimingGun()
    {
        isAiming = true;
        gameObject.GetComponent<Animator>().SetBool("IsAiming", isAiming);
    }
    public void NotAimingGun()
    {
        isAiming = false;
        gameObject.GetComponent<Animator>().SetBool("IsAiming", isAiming);
    }
}
