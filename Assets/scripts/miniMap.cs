using UnityEngine;
using System.Collections;

public class miniMap : MonoBehaviour {
	Transform target;
	float damping  = 6.0f;
	bool smooth = true;

	// Use this for initialization
	void Start () {
		if (rigidbody) {
			rigidbody.freezeRotation = true;		
		}

		Debug.Log (target.position);
	}

	void LateUpdate(){
		if (target == true) {
			if (smooth == true){
				//look at and dampen the rotation
				Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*damping);
			}
			else{
				//just look at
				transform.LookAt(target);
			}
			Vector3 temp = new Vector3(target.position.x, target.position.y+90, target.position.z);
			transform.position = temp;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
