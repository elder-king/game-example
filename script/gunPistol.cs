using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class gunPistol : MonoBehaviour
{
    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int mag_size = 10;
    public int current_magAmmo = 30;
    public Text mag_counter;
    public Text curennt_ammo;
    public int amountToRefill;
    [Range(0.0f, 3.0f)]
    public float bulletSpread;

    public float reloadTime = 2f;
    public bool isReloading;
    bool can_reload = true;
    bool isShooting;

    //public Animator animator;

    InputAction shoot;

    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

        currentAmmo = mag_size * 2;
    }
    private void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("isReloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        mag_counter.text = current_magAmmo.ToString();
        curennt_ammo.text = currentAmmo.ToString();

        if (current_magAmmo == 0 && !isReloading)
        {

            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && current_magAmmo != mag_size && !isReloading)
        {

            StartCoroutine(Active_Reload());
        }

        if (isReloading)
            return;


        if (Input.GetButtonDown("Fire1") && current_magAmmo != 0 && Time.time >= nextTimeToFire)
        {
            isShooting = true;
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
        else
        {
            isShooting = false;
        }

        //animator.SetBool("isShooting", isShooting);
    }

    private void Fire()
    {
        muzzleFlash.Play();
        current_magAmmo--;
        Vector3 ShootDiraction = fpsCam.transform.forward;
        ShootDiraction.x += Random.Range(-bulletSpread, bulletSpread);
        ShootDiraction.y += Random.Range(-bulletSpread, bulletSpread);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position + ShootDiraction, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);
        }
    }
    IEnumerator Active_Reload()
    {
        if (can_reload == true)
            if (currentAmmo >= 1)
            {
                isReloading = true;
                //animator.SetBool("isReloading", true);
                yield return new WaitForSeconds(reloadTime);
                int Reload_amount = mag_size - current_magAmmo;
                Reload_amount = (currentAmmo - Reload_amount) >= 0 ? Reload_amount : currentAmmo;
                current_magAmmo += Reload_amount;
                currentAmmo -= Reload_amount;
                
                //animator.SetBool("isReloading", false);
                isReloading = false;
            }
    }
    IEnumerator Reload()
    {
        if (can_reload == true)
            if (currentAmmo >= 1)
            {
                isReloading = true;
                //animator.SetBool("isReloading", true);
                yield return new WaitForSeconds(reloadTime);
                int Reload_amount = mag_size - current_magAmmo;
                Reload_amount = (currentAmmo - Reload_amount) >= 0 ? Reload_amount : currentAmmo;
                current_magAmmo += Reload_amount;
                currentAmmo -= Reload_amount;

                //animator.SetBool("isReloading", false);
                isReloading = false;
            }
    }
    public void ammo_refall()
    {
        currentAmmo += amountToRefill;
        can_reload = true;
    }
}