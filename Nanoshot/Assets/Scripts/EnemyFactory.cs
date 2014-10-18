using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	protected static EnemyFactory instance;

	public GameObject EnemyOrbPrefab;
	public GameObject EnemyDendritePrefab;

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

}
