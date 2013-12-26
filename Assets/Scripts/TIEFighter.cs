using UnityEngine;
using System.Collections;

public class TIEFighter : SpaceShip {

	public int num_in_group = 0;			//Номер винищувача в групі
	public int algoritm_num = 1;			//Номер алгоритму руху
	public bool in_position2 = true;		//Чи виницувач на позиції №2

	void Awake(){
		//Пошук ворога
		target = GameObject.Find("Player");
		//Налаштування бойових характеристик
		ship_health = 20;
		ship_point = 10;
		ship_speed = 50;
		fire_speed = 50;
		fire_frequency = Random.Range (3f, 5f);
		//Запуск таймера пострілів
		StartCoroutine(FireLaserDelay());
		AudioSource.PlayClipAtPoint(flying,transform.position,0.6f);
	}

	void Update () {
		//Перевірка на зміну алгориту руху
		if (GameSettings.change_algoritm){
			algoritm_num++;
			if(algoritm_num>3){algoritm_num=1;}
			in_position = false;
			in_position2 = true;
		}
		//Вибір алгоритму руху
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

	IEnumerator FireLaserDelay(){
		//Затримка пострілу
		while (target!=null){
			for (float timer = 0; timer < fire_frequency; timer += Time.deltaTime)
				yield return 0;
			FireLaser();
			fire_frequency = Random.Range (3f, 5f);
		}
	}

	void SwitchMovePoint(){
		//Вибір бажаної точки місцезнаходження на основі початкової точки
		switch(num_in_group){
			case 1: move_point=new Vector3(start_point.x+10, 5, start_point.z+10); break;
			case 2: move_point=new Vector3(start_point.x+10, 5, start_point.z-10); break;
			case 3: move_point=new Vector3(start_point.x-10, 5, start_point.z-10); break;
			case 4: move_point=new Vector3(start_point.x-10, 5, start_point.z+10); break;
		default: move_point=start_point; break;
		}
	}

	void MovementAlgoritm1(){
		//Алгоритм руху №1
		if (!in_position) {
			SwitchMovePoint();
			if (MovingToMovePoint() < 0.1f) {
				in_position = true;
				ship_speed=10;
			}
		}else{
			num_in_group++;
			if(num_in_group > 4){num_in_group = 1;}
			in_position = false;
		}
	}

	void MovementAlgoritm2(){
		//Алгоритм руху №2
		if (!in_position) {
			SwitchMovePoint();
			if (MovingToMovePoint() < 0.1f) {
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
			if (MovingToMovePoint() < 0.1f) {
				in_position = false;
				ship_speed=10;
			}
		}
	}

	void MovementAlgoritm3(){
		//Алгоритм руху №3
		if (!in_position) {
			SwitchMovePoint();
			if (MovingToMovePoint() < 0.1f) {
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
			if (MovingToMovePoint() < 0.1f) {
				in_position2 = !in_position2;
				ship_speed=10;
			}
		}
	}
}
