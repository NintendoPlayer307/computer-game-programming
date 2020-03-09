//This script is a Manager that controls the flow and control of the game. It keeps track of 
//player data (item count, death, etc) and interfaces with the UI Manager. All game commands 
//are issued through the static method of this class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //This class hold a static reference to itself to ensure that there will only be existence.
    //This is often referred to as a "singleton" design pattern. Other scripts access this one 
    //through its public static methods.
    public static GameManager instance;

    public int _itemCount = 0;                              //How many items the player has collected
    public int _playerLives = 5;                            //How many lives the player has for the game

    private bool _isGameOver = false;                       //Is the game currently over?
    public float waitToRespawn = 0.5f;                      //How long before respawning player

    //public Checkpoints[] checkpoints;                     //An array of all the avaiable checkpoints
    public Vector2 spawnPoint;                              //Location for the player to respawn at

    void Awake()
    {
        //If a Game Manager exists and this isn't it
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //Set this as the current Game Manager
        instance = this;

        //Persis this object between scene reloads
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //checkpoints = FindObjectsOfType<Checkpoints>();
        //spawnPoint = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to handle players health in the game
    public void ProcessPlayerDeath()
    {
        if(_playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    // Method to take a life from the player
    private void TakeLife()
    {
        _playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        UIManager.instance.UpdateLivesText(_playerLives);
    }

    // Method to reset the game if player is out of lives
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
        UIManager.instance.UpdateLivesText(_playerLives);
    }

    // Method to add to item count
    public void AddToItemCount()
    {
        _itemCount++;
        UIManager.instance.UpdateScoreText(_itemCount);
    }

    //Method to set a new spawn position for the player
    public void SetSpawnPoint(Vector2 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelRoutine());
    }

    public IEnumerator EndLevelRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        UIManager.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIManager.instance.fadeSpeed) + 0.25f);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        SceneManager.LoadScene(1);
    }
}
