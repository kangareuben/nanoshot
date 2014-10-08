using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Public variables
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode shoot;

	// Move speed
	public float speed;

	// Rate the player can shoot
	public float shotsPerSecond;


	// Private variables
	private const float _fireRate = 1f/60f;
	private float _shotsPerSecond;
	private float _shotDelay;
	private Object _bullet;

	// Use this for initialization
	void Start () {
		// Load assets
		_bullet = Resources.Load("Prefabs/bullet");

		// Set variables
		_shotsPerSecond = (60.0f/shotsPerSecond) * _fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		float velX = 0.0f;
		float velY = 0.0f;

		BoxCollider2D collider = GetComponent<BoxCollider2D>();

		// Check for up and down movement, else float
		if(Input.GetKey(moveUp)){
			velY = speed;
		}
		else if(Input.GetKey(moveDown)){
			velY = speed * -1f;
		}
		else {
			velY += Mathf.Sin(Time.time * 3f);
			velX = 0f;
		}

		// Check for left and right movement
		if(Input.GetKey(moveRight)){
			velX = speed;
		}
		else if(Input.GetKey(moveLeft)){
			velX = speed * -1f;
		}

		// Check for shooting
		if(Input.GetKey(shoot)){
			if(_shotDelay >= _shotsPerSecond){
				Instantiate (_bullet, new Vector3(transform.position.x + (collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
				_shotDelay = 0.0f;
			}
		}

		// Update variables
		_shotDelay += _fireRate;
		rigidbody2D.velocity = new Vector3(velX, velY, 0);
		Debug.Log(_shotDelay);
	}
}