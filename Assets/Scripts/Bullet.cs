using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public int bullet_speed;			//Швидкість польоту кулі
	public int bullet_power;			//Пошкодження, що нанесуться кулею
	public string target_tag;			//Мітка цілі для перевірки на ворожість
	public Vector3 target;				//Місце розташування цілі

	public float MoveToTarget () {
		//Переміщення до заданої цілі
		transform.position=Vector3.MoveTowards (transform.position, target, bullet_speed * Time.deltaTime);
		Vector3 direction = target - transform.position;
		float distance = direction.magnitude;
		return distance;
	}

	public void SelfDestroy(){
		//Самознищення
		Destroy(gameObject);
	}

	public IEnumerator SelfDestroyDelay()	{
		//Затримка до самознищення
		for (float timer = 4; timer >= 0; timer -= Time.deltaTime)
			yield return 0;
		SelfDestroy();
	}
	
	void OnTriggerEnter(Collider target_tmp){
		//Перевірка зіткнення
		if (target_tmp.gameObject.tag == target_tag){
			GiveDamage(target_tmp.gameObject);
			SelfDestroy();
		}
	}
	
	void GiveDamage(GameObject target_tmp){
		//Нанесення пошкодження цілі
		TIEFighter en = target_tmp.gameObject.GetComponent<TIEFighter> ();
		if (en != null) {
			en.GiveDamageToShip(bullet_power);
		}

		DroidBomber enb = target_tmp.gameObject.GetComponent<DroidBomber> ();
		if (enb != null) {
			enb.GiveDamageToShip(bullet_power);
		}

		Player pl = target_tmp.gameObject.GetComponent<Player> ();
		if (pl != null) {
			pl.GiveDamageToPlayer(bullet_power);
		}
		//Створення вибуху
		GameObject bang = Instantiate(Resources.Load("Boom"))as GameObject;
		bang.transform.position = new Vector3((target_tmp.transform.position.x+transform.position.x)/2,target_tmp.transform.position.y+5,(target_tmp.transform.position.z+transform.position.z)/2);
	}
}
