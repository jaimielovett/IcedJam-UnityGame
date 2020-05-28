using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SizeLevel : Level {

    public SizeLevel() {

        MinNumShapesForLevel = 5;
        MaxNumShapesForLevel = 5;

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_EasyDifficultyLevelTime;
                break;

            case GameDifficulty.NORMAL:
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_NormalDifficultyLevelTime;
                break;

            case GameDifficulty.HARD:
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_HardDifficultyLevelTime;
                break;

            case GameDifficulty.INSANE:
                TimerController.Instance.CorrectClickTimerIncrease = ConfigConstants.k_HardDifficultyLevelTime;
                break;
        }

        colouredShapeScale = 2.0f;
        colouredShapeScaleDecrement = -0.5f;

        // Shuffle the coloured shape variations list and then store it in a temp list.
        // Remove shapes from the temp list until we've got the same amount in the temp list
        // as the max number of shapes for the level. Then set the coloured shape list to our temp list.
        ColouredShapesController.Instance.ShuffleColouredShapeVariationsList();
        List<string> tempList = ColouredShapesController.Instance.GetColouredShapeVariationsList();
        for (int i = tempList.Count; i > MaxNumShapesForLevel; i--)
        {
            tempList.RemoveAt(0);
        }

        InitiateShapes(tempList);
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;

        if (GameController.Instance.CurrentLevel == LevelType.LARGEST_SIZE) {

            ColouredShapesController.Instance.SetTargetColouredShape(ColouredShapesController.Instance.GetColouredShapeList()[0]);
        }
        else if (GameController.Instance.CurrentLevel == LevelType.SMALLEST_SIZE) {

            ColouredShapesController.Instance.SetTargetColouredShape(ColouredShapesController.Instance.GetColouredShapeList()[ColouredShapesController.Instance.GetColouredShapeList().Count - 1]);
        }
    }

    public override void Update() {

        if (ColouredShapesController.Instance.GetColouredShapeList().Count > 0) {

            if (GameController.Instance.CurrentLevel == LevelType.LARGEST_SIZE) {

                ColouredShapesController.Instance.SetTargetColouredShape(ColouredShapesController.Instance.GetColouredShapeList()[0]);
            }
            else if (GameController.Instance.CurrentLevel == LevelType.SMALLEST_SIZE) {

                ColouredShapesController.Instance.SetTargetColouredShape(ColouredShapesController.Instance.GetColouredShapeList()[ColouredShapesController.Instance.GetColouredShapeList().Count - 1]);
            }
        }
        else {

            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
