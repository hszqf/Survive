using UnityEngine;
using System.Collections;

public class eatBranch : eatScript {

		public override void Eat(){
				Debug.Log ("Eat Branch");
				PlayerData.P_hungery += 1;//需要放到PlayerData中一个方法，会受其他状态影响

				PlayerData.RemoveItem(item.id);
		}
}
