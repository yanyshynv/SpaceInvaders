using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public int speed = 100;
	public int power = 10;
	public AudioClip boom1;
	public AudioClip boom2;
	public AudioClip empire;
	public string enemy_tag = "Enemy";
	void Update () {
		transform.position=new Vector3 (transform.position.x,transform.position.y,transform.position.z+speed*Time.deltaTime);
		StartCoroutine(SelfDestroy());
	}

	IEnumerator SelfDestroy()	{
		for (float timer = 3; timer >= 0; timer -= Time.deltaTime)
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
		Enemy en = target.gameObject.GetComponent<Enemy> ();
		if (en != null) {
			en.health-=power;
			if(en.health<=0){
				AudioSource.PlayClipAtPoint(boom2,transform.position);
				Destroy (target.gameObject);
				GameSettings.health++;
				GameSettings.enemies--;
				if(GameSettings.enemy_gen_frequency>2f){GameSettings.enemy_gen_frequency-=0.1f;}
				GameSettings.points+=10;
				if(GameSettings.health>100){GameSettings.health=100;}
			}
			else{
				AudioSource.PlayClipAtPoint(boom1,transform.position);
			}
		}
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