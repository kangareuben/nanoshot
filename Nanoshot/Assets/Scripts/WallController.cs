using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
	public float speed;
	public int hp;
	private PlayerController _playerController;
	private GameHandler _gameHandler;

	// Use this for initialization
	void Start () {
		_playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
		hp = 3;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector3(-speed, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D e){
		if(e.gameObject.tag == "PlayerBullet"){
			hp--;
			if(hp <= 0)
			{
				Collider.Destroy (this.gameObject);
				_gameHandler.score++;
			}
			Collider.Destroy (e.gameObject);
		}
		if(e.gameObject.tag == "Player"){
			if(_playerController.invulnerable > 0)
			{

			}
			else
			{
				_playerController.lives--;
				_gameHandler.score--;
				Collider.Destroy (this.gameObject);
				_playerController.invulnerable = 300;
			}
		}
	}
}
