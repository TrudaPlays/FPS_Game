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
                    // Plays the pickup sound here
                    sfx.PickUpAmmo(); 
                }
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //makes the ammo spin around and hover 
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}
