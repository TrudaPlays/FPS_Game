using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerPickup : MonoBehaviour
{
    public int healthAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Find the ShootingController on the player (or its children)
            PlayerHealth health = other.GetComponentInChildren<PlayerHealth>();

            if (health != null)
            {
                health.AddHealth(healthAmount);
                // AUTOMATICALLY find the SoundFX script in the scene
                SoundFX sfx = FindObjectOfType<SoundFX>();
                if (sfx != null)
                {
                    // Plays the pickup sound here
                    sfx.PickUpBurger();
                }
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}
