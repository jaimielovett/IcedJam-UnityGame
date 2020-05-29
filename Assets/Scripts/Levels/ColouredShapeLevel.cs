using UnityEngine;
using System.Collections;

public class ColouredShapeLevel : Level {

    public ColouredShapeLevel() : base() 
    {
        InitiateRandomNumberOfShapes();
        TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
        ColouredShapesController.Instance.SetRandomTargetColouredShape();
    }

    public override void Update()
    {
        base.Update();
    }
}
