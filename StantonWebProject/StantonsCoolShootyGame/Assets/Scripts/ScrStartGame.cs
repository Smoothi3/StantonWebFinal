using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrStartGame : MonoBehaviour
{
    public GameObject nameBox;

    public void StartClicked()
    {
        ScrGameManager.GameStart();
        if(ScrGameManager.playerName == nameBox.GetComponent<InputField>().text)
        {
            ScrGameManager.timesPlayed++;
        } else
        {
            ScrGameManager.timesPlayed = 1;
        }
        ScrGameManager.playerName = nameBox.GetComponent<InputField>().text;
        SceneManager.LoadScene(1);
    }
}
