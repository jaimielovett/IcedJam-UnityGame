using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour {

    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _beginnerDifficultyButton;
    [SerializeField] private Button _normalDifficultyButton;
    [SerializeField] private Button _hardDifficultyButton;
    [SerializeField] private Button _insaneDifficultyButton;
    [SerializeField] private Text _totalScoreText;

    public void Quit() {

        Application.Quit();
    }

    public void Update() {

        if (GameController.Instance.State == GameState.GAME_OVER)
            _totalScoreText.text = "Total Score: $" + ScoreController.Instance.CurrentScore;
    }

    public void PlayAgain() {

        _retryButton.gameObject.SetActive(false);
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
        GameController.Instance.InitialiseGame();
        ResetRetryButton();
    }

    public void BeginnerDifficulty() {

        GameController.Instance.Difficulty = GameDifficulty.EASY;
        GameController.Instance.InitialiseGame();
        GameController.Instance.State = GameState.LEVEL;
        ResetRetryButton();
    }

    public void NormalDifficulty() {

        GameController.Instance.Difficulty = GameDifficulty.NORMAL;
        GameController.Instance.InitialiseGame();
        GameController.Instance.State = GameState.LEVEL;
        ResetRetryButton();
    }

    public void HardDifficulty() {

        GameController.Instance.Difficulty = GameDifficulty.HARD;
        GameController.Instance.InitialiseGame();
        GameController.Instance.State = GameState.LEVEL;
        ResetRetryButton();
    }

    public void InsaneDifficulty()
    {
        GameController.Instance.Difficulty = GameDifficulty.INSANE;
        GameController.Instance.InitialiseGame();
        GameController.Instance.State = GameState.LEVEL;
        ResetRetryButton();
    }

    public void ResetRetryButton()
    {
        _retryButton.gameObject.SetActive(true);
        _beginnerDifficultyButton.gameObject.SetActive(false);
        _normalDifficultyButton.gameObject.SetActive(false);
        _hardDifficultyButton.gameObject.SetActive(false);
        _insaneDifficultyButton.gameObject.SetActive(false);
    }
}
