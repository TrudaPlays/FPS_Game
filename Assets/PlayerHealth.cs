using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject hurtScreen;
    public GameObject gameOverScreen;
    public SoundFX soundFX;
    public LevelController levelController;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        hurtScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        currentHealth = maxHealth;
        UpdateHealthHUD();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log("Food Picked Up! Current Health: " + currentHealth);
        healthText.text = "Food Picked Up! Current Health: " + currentHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        soundFX.PlayerHurt();
        StartCoroutine(ScreenFlashRed());
        UpdateHealthHUD();
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            PlayerHasDied();
            soundFX.GameOver();
            Time.timeScale = 0f;
            // You could restart the level here
        }
    }

    void UpdateHealthHUD()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    public void PlayerHasDied()
    {
        levelController.EndLevel();
        Debug.Log("Player has died!");
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //flashes the screen red
    public IEnumerator ScreenFlashRed()
    {
        hurtScreen.SetActive (true);

        yield return new WaitForSeconds(0.2f);

        hurtScreen.SetActive(false);
    }
}
