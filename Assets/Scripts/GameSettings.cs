using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public GameObject _setting_menu;
	public GameObject _main_menu;
	public GameObject _slice;
	public GameObject _label_3;
	public GameObject _check_box_1;
	public GameObject _check_box_2;

	public bool in_game = false;
	public bool in_pause = false;
	public bool in_end = false;

	public bool third_person_vision = true;
	public int points=0;
	public float speed=20;
	public int hardness=1;

	int H_space = Screen.width/20;
	int V_space = Screen.height/15;
	int BUT_width = Screen.width/5;
	int BUT_height = Screen.height/15;
	int FONT_Size = Screen.height/25;

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () {
		Vector3 v = new Vector3 (5, 190, 5);
		Quaternion q = Quaternion.Euler (90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (in_game) {
			GUI.skin.label.fontSize = FONT_Size;
			GUI.Label (new Rect (H_space, V_space, BUT_width, BUT_height), "Points: " + points);
			GUI.Label (new Rect (H_space, V_space*2, BUT_width, BUT_height), "Speed: " + speed);
			if(in_pause){
				GUI.Box(new Rect(Screen.width/2-BUT_width,Screen.height/4,BUT_width*2,BUT_height*10),"");
				if(in_end){
					GUI.Label (new Rect(Screen.width/2-BUT_width/2,Screen.height/4+BUT_height+V_space,BUT_width,BUT_height),"Your score: "+points);
				}
				else{
					if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+BUT_height+V_space,BUT_width,BUT_height),"Resume")){
						Pause();
					}
				}
				if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+2*(BUT_height+V_space),BUT_width,BUT_height),"New Game")){
					Pause();
					NewGame();
				}
				if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+3*(BUT_height+V_space),BUT_width,BUT_height),"Exit")){
					Application.Quit();
				}
			}
		}

	}

	public void NewGame(){
		in_end = false;
		points=0;
		speed=20;
		Pause ();
		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale=1;

	}

	public void Pause(){
		if (!in_end) {
			in_pause = !in_pause;
			Screen.showCursor = in_pause;
			if (in_pause) {
				Time.timeScale=0;
				GameObject main_panel  = GameObject.Find("Main_panel");
				NGUITools.SetActive(main_panel,true);
			} 
			else {
				Time.timeScale=1;
				GameObject main_panel  = GameObject.Find("Main_panel");
				NGUITools.SetActive(main_panel,false);
			}	
		}
	}

	public void EndofGame(){
		Pause ();
		in_end = true;
	}

	public void ChangeCam(){
		third_person_vision = !third_person_vision;
		GameObject cam = GameObject.Find("Main Camera");
		if(third_person_vision){
			cam.transform.position = GameObject.Find ("snake_tail_3").transform.position;
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + 20, cam.transform.position.z);
			cam.transform.rotation = Quaternion.Euler(30, GameObject.Find ("Snake_head").transform.eulerAngles.y, 0);
		}
		else{
			cam.transform.position = new Vector3 (5, 190, 5);
			cam.transform.rotation = Quaternion.Euler (90, 0, 0);
		}
	}

	void ButtNewGame() {
		Application.LoadLevel("Game");
		in_game = true;
	}
	
	void ButtExit () {
		Application.Quit();
	}
	

	void ButtSettings() {
		NGUITools.SetActive(_main_menu,false);
		NGUITools.SetActive(_setting_menu,true);
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		UICheckbox chbox = GameObject.Find("Checkbox_3rd_person_cam").GetComponent<UICheckbox>();
		chbox.startsChecked = gs.third_person_vision;
		UICheckbox chbox2 = GameObject.Find("Checkbox_Up_cam").GetComponent<UICheckbox>();
		chbox2.startsChecked = !gs.third_person_vision;
	}
	void ButtCancel(){
		NGUITools.SetActive(_main_menu,true);
		NGUITools.SetActive(_setting_menu,false);

	}
	void ButtOk(){
		NGUITools.SetActive(_main_menu,true);
		NGUITools.SetActive(_setting_menu,false);
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		UICheckbox chbox = _check_box_2.GetComponent<UICheckbox>();
		gs.third_person_vision = chbox.isChecked;
		//UICheckbox hard = _slice.GetComponent<UISlider>();
		//gs.hardness = hard.
		//UICheckbox label = _label_3.GetComponent<UILabel>();
		//label. = "- Hard";
	}
}
