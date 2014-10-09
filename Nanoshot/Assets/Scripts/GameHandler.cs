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

	// Private variables
	private float _borderWidth = 1f;
	private GUIText _scoreText;

	private Object _enemyOnePrefab;

	private PlayerController _playerScript;
	private float _spawnCooldown;

	// Use this for initialization
	void Start () {

		// Load assets
		_enemyOnePrefab = Resources.Load("Prefabs/EnemyOne");

		// Initalize level
		topWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		topWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height, 0f)).y + (_borderWidth/2));
		
		bottomWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		bottomWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).y - (_borderWidth/2));
		
		leftWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y );
		leftWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).x - (_borderWidth*2), 0f);
		
		rightWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y);
		rightWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width, 0f, 0f)).x + (_borderWidth*2), 0f);

		_scoreText = (GUIText)FindObjectOfType(typeof(GUIText));

		player.position = new Vector3(-9, 0, 0);
		_playerScript = player.gameObject.GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		// Update score text

		spawnEnemies ();
		_scoreText.text = "Score: " + score;

		_spawnCooldown++;

		if(player != null){
			if(_playerScript.lives <= 0){
				Debug.Log ("You died");

				_playerScript.explode();
			}
		}
	}

	/*
	 * Spawns enemies given a chance
	 */
	void spawnEnemies(){

		if(_spawnCooldown > 30){
			float rand = Random.Range(0, 100);

			if(rand < chanceForEnemySpawn){
				Instantiate (_enemyOnePrefab, new Vector3(12f, Random.Range(-2, 1), 0), transform.rotation);

			}

			_spawnCooldown = 0;
		}
	}
}
