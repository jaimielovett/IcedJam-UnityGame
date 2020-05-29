using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProximityLevel : Level {

    private float _delayTimer = 1.0f;

    public ProximityLevel()
    {
        ColouredShapesController.Instance.AddToColouredShapeList("Proximity Circle");

        List<string> tempList = ColouredShapesController.Instance.GetColouredShapeList();
        for (int i = 0; i < tempList.Count - 1; i++)
        {

            tempList.RemoveAt(0);
        }

        switch (GameController.Instance.Difficulty) 
        {
            case GameDifficulty.EASY:
                TimerController.Instance.RemainingTime = ConfigConstants.k_EasyDifficultyProximityLevelTime;
                break;

            case GameDifficulty.NORMAL:
                TimerController.Instance.RemainingTime = ConfigConstants.k_NormalDifficultyProximityLevelTime;
                break;

            case GameDifficulty.HARD:
                TimerController.Instance.RemainingTime = ConfigConstants.k_HardDifficultyProximityLevelTime;
                break;

            case GameDifficulty.INSANE:
                TimerController.Instance.RemainingTime = ConfigConstants.k_InsaneDifficultyProximityLevelTime;
                break;
        }

        InitiateShape(tempList[0]);
    }

    public override void Update() 
    {
        if (TimerController.Instance.LevelElapsedTime > _delayTimer)
            GameController.Instance.IsProximityLevelActive = true;

        if (TimerController.Instance.RemainingTime <= 0 && GameController.Instance.State != GameState.GAME_OVER)
        {
            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
