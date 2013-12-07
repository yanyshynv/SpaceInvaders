using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {
	public int max_size=20;
	public float time=2;
	private float speed=0;
	void Start(){
		speed=(max_size-1)/time;
		StartCoroutine(Countdown());
	}

	void Update () {
		if (transform.localScale.x < max_size) {
			transform.localScale += new Vector3 (speed * Time.deltaTime, 0f, speed * Time.deltaTime);
		}
	}

	IEnumerator Countdown()	{
		for (float timer = time; timer >= 0; timer -= Time.deltaTime) {
			yield return 0;
		}
		Destroy(gameObject);
	}
}
