using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private bool _colourChanged = false;
    private float _timeColourChanged = 0.0f;
    private float RESET_BACKGROUND_COLOUR_DELAY = 0.15f;

    public static BackgroundController Instance { get; private set; }

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

    void Start() {

        _spriteRenderer = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / _spriteRenderer.sprite.bounds.size.x,
            worldScreenHeight / _spriteRenderer.sprite.bounds.size.y, 1);

        ChangeBackgroundColour("orange");
    }

    void OnMouseDown() {

        if (!GameController.Instance.IsBackgroundClickAllowed)
        {
            GameController.Instance.LoseLife();
            ColouredShapesController.Instance.DestroyAllShapes();
            AudioController.Instance.PlayGameOverClip();
        }
    }

    void Update() {

        if (_colourChanged && TimerController.Instance.GameElapsedTime > _timeColourChanged + RESET_BACKGROUND_COLOUR_DELAY) {

            ChangeBackgroundColour("orange");
            _colourChanged = false;
        }
        else if ((GameController.Instance.CurrentLevel == LevelType.AVOID && GameController.Instance.IsAvoidLevelActive) ||
                    (GameController.Instance.CurrentLevel == LevelType.PROXIMITY && GameController.Instance.IsProximityLevelActive)) {

            ChangeBackgroundColour("green");
        }
    }

    public void ChangeBackgroundColour(string colour) {

        _colourChanged = true;
        _timeColourChanged = TimerController.Instance.GameElapsedTime;
        colour = colour.ToLower();
        switch (colour) {

            case "red":
                _spriteRenderer.color = new Color(0.98f, 0.54f, 0.58f, 1f);
                break;

            case "green":
                _spriteRenderer.color = new Color(0.57f, 0.90f, 0.50f, 1f);
                break;

            case "blue":
                _spriteRenderer.color = new Color(0.46f, 0.62f, 0.80f, 1f);
                break;

            case "purple":
                _spriteRenderer.color = new Color(0.68f, 0.46f, 0.80f, 1f);
                break;

            case "orange":
                _spriteRenderer.color = new Color(1.0f, 0.82f, 0.55f, 1f);
                break;

            default:
                _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }
}
