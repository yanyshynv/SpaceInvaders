using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform Camera;
	public float gen_frequency;
	private int enemy_algoritm=1;
	public AudioClip fire;

	void Start(){
		GameSettings.in_game=true;
		GameSettings.main_menu = GameObject.Find ("Main_panel");
		NGUITools.SetActive(GameSettings.main_menu,false);
		GenerateEnemy ();
		StartCoroutine(GenEnemy());
	}

	void Update () {
		gen_frequency = GameSettings.enemy_gen_frequency;
		if (!GameSettings.in_pause) {
			moveToMousePosition ();
		}
		if (Input.GetButtonDown ("Fire1")) {
			GameObject bullet = Instantiate(Resources.Load("LaserRed"))as GameObject;
			bullet.transform.position = transform.position;
			AudioSource.PlayClipAtPoint(fire,transform.position);
		}
	}

	void moveToMousePosition(){
		if (Input.mousePosition.x/Screen.width != (transform.position.x+95)/190) {
			transform.position=new Vector3 (Input.mousePosition.x*190/Screen.width-95,5,0);
			Camera.position=new Vector3 (Input.mousePosition.x*18/Screen.width-9,130,65);
		}
	}

	IEnumerator GenEnemy()	{
		while (true){
			for (float timer = 0; timer < gen_frequency; timer += Time.deltaTime)
				yield return 0;
			GenerateEnemy();
		}
	}

	void GenerateEnemy(){
		Vector3 start_point = new Vector3(Random.Range(-9, 9)*10, 5, 160);
		Vector3 end_point= new Vector3(Random.Range(-8, 8)*10, 5, Random.Range(5, 13)*10);

		for (var i=1; i<=4; i++) {
			GameObject enemy = Instantiate(Resources.Load("Enemy"))as GameObject;
			Enemy en = enemy.GetComponent<Enemy> ();
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
			GameSettings.enemies++;
		}

		enemy_algoritm=Mathf.RoundToInt(Random.Range(0.6f,3.4f));
	}
}