using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Make : MonoBehaviour
{
    //每行显示rownum个物体
    //int rownum = 1;

    //放置到content下
    public GameObject makeContent;
    RectTransform rt;

    ///放置区域为Scrollv，动态适配没做
    //public GameObject Scrollv;
    //float Scrollv_width;
    //float Scrollv_height;

    //预制的道具展示方式，动态适配没做
    public GameObject makeItem;
    public GameObject materialItem;
    //float bagItem_width;
    //float bagItem_height;

    //面板生成的tip队列，关闭面板清空
    //public static List<GameObject> Tips;


    // Use this for initialization
    void Start()
    {
        rt = makeContent.GetComponent<RectTransform>();
        //Tips = new List<GameObject> ();
    }
	
    // Update is called once per frame
    void Update()
    {
        int[] makeitem = GetMakeItem();
        int itemnum = makeitem.Length;
        //Debug.Log ("itemnum:"+itemnum);
        //背包中有物品
        if (itemnum != 0 && GameData.PlayerMakeItemsDirty)
        {
            //先清空再刷新
            ClearMake();
            //计算出物品行数
            int row = itemnum;
            //Debug.Log ("row:"+row);

            //TODO  根据行数，改变背包内容展示高度,上下左右都按50的距离来算
            int h = 120 * row + 60;
            rt.sizeDelta = new Vector2(0, h);

            //添加物体
            for (int i = 0; i < row; i++)
            {
                GameObject g = (GameObject)Instantiate(makeItem);
                g.transform.SetParent(makeContent.transform);
                int id = makeitem[i];

                g.name = GameData.AllItems[id].name + "制造";
                g.transform.GetChild(0).name = GameData.AllItems[id].name;
                g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameData.AllItems[id].sprite;
                g.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
                g.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = GameData.AllItems[id].name;
                g.transform.GetChild(0).GetChild(3).GetComponent<MakeThings>().thingid = id;

                //材料显示
                Transform materialPanel = g.transform.GetChild(2);
                for (int k = 0; k < GameData.AllItems[id].material.Length / 2; k++)
                {
                    int materialid = GameData.AllItems[id].material[k, 0];
                    GameObject m = (GameObject)Instantiate(materialItem);
                    m.transform.SetParent(materialPanel);

                    m.name = GameData.AllItems[materialid].name;
                    m.transform.GetChild(0).name = GameData.AllItems[materialid].name;
                    m.transform.GetChild(0).GetComponent<Image>().sprite = GameData.AllItems[materialid].sprite;
                    m.transform.GetChild(1).GetComponent<Text>().text = GameData.AllItems[id].material[k, 1].ToString();
                    g.transform.GetChild(0).GetChild(3).GetComponent<MakeThings>().material = GameData.AllItems[id].material;
                    m.transform.localPosition = new Vector2(35 + (35 + 15) * k - 140, -35 + 36.25f);



                    //检查当前拥有的道具数量是否足够，不够置灰
                    if (!hasMaterial(materialid, GameData.AllItems[id].material[k, 1]))
                    {
                        m.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                    }
                    else
                    {
                        m.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                    }
                    //检查当前拥有的道具数量是否全部足够，不够按钮置灰
                    if (!hasAllMaterial(id))
                    {
                        g.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = false;
                        g.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "材料不足";
                    }
                    else
                    {
                        g.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = true;
                        g.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "制作";

                    }
                    //tip加入队列
                    //Tips.Add(g.transform.GetChild (0).GetChild (4).gameObject.GetComponent<TipItem>().tipobj);

                }
                //5.0后要先获取type,不然会变成像下面这样
                /*
								System.Type mytype = System.Type.GetType (GameData.makeItemScriptSpr [id]);
								makeButton makeBT = (makeButton)g.transform.GetChild(0).GetChild (3).gameObject.AddComponent (mytype);//给按钮添加脚本
								makeBT.item = GameData.AllItems [id];*/

                //TODO 应该按宽度来安排位置，但是外面固定450宽的话，这里分4份，间隔都是50
                g.transform.localPosition = new Vector2(216, -60 - i * 120);

            }


            //标记背包已经刷新，下次update不要调用
            GameData.PlayerMakeItemsDirty = false;
        }
        else if (itemnum == 0)
        {//没有道具了
            //清空背包再刷新
            ClearMake();
            GameData.PlayerMakeItemsDirty = false;
        } 


    }

    public bool hasMaterial(int id, int num)
    {
        int itemnum = PlayerData.Items.Count;
        if (itemnum != 0)
        {
            for (int i = 0; i < itemnum; i++)
            {
                if (PlayerData.Items[i].id == id)
                {
                    if (PlayerData.Items[i].num >= num)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }


    public bool hasAllMaterial(int id)
    {
        int num = GameData.AllItems[id].material.Length / 2;
        for (int i = 0; i < num; i++)
        {
            if (!hasMaterial(GameData.AllItems[id].material[i, 0], GameData.AllItems[id].material[i, 1]))
            {
                return false;
            }
        }

        return true;
    }

    public int[] GetMakeItem()
    {
        int num = 0;
        int[] id;
        for (int i = 0; i < GameData.AllItems.Count; i++)
        {
            if (GameData.AllItems[i].canmake)
            {
                num++;
            }
        }
        id = new int[num];

        int j = 0;
        for (int i = 0; i < GameData.AllItems.Count; i++)
        {
            if (GameData.AllItems[i].canmake)
            {
                id[j] = GameData.AllItems[i].id;
                j++;
            }
        }

        return id;
    }


    void ClearMake()
    {
        for (int i = 0; i < makeContent.transform.childCount; i++)
        {
            Destroy(makeContent.transform.GetChild(i).gameObject);
        }

    }



}
