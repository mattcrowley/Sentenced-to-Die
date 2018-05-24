using UnityEngine;
using System.Collections;

public class GUICrosshair : MonoBehaviour {

	public Texture2D crosshairTexture;
	private Rect position;
	public bool OriginalOn = true;
	
	// Update is called once per frame
	void Update () {
		//position = Rect ((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
		position = new Rect ((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
	}

	void OnGUI (){
		if (OriginalOn == true) {
			GUI.DrawTexture(position, crosshairTexture);
		}
	}
}
