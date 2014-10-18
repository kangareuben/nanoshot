using UnityEngine;
using System.Collections;

public class EnemyControl2 : MonoBehaviour {

	public float speed;
	public int timeUntilFire;
	
	private Object _bullet;
	private float _random;
	private BulletControl _bulletScript;
	private PlayerController _playerScript;
	private BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
		//CHANGE THE PREFAB, 
		//ENEMY 2 PROBABLY GETS ITS OWN BULLET IMAGE
		_bullet = Resources.Load("Prefabs/EnemyBullet");
		
		GameObject bulletObject = (GameObject)_bullet;
		_collider = bulletObject.GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();
		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		_playerScript = playerObject.GetComponent<PlayerController>();
		_random = Random.Range (0, 2 * Mathf.PI);
		timeUntilFire = (int)Random.Range(120,240);
	}
	
	// Update is called once per frame
	void Update () {
		float velY = 0;
		velY += Mathf.Sin((Time.time + _random) * 3f) * 6;
		rigidbody2D.velocity = new Vector3(-speed, velY, 0);
		timeUntilFire--;
		if (timeUntilFire == 0) {
			timeUntilFire = (int)Random.Range(120,240);
			Shoot();
		}
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.gameObject.tag == "leftWall")
		{
			Collider.Destroy(this.gameObject);
		}
	}
	
	void OnDestroy()
	{
		
	}
	
	void Shoot()
	{
		_bulletScript.speedX = -8;
		//Use player's location to determine y velocity?
		_bulletScript.speedY = 0;
		Instantiate (_bullet, new Vector3(transform.position.x - .8f/*+ (_collider.transform.position.x/2 + 0.3f)*/, transform.position.y /*+ 0.2f*/, 0f), transform.rotation);
	}
}
