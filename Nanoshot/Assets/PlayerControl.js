#pragma strict

var moveUp 		: KeyCode;
var moveDown 	: KeyCode;
var moveLeft 	: KeyCode;
var moveRight	: KeyCode;

var speed : float;

function Start () {
	
}

function Update () {

	var velY : float;
	var velX : float;
	
	if(Input.GetKey(moveUp)){
		velY = speed;
	}
	else if(Input.GetKey(moveDown)){
		velY = speed * -1;
	}
	else {
		velY += Mathf.Sin(Time.time * 3);
		velX = 0;
	}
	
	if(Input.GetKey(moveRight)){
		velX = speed;
	}
	else if(Input.GetKey(moveLeft)){
		velX = speed * -1;
	}
	
	
	
	rigidbody2D.velocity.y = velY;
	rigidbody2D.velocity.x = velX;
	
}