using UnityEngine;
using System.Collections;

public class eatMeat : eatScript {
		public override void Eat(){
				Debug.Log ("Eat Meat");
				PlayerData.P_hungery += 20;//需要放到PlayerData中一个方法，会受其他状态影响
				PlayerData.RemoveItem(item.id);
		}
	
}
