using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ViewInGame : MonoBehaviour
{
    public TextMeshProUGUI coinLabel;

    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highscoreLabel;
    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        TextMeshProUGUI[] allLabels = GetComponents<TextMeshProUGUI>();

    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            coinLabel.SetText(GameManager.instance.collectedCoins.ToString());
            scoreLabel.SetText((PlayerController.instance.GetDistance() + GameManager.instance.collectedCoins).ToString("F1"));
            highscoreLabel.SetText(PlayerPrefs.GetFloat("highscore", 0).ToString("F0"));
        }
    }
}
