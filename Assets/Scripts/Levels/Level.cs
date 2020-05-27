using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level {

    // Constant string variables for the resource paths.
    protected const string COLOURED_SHAPES_PREFAB_PATH = "Shapes/";

    protected float colouredShapeScale = 0.0f;
    protected float colouredShapeScaleDecrement = 0.0f;

    // Min and Max shapes allowed per level.
    private int minNumShapesForLevel;
    private int maxNumShapesForLevel;

    public Level() {

        // Clear the shape lists and the elapsed timer before we start the level.
        ColouredShapesController.Instance.ClearAllLists();
        TimerController.Instance.ResetLevelElapsedTimer();

        ColouredShapesController.Instance.InitialiseLists();
        AddAllVariationsOfColouredShapesToColouredShapeVariationsList();
        AddAllVariationsToAvoidLevelShapesList();
    }

    public virtual void Update() {

    }

    protected int MinNumShapesForLevel {

        get {
            return minNumShapesForLevel;
        }
        set {
            minNumShapesForLevel = value;
        }
    }

    protected int MaxNumShapesForLevel {

        get {
            return maxNumShapesForLevel;
        }
        set {
            maxNumShapesForLevel = value;
        }
    }

    // All of the possible shapes we have in the game will be added to the coloured shapes variation list.
    // This can be used for levels that use all of the different kinds of shapes to instantiate them.
    protected void AddAllVariationsOfColouredShapesToColouredShapeVariationsList() {

        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Red Circle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Blue Circle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Green Circle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Pink Circle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Red Square");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Blue Square");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Green Square");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Pink Square");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Red Triangle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Blue Triangle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Green Triangle");
        ColouredShapesController.Instance.AddToColouredShapeVariationsList("Pink Triangle");

        if (RewardController.Instance.PurpleColourReward.IsUnlocked)
        {
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Purple Circle");
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Purple Square");
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Purple Triangle");
        }

        if (RewardController.Instance.PentagonShapeReward.IsUnlocked)
        {
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Red Pentagon");
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Green Pentagon");
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Blue Pentagon");
            ColouredShapesController.Instance.AddToColouredShapeVariationsList("Purple Pentagon");
        }
    }

    protected void AddAllVariationsToAvoidLevelShapesList() {

        ColouredShapesController.Instance.AddToAvoidLevelShapeList("White Circle");
        ColouredShapesController.Instance.AddToAvoidLevelShapeList("White Square");
        ColouredShapesController.Instance.AddToAvoidLevelShapeList("White Triangle");
    }

    // Chooses a coloured shape, this is used for selecting the main shape in the scene.
    protected string ChooseColouredShape(int numShapeTypes, int numColourTypes) {
        var shapeToUse = Random.Range(0, numShapeTypes);
        var colourToUse = Random.Range(0, numColourTypes);
        string colouredShapeToUse = "";

        switch (shapeToUse) {
            // Circle
            case 0:
                switch (colourToUse) {
                    // Red
                    case 0:
                        colouredShapeToUse = "Red Circle";
                        break;

                    // Blue
                    case 1:
                        colouredShapeToUse = "Blue Circle";
                        break;

                    // Green
                    case 2:
                        colouredShapeToUse = "Green Circle";
                        break;

                    // Pink
                    case 3:
                        colouredShapeToUse = "Pink Circle";
                        break;

                    // Purple
                    case 4:
                        colouredShapeToUse = "Purple Circle";
                        break;
                }
                break;

            // Square
            case 1:
                switch (colourToUse) {
                    // Red
                    case 0:
                        colouredShapeToUse = "Red Square";
                        break;

                    // Blue
                    case 1:
                        colouredShapeToUse = "Blue Square";
                        break;

                    // Green
                    case 2:
                        colouredShapeToUse = "Green Square";
                        break;

                    // Pink
                    case 3:
                        colouredShapeToUse = "Pink Square";
                        break;

                    // Purple
                    case 4:
                        colouredShapeToUse = "Purple Square";
                        break;
                }
                break;

            // Triangle
            case 2:
                switch (colourToUse) {
                    // Red
                    case 0:
                        colouredShapeToUse = "Red Triangle";
                        break;

                    // Blue
                    case 1:
                        colouredShapeToUse = "Blue Triangle";
                        break;

                    // Green
                    case 2:
                        colouredShapeToUse = "Green Triangle";
                        break;

                    // Pink
                    case 3:
                        colouredShapeToUse = "Pink Triangle";
                        break;

                    // Purple
                    case 4:
                        colouredShapeToUse = "Purple Triangle";
                        break;
                }
                break;

            // Pentagon
            case 3:
                switch (colourToUse)
                {
                    // Red
                    case 0:
                        colouredShapeToUse = "Red Pentagon";
                        break;

                    // Blue
                    case 1:
                        colouredShapeToUse = "Blue Pentagon";
                        break;

                    // Green
                    case 2:
                        colouredShapeToUse = "Green Pentagon";
                        break;

                    // Purple
                    case 3:
                        colouredShapeToUse = "Pink Pentagon";
                        break;

                    // Purple
                    case 4:
                        colouredShapeToUse = "Purple Pentagon";
                        break;
                }
                break;
        }
        return colouredShapeToUse;
    }

    // Initiate a random number of shapes. This generates a random number between the minimum and maximum number of shapes
    // and then instantiates random shapes totalling the random number generated.
    protected void InitiateRandomNumberOfShapes()
    {
        int numShapes = Random.Range(minNumShapesForLevel, maxNumShapesForLevel + 1);
        List<string> colouredShapesList = ColouredShapesController.Instance.GetColouredShapeVariationsList();
        int colouredShapeCount = colouredShapesList.Count;
        int numEachShape = numShapes / colouredShapeCount;
        int remainderShape = numShapes % colouredShapeCount;

        for (var i = 0; i < colouredShapeCount; i++)
        {
            for (var j = 0; j < numEachShape; j++)
            {
                string shapeToLoadName = colouredShapesList[i];
                GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
                colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
                colouredShapeScale += colouredShapeScaleDecrement;
            }
        }

        for (int k = 0; k < remainderShape; k++)
        {
            string shapeToLoadName = colouredShapesList[Random.Range(0, colouredShapeCount - 1)];
            GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
            colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
            colouredShapeScale += colouredShapeScaleDecrement;
        }
    }

    protected void InitiateShapesForAvoidLevel() {

        int numShapes = Random.Range(minNumShapesForLevel, maxNumShapesForLevel + 1);
        List<string> shapesList = ColouredShapesController.Instance.GetAvoidLevelShapeList();
        int colouredShapeCount = shapesList.Count;
        int numEachShape = numShapes / colouredShapeCount;
        int remainderShape = numShapes % colouredShapeCount;

        for (var i = 0; i < colouredShapeCount; i++) {
            for (var j = 0; j < numEachShape; j++) {
                string shapeToLoadName = shapesList[i];
                GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
                colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
                colouredShapeScale += colouredShapeScaleDecrement;
            }
        }

        for (int k = 0; k < remainderShape; k++) {
            string shapeToLoadName = shapesList[Random.Range(0, colouredShapeCount - 1)];
            GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
            colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
            colouredShapeScale += colouredShapeScaleDecrement;
        }
    }

    // When the coloured shapes list is already populated, call this method to initiate them all.
    protected void InitiateShapes(List<string> colouredShapesList)
    {
        for (int i = 0; i < colouredShapesList.Count; i++)
        {
            string shapeToLoadName = colouredShapesList[i];
            GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
            colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
            colouredShapeScale += colouredShapeScaleDecrement;
        }
    }

    // Initiate a single shape.
    protected void InitiateShape(string colouredShapeName) {
        GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + colouredShapeName));
        colouredShape.transform.localScale += new Vector3(colouredShapeScale, colouredShapeScale, colouredShapeScale);
        colouredShapeScale += colouredShapeScaleDecrement;
    }

    // When the coloured shapes list is already populated, call this method to initiate them all but with a random position.
    protected void InitiateShapesWithRandomPosition()
    {
        List<string> colouredShapesList = new List<string>(ColouredShapesController.Instance.GetColouredShapeList());
        for (int i = 0; i < colouredShapesList.Count; i++)
        {
            string shapeToLoadName = colouredShapesList[i];
            GameObject colouredShape = (GameObject)MonoBehaviour.Instantiate(Resources.Load(COLOURED_SHAPES_PREFAB_PATH + shapeToLoadName));
            colouredShape.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-3, 3), 0);
        }
    }
}
