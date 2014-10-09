using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	public float speedX;
	public float speedY;
	public int movementType;

	private GameHandler _gameHandler;

	// Use this for initialization
	void Start () {
		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if(movementType == 0)
		{
			rigidbody2D.velocity = new Vector3(speedX, speedY, 0);
		}
		else if(movementType == 1)
		{
			//Other movement
		}
	}

	void OnTriggerEnter2D(Collider2D e){

		if(e.gameObject.tag == "Enemy"){
			Collider.Destroy(this.gameObject);
			Collider.Destroy(e.gameObject);
			_gameHandler.score++;
		}
		else if(e.gameObject.tag == "Border"){
			Collider.Destroy(this.gameObject);
		}
	}
}
