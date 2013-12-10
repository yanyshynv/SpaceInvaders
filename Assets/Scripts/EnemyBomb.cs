using UnityEngine;
using System.Collections;

public class EnemyBomb : MonoBehaviour {
	
	public int health = 50;
	public int ship_speed = 30;
	public int fire_speed = 40;
	public float fire_frequency;
	private bool in_position = false;
	public Vector3 start_point;
	private Vector3 move_point;
	public AudioClip fire;
	public AudioClip flying;
	
	void Start(){
		fire_frequency = Random.Range (2f, 4f);
		StartCoroutine(FireLaser());
		AudioSource.PlayClipAtPoint(flying,transform.position,0.6f);
		//start_point=new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 15)*10);
	}
	
	void Update () {
			MovementAlgoritm ();
	}
	
	void OnCollisionEnter (Collision col){
		Debug.Log (col.gameObject.name);
	}
	
	IEnumerator FireLaser()	{
		while (true){
			for (float timer = 0; timer < fire_frequency; timer += Time.deltaTime)
				yield return 0;
			Laser();
			fire_frequency = Random.Range (2f, 4f);
		}
	}
	
	void Laser(){
		GameObject pl = GameObject.Find("Player");
		if(pl!=null){
			GameObject bullet = Instantiate(Resources.Load("Bomb"))as GameObject;
			bullet.transform.position = transform.position;
			Bomb bt = bullet.GetComponent<Bomb>();
			bt.player_point = pl.transform.position;
			bt.player_point = pl.transform.position;
			AudioSource.PlayClipAtPoint(fire,transform.position,0.6f);
		}
	}
	
	void MovementAlgoritm(){
		if (!in_position) {
			transform.position = Vector3.MoveTowards (transform.position, start_point, ship_speed * Time.deltaTime);
			Vector3 direction = start_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				in_position = true;
				ship_speed=10;
				move_point = new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 13)*10);
			}
		}
		else{
			transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
			Vector3 direction = move_point - transform.position;
			float distance = direction.magnitude;
			if (distance < 0.5f) {
				move_point = new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 13)*10);
			}

		}
	}
}
