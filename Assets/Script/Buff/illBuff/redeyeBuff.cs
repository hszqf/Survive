using UnityEngine;
using System.Collections;

public class redeyeBuff : illBuff {
		public GameObject redmask;

	// Use this for initialization
	void Start () {
				redmask = GameData.Mycamera.transform.Find("redmask").gameObject;
				Debug.Log (redmask.name);
				LifeCycle (-1f);//永久buff不会自行销毁
				redmask.SetActive(true);
	}
	
		void OnDistory(){
				redmask.SetActive(false);
		}


}
