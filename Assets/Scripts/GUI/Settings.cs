using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	public GameObject _show;
	public GameObject _hide;
	void OnClick () {
		NGUITools.SetActive(_hide,false);
		NGUITools.SetActive(_show,true);
		GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings> ();
		UICheckbox chbox = GameObject.Find("Checkbox_3rd_person_cam").GetComponent<UICheckbox>();
		chbox.startsChecked = gs.third_person_vision;
		UICheckbox chbox2 = GameObject.Find("Checkbox_Up_cam").GetComponent<UICheckbox>();
		chbox2.startsChecked = !gs.third_person_vision;
	}
}
