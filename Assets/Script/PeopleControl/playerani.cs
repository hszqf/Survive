using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class playerani : MonoBehaviour
{
    public GameObject attcollider;
    private Animator ani;

    float basespeed = 0.85f;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
        //根据当前速度系数改变动画速度
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            ani.speed = basespeed * GameData.PlayerMoveSpeed;
        }
        else
        {
            ani.speed = basespeed;
        }
        if (PlayerData.canmove)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                if (ani.GetFloat("go") != 1)
                {
                    ani.SetFloat("go", 1);
                }
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if (ani.GetFloat("go") != 1)
                {
                    ani.SetFloat("go", 1);
                }
            }
            else
            {
                ani.SetFloat("go", 0);
            }
            //!ani.GetCurrentAnimatorStateInfo(0).IsName("attact")判断state已经切换到别的，不然会产生一个卡死的bug!!!!!!!!!!
            if (Input.GetMouseButton(0) && ani.GetFloat("attact") == 0 && !ani.GetCurrentAnimatorStateInfo(0).IsName("attact"))
            {
                //Debug.Log ("attstart");
                //点击ui时不触发攻击，点击的不是ui才触发
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("当前没有触摸在UI上");
                    PlayerData.canmove = false;
                    ani.SetFloat("attact", 1);
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
			
            }
        }
    }

    void att()
    {
        attcollider.GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<AudioSource>().Play();
    }


    void attend()
    {
        //if(Input.GetMouseButtonUp (0) && ani.GetFloat ("attact") == 1){
        //Debug.Log ("attend");
        attcollider.GetComponent<BoxCollider2D>().enabled = false;
        ani.SetFloat("attact", 0);	
        PlayerData.canmove = true;
        //}
    }

}
