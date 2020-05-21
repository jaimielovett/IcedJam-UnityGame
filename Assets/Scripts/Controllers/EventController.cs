using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour {

    public static EventController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool OnMouseClickSizeLevel(string colouredShapeName) {

        if (colouredShapeName == ColouredShapesController.Instance.GetTargetColouredShape()) {

            ColouredShapesController.Instance.RemoveFromColouredShapeList(colouredShapeName);
            return true;
        }
        else {

            return false;
        }
    }

    public bool OnMouseClickColouredShape(string colouredShapeName) {

        if (colouredShapeName == ColouredShapesController.Instance.GetTargetColouredShape()) {

            ColouredShapesController.Instance.RemoveFromColouredShapeList(colouredShapeName);
            ColouredShapesController.Instance.SetRandomTargetColouredShape();
            return true;
        }
        else {

            return false;
        }
    }

    public bool OnMouseClickColour(string colour) {

        if (colour == ColouredShapesController.Instance.GetTargetColour()) {

            ColouredShapesController.Instance.RemoveFromColourList(colour);
            ColouredShapesController.Instance.SetRandomTargetColour();
            return true;
        }
        else {

            return false;
        }
    }

    public bool OnMouseClickShape(string shape) {

        if (shape == ColouredShapesController.Instance.GetTargetShape()) {

            ColouredShapesController.Instance.RemoveFromShapeList(shape);
            ColouredShapesController.Instance.SetRandomTargetShape();
            return true;
        }
        else
        {
            return false;
        }
    }
}
