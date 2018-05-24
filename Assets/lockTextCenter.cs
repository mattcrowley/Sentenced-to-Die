using UnityEngine;
using System.Collections;

public class lockTextCenter : MonoBehaviour {
	
	void OnGUI(){
		if (this.gameObject.guiText.text != ""){
			GUIStyle myStyle = new GUIStyle("box");
			myStyle.fontSize = this.gameObject.guiText.fontSize;
			GUI.color = Color.white;
			myStyle.stretchWidth = true;
			myStyle.stretchHeight = true;
			
			//dimensions.x is width, y is height
			Vector2 dimensions = myStyle.CalcSize (new GUIContent (this.gameObject.guiText.text));
			
			GUI.Box (new Rect ( (Screen.width - dimensions.x)/2, (Screen.height - dimensions.y) /2,dimensions.x, dimensions.y), this.gameObject.guiText.text, myStyle);
		}
	}
	
}