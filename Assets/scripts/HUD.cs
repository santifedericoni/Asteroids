using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField]
    public Text scoreText;
    float score = 0;
    public const string scorePrefix = "Timer: ";
    //Timer initializer
    float elapsedSeconds = 0;
    //Stop Timer initializer

    bool gameTimerIsRunning;
    // Start is called before the first frame update
    void Start()
    {
        gameTimerIsRunning = true;
        scoreText.text = scorePrefix + score.ToString();
    }

    // Update is called once per frame

    void Update()
    {
        if (gameTimerIsRunning == true)
        {
            elapsedSeconds += Time.deltaTime;
            int timer = (int)elapsedSeconds;
            scoreText.text = timer.ToString();
        }
        else
        {
            StopGameTimer();
        }
    }

    public void StopGameTimer()
    {
        gameTimerIsRunning = false;
        elapsedSeconds = 0;
    }

}



