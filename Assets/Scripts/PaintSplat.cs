using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplat : MonoBehaviour { 
	
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
    }
}
