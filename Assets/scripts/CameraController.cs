using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
		//Debug.Log (offset.ToString ());
	}
	

	void LateUpdate () { //best used for follow camera, last known states, etc
		Vector3 pos = new Vector3 (player.transform.position.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
		//transform.position = player.transform.position + offset;
		transform.position = pos;
	}
}
