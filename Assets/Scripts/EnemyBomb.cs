using UnityEngine;
using System.Collections;

public class EnemyBomb : SpaceShip {
	

	void Awake(){
		target = GameObject.Find("Player");

		ship_health = 50;
		ship_speed = 30;
		fire_speed = 40;
		fire_frequency = Random.Range (2f, 4f);

		transform.position = new Vector3(Random.Range(-9, 9)*10, 5, 160);
		move_point = new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 13)*10);

		StartCoroutine(FireBombDelay());
		AudioSource.PlayClipAtPoint(flying,transform.position,0.6f);
	}
	
	void Update () {
		MovementAlgoritm ();
	}

	IEnumerator FireBombDelay()	{
		while (target!=null){
			for (float timer = 0; timer < fire_frequency; timer += Time.deltaTime)
				yield return 0;
			FireBomb();
			fire_frequency = Random.Range (2f, 4f);
		}
	}

	void MovementAlgoritm(){
		if (!in_position) {
			if (MovingToMovePoint() < 0.1f) {
				in_position = true;
				ship_speed=10;
				move_point = new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 13)*10);
			}
		}
		else{
			if (MovingToMovePoint() < 0.1f) {
				move_point = new Vector3(Random.Range(-9, 9)*10, 5, Random.Range(5, 13)*10);
			}

		}
	}
}
