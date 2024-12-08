using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCJump : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private bool jumpBool = false;
    private Animator animator;
    private int walkEHash = Animator.StringToHash("walkE");
    private int dieEHash = Animator.StringToHash("dieE");
    private int jumpEHash = Animator.StringToHash("jumpE");
    private int attackEHash = Animator.StringToHash("attackE");
    private int count = 500;
    private int jumpForce = 90;
    public int jump = 2;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
    }

    private void Update()
    {
        if (jump == 0)
        {
            count--;
            count = count_jump(count);
        }
        if (jumpBool)
        {
            JumpForce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("floor"))
        {
            jumpBool = true;
            animator.SetBool(dieEHash, false);
            animator.SetBool(jumpEHash, jumpBool);
            animator.SetBool(walkEHash, false);
            animator.SetBool(attackEHash, false);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        jumpBool = false;
        animator.SetBool(dieEHash, false);
        animator.SetBool(jumpEHash, jumpBool);
        animator.SetBool(walkEHash, false);
        animator.SetBool(attackEHash, false);
    }

    private void JumpForce()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }

    private int count_jump(int count)
    {
        if (count <= 0)
        {
            count = 500;
            jump = 2;    
        }
        return count;
    }

}
