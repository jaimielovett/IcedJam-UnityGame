using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackgroundAnimation : Level {

	// Use this for initialization
	public MainMenuBackgroundAnimation() {

        MinNumShapesForLevel = 12;
        MaxNumShapesForLevel = 15;
        InitiateRandomNumberOfShapes();
    }
}
