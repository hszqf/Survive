using UnityEngine;
using System.Collections;

public class DayNight : MonoBehaviour {
		public GameObject nightmask;
		int Day = 0;

		float OneDayTime = 240f;//每天的时间长度，秒

		float nowTime;//游戏内当天的时间

		float startTime = 50f;//游戏开始时间调整

		//游戏内时间段划分
		public static int Day_state = 0;//0天亮 1中午 2天黑 3午夜
		float[]  Day_state_time = {0.25f,0.5f,0.75f,1f};

		public void Awake(){
				GameData.Game_totaltime = startTime;
		}
	// Update is called once per frame
	void Update () {
				nowTime = GameData.Game_totaltime - Day * OneDayTime;
				//Debug.Log (nowTime);
				if (nowTime / OneDayTime < Day_state_time[0]) {//天亮
						Day_state = 0;
						nightmask.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 1 - Day_state_time.Length * nowTime / OneDayTime);
				} else if (nowTime / OneDayTime < Day_state_time[1]) {//中午
						Day_state = 1;
						nightmask.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
				} else if (nowTime / OneDayTime < Day_state_time[2]) {//天黑
						Day_state = 2;
						nightmask.GetComponent<SpriteRenderer>().color = new Color(0,0,0, Day_state_time.Length*(nowTime/OneDayTime-Day_state_time[1]));
				}else{//午夜
						Day_state = 3;
						nightmask.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 1);
				}

				if ( nowTime> OneDayTime) {
						NextDay ();
				}
	}


		void NextDay(){
				Day++;	
				//Debug.Log (Day);
		}
}
