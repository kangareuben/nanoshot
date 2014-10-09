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

	// Lives
	public int lives;

	//Weapon type
	public int weaponType;

	// Rate the player can shoot
	public float shotsPerSecond;


	// Private variables
	private const float _fireRate = 1f/60f;
	private float _shotsPerSecond;
	private float _shotDelay;
	private Object _bullet;
	private BulletControl _bulletScript;
	private BoxCollider2D _collider;

	private GameHandler _gameHandler;

	// Use this for initialization
	void Start () {
		// Load assets
		_bullet = Resources.Load("Prefabs/bullet");

		GameObject bulletObject = (GameObject)_bullet;
		_collider = GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();

		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();

		weaponType = 0;

		// Set variables
		_shotsPerSecond = (60.0f/shotsPerSecond) * _fireRate;
	}
	
	/*
	 * Update is called once per frame
	 */
	void Update () {
		float velX = 0.0f;
		float velY = 0.0f;

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

				switch(weaponType){
					case 0: smallGunShoot(); break;
					case 1: medGunShoot(); break;
					case 2: largeGunShoot(); break;
				}

				_shotDelay = 0.0f;
			}
		}

		// Update variables
		_shotDelay += _fireRate;
		rigidbody2D.velocity = new Vector3(velX, velY, 0);
	}

	/*
	 * Triggers for player picking up powerups and hitting enemies
	 */
	void OnTriggerEnter2D(Collider2D e){
		if(e.gameObject.tag == "Enemy"){
			Collider.Destroy (e.gameObject);
			this.lives--;
			_gameHandler.score--;
		}
	}

	/*
	 * Removes the player from the game, called on losing all lives
	 */
	public void explode(){
		Collider.Destroy (this.gameObject);
	}

	/*
	 * Fires single bullet straight
	 */
	void smallGunShoot(){
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
	}

	/*
	 * Fires three bullets in a spread
	 */
	void medGunShoot(){
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 2;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = -2;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
	}

	/*
	 * Fires five bullets in a spread
	 */
	void largeGunShoot(){
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 2.5f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 1.25f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = -1.25f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
		_bulletScript.speedX = 4;
		_bulletScript.speedY = -2.5f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x + 0.3f), transform.position.y + 0.2f, 0f), transform.rotation);
	}
}