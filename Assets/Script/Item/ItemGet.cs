using UnityEngine;
using System.Collections;

public class ItemGet : MonoBehaviour {
		/*Item层次说明，
		Item：物品掉落的表现层，只与地面和其他物品碰撞，实现地上堆积的效果。
		ActItem：触发层，只和玩家检测碰撞，用来触发拾取功能。
		物品的prefab外层为Item，添加缸体和碰撞区。
		内层为ActItem，添加触发区，添加本脚本。
		*/
		public int id = -1;
		public KeyCode actKey = KeyCode.E;//默认互动按e，可更改，但是注意提示tip需要一致

		//碰到玩家后触发收取的动画
		bool fly = false;
		float m_speed = 1f;
		float scalevalue = 0.005f;

		Transform target;

	
	// Update is called once per frame
	void Update () {
				if(fly){
						if (Vector3.Distance(transform.parent.localPosition, target.position) <= 0.1f)
						{
								PlayerData.AddItem (id);
								Destroy (this.transform.parent.gameObject);
						}
						else
						{
								transform.parent.position = Vector3.MoveTowards(transform.parent.localPosition, target.localPosition, m_speed * Time.deltaTime);
								transform.parent.localScale= new Vector3(1-scalevalue, 1-scalevalue,1f);
								if (scalevalue < 1) {
										scalevalue += scalevalue;
								}
						}
				}
	}

		void OnTriggerStay2D(Collider2D col){
				if(!fly && col.gameObject.name == "Player" && Input.GetKey(actKey)){
						transform.parent.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
						transform.parent.GetComponent<Rigidbody2D> ().gravityScale = 0;
						transform.parent.GetComponent<Collider2D> ().enabled = false;
						fly = true;
						target = col.transform;
				}
		}

		/*
		void OnCollisionEnter2D(Collision2D col){
				if(col.gameObject.name == "Player" && Input.GetKey(actKey)){
						GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
						GetComponent<Rigidbody2D> ().gravityScale = 0;
						GetComponent<Collider2D> ().enabled = false;
						fly = true;
						target = col.transform;
				}
		}
*/
}
