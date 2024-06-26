using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private Transform muzzle;
    private bool isShooting;
    private float timeSinceLastShot;
    private float timeSinceReloadingStart;

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

        if (!gunData.reloading && gunData.currentAmmo == 0)
        {
            Reload();
        }

        if (gunData.reloading)
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
    }

    private void OnGunShot()
    {
        Transform gunBarrel = muzzle;

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, transform.rotation);

        Vector3 shootDirection = (gunBarrel.transform.forward);

        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(UnityEngine.Random.Range(-3f, 3f), Vector3.up) * shootDirection * 180;

        // gunshot effects
    }

    private bool CanShoot()
    {
        
        if (!gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && gunData.currentAmmo > 0)
            return true;

        else
            if (gunData.currentAmmo == 0)
            {
                Debug.Log("No ammo!");
            }
        return false;

    }

    private bool CanReload()
    {
        if (!gunData.reloading && !isShooting && gunData.currentAmmo != gunData.magSize)
            return true;

        else
            return false;
    }

    public void Shoot()
    {
        isShooting = !isShooting;
        Debug.Log(isShooting);

    }
    public void Reload()
    {
        if (CanReload())
        {
            gunData.reloading = true;
            Debug.Log("Initiating reloading");
        }
        
        

    }
}
