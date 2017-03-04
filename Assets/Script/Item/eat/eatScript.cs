using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class eatScript : MonoBehaviour {
		Button eatBT;
		public GameData.Item item;
	// Use this for initialization
	void Start () {
				eatBT = GetComponent<Button> ();
				eatBT.onClick.AddListener(Eat) ;
	}

		//各自实现各自的逻辑去
		public virtual void Eat (){}


}
