using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float timeToWait = 2; //attack speed
	public bool hasAttacked = false;
	public float timeAttacked = -1.0f;

	public float Speed = 3.0f;
	public float rotationSpeed = 3.0f;
	private Transform myTransform;
	private Transform target;
	private float distance;
	private Vector3 lookDir;
	
	// Use this for initialization
	void Awake ()
	{
		myTransform = transform;

	}
	
	void Start ()
	{
		target = GameObject.Find("First Person Controller").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (hasAttacked == true && (timeAttacked + timeToWait <= Time.time)) {
			hasAttacked = false;
		}
		
		distance = Vector3.Distance (target.position, myTransform.position);

		lookDir = target.position - myTransform.position;
		
		if (distance < 10.0f) {
			//myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
			  //                                       Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);

			Vector3 newRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position).eulerAngles;
			newRotation.x = 0;
			newRotation.z = 0;
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.Euler(newRotation), Time.deltaTime);


			//myTransform.LookAt(target);
			//move towards the player
			rigidbody.AddForce(lookDir.normalized*Speed);
			this.rigidbody.velocity = lookDir.normalized*Speed;
			//Debug.Log(myTransform.forward);
			//myTransform.position += myTransform.forward * Speed * Time.deltaTime;


		} else if (distance > 10.0f || distance <= 3) {
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
			                                         Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		}
	}

	public void waitTime(float curTimeHit){
		hasAttacked = true;
		timeAttacked = curTimeHit;
	}

}
