using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour {
	public Transform target;
	public float speed = 0;
	public float targetDistance;
	public int way_couter = 1;
	public int index = 0;
	public bool last_in_tale = true;

	void Start () {

	}

	void Update () {
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		find_target();
		Vector3 direction = target.position - transform.position;
		float distance = direction.magnitude;
		if(target.tag=="Snake"){
			if(gs!=null){speed = gs.speed;}
			if (distance > targetDistance){
				transform.position += direction.normalized * (distance - targetDistance);
				transform.LookAt(target);
			}
		}
		if (target.tag=="Way_point"){
			if(index>1){
				Vector3 direction2 = GameObject.Find("snake_tail_"+(index-1)).transform.position - transform.position;
				float distance2 = direction2.magnitude;
				if(distance2 > (12f)){if(gs!=null){speed = gs.speed*1.5f;}}
				else{if(gs!=null){speed = gs.speed;}}
			}
			else{if(gs!=null){speed = gs.speed;}}
			if (distance > 0.8f){
				transform.position += direction.normalized * (Time.deltaTime*speed);
				transform.LookAt(target);
			}
			else{
				if(last_in_tale){
					Destroy(GameObject.Find("way_point_"+(way_couter)));
				}
				way_couter++;
			}
		}
	}

	void find_target(){
		var tmp_target = GameObject.Find("way_point_"+way_couter);
		if(tmp_target!=null){
			target=tmp_target.transform;
		}
		else{
			target = GameObject.Find("Snake_head").transform;
		}
	}
}
