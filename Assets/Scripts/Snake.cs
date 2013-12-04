using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {
	public float speed = 0;
	private int num = 0;
	private int wp_number = 0;
	private int tl_number = 0;
	private bool horisontal = false;
	private bool turning = false;
	public Vector3 move_point = new Vector3(0.0f,5.0f,50.0f);
	private CharacterController mover;
	public Quaternion look_direction = Quaternion.Euler(0,0,0);
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		mover = GetComponent<CharacterController>();
		AddTale(transform);
		AddTale(GameObject.Find("snake_tail_1").transform);
		AddTale(GameObject.Find("snake_tail_2").transform);
	}

	void Update () {
		GameObject cam = GameObject.Find("Main Camera");
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		if(gs!=null){speed = gs.speed;}



		if (Input.GetKeyDown (KeyCode.Space)) {
			if(gs!=null){gs.ChangeCam();}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(gs!=null){gs.Pause();}
		}

		if(turning){
			if(Mathf.Abs(transform.position.x/10-Mathf.Round(transform.position.x/10))<0.05 && Mathf.Abs(transform.position.z/10-Mathf.Round(transform.position.z/10))<0.05){
				ChangeMoveDirection();
			}
		}
		else{
			if(gs.third_person_vision){
				//3-rd person camera and keyboard
				cam.transform.position = GameObject.Find ("snake_tail_3").transform.position;
				cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + 20, cam.transform.position.z);
				Quaternion rotate = Quaternion.Euler(30, transform.eulerAngles.y, 0);
				cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, rotate,1*Time.deltaTime);
				if (Input.GetKeyDown(KeyCode.LeftArrow)){
					look_direction = Quaternion.Euler(0,transform.eulerAngles.y-90,0); turning = true;
				}
				if (Input.GetKeyDown(KeyCode.RightArrow)){
					look_direction = Quaternion.Euler(0,transform.eulerAngles.y+90,0); turning = true;
				}
			}
			else{
				//UP Camera and keyboard
				cam.transform.position = new Vector3 (5, 190, 5);
				cam.transform.rotation = Quaternion.Euler (90, 0, 0);
				if(horisontal){
					if (Input.GetKeyDown(KeyCode.UpArrow)){
						look_direction = Quaternion.Euler(0,0,0); turning = true; 
					}
					if (Input.GetKeyDown(KeyCode.DownArrow)){
						look_direction = Quaternion.Euler(0,180,0); turning = true;
					}
					transform.position = new Vector3(transform.position.x,Mathf.Round(transform.position.y),Mathf.Round(transform.position.z));
				}
				else{
					if (Input.GetKeyDown(KeyCode.LeftArrow)){
						look_direction = Quaternion.Euler(0,-90,0); turning = true;
					}
					if (Input.GetKeyDown(KeyCode.RightArrow)){
						look_direction = Quaternion.Euler(0,90,0); turning = true;
					}
					transform.position = new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y),transform.position.z);
				}
			}
		}

		mover.Move(transform.forward*speed*Time.deltaTime);
	}

	void ChangeMoveDirection(){
		transform.rotation = look_direction;
		horisontal=!horisontal;
		turning = false;
		AddNewPoint();
		//Quaternion.Slerp(transform.rotation, look_direction,50*Time.deltaTime);
	}

	void AddNewPoint(){
		GameObject way_point = Instantiate(Resources.Load("Way_point")) as GameObject;
		way_point.transform.position = transform.position;
		Way_Point wp = way_point.AddComponent<Way_Point>();
		wp.index = ++wp_number;
		way_point.name = "way_point_"+wp.index;
	}

	void AddTale(Transform parent){
		tl_number++;
		GameObject _tail = Instantiate(Resources.Load("Snake_tail")) as GameObject;
		_tail.name = "snake_tail_"+tl_number;
		Tail tail = _tail.AddComponent<Tail>();
		tail.transform.position = parent.transform.position - parent.transform.forward * 10;
		tail.transform.rotation = parent.transform.rotation;
		tail.targetDistance = 10 * tl_number;
		tail.index = tl_number;
		if (tl_number > 1) {
			Tail a = parent.GetComponent<Tail> ();
			a.last_in_tale = false;
			tail.way_couter = a.way_couter;
			tail.last_in_tale = true;
		}
		else{
			Destroy(tail.collider);
		}

	}

	public void OnControllerColliderHit(ControllerColliderHit hit){
		Strawberry food = hit.collider.GetComponent<Strawberry>();
		if (food != null){
			food.Eat();
			AddTale(GameObject.Find("snake_tail_"+tl_number).transform);
		}
		Tail tail = hit.collider.GetComponent<Tail>();
		if (tail != null){EndofGame();}
		Wall wall = hit.collider.GetComponent<Wall>();
		if (wall != null){EndofGame();}
	}

	void EndofGame(){
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		if(gs!=null){gs.EndofGame();}
	}
}
