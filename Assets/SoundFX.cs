using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioClip playerHurtSound;
    public AudioClip enemyHurtSound;
    public AudioClip gameOverSound;
    public AudioClip gameFinishedSound;
    public AudioClip playerShootSound;
    public AudioClip ammoPickupSound;
    public AudioClip burgerPickupSound;

    public AudioSource audioSrc;

    void Start()
    {

    }
    public void PickUpAmmo()
    {
        audioSrc.PlayOneShot(ammoPickupSound);
    }
    public void PickUpBurger()
    {
        audioSrc.PlayOneShot(burgerPickupSound);
    }

    public void PlayerHurt()
    {
        audioSrc.PlayOneShot(playerHurtSound);
    }

    public void EnemyHit()
    {
        audioSrc.PlayOneShot(enemyHurtSound);
    }
    public void GameOver()
    {
        audioSrc.PlayOneShot(gameOverSound);
    }
    public void LevelComplete()
    {
        
    }
    public void PlayerShoot()
    {
        audioSrc.PlayOneShot(playerShootSound);
    }






}
