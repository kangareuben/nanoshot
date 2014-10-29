using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<GameObject> bulletList = new List<GameObject>();

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
	private SpriteRenderer _bulletRenderer;
	private BoxCollider2D _collider;

	public int tripleShotAmmo = 0;
	public int quintShotAmmo = 0;

	private GameHandler _gameHandler;

	private GameObject soundHolder;
	private Component[] audioSources;
	private AudioSource shootSoundEffect;

	private SpriteRenderer _renderer;
	private Animator _anim;
	private string[] _animationStates = new string[3];

	private Sprite[] _bulletSprites = new Sprite[5];
	//private Controller
	// Use this for initialization
	void Start () {
		// Load assets
		_bullet = Resources.Load("Prefabs/bullet");

		_bulletSprites[0] = Resources.Load<Sprite>("Art/pillbullet");
		_bulletSprites[1] = Resources.Load<Sprite>("Art/pillbullet1");
		_bulletSprites[2] = Resources.Load<Sprite>("Art/pillbullet2");
		_bulletSprites[3] = Resources.Load<Sprite>("Art/pillbullet3");
		_bulletSprites[4] = Resources.Load<Sprite>("Art/pillbullet4");

		_animationStates[2] = "fullHealth";
		_animationStates[1] = "medHealth";
		_animationStates[0] = "lowHealth";

		_anim = this.gameObject.GetComponent<Animator>();
		_renderer = this.gameObject.GetComponent<SpriteRenderer>();

		GameObject bulletObject = (GameObject)_bullet;
		_collider = GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();
		_bulletRenderer = bulletObject.GetComponent<SpriteRenderer>();

		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();

		soundHolder = GameObject.Find("SoundHolder");
		audioSources = soundHolder.GetComponents(typeof(AudioSource));
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
		if(invulnerable > 0){
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			if(lives > 0 ){
				_anim.CrossFade (_animationStates[lives - 1], 0f);
			}
		} else {
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		}

		if (invulnerable < 0) {
			invulnerable = 0;
		}

	}


	/*
	 * Triggers for player picking up powerups and hitting enemies
	 */
	void OnTriggerEnter2D(Collider2D e){
		if(e.gameObject.tag == "Enemy"){

			if(invulnerable <= 0)
			{
				Collider.Destroy (e.gameObject);

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

	public void resetPlayer(){
		for (int i = 0; i < bulletList.Count; i++) {
			Destroy(bulletList[i]);
		}
		lives = 3;
		invulnerable = 0;
		weaponType = 0;
		tripleShotAmmo = 0;
		quintShotAmmo = 0;
		_anim.CrossFade (_animationStates[lives - 1], 0f);
	}

	/*
	 * Fires single bullet straight
	 */
	void smallGunShoot(){
		_bulletScript.speedY = 0;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.1f, 0f), Quaternion.Euler (0, 0, 0)) as GameObject;
		bulletList.Add(b);

		shootSoundEffect.volume = .5f;
		shootSoundEffect.Play();
	} 

	/*
	 * Fires three bullets in a spread
	 */
	void medGunShoot(){
		_bulletScript.speedY = 2;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b1 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.15f, 0f), Quaternion.Euler (0, 0, 16)) as GameObject;
		_bulletScript.speedY = 0;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b2 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.1f, 0f), Quaternion.Euler (0, 0, 0)) as GameObject;
		_bulletScript.speedY = -2;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b3 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y, 0.05f), Quaternion.Euler (0, 0, -16)) as GameObject;

		bulletList.Add(b1);
		bulletList.Add(b2);
		bulletList.Add(b3);

		tripleShotAmmo -= 3;

		shootSoundEffect.volume = .75f;
		shootSoundEffect.Play();
	}

	/*
	 * Fires five bullets in a spread
	 */
	void largeGunShoot(){
		_bulletScript.speedY = 2.5f;

		_bulletRenderer.sprite = randomBullet ();
		GameObject b1 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.2f, 0f), Quaternion.Euler (0, 0, 24)) as GameObject;
		_bulletScript.speedY = 1.25f;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b2 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.15f, 0f), Quaternion.Euler (0, 0, 12)) as GameObject;
		_bulletScript.speedY = 0;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b3 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.1f, 0f), Quaternion.Euler (0, 0, 0)) as GameObject;
		_bulletScript.speedY = -1.25f;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b4 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y + 0.05f, 0f), Quaternion.Euler (0, 0, -12)) as GameObject;
		_bulletScript.speedY = -2.5f;
		_bulletRenderer.sprite = randomBullet ();
		GameObject b5 = Instantiate (_bullet, new Vector3(transform.position.x + (_collider.size.x - 0.1f), transform.position.y, 0f), Quaternion.Euler (0, 0, -24)) as GameObject;

		bulletList.Add(b1);
		bulletList.Add(b2);
		bulletList.Add(b3);
		bulletList.Add(b4);
		bulletList.Add(b5);

		quintShotAmmo -= 5;

		shootSoundEffect.volume = 1;
		shootSoundEffect.Play();
	}

	Sprite randomBullet(){
		Sprite bul = null;

		int rand = Random.Range (0, 4);
		switch(rand){
			case 0: bul = _bulletSprites[0]; break;
			case 1: bul = _bulletSprites[1];break;
			case 2: bul = _bulletSprites[2];break;
			case 3: bul = _bulletSprites[3];break;
			case 4: bul = _bulletSprites[4];break;
			default: bul = _bulletSprites[0];break;
		}
		return bul;
	}
}