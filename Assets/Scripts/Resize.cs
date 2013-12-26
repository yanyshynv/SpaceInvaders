using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	private float time = 1;
	private float x;
	private float z;

	public GameObject mm;

	void Start(){
		//Початкові розміри
		x = transform.localScale.x;
		z = transform.localScale.z;
		NGUITools.SetActive (mm, false);
	}
	void Update(){
		//Зміна розмірів
		if (time < 10){
			transform.localScale=new Vector3(x/time,1,z/time);
			time += 2 * Time.deltaTime;
			if (time >= 10){
				NGUITools.SetActive (mm, true);
			}
		}
	}
}
