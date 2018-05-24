using UnityEngine;
using System.Collections;

public class randomPlayerLocation : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createPlayerLocation(Vector3 newLoc){
		this.transform.position = newLoc;

		GameObject light = GameObject.Find ("Player's Light");
		light.transform.position = newLoc;
	}
}
