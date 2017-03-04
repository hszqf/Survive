using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wetBuff : illBuff {

		float startdry_time = 20f;
		//float dry_time = 10f;

		int nownum = 0;
		int startnum = 0;

	// Use this for initialization
	void Start () {
			
				LifeCycle (-1f);//永久buff不会自行销毁
				InvokeRepeating("dry",0,1f);
	}


		//x秒没有增加湿度，就开始销毁
		public void Update(){
				nownum = buff.num;
				if (nownum > startnum) {
						startnum = nownum;
						wet ();//湿润增加了重新计算干燥时间
				}else{//干燥期间nownum < startnum
						startnum = nownum;
				}

				if (startdry_time == 0) {
						startdry_time = 20f;
						Die ();//消去一层
				}
		}

		void wet(){
				startdry_time = 20f;	
		}

		void dry(){
				startdry_time--;
				if (startdry_time < 0) {
						startdry_time = 0;
				}
		}

		void OnDistory(){
				
		}


}
