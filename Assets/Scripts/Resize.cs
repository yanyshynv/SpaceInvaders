using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {
	float time = 1;
	float x;
	float z;
	public GameObject mm;
	void Start(){
		x = transform.localScale.x;
		z = transform.localScale.z;
		NGUITools.SetActive (mm, false);
	}
	void Update(){
		if (time < 10){
			transform.localScale=new Vector3(x/time,1,z/time);
			time += 2 * Time.deltaTime;
			if (time >= 10){
				NGUITools.SetActive (mm, true);
			}
		}
	}
}
