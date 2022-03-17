using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrPlayer : MonoBehaviour
{
    public GameObject objGun;
    public GameObject bullet;
    private Rigidbody2D rigidBody;
    float maxVel = 5;
    float velocity = 0;
    float jumpForce = 400;
    public bool grounded = false;
    float reloadTimer = 1f;
    float reloadTick = 0;
    public bool reload = false;
    public bool reloading = false;
    public int bullets = 5;
    float prevSide = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool fire = Input.GetKeyDown(KeyCode.Space);

        if(reload == true && fire)
        {
            reloading = true;
        }

        if(reloading)
        {
            reloadTick += Time.deltaTime;
            objGun.transform.Rotate(0, 0, 15);
            if(reloadTick >= reloadTimer)
            {
                reloadTick = 0;
                objGun.transform.rotation = Quaternion.Euler(0, 0, 0);
                reload = false;
                reloading = false;
                bullets = 5;
            }
        }

        if (fire && (reload == false))
        {
            //Shoot bullet
            bullets--;
            if(bullets <= 0)
            {
                reload = true;
            }
            //Debug.Log("Bang" + bullets.ToString());
            GameObject newBullet = GameObject.Instantiate(bullet, objGun.transform.position, objGun.transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * prevSide, 0);
        }

        float horMove = Input.GetAxisRaw("Horizontal");
        velocity = Mathf.Lerp(velocity, horMove * maxVel, 0.025f);
        //Debug.Log(velocity);
        //Debug.Log(rigidBody.position.x + horMove * Time.fixedDeltaTime * velocity);
        rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
        //rigidBody.MovePosition(new Vector2(rigidBody.position.x + (horMove * velocity)*Time.deltaTime, 0));

        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Debug.Log("Jump");
                rigidBody.AddForce(new Vector2(0, jumpForce));
            }
        }

        if(horMove != 0)
        {
            gameObject.transform.localScale = new Vector2(horMove, 1);
            prevSide = horMove;
        }

        if(gameObject.transform.position.x < -11)
        {
            gameObject.transform.position = new Vector2(-11, gameObject.transform.position.y);
        }

        if(gameObject.transform.position.x > 11)
        {
            gameObject.transform.position = new Vector2(11, gameObject.transform.position.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            grounded = true;
        }

        if(collision.transform.tag == "Enemy")
        {
            ScrGameManager.GameEnd();
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            grounded = false;
        }
    }
}
