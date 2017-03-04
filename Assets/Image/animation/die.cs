using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {
		void aniend(){
				Debug.Log ("die");
				GetComponent<Animator> ().Stop ();
		}
}
