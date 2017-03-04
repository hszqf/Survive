using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour
{
    public GameObject weathermask;
    Transform maskTransform;

    public GameObject raindrop;

    public enum Type
    {
        SUN,
        RAIN,
        SNOW
    }

    bool isSun = false;
    bool isRain = false;
    bool isSnow = false;
    float rainrate = 0.01f;

    public static Type nowWeather = Type.RAIN;

    public void Start()
    {
        maskTransform = weathermask.transform;
    }
    // Update is called once per frame
    void Update()
    {
        maskTransform = weathermask.transform;

        switch (nowWeather)
        {
            case Type.SUN:
                if (!isSun)
                {
                    cancelstate();
                    CancelInvoke();
                    isSun = true;
                }
                break;
            case Type.RAIN:
                if (!isRain)
                {
                    cancelstate();
                    CancelInvoke();
                    isRain = true;
                    InvokeRepeating("rain", rainrate, rainrate);
                }
                break;
            case Type.SNOW:
                if (!isSnow)
                {
                    cancelstate();
                    CancelInvoke();
                    isSnow = true;
                }
                CancelInvoke();
                break;
            default:
                break;
        }
    }

    void cancelstate()
    {
        isSun = false;
        isRain = false;
        isSnow = false;
    }

    void rain()
    {


        float x = Random.Range(0, 1.5f * Screen.width);
        Vector3 v = new Vector3(x, 0, 0);
        Vector3 vv = Camera.main.ScreenToWorldPoint(v);
        //Debug.Log (vv.x);
        Vector3 vvv = new Vector3(vv.x, 0, 0);

        Instantiate(raindrop, maskTransform.localPosition + vvv, maskTransform.localRotation);
        //Debug.Log (maskTransform.position);
    }


}
