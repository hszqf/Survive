using UnityEngine;
using System.Collections;

public class ActObject : MonoBehaviour {
		//生命值,需要初始化
		public int life;
		//是否通过生命为0简单判定死亡
		public bool isnomaldie = true;
		//是否死亡时掉落物品
		public bool isdiedrop = true;
		//可掉落物品
		public int[] carryItem;
		//可掉感染疾病
		public int[] carryIll;

		public AttedType attedtype = AttedType.SHAKE;//默认是抖动

		public enum AttedType{
				SHAKE,
				BACK
		}
		//===========抖动设置==========
		private float shakeTime = 0.2f;
		private float fps= 20.0f;
		private float frameTime =0.01f;
		private float shakeDelta =0.02f;
		private  bool isshake =false;
		private Vector3 old;
		//===========击退设置==========
		float hitbackpower = 6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (life <= 0 && isnomaldie) {
						Die ();
				} else {
						AI ();
				}

				if (isshake)
				{
						if (shakeTime > 0) {
								shakeTime -= Time.deltaTime;
								if (shakeTime <= 0 ) {
										transform.position = old;
										isshake = false;
										shakeTime = 0.2f;
										fps = 20.0f;
										frameTime = 0.01f;
										shakeDelta = 0.02f;
								} else {
										frameTime += Time.deltaTime;

										if (frameTime > 1.0 / fps) {
												frameTime = 0;
												transform.position += new Vector3 (shakeDelta * (-1.0f + 2.0f * Random.value), shakeDelta * (-1.0f + 2.0f * Random.value), 0f);
										}
								}
						}
				}


	}

		//活着的时候每次update被调用的ai
		public virtual void AI(){}


		public void Die(){
				Illcheck ();
				if (isdiedrop) {
						DropItem ();
				}
				Destroy (gameObject);
		}

		public void DropItem(){
				int rnd = Random.Range (0, carryItem.Length);//随机出掉落物
				if (carryItem [rnd] != -1) {
						GameData.CreateItem (carryItem [rnd], transform.position, transform.localRotation);
				}
		}

		public void Illcheck(){
				int rnd = Random.Range (0, carryIll.Length);//随机出疾病
				Debug.Log(carryIll.Length);
				if(carryIll[rnd] != -1){//不为-1就感染
						PlayerData.Buff ill = new PlayerData.Buff();
						ill.id = carryIll[rnd];
						ill.num = 1;
						PlayerData.AddBuff(ill,1);
				}
		}

		void OnTriggerEnter2D(Collider2D col){
				if (col.name == "attCollider") {
						life -= PlayerData.Att;
						switch ((int)attedtype) {
						case (int)ActObject.AttedType.SHAKE:
								//Debug.Log (gameObject.name);
								if (!isshake) {
										Shake ();
								}
								break;
						case (int)ActObject.AttedType.BACK:
								HitBack (PlayerData.GetAttDirection());
								break;
						default:
								break;
						}
				}
		}

		void Shake(){
				old = transform.position;
				isshake = true;
		}

		void HitBack(Vector2 t){
				GetComponent<Rigidbody2D> ().velocity += t*hitbackpower;
		}
}
