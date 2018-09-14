using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text countText;
    public Text timerText;

    private PlayerController playerReference;

    public bool gameEnd;

    private int timer;

    private void Awake()
    {
        Time.timeScale = 1f;

        playerReference = FindObjectOfType<PlayerController>();
        
        gameEnd = false;

        timer = 90;

        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerReference.numberOfPickup > 0)
        {
            scoreText.text = "Player Score: " + playerReference.score;
            countText.text = "Number of Pickup Left: " + playerReference.numberOfPickup + "\nNumber of Harmful Objects Left: " + playerReference.numberOfHarmfulPickup;
            timerText.text = "Time: " + timer / 60 + " min " + timer % 60 + " sec";
        }
        if (playerReference.numberOfPickup <= 0)
        {
            scoreText.enabled = false;
            countText.enabled = false;
            timerText.enabled = false;

            gameEnd = true;
        }
    }

    IEnumerator CountDown()
    {
        for (int counter = timer; counter > 0; counter--)
        {
            if (timer > 0)
            {
                yield return new WaitForSeconds(1f);

                timer--;
            }

            if (timer <= 0)
            {
                gameEnd = true;
            }
        }
    }
}
