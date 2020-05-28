using UnityEngine;
using System.Collections;
using Thinksquirrel.CShake;

public class ColouredShape : MonoBehaviour {

    [SerializeField] private string _objName;
    [SerializeField] private string _colour;
    [SerializeField] private string _shape;
    [SerializeField] private GameObject _scoreTextPrefab;
    [SerializeField] private GameObject _destroyAnimation;
    [SerializeField] private GameObject _paintSplat;
    private Transform _shapeOutline;

    private Color _originalColour;
    private Vector3 _originalScale;
    private int _shrinkCount = 0;
    private int _stretchCount = 0;

    private Vector3 _velocity;
    private float _speed = 5.0f;
    private float _rotationSpeed;
    private Vector3 _rotationDirection;
    private bool _mouseEnteredShape = false;
    private bool _isClickable = false;
    private SpriteRenderer _sprite;

    void Awake() {
        _sprite = GetComponent<SpriteRenderer>();
        _shapeOutline = transform.GetChild(0);

        ColouredShapesController.Instance.AddToColouredShapeList(_objName);
        ColouredShapesController.Instance.AddToColourList(_colour);
        ColouredShapesController.Instance.AddToShapeList(_shape);
    }

    void Start()
    {
        _originalColour = _sprite.color;
        _originalScale = transform.localScale;

        // Set the speed of the shapes based on the difficulty.
        switch (GameController.Instance.Difficulty) {

            case GameDifficulty.EASY:
                _speed = ConfigConstants.k_EasyDifficultyShapeSpeed;
                break;

            case GameDifficulty.NORMAL:
                _speed = ConfigConstants.k_NormalDifficultyShapeSpeed;
                break;

            case GameDifficulty.HARD:
                _speed = ConfigConstants.k_HardDifficultyShapeSpeed;
                break;

            case GameDifficulty.INSANE:
                _speed = ConfigConstants.k_InsaneDifficultyShapeSpeed;
                break;
        }

        // Random rotation on creation.
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0, 360f);
        transform.eulerAngles = euler;
        _rotationSpeed = Random.Range(0f, 1f);
        _rotationDirection = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.back;

        // The velocity of the shape.
        _velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
    }

    void FixedUpdate() {

        if (GameController.Instance.State == GameState.LEVEL    &&
            !GameController.Instance.IsAvoidLevelActive         &&
            !GameController.Instance.IsProximityLevelActive)
        {
            _isClickable = true;
            _sprite.sortingLayerName = "FrontShape";
        }
        else
        {
            _isClickable = false;
            _sprite.sortingLayerName = "BackShape";
            _sprite.sortingOrder = 1;
        }

        // Move the shape if the level type is not Reaction.
        if (GameController.Instance.CurrentLevel != LevelType.REACTION)
            transform.position += _velocity * _speed * Time.deltaTime;

        // If we're playing the proximity level and it's active, if the mouse enters the shape, we set the colour of the shape to green.
        // If the mouse has not entered the shape, or leaves the shape while proximity level is active, set the colour of the shape to red
        // and set the game state to GAME_OVER.
        if (GameController.Instance.IsProximityLevelActive && GameController.Instance.State == GameState.LEVEL)
        {
            if (_mouseEnteredShape)
            {
                _sprite.color = new Color(0.57f, 0.90f, 0.50f, 1f);
            }
            else
            {
                _sprite.color = new Color(0.98f, 0.54f, 0.58f, 1f);
                GameController.Instance.LoseLife();
                ColouredShapesController.Instance.DestroyAllShapes();
                AudioController.Instance.PlayGameOverClip();
            }
        }
        // If the current level is not 'Avoid', switch the shapes z position to bring the current objects to the front of the screen.
        // This means that the current object will always appear infront of the other objects.
        else if (GameController.Instance.CurrentLevel == LevelType.COLOUR)
        {
            if (_colour == ColouredShapesController.Instance.GetTargetColour())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        else if (GameController.Instance.CurrentLevel == LevelType.SHAPE)
        {
            if (_shape == ColouredShapesController.Instance.GetTargetShape())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        else
        {
            if (_objName == ColouredShapesController.Instance.GetTargetColouredShape())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
    }

    void Update()
    {
        // If the game is paused, disable the renderer and vice versa.
        if (GameController.Instance.State == GameState.PAUSE)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
        }

        transform.Rotate(_rotationDirection, _rotationSpeed);
    }

    void OnCollisionEnter2D(Collision2D other) {

        // Reflect off the wall on collision with it.
        var hit = other.contacts[0];
        _velocity = Vector3.Reflect(_velocity, hit.normal);

        if (GameController.Instance.CurrentLevel != LevelType.PROXIMITY)
        {
            // Briefly turn the shape a white colour when it collides with the wall.
            _sprite.color = new Color(1, 1, 1, 1);
            StartCoroutine(DelayBeforeReturningToOriginalColour(0.05f));

            InvokeRepeating("Shrink", 0f, 0.02f);
        }
        else if (GameController.Instance.ScreenShakeOn) // Screen shake for the Proximity Level.
        {
            CameraShake.ShakeAll(CameraShake.ShakeType.CameraMatrix,                // Shake Type
                     2,                                                             // Number of Shakes
                     new Vector3(_originalScale.x, _originalScale.y, 0),            // Shake Amount
                     new Vector3(0, 0, 0),                                          // Rotation Amount
                     0.1f,                                                          // Distance
                     50,                                                            // Speed
                     0.2f,                                                          // Decay
                     1,                                                             // UI Shake Modifier
                     true);                                                         // Multiply by Time Scale?
        }
    }

    private void Shrink()
    {
        if (_shrinkCount >= 10)
        {
            CancelInvoke("Shrink");
            _shrinkCount = 0;
            InvokeRepeating("Stretch", 0f, 0.02f);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.0085f, transform.localScale.y - 0.0085f, transform.localScale.z - 0.0085f);
        }
        _shrinkCount += 1;
    }

    private void Stretch()
    {
        _stretchCount += 1;
        if (_stretchCount >= 10)
        {
            CancelInvoke("Stretch");
            _stretchCount = 0;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.0085f, transform.localScale.y + 0.0085f, transform.localScale.z + 0.0085f);
        }
    }

    private IEnumerator DelayBeforeReturningToOriginalColour(float delay)
    {
        yield return new WaitForSeconds(delay);
        _sprite.color = _originalColour;
    }

    private IEnumerator DelayBeforeReturningToOriginalScale(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = _originalScale;
    }

    // On the mouse click we switch depending on the current level so we know exactly what shape was pressed
    // and if it's the target objective of the game. If it is, destroy the shape, if not the game is over.
    void OnMouseDown() {

        bool destroyShape = false;

        if (_isClickable) {

            switch (GameController.Instance.CurrentLevel) {

                case LevelType.COLOURED_SHAPE:
                    destroyShape = EventController.Instance.OnMouseClickColouredShape(_objName);
                    break;

                case LevelType.COLOUR:
                    destroyShape = EventController.Instance.OnMouseClickColour(_colour);
                    break;

                case LevelType.ODD_ONE_OUT:
                    destroyShape = EventController.Instance.OnMouseClickColouredShape(_objName);
                    if (destroyShape)
                    {
                        GameController.Instance.IsLevelComplete = true;
                        GameController.Instance.IsLevelCompletedSuccessfully = true;
                    }

                    break;

                case LevelType.SHAPE:
                    destroyShape = EventController.Instance.OnMouseClickShape(_shape);
                    break;

                case LevelType.REACTION:
                    destroyShape = EventController.Instance.OnMouseClickColouredShape(_objName);
                    if (destroyShape)
                    {
                        GameController.Instance.IsLevelComplete = true;
                        GameController.Instance.IsLevelCompletedSuccessfully = true;
                    }
                    break;

                case LevelType.LARGEST_SIZE:
                    destroyShape = EventController.Instance.OnMouseClickSizeLevel(_objName);
                    break;

                case LevelType.SMALLEST_SIZE:
                    destroyShape = EventController.Instance.OnMouseClickSizeLevel(_objName);
                    break;

                //case LevelType.MEMORY:
                //    destroyShape = EventController.Instance.OnMouseClickSizeLevel(_objName);
                //    break;
            }

            // Odd One Out takes care of destroying the shapes itself because when GameController.IsLevelComplete = true.
            // All of the gameObjects in the scene are destroyed.
            if (destroyShape && (GameController.Instance.CurrentLevel != LevelType.ODD_ONE_OUT || GameController.Instance.CurrentLevel != LevelType.REACTION)) {
                GameController.Instance.IsLevelComplete = true;
                GameController.Instance.IsLevelCompletedSuccessfully = true;
                ColouredShapesController.Instance.DestroyAllShapes();
                DestroyShape();
                ScoreController.Instance.CurrentScore += ScoreController.Instance.CorrectClickScore;
                Instantiate(_scoreTextPrefab, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                TimerController.Instance.RemainingTime = TimerController.Instance.CorrectClickTimerIncrease;
            }
            else if (!destroyShape) {

                GameController.Instance.LoseLife();
                ColouredShapesController.Instance.DestroyAllShapes();
                AudioController.Instance.PlayGameOverClip();
            }
        }
    }

    void OnMouseOver() 
    {
        if (GameController.Instance.CurrentLevel == LevelType.AVOID &&
            GameController.Instance.IsAvoidLevelActive              &&
            GameController.Instance.State != GameState.PAUSE) {

            _sprite.color = new Color(1f, 0f, 0f, 1f);
            GameController.Instance.LoseLife();
            ColouredShapesController.Instance.DestroyAllShapes();
            AudioController.Instance.PlayGameOverClip();
        }
    }

    void OnMouseEnter() {

        _mouseEnteredShape = true;

        if (GameController.Instance.CurrentLevel != LevelType.AVOID ||
            GameController.Instance.CurrentLevel != LevelType.LARGEST_SIZE ||
            GameController.Instance.CurrentLevel != LevelType.SMALLEST_SIZE)
            _shapeOutline.gameObject.SetActive(true);
    }

    void OnMouseExit() {

        _mouseEnteredShape = false;
        if (GameController.Instance.CurrentLevel != LevelType.AVOID ||
            GameController.Instance.CurrentLevel != LevelType.LARGEST_SIZE ||
            GameController.Instance.CurrentLevel != LevelType.SMALLEST_SIZE)
            _shapeOutline.gameObject.SetActive(false);
    }

    public void DestroyShape() {

        // Screen Shake code: If the current level is 'Size' then we want to shake the screen dependant on the scale of the shape.
        // This gives a nice effect for the bigger shapes, making the screen shake more. But the default scale adds a bit too much screen
        // shake to use this for every level, hence having the if/else statement.
        if (GameController.Instance.ScreenShakeOn)
        {
            if (GameController.Instance.CurrentLevel == LevelType.LARGEST_SIZE || GameController.Instance.CurrentLevel == LevelType.SMALLEST_SIZE)
            {
                CameraShake.ShakeAll(CameraShake.ShakeType.CameraMatrix,                        // Shake Type
                                 2,                                                             // Number of Shakes
                                 new Vector3(_originalScale.x, _originalScale.y, 0),            // Shake Amount
                                 new Vector3(0, 0, 0),                                          // Rotation Amount
                                 0.1f,                                                          // Distance
                                 50,                                                            // Speed
                                 0.2f,                                                          // Decay
                                 1,                                                             // UI Shake Modifier
                                 true);                                                         // Multiply by Time Scale?
            }
            else
            {
                CameraShake.ShakeAll(CameraShake.ShakeType.CameraMatrix,                        // Shake Type
                                 2,                                                             // Number of Shakes
                                 new Vector3(0.3f, 0.3f, 0),                                    // Shake Amount
                                 new Vector3(0, 0, 0),                                          // Rotation Amount
                                 0.1f,                                                          // Distance
                                 50,                                                            // Speed
                                 0.2f,                                                          // Decay
                                 1,                                                             // UI Shake Modifier
                                 true);                                                         // Multiply by Time Scale?
            }
        }

        GameObject destroyAnimation = Instantiate(_destroyAnimation, transform.position, Quaternion.identity) as GameObject;
        Destroy(destroyAnimation, 1);
        GameObject paintSplat = Instantiate(_paintSplat, transform.position, Quaternion.identity) as GameObject;
        // Set the scale of the paint splat. We do this by multiplying 1.67 * the shapes scale.
        // This is because paint splats were made to 0.5 scale and most shapes are 0.3 (a difference of 1.67).
        // This scaling of 1.67 means that for any levels where bigger shapes are used, bigger splats will occur.
        // However it's nice to have slightly random splats, so instead of 1.67, Random.Range(1.37, 1.97).
        float scaleAmount = Random.Range(1.37f, 1.97f);
        paintSplat.transform.localScale = new Vector3(scaleAmount * transform.localScale.x, scaleAmount * transform.localScale.y, 0);
        AudioController.Instance.PlayImpactClip();
        int paintSplatDisappearTime = Random.Range(10, 20);
        Destroy(paintSplat, paintSplatDisappearTime);
        Destroy(gameObject);

        ScoreController.Instance.CalculateCorrectClickScore();
        if (GameController.Instance.State == GameState.LEVEL)
        {
            ScoreController.Instance.CorrectClickScore = ScoreController.Instance.CorrectClickScore * MultiplierController.Instance.Multiplier;
            MultiplierController.Instance.MultiplierLogic(TimerController.Instance.RemainingTime);
        }
    }
}
