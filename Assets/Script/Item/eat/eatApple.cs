using UnityEngine;
using System.Collections;

public class eatApple : eatScript {

		public override void Eat(){
				Debug.Log ("Eat Apple");
				PlayerData.P_hungery += 10;//需要放到PlayerData中一个方法，会受其他状态影响
				PlayerData.Buff b = new PlayerData.Buff();
				b.id = item.id;
				b.num = 1;
				PlayerData.AddBuff(b,0);//物品的id和buffid一一对应,0是物品buff
				PlayerData.RemoveItem(item.id);
		}
}
