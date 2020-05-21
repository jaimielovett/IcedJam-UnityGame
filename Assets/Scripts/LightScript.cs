using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour 
{
    public float sqr;

    private bool impact = false;

	void Start () 
	{
		impact = true;
		gameObject.GetComponent<Light>().intensity = 7;
		sqr = gameObject.GetComponent<Light>().intensity * gameObject.GetComponent<Light>().intensity * (( gameObject.GetComponent<Light>().intensity < 0.0f ) ? -1.0f : 1.0f);
	}
	
	void Update () 
	{
		if (impact)
		{
			gameObject.GetComponent<Light>().intensity -= (1.0f / Time.deltaTime) * sqr * .0001f;
			if (gameObject.GetComponent<Light>().intensity <= 0)
			{
				Destroy (gameObject);
			}
		}
	}
}