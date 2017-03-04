using UnityEngine;
using System.Collections;

public class MakeBT : MonoBehaviour {
		public GameObject MakePanel;

		public void click () {
				if (MakePanel.activeSelf) {
						MakePanel.SetActive (false);
				} else {
						MakePanel.SetActive (true);
				}
		}
}
