using UnityEngine;
using System.Collections;

public class playermove : MonoBehaviour
{
    public float basespeed = 0.5f;
    public float basejumpspeed = 2f;
    public bool islanded = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.canmove)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
			
                Vector2 tmp = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector2(basespeed * GameData.PlayerMoveSpeed, tmp.y);
		
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
			
                Vector2 tmp = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector2(-basespeed * GameData.PlayerMoveSpeed, tmp.y);
			
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (islanded)
                {
                    Vector2 tmp = GetComponent<Rigidbody2D>().velocity;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(tmp.x, basejumpspeed * Mathf.Sqrt(GameData.PlayerMoveSpeed));
                    islanded = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "ground")
        {
            islanded = true;
        }
    }
}
