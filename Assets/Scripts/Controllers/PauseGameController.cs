using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour {

    [SerializeField] private Transform _pauseMenuCanvas;
    [SerializeField] private Toggle _screenShakeToggle;
    [SerializeField] private Toggle _globalMuteToggle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        GameController.Instance.ScreenShakeOn = _screenShakeToggle.isOn;
        GameController.Instance.GlobalMuteOn = _globalMuteToggle.isOn;
    }

    public void Pause()
    {
        if (GameController.Instance.State == GameState.LEVEL            &&
            GameController.Instance.CurrentLevel != LevelType.PROXIMITY &&
            GameController.Instance.CurrentLevel != LevelType.AVOID)
        {
            GameController.Instance.State = GameState.PAUSE;
        }
        else if (GameController.Instance.State != GameState.GAME_OVER && GameController.Instance.State != GameState.STORE)
        {
            GameController.Instance.State = GameState.LEVEL;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
