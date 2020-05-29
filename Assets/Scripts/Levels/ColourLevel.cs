using UnityEngine;
using System.Collections;

public class ColourLevel : Level {

    public ColourLevel() : base()
    {
        InitiateRandomNumberOfShapes();
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
        ColouredShapesController.Instance.SetRandomTargetColour();
    }
}
