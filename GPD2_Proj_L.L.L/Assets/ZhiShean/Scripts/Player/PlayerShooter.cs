using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed;
    public float fireRate;

    private Vector3 destination;
    private float timeToFire;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.5f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.5f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ShootProjectile()
    {
        currentAmmo--;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(firePoint);
    }

    void InstantiateProjectile(Transform firepoint)
    {
        var projectileObj = Instantiate (projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projectileSpeed;

        Destroy(projectileObj, 5f);
    }
}
