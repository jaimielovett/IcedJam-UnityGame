using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    private int _currentScore = 0;
    private int _totalScore = 0;
    private int _correctClickScore;
    private int _levelCompleteScore;

    public static ScoreController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {

        _currentScore = 0;

        CalculateCorrectClickScore();
        CalculateLevelCompleteScore();
    }

    public int TotalScore
    {
        get
        {
            return _totalScore;
        }
        set
        {
            _totalScore = value;
        }
    }

    public int CurrentScore {

        get {

            return _currentScore;
        }
        set {

            _currentScore = value;
        }
    }

    public int CorrectClickScore {

        get {

            return _correctClickScore;
        }
        set {

            _correctClickScore = value;
        }
    }

    public int LevelCompleteScore {

        get {

            return _levelCompleteScore * MultiplierController.Instance.Multiplier;
        }
        set {

            _levelCompleteScore = value;
        }
    }

    // Calculate the value for _correctClickScore based on the game difficulty.
    public void CalculateCorrectClickScore() {

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                CorrectClickScore = 1;
                break;

            case GameDifficulty.NORMAL:
                CorrectClickScore = 5;
                break;

            case GameDifficulty.HARD:
                CorrectClickScore = 10;
                break;

            case GameDifficulty.INSANE:
                CorrectClickScore = 20;
                break;
        }
    }

    // Calculate the value for _levelCompleteScore based on the game difficulty.
    public void CalculateLevelCompleteScore() {

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                LevelCompleteScore = 10;
                break;

            case GameDifficulty.NORMAL:
                LevelCompleteScore = 50;
                break;

            case GameDifficulty.HARD:
                LevelCompleteScore = 100;
                break;

            case GameDifficulty.INSANE:
                LevelCompleteScore = 200;
                break;
        }
    }
}
