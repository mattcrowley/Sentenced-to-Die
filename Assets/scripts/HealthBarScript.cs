using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public Vector2 size = new Vector2(60,20);
	public int healthValue;
	public int maxHealth;
	
	void Start(){
		healthValue = 100;
		maxHealth = 100;
	}
	
	void OnGUI() {
		GUIStyle myStyle = new GUIStyle ("box");
		myStyle.fontSize = this.gameObject.guiText.fontSize;
		this.gameObject.guiText.text = "Player's Health: " + healthValue;

		size = myStyle.CalcSize (new GUIContent (this.gameObject.guiText.text));
		
		GUI.color = Color.red;
		GUI.HorizontalScrollbar(new Rect (0,60,size.x,size.y), 0, healthValue, 0, maxHealth);
		
		GUI.color = Color.white;
		GUI.contentColor = Color.white;                
		GUI.Label (new Rect (0, 65, size.x, size.y), "Player's Health: " + healthValue + "/" + maxHealth);//, myStyle);
	}
	
	void Update() {
	}

	public void increaseHealth(int amt){
		healthValue += amt;

		if (healthValue > 100)
			healthValue = 100;
	}

	public void decreaseHealth(int amt){
		healthValue -= amt;
	}

}