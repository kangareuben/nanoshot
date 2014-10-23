using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	public float speedX;
	public float speedY;
	public int movementType;

	private GameHandler _gameHandler;
	private PlayerController _playerController;

	// Use this for initialization
	void Start () {
		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
		_playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

		if(_gameHandler.isGame){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D e){

		if (e.gameObject.tag == "Enemy" && this.gameObject.tag == "PlayerBullet") {
			Collider.Destroy (this.gameObject);
			Collider.Destroy (e.gameObject);
			_gameHandler.score++;
		} 
		else if (e.gameObject.tag == "Player" && this.gameObject.tag == "EnemyBullet") {
			Collider.Destroy (this.gameObject);
			if(_playerController.invulnerable <= 0)
			{
				_playerController.invulnerable = 300;
				_gameHandler.score--;
				_playerController.lives--;
			}

		}
		else if(e.gameObject.tag == "Border" || e.gameObject.tag == "leftWall"){
			Collider.Destroy(this.gameObject);
		}
	}
}
