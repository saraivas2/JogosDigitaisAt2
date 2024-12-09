using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class player : MonoBehaviour
{

    private int vel = 4;
    private Vector3 posicao;
    private Rigidbody2D rb;
    [SerializeField] private int jump = 2;
    private int forca = 90;
    public bool move, espada,death = false;
    [SerializeField] private bool bool_jump, bool_jumpUp = false;
    [SerializeField] private int count = 500;
    private Animator animator;
    private int runPlayerHash = Animator.StringToHash("runPlayer");
    private int attack1Hash = Animator.StringToHash("Attack1");
    private int deathHash = Animator.StringToHash("death");
    private int jumpUpHash = Animator.StringToHash("jumpUp");
    private int jumpDownHash = Animator.StringToHash("jumpDown");
    private int idleHash = Animator.StringToHash("idle");
    [SerializeField] private int vida = 100;
    private float valorUp = 0;
    [SerializeField] private bool damage = false;
    private GameObject Player;
    private GameObject Fire;
    private Collider2D isAttack;
    public float radius;
    public LayerMask EnemyLayer;
    private float timeAttack = 0.2f;
    private float altura;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        Fire = Resources.Load("FireObjeto") as GameObject;
        Player = GameObject.FindWithTag("idle");
        GameObject attackObject = GameObject.FindWithTag("espada");
        isAttack=attackObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fire"))
        {
            damage = true;
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("damage"))
        {
            vida -= 2;  
        }
        
        if (collision.gameObject.CompareTag("floor"))
        {
            jump = 2;
            bool_jump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
          bool_jump = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("damage")) 
        {
            timeAttack -= Time.deltaTime;
            if (timeAttack < 0)
            {
                vida -= 2;
                timeAttack = 0.2f;
            }
            
        }
        
        if (collision.gameObject.CompareTag("floor"))
        {
            jump = 2; 
            bool_jump = false;
        }

    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            //count--;
            comando();
            //count = count_jump(count);
            altura = rb.velocity.y;

            if (bool_jump)
            {
                bool_jumpUp = jumpUp(altura);
                JumpPlayer();
            }

            if (damage)
            {
                vida = vida - 10;
                damage = false;
            }

            if (vida < 0)
            {
                death = true;
            }

            if (bool_jump == false & move == false & espada == false & death == false)
            {
                IdlePlayer();
            }
        }
        else
        {
            DeathPlayer();
        }
    }

    
    /*private int count_jump(int count) 
    { 
        if (count <= 0)
        {
            count = 500;
            if (jump == 0) 
            { 
                //jump = 2;
                //bool_jump = false;
            }
            return count;
        }
        return count;
    }*/

    private void movePlayer()
    {
        animator.SetBool(runPlayerHash, move==true && espada==false && bool_jump==false);
        animator.SetBool(attack1Hash, false);
        animator.SetBool(jumpUpHash, false);
        animator.SetBool(jumpDownHash, false);
        animator.SetBool(deathHash, false);
        animator.SetBool(idleHash, false);
        
    }

    private void JumpPlayer()
    {
        animator.SetBool(runPlayerHash, false);
        animator.SetBool(attack1Hash, false);
        animator.SetBool(jumpUpHash, bool_jump==true && espada==false && bool_jumpUp==true);
        animator.SetBool(jumpDownHash, bool_jump==true && espada== false && bool_jumpUp == false);
        animator.SetBool(deathHash, false);
        animator.SetBool(idleHash, false);
    }


    private void AttackPlayer()
    {
        animator.SetBool(runPlayerHash, false);
        animator.SetBool(attack1Hash, espada==true && bool_jump==false);
        animator.SetBool(jumpUpHash, false);
        animator.SetBool(jumpDownHash, false);
        animator.SetBool(deathHash, false);
        animator.SetBool(idleHash, false);
        
    }


    private void DeathPlayer()
    {
        animator.SetBool(runPlayerHash, false);
        animator.SetBool(attack1Hash, false);
        animator.SetBool(jumpUpHash, false);
        animator.SetBool(jumpDownHash, false);
        animator.SetBool(deathHash, death==true);
        animator.SetBool(idleHash, false);
        Invoke("ReloadScene", 3f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void IdlePlayer()
    {
        animator.SetBool(runPlayerHash, false);
        animator.SetBool(attack1Hash, false);
        animator.SetBool(jumpUpHash, false);
        animator.SetBool(jumpDownHash, false);
        animator.SetBool(deathHash, false);
        animator.SetBool(idleHash, true);

    }
    private bool jumpUp(float altura)
    {
        if (altura > valorUp)
        {
            valorUp = altura;
            return true;
        }
        else
        {
            valorUp = 0;
            return false;
        }
    }
    void comando()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;
            movePlayer();
            
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;
            movePlayer();
        }
        else
        {
            move = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) & (jump > 1 & jump <= 2))
            {
                jump--;
                bool_jump = true; 
                rb.AddForce(new Vector2(0, 2 * forca), ForceMode2D.Force);
                
        }
        else
        {
            bool_jump = false;
        }

               

        if (Input.GetKey(KeyCode.DownArrow))
        {
            espada = true;
            AttackPlayer();
            timeAttack -= Time.deltaTime;
            if (timeAttack < 0)
            {
                Collider2D[] isAttack = Physics2D.OverlapCircleAll(Player.transform.position, radius, EnemyLayer);
                foreach (Collider2D col in isAttack)
                {
                    if (col.CompareTag("enemies"))
                    {
                        col.transform.GetComponent<NPCMovement>().tomarDano(5);
                    }
                    else if (col.CompareTag("enemy2"))
                    {
                        col.transform.GetComponent<NPCMovement1>().tomarDano(5);
                    }
                }
                timeAttack = 0.2f;
            }
        }
        else
        {
            espada = false;
        }
    }    
}
