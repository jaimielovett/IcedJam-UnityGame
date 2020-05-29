﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReactionLevel : Level {

    public readonly float levelTimer;
    private float minTimer = 1.0f;
    private float maxTimer = 5.0f;

    private string _colouredShapeName;

    public ReactionLevel() : base() 
    {
        MaxNumShapesForLevel = 1;

        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                minTimer = ConfigConstants.k_EasyDifficultyReactionLevelMinTime;
                maxTimer = ConfigConstants.k_EasyDifficultyReactionLevelMaxTime;
                break;

            case GameDifficulty.NORMAL:
                minTimer = ConfigConstants.k_NormalDifficultyReactionLevelMinTime;
                maxTimer = ConfigConstants.k_NormalDifficultyReactionLevelMaxTime;
                break;

            case GameDifficulty.HARD:
                minTimer = ConfigConstants.k_HardDifficultyReactionLevelMinTime;
                maxTimer = ConfigConstants.k_HardDifficultyReactionLevelMaxTime;
                break;

            case GameDifficulty.INSANE:
                minTimer = ConfigConstants.k_InsaneDifficultyReactionLevelMinTime;
                maxTimer = ConfigConstants.k_InsaneDifficultyReactionLevelMaxTime;
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
