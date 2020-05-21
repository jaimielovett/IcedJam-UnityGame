using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SizeLevel : Level {

    public SizeLevel() {

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 5;
                TimerController.Instance.CorrectClickTimerIncrease = 5.0f;
                break;

            case GameDifficulty.NORMAL:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 5;
                TimerController.Instance.CorrectClickTimerIncrease = 3.0f;
                break;

            case GameDifficulty.HARD:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 5;
                TimerController.Instance.CorrectClickTimerIncrease = 2.0f;
                break;

            case GameDifficulty.INSANE:
                MinNumShapesForLevel = 5;
                MaxNumShapesForLevel = 5;
                TimerController.Instance.CorrectClickTimerIncrease = 1.0f;
                break;
        }

        colouredShapeScale = 0.6f;
        colouredShapeScaleDecrement = -0.15f;

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
