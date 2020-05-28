﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OddOneOutLevel : Level
{
    // This is the main shape that will be used.
    private string _colouredShapeToUse;
    // This is the odd shape that will be used (the one the user needs to click to complete the level).
    private string _oddColouredShapeToUse;

    public OddOneOutLevel() : base()
    {
        switch (GameController.Instance.Difficulty)
        {
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

        // Select the shape at random we're going to be using as our main shape in the scene.
        int numShapeTypes = 3;
        int numColourTypes = 7;
        _colouredShapeToUse = ChooseColouredShape(numShapeTypes, numColourTypes);

        // Select the odd coloured shape we're going to use.
        _oddColouredShapeToUse = ChooseColouredShapeForOddOneOutTarget();

        // Add the main shape and odd shape to the temporary list and initiate them.
        List<string> tempList = new List<string>();
        for (int i = 0; i < MaxNumShapesForLevel; i++)
        {
            tempList.Add(_colouredShapeToUse);
        }
        tempList.Add(_oddColouredShapeToUse);
        InitiateShapes(tempList);

        ColouredShapesController.Instance.SetTargetColouredShape(_oddColouredShapeToUse);
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
    }

    // Chooses a coloured shape to be the odd one out target. Takes into account the main shape being used already
    // as to not pick the same shape.
    private string ChooseColouredShapeForOddOneOutTarget()
    {
        string oddShapeToUse = "";
        if (_colouredShapeToUse.Contains("Red"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Red", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Green"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Green", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Blue"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Blue", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Pink"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Pink", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Orange"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Orange", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Yellow"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Yellow", _colouredShapeToUse);
        }
        else if (_colouredShapeToUse.Contains("Purple"))
        {
            oddShapeToUse = SelectRandomShapeByColour("Purple", _colouredShapeToUse);
        }
        return oddShapeToUse;
    }

    // Selects a random shape by passing in a colour and the main shape in the scene.
    // This will ensure that the odd shape to use will be of the same colour as the main shape
    // but obviously won't be the same as the main shape.
    private string SelectRandomShapeByColour(string colour, string mainShape)
    {
        string randomShape = "";
        int randomNum = Random.Range(0, ColouredShapesController.NUM_DIFF_SHAPES - 1);
        List<string> shapes = new List<string>();
        List<string> colouredShapesList = new List<string>(ColouredShapesController.Instance.GetColouredShapeVariationsList());

        for (int i = 0; i < colouredShapesList.Count; i++)
        {
            if (colouredShapesList[i].Contains(colour) && colouredShapesList[i] != _colouredShapeToUse)
                shapes.Add(colouredShapesList[i]);
        }
        return randomShape = shapes[randomNum];
    }
}
