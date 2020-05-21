using UnityEngine;
using System.Collections;

public class AvoidLevel : Level {

    private float _delayTimer = 1.0f;

	// Use this for initialization
	public AvoidLevel() {

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 10;
                TimerController.Instance.RemainingTime = 4.0f;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = 10;
                MaxNumShapesForLevel = 15;
                TimerController.Instance.RemainingTime = 6.0f;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = 15;
                MaxNumShapesForLevel = 20;
                TimerController.Instance.RemainingTime = 8.0f;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = 20;
                MaxNumShapesForLevel = 30;
                TimerController.Instance.RemainingTime = 10.0f;
                break;
        }

        InitiateShapesForAvoidLevel();
        TimerController.Instance.ResetLevelElapsedTimer();
    }

    public override void Update() {

        if (TimerController.Instance.LevelElapsedTime > _delayTimer)
            GameController.Instance.IsAvoidLevelActive = true;

        if (TimerController.Instance.RemainingTime <= 0 && GameController.Instance.State != GameState.GAME_OVER) {

            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
