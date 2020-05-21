using UnityEngine;
using System.Collections;

public class ColourLevel : Level {

    public ColourLevel() : base()
    {
        switch (GameController.Instance.Difficulty)
        {
            case GameDifficulty.EASY:
                MinNumShapesForLevel = 3;
                MaxNumShapesForLevel = 6;
                TimerController.Instance.CorrectClickTimerIncrease = 5.0f;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = 6;
                MaxNumShapesForLevel = 9;
                TimerController.Instance.CorrectClickTimerIncrease = 3.0f;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = 9;
                MaxNumShapesForLevel = 12;
                TimerController.Instance.CorrectClickTimerIncrease = 2.0f;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = 12;
                MaxNumShapesForLevel = 15;
                TimerController.Instance.CorrectClickTimerIncrease = 1.0f;
                break;
        }

        InitiateRandomNumberOfShapes();
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
        ColouredShapesController.Instance.SetRandomTargetColour();
    }
}
