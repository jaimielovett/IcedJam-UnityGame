using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryLevel : Level {

    private List<string> memoryShapesList;
    private float _delayTimer = 0f;
    private int count = 0;

    public MemoryLevel() : base()
    {
        switch (GameController.Instance.Difficulty)
        {
            case GameDifficulty.EASY:
                MinNumShapesForLevel = 3;
                MaxNumShapesForLevel = 3;
                TimerController.Instance.CorrectClickTimerIncrease = 5.0f;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = 4;
                MaxNumShapesForLevel = 4;
                TimerController.Instance.CorrectClickTimerIncrease = 3.0f;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 5;
                TimerController.Instance.CorrectClickTimerIncrease = 2.0f;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = 6;
                MaxNumShapesForLevel = 6;
                TimerController.Instance.CorrectClickTimerIncrease = 1.0f;
                break;
        }

        ColouredShapesController.Instance.ShuffleColouredShapeVariationsList();
        memoryShapesList = ColouredShapesController.Instance.GetColouredShapeVariationsList();
        for (int i = memoryShapesList.Count; i > MaxNumShapesForLevel; i--)
        {
            memoryShapesList.RemoveAt(0);
        }

        // Set the Remaining Time to be equal to the number of shapes shown + 1 multiplied by the correct timer increase / 2.
        // The timer is hidden while the shapes are being shown, and then after the last shape is shown the timer is shown.
        // This means that by the time that the timer is shown, the correct amount of time left will be shown.
        TimerController.Instance.RemainingTime = (memoryShapesList.Count + 1) * TimerController.Instance.CorrectClickTimerIncrease / 2;
    }

    public override void Update()
    {
        if (count < memoryShapesList.Count)
        {
            if (TimerController.Instance.LevelElapsedTime > _delayTimer)
            {
                _delayTimer = TimerController.Instance.CorrectClickTimerIncrease / 2;
                TimerController.Instance.ResetLevelElapsedTimer();
                InitiateShape(memoryShapesList[count]);
                count++;
            }
        }
        else if (ColouredShapesController.Instance.GetColouredShapeList().Count > 0)
        {
            GameController.Instance.IsMemoryLevelActive = true;
            ColouredShapesController.Instance.SetTargetColouredShape(ColouredShapesController.Instance.GetColouredShapeList()[0]);
        }
        else
        {
            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
