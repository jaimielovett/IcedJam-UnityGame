using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    private GameState _state;
    private GameState _previousState;
    private GameDifficulty _difficulty = GameDifficulty.NORMAL;
    private LevelType _currentLevel;

    // _isLevelComplete states when a level is finished.
    // _isLevelCompletedSuccessfully states whether or not the player finished the level successfully (should they get points),
    // or did they lose a life.
    private bool _isLevelComplete = true;
    private bool _isLevelCompletedSuccessfully = false;
    private bool _isAvoidLevelActive = false;
    private bool _isProximityLevelActive = false;
    private bool _isMemoryLevelActive = false;
    private bool _isBackgroundClickAllowed = false;

    private int _numLives;
    private int _maxNumLives = 2;

    // Game settings
    private bool _screenShakeOn = true;
    private bool _globalMuteOn = false;

    // The timer controller thinks the game is over when the remaining time is on 0, which it defaults to, so everytime
    // the game starts it would think it is game over. So we set this bool to false and only set it true when a level is selected,
    // and set the logic in the timer controller to only end the game if _hasGameStarted is true and the timer reaches 0.
    private bool _hasGameStarted = false;

    [SerializeField] private Transform _mainMenu;
    [SerializeField] private Transform _UI;
    [SerializeField] private Transform _gameOverUI;
    [SerializeField] private Transform _pauseUI;
    [SerializeField] private Transform _storeUI;
    [SerializeField] private Transform _background;
    [SerializeField] private Transform _timerText;
    [SerializeField] private GameObject _loseLifePrefab;

    public static GameController Instance { get; private set; }

    public int NumLives
    {
        get
        {
            return _numLives;
        }
        set
        {
            _numLives = value;
            if (_numLives <= 0)
            {
                SetGameOver();
            }
        }
    }

    public int MaxNumLives
    {
        get
        {
            return _maxNumLives;
        }
        set
        {
            _maxNumLives = value;
        }
    }

    public bool HasGameStarted
    {
        get
        {
            return _hasGameStarted;
        }
        set
        {
            _hasGameStarted = value;
        }
    }

    public GameState State
    {
        get
        {
            return _state;
        }
        set
        {
            PreviousState = State;
            _state = value;
        }

    }

    public GameState PreviousState
    {
        get
        {
            return _previousState;
        }
        private set
        {
            _previousState = value;
        }
    }

    public GameDifficulty Difficulty
    {
        get
        {
            return _difficulty;
        }
        set
        {
            _difficulty = value;
        }
    }

    public LevelType CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
        set
        {
            _currentLevel = value;
        }
    }

    public bool IsAvoidLevelActive
    {
        get
        {
            return _isAvoidLevelActive;
        }
        set
        {
            _isAvoidLevelActive = value;
        }
    }

    public bool IsProximityLevelActive
    {
        get
        {
            return _isProximityLevelActive;
        }
        set
        {
            _isProximityLevelActive = value;
        }
    }

    public bool IsMemoryLevelActive
    {
        get
        {
            return _isMemoryLevelActive;
        }
        set
        {
            _isMemoryLevelActive = value;
        }
    }

    public bool IsLevelComplete
    {
        get
        {
            return _isLevelComplete;
        }
        set
        {
            _isLevelComplete = value;
        }
    }

    public bool IsLevelCompletedSuccessfully
    {
        get
        {
            return _isLevelCompletedSuccessfully;
        }
        set
        {
            _isLevelCompletedSuccessfully = value;
        }
    }

    public bool IsBackgroundClickAllowed
    {
        get
        {
            return _isBackgroundClickAllowed;
        }
        set
        {
            _isBackgroundClickAllowed = value;
        }
    }

    public bool ScreenShakeOn
    {
        get
        {
            return _screenShakeOn;
        }
        set
        {
            _screenShakeOn = value;
        }
    }

    public bool GlobalMuteOn
    {
        get
        {
            return _globalMuteOn;
        }
        set
        {
            _globalMuteOn = value;
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
        InitialiseGame();
        SetPlayerPreferences();
    }

    void Update()
    {
        switch (State)
        {
            case GameState.LEVEL:

                _mainMenu.gameObject.SetActive(false);
                _UI.gameObject.SetActive(true);
                _pauseUI.gameObject.SetActive(false);
                if (PreviousState == GameState.PAUSE)
                    Time.timeScale = 1;
                _storeUI.gameObject.SetActive(false);
                _background.gameObject.SetActive(true);
                _timerText.gameObject.SetActive(true);
                _gameOverUI.gameObject.SetActive(false);
                break;

            case GameState.GAME_OVER:

                ColouredShapesController.Instance.DestroyAllShapes();
                ColouredShapesController.Instance.DestroyAllPaintSplats();
                _mainMenu.gameObject.SetActive(false);
                _UI.gameObject.SetActive(false);
                _pauseUI.gameObject.SetActive(false);
                _storeUI.gameObject.SetActive(false);
                _background.gameObject.SetActive(false);
                _timerText.gameObject.SetActive(false);
                _gameOverUI.gameObject.SetActive(true);
                break;

            case GameState.PAUSE:

                _mainMenu.gameObject.SetActive(false);
                _UI.gameObject.SetActive(false);
                _pauseUI.gameObject.SetActive(true);
                Time.timeScale = 0;
                _storeUI.gameObject.SetActive(false);
                _background.gameObject.SetActive(false);
                _timerText.gameObject.SetActive(false);
                _gameOverUI.gameObject.SetActive(false);
                break;

            case GameState.STORE:
                StoreController.Instance.SetButtonStatusOnStoreLoad();
                _mainMenu.gameObject.SetActive(false);
                _UI.gameObject.SetActive(false);
                _pauseUI.gameObject.SetActive(false);
                _storeUI.gameObject.SetActive(true);
                _background.gameObject.SetActive(false);
                _timerText.gameObject.SetActive(false);
                _gameOverUI.gameObject.SetActive(false);
                break;

            case GameState.MAIN_MENU:

                _mainMenu.gameObject.SetActive(true);
                _UI.gameObject.SetActive(false);
                _pauseUI.gameObject.SetActive(false);
                _storeUI.gameObject.SetActive(false);
                _background.gameObject.SetActive(false);
                _timerText.gameObject.SetActive(false);
                _gameOverUI.gameObject.SetActive(false);
                break;
        }
    }

    public void InitialiseGame()
    {
        ScoreController.Instance.CurrentScore = 0;
        LevelController.Instance.IsFirstPass = true;
        LevelController.Instance.levelsToPlayList.Clear();
        MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
        AudioController.Instance.StopMusicClip();
        AudioController.Instance.PlayMusicClip();
        IsLevelComplete = true;
        IsAvoidLevelActive = false;
        IsProximityLevelActive = false;
        IsMemoryLevelActive = false;
        IsBackgroundClickAllowed = false;
        NumLives = MaxNumLives;
    }

    private void SetPlayerPreferences()
    {
        // Set Player Preferences from stored registry file.
        ScoreController.Instance.TotalScore = PlayerPrefs.GetInt("TotalScore");
        RewardController.Instance.X2MultiplierReward.IsUnlocked = PlayerPrefs.GetInt("X2MultiplierReward") == 1;
        RewardController.Instance.X4MultiplierReward.IsUnlocked = PlayerPrefs.GetInt("X4MultiplierReward") == 1;
        RewardController.Instance.X8MultiplierReward.IsUnlocked = PlayerPrefs.GetInt("X8MultiplierReward") == 1;
        RewardController.Instance.PurpleColourReward.IsUnlocked = PlayerPrefs.GetInt("PurpleColourReward") == 1;
        RewardController.Instance.PentagonShapeReward.IsUnlocked = PlayerPrefs.GetInt("PentagonShapeReward") == 1;
        RewardController.Instance.ExtraLifeReward.IsUnlocked = PlayerPrefs.GetInt("ExtraLifeReward") == 1;
        RewardController.Instance.X20MaxMultiplierReward.IsUnlocked = PlayerPrefs.GetInt("X20MaxMultiplierReward") == 1;
        RewardController.Instance.HardModeReward.IsUnlocked = PlayerPrefs.GetInt("HardModeReward") == 1;
        RewardController.Instance.InsaneModeReward.IsUnlocked = PlayerPrefs.GetInt("InsaneModeReward") == 1;

        if (RewardController.Instance.X8MultiplierReward.IsUnlocked)
        {
            MultiplierController.Instance.MinMultiplier = 8;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
        }
        else if (RewardController.Instance.X4MultiplierReward.IsUnlocked)
        {
            MultiplierController.Instance.MinMultiplier = 4;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
        }
        else if (RewardController.Instance.X2MultiplierReward.IsUnlocked)
        {
            MultiplierController.Instance.MinMultiplier = 2;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
        }

        if (RewardController.Instance.ExtraLifeReward.IsUnlocked)
        {
            Instance.MaxNumLives++;
            Instance.NumLives = GameController.Instance.MaxNumLives;
        }

        if (RewardController.Instance.X20MaxMultiplierReward.IsUnlocked)
        {
            MultiplierController.Instance.MaxMultiplier = 20;
        }
    }

    public void SetGameOver()
    {
        ScoreController.Instance.TotalScore += ScoreController.Instance.CurrentScore;
        PlayerPrefs.SetInt("TotalScore", ScoreController.Instance.TotalScore);
        State = GameState.GAME_OVER;
    }

    public void LoseLife()
    {
        NumLives--;
        IsLevelComplete = true;
        IsLevelCompletedSuccessfully = false;
        Instantiate(_loseLifePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
