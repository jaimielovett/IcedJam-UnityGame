using UnityEngine;
using UnityEngine.UI;

/*
 * The StoreController class deals with the UI for the Store.
 * The Store is very dynamic, colours and states of buttons change depending on whether or not  
 * a reward is unlocked, and whether the player can afford a reward or not.
 */

public class StoreController : MonoBehaviour
{

    [SerializeField] private Text _totalScoreTextBox;

    [SerializeField] private GameObject _X2MultiplierRewardImage;
    [SerializeField] private Sprite _X2MultiplierRewardSprite;
    [SerializeField] private Sprite _X2MultiplierRewardSpriteCantAfford;
    [SerializeField] private Sprite _X2MultiplierRewardSpriteUnlocked;
    [SerializeField] private Button _X2MultiplierRewardButton;

    [SerializeField] private GameObject _X4MultiplierRewardImage;
    [SerializeField] private Sprite _X4MultiplierRewardSprite;
    [SerializeField] private Sprite _X4MultiplierRewardSpriteCantAfford;
    [SerializeField] private Sprite _X4MultiplierRewardSpriteUnlocked;
    [SerializeField] private Button _X4MultiplierRewardButton;

    [SerializeField] private GameObject _X8MultiplierRewardImage;
    [SerializeField] private Sprite _X8MultiplierRewardSprite;
    [SerializeField] private Sprite _X8MultiplierRewardSpriteCantAfford;
    [SerializeField] private Sprite _X8MultiplierRewardSpriteUnlocked;
    [SerializeField] private Button _X8MultiplierRewardButton;

    [SerializeField] private GameObject _purpleRewardImage;
    [SerializeField] private Sprite _purpleRewardSprite;
    [SerializeField] private Sprite _purpleRewardSpriteCantAfford;
    [SerializeField] private Sprite _purpleRewardSpriteUnlocked;
    [SerializeField] private Button _purpleRewardButton;

    [SerializeField] private GameObject _pentagonRewardImage;
    [SerializeField] private Sprite _pentagonRewardSprite;
    [SerializeField] private Sprite _pentagonRewardSpriteCantAfford;
    [SerializeField] private Sprite _pentagonRewardSpriteUnlocked;
    [SerializeField] private Button _pentagonRewardButton;

    [SerializeField] private GameObject _extraLifeRewardImage;
    [SerializeField] private Sprite _extraLifeRewardSprite;
    [SerializeField] private Sprite _extraLifeRewardSpriteCantAfford;
    [SerializeField] private Sprite _extraLifeRewardSpriteUnlocked;
    [SerializeField] private Button _extraLifeRewardButton;

    [SerializeField] private GameObject _X20MaxMultiplierRewardImage;
    [SerializeField] private Sprite _X20MaxMultiplierRewardSprite;
    [SerializeField] private Sprite _X20MaxMultiplierRewardSpriteCantAfford;
    [SerializeField] private Sprite _X20MaxMultiplierRewardSpriteUnlocked;
    [SerializeField] private Button _X20MaxMultiplierRewardButton;

    [SerializeField] private GameObject _hardModeRewardImage;
    [SerializeField] private Sprite _hardModeRewardSprite;
    [SerializeField] private Sprite _hardModeRewardSpriteCantAfford;
    [SerializeField] private Sprite _hardModeRewardSpriteUnlocked;
    [SerializeField] private Button _hardModeRewardButton;

    [SerializeField] private GameObject _insaneModeRewardImage;
    [SerializeField] private Sprite _insaneModeRewardSprite;
    [SerializeField] private Sprite _insaneModeRewardSpriteCantAfford;
    [SerializeField] private Sprite _insaneModeRewardSpriteUnlocked;
    [SerializeField] private Button _insaneModeRewardButton;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _boughtRewardClip;

    public static StoreController Instance { get; private set; }

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

    public void BuyX2Multiplier()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.X2MultiplierReward.Cost)
        {
            RewardController.Instance.X2MultiplierReward.IsUnlocked = true;
            MultiplierController.Instance.MinMultiplier = 2;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
            ScoreController.Instance.TotalScore -= RewardController.Instance.X2MultiplierReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyX4Multiplier()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.X4MultiplierReward.Cost)
        {
            RewardController.Instance.X4MultiplierReward.IsUnlocked = true;
            MultiplierController.Instance.MinMultiplier = 4;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
            ScoreController.Instance.TotalScore -= RewardController.Instance.X4MultiplierReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyX8Multiplier()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.X8MultiplierReward.Cost)
        {
            RewardController.Instance.X8MultiplierReward.IsUnlocked = true;
            MultiplierController.Instance.MinMultiplier = 8;
            MultiplierController.Instance.Multiplier = MultiplierController.Instance.MinMultiplier;
            ScoreController.Instance.TotalScore -= RewardController.Instance.X8MultiplierReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyPurpleColour()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.PurpleColourReward.Cost)
        {
            RewardController.Instance.PurpleColourReward.IsUnlocked = true;
            ScoreController.Instance.TotalScore -= RewardController.Instance.PurpleColourReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyPentagonShape()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.PentagonShapeReward.Cost)
        {
            RewardController.Instance.PentagonShapeReward.IsUnlocked = true;
            ScoreController.Instance.TotalScore -= RewardController.Instance.PentagonShapeReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyExtraLife()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.ExtraLifeReward.Cost)
        {
            RewardController.Instance.ExtraLifeReward.IsUnlocked = true;
            GameController.Instance.MaxNumLives++;
            GameController.Instance.NumLives = GameController.Instance.MaxNumLives;
            ScoreController.Instance.TotalScore -= RewardController.Instance.ExtraLifeReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyX20MaxMultiplier()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.X20MaxMultiplierReward.Cost)
        {
            RewardController.Instance.X20MaxMultiplierReward.IsUnlocked = true;
            MultiplierController.Instance.MaxMultiplier = 20;
            ScoreController.Instance.TotalScore -= RewardController.Instance.X20MaxMultiplierReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyHardMode()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.HardModeReward.Cost)
        {
            RewardController.Instance.HardModeReward.IsUnlocked = true;
            ScoreController.Instance.TotalScore -= RewardController.Instance.HardModeReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    public void BuyInsaneMode()
    {
        if (ScoreController.Instance.TotalScore >= RewardController.Instance.InsaneModeReward.Cost)
        {
            RewardController.Instance.InsaneModeReward.IsUnlocked = true;
            ScoreController.Instance.TotalScore -= RewardController.Instance.InsaneModeReward.Cost;
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(_boughtRewardClip);
        }
        SetButtonStatusOnStoreLoad();
    }

    // When the store button is clicked, set the buttons visibility depending on whether or not they can afford the rewards.
    public void SetButtonStatusOnStoreLoad()
    {
        PlayerPrefs.SetInt("TotalScore", ScoreController.Instance.TotalScore);
        // Set the total score so that the player can see how much they have and what they can afford.
        _totalScoreTextBox.text = "$" + ScoreController.Instance.TotalScore.ToString("0");

        // X2 Multiplier
        if (RewardController.Instance.X2MultiplierReward.IsUnlocked)
        {
            _X2MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X2MultiplierRewardSpriteUnlocked;
            _X2MultiplierRewardButton.interactable = false;
            _X2MultiplierRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _X2MultiplierRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.X2MultiplierReward.Cost)
        {
            _X2MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X2MultiplierRewardSprite;
            // Set the button colour to its original colour (white).
            _X2MultiplierRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _X2MultiplierRewardButton.interactable = true;
        }
        else
        {
            _X2MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X2MultiplierRewardSpriteCantAfford;
            _X2MultiplierRewardButton.interactable = false;
            _X2MultiplierRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // X4 Multiplier
        if (RewardController.Instance.X4MultiplierReward.IsUnlocked)
        {
            _X4MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X4MultiplierRewardSpriteUnlocked;
            _X4MultiplierRewardButton.interactable = false;
            _X4MultiplierRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _X4MultiplierRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.X4MultiplierReward.Cost && RewardController.Instance.X2MultiplierReward.IsUnlocked)
        {
            _X4MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X4MultiplierRewardSprite;
            // Set the button colour to its original colour (white).
            _X4MultiplierRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _X4MultiplierRewardButton.interactable = true;
        }
        else
        {
            _X4MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X4MultiplierRewardSpriteCantAfford;
            _X4MultiplierRewardButton.interactable = false;
            _X4MultiplierRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // X8 Multiplier
        if (RewardController.Instance.X8MultiplierReward.IsUnlocked)
        {
            _X8MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X8MultiplierRewardSpriteUnlocked;
            _X8MultiplierRewardButton.interactable = false;
            _X8MultiplierRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _X8MultiplierRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.X8MultiplierReward.Cost && RewardController.Instance.X4MultiplierReward.IsUnlocked)
        {
            _X8MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X8MultiplierRewardSprite;
            // Set the button colour to its original colour (white).
            _X8MultiplierRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _X8MultiplierRewardButton.interactable = true;
        }
        else
        {
            _X8MultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X8MultiplierRewardSpriteCantAfford;
            _X8MultiplierRewardButton.interactable = false;
            _X8MultiplierRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // Purple Colour
        if (RewardController.Instance.PurpleColourReward.IsUnlocked)
        {
            _purpleRewardImage.GetComponent<SpriteRenderer>().sprite = _purpleRewardSpriteUnlocked;
            _purpleRewardButton.interactable = false;
            // Set the button colour to green.
            _purpleRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _purpleRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.PurpleColourReward.Cost)
        {
            _purpleRewardImage.GetComponent<SpriteRenderer>().sprite = _purpleRewardSprite;
            // Set the button colour to its original colour (white).
            _purpleRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _purpleRewardButton.interactable = true;
        }
        else
        {
            _purpleRewardImage.GetComponent<SpriteRenderer>().sprite = _purpleRewardSpriteCantAfford;
            _purpleRewardButton.interactable = false;
            // Set the button colour to red.
            _purpleRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // Pentagon Shape
        if (RewardController.Instance.PentagonShapeReward.IsUnlocked)
        {
            _pentagonRewardImage.GetComponent<SpriteRenderer>().sprite = _pentagonRewardSpriteUnlocked;
            _pentagonRewardButton.interactable = false;
            // Set the button colour to green.
            _pentagonRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _pentagonRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.PentagonShapeReward.Cost && RewardController.Instance.PurpleColourReward.IsUnlocked)
        {
            _pentagonRewardImage.GetComponent<SpriteRenderer>().sprite = _pentagonRewardSprite;
            // Set the button colour to its original colour (white).
            _pentagonRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _pentagonRewardButton.interactable = true;
        }
        else
        {
            _pentagonRewardImage.GetComponent<SpriteRenderer>().sprite = _pentagonRewardSpriteCantAfford;
            _pentagonRewardButton.interactable = false;
            // Set the button colour to red.
            _pentagonRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // Extra Life
        if (RewardController.Instance.ExtraLifeReward.IsUnlocked)
        {
            _extraLifeRewardImage.GetComponent<SpriteRenderer>().sprite = _extraLifeRewardSpriteUnlocked;
            _extraLifeRewardButton.interactable = false;
            // Set the button colour to green.
            _extraLifeRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _extraLifeRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.ExtraLifeReward.Cost && RewardController.Instance.PentagonShapeReward.IsUnlocked)
        {
            _extraLifeRewardImage.GetComponent<SpriteRenderer>().sprite = _extraLifeRewardSprite;
            // Set the button colour to its original colour (white).
            _extraLifeRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _extraLifeRewardButton.interactable = true;
        }
        else
        {
            _extraLifeRewardImage.GetComponent<SpriteRenderer>().sprite = _extraLifeRewardSpriteCantAfford;
            _extraLifeRewardButton.interactable = false;
            // Set the button colour to red.
            _extraLifeRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // X20 Max Multiplier
        if (RewardController.Instance.X20MaxMultiplierReward.IsUnlocked)
        {
            _X20MaxMultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X20MaxMultiplierRewardSpriteUnlocked;
            _X20MaxMultiplierRewardButton.interactable = false;
            _X20MaxMultiplierRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _X20MaxMultiplierRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.X20MaxMultiplierReward.Cost)
        {
            _X20MaxMultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X20MaxMultiplierRewardSprite;
            // Set the button colour to its original colour (white).
            _X20MaxMultiplierRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _X20MaxMultiplierRewardButton.interactable = true;
        }
        else
        {
            _X20MaxMultiplierRewardImage.GetComponent<SpriteRenderer>().sprite = _X20MaxMultiplierRewardSpriteCantAfford;
            _X20MaxMultiplierRewardButton.interactable = false;
            _X20MaxMultiplierRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // Hard Mode
        if (RewardController.Instance.HardModeReward.IsUnlocked)
        {
            _hardModeRewardImage.GetComponent<SpriteRenderer>().sprite = _hardModeRewardSpriteUnlocked;
            _hardModeRewardButton.interactable = false;
            _hardModeRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _hardModeRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.HardModeReward.Cost && RewardController.Instance.X20MaxMultiplierReward.IsUnlocked)
        {
            _hardModeRewardImage.GetComponent<SpriteRenderer>().sprite = _hardModeRewardSprite;
            // Set the button colour to its original colour (white).
            _hardModeRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _hardModeRewardButton.interactable = true;
        }
        else
        {
            _hardModeRewardImage.GetComponent<SpriteRenderer>().sprite = _hardModeRewardSpriteCantAfford;
            _hardModeRewardButton.interactable = false;
            _hardModeRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }

        // Insane Mode
        if (RewardController.Instance.InsaneModeReward.IsUnlocked)
        {
            _insaneModeRewardImage.GetComponent<SpriteRenderer>().sprite = _insaneModeRewardSpriteUnlocked;
            _insaneModeRewardButton.interactable = false;
            _insaneModeRewardButton.GetComponent<Image>().color = new Color(0.57f, 0.90f, 0.50f, 1f);
            _insaneModeRewardButton.GetComponentInChildren<Text>().text = "U\nn\nl\no\nc\nk\ne\nd";
        }
        else if (ScoreController.Instance.TotalScore >= RewardController.Instance.InsaneModeReward.Cost && RewardController.Instance.HardModeReward.IsUnlocked)
        {
            _insaneModeRewardImage.GetComponent<SpriteRenderer>().sprite = _insaneModeRewardSprite;
            // Set the button colour to its original colour (white).
            _insaneModeRewardButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            _insaneModeRewardButton.interactable = true;
        }
        else
        {
            _insaneModeRewardImage.GetComponent<SpriteRenderer>().sprite = _insaneModeRewardSpriteCantAfford;
            _insaneModeRewardButton.interactable = false;
            _insaneModeRewardButton.GetComponent<Image>().color = new Color(0.98f, 0.54f, 0.58f, 1f);
        }
    }

    public void ExitStoreButton()
    {
        GameController.Instance.State = GameState.MAIN_MENU;
        
        // Save the Player Preferences.
        PlayerPrefs.SetInt("X2MultiplierReward", RewardController.Instance.X2MultiplierReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("X4MultiplierReward", RewardController.Instance.X4MultiplierReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("X8MultiplierReward", RewardController.Instance.X8MultiplierReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("PurpleColourReward", RewardController.Instance.PurpleColourReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("PentagonShapeReward", RewardController.Instance.PentagonShapeReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("ExtraLifeReward", RewardController.Instance.ExtraLifeReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("X20MaxMultiplierReward", RewardController.Instance.X20MaxMultiplierReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("HardModeReward", RewardController.Instance.HardModeReward.IsUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("InsaneModeReward", RewardController.Instance.InsaneModeReward.IsUnlocked ? 1 : 0);
    }
}
