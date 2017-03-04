using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    //预制的动画切换
    public GameObject playernormal;
    public GameObject playerdie;
    //人物自己
    public static GameObject Player;
    //攻击判定框
    public static GameObject attCollider;

    ///玩家状态结算间隔
    float CyclyTime = 5f;
//20秒
    //玩家的饱食度
    public static float Maxhungery = 100f;
    public static float P_hungery = Maxhungery;
    public  float hungery = 1;
//每CyclyTime秒消耗
    //玩家的水分
    public static float Maxwater = 100f;
    public static float P_water = Maxwater;
    public  float water = 1;
//每CyclyTime秒消耗
    //玩家的精神
    public static float Maxsan = 100f;
    public static float P_san = Maxsan;
    public  float san = 1;
//每CyclyTime秒消耗
    //玩家的攻击力
    public static int Att = 10;


    ///TODO 改成2维数组 玩家身上的物品列表
    public static List<GameData.Item> Items;
    ///用来给背包ui刷新使用
    public static bool PlayerBagItemDirty = true;
//每次道具有改动，这个标记为true，用来给背包ui刷新使用。

    ///TODO 改成2维数组 玩家身上的buff列表
    public static List<Buff> Buffs;
    ///用来给buffui刷新使用
    public static bool PlayerBuffDirty = true;
//每次道具有改动，这个标记为true，用来给buffui刷新使用。

    public static bool canmove = false;

    void Start()
    {
				

        Items = new List<GameData.Item>();
        Buffs = new List<Buff>();
        Player = gameObject;
        attCollider = GameObject.Find("attCollider");

        InvokeRepeating("LifeCycly", CyclyTime, CyclyTime);
    }


    void LifeCycly()
    {
        //各项消耗
        P_hungery -= hungery;
        P_water -= water;
        P_san -= san;

        if (P_hungery == 0 || P_water == 0 || P_san == 0)
        {
            Die();
        }
    }

    static public void RemoveItem(int itemid)
    {
        bool done = false;
        for (int i = 0; i < Items.Count; i++)
        {
            //已经有相同的道具，只增加数量
            if (Items[i].id == itemid)
            {
                //Debug.Log (i);
                GameData.Item newitem = Items[i];
                newitem.num -= 1;//只能一个一个减
                if (newitem.num == 0)
                { //没了
                    Items.RemoveAt(i);
                    done = true;
                    i = Items.Count;//end
                }
                else
                {
                    //取出原来的元素，把新的放进去。。。有点麻烦
                    Items.RemoveAt(i);
                    Items.Insert(i, newitem);
                    //Debug.Log ("id:"+Items[i].id + "  num:"+Items[i].num );
                    done = true;
                    i = Items.Count;//end
                }
            } 
        }

        if (!done)
        {
            Debug.Log("消耗物品的地方出错了");
        }

        PlayerBagItemDirty = true;
        GameData.PlayerMakeItemsDirty = true;
    }



    static public void AddItem(int itemid)
    {
        bool done = false;

        for (int i = 0; i < Items.Count; i++)
        {
            //已经有相同的道具，只增加数量
            if (Items[i].id == itemid)
            {
                GameData.Item newitem = Items[i];
                newitem.num += 1;//数量加一
                //取出原来的元素，把新的放进去。。。有点麻烦
                Items.RemoveAt(i);
                Items.Insert(i, newitem);

                //Debug.Log ("id:"+Items[i].id + "  num:"+Items[i].num );
                done = true;
                i = Items.Count;//end
            } 
        }

        if (!done)
        {
            Items.Add(GameData.AllItems[itemid]);
            //Debug.Log ("id:"+item.id + "  num:"+item.num );
        }

        PlayerBagItemDirty = true;
        GameData.PlayerMakeItemsDirty = true;
    }

    static public void RemoveBuff(Buff buff)
    {
        bool done = false;
        for (int i = 0; i < Buffs.Count; i++)
        {
            //已经有相同的道具，只增加数量
            if (Buffs[i].id == buff.id)
            {
                //Debug.Log (i);
                Buff old = Buffs[i];
                old.num -= 1;//只能一个一个吃
                if (old.num == 0)
                { //没了
                    Buffs.RemoveAt(i);
                    done = true;
                    i = Buffs.Count;//end
                }
                else
                {
                    //取出原来的元素，把新的放进去。。。有点麻烦
                    Buffs.RemoveAt(i);
                    Buffs.Insert(i, old);
                    Debug.Log("id:" + Buffs[i].id + "  num:" + Buffs[i].num);
                    done = true;
                    i = Buffs.Count;//end
                }
            } 
        }

        if (!done)
        {
            Debug.Log("消除buff的地方出错了");
        }

        PlayerBuffDirty = true;
    }


    //type是buff类型，0是道具buff，1是疾病buff,疾病buff从1000开始
    static public void AddBuff(Buff buff, int type)
    {
        bool done = false;
        if (type == 1 && buff.id < 1000)
        {
            Debug.Log("有疾病的id不对，疾病id从1000开始");
        }
        for (int i = 0; i < Buffs.Count; i++)
        {
            //已经有相同的道具，只增加数量
            if (Buffs[i].id == buff.id)
            {
                Buff old = Buffs[i];
                old.num += buff.num;
                //取出原来的元素，把新的放进去。。。有点麻烦
                Buffs.RemoveAt(i);
                Buffs.Insert(i, old);

                //Debug.Log ("id:"+Items[i].id + "  num:"+Items[i].num );
                done = true;
                i = Buffs.Count;//end
            } 
        }

        if (!done)
        {
            Buffs.Add(buff);
            //Debug.Log ("id:"+item.id + "  num:"+item.num );
        }

        PlayerBuffDirty = true;

    }

    public static Vector2 GetAttDirection()
    {
        return  attCollider.transform.position - Player.transform.position;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //和玩家碰撞逻辑
    }

    void Die()
    {
        //死亡
        CancelInvoke();
        playernormal.SetActive(false);
        playerdie.SetActive(true);
        canmove = false;
    }



    public struct Buff
    {
        public int id;
        public int num;
        //public int time = -1;
    }

}
