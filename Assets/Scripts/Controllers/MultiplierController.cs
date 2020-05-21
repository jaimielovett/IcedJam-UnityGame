using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierController : MonoBehaviour {

    [SerializeField] private int _multiplier = 1;
    private int _minMultiplier = 1;
    private int _maxMultiplier = 10;

    public static MultiplierController Instance { get; private set; }

    public int Multiplier
    {
        get
        {
            return _multiplier;
        }
        set
        {
            _multiplier = value;
        }
    }

    public int MinMultiplier
    {
        get
        {
            return _minMultiplier;
        }
        set
        {
            _minMultiplier = value;
        }
    }

    public int MaxMultiplier
    {
        get
        {
            return _maxMultiplier;
        }
        set
        {
            _maxMultiplier = value;
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
        if (RewardController.Instance.X2MultiplierReward.IsUnlocked)
        {
            MinMultiplier = 2;
        }
        else
        {
            MinMultiplier = 1;
        }
    }
	
	void Update()
    {
    }

    public void MultiplierLogic(float timeRemaining)
    {
        if (GameController.Instance.CurrentLevel != LevelType.AVOID &&
            GameController.Instance.CurrentLevel != LevelType.REACTION &&
            GameController.Instance.CurrentLevel != LevelType.PROXIMITY &&
            GameController.Instance.CurrentLevel != LevelType.ODD_ONE_OUT)
        {
            if (timeRemaining >= TimerController.Instance.CorrectClickTimerIncrease * 0.5)
            {
                if (Multiplier < MaxMultiplier)
                {
                    Multiplier += 1;
                }
            }
            else
            {
                if (Multiplier > MinMultiplier)
                {
                    Multiplier -= 1;
                }
            }
        }
    }
}
