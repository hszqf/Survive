using UnityEngine;
using System.Collections;

public class eatAppleBuff : eatBuff {
		
		public void Start(){
				GameData.PlayerMoveSpeed = 0.5f;//吃饱了走不动
				LifeCycle (20f);
		}
		public void OnDestroy(){
				GameData.restPlayerMoveSpeed ();//复位
		}
}
