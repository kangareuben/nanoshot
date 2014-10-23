using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControlOne : MonoBehaviour {

	public float speed;
	public int timeUntilFire;
	public List<GameObject> bulletList = new List<GameObject>();

	private Object _bullet;
	private float _random;
	private BulletControl _bulletScript;
	private BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
		_bullet = Resources.Load("Prefabs/EnemyBullet");
		
		GameObject bulletObject = (GameObject)_bullet;
		_collider = bulletObject.GetComponent<BoxCollider2D>();
		_bulletScript = bulletObject.GetComponent<BulletControl>();
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
		for (int i = 0; i < bulletList.Count; i++) {
			Collider.Destroy(bulletList[i].gameObject);
		}
	}



	void Shoot()
	{
		_bulletScript.speedX = -8;
		_bulletScript.speedY = 0;
		GameObject b = Instantiate (_bullet, new Vector3(transform.position.x - .8f/*+ (_collider.transform.position.x/2 + 0.3f)*/, transform.position.y /*+ 0.2f*/, 0f), transform.rotation) as GameObject;
		bulletList.Add(b);
	}
}
