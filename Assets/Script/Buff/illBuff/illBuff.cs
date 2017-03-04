using UnityEngine;
using System.Collections;


//buff基类，带定时功能，不设置就是永久buff
public class illBuff : MonoBehaviour
{
    public PlayerData.Buff buff;

    public void LifeCycle(float time)
    {
        //-1就是永久buff不会自行销毁
        if (time != -1f)
        {
            InvokeRepeating("Die", time, time);
        }
    }

    public void Die()
    {
        PlayerData.RemoveBuff(buff);

        //如果没了就销毁
        bool exists = false;
        for (int i = 0; i < PlayerData.Buffs.Count; i++)
        {
            if (PlayerData.Buffs[i].id == buff.id)
            {
                exists = true;
            }
        }

        if (!exists)
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }

}
