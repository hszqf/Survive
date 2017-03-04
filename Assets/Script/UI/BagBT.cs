using UnityEngine;
using System.Collections;

public class BagBT : MonoBehaviour {
		public GameObject BagPanel;

		public void click () {
				if (BagPanel.activeSelf) {
						BagPanel.SetActive (false);
				} else {
						BagPanel.SetActive (true);
				}
		}

}
