using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

public class XMLManager
{
    public List<GameData.Item> Items;
    public List<GameData.Item> Ills;
    public List<GameData.ActItem> ActItems;

    ///初始化读取xml中的配置
    public void init(string path)
    {
        Items = new List<GameData.Item>();
        Ills = new List<GameData.Item>();
        ActItems = new List<GameData.ActItem>();

        LoadXml(path);
    }

    public List<GameData.Item> Get_Items()
    {
        return Items;
    }

    public List<GameData.Item> Get_Ills()
    {
        return Ills;
    }

    public List<GameData.ActItem> Get_ActItems()
    {
        return ActItems;
    }

    void LoadXml(string path)
    {
        //创建xml文档
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        xml.Load(XmlReader.Create((path), set));

        //得到Items节点下的所有子节点
        XmlNode node_Items = xml.GetElementsByTagName("Items")[0];
        XmlNodeList Items_xmlNodeList = node_Items.ChildNodes;

        XmlNode node_Ills = xml.GetElementsByTagName("Ills")[0];
        XmlNodeList Ill_xmlNodeList = node_Ills.ChildNodes;

        XmlNode node_ActItems = xml.GetElementsByTagName("ActItems")[0];
        XmlNodeList ActItems_xmlNodeList = node_ActItems.ChildNodes;

        //读取道具
        foreach (XmlElement xl1 in Items_xmlNodeList)
        {	
            GameData.Item tmpItem = new GameData.Item();
            tmpItem.id = int.Parse(xl1.GetAttribute("id"));
            tmpItem.name = xl1.GetAttribute("name");
            tmpItem.num = 1;

            string SpritePath = "Item/" + xl1.GetAttribute("image");
            //Debug.Log (SpritePath);
            Sprite s = Resources.Load<Sprite>(SpritePath);
            //if(s == null)Debug.Log ("null");
            tmpItem.sprite = s;

            System.Type mytype = System.Type.GetType(xl1.GetAttribute("eatScript"));
            tmpItem.eatScript = mytype;

            System.Type mytypebuff = System.Type.GetType(xl1.GetAttribute("eatScript") + "Buff");
            tmpItem.eatBuffScript = mytypebuff;

            if (xl1.GetAttribute("isweapon") == "true")
            {
                tmpItem.isweapon = true;
            }
            else
            {
                tmpItem.isweapon = false;
            }
            if (xl1.GetAttribute("canmake") == "true")
            {
                tmpItem.canmake = true;
                string tmp = xl1.GetAttribute("material");
                string[] tmpm = tmp.Split(';');

                tmpItem.material = new int[tmpm.Length, 2];
                for (int i = 0; i < tmpm.Length; i++)
                {
                    string[] tmpmm = tmpm[i].Split(',');
                    tmpItem.material[i, 0] = int.Parse(tmpmm[0]);
                    tmpItem.material[i, 1] = int.Parse(tmpmm[1]);
                }

            }
            else
            {
                tmpItem.canmake = false;
            }
				
            Items.Add(tmpItem);
        }

        //读取疾病
        foreach (XmlElement xl1 in Ill_xmlNodeList)
        {	
            GameData.Item tmpItem = new GameData.Item();
            tmpItem.id = int.Parse(xl1.GetAttribute("id"));
            tmpItem.name = xl1.GetAttribute("name");
            //Debug.Log (tmpItem.name);
            string SpritePath = "Ill/" + xl1.GetAttribute("image");

            Sprite s = Resources.Load<Sprite>(SpritePath);
            //if(s == null)Debug.Log ("null");
            tmpItem.sprite = s;

            System.Type mytypebuff = System.Type.GetType(xl1.GetAttribute("illbuffScript"));
            tmpItem.illBuffScript = mytypebuff;

            Ills.Add(tmpItem);
        }

        //读取地图互动物体
        foreach (XmlElement xl1 in ActItems_xmlNodeList)
        {	
            GameData.ActItem tmpItem = new GameData.ActItem();
            tmpItem.id = int.Parse(xl1.GetAttribute("id"));
            tmpItem.name = xl1.GetAttribute("name");
            //Debug.Log (tmpItem.name);
            tmpItem.life = int.Parse(xl1.GetAttribute("life"));
            if (xl1.GetAttribute("isnomaldie") == "true")
            {
                tmpItem.isnomaldie = true;
            }
            else
            {
                tmpItem.isnomaldie = false;
            }
            if (xl1.GetAttribute("isdiedrop") == "true")
            {
                tmpItem.isdiedrop = true;
            }
            else
            {
                tmpItem.isdiedrop = false;
            }

            string tmp = xl1.GetAttribute("carryItem");
            string[] tmpm = tmp.Split(',');
            tmpItem.carryItem = new int[tmpm.Length];
            for (int i = 0; i < tmpm.Length; i++)
            {
                tmpItem.carryItem[i] = int.Parse(tmpm[i]);
            }

            string tmp1 = xl1.GetAttribute("carryIll");
            string[] tmpm1 = tmp1.Split(',');
            tmpItem.carryIll = new int[tmpm1.Length];

            for (int i = 0; i < tmpm1.Length; i++)
            {
                tmpItem.carryIll[i] = int.Parse(tmpm1[i]);
            }

            string attedtypes = xl1.GetAttribute("attedtype");
            if (attedtypes == "SHAKE")
            {
                tmpItem.attedtype = ActObject.AttedType.SHAKE;
            }
            if (attedtypes == "BACK")
            {
                tmpItem.attedtype = ActObject.AttedType.BACK;
            }

            string gPath = "ActObject/" + xl1.GetAttribute("obj");
            GameObject g = Resources.Load<GameObject>(gPath);
            //if(s == null)Debug.Log ("null");
            tmpItem.obj = g;

            ActItems.Add(tmpItem);
        }


        //print(xml.OuterXml);
    }


}
