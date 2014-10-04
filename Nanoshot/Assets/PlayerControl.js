#pragma strict

var moveUp : KeyCode;
var moveDown :KeyCode;

var speed : float;

function Start () {
	
}

function Update () {

	var velY : float;
	
	if(Input.GetKey(moveUp)){
		velY = speed;
	}
	else if(Input.GetKey(moveDown)){
		velY = speed * -1;
	}
	else {
		velY += Mathf.Sin(Time.time * 3);
	}
	
	
	
	rigidbody2D.velocity.y = velY;
	
}