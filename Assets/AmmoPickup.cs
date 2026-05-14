using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Find the ShootingController on the player (or its children)
            ShootingController shooter = other.GetComponentInChildren<ShootingController>();

            if (shooter != null)
            {
                shooter.AddAmmo(ammoAmount);
                // AUTOMATICALLY find the SoundFX script in the scene
                SoundFX sfx = FindObjectOfType<SoundFX>();
                if (sfx != null)
                {
                    // Play your pickup sound here
                    sfx.PickUpAmmo(); 
                }
                Destroy(gameObject);

                // Optional: Play a sound effect from your SoundFX script here
                // FindObjectOfType<SoundFX>().PlayPickupSound();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}
