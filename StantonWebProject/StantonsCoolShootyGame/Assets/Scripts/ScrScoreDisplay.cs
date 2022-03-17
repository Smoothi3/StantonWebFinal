using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
using TMPro;
using UnityEngine.UI;

public class ScrScoreDisplay : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<ScrPlayer>().reload)
        {
            bulletText.text = "Reload";
            if (player.GetComponent<ScrPlayer>().reloading)
            {
                bulletText.text = "Reloading...";
            }
        } else
        {
            bulletText.text = "Bullets: " + player.GetComponent<ScrPlayer>().bullets.ToString();
        }

        
        scoreText.text = "Score: " + ScrGameManager.score.ToString();
    }
}
