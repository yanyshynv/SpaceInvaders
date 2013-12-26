using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform Camera;
	public float speed=70;

	public AudioClip fire;
	public AudioClip boom1;
	public AudioClip empire;

	private float mouse_position;

	void Start(){
		mouse_position=Input.mousePosition.x;
	}

	void Update () {
		if (!GameSettings.in_pause) {
			moveToMousePosition ();
			PlayerKeyController();
		}
	}

	void moveToMousePosition(){
		if(mouse_position!=Input.mousePosition.x){
			if (Input.mousePosition.x/Screen.width != (transform.position.x+95)/190) {
				transform.position=new Vector3 (Input.mousePosition.x*190/Screen.width-95,5,0);
				Camera.position=new Vector3 (Input.mousePosition.x*18/Screen.width-9,130,65);
			}
			mouse_position=Input.mousePosition.x;
		}
	}

	void PlayerKeyController(){
		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space)) {
			PlayerFire();
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			if (transform.position.x>-95) {
				transform.position-=new Vector3(speed*Time.deltaTime,0f,0f);
			}
		}
		
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
			if (transform.position.x<95) {
				transform.position+=new Vector3(speed*Time.deltaTime,0f,0f);
			}
		}
	}

	void PlayerFire () {
		GameObject bullet = Instantiate(Resources.Load("LaserRed"))as GameObject;
		bullet.transform.position = transform.position;
		AudioSource.PlayClipAtPoint(fire,transform.position);
	}

	public int GiveDamageToPlayer(int h){
		
		GameSettings.health-=h;
		
		if(GameSettings.health<0){
			GameSettings.health=0;
			DestroyPlayer();
		}else{
			AudioSource.PlayClipAtPoint(boom1,transform.position);
		}
		return GameSettings.health;
	}
	
	public void DestroyPlayer(){
		AudioSource.PlayClipAtPoint(empire,transform.position);
		GameSettings.u_r_dead=true;
		Destroy (gameObject);
	}
}