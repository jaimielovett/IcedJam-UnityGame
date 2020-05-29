using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColouredShapesController : MonoBehaviour {

    public const int NUM_DIFF_COLOURS = 3;
    public const int NUM_DIFF_SHAPES = 3;

    private List<string> _colouredShapeVariationsList;
    private List<string> _avoidLevelShapeList;
    private List<string> _colouredShapeList;
    private List<string> _colourList;
    private List<string> _shapeList;

    private string _targetColouredShape;
    private string _targetColour;
    private string _targetShape;

    private float delayTime = 0.075f;
    private float sleepTimeScale = 0.5f;
    private bool isGameAsleep = false;

    public static ColouredShapesController Instance { get; private set; }

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

    public void InitialiseLists() {

        _colouredShapeVariationsList = new List<string>();
        _avoidLevelShapeList = new List<string>();
        _colouredShapeList = new List<string>();
        _colourList = new List<string>();
        _shapeList = new List<string>();
    }

    // Destroys every game object in the scene with the tag 'ColouredShape'.
    public void DestroyAllShapes()
    {
        isGameAsleep = true;
        Time.timeScale = sleepTimeScale;

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ColouredShape");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<ColouredShape>().DestroyShape();
        }
    }

    private void Update()
    {
        if (isGameAsleep)
        {
            StartCoroutine(DelayAfterPauseOnDestroy(delayTime));
            isGameAsleep = false;
        }
    }

    private IEnumerator DelayAfterPauseOnDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1;
    }

    // Destroys every game object in the scene with the tag 'PaintSplat'.
    public void DestroyAllPaintSplats()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("PaintSplat");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    // Sets the target coloured shape to the passed in string value.
    public void SetTargetColouredShape(string targetColouredShape)
    {
        if (_colouredShapeList.Contains(targetColouredShape))
            _targetColouredShape = targetColouredShape;
    }

    // Sets the target coloured shape randomly by picking one from the coloured shape list.
    public void SetRandomTargetColouredShape() {

        int listLength = _colouredShapeList.Count;
        if (listLength > 0) {

            _targetColouredShape = _colouredShapeList[Random.Range(0, listLength)];
        }
        else {

            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }

    // Returns the target coloured shape.
    public string GetTargetColouredShape() {

        return _targetColouredShape;
    }

    // Sets the target colour randomly by picking one from the colour list.
    public void SetRandomTargetColour() {

        int listLength = _colourList.Count;
        if (listLength > 0)
            _targetColour = _colourList[Random.Range(0, listLength)];
        else
        {
            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }

    // Returns the target colour.
    public string GetTargetColour() {

        return _targetColour;
    }

    // Sets the target shape randomly by picking one from the shape list.
    public void SetRandomTargetShape() {

        int listLength = _shapeList.Count;
        if (listLength > 0){ 

            _targetShape = _shapeList[Random.Range(0, listLength)];
        }
        else {

            GameController.Instance.IsLevelComplete = true;
            GameController.Instance.IsLevelCompletedSuccessfully = true;
        }
    }

    // Returns the target shape.
    public string GetTargetShape() {
        return _targetShape;
    }

    public void AddToAvoidLevelShapeList(string colouredShape) {

        _avoidLevelShapeList.Add(colouredShape);
    }

    public void ClearAvoidLevelShapeList() {

        if (_avoidLevelShapeList != null)
            _avoidLevelShapeList.Clear();
    }

    public void AddToColouredShapeVariationsList(string colouredShape) {

        _colouredShapeVariationsList.Add(colouredShape);
    }

    public void RemoveFromColouredShapeVariationsList(string colouredShape) {

        if (_colouredShapeVariationsList.Contains(colouredShape))
            _colouredShapeVariationsList.Remove(colouredShape);
    }

    // Shuffles the order of the coloured shape variations list.
    // This is useful for the Size Level where we shuffle the list and then remove indices until we're left
    // with the number of shapes that we want.
    public void ShuffleColouredShapeVariationsList() {

        for (var t = 0; t < _colouredShapeVariationsList.Count; t++) {

            string tmp = _colouredShapeVariationsList[t];
            var r = Random.Range(t, _colouredShapeVariationsList.Count);
            _colouredShapeVariationsList[t] = _colouredShapeVariationsList[r];
            _colouredShapeVariationsList[r] = tmp;
        }
    }

    public void ClearColouredShapeVariationsList() {

        if (_colouredShapeVariationsList != null)
            _colouredShapeVariationsList.Clear();
    }

    // Add the name of a coloured shape to the coloured shape list, this will hold all of the shapes currently in the scene.
    public void AddToColouredShapeList(string colouredShape) {

        _colouredShapeList.Add(colouredShape);
    }

    public void RemoveFromColouredShapeList(string colouredShape) {

        if (_colouredShapeList.Contains(colouredShape))
            _colouredShapeList.Remove(colouredShape);
    }

    public void SetColouredShapeList(List<string> list) {

        _colouredShapeList = list;
    }

    public void ShuffleColouredShapeList() {

        for (var t = 0; t < _colouredShapeList.Count; t++) {

            string tmp = _colouredShapeList[t];
            var r = Random.Range(t, _colouredShapeList.Count);
            _colouredShapeList[t] = _colouredShapeList[r];
            _colouredShapeList[r] = tmp;
        }
    }

    private void ClearColouredShapeList() {

        if (_colouredShapeList != null)
            _colouredShapeList.Clear();
    }

    // Add the name of the colour of a shape to the colour list.
    public void AddToColourList(string colour) {

        _colourList.Add(colour);
    }

    public void RemoveFromColourList(string colour) {

        if (_colourList.Contains(colour))
            _colourList.Remove(colour);
    }

    private void ClearColourList() {

        if (_colourList != null)
            _colourList.Clear();
    }

    // Add the name of the shape to the shape list.
    public void AddToShapeList(string shape) {

        _shapeList.Add(shape);
    }

    public void RemoveFromShapeList(string shape) {

        if (_shapeList.Contains(shape))
            _shapeList.Remove(shape);
    }

    private void ClearShapeList() {

        if (_shapeList != null)
            _shapeList.Clear();
    }

    public void ClearAllLists() {

        ClearColouredShapeVariationsList();
        ClearAvoidLevelShapeList();
        ClearColouredShapeList();
        ClearColourList();
        ClearShapeList();
    }

    public List<string> GetColouredShapeList() {

        return _colouredShapeList;
    }

    public List<string> GetColouredShapeVariationsList() {

        return _colouredShapeVariationsList;
    }

    public List<string> GetAvoidLevelShapeList() {

        return _avoidLevelShapeList;
    }
}
