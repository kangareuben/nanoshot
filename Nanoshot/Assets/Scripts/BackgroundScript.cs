using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
	private float x;

	// Use this for initialization
	void Start () {
		x = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		x -= .05f;
		if (x < -24){
			x = 24;
		}
		Vector3 temp = this.transform.position;
		temp.x = x;
		this.transform.position = temp;
	}
}
