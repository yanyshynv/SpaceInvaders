using UnityEngine;
using System.Collections;

public class Bomb : Bullet {
	
	void Awake () {
		//Налаштування бойових параметрів
		bullet_speed = 40;
		bullet_power = 15;
		target_tag = "Player";
		//Запуск таймера самознищення
		StartCoroutine(SelfDestroyDelay());
	}

	void Update () {
		//Переміщення до цілі
		if (MoveToTarget() < 0.1f) {
			GameObject bang = Instantiate(Resources.Load("Boom")) as GameObject;
			bang.transform.position = transform.position;
			SelfDestroy();
		}
	}
}