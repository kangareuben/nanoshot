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

	// Keycode for starting and restarting the game - "enter"
	public KeyCode restartGame;
	public KeyCode pauseGame;

	public int pauseCooldown;

	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;

	public Transform player;
	public int previousLives;
	public int score = 0;
	public float chanceForEnemySpawn;
	public bool isGame = false;

	public float chanceForTripleShotSpawn;
	public float chanceForQuintupleShotSpawn;

	// Private variables
	private float _borderWidth = 1f;
	private GUIText _scoreText;
	private GUIText _ammoText;
	private GUIText _livesText;
	private GUIText _weaponText;
	private GUIText _pauseText;


	private Object _powerUp1;
	private Object _powerUp2;
	private object _playerPrefab;

	private PlayerController _playerScript;
	private float _spawnEnemyCooldown;
	private float _spawnPowerupCooldown;

	private List<GameObject> enemyList = new List<GameObject>();
	private List<GameObject> enemyBulletList = new List<GameObject>();
	private List<GameObject> playerBulletList = new List<GameObject>();

	private GameObject soundHolder;
	private Component[] audioSources;
	private AudioSource backgroundMusic;

	// Use this for initialization
	void Start () {

		// Load assets
		_powerUp1 = Resources.Load ("Prefabs/powerup1");
		_powerUp2 = Resources.Load ("Prefabs/powerup2");
		_playerPrefab = Resources.Load("Prefabs/Player");

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
		_pauseText = GameObject.FindGameObjectWithTag("PauseText").GetComponent<GUIText>();

		player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);
		_playerScript = player.gameObject.GetComponent<PlayerController>();

		previousLives = _playerScript.lives;

		soundHolder = GameObject.Find("SoundHolder");
		audioSources = soundHolder.GetComponents(typeof(AudioSource));
		backgroundMusic = (AudioSource)audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {

		switch(curState){
			case GameState.MAIN_MENU_SCREEN: 
				
				if(Input.GetKey(restartGame))
				{
					curState = GameState.GAME_SCREEN;
				}

				Vector3 tempStart = mainCam.transform.position;
				tempStart.y = 16;
				mainCam.transform.position = tempStart;
				soundHolder.transform.position = tempStart;
				break;
			case GameState.GAME_SCREEN: 
				isGame = false;
				_pauseText.text = "";
				pauseCooldown++;
				// Update score text
				spawnEnemies ();
				spawnPowerups();
				_scoreText.text = "Score: " + score;
				
			if(Input.GetKey(pauseGame) && pauseCooldown > 50)
				{
					curState = GameState.PAUSE_SCREEN;
					pauseCooldown = 0;
				}

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
						
						//_playerScript.explode();
						player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);


						curState = GameState.GAME_OVER_SCREEN;
					}
				}				
				// set camera to game screen
				Vector3 tempMain = mainCam.transform.position;
				tempMain.y = 0;
				mainCam.transform.position = tempMain;
				soundHolder.transform.position = tempMain;
				break;
			case GameState.GAME_OVER_SCREEN:
				isGame = true;

				if(Input.GetKey(restartGame))
				{
					curState = GameState.GAME_SCREEN;
					// reset game
					resetGame();
				}

				Vector3 tempGameOver = mainCam.transform.position;
				tempGameOver.y = -16;
				mainCam.transform.position = tempGameOver;
				soundHolder.transform.position = tempGameOver;


				break;
			case GameState.PAUSE_SCREEN:
			pauseCooldown++;
			_pauseText.text = "Pause";
			Time.timeScale = 0;
			if(Input.GetKey(pauseGame) && pauseCooldown > 50)
			{
				Time.timeScale = 1;
				curState = GameState.GAME_SCREEN;
				pauseCooldown = 0;

									
			}
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
			Destroy(enemyList[i]);
		}

		resetPlayer();

	
}

	void resetPlayer(){
		for (int i = 0; i < _playerScript.bulletList.Count; i++) {
			Destroy(_playerScript.bulletList[i]);
		}
		_playerScript.lives = 3;
		_playerScript.invulnerable = 0;
		score = 0;
		//player.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f) ).x + (_borderWidth*2), 0f, 0f);

		//_playerScript.position
	}
}
