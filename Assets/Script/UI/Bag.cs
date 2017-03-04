using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    //每行显示rownum个物体
    int rownum = 4;

    //放置到content下
    public GameObject bagContent;
    RectTransform rt;

    ///放置区域为Scrollv，动态适配没做
    //public GameObject Scrollv;
    //float Scrollv_width;
    //float Scrollv_height;

    //预制的道具展示方式，动态适配没做
    public GameObject bagItem;
    //float bagItem_width;
    //float bagItem_height;

    // Use this for initialization
    void Start()
    {
        rt = bagContent.GetComponent<RectTransform>();
        //bagItem_width = bagItem.GetComponent<RectTransform> ().rect.width;
        //bagItem_height = bagItem.GetComponent<RectTransform> ().rect.height;
        //Scrollv_width = Scrollv.GetComponent<RectTransform> ().rect.width;
        //Scrollv_height = Scrollv.GetComponent<RectTransform> ().rect.height;
        //Debug.Log (Scrollv_width);
        //Debug.Log (Scrollv_height);
    }
	
    // Update is called once per frame
    void Update()
    {
        int itemnum = PlayerData.Items.Count;
        //Debug.Log ("itemnum:"+itemnum);
        //背包中有物品
        if (itemnum != 0 && PlayerData.PlayerBagItemDirty)
        {
            //先清空背包再刷新
            ClearBag();
            //计算出物品行数
            int row = Mathf.CeilToInt((float)itemnum / (float)rownum);
            //Debug.Log ("row:"+row);

            //TODO  根据行数，改变背包内容展示高度,上下左右都按50的距离来算
            int h = 100 * row + 50;
            rt.sizeDelta = new Vector2(0, h);

            //添加物体
            for (int i = 0; i < row; i++)
            {
                //一行
                for (int j = 0; j < rownum; j++)
                {
                    if (j + j * i < itemnum)
                    {//总的没有越界
                        GameObject g = (GameObject)Instantiate(bagItem);
                        g.transform.SetParent(bagContent.transform);
                        int id = PlayerData.Items[j + j * i].id;

                        g.name = GameData.AllItems[id].name;
                        g.transform.GetChild(0).GetComponent<Image>().sprite = GameData.AllItems[id].sprite;
                        g.transform.GetChild(1).GetComponent<Text>().text = PlayerData.Items[j + j * i].num.ToString();
                        g.transform.GetChild(2).GetComponent<Text>().text = GameData.AllItems[id].name;

                        //5.0后要先获取type,不然会变成像下面这样
                        //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Script/UI/Bag.cs (62,13)", GameData.ItemScriptSpr[id]);
                        //System.Type mytype = System.Type.GetType (GameData.makeItemScriptSpr [id]);

                        eatScript eatBT = (eatScript)g.transform.GetChild(3).gameObject.AddComponent(GameData.AllItems[id].eatScript);//给按钮添加脚本
                        eatBT.item = PlayerData.Items[j + j * i];

                        //TODO 应该按宽度来安排位置，但是外面固定450宽的话，这里分4份，间隔都是50
                        g.transform.localPosition = new Vector2(50 + j * 100, -50 - i * 100);
                    }
                }

            }


            //标记背包已经刷新，下次update不要调用
            PlayerData.PlayerBagItemDirty = false;
        }
        else if (itemnum == 0)
        {//没有道具了
            //清空背包再刷新
            ClearBag();
            PlayerData.PlayerBagItemDirty = false;
        } 


    }

    void ClearBag()
    {
        for (int i = 0; i < bagContent.transform.childCount; i++)
        {
            Destroy(bagContent.transform.GetChild(i).gameObject);
        }

    }



}
