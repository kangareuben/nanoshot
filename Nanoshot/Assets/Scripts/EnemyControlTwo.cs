using UnityEngine;
using System.Collections;

public class EnemyControlTwo : MonoBehaviour {

	public float speed;
	public int timeUntilFire;
	
	private Object _bullet;
	private float _random;
	private BulletControl _bulletScript;
	private PlayerController _playerScript;
	private BoxCollider2D _collider;

	private int _rotDirection;
	private int _rotSpeed;

	// Use this for initialization
	void Start () {
		//CHANGE THE PREFAB, 
		//ENEMY 2 PROBABLY GETS ITS OWN BULLET IMAGE
		_bullet = Resources.Load("Prefabs/BulletTwo");
		
		GameObject bulletObject = (GameObject)_bullet;
		_collider = bulletObject.GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();
		_random = Random.Range (0, 2 * Mathf.PI);
		timeUntilFire = (int)Random.Range(60,120);

		_rotSpeed = Random.Range (1, 2);

		int t = Random.Range (0, 10);

		if(t < 5){
			_rotDirection = 1;
		} else {
			_rotDirection = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float velY = 0;
		float velX = -speed;
		float zRotation =  _rotSpeed * _rotDirection;
		transform.Rotate (0, 0, zRotation);
		rigidbody2D.velocity = new Vector3(velX, velY, 0);
		timeUntilFire--;
		if (timeUntilFire == 0) {
			timeUntilFire = (int)Random.Range(60,180);
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
		_bulletScript.speedX = 8 * Mathf.Cos (0 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (0 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (0 + transform.rotation.z)));
		_bulletScript.speedX = 8 * Mathf.Cos (Mathf.PI / 3 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (Mathf.PI / 3 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (Mathf.PI / 3 + transform.rotation.z)));
		_bulletScript.speedX = 8 * Mathf.Cos (2 * Mathf.PI / 3 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (2 * Mathf.PI / 3 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (2 * Mathf.PI / 3 + transform.rotation.z)));
		_bulletScript.speedX = 8 * Mathf.Cos (3 * Mathf.PI / 3 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (3 * Mathf.PI / 3 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (3 * Mathf.PI / 3 + transform.rotation.z)));
		_bulletScript.speedX = 8 * Mathf.Cos (4 * Mathf.PI / 3 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (4 * Mathf.PI / 3 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (4 * Mathf.PI / 3 + transform.rotation.z)));
		_bulletScript.speedX = 8 * Mathf.Cos (5 * Mathf.PI / 3 + transform.rotation.z);
		_bulletScript.speedY = 8 * Mathf.Sin (5 * Mathf.PI / 3 + transform.rotation.z);
		Instantiate (_bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, (180 / Mathf.PI) * (5 * Mathf.PI / 3 + transform.rotation.z)));
	}
}
