using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	
	public int speed = 40;
	public int power = 10;
	public AudioClip boom1;
	public AudioClip boom2;
	public AudioClip empire;
	public string enemy_tag = "Player";
	public Vector3 player_point;
	void Update () {
		transform.position=Vector3.MoveTowards (transform.position, player_point, speed * Time.deltaTime);
		StartCoroutine(SelfDestroy());
		Vector3 direction = player_point - transform.position;
		float distance = direction.magnitude;
		if (distance < 0.5f) {
			GameObject bang = Instantiate(Resources.Load("Boom")) as GameObject;
			bang.transform.position = transform.position;
			Destroy(gameObject);
		}
	}
	
	IEnumerator SelfDestroy()	{
		for (float timer = 4; timer >= 0; timer -= Time.deltaTime)
			yield return 0;
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider target){
		if (target.gameObject.tag == enemy_tag) {
			giveDamage(target.gameObject);
			Destroy (gameObject);
		}
	}
	
	void giveDamage(GameObject target){
		Player pl = target.gameObject.GetComponent<Player> ();
		if (pl != null) {
			GameSettings.health-=power;
			if(GameSettings.health<=0){
				GameSettings.health=0;
				GameSettings.u_r_dead=true;
				AudioSource.PlayClipAtPoint(boom2,transform.position);
				AudioSource.PlayClipAtPoint(empire,transform.position);
				Destroy (target.gameObject);
			}
			else{
				AudioSource.PlayClipAtPoint(boom1,transform.position);
			}
		}
		GameObject bang = Instantiate(Resources.Load("Boom"))as GameObject;
		bang.transform.position = new Vector3(transform.position.x,target.transform.position.y+5,(target.transform.position.z+transform.position.z)/2);
		Debug.Log("Destroy "+enemy_tag);
	}
}