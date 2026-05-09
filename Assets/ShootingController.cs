using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ShootingController : MonoBehaviour
{
    public Transform shootPoint;
    public float fireRate;
    public float Damage;
    public float fireRange;
    public bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isFiring == false)
        {
            StartCoroutine(Fire());
        }
    }

    public IEnumerator Fire()
    {
        isFiring = true;
        // Store hit information
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, fireRange))
        {
            Debug.Log("SHot fired!");
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy is hit!" + hit.collider.name);
                // Example: Change the color of the object we hit
                hit.collider.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        // Visualize the ray in the Scene view
        Debug.DrawRay(shootPoint.position, shootPoint.forward * fireRange, Color.green);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }
}
