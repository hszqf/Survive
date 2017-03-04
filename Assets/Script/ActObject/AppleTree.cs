using UnityEngine;
using System.Collections;

public class AppleTree : ActObject {
		//循环计数
		public int hittime = 0;
		int hitHP = 3;
		int hitturn = 0;

		int alllife;

		void Start(){
				//isnomaldie = false;//特殊死亡逻辑，自己来实现
				//isdiedrop = false;//死亡的时候不额外掉落东西,掉落物都自己处理了
				//life = 60;
				alllife = life;
				//初始化可掉落物品,池概率，会从中直接抽取一个id
				//carryItem = new int[]{0,0,1,1,1,1};//TODO 应该掉毛才对
				//-1为不会生病
				//carryIll = new int[]{-1};
		}

		public override void AI(){
				hittime = ((alllife - life)/PlayerData.Att)/(hitturn+1);
				if (hittime == hitHP) {
						hitturn++;
						DropItem();
						hittime = 0;
				}
				if (life == 0) {
						Die ();
				}

		}


}
