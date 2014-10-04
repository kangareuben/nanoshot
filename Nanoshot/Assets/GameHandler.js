#pragma strict

// Public variables
var mainCam : Camera;

var topWall : BoxCollider2D;
var bottomWall : BoxCollider2D;
var leftWall : BoxCollider2D;
var rightWall : BoxCollider2D;

var player : Transform;


// Private variables
var _borderWidth : float = 1f;

function Start () {

	topWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
	topWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height, 0f)).y + (_borderWidth/2));
	
	bottomWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
	bottomWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).y - (_borderWidth/2));
	
	leftWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y );
	leftWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).x - (_borderWidth/2), 0f);
	
	rightWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y);
	rightWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width, 0f, 0f)).x + (_borderWidth/2), 0f);
	
}

function Update () {

}