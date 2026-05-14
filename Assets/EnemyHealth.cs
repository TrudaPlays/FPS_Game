using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Color flashColor = Color.red;
    public Color normalColor = Color.white;
    public float flashDuration;

    public TextMeshProUGUI enemyText;

    public EnemyTracker enemyTrackerScript;

    public SoundFX soundFX;

    public GameObject deathEffect;

    private Renderer[] enemyRenderers;
    private List<Color> originalColors = new List<Color>();

    public GameObject ammoObject;
    public GameObject burgerObject;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        // We grab the Renderer so we can change the color
        enemyRenderers = GetComponentsInChildren<Renderer>();
        // Store the starting color of every material on every part of the fish
        foreach (Renderer rend in enemyRenderers)
        {
            foreach (Material mat in rend.materials)
            {
                originalColors.Add(mat.color);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        soundFX.EnemyHit();
        // Trigger the red flash
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
            enemyTrackerScript.numEnemiesDead++;
            enemyTrackerScript.enemiesLeftToKill--;
            enemyText.text = "Enemies left: " + enemyTrackerScript.enemiesLeftToKill;
        }
    }

    IEnumerator FlashRed()
    {
        // 3. Loop through every part and make it red
        foreach (Renderer rend in enemyRenderers)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_BaseColor", flashColor);
            }
        }
        // Wait for a tiny bit
        yield return new WaitForSeconds(flashDuration);
        // 2. Restore exactly what was there before
        int colorIndex = 0;
        foreach (Renderer rend in enemyRenderers)
        {
            foreach (Material mat in rend.materials)
            {
                mat.color = originalColors[colorIndex];
                colorIndex++;
            }
        }
    }

    void Die()
    {
        //spawns some ammo or a burger at the player's feet for as a bonus for defeating the enemy!
        Vector3 spawnPos = transform.position + new Vector3(0, 0.2f, 0);
        float drop;
        drop = Random.Range(0, 2);
        if(drop == 0)
        {
            Instantiate(ammoObject, spawnPos, Quaternion.identity);
        }
        else if (drop == 1)
        {
            Instantiate(burgerObject, spawnPos, Quaternion.identity);
        }
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        // Destroy the enemy object LAST or we run into nullreference errors...eeek!!
        Destroy(gameObject);
    }
}
