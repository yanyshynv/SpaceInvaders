using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {
	private float ch_frequency=15f;
	public static int points=0;
	public static int health=100;
	public static int enemies=0;
	public static float enemy_gen_frequency=10f;
	public static bool change_algoritm = false;
	public static GameObject main_menu;
	public static bool in_game=false;
	public static bool in_pause=false;
	public static bool u_r_dead=false;
	public GUISkin skin;
	public GUISkin skin2;

	void Awake(){
		DontDestroyOnLoad (this);
	}

	void Start(){
		StartCoroutine(ChangeAlgoritm());

	}
	
	void OnGUI(){
		if (in_game && !in_pause) {
			GUI.skin = skin2;
			GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.045f);
			GUI.Label (new Rect (Screen.width*0.05f, Screen.height*0.9f, Screen.width/4, Screen.height/10), "Health: " + health);

			GUI.skin = skin;
			GUI.skin.label.fontSize = Mathf.RoundToInt(Screen.height*0.045f);
			GUI.Label (new Rect (Screen.width*0.05f, Screen.height*0.95f, Screen.width/4, Screen.height/10), "Points:  " + points);
			if (u_r_dead) {
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
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(in_pause){
				ButtResume();
			}
			else{
				in_pause=true;
				NGUITools.SetActive(main_menu,true);
				Time.timeScale=0;

			}
		}

		if (in_game) {
			Screen.showCursor = in_pause;
		}
	}

	IEnumerator ChangeAlgoritm()	{
		while (true){
			for (float timer = 0; timer < ch_frequency; timer += Time.deltaTime)
				yield return 0;
			change_algoritm = true;
			ch_frequency=Random.Range(5f,10f);
			yield return 0;
			change_algoritm = false;
		}
	}
	void ButtResume(){
		if (in_pause) {
			in_pause=false;
			Time.timeScale=1;
			NGUITools.SetActive(main_menu,false);
		} 
	}
	void ButtNewGame(){
		if (in_game) {
			in_pause=false;
			Time.timeScale=1;
		} 
		else {
			in_game=true;
		}
		Application.LoadLevel("Space");
		points=0;
		health=100;
		enemies=0;
		enemy_gen_frequency=10f;
		u_r_dead = false;
	}

	void ButtExit(){
		if (in_game) {
			Application.LoadLevel("MainMenu");
			in_game=false;
			in_pause=false;
			u_r_dead = false;
			Time.timeScale=1;
		} 
		else {
			Application.Quit ();
		}
	}
}
