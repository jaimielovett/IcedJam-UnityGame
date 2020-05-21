using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScoreText : MonoBehaviour {

    public float textMovementSpeed = 5.0f;
    public TextMesh _levelCompleteScoreText;
    private const float _destroyDelay = 1.0f;

    private void Start() {

        _levelCompleteScoreText = gameObject.GetComponentInChildren<TextMesh>();
        _levelCompleteScoreText.GetComponent<Renderer>().sortingLayerID = _levelCompleteScoreText.transform.parent.GetComponent<Renderer>().sortingLayerID;
        _levelCompleteScoreText.text = "$" + ScoreController.Instance.LevelCompleteScore.ToString("0");
        StartCoroutine(DestroyLevelCompleteScoreText());
    }

    private void Update() {

        var y = Time.deltaTime * textMovementSpeed;
        transform.Translate(0, y, 0);
    }

    private IEnumerator DestroyLevelCompleteScoreText() {

        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);
    }
}
