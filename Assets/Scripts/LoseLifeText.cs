using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLifeText : MonoBehaviour {

    [SerializeField] private GameObject _loseLifePrefab;

    private float _textVelocity = 0.0f;
    private float _destroyDelay = 1.0f;

    void Start()
    {
        _loseLifePrefab.GetComponent<Renderer>().sortingLayerID = _loseLifePrefab.transform.GetComponent<Renderer>().sortingLayerID;
        StartCoroutine(DestroyLoseLifeText());
    }

    void FixedUpdate()
    {
        // Hide the lose life text if the game is paused.
        if (GameController.Instance.State == GameState.PAUSE)
        {
            _loseLifePrefab.GetComponent<Renderer>().enabled = false;
            _loseLifePrefab.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
            _loseLifePrefab.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        else
        {
            _loseLifePrefab.GetComponent<Renderer>().enabled = true;
            _loseLifePrefab.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
            _loseLifePrefab.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().enabled = true;
        }

        float y = Time.deltaTime * _textVelocity;
        transform.Translate(0, y, 0);
    }

    private IEnumerator DestroyLoseLifeText()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);
    }
}
