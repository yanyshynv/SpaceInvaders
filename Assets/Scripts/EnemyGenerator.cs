using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

	private int enemy_algoritm=1;			//Номер алгоритму групи ворогів

	private GameObject player;

	void Start(){
		GameSettings.in_game=true;
		player = GameObject.Find ("Player");
		//Приховання меню "Пауза"
		GameSettings.main_menu = GameObject.Find ("Main_panel");
		NGUITools.SetActive(GameSettings.main_menu,false);
		//Генерація групи винищувачів
		GenerateTIEFighter ();
		//Запуск таймерів генерації ворогів
		StartCoroutine(GenerateTIEFighterDelay());
		StartCoroutine(GenerateDroidBomberDelay());
	}

	IEnumerator GenerateTIEFighterDelay()	{
		//Затримка генерації групи винищувачів
		while (player!=null){
			for (float timer = 0; timer < GameSettings.enemy_gen_frequency; timer += Time.deltaTime)
				yield return 0;
			if(GameSettings.enemies_number<GameSettings.max_enemies_number){GenerateTIEFighter();}
		}
	}
	
	IEnumerator GenerateDroidBomberDelay()	{
		//Затримка генерації бомбардировщика
		while (player!=null){
			for (float timer = 0; timer < GameSettings.enemy_gen_frequency*1.6f; timer += Time.deltaTime)
				yield return 0;
			if(GameSettings.enemies_number<GameSettings.max_enemies_number){GenerateDroidBomber();}
		}
	}
	
	void GenerateTIEFighter(){
		//Генерація групи винищувачів
		Vector3 start_point = new Vector3(Random.Range(-9, 9)*10, 5, 160);
		Vector3 end_point= new Vector3(Random.Range(-8, 8)*10, 5, Random.Range(5, 13)*10);
		
		for (var i=1; i<=4; i++) {
			GameObject enemy = Instantiate(Resources.Load("TIEFighter"))as GameObject;
			TIEFighter en = enemy.GetComponent<TIEFighter> ();
			en.num_in_group = i;
			en.algoritm_num = enemy_algoritm;
			switch(en.num_in_group){
			case 1: enemy.transform.position=new Vector3(start_point.x+10, 5, start_point.z+10); break;
			case 2: enemy.transform.position=new Vector3(start_point.x+10, 5, start_point.z-10); break;
			case 3: enemy.transform.position=new Vector3(start_point.x-10, 5, start_point.z-10); break;
			case 4: enemy.transform.position=new Vector3(start_point.x-10, 5, start_point.z+10); break;
			default: enemy.transform.position=start_point; break;
			}
			en.start_point = end_point;
			GameSettings.enemies_number++;
		}
		
		enemy_algoritm=Mathf.RoundToInt(Random.Range(0.6f,3.4f));
	}
	
	void GenerateDroidBomber(){
		//Генерація бомбардировщика
		GameObject enemy = Instantiate(Resources.Load("DroidBomber"))as GameObject;
		GameSettings.enemies_number++;
	}
}
