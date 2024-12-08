using System.Collections;   
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class idle : MonoBehaviour
{
    float vel = 2f;
    private bool move, tiro = false;
    private Transform flip;
    private Rigidbody2D rb;
    private Animator animator;
    private float jump = 2f;
    private int runningHash = Animator.StringToHash("running");
    private int jupingHash = Animator.StringToHash("jumping");
    private int shootingHash = Animator.StringToHash("shooting");
    private int runshootHash = Animator.StringToHash("runshoot");
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flip = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        comando();
        animator.SetBool(runningHash, move & !tiro);
        animator.SetBool(jupingHash, jump >0 & jump<2f);
        animator.SetBool(shootingHash, tiro & !move);
        animator.SetBool(runshootHash, tiro & move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump = 2f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        move=false;
    }

    void comando()
    {
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;


        }
        else if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;

        }
        else
        {
            move = false;
        }



        if ((Input.GetKeyDown(KeyCode.Space) | Input.GetKey(KeyCode.UpArrow)) & jump > 0)
        {
            jump--;
            rb.AddForce(Vector2.up * 350);
        }

        if (Input.GetKey(KeyCode.Q))

        {
            tiro = true;
            //Instantiate(balas, cano.transform.position, cano.transform.rotation);

        }
        else
        {
            tiro = false;
        }

        
    }
}
