using UnityEngine;
using System.Collections;

public class HealthBonus : Bonus {

	public void TakeBonus(){
		//Отримання бонусу
		GameSettings.health += bonus;
		if (GameSettings.health > 100) {GameSettings.health=100;}
		AudioSource.PlayClipAtPoint(taking,transform.position);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider target_tmp){
		//Реакція бонусу на зіткнення
		if (target_tmp.gameObject.tag == "Player"){
			TakeBonus();
		}
	}
}
