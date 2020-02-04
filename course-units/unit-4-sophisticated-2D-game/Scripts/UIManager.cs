using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text livesText;
    public Text scoreText;

    void Awake()
    {
        //If a UI Manager exists and this isn't it
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //Set this as the current UI Manager
        instance = this;

        //Persis this object between scene reloads
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + 5;
        scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLivesText(int playerLives)
    {
        livesText.text = "Lives: " + playerLives.ToString();
    }

    public void UpdateScoreText(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }
}
