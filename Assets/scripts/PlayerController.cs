using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public GUIText weaponCountText;
	public GUIText monsterCountText;
	public GUIText playerDeadText;
	public GUIText winText;

	public int numStartWeapons;
	
	public int numWeapons; //holds number of weapons we can throw

	public int numMonsters; //total number of monsters in game currently
	public AudioClip[] sound = new AudioClip[5];


	void Start(){
		numWeapons = numStartWeapons;

		setCountText ();
		InvokeRepeating("PlayClipAndChange",5.0f,10.0f);
		winText.text = "";
		playerDeadText.text = "";

	}
	void PlayClipAndChange(){
		audio.clip = sound[Random.Range(0, 5)];
		audio.Play();
	}

	void OnControllerColliderHit(ControllerColliderHit other){
		//not the best way to handle this, but it works
		if ((other.transform.parent.tag == "NormalMonster") || (other.transform.parent.tag == "BossMonster")
		    || other.transform.tag == "NormalMonster" || other.transform.tag == "BossMonster") {
			EnemyAI ai = other.transform.parent.gameObject.GetComponent<EnemyAI> ();
			EnemyAI ai2 = other.transform.gameObject.GetComponent<EnemyAI>();

			if ( (ai != null && ai.hasAttacked == false) || (ai2 != null && ai2.hasAttacked == false) ) {
				//Debug.Log ("Normal monster is attacking!!!");

				//change animation to attacking animation
				//other.gameObject.GetComponent<Animator>().animation 

				this.GetComponentInChildren<HealthBarScript> ().healthValue -= 10;



				//need to set some boolean variable in the other object, so we do not get hit 100 times at once
				//Debug.Log ("Current Time: " + Time.time);
				if (ai != null)
					ai.waitTime (Time.time);
				else if (ai2 != null)
					ai2.waitTime (Time.time);
			}
			if (ai == null && ai2 == null){
				//Debug.Log("NO AI ATTACHED!!!!");
				//Debug.Log("parent: " + other.transform.parent.name);
				//Debug.Log("self: " + other.transform.name);
			}
		}
		else if (other.transform.tag == "weaponPowerup"){
			this.numWeapons += 10;
			other.gameObject.SetActive(false);
			setCountText();
		}
		else if (other.transform.tag == "healthPowerup"){
			this.GetComponentInChildren<HealthBarScript>().increaseHealth(50);
			other.gameObject.SetActive(false);
		}
		else if (other.transform.tag == "lightPowerup"){ //increase flashlight power permanently
			this.GetComponentInChildren<Light>().intensity = 3.5f;
			other.gameObject.SetActive(false);
		}
	}

	public void Update(){
		if (numMonsters == 0) {
			GameObject.Find("Game Controller").GetComponent<GameController>().gameWon = true;
		}
	}


	public void setCountText(){
		weaponCountText.text = "Weapons Left: " + numWeapons.ToString ();
	}

	public void setMonText(){
		monsterCountText.text = "Monsters Left: " + numMonsters.ToString ();
	}

	public void setWinText(){
		winText.text = "You managed to escape from the hell in your mind. \nYou're finally free...\nFree to die.";
	}
}

//Destroy(other.gameObject);