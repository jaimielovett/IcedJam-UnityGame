using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredShapeScoreText : MonoBehaviour {

    [SerializeField] private TextMesh _scoreText;

    private float _textVelocity = 5.0f;
    private float _destroyDelay = 1.0f;

	void Start() {

        _scoreText.GetComponent<Renderer>().sortingLayerID = _scoreText.transform.parent.GetComponent<Renderer>().sortingLayerID;

        _scoreText.text = "$" + ScoreController.Instance.CorrectClickScore.ToString("0");
        StartCoroutine(DestroyScoreText());
	}
	
	void FixedUpdate() {

        // Hide the coloured shape text if the game is paused.
        if (GameController.Instance.State == GameState.PAUSE)
        {
            _scoreText.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            _scoreText.GetComponent<Renderer>().enabled = true;
        }

        float y = Time.deltaTime * _textVelocity;
        transform.Translate(0, y, 0);
	}

    private IEnumerator DestroyScoreText() {

        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);
    }
}
