using UnityEngine;
using System.Collections;

public class ColouredShapeLevel : Level {

    public ColouredShapeLevel() : base() {

        switch(GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                MinNumShapesForLevel = ConfigConstants.k_EasyDifficultyLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_EasyDifficultyLevelMaxShapes;
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_EasyDifficultyLevelTime;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = ConfigConstants.k_NormalDifficultyLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_NormalDifficultyLevelMaxShapes;
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_NormalDifficultyLevelTime;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = ConfigConstants.k_HardDifficultyLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_HardDifficultyLevelMaxShapes;
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_HardDifficultyLevelTime;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = ConfigConstants.k_InsaneDifficultyLevelMinShapes;
                MaxNumShapesForLevel = ConfigConstants.k_InsaneDifficultyLevelMaxShapes;
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_InsaneDifficultyLevelTime;
                break;
        }

        InitiateRandomNumberOfShapes();
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
        ColouredShapesController.Instance.SetRandomTargetColouredShape();
    }

    public override void Update()
    {
        base.Update();
    }
}
