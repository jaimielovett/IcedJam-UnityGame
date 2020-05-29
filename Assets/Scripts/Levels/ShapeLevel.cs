using UnityEngine;
using System.Collections;

public class ShapeLevel : Level
{
    public ShapeLevel() : base() {

        InitiateRandomNumberOfShapes();
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
        ColouredShapesController.Instance.SetRandomTargetShape();
    }
}
