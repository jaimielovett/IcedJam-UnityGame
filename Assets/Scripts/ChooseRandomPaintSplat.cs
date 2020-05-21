using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChooseRandomPaintSplat : MonoBehaviour {

    [SerializeField] private Sprite[] _paintSplatSprites;
    private SpriteRenderer _sprite;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Choose a random sprite for the paint splat.
        int shapeSpriteIndex = Random.Range(0, _paintSplatSprites.Length);
        _sprite.sprite = _paintSplatSprites[shapeSpriteIndex];

        // Create a random rotation for the paint splat.
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0, 360f);
        _sprite.transform.eulerAngles = euler;
    }
}
