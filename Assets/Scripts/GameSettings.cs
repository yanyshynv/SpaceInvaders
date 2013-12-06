using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {
	private float ch_frequency=15f;
	public static int points=0;
	public static int health=100;
	public static bool change_algoritm = false;

	void Start(){
		StartCoroutine(ChangeAlgoritm());
	}
	
	void OnGUI(){
		GUI.skin.label.fontSize = 20;
		GUI.Label (new Rect (50, 50, 150, 50), "Health=" + health);
		GUI.Label (new Rect (50, 70, 150, 50), "Points=" + points);
	}
	void Update () {

	}

	IEnumerator ChangeAlgoritm()	{
		while (true){
			for (float timer = 0; timer < ch_frequency; timer += Time.deltaTime)
				yield return 0;
			change_algoritm = true;
			ch_frequency=Random.Range(5f,20f);
			yield return 0;
			change_algoritm = false;
		}
	}

	void ButtNewGame(){Application.LoadLevel("Space");}
	void ButtExit(){Application.Quit ();}
}
