using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

    private bool _startTimer = false;
    private float _levelElapsedTime;
    private float _gameElapsedTime;
    private float _remainingTime = 0.0f;
    private float _correctClickTimerIncrease = 0.0f;

    public static TimerController Instance { get; private set; }

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

    private void Update() {

        _gameElapsedTime += Time.deltaTime;
        _levelElapsedTime += Time.deltaTime;

        if (_remainingTime > 0.0f) {

            _remainingTime -= Time.deltaTime;
        }
        else if (
            GameController.Instance.HasGameStarted &&
            !GameController.Instance.IsLevelComplete &&
            GameController.Instance.State == GameState.LEVEL &&
            !GameController.Instance.IsAvoidLevelActive &&
            !GameController.Instance.IsProximityLevelActive)
        {
            GameController.Instance.LoseLife();
            ColouredShapesController.Instance.DestroyAllShapes();
            AudioController.Instance.PlayGameOverClip();
        }
    }

    public float RemainingTime {

        get {

            return _remainingTime;
        }
        set {
            _remainingTime = value;
        }
    }

    public float CorrectClickTimerIncrease
    {
        
        get {
            return _correctClickTimerIncrease;
        }
        set {
            _correctClickTimerIncrease = value;
        }
    }

    public void ResetLevelElapsedTimer() {

        _levelElapsedTime = 0.0f;
    }

    public float LevelElapsedTime {

        get {

            return _levelElapsedTime;
        }
        set {

            _levelElapsedTime = value;
        }
    }

    public float GameElapsedTime {

        get {

            return _gameElapsedTime;
        }
        set {

            _gameElapsedTime = value;
        }
    }
}
