using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    int score;
    public int scoreQuota = 16;

    public TextMeshProUGUI scoreUI;
    public GameObject winScreen;
    public GameObject loseScreen;


    private static float respawnTimerMax = 3.0f;
    private float respawnTimer = respawnTimerMax;


    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        scoreUI.SetText("Score: " + score.ToString() + "/" + scoreQuota.ToString());

        if (score >= scoreQuota)
            Win();
    }

    public void GameOver()
    {
        loseScreen.SetActive(true);
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0.0f)
            SceneManager.LoadScene(0);
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }
}
