using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public List<int> levelsToPlayList = new List<int>();

    [SerializeField] private GameObject _levelCompleteScoreText;

    private bool _isFirstPass = true;

    private ColouredShapeLevel _colouredShapeLevel;
    private ColourLevel _colourLevel;
    private OddOneOutLevel _oddOneOutLevel;
    private ShapeLevel _shapeLevel;
    private ReactionLevel _reactionLevel;
    private AvoidLevel _avoidLevel;
    private SizeLevel _sizeLevel;
    private ProximityLevel _proximityLevel;
    private MemoryLevel _memoryLevel;

    private const int NUM_LEVEL_TYPES = (int)LevelType.NUM_LEVEL_TYPES;

    public static LevelController Instance { get; private set; }

    public bool IsFirstPass
    {
        get
        {
            return _isFirstPass;
        }
        set
        {
            _isFirstPass = value;
        }
    }

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

    void Start()
    {
        PopulateLevelsToPlayList();
        ShuffleLevelsToPlayList();
    }

    // Populate the _levelsToPlayList which makes sure the user gets to play all of the different levels
    // at least once before they play the same level again. This adds all the numbers between 0 and the number
    // of level types, then ShuffleLevelsToPlayList() is called. 
    //Each time a level is played, an index is removed, until there are none left and the cycle begins again.
    private void PopulateLevelsToPlayList() {

        for (int i = 0; i < NUM_LEVEL_TYPES; i++)
        {

            levelsToPlayList.Add(i);
        }
    }

    private void ShuffleLevelsToPlayList() {

        for (var t = 0; t < levelsToPlayList.Count; t++) {

            int tmp = levelsToPlayList[t];
            var r = Random.Range(t, levelsToPlayList.Count);
            levelsToPlayList[t] = levelsToPlayList[r];
            levelsToPlayList[r] = tmp;
        }
    }

    void Update()
    {
        // If the level is complete, select a new one.
        // Else, set the text for the current target text box.
        if (GameController.Instance.IsLevelComplete && GameController.Instance.State == GameState.LEVEL)
        {
            GameController.Instance.IsLevelComplete = false;
            ColouredShapesController.Instance.ClearAllLists();

            // Clears the previous levels shapes if the level was Avoid, Odd One Out, or Proximity.
            if (GameController.Instance.CurrentLevel == LevelType.AVOID ||
                GameController.Instance.CurrentLevel == LevelType.ODD_ONE_OUT ||
                GameController.Instance.CurrentLevel == LevelType.PROXIMITY)
            {
                ColouredShapesController.Instance.DestroyAllShapes();
            }

            GameController.Instance.HasGameStarted = true;
            SelectNewLevel();
        }
        // When Game Over, destroy all objects, clear the current level and
        // set the current target text box text blank.
        else if (GameController.Instance.State == GameState.GAME_OVER) {

            GameController.Instance.CurrentLevel = LevelType.GAME_OVER;
            GameController.Instance.HasGameStarted = false;
            //_currentTargetTextBox.text = "";
        }
        else {

            switch (GameController.Instance.CurrentLevel) {

                case LevelType.COLOURED_SHAPE:
                    UIController.Instance.SetCurrentTargetTextBox(ColouredShapesController.Instance.GetTargetColouredShape());
                    break;

                case LevelType.COLOUR:
                    UIController.Instance.SetCurrentTargetTextBox(ColouredShapesController.Instance.GetTargetColour());
                    break;

                case LevelType.ODD_ONE_OUT:
                    UIController.Instance.SetCurrentTargetTextBox("Odd One Out");
                    break;

                case LevelType.SHAPE:
                    UIController.Instance.SetCurrentTargetTextBox(ColouredShapesController.Instance.GetTargetShape());
                    break;

                case LevelType.REACTION:
                    UIController.Instance.SetCurrentTargetTextBox("Reaction");
                    break;

                case LevelType.AVOID:
                    _avoidLevel.Update();
                    UIController.Instance.SetCurrentTargetTextBox("Avoid");
                    break;

                case LevelType.LARGEST_SIZE:
                    _sizeLevel.Update();
                    UIController.Instance.SetCurrentTargetTextBox("Largest Size");
                    break;

                case LevelType.SMALLEST_SIZE:
                    _sizeLevel.Update();
                    UIController.Instance.SetCurrentTargetTextBox("Smallest Size");
                    break;

                case LevelType.PROXIMITY:
                    _proximityLevel.Update();
                    UIController.Instance.SetCurrentTargetTextBox("Proximity");
                    break;

                //case LevelType.MEMORY:
                //    _memoryLevel.Update();
                //    _currentTargetTextBox.text = "Memory";
                //    break;
            }
        }
	}

    // Select a new random level to load.
    private void SelectNewLevel() {

        if (!IsFirstPass && GameController.Instance.IsLevelCompletedSuccessfully) {

            AudioController.Instance.PlayLevelCompleteClip();
            Instantiate(_levelCompleteScoreText, new Vector3(0, 0, 1), Quaternion.identity);
            ScoreController.Instance.CurrentScore += ScoreController.Instance.LevelCompleteScore;
            GameController.Instance.IsLevelCompletedSuccessfully = false;
        }
        else {

            IsFirstPass = false;
        }

        GameController.Instance.State = GameState.LEVEL;
        GameController.Instance.IsAvoidLevelActive = false;
        GameController.Instance.IsProximityLevelActive = false;
        GameController.Instance.IsMemoryLevelActive = false;

        // If the _levelsToPlayList contains a number, pick the first one
        // then remove it from the list. When this list is empty, we populate and shuffle the list
        // and then pick the first number and remove it from the list. This logic ensures that
        // all the levels are played at least once in a loop before playing the same level again.
        int randomLevel;
        if (levelsToPlayList.Count > 0)
        {
            randomLevel = levelsToPlayList[0];
            levelsToPlayList.RemoveAt(0);
        }
        else
        {
            PopulateLevelsToPlayList();
            ShuffleLevelsToPlayList();
            randomLevel = levelsToPlayList[0];
            levelsToPlayList.RemoveAt(0);
        }

        switch (randomLevel) {

            case 0:
                GameController.Instance.CurrentLevel = LevelType.COLOURED_SHAPE;
                _colouredShapeLevel = new ColouredShapeLevel();
                break;

            case 1:
                GameController.Instance.CurrentLevel = LevelType.COLOUR;
                _colourLevel = new ColourLevel();
                break;

            case 2:
                GameController.Instance.CurrentLevel = LevelType.ODD_ONE_OUT;
                _oddOneOutLevel = new OddOneOutLevel();
                break;

            case 3:
                GameController.Instance.CurrentLevel = LevelType.SHAPE;
                _shapeLevel = new ShapeLevel();
                break;

            case 4:
                GameController.Instance.CurrentLevel = LevelType.REACTION;
                _reactionLevel = new ReactionLevel();
                float delay = UnityEngine.Random.Range(0.0f, _reactionLevel.levelTimer - 0.5f);
                StartCoroutine(Delay(delay, _reactionLevel));
                break;

            case 5:
                GameController.Instance.CurrentLevel = LevelType.AVOID;
                _avoidLevel = new AvoidLevel();
                break;

            case 6:
                GameController.Instance.CurrentLevel = LevelType.LARGEST_SIZE;
                _sizeLevel = new SizeLevel();
                break;

            case 7:
                GameController.Instance.CurrentLevel = LevelType.SMALLEST_SIZE;
                _sizeLevel = new SizeLevel();
                break;

            case 8:
                GameController.Instance.CurrentLevel = LevelType.PROXIMITY;
                _proximityLevel = new ProximityLevel();
                break;
            //case 9:
            //    _memoryLevel = new MemoryLevel();
            //    GameController.Instance.CurrentLevel = LevelType.MEMORY;
            //    break;
        }
    }

    // Add a delay for the reaction level between the level loading and showing the coloured shape to click.
    private IEnumerator Delay(float delay, ReactionLevel instance)
    {
        yield return new WaitForSeconds(delay);
        instance.ShowColouredShape();
    }
}
