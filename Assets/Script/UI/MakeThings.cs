using UnityEngine;
using System.Collections;

public class MakeThings : MonoBehaviour {
		public int thingid;
		public int[,] material;

		public void click () {
				PlayerData.AddItem (thingid);
				int materialnum = material.Length / 2;
				for (int i = 0; i < materialnum; i++) {
						int num = material [i, 1];
						//找出需要材料数量，扣掉
						for(int j =0;j<num;j++){
								PlayerData.RemoveItem (material [i, 0]);
						}
				}

		}
}
