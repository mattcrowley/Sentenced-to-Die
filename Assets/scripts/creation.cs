using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;



public class creation : MonoBehaviour {
	public int numItems;
	public int rangeX;
	public int rangeY;
	public Terrain ground;
	
	public GameObject shape;
	GameObject[] pickupItems;
	GameObject parent;

	// Use this for initialization
	void Awake () {
		numItems = Random.Range (rangeX, rangeY);
		pickupItems = new GameObject[numItems];

		parent = GameObject.Find ("Pickups");

		createGameItems ();
	}

	void createGameItems(){
		for (int i = 0; i < numItems; i++) {
			Vector3 a = new Vector3(Random.Range (ground.GetPosition().x + 0.5f,
			                          ground.GetPosition().x+ground.terrainData.size.x - 0.5f),
			            0.5f,
			            Random.Range (ground.GetPosition().z + 0.5f,
			              ground.GetPosition().z+ground.terrainData.size.z - 0.5f));

			GameObject temp = Instantiate(shape, a,
			                              transform.rotation) as GameObject;
			temp.gameObject.name = "ITEM " + (i+1);
			temp.transform.parent = parent.transform;
			temp.layer = parent.layer;
			pickupItems[i] = temp;


		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
