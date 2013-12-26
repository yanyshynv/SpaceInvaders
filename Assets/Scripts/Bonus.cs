using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public int bonus = 10;				//Розмір бонусу
	public int speed = 10;				//швидкість руху бонусу
	public Vector3 move_point;			//Бажана точка місцезнаходження
	
	public AudioClip taking;			//Звук при отриманні бонусу
	public AudioClip destroy;			//Звук при самознищенні бонусу
	
	void Awake(){
		//Запуск таймеру самознищення
		StartCoroutine(SelfDestroyDelay());
		//Генерація бажаної точки місцезнаходження
		move_point = new Vector3 (Random.Range (-150, 150), 5, 0);
	}
	
	void Update(){
		MovingAway ();
	}
	
	void MovingAway(){
		//Переміщення бонусу
		transform.position=Vector3.MoveTowards (transform.position, move_point, speed * Time.deltaTime);
	}
	
	public void SelfDestroy(){
		///Самознищення бонусу
		AudioSource.PlayClipAtPoint(destroy,transform.position);
		Destroy(gameObject);
	}
	
	public IEnumerator SelfDestroyDelay()	{
		//Затримка перед самознищенням бонусу
		for (float timer = 4; timer >= 0; timer -= Time.deltaTime)
			yield return 0;
		SelfDestroy();
	}
}
