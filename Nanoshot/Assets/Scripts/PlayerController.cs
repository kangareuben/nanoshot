using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Public variables
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;

	public KeyCode wep1;
	public KeyCode wep2;
	public KeyCode wep3;

	public KeyCode shoot;

	// Move speed
	public float speed;

	// Lives
	public int lives;

	//Weapon type
	public int weaponType;

	// Rate the player can shoot
	public float shotsPerSecond;

	//Timer for player invulnerability
	public int invulnerable;

	// Private variables
	private const float _fireRate = 1f/60f;
	private float _shotsPerSecond;
	private float _shotDelay;
	private Object _bullet;
	private BulletControl _bulletScript;
	private BoxCollider2D _collider;

	public int tripleShotAmmo = 0;
	public int quintShotAmmo = 0;

	private GameHandler _gameHandler;

	private GameObject soundHolder;
	private Component[] audioSources;
	private AudioSource backgroundMusic;
	private AudioSource shootSoundEffect;

	// Use this for initialization
	void Start () {
		// Load assets
		_bullet = Resources.Load("Prefabs/bullet");

		GameObject bulletObject = (GameObject)_bullet;
		_collider = GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();

		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();

		soundHolder = GameObject.Find("SoundHolder");
		audioSources = soundHolder.GetComponents(typeof(AudioSource));
		backgroundMusic = (AudioSource)audioSources[0];
		shootSoundEffect = (AudioSource)audioSources[1];

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

		if(Input.GetKey (wep1)){
			weaponType = 0;

		} else if(Input.GetKey (wep2)){
			weaponType = 1;

		} else if(Input.GetKey (wep3)){
			weaponType = 2;
		}

		// Check for ammo
		if(weaponType == 1 && tripleShotAmmo == 0){
			weaponType = 0;
		}
		if(weaponType == 2 && quintShotAmmo == 0){
			weaponType = 0;
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

		invulnerable--;
		if (invulnerable < 0) {
			invulnerable = 0;
		}
	}

	/*
	 * Triggers for player picking up powerups and hitting enemies
	 */
	void OnTriggerEnter2D(Collider2D e){
		if(e.gameObject.tag == "Enemy"){
			Collider.Destroy (e.gameObject);
			if(invulnerable <= 0)
			{
				invulnerable = 300;
				this.lives--;
				_gameHandler.score--;
			}
		}

		if(e.gameObject.tag == "powerup"){
			PowerupController powerupScript = e.gameObject.GetComponent<PowerupController>();
			weaponType = powerupScript.powerupType;

			switch(powerupScript.powerupType){
				case 1: tripleShotAmmo = 30; break;
				case 2: quintShotAmmo = 30; break;
			}

			Collider.Destroy(e.gameObject);
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
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.2f, 0f), Quaternion.Euler (0, 0, 0));

		shootSoundEffect.volume = .5f;
		shootSoundEffect.Play();
	}

	/*
	 * Fires three bullets in a spread
	 */
	void medGunShoot(){
		_bulletScript.speedY = 2;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.4f, 0f), Quaternion.Euler (0, 0, 16));
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.2f, 0f), Quaternion.Euler (0, 0, 0));
		_bulletScript.speedY = -2;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y, 0f), Quaternion.Euler (0, 0, -16));

		tripleShotAmmo -= 3;

		shootSoundEffect.volume = .75f;
		shootSoundEffect.Play();
	}

	/*
	 * Fires five bullets in a spread
	 */
	void largeGunShoot(){
		_bulletScript.speedY = 2.5f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.5f, 0f), Quaternion.Euler (0, 0, 24));
		_bulletScript.speedY = 1.25f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.3f, 0f), Quaternion.Euler (0, 0, 12));
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.2f, 0f), Quaternion.Euler (0, 0, 0));
		_bulletScript.speedY = -1.25f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.1f, 0f), Quaternion.Euler (0, 0, -12));
		_bulletScript.speedY = -2.5f;
		Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y - 0.1f, 0f), Quaternion.Euler (0, 0, -24));

		quintShotAmmo -= 5;

		shootSoundEffect.volume = 1;
		shootSoundEffect.Play();
	}
}