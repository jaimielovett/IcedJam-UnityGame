using UnityEngine;
using System.Collections;

public class AvoidLevel : Level {

    private float _delayTimer = 1.0f;

	// Use this for initialization
	public AvoidLevel()
    {
        switch (GameController.Instance.Difficulty)
        {

            case GameDifficulty.EASY:
                MinNumShapesForLevel = ConfigConstants.k_EasyDifficultyAvoidLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_EasyDifficultyAvoidLevelMaxShapes;
                TimerController.Instance.RemainingTime = ConfigConstants.k_EasyDifficultyAvoidLevelTime;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = ConfigConstants.k_NormalDifficultyAvoidLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_NormalDifficultyAvoidLevelMaxShapes;
                TimerController.Instance.RemainingTime = ConfigConstants.k_NormalDifficultyAvoidLevelTime;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = ConfigConstants.k_HardDifficultyAvoidLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_HardDifficultyAvoidLevelMaxShapes;
                TimerController.Instance.RemainingTime = ConfigConstants.k_HardDifficultyAvoidLevelTime;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = ConfigConstants.k_InsaneDifficultyAvoidLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_InsaneDifficultyAvoidLevelMaxShapes;
                TimerController.Instance.RemainingTime = ConfigConstants.k_InsaneDifficultyAvoidLevelTime;
                break;
        }

        InitiateShapesForAvoidLevel();
        TimerController.Instance.ResetLevelElapsedTimer();
    }

    public override void Update()
    {
        if (TimerController.Instance.LevelElapsedTime > _delayTimer)
            GameController.Instance.IsAvoidLevelActive = true;

        if (TimerController.Instance.RemainingTime <= 0 && GameController.Instance.State != GameState.GAME_OVER)
        {
            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
