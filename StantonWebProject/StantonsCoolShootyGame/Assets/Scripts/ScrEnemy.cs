using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrEnemy : MonoBehaviour
{
    public GameObject player;
    float maxVelocity = 2;
    float velocity = 0;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float horMove = 0;

        if (player.transform.position.x > gameObject.transform.position.x)
        {
            horMove = 1;
        } else
        {
            horMove = -1;
        }

        velocity = Mathf.Lerp(velocity, horMove * maxVelocity, 0.025f);

        rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);

        if (horMove != 0)
        {
            gameObject.transform.localScale = new Vector2(horMove, 1);
        }
    }
}
