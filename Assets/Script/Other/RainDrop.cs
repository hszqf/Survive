using UnityEngine;
using System.Collections;

public class RainDrop : MonoBehaviour {
		//float dietime = 3f;

		float splashspeed = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				
	}


		void OnCollisionEnter2D(Collision2D col){

						Splash ();
						Splash ();
						Splash ();

				//湿度增加
				if(col.gameObject.name == "Player"){
						PlayerData.Buff ill = new PlayerData.Buff();
						ill.id = (int)GameData.illType.cold;
						ill.num = 1;
						PlayerData.AddBuff(ill,1);
				}
						die ();
				//}

				//Invoke ("die", dietime);
		}

		void die(){



				Destroy (gameObject);	
		}

		void Splash(){//去掉第一个后第二个变成第一个，所以用一个方法就行
				Transform littleraindrop = transform.GetChild (0);//不先去取出来，不是自己的子对象后就找不到了

				littleraindrop.parent = null;
				littleraindrop.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				littleraindrop.gameObject.GetComponent<Rigidbody2D> ().velocity = getSplashForce();
				//littleraindrop.gameObject.name = "velocity:" + transform.GetChild (0).gameObject.GetComponent<Rigidbody2D> ().velocity.ToString ();
		}

		Vector2 getSplashForce(){
				float x = Random.Range (-splashspeed, splashspeed);
				float y = Random.Range ( splashspeed, 1.5f*splashspeed);
				Vector2 v = new Vector2 (x,y);

				return v;
		}


}
