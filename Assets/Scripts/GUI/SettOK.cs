using UnityEngine;
using System.Collections;

public class SettOK : MonoBehaviour {
	
	public GameObject _show;
	public GameObject _hide;
	void OnClick () {
		NGUITools.SetActive(_hide,false);
		NGUITools.SetActive(_show,true);
	}
}
