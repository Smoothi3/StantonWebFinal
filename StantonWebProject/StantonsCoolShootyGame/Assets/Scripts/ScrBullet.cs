using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrBullet : MonoBehaviour
{
    public GameObject gameManager;
    public float activeTime = 2f;
    float activeTick = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        activeTick += Time.deltaTime;
        if(activeTick >= activeTime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            GameObject.Destroy(collision.gameObject);
            GameObject.Destroy(gameObject);
            ScrGameManager.score++;
        }
    }
}
