using UnityEngine;
using System.Collections;

public class EnemyControlOne : MonoBehaviour {

	public float speed;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float velY = 0.0f;
		velY += Mathf.Sin(Time.time * 3f) * 6;
		rigidbody2D.velocity = new Vector3(-speed, velY, 0);
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
}
