using UnityEngine;
using System.Collections;

public class VisualHealthBar : MonoBehaviour {

	public float health;
	float maxHealth;
	
	float adjustment = 2.3f;
	private Vector3 worldPosition = new Vector3();
	private Vector3 screenPosition = new Vector3();
	private Transform myTransform;
	private Camera myCamera;
	private int healthBarHeight = 5;
	private int healthBarLeft = 110;
	private int barTop = 1;
	private GUIStyle myStyle = new GUIStyle();
	
	
	//assign the camera to a variable so we can raycast from it
	private GameObject myCam;

	void Awake()
	{
		myCam = GameObject.Find("Main Camera"); 

		myTransform = transform;
		myCamera = Camera.main;
		health = 100; 
		maxHealth = 100;
	}
	
	void OnGUI()
	{
		worldPosition = new Vector3(myTransform.position.x, myTransform.position.y + adjustment,myTransform.position.z);
		screenPosition = myCamera.WorldToScreenPoint(worldPosition);
		
		//creating a ray that will travel forward from the camera's position   
		Ray ray = new Ray (myCamera.transform.position, transform.forward);
		RaycastHit hit;
		//Vector3 forward = transform.TransformDirection(Vector3.forward);
		float distance = Vector3.Distance(myCamera.transform.position, transform.position); //gets the distance between the camera, and the intended target we want to raycast to
		
		//if something obstructs our raycast, that is if our characters are no longer 'visible,' dont draw their health on the screen.
		//if (!Physics.Raycast(ray, out hit, distance))
		if (distance <= 7.0f){
		//{
			//Debug.Log("IN RANGE");
			GUI.color = Color.red;
			GUI.HorizontalScrollbar(new Rect (screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop, 100, 0), 0, health, 0, maxHealth); //displays a healthbar
			
			GUI.color = Color.white;
			GUI.contentColor = Color.white;                
			GUI.Label(new Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop+5, 100, 100), ""+health+"/"+maxHealth); //displays health in text format
		}
	}

	public void decreaseHealth(float value){
		health -= value;
	}
}
