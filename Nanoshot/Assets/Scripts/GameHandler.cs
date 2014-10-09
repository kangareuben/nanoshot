using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Public variables
	public Camera mainCam;

	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;

	public Transform player;

	public int score = 0;

	// Private variables
	private float _borderWidth = 1f;
	private GUIText _scoreText;

	// Use this for initialization
	void Start () {

		topWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		topWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height, 0f)).y + (_borderWidth/2));
		
		bottomWall.size = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width * 2f, 0f, 0f)).x, _borderWidth);
		bottomWall.center = new Vector2( 0f, mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).y - (_borderWidth/2));
		
		leftWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y );
		leftWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(0f, 0f, 0f)).x - (_borderWidth*2), 0f);
		
		rightWall.size = new Vector2( _borderWidth, mainCam.ScreenToWorldPoint( new Vector3(0f, Screen.height * 2f, 0f)).y);
		rightWall.center = new Vector2( mainCam.ScreenToWorldPoint( new Vector3(Screen.width, 0f, 0f)).x + (_borderWidth*2), 0f);

		_scoreText = (GUIText)FindObjectOfType(typeof(GUIText));
	}
	
	// Update is called once per frame
	void Update () {
		// Update score text
		_scoreText.text = "Score: " + score;
	}
}
