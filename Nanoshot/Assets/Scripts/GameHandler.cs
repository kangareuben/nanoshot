using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Public variables
	public Camera mainCam;

	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;

	public Transform player;

	public int score = 0;
	public float chanceForEnemySpawn;

	public float chanceForTripleShotSpawn;
	public float chanceForQuintupleShotSpawn;

	// Private variables
	private float _borderWidth = 1f;
	private GUIText _scoreText;

	private Object _powerUp1;
	private Object _powerUp2;

	private PlayerController _playerScript;
	private float _spawnEnemyCooldown;
	private float _spawnPowerupCooldown;

	// Use this for initialization
	void Start () {

		// Load assets
		_powerUp1 = Resources.Load ("Prefabs/powerup1");
		_powerUp2 = Resources.Load ("Prefabs/powerup2");

		// Initalize level
		topWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		topWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height, 0f)).y + (_borderWidth/2));
		
		bottomWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		bottomWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).y - (_borderWidth/2));
		
		leftWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y );
		leftWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).x - (_borderWidth*2), 0f);
		
		rightWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y);
		rightWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width, 0f, 0f)).x + (_borderWidth*2), 0f);

		_scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<GUIText>();

		player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);
		_playerScript = player.gameObject.GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		// Update score text

		spawnEnemies ();
		spawnPowerups();
		_scoreText.text = "Score: " + score;

		_spawnEnemyCooldown++;
		_spawnPowerupCooldown++;

		if(player != null){
			if(_playerScript.lives <= 0){
				Debug.Log ("You died");

				_playerScript.explode();
			}
		}
	}

	/*
	 * Spawns enemies with given chance
	 */
	void spawnEnemies(){

		if(_spawnEnemyCooldown > 60){
			float rand = Random.Range(0, 100);

			if(rand < chanceForEnemySpawn){
				EnemyFactory.SpawnEnemyOrb(12f, Random.Range(-2, 1));

			}

			_spawnEnemyCooldown = 0;
		}
	}

	/*
	 * Spawns powerups with specific chances
	 */
	void spawnPowerups(){
		if(_spawnPowerupCooldown > 60){
			float rand1 = Random.Range(0, 100);
			float rand2 = Random.Range(0, 100);
			
			if(rand1 < chanceForTripleShotSpawn){
				Instantiate (_powerUp1, new Vector3(Random.Range(-2, 7), 4.5f, 0), transform.rotation);
			} else if(rand2 < chanceForQuintupleShotSpawn){
				Instantiate (_powerUp2, new Vector3(Random.Range(-2, 7), 4.5f, 0), transform.rotation);
			}
			
			_spawnPowerupCooldown = 0;
		}
	}
}
