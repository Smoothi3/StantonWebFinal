using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrTitleTransition : MonoBehaviour
{
    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToHighScore()
    {
        SceneManager.LoadScene(3);
    }
}
