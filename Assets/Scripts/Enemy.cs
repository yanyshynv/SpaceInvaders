using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health = 20;
	public int ship_speed = 50;
	public int fire_speed = 50;
	public int num_in_group = 0;
	public int algoritm_num = 1;
	public float fire_frequency;
	private bool in_position = false;
	private bool in_position2 = true;
	public Vector3 start_point;
	private Vector3 move_point;
	public AudioClip fire;
	public AudioClip flying;

	void Start(){
		fire_frequency = Random.Range (1f, 4f);
		StartCoroutine(FireLaser());
		AudioSource.PlayClipAtPoint(flying,transform.position,0.6f);
		//start_point=new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 15)*10);
	}

	void Update () {
		if (GameSettings.change_algoritm){
			algoritm_num++;
			if(algoritm_num>3){algoritm_num=1;}
		}
		switch (algoritm_num) {
				case 1:
						MovementAlgoritm1 ();
						break;
				case 2:
						MovementAlgoritm2 ();
						break;
				case 3:
						MovementAlgoritm3 ();
						break;
				default:
						MovementAlgoritm1 ();
						break;
				}
	}

	void OnCollisionEnter (Collision col){
		Debug.Log (col.gameObject.name);
	}

	IEnumerator FireLaser()	{
		while (true){
			for (float timer = 0; timer < fire_frequency; timer += Time.deltaTime)
				yield return 0;
			Laser();
			fire_frequency = Random.Range (1f, 4f);
		}
	}

	void Laser(){
		GameObject bullet = Instantiate(Resources.Load("Laser"))as GameObject;
		bullet.transform.position = transform.position;
		Laser bt = bullet.GetComponent<Laser>();
		bt.enemy_tag = "Player";
		bt.speed=-fire_speed;
		AudioSource.PlayClipAtPoint(fire,transform.position,0.6f);
	}

	void MovementAlgoritm1(){
		if (!in_position) {
			switch(num_in_group){
			case 1: move_point=new Vector3(start_point.x+10, 5, start_point.z+10); break;
			case 2: move_point=new Vector3(start_point.x+10, 5, start_point.z-10); break;
			case 3: move_point=new Vector3(start_point.x-10, 5, start_point.z-10); break;
			case 4: move_point=new Vector3(start_point.x-10, 5, start_point.z+10); break;
			default: move_point=start_point; break;
			}
			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position = true;
				ship_speed=10;
			}
		}
		else{
			num_in_group++;
			if(num_in_group > 4){num_in_group = 1;}
			in_position = false;
		}
	}

	void MovementAlgoritm2(){
		if (!in_position) {
			switch(num_in_group){
			case 1: move_point=new Vector3(start_point.x+10, 5, start_point.z+10); break;
			case 2: move_point=new Vector3(start_point.x+10, 5, start_point.z-10); break;
			case 3: move_point=new Vector3(start_point.x-10, 5, start_point.z-10); break;
			case 4: move_point=new Vector3(start_point.x-10, 5, start_point.z+10); break;
			default: move_point=start_point; break;
			}
			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position = true;
				ship_speed=10;
			}
		}
		else{
			switch(num_in_group){
			case 1: move_point=new Vector3(start_point.x+20, 5, start_point.z+15); break;
			case 2: move_point=new Vector3(start_point.x+20, 5, start_point.z-15); break;
			case 3: move_point=new Vector3(start_point.x-20, 5, start_point.z-15); break;
			case 4: move_point=new Vector3(start_point.x-20, 5, start_point.z+15); break;
			default: move_point=start_point; break;
			}
			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position = false;
				ship_speed=10;
			}
		}
	}

	void MovementAlgoritm3(){
		if (!in_position) {
			switch(num_in_group){
			case 1: move_point=new Vector3(start_point.x+10, 5, start_point.z+10); break;
			case 2: move_point=new Vector3(start_point.x+10, 5, start_point.z-10); break;
			case 3: move_point=new Vector3(start_point.x-10, 5, start_point.z-10); break;
			case 4: move_point=new Vector3(start_point.x-10, 5, start_point.z+10); break;
			default: move_point=start_point; break;
			}
			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position = true;
				ship_speed=10;
			}

		}
		else{
			if(in_position2){
				switch(num_in_group){
				case 1: move_point=new Vector3(start_point.x, 5, start_point.z+10); break;
				case 2: move_point=new Vector3(start_point.x+20, 5, start_point.z-10); break;
				case 3: move_point=new Vector3(start_point.x, 5, start_point.z-10); break;
				case 4: move_point=new Vector3(start_point.x-20, 5, start_point.z+10); break;
				default: move_point=start_point; break;
				}
			}
			else{
				switch(num_in_group){
				case 1: move_point=new Vector3(start_point.x+20, 5, start_point.z+10); break;
				case 2: move_point=new Vector3(start_point.x, 5, start_point.z-10); break;
				case 3: move_point=new Vector3(start_point.x-20, 5, start_point.z-10); break;
				case 4: move_point=new Vector3(start_point.x, 5, start_point.z+10); break;
				default: move_point=start_point; break;
				}
			}

			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position2 = !in_position2;
				ship_speed=10;
			}
		}
	}
}
