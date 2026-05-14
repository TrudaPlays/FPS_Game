using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //shows the instructions upon entering the game for a few seconds and then hides them
        StartCoroutine(ShowInstructions());
    }

    public IEnumerator ShowInstructions()
    {
        gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);

    }
}
