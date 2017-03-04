using UnityEngine;
using System.Collections;

public class FlipX : MonoBehaviour {
	public GameObject attcollider;
	private Transform _trans;

	// Use this for initialization
	void Start () {
		_trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
				if (PlayerData.canmove) {
						Vector2 direction = _trans.localScale;

						if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
								direction.x = -1;
								attcollider.transform.localPosition = new Vector3(-0.086f,-0.005f,0);
						} else if (Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.A)) {
								direction.x = 1;
								attcollider.transform.localPosition = new Vector3(0.086f-0.005f,0);
						}

						_trans.localScale = direction;
				}
	}
}
