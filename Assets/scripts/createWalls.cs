using UnityEngine;
using System.Collections;

public class createWalls : MonoBehaviour {
	public Terrain ground;

	// Use this for initialization
	void Start () {
		//Debug.Log(ground.terrainData.size.z); //width, height, length
											  //x	 , y	 , z
		addWall (ground.terrainData.size, "North");
		addWall (ground.terrainData.size, "South");
		addWall (ground.terrainData.size, "East");
		addWall (ground.terrainData.size, "West");
		
	}

	public void addWall(Vector3 terData, string side){
		GameObject Wall = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Wall.transform.localScale = new Vector3 (1, 1, ground.terrainData.size.z);

		if (side == "North") {
			float zPos = ground.terrainData.size.z / 2 + ground.GetPosition().z;
			Wall.transform.position = new Vector3 (ground.GetPosition().x, 0, zPos); //set cube in center
			Wall.name = "North Wall";
		} 
		else if (side == "South") {
			float zPos = ground.terrainData.size.z / 2 + ground.GetPosition().z;
			float xPos = ground.GetPosition().x + ground.terrainData.size.x; //sets xPos to be the terrain's pos + the length of the terrain
			Wall.transform.position = new Vector3 (xPos, 0, zPos); //set cube in center south								
			Wall.name = "South Wall";
		}
		else if (side == "West"){
			//need to rotate:
			float zPos = ground.GetPosition().z;
			float xPos = ground.terrainData.size.x/2 + ground.GetPosition().x;
			Wall.transform.localScale = new Vector3(ground.terrainData.size.x , 1, 1);
			Wall.transform.position = new Vector3 (xPos, 0, zPos); //set cube in center south	
			Wall.name = "West Wall";
		}
		else if (side == "East"){
			//need to rotate:
			float zPos = ground.terrainData.size.z + ground.GetPosition().z;
			float xPos = ground.terrainData.size.x / 2 + ground.GetPosition().x;
			Wall.transform.localScale = new Vector3(ground.terrainData.size.x , 1, 1);
			Wall.transform.position = new Vector3 (xPos, 0, zPos); //set cube in center south	
			Wall.name = "East Wall";
		}

		Wall.transform.parent = GameObject.Find ("Walls").transform;
		Wall.layer = GameObject.Find ("Walls").layer;

	}
}
