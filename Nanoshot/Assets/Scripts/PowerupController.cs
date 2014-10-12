using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	public int powerupType;


	private int _speed = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector3(0, -_speed, 0);
		
	}

	void OnTriggerEnter2D(Collider2D e){
		if(transform.position.y < 0 &&  e.gameObject.tag == "Border"){
			Collider.Destroy(this.gameObject);
		}
	}
}
