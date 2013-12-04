using UnityEngine;
using System.Collections;

public class Strawberry : MonoBehaviour {
	public int points = 10;

	void Update () {
		transform.Rotate(Vector3.up, 60 * Time.deltaTime);
	}

	public void Eat(){
		//Game.points += points;
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		if(gs!=null){
			gs.speed += 1;
			gs.points += points;
		}
		Destroy(gameObject);
		GenerateNewStrawberry ();
	}

	public static void GenerateNewStrawberry(){
		GameObject strawberry = Instantiate(Resources.Load("Strawberry"))as GameObject;
		while (true){
			strawberry.transform.position = new Vector3(Random.Range(-9, 10)*10, 5, Random.Range(-9, 10)*10);
			Bounds st_bounds = strawberry.collider.bounds;
			bool intersects = false;
			foreach(Collider objectColiider in FindObjectsOfType(typeof(Collider))){
				if (objectColiider != strawberry.collider){
					if (objectColiider.bounds.Intersects(st_bounds)){
						intersects = true;
						break;
					}
				}
			}
			if (!intersects){
				break;
			}
		}
	}
}