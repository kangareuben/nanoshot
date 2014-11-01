using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	protected static EnemyFactory instance;

	public GameObject EnemyOrbPrefab;
	public GameObject EnemyDendritePrefab;
	public GameObject WallPrefab;

	private const int maxSpawns = 20;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Spawn individual enemies
	public static EnemyControlOne SpawnEnemyOrb(float x, float y){
		GameObject b = Object.Instantiate(instance.EnemyOrbPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
		EnemyControlOne e = b.GetComponent<EnemyControlOne>();
		return e;
	}

	public static EnemyControlTwo SpawnEnemyDendrite(float x, float y, float s){
		GameObject b = Object.Instantiate (instance.EnemyDendritePrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
		EnemyControlTwo e = b.GetComponent<EnemyControlTwo>();
		e.speed = s;
		return e;
	}

	public static WallController SpawnWall(float x, float y, float s){
		GameObject b = Object.Instantiate (instance.WallPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
		WallController e = b.GetComponent<WallController>();
		e.speed = s;
		return e;
	}

}
