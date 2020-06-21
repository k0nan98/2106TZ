using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    public TextMeshProUGUI Score;
    void Update()
    {
        Score.text = "Score:" + PlayerGUI.getScore();
    }
    public void restartGame()
    {
        PlayerGUI.ResetScore();
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void Exit()
    {
        PlayerGUI.ResetScore();
        SceneManager.LoadScene(0);
    }
}
