using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    private bool IsFacingRight = true;
    private bool grounded = false;
    private Animator animator;
    private bool isDead = false;
    public GameObject checkPoint;
    public int checkPointNo = 1;
    public float speed = 4f;
    public float jumpPower = 4f;
    public float attackPower = 1f;
    public float Health = 10f;
    public int Lives = 3;
    public Text health_txt;
    public Text Lives_txt;

    // Use this for initialization
    void Start () {
        health_txt.text = "Health: ";
        Lives_txt.text = "Lives: ";
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isidle", true);
        animator.SetBool("iswalk", false);
        animator.SetBool("isdie", false);
        animator.SetBool("isattack", false);
        animator.SetBool("isjump", false);
    }
	
	// Update is called once per frame
	void Update () {
        health_txt.text = "Health: " + Health.ToString();
        Lives_txt.text = "Lives: " + Lives.ToString();
        if (Input.GetKey(KeyCode.RightArrow)) // move right
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
            if (!IsFacingRight)
            {
                flip();
                IsFacingRight = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // move left
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
            if (IsFacingRight)
            {
                flip();
                IsFacingRight = false;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) && grounded) // jump
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(this.GetComponent<Rigidbody2D>().velocity.x, jumpPower);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // crouch and slide
        }
        
        //Animation
        if (isDead)
        {
            animator.SetBool("isidle", false);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", true);
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", false);
            Debug.Log("DEAD");
        }
        else if ((Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow)) && grounded)
        {
            animator.SetBool("isidle", false);
            animator.SetBool("iswalk", true);
            animator.SetBool("isdie", false);
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", false);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //Attack
            animator.SetBool("isidle", false);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", false);
            animator.SetBool("isattack", true);
            animator.SetBool("isjump", false);
        }
        else if (!grounded)
        {
            animator.SetBool("isidle", false);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", false);
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", true);
            
        }
        else if (grounded)
        {
            animator.SetBool("isidle", true);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", false);
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", false);
            
        }
        
        //if (this.transform.position.x < 3) // don't break level start
        //{
        //    this.transform.position = new Vector2(3, this.transform.position.y);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().takeDamage(attackPower);
        }
        if (collision.gameObject.tag == "NextLevel")
        {
            SceneManager.LoadScene("Scene2");
        }
        if (collision.gameObject.tag == "life")
        {
            this.Lives++;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    private void flip()
    {
        if (this.transform.rotation.y == 0)
        {
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void takeDamage(float damage)
    {
        this.Health -= damage;
        if (this.Health <= 0f)
        {
            if (Lives > 0)
            {
                animator.SetBool("isidle", false);
                animator.SetBool("iswalk", false);
                animator.SetBool("isdie", true);
                animator.SetBool("isattack", false);
                animator.SetBool("isjump", false);

                this.transform.position = GameObject.FindGameObjectWithTag("check" + checkPointNo.ToString()).transform.position;
                this.Health = 10f;
                Lives--;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var item in enemies)
                {
                    Destroy(item);
                }
            }
            else
            {
                isDead = true;
                Health = 0f;
                Destroy(this.gameObject,2.0f);
                Debug.Log("Game Over");
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void takeSpikeDamage()
    {
        if (Lives > 0)
        {
            animator.SetBool("isidle", false);
            animator.SetBool("iswalk", false);
            animator.SetBool("isdie", true);
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", false);

            this.transform.position = GameObject.FindGameObjectWithTag("check" + checkPointNo.ToString()).transform.position;
            this.Health = 10f;
            Lives--;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var item in enemies)
            {
                Destroy(item);
            }
        }
        else
        {
            isDead = true;
            Health = 0f;
            Destroy(this.gameObject, 2.0f);
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }
}
