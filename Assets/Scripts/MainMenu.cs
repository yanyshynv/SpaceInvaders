using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	int H_space = Screen.width/20;
	int V_space = Screen.height/15;
	int BUT_width = Screen.width/5;
	int BUT_height = Screen.height/15;
	int FONT_Size = Screen.height/25;
	// Use this for initialization
	void Start () {
		Screen.showCursor = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		/*GUI.skin.button.fontSize = FONT_Size;
		GUI.skin.button.onActive.textColor = Color.red;
		if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4,BUT_width,BUT_height),"New Game")){
			Application.LoadLevel("Game");
			if(Application.isLoadingLevel){
				GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
				if(gs!=null){gs.in_game = true;}
			}
		}
		if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+BUT_height+V_space,BUT_width,BUT_height),"Load Game")){
			
		}
		if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+2*(BUT_height+V_space),BUT_width,BUT_height),"Settings")){
			
		}
		if(GUI.Button(new Rect(Screen.width/2-BUT_width/2,Screen.height/4+3*(BUT_height+V_space),BUT_width,BUT_height),"Exit")){
			Application.Quit();
		}*/
	}



}