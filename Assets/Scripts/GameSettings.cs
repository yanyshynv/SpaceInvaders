using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public static int points=0;							//Очки гравця
	public static int health=100;						//Життя гравця
	public static int enemies_number=0;					//Текуча кількість ворогів
	public static int max_enemies_number = 9;			//Максимальна кількість ворогів

	private float change_algoritm_frequency=15f;		//Частота зміну алгоритму руху груп ворогів
	public static float enemy_gen_frequency=10f;		//Частота генерації нового ворога

	public static GameObject main_menu;					//Обєкт меню "Пауза" у грі

	public static bool change_algoritm = false;			//Чи потрібна зміна алгоритму
	public static bool in_game=false;					//Чи гра розпочата
	public static bool in_pause=false;					//Чи гра призупинена
	public static bool u_r_dead=false;					//Чи гравець знищений

	public GUISkin skin;
	public GUISkin skin2;

	void Awake(){
		DontDestroyOnLoad (this);
	}

	void Start(){
		//Запуск таймеру зміни алгоритму руху груп ворогів
		StartCoroutine(ChangeAlgoritm());
	}
	
	void OnGUI(){
		if (in_game && !in_pause) {
			//Відображення графічного інтерфейсу в грі
			GUI.skin = skin2;
			GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.045f);
			GUI.Label (new Rect (Screen.width*0.05f, Screen.height*0.9f, Screen.width/4, Screen.height/10), "Health: " + health);

			GUI.skin = skin;
			GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.045f);
			GUI.Label (new Rect (Screen.width*0.05f, Screen.height*0.95f, Screen.width/4, Screen.height/10), "Points:  " + points);
			if (u_r_dead) {
				//Відображення графічного інтерфейсу коли гравець знищений
				GUI.skin = skin;
				GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.08f);
				GUI.Label (new Rect (Screen.width*0.1f, Screen.height*0.3f, Screen.width*0.8f, Screen.height/8), "Score: " + points);
				GUI.skin = skin2;
				GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.12f);
				GUI.Label (new Rect (Screen.width*0.1f, Screen.height*0.4f, Screen.width*0.8f, Screen.height/3), "Empire always wins");
			}
		}
	}

	void Update () {
		//Вхід/вихід в меню "Пауза"
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(in_pause){
				ButtResume();
			}
			else{
				NGUITools.SetActive(main_menu,true);
				GamePause();
			}
		}
	}

	IEnumerator ChangeAlgoritm()	{
		//Затримка зміни алгоритму груп ворогів
		while (true){
			for (float timer = 0; timer < change_algoritm_frequency; timer += Time.deltaTime)
				yield return 0;
			change_algoritm = true;
			change_algoritm_frequency = Random.Range(5f,10f);
			yield return 0;
			change_algoritm = false;
		}
	}

	public static void ChangeDifficulty(){
		//Зміна рівня важкості гри
		if(GameSettings.enemy_gen_frequency>2f){GameSettings.enemy_gen_frequency-=0.05f;}
		max_enemies_number++;
	}

	void GamePause(){
		//Пауза у грі
		in_pause=true;
		Screen.showCursor = in_pause;
		Time.timeScale=0;
	}

	void GameUnPause(){
		//Відновити гру
		in_pause=false;
		Screen.showCursor = in_pause;
		Time.timeScale=1;
	}

	void ButtResume(){
		//Продовжити гру
		if (in_pause) {
			GameUnPause();
			NGUITools.SetActive(main_menu,false);
		} 
	}

	void ButtNewGame(){
		//Розпочати нову гру
		if (in_game) {
			GameUnPause();
		} 
		Application.LoadLevel("Space");
		ReloadGameData();
	}

	void ButtExit(){
		//Вихід з гри
		if (in_game) {
			Application.LoadLevel("MainMenu");
			in_game=false;
			u_r_dead = false;
			GameUnPause();
		} 
		else {
			Application.Quit ();
		}
	}

	void ReloadGameData(){
		//Онулити дані гри
		points=0;
		health=100;
		enemies_number=0;
		enemy_gen_frequency=10f;
		max_enemies_number = 9;
		u_r_dead = false;
	}
}
