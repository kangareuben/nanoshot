using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector3(speed, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D e){

		if(e.gameObject.tag == "Enemy"){
			Collider.Destroy(this.gameObject);
			Collider.Destroy(e.gameObject);
		}
		else if(e.gameObject.tag == "Border"){
			Collider.Destroy(this.gameObject);
		}
	}
}
