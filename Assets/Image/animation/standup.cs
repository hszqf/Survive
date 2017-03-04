using UnityEngine;
using System.Collections;

public class standup : MonoBehaviour {
		public GameObject g;

		void aniend(){
				g.GetComponent<SpriteRenderer> ().enabled = true;
				PlayerData.canmove = true;
				Destroy (gameObject);
		}
}
