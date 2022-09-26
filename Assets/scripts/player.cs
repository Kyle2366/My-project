using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody2D rb;
    float currentSpeed;
    float walkSpeed = 1f;
    float runSpeed = 8f;
    public Animator anim;
    int health = 100;
    public GameObject startPos;
    bool touchingPlatform = false;
    float jumpVelocity = 17f;
   
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        if (health <= 0)
        {
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {
        anim.SetTrigger("death");
        health = 100;
        yield return new WaitForSeconds(1f);

        transform.position = startPos.transform.position;
    }



    void MyInput()
    {
        print(touchingPlatform);
        Vector2 vel = rb.velocity;


        if (Input.GetKey("up") && touchingPlatform)
        {
            vel.y = jumpVelocity;
            anim.SetBool("jumping", true);
        }
        else
        {
            anim.SetBool("jumping", false);
        }

        if (Input.GetKey("down"))
        {
            print("player pressed down.");
            rb.velocity = new Vector2(0, -2);
        }

        if (Input.GetKey("right"))
        {

            print("player pressed right.");
            vel.x = 4;
            anim.SetBool("running", true);
        }

        if (Input.GetKey("left"))
        {
            print("player pressed left.");
            vel.x = -4;
            anim.SetBool("running", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attacking", true);
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("attacking2", true);
                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetBool("attacking3", true);
                }

            }

        }
        else
        {
            anim.SetBool("attacking", false);
            anim.SetBool("attacking2", false);
            anim.SetBool("attacking3", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("block", true);
        }
        else
        {
            anim.SetBool("block", false);
        }

        //health test
        if (Input.GetKeyDown(KeyCode.L))
        {
            health -= 20;
        }
        rb.velocity = vel;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="platform")
        {
            touchingPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingPlatform = false;
        }
    }

    void OnCollisionenemy2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            touchingPlatform = true;
        }
    }
    void OnCollisionenemyexit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            touchingPlatform = false;
        }
    }

}

