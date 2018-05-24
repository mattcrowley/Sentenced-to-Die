using UnityEngine;
using System.Collections;
using System.Collections.Generic; //for using List
using Random = UnityEngine.Random;


//this class will load the maze, the player's position, and essentially run the game from start to finish.
public class GameController : MonoBehaviour {
	private static bool spawned = false;

	GameObject pLight;

	Vector3 playersPos;
	GameObject player; //used in this class, but does not need to be assigned, will be Found

	GameObject maze;
	MazeGenerator randomMaze;

	MazeGenerator tempMaze;

	float mazeWidth; //holds size of maze to use later when generating monster's pos's and player's pos's.
	float mazeHeight;

	public bool gameWon = false;
	int numNormalMonsters = 4; //how many normal monsters to generate
	int numBossMonsters = 3;
	int numHiddenMonsters = 1;
		
	public List<GameObject> monsterChoices; //object to hold monster prefabs to create in random positions
	public List<GameObject> bossMonsterChoices;
	public List<GameObject> hiddenMonsterChoices;
	
	public List<GameObject> weaponPowerups; //needs to be its own, because all monsters must be killed to win the game
	int numWeaponPowerups;

	public List<GameObject> otherPowerups; //health or flashlight upgrade at this moment
	int numOtherPowerups;

	bool continueGame = true; //variable to make the game pause
	bool backToMenu = false;
	bool backToStart = false;

	float curTime;


	void Awake(){
		numWeaponPowerups = numNormalMonsters + numBossMonsters + numHiddenMonsters + 6; //need enough to kill ALL
		numOtherPowerups = otherPowerups.Count;

		player = GameObject.Find ("First Person Controller");
		pLight = GameObject.Find ("Player's Light");
		maze = GameObject.Find ("MAZE");

		if (player == null) {
			//Debug.Log("player not found");
		}

		if (spawned == false) {
			//Debug.Log("Not spawned yet, creating stuff");
			spawned = true;
			DontDestroyOnLoad (gameObject);

			randomMaze = maze.GetComponent<MazeGenerator> ();

			runGameLoop ();
		} 
		else {
			//Debug.Log("Already spawned!");

			randomMaze = maze.GetComponent<MazeGenerator>();

			//then place the monsters in the maze randomly.
			genMonstersPos ();

			genPowerupPos();

			DestroyImmediate(gameObject);
		}

	}

	
	void runGameLoop(){
		createMaze ();

		//place the monsters in the maze randomly.
		genMonstersPos ();

		//now, place player in the maze randomly.
		genPlayerPos ();

		genPowerupPos ();
	}

	void createMaze(){
		//Debug.Log ("creating new maze");
		randomMaze.LayFloorTiles();
		randomMaze.PlaceBorderWalls();
		randomMaze.CreateMazeThroughRecusriveDivision();

		tempMaze = randomMaze;
	}

	//code created by Pascual, combined by Matt, edited by Matt
	void genPlayerPos(){
		Collider[] c;

		//this does not allow the player to spawn inside the walls of the maze
		do {
			Vector3 randomPos = new Vector3 (Random.Range (2, randomMaze.Width * 2 - 4), 2.05f, Random.Range (2, randomMaze.Height * 2 - 4));

			player.GetComponent<randomPlayerLocation> ().createPlayerLocation (randomPos);
			c = Physics.OverlapSphere (randomPos, .4f);

			playersPos = randomPos;

		} while (c.Length != 0);
	}

	//code started by Pascual, finished and edited by Matt
	void genMonstersPos(){
		//generate the normal monsters:
		for (int i = 0; i < numNormalMonsters; i++) {
			Vector3 randomPos;
			randomPos = new Vector3 (Random.Range (2, randomMaze.Width*2-4), 1, Random.Range (2, randomMaze.Height*2-4));
	
			int indexMonster = Random.Range(0, monsterChoices.Count-1); //Count gets total size, subtract 1 for the index
			GameObject monsterChosen = monsterChoices[indexMonster]; //this is the monster we will instantiate now...

			GameObject go = Instantiate (monsterChosen, randomPos, Quaternion.identity) as GameObject;

			go.transform.parent = GameObject.Find ("Monsters").transform;
			go.transform.name = "Monster #" + (i+1);
		}

		//now generate that one hidden/cloaked monster:
		for (int i = 0; i < numHiddenMonsters; i++){
			Vector3 randomPos;
			randomPos = new Vector3 (Random.Range (2, randomMaze.Width*2-4), 1, Random.Range (2, randomMaze.Height*2-4));

			GameObject monsterChosen = hiddenMonsterChoices[0]; //at this point we only have ONE cloaked monster

			GameObject go = Instantiate (monsterChosen, randomPos, Quaternion.identity) as GameObject;

			go.transform.parent = GameObject.Find ("Monsters").transform;
			go.transform.name = "Cloaked Monster #" + (i+1);
		}

		//now finally generate one of each of David's monster designs, as they are Boss monsters:
		for (int i = 0; i < numBossMonsters; i++){
			Vector3 randomPos;
			randomPos = new Vector3 (Random.Range (2, randomMaze.Width*2-4), 1, Random.Range (2, randomMaze.Height*2-4));
			
			GameObject monsterChosen = bossMonsterChoices[i]; //at this point we only have 3 boss monsters
			
			GameObject go = Instantiate (monsterChosen, randomPos, Quaternion.identity) as GameObject;
			
			go.transform.parent = GameObject.Find ("Monsters").transform;
			go.transform.name = "Boss Monster #" + (i+1);
		}
		player.GetComponent<PlayerController> ().numMonsters = numNormalMonsters + numHiddenMonsters + numBossMonsters;
		player.GetComponent<PlayerController> ().setMonText ();

	}

	Vector3 colliding(){
		bool collideWall;
		Vector3 randomPos = new Vector3 (Mathf.Round(Random.Range (2, randomMaze.Width*2-4)), 1,
		                                             Mathf.Round(Random.Range (2, randomMaze.Height*2-4)));;
		Collider[] c;

		do{
			collideWall = false;
			
			c = Physics.OverlapSphere(randomPos, weaponPowerups[0].GetComponent<BoxCollider>().size.x/2.0f);
			
			//yeah
			for (int k = 0; k < c.Length; k++){ //this will test if the powerup is embedded inside a wall (not the floor).
				//Debug.Log(c[k].tag);
				if (c[k].tag == "innerWall" || c[k].tag == "outerWall"){
					collideWall = true;
					break;
				}
			}
			
			if (collideWall == true){ //if the object is embedded inside a wall...
				//shift the created object's position, only x or z:
				randomPos.x += 1;
				randomPos.z += 1;
				if (randomPos.z >= randomMaze.Width*2-4) //off screen, then reset to new position
					randomPos.z = Mathf.Round(Random.Range(2, randomMaze.Height*2-4));
				if (randomPos.x >= randomMaze.Width*2-4)
					randomPos.x = Mathf.Round(Random.Range(2, randomMaze.Width*2-4));
			}
		} while(collideWall == true);

		return randomPos;
	}

	//created by MATT:
	void genPowerupPos(){
		Vector3 randomPos;
		//first generate the weapons:
		for (int i = 0; i < numWeaponPowerups; i++) {
			randomPos = colliding ();
			
			GameObject power = weaponPowerups[0];
			
			GameObject go = Instantiate(power, randomPos, power.transform.rotation) as GameObject;
			
			go.SetActive(true);
			
			go.transform.parent = GameObject.Find ("Powerups").transform;
			go.transform.name = "Weapon Powerup #" + (i+1);
			
		}

		//now generate the health and flashlight upgrade
		for (int i = 0; i < numOtherPowerups; i++) {
			randomPos = colliding ();
			
			GameObject power = otherPowerups[i];
			
			GameObject go = Instantiate(power, randomPos, power.transform.rotation) as GameObject;
			
			go.SetActive(true);
			
			go.transform.parent = GameObject.Find ("Powerups").transform;
			go.transform.name = "Other Powerup #" + (i+1);
			
		}
	}

	public void goToMenu(){
		spawned = false;
		Screen.lockCursor = false;
		Destroy(tempMaze.transform.root.gameObject);
		DestroyImmediate (this.gameObject);
		
		Application.LoadLevel(0);
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.Find ("First Person Controller");
		}
		if (pLight == null) {
			pLight = GameObject.Find("Player's Light");
		}
		if (pLight != null)
			pLight.transform.position = player.transform.position;

		if (backToMenu && (Time.time >= curTime + 5f)) { //end of game, you all won!
			backToMenu = false;
			goToMenu();
		}
		if (backToStart && Time.time >= curTime + 3f){ //reload current scene!
			backToStart = false;
			Application.LoadLevel(1);
		}

		if (continueGame && ((player != null && player.GetComponent<PlayerController>().numMonsters == 0) || gameWon == true)) {
			continueGame = false;
			//print a "You won!" message
			player.GetComponentInChildren<PlayerController>().setWinText();
			
			StartCoroutine("pauseSeconds", 5);
		//	StopCoroutine("pauseSeconds");
			//send player back to main menu:
			backToMenu = true;
			curTime = Time.time;
		}

		if (continueGame && (player != null && gameWon == false && player.GetComponentInChildren<HealthBarScript>().healthValue <= 0)) {
			continueGame = false;
			Debug.Log("player is dead!!!");
			player.transform.position = playersPos;
			//player.GetComponentInChildren<HealthBarScript>().healthValue = 100;

			DontDestroyOnLoad(tempMaze.transform.root.gameObject);

			player.GetComponentInChildren<PlayerController>().playerDeadText.text = "You are DEAD!!!!";

			//right now, we want to pause the game to display text saying the player is dead:
			StartCoroutine("pauseSeconds", 3);
	//		StopCoroutine("pauseSeconds");

			//Application.LoadLevel(1);
			backToStart = true;
			curTime = Time.time;

			//Application.LoadLevel(1);

		}
	
	}

	public IEnumerator pauseSeconds(int numSec){
		yield return new WaitForSeconds ((float) numSec);
		continueGame = true;
	}
}
