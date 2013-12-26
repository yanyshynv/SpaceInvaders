using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	public int max_size=20;			//Допустимий розмір вибуху
	public float time=2;			//Час життя вибуху

	private float speed=0;			//Швидкість збільшення розміру вибуху

	void Start(){
		//Запуск зміни розміру вибуху
		speed=(max_size-1)/time;
		StartCoroutine(Countdown());
	}

	void Update () {
		//Збільшення розмірів вибузу до домустимого значення
		if (transform.localScale.x < max_size) {
			transform.localScale += new Vector3 (speed * Time.deltaTime, 0f, speed * Time.deltaTime);
		}
	}

	IEnumerator Countdown()	{
		//Таймер життя вибуху
		for (float timer = time; timer >= 0; timer -= Time.deltaTime) {
			yield return 0;
		}
		Destroy(gameObject);
	}
}
