using UnityEngine;
using System.Collections;

public class littleraindrop : MonoBehaviour {
		float lifetime = 1f;

		float alph = 255f;
	// Use this for initialization
	void Start () {
				Invoke ("die",lifetime);
	}
	
	// Update is called once per frame
	void Update () {
				
				GetComponent<SpriteRenderer> ().color = new Color (0,0,0,alph/255f);
				alph -= 3f;
				if (alph < 0) {
						alph = 0;
				}
				//Debug.Log (GetComponent<SpriteRenderer> ().color.a);
	}

		void die(){
				Destroy (gameObject);
		}



}
