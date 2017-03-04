using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    //每行显示rownum个物体
    int rownum = 5;

    //预制的buff展示方式，动态适配没做
    public GameObject buffItem;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        int buffnum = PlayerData.Buffs.Count;
        //Debug.Log ("buffnum:"+buffnum);
        //有buff
        if (buffnum != 0 && PlayerData.PlayerBuffDirty)
        {
            //TODO  如果只是数量更改，不要整个清理，现在会导致tip闪烁.
            //TODO 先检测，有新buff加到最后就行，数字变就改数字。背包也可以这样减少消耗
            //先清空背包再刷新
            ClearBuff();
            //计算出buff行数
            int row = Mathf.CeilToInt((float)buffnum / (float)rownum);
            //Debug.Log ("row:"+row);

            //添加物体
            for (int i = 0; i < row; i++)
            {
                //一行
                for (int j = 0; j < rownum; j++)
                {
                    if (j + j * i < buffnum)
                    {//总的没有越界
                        GameObject g = (GameObject)Instantiate(buffItem);
                        g.transform.SetParent(transform);
                        int buffid = PlayerData.Buffs[j + j * i].id;
                        if (buffid >= 1000)
                        {//疾病buff
                            int realid = buffid - 1000;//疾病列表中的位置
                            Debug.Log(realid);
                            g.name = GameData.AllIlls[realid].name + "Buff";
                            g.transform.GetComponent<Image>().sprite = GameData.AllIlls[realid].sprite;

                            g.transform.GetChild(0).GetComponent<Text>().text = PlayerData.Buffs[j + j * i].num.ToString();

                            //buff图标的逻辑,物品的string脚本后面街上Buff
                            //System.Type mytype = System.Type.GetType (GameData.AllIlls [realid].illBuffScript);
                            illBuff ib = (illBuff)g.AddComponent(GameData.AllIlls[realid].illBuffScript);//添加脚本
                            ib.buff = PlayerData.Buffs[j + j * i];

                        }
                        else
                        {//itembuff
														
                            g.name = GameData.AllItems[buffid].name + "Buff";
                            g.transform.GetComponent<Image>().sprite = GameData.AllItems[buffid].sprite;

                            g.transform.GetChild(0).GetComponent<Text>().text = PlayerData.Buffs[j + j * i].num.ToString();


                            //TODO buff图标的逻辑,物品的string脚本后面街上Buff
                            //System.Type mytype = System.Type.GetType (GameData.eatItemScriptSpr [buffid] + "Buff");
                            eatBuff et = (eatBuff)g.AddComponent(GameData.AllItems[buffid].eatBuffScript);//添加脚本
                            et.buff = PlayerData.Buffs[j + j * i];

                        }

                        //TODO 这里有个奇怪的问题，所以手动减111加70.他没有按prefab的左上对齐而是对齐了parents的中心
                        g.transform.localPosition = new Vector2(23 + j * 39 - 111, -16 - i * 32 + 70);

                    }
                }

            }


            //标记背包已经刷新，下次update不要调用
            PlayerData.PlayerBuffDirty = false;

				
        }
        else if (buffnum == 0)
        {//没有buff了
            //清空buff再刷新
            ClearBuff();
            PlayerData.PlayerBuffDirty = false;
        } 

    }

    void ClearBuff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }




}
