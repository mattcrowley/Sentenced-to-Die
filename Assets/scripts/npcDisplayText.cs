using UnityEngine;
using System.Collections;

public class npcDisplayText : MonoBehaviour {

	// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
	// Do test the code! You usually need to change a few small bits.
	
	public Transform player;
	public GUIText talkTextGUI;
	public GUITexture[] textures;
	public string[] talkLines;
	public float textScrollSpeed=20;

	public int distanceToChat=5;
	public bool  talking;
	private bool  textIsScrolling;
	public int currentLine;
	
	public void  Awake (){
		for(int i= 0;i<textures.Length;i++){
			//Disables all NPC textures until they are called upon during dialogue
			textures[i].enabled = false;
		}
		textScrollSpeed = 1;
		currentLine = 0;
	}

	public void  Update (){
		if(talking){
			if(Input.GetButtonDown("Fire1")){
				if(textIsScrolling){
					talkTextGUI.text = talkLines[currentLine];
					textIsScrolling = false;
				}
				else{
					talkTextGUI.text = "";
					if(currentLine < talkLines.Length - 1){

						currentLine+= 1;
						startScrolling();
					}
					else{
						currentLine = 0;
						talkTextGUI.text = "";
						talking = false;
						for(int i= 0;i<textures.Length;i++){
							textures[i].enabled = false;
						}
						//End of chat.
						//Add custom end of dialogue functions here.

						Application.LoadLevel(1);
					}
				}
			}
		}
	}

	public IEnumerator startScrolling (){
		textIsScrolling = true;
		talking = true;
		int startLine = currentLine;
		string displayText = "";

		for(int i = 0; i < talkLines[currentLine].Length; i++){
			if(textIsScrolling && currentLine == startLine){
				displayText += talkLines[currentLine][i];
				talkTextGUI.text = displayText;
				textures[currentLine].enabled = true;
				yield return new WaitForSeconds(textScrollSpeed / 100);
				if(currentLine == 0){
					textures[0].enabled = true;
					//Debug.Log(textures[0].enabled);
				}
				if(currentLine == 1){
					Debug.Log ("sdfsdlkfjsdlijf");
					textures[0].enabled = false;
					textures[1].enabled = true;
				}
				if(currentLine == 2){
					textures[1].enabled = false;
					textures[2].enabled = true;
				}
				if(currentLine == 3){
					textures[2].enabled = false;
					textures[3].enabled = true;
				}
				if(currentLine == 4){
					textures[3].enabled = false;
					textures[4].enabled = true;
				}
				if(currentLine == 5){
					textures[4].enabled = false;
					textures[5].enabled = true;
				}
				if(currentLine == 6){
					textures[5].enabled = false;
					textures[6].enabled = true;
				}
				if(currentLine == 7){
					textures[6].enabled = false;
					textures[7].enabled = true;
				}
				if(currentLine == 8){
					textures[7].enabled = false;
					textures[8].enabled = true;
				}
				if(currentLine == 9){
					textures[8].enabled = false;
					textures[9].enabled = true;
				}
			}
			else{
				yield break;
			}
		}
		textIsScrolling = false;	
	}
}
