using UnityEngine;
using System.Collections;

public class clickedPlayButton : MonoBehaviour {
	GameObject scrollText;
	bool visibleButton = true;


	// Use this for initialization
	void Awake () {
		scrollText = GameObject.Find ("Scrolling Text");	
		GameObject.Find ("Main Camera").transform.position = new Vector3 (6.895855e-07f, 0.002691269f, 7.887939f); 
		GameObject.Find ("Main Camera").transform.eulerAngles = new Vector3 (0, 180, 0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		//GUI.backgroundColor = Color.clear; //makes transparent button

		//old rect
		//new Rect((422) screen.width/2.5f? maybe , (412) screen.height/1.45f

		//optimized for 1024X768 only!!!!
		if (visibleButton) {
			if (GUI.Button (new Rect (Screen.width / 2.77f, Screen.height / 1.45f, 288, 64), "")) {
				scrollText.GetComponent<npcDisplayText> ().talking = true;
				scrollText.GetComponent<npcDisplayText> ().currentLine = 0;
					
				visibleButton = false; //do not show button again
					
				StartCoroutine (scrollText.GetComponent<npcDisplayText> ().startScrolling ());

			}
		}


	}

}
