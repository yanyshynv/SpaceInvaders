using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {

	public int ship_health;					//Життя корабля
	public int ship_point;					//Очки при знищенні корабля
	public int ship_speed;					//Швидкість руху корабля
	public int fire_speed;					//швидкість польту пострілу

	public float fire_frequency;			//Частота пострілів корабля

	public bool in_position = false;		//Чи корабель на позиції
	public Vector3 start_point;				//Початкова точка місцезнаходження корабля
	public Vector3 move_point;				//Бажана точка місцезнаходження корабля

	public GameObject target;				//Обєкт ціль (ворог)

	public AudioClip fire;					//Звук пострілу корабля
	public AudioClip flying;				//Звук польоту корабля
	public AudioClip boom1;					//Звук пораження корабля
	public AudioClip boom2;					//Звук при знищенні корабля

	public int GiveDamageToShip(int h){
		//Нанесення пошкоджень кораблю
		ship_health-=h;

		if(ship_health<=0){
			DestroyShip();
		}else{
			AudioSource.PlayClipAtPoint(boom1,transform.position,0.6f);
		}
		return ship_health;
	}

	void DestroyShip(){
		//Знищення корабля
		ship_health=0;
		GameSettings.points+=ship_point;
		GameSettings.enemies_number--;
		GameSettings.ChangeDifficulty();
		AudioSource.PlayClipAtPoint(boom2,transform.position,0.8f);
		GenerateBonus ();
		Destroy (gameObject);
	}

	void GenerateBonus(){
		//Генерація бонуса при знищенні корабля
		float tmp = Random.Range (0, 10);
		if (tmp < 2) {
			GameObject bonus_tmp;
			if(tmp < 1 && GameSettings.health<100){
				bonus_tmp = Instantiate(Resources.Load("HealthBonus")) as GameObject;
			}else{
				bonus_tmp = Instantiate(Resources.Load("PointBonus")) as GameObject;
			}
			bonus_tmp.transform.position = transform.position;	
		}
	}

	public float MovingToMovePoint(){
		//Переміщення до бажаної точки місцезнаходження
		transform.position = Vector3.MoveTowards (transform.position, move_point, ship_speed * Time.deltaTime);
		Vector3 direction = move_point - transform.position;
		float distance = direction.magnitude;
		return distance;
	}

	public void FireLaser(){
		//Постріл лазером
		if(target!=null){
			GameObject laser_tmp = Instantiate(Resources.Load("Laser")) as GameObject;
			laser_tmp.transform.position = transform.position;
			Laser lt = laser_tmp.GetComponent<Laser>();
			lt.target_tag = target.tag;
			lt.bullet_speed =-fire_speed;
			AudioSource.PlayClipAtPoint(fire,transform.position,0.6f);
		}
	}

	public void FireBomb(){
		//Постріл бомбою
		if(target!=null){
			GameObject bomb_tmp = Instantiate(Resources.Load("Bomb")) as GameObject;
			bomb_tmp.transform.position = transform.position;
			Bomb bt = bomb_tmp.GetComponent<Bomb>();
			bt.target_tag = target.tag;
			bt.bullet_speed = fire_speed;
			bt.target = target.transform.position;
			AudioSource.PlayClipAtPoint(fire,transform.position,0.6f);
		}
	}
}
