using UnityEngine;
using System.Collections;

public class MonsterHit : MonoBehaviour {
	public VisualHealthBar bar;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (bar.health <= 0) {
			this.gameObject.SetActive(false);
			GameObject player = GameObject.Find("First Person Controller");
			player.GetComponent<PlayerController>().numMonsters--;
			player.GetComponent<PlayerController>().setMonText();
			PlayerController p = GameObject.Find("First Person Controller").GetComponent<PlayerController>();

			if (p.numMonsters == 0){
				GameObject.Find("Game Controller").GetComponent<GameController>().gameWon = true;
			}
		}
	}
	

	void OnCollisionEnter(Collision weapon) {
		if (weapon.gameObject.tag == "WeaponItem" && (bar.health > 0 || this.gameObject.tag == "NormalMonster" || this.gameObject.tag=="BossMonster")) {
			weapon.gameObject.SetActive(false);

			//this is for stupid skeletons...
			if (this.gameObject.name == "spear" || this.gameObject.name == "woodenshield" || this.gameObject.name == "Bip01"){
				this.transform.parent.GetComponent<VisualHealthBar>().decreaseHealth(10);
			}
			else{
				//now deplete health of monster
				bar.decreaseHealth(10);
			}

		}
	}

}
