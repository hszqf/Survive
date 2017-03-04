using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class makeButton : MonoBehaviour {
		Button makeBT;
		public GameData.Item item;
	// Use this for initialization
	void Start () {
				makeBT = GetComponent<Button> ();
				makeBT.onClick.AddListener(Make) ;
	}

		//各自实现各自的逻辑去
		public virtual void Make (){}


}
