using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum GameState{
	MAIN_MENU_SCREEN,
	GAME_SCREEN,
	GAME_OVER_SCREEN,
	PAUSE_SCREEN
};

public class GameHandler : MonoBehaviour {

	private GameState curState = GameState.MAIN_MENU_SCREEN;

	// Public variables
	public Camera mainCam;

	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;

	public Transform player;
	public int previousLives;
	public int score = 0;
	public float chanceForEnemySpawn;

	public float chanceForTripleShotSpawn;
	public float chanceForQuintupleShotSpawn;

	// Private variables
	private float _borderWidth = 1f;
	private GUIText _scoreText;
	private GUIText _ammoText;
	private GUIText _livesText;
	private GUIText _weaponText;

	private Object _powerUp1;
	private Object _powerUp2;

	private PlayerController _playerScript;
	private float _spawnEnemyCooldown;
	private float _spawnPowerupCooldown;

	private List<GameObject> enemyList = new List<GameObject>();



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
		_ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<GUIText>();
		_livesText = GameObject.FindGameObjectWithTag("LivesText").GetComponent<GUIText>();
		_weaponText = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<GUIText>();

		player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);
		_playerScript = player.gameObject.GetComponent<PlayerController>();

		previousLives = _playerScript.lives;
	}
	
	// Update is called once per frame
	void Update () {

		switch(curState){
			case GameState.MAIN_MENU_SCREEN: 
				Vector3 temp = mainCam.transform.position;
				temp.y = 16;
				mainCam.transform.position = temp;
				break;
			case GameState.GAME_SCREEN: 
				// Update score text
				spawnEnemies ();
				spawnPowerups();
				_scoreText.text = "Score: " + score;
				
				if(_playerScript.weaponType == 0)
				{
					_weaponText.text = "Weapon Type: Single Shot";
					_ammoText.text = "Shots Remaining: Unlimited";
				}
				else if(_playerScript.weaponType == 1)
				{
					_weaponText.text = "Weapon Type: 3 Shot Spread";
					_ammoText.text = "Shots Remaining: " + _playerScript.tripleShotAmmo;
				}
				else if(_playerScript.weaponType == 2)
				{
					_weaponText.text = "Weapon Type: 5 Shot Spread";
					_ammoText.text = "Shots Remaining: " + _playerScript.quintShotAmmo;
				}
				
				_livesText.text = "Lives Remaining: " + _playerScript.lives;
				
				
				_spawnEnemyCooldown++;
				_spawnPowerupCooldown++;
				
				if(player != null){
					if(_playerScript.lives > 0 && previousLives > _playerScript.lives)
					{
						previousLives = _playerScript.lives;
						player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);
					}
					if(_playerScript.lives <= 0){
						Debug.Log ("You died");
						
						_playerScript.explode();
					}
				}
				break;
			case GameState.GAME_OVER_SCREEN: 
				break;
			case GameState.PAUSE_SCREEN: 
				break;
			default: break;
		}

	}

	/*
	 * Spawns enemies with given chance
	 */
	void spawnEnemies(){

		if(_spawnEnemyCooldown > 60){
			float rand = Random.Range(0, 100);

			if(rand < chanceForEnemySpawn){
				enemyList.Add(EnemyFactory.SpawnEnemyOrb(12f, Random.Range(-2, 1)).gameObject);
				enemyList.Add(EnemyFactory.SpawnEnemyDendrite(12f, Random.Range(-2, 1), 3).gameObject);

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

	void resetGame(){
		for(int i = 0; i < enemyList.Count; i++){

		}
	}
}
