using UnityEngine;
using System.Collections;

public class FireWeapon : MonoBehaviour {
	public Rigidbody projectile;
	float distance = 50.0f;

	PlayerController p;

	void Start(){
		p = this.GetComponent<PlayerController> ();
	}
	
	void Update() {
		if (this.enabled == true && Input.GetButtonDown("Fire1") && p.numWeapons != 0) {
			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
			position = Camera.main.ScreenToWorldPoint(position);

			//projectile.transform.eulerAngles = new Vector3(0,0,270);

		
			Rigidbody go = Instantiate(projectile, transform.position, projectile.rotation) as Rigidbody;

			//go.transform.eulerAngles = new Vector3(0,0,270);
			go.transform.LookAt(position);


			go.AddForce(go.transform.forward * 100);
			go.velocity = go.transform.forward *40;

			go.gameObject.SetActive(true);
			go.tag = "WeaponItem";

			//added in to account for limited weapons
			p.numWeapons--;
			p.setCountText();
		}
	}

}
