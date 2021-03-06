﻿using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	public int powerupType;


	private int _speed = 3;
	private GameHandler _gameHandler;

	// Use this for initialization
	void Start () {
		_gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector3(-_speed, 0, 0);

		if(_gameHandler.isGame){
			Destroy(this.gameObject);
		}		
	}

	void OnTriggerEnter2D(Collider2D e){
		if(transform.position.y < 0 &&  e.gameObject.tag == "Border"){
			Collider.Destroy(this.gameObject);
		}
	}
}
