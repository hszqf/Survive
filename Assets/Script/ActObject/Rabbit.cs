using UnityEngine;
using System.Collections;

public class Rabbit : ActObject {

	// Use this for initialization
	void Start () {
				//isnomaldie = true;//特殊死亡逻辑，自己来实现
				//isdiedrop = true;//死亡的时候掉落东西
				//life = 30;
				//type = AttedType.BACK;

				//初始化可掉落物品,池概率，会从中直接抽取一个id，-1为不会掉落的情况
				//carryItem = new int[]{3};//TODO 应该还会掉毛才对
				//-1为不会生病
				//carryIll = new int[]{-1,-1,-1,(int)GameData.illType.redeye};
	}



		//活着的时候每次update被调用的ai
		public override void AI(){

		}


}
