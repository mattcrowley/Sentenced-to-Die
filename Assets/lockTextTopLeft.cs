using UnityEngine;
using System.Collections;

public class lockTextTopLeft : MonoBehaviour {

	void OnGUI(){
		//GUIStyle style = new GUIStyle ();
		//style.CalcMinMaxWidth (new GUIContent(this.gameObject.guiText.text), out 0f, out 1000f);
		//style.
		GUIStyle myStyle = new GUIStyle("box");
		myStyle.fontSize = this.gameObject.guiText.fontSize;
		GUI.color = Color.white;
		myStyle.stretchWidth = true;
		myStyle.stretchHeight = true;

		//dimensions.x is width, y is height
		Vector2 dimensions = myStyle.CalcSize (new GUIContent (this.gameObject.guiText.text));

		GUI.Box (new Rect (0,0,dimensions.x, dimensions.y), this.gameObject.guiText.text, myStyle);

		//GUI.color = Color.green;
		//GUI.HorizontalScrollbar(new Rect (0,60,60,20), 0, );
		
		//GUI.color = Color.white;
		//GUI.contentColor = Color.white;                
		//GUI.Label (new Rect (0, 65, size.x, size.y), "Player's Health: " + healthValue + "/" + maxHealth);//, myStyle);

	}
	
}