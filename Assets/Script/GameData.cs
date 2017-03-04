using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    ///控制移动，跳跃高度，动画速度的参数
    public static float PlayerMoveSpeed = 1f;

    public GameObject Player;
    // 以玩家为中心开始生成
    public float Xdistance = 3f;
    // 距离玩家多远生成
    // 标记已经生成完毕的区域
    public float DoneMin = 0;
    public float DoneMax = 0;

    // 游戏世界中基础的道具模板
    public static GameObject ItemBase;
    public  GameObject ItemBaset;

    //掉落道具时，道具弹起的效果
    public static float ItemDropJumpForce = 10f;


    //===========================时间管理=======================
    //累计时间
    float Map_time;
    public float Map_creattime = 1;
    ///游戏总时间
    public static float Game_totaltime = 0f;
    //===========================天气管理=======================
    float probability_change = 30f;
    //每帧30秒触发天气切换的逻辑

    int probability_sun = 10;
    int probability_rain = 10;
    int probability_snow = 10;
    //===============================================================================

	
    //==================================常用全局物体===============================
    ///摄像机
    public static GameObject Mycamera;
    public  GameObject Mycamerat;
    ///UIROOT
    public static GameObject Mycanvas;
    public  GameObject Mycanvast;
    //===============================================================================


    //==================================总地图物体列表===============================
    ///总地图物体列表
    public static List<ActItem> AllActItems;

    public struct ActItem
    {
        public int id;
        public string name;
        public GameObject obj;
        public int life;
        //生命值,需要初始化
        public bool isnomaldie;
        //是否通过生命为0简单判定死亡
        public bool isdiedrop;
        //是否死亡时掉落物品
        public int[] carryItem;
        //可掉落物品
        public int[] carryIll;
        //可掉感染疾病
        public ActObject.AttedType attedtype;
        //被攻击后的反应类型
    }

    //==================================总道具列表===============================
    ///总地图物体列表
    public static List<Item> AllItems;
    ///用来给制作刷新使用
    public static bool PlayerMakeItemsDirty = true;
    ///总疾病列表
    public static List<Item> AllIlls;

    public struct Item
    {
        public int id;
        public int num;
        public string name;
        public Sprite sprite;
        public System.Type eatScript;
        public System.Type eatBuffScript;
        public System.Type illBuffScript;
        public bool isweapon;
        public bool canmake;
        public int[,] material;
        //制作材料 ID，NUM
    }

    public enum illType
    {
        redeye = 1000,
        cold = 1001
    }
    //===============================================================================


    public void Start()
    {
        //初始化全局变量
        Mycamera = Mycamerat;
        Mycanvas = Mycanvast;

        ItemBase = ItemBaset;

        //TODO 读取所有物品信息，还需整合其他信息进来
        XMLManager xml = new XMLManager();
        string mapXmlPath = Application.dataPath + "/Data/Items.xml";
        xml.init(mapXmlPath);
        AllItems = xml.Get_Items();
        AllIlls = xml.Get_Ills();
        AllActItems = xml.Get_ActItems();

        InvokeRepeating("changeWeather", probability_change, probability_change);
    }


    public void Update()
    {
        Game_totaltime += Time.deltaTime;
        float px = Player.transform.position.x;
        //在更新完成区域前检测并生成
        CreatMap();

        //更新已完成区域
        float DoneMint = px - Xdistance;
        float DoneMaxt = px + Xdistance;
        if (DoneMint < DoneMin)
        {
            DoneMin = DoneMint;
        }
        if (DoneMaxt > DoneMax)
        {
            DoneMax = DoneMaxt;
        }
    }

    //天气切换
    void changeWeather()
    {
        Weather.Type weather = Weather.Type.SUN;
        int total_probablity = probability_sun + probability_rain + probability_snow;
        int rnd = Random.Range(0, total_probablity);
        if (rnd < probability_sun)
        {
            weather = Weather.Type.SUN;
        }
        else if (rnd < probability_sun + probability_rain)
        {
            weather = Weather.Type.RAIN;
        }
        else
        {
            weather = Weather.Type.SNOW;
        }

        Weather.nowWeather = weather;
        Debug.Log("change weather" + weather);
    }


    void CreatMap()
    {
        float px = Player.transform.position.x;
        int rnd = Random.Range(0, 2);//随机出0/1,标记左右，所以每Map_creattime触发时有一半概率生成不了
        Map_time += Time.deltaTime;
        //Debug.Log (time);
        if (Map_time > Map_creattime)
        {
            Map_time = 0;
            if (isNewer() && rnd == 0)
            {
                int id = Random.Range(0, AllActItems.Count);
                CreateActObject(id, new Vector3(px + Xdistance, 0, 0));
                //CreateItem (2, new Vector3 (px + Xdistance+0.5f, 0, 0), Quaternion.identity);
            }
            if (isNewl() && rnd == 1)
            {
                int id = Random.Range(0, AllActItems.Count);
                CreateActObject(id, new Vector3(px - Xdistance, 0, 0));
                //CreateItem (2, new Vector3 (px - Xdistance-0.5f, 0, 0), Quaternion.identity);
            }

        }
    }

    //查看当前位置是否已经生成过了
    bool isNewl()
    {
        if (DoneMin <= Player.transform.position.x - Xdistance && Player.transform.position.x - Xdistance <= DoneMax)
        {
            return false;
        }
        return true;
    }

    bool isNewer()
    {
        if (DoneMin <= Player.transform.position.x + Xdistance && Player.transform.position.x + Xdistance <= DoneMax)
        {
            return false;
        }
        return true;
    }

    //调用后直接在指定位置生成指定id的Animal
    public static GameObject CreateActObject(int i, Vector3 p)
    {
        //Debug.Log (i);
        ActItem tmp = AllActItems[i];
        //Debug.Log (tmp.obj);
        GameObject g = (GameObject)Instantiate(tmp.obj, tmp.obj.transform.localPosition + p, tmp.obj.transform.localRotation);
        g.name = tmp.name;

        ActObject actO = g.GetComponent<ActObject>();
        actO.life = tmp.life;
        actO.isnomaldie = tmp.isnomaldie;
        actO.isdiedrop = tmp.isdiedrop;
        actO.carryItem = tmp.carryItem;
        actO.carryIll = tmp.carryIll;
        actO.attedtype = tmp.attedtype;
        return g;
    }
    //调用后直接在指定位置通过模板生成指定id的Item
    public static GameObject CreateItem(int i, Vector3 p, Quaternion q)
    {
        GameObject t = (GameObject)Instantiate(ItemBase, p + new Vector3(0, 0, -2), q);//让掉落的道具显示在靠前的位置
        t.name = AllItems[i].name;
        t.GetComponent<SpriteRenderer>().sprite = AllItems[i].sprite;
        t.transform.GetChild(0).GetComponent<ItemGet>().id = i;
        PolygonCollider2D polygon = t.AddComponent<PolygonCollider2D>();

        t.transform.GetChild(0).gameObject.AddComponent<PolygonCollider2D>().points = polygon.points;
        t.transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;

        t.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, ItemDropJumpForce, 0));
        return t;
    }

    public static void restPlayerMoveSpeed()
    {
        PlayerMoveSpeed = 1f;
    }

}
