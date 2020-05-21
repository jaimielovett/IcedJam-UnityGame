using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _beginnerDifficultyButton;
    [SerializeField] private Button _normalDifficultyButton;
    [SerializeField] private Button _hardDifficultyButton;
    [SerializeField] private Button _insaneDifficultyButton;

    public void Play()
    {
        _playButton.gameObject.SetActive(false);
        _beginnerDifficultyButton.gameObject.SetActive(true);
        _normalDifficultyButton.gameObject.SetActive(true);
        _hardDifficultyButton.gameObject.SetActive(true);
        _insaneDifficultyButton.gameObject.SetActive(true);

        if (!RewardController.Instance.HardModeReward.IsUnlocked)
        {
            _hardDifficultyButton.interactable = false;
        }
        else
        {
            _hardDifficultyButton.interactable = true;
        }

        if (!RewardController.Instance.InsaneModeReward.IsUnlocked)
        {
            _insaneDifficultyButton.interactable = false;
        }
        else
        {
            _insaneDifficultyButton.interactable = true;
        }
    }

    public void Store()
    {
        GameController.Instance.State = GameState.STORE;
        ResetPlayButton();
    }

    public void BeginnerDifficulty()
    {
        GameController.Instance.Difficulty = GameDifficulty.EASY;
        GameController.Instance.State = GameState.LEVEL;
        ResetPlayButton();
    }

    public void NormalDifficulty()
    {
        GameController.Instance.Difficulty = GameDifficulty.NORMAL;
        GameController.Instance.State = GameState.LEVEL;
        ResetPlayButton();
    }

    public void HardDifficulty()
    {
        GameController.Instance.Difficulty = GameDifficulty.HARD;
        GameController.Instance.State = GameState.LEVEL;
        ResetPlayButton();
    }

    public void InsaneDifficulty()
    {
        GameController.Instance.Difficulty = GameDifficulty.INSANE;
        GameController.Instance.State = GameState.LEVEL;
        ResetPlayButton();
    }

    public void ResetPlayButton()
    {
        _playButton.gameObject.SetActive(true);
        _beginnerDifficultyButton.gameObject.SetActive(false);
        _normalDifficultyButton.gameObject.SetActive(false);
        _hardDifficultyButton.gameObject.SetActive(false);
        _insaneDifficultyButton.gameObject.SetActive(false);
    }

    public void Quit() {

        Application.Quit();
    }
}
