// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Events
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    // Singleton
    static EndGameMenu instance;

    // Events
    public static Action newGameClicked;
    public static Action quitClicked;

    // Make sure to attach these Buttons in the Inspector
    public Button newGame, quit;

    // Output
    [SerializeField]
    public TextMeshProUGUI finalScoreText;
    [SerializeField]
    public TextMeshProUGUI dominantBandText;

    void Start()
    {
        // Singleton
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            //Calls the TaskOnClick method when you click the Button
            newGame.onClick.AddListener(TaskOnClickNewGame);
            quit.onClick.AddListener(TaskOnClickQuit);
        }
    }

    void TaskOnClickNewGame()
    {
        // Let the MenuManager know.
        newGameClicked?.Invoke();
    }
    void TaskOnClickQuit()
    {
        // Let the MenuManager know.
        quitClicked?.Invoke();
    }

    void DisplayFinalScore(int finalScore)
    {
        finalScoreText.text = finalScore.ToString();
        //Display Dominant Band
        dominantBandText.text = EmotivManager.instance.finalScore();
    }

    private void OnEnable()
    {
        TimeManager.sendFinalScore += DisplayFinalScore;
    }

    private void OnDisable()
    {
        TimeManager.sendFinalScore -= DisplayFinalScore;
    }
}
