using UnityEngine;
using System.Collections;

public class projectileCollideWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "Wall" || other.gameObject.name == "OutterWall"
		    || other.gameObject.name == "Floor" || other.gameObject.name == "Ceiling"
		    ) {
			this.gameObject.SetActive(false);
		}

		if (other.gameObject.name == "Fake Wall") { //destroy both projectile and fake wall!
			this.gameObject.SetActive(false);

			other.gameObject.SetActive(false);
		}

	}

}
