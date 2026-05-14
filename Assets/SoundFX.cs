using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    //all the sound effects in the game
    public AudioClip playerHurtSound;
    public AudioClip enemyHurtSound;
    public AudioClip gameOverSound;
    public AudioClip gameFinishedSound;
    public AudioClip playerShootSound;
    public AudioClip ammoPickupSound;
    public AudioClip burgerPickupSound;

    public AudioSource audioSrc;
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
        //does nothing
    }
    public void PlayerShoot()
    {
        audioSrc.PlayOneShot(playerShootSound);
    }
}
