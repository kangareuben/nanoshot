using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Public variables
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode shoot;

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float velX = 0; //rigidbody2D.velocity.x;
		float velY = 0; //rigidbody2D.velocity.y;

		if(Input.GetKey(moveUp)){
			velY = speed;
		}
		else if(Input.GetKey(moveDown)){
			velY = speed * -1;
		}
		else {
			velY += Mathf.Sin(Time.time * 3);
			velX = 0;
		}
		
		if(Input.GetKey(moveRight)){
			velX = speed;
		}
		else if(Input.GetKey(moveLeft)){
			velX = speed * -1;
		}

		rigidbody2D.velocity = new Vector3(velX, velY, 0);
	}
}