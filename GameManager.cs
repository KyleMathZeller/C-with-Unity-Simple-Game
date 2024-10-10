using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public int collectedCoins = 0;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentGameState = GameState.menu;
    }
    //Method to start the game
    public void StartGame()
    {
        PlayerController.instance.StartGame();
        SetGameState(GameState.inGame);
    }
    //Method for when the player dies
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    //Method for when condition to return to the main menu is incurred
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            //setup unity scene for main menu
            menuCanvas.enabled = true;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == GameState.inGame)
        {
            //setup unity scene in game
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        //Changed to else to prevent an invalid game state from existing change back is further states added
        else if (newGameState == GameState.gameOver)
        {
            //setup unity scene for game over
            menuCanvas.enabled = false;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }
        currentGameState = newGameState;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("s"))
        {
            StartGame();
        }
    }
    public void CollectedCoin()
    {
        collectedCoins++;
    }
}
