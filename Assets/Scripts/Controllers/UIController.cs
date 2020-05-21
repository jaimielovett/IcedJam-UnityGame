using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {

    [SerializeField] private Text _multiplierTextBox;
    [SerializeField] private Text _currentTargetTextBox;
    [SerializeField] private Text _totalScoreTextBox;
    [SerializeField] private TextMesh _timerTextBox;
    [SerializeField] private Image _firstLifeImage;
    [SerializeField] private Image _secondLifeImage;
    [SerializeField] private Image _thirdLifeImage;

    public static UIController Instance { get; private set; }

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
		
	}
	
	void Update()
    {
        // Set the text for the total score, remaining time and multiplier total.
        _timerTextBox.text = Math.Floor(TimerController.Instance.RemainingTime + 0.95).ToString("0");
        _totalScoreTextBox.text = "$" + ScoreController.Instance.CurrentScore.ToString("0");
        _multiplierTextBox.text = "X" + MultiplierController.Instance.Multiplier.ToString("0");

        if (GameController.Instance.NumLives == 1)
        {
            _firstLifeImage.gameObject.SetActive(true);
            _secondLifeImage.gameObject.SetActive(false);
            _thirdLifeImage.gameObject.SetActive(false);
        }
        else if (GameController.Instance.NumLives == 2)
        {
            _firstLifeImage.gameObject.SetActive(true);
            _secondLifeImage.gameObject.SetActive(true);
            _thirdLifeImage.gameObject.SetActive(false);
        }
        else if (GameController.Instance.NumLives == 3)
        {
            _firstLifeImage.gameObject.SetActive(true);
            _secondLifeImage.gameObject.SetActive(true);
            _thirdLifeImage.gameObject.SetActive(true);
        }
    }

    public void SetCurrentTargetTextBox(string text)
    {
        Instance._currentTargetTextBox.text = text;
    }
}
