using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject tipobj;
    string myname;
    public GameObject mytip;


    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Pointer enter.");
        myname = transform.parent.gameObject.name;
        float h = GetComponent<RectTransform>().rect.height;
        mytip = (GameObject)Instantiate(tipobj, transform.position + new Vector3(0, h, 0), transform.localRotation);
        mytip.transform.SetParent(GameData.Mycanvas.transform);
        mytip.transform.GetChild(0).GetComponent<Text>().text = getTip();
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Pointer exit.");
        Destroy(mytip);

    }

	
    string getTip()
    {
        string str = "这个物品的说明制作者忘写了，快去怼他";
        if (myname == "种子")
        {
            str = "吃了头上会长芽";
        }
        if (myname == "苹果")
        {
            str = "饿得时候吃它就对了！！！";
        }
        if (myname == "石头")
        {
            str = "你确定你真的要吃吗？";
        }
						

        return str;
    }
	
}
