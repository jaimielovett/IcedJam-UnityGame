using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReactionLevel : Level {

    public readonly float levelTimer;
    private float minTimer = 1.0f;
    private float maxTimer = 5.0f;

    private string _colouredShapeName;

    public ReactionLevel() : base() {

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                minTimer = 3.0f;
                maxTimer = 6.0f;
                MaxNumShapesForLevel = 1;
                break;

            case GameDifficulty.NORMAL:
                minTimer = 1.0f;
                maxTimer = 4.0f;
                MaxNumShapesForLevel = 1;
                break;

            case GameDifficulty.HARD:
                minTimer = 1.0f;
                maxTimer = 3.0f;
                MaxNumShapesForLevel = 1;
                break;

            case GameDifficulty.INSANE:
                minTimer = 1.0f;
                maxTimer = 2.0f;
                MaxNumShapesForLevel = 1;
                break;
        }

        List<string> colouredShapesVariationList = new List<string>(ColouredShapesController.Instance.GetColouredShapeVariationsList());
        _colouredShapeName = colouredShapesVariationList[Random.Range(0, colouredShapesVariationList.Count - 1)];
        ColouredShapesController.Instance.AddToColouredShapeList(_colouredShapeName);
        levelTimer = Random.Range(minTimer, maxTimer);
        TimerController.Instance.RemainingTime = levelTimer;
        ColouredShapesController.Instance.SetTargetColouredShape(_colouredShapeName);
    }

    public void ShowColouredShape()
    {
        InitiateShapesWithRandomPosition();
    }
}
