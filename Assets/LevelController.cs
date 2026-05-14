using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    void Start()
    {

    }

    public void EndLevel()
    {
        // This stops the song immediately
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
