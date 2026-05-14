using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UI;
using TMPro;


public class ShootingController : MonoBehaviour
{
    public Transform shootPoint;
    public float fireRate;
    public float Damage;
    public float fireRange;
    public bool isFiring = false;
    public SoundFX soundFX;
    public TextMeshProUGUI ammoText;
    public float ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = 200f;
        ammoText.text = "Ammo: " + ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isFiring == false && ammoCount > 0)
        {
            StartCoroutine(Fire());
        }
        else if(ammoCount <= 0)
        {
            ammoText.text = "Out of ammo!! Ammo: " + ammoCount.ToString();
        }
    }

    public IEnumerator Fire()
    {
        soundFX.PlayerShoot();
        isFiring = true;
        // Store hit information
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, fireRange))
        {
            UpdateAmmoHUD();
            // 1. See EXACTLY what you hit, regardless of tag
            Debug.Log("Raycast hit: " + hit.collider.name + " with Tag: " + hit.collider.tag);
            if (hit.collider.CompareTag("Enemy"))
            {
                // Try to find the EnemyHealth script on the object we hit
                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

                enemy.TakeDamage(Damage);
                Debug.Log("Enemy damaged!");
            }
        }
        // Visualize the ray in the Scene view
        Debug.DrawRay(shootPoint.position, shootPoint.forward * fireRange, Color.green);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        Debug.Log("Ammo Picked Up! Current Ammo: " + ammoCount);
        ammoText.text = "Ammo Picked Up! Current Ammo: " + ammoCount.ToString();
    }
    public void UpdateAmmoHUD()
    {
        ammoCount -= 8;
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + ammoCount.ToString();
        }

    }
}
