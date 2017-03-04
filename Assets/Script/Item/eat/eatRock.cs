using UnityEngine;
using System.Collections;

public class eatRock : eatScript {
		public override void Eat(){
				Debug.Log ("Eat Rock");
				PlayerData.RemoveItem(item.id);
		}
	
}
