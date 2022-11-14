using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private bool isAttack = false;
    public float speed = 4f;
    public GameObject player;
    public float damage = 1f;
    public float Health = 2f;
    public float AttackTimer = 1f;
    public int levelNo = 2;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isattack", false);
        animator.SetBool("iswalk", false);
        animator.SetBool("isdie", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            animator.SetBool("isattack", false);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", true);
        }
        else
        {
            if (isAttack)
            {
                animator.SetBool("isattack", true);
                animator.SetBool("iswalk", false);
                animator.SetBool("isdie", false);
            }
            else
            {
                if (this.gameObject.tag == "Boss")
                {
                    if (Mathf.Abs(player.transform.position.x - this.transform.position.x) <= 15f)
                    {
                        if (player.transform.position.x < this.transform.position.x - 0.7f)
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
                            animator.SetBool("isattack", false);
                            animator.SetBool("iswalk", true);
                            animator.SetBool("isdie", false);
                        }
                        if (player.transform.position.x > this.transform.position.x + 0.7f)
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
                            animator.SetBool("isattack", false);
                            animator.SetBool("iswalk", true);
                            animator.SetBool("isdie", false);
                        }
                    }
                }
                else
                {
                    if (player.transform.position.x < this.transform.position.x - 0.7f)
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
                        animator.SetBool("isattack", false);
                        animator.SetBool("iswalk", true);
                        animator.SetBool("isdie", false);
                    }
                    if (player.transform.position.x > this.transform.position.x + 0.7f)
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
                        animator.SetBool("isattack", false);
                        animator.SetBool("iswalk", true);
                        animator.SetBool("isdie", false);
                    }
                }
                flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isDead)
        {
            collision.gameObject.GetComponent<Movement>().takeDamage(damage);
            isAttack = true;
        }
        if (collision.gameObject.tag == "NextLevel" && !isDead)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttack = false;
        }
    }

    public void takeDamage(float damage)
    {
        this.Health -= damage;
        if (this.Health <= 0f)
        {
            if (this.gameObject.tag == "Boss" && levelNo == 1)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                isDead = true;
                string tmp = this.gameObject.tag;
                Destroy(this.gameObject, 1.0f);
                if (tmp == "Boss")
                {
                    SceneManager.LoadScene("Won");
                }
            }
        }
    }
    private void flip()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
