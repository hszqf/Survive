using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipBuff : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject tipobj;
    string myname;
    GameObject mytip;



    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Pointer enter.");
        myname = gameObject.name;
        mytip = (GameObject)Instantiate(tipobj, transform.position + new Vector3(70, 45, 0), transform.localRotation);//这里位置有问题 硬编码了一下
        mytip.transform.SetParent(GameData.Mycanvas.transform);//放到Canvas下
        mytip.transform.GetChild(0).GetComponent<Text>().text = getTip();
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Pointer exit.");
        Destroy(mytip);
    }

    string getTip()
    {
        //疾病BuffTip
        string str = "这个Buff的说明制作者忘写了，快去怼他";
        if (myname == "红眼病Buff")
        {
            str = "红眼病：欺负兔兔是不好的";
        }





        //道具BuffTip
        if (myname == "种子Buff")
        {
            str = "头上长了个树苗？";
        }
        if (myname == "苹果Buff")
        {
            str = "你已经吃饱了";
        }
        if (myname == "石头Buff")
        {
            str = "你真的吃了";
        }
						

        return str;
    }
	
    //如果buff每次都刷新这里会导致闪烁，buff如果只是数字更改，不要刷新整个
    void OnDestroy()
    {
        Debug.Log("Pointer exit.");
        Destroy(mytip);
    }
}
