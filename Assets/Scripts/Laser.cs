using UnityEngine;
using System.Collections;

public class Laser : Bullet {

	void Awake () {
		//Налаштування бойових параметрів
		bullet_speed = 100;
		bullet_power = 10;
		target_tag = "Enemy";
		//Запуск таймера самознищення
		StartCoroutine(SelfDestroyDelay());
	}

	void Update () {
		//переміщення по траєкторії
		transform.position=new Vector3 (transform.position.x,transform.position.y,transform.position.z+bullet_speed*Time.deltaTime);
	}
}