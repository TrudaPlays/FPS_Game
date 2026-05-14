using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool isLevelComplete;
    public float numEnemies;
    public float numEnemiesDead;
    public float enemiesLeftToKill;
    public GameObject levelCompleteScreen;
    public LevelController levelController;
    public TextMeshProUGUI enemyText;
    public SoundFX soundFX;

    // Start is called before the first frame update
    void Start()
    {
        isLevelComplete = false;
        Time.timeScale = 1f;
        levelCompleteScreen.SetActive(false);
        foreach(GameObject enemy in enemies)
        {
            numEnemies++;
            enemiesLeftToKill++;
        }
        Debug.Log("number of enemies = " + numEnemies);
        Debug.Log("number of enemies left = " + enemiesLeftToKill);
        enemyText.text = "Enemy count= " + enemiesLeftToKill;
    }

    // Update is called once per frame
    void Update()
    {
        if(numEnemiesDead == numEnemies)
        {
            levelController.EndLevel();
            soundFX.LevelComplete();
            Time.timeScale = 0f;
            Debug.Log("Level Complete");
            isLevelComplete = true;
            levelCompleteScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
