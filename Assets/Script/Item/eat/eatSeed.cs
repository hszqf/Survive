using UnityEngine;
using System.Collections;

public class eatSeed : eatScript {

		public override void Eat(){
				Debug.Log ("Eat Seed");
				PlayerData.Buff b = new PlayerData.Buff();
				b.id = item.id;
				b.num = 1;
				PlayerData.AddBuff(b,0);//物品的id和buffid一一对应
				PlayerData.RemoveItem(item.id);
		}

}
