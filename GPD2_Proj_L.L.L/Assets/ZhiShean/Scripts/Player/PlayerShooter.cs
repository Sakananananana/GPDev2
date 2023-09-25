using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TMP_Text ammoText;

    public Animator animator;

    public AudioSource SFX;

    public AudioClip[] gunSFX;

    public GameObject pauseMenu;
    public GameObject note1Panel;
    public GameObject note2Panel;
    public GameObject note3Panel;

    public GameObject startingPanel;
    public GameObject endingPanel;

    ObjectInteraction oI;

    // Start is called before the first frame update
    void Start()
    {
        oI = GetComponent<ObjectInteraction>();

        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.activeSelf && !note1Panel.activeSelf && !note2Panel.activeSelf && !note3Panel.activeSelf && !startingPanel.activeSelf && !endingPanel.activeSelf && !oI.isDead)
        {
            ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();

            if (isReloading)
            {
                return;
            }

            if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo))
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                ShootProjectile();
            }
        }
    }

    IEnumerator Reload()
    {
        SFX.PlayOneShot(gunSFX[1]);

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
        SFX.PlayOneShot(gunSFX[0]);

        currentAmmo--;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
