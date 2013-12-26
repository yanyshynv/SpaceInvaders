using UnityEngine;
using System.Collections;

public class PointBonus : Bonus {
	
	public void TakeBonus(){
		//Отримання бонусу
		GameSettings.points += bonus;
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
