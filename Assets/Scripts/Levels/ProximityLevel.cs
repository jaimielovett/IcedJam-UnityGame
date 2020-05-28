using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProximityLevel : Level {

    private float _delayTimer = 1.0f;

    public ProximityLevel() {

        ColouredShapesController.Instance.AddToColouredShapeList("Proximity Circle");
        //ColouredShapesController.Instance.AddToColouredShapeList("Proximity Square");
        //ColouredShapesController.Instance.AddToColouredShapeList("Proximity Triangle");
        //ColouredShapesController.Instance.ShuffleColouredShapeList();

        List<string> tempList = ColouredShapesController.Instance.GetColouredShapeList();
        for (int i = 0; i < tempList.Count - 1; i++) {

            tempList.RemoveAt(0);
        }

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:

                TimerController.Instance.RemainingTime = 4.0f;
                break;

            case GameDifficulty.NORMAL:
                TimerController.Instance.RemainingTime = 3.0f;
                break;

            case GameDifficulty.HARD:
                TimerController.Instance.RemainingTime = 8.0f;
                break;

            case GameDifficulty.INSANE:
                TimerController.Instance.RemainingTime = 10.0f;
                break;
        }

        InitiateShape(tempList[0]);
    }

    public override void Update() {

        if (TimerController.Instance.LevelElapsedTime > _delayTimer)
            GameController.Instance.IsProximityLevelActive = true;

        if (TimerController.Instance.RemainingTime <= 0 && GameController.Instance.State != GameState.GAME_OVER) {

            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }
}
