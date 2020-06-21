using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    public PlayerController Player;
    public Slider HealthBar;
    public TextMeshProUGUI Score;

    static int score = 0;
    public static int getScore()
    {
        return score;
    }
    public static void ResetScore()
    {
        score = 0;
    }
    public static void AddKill()
    {
        score++;
    }
    private void FixedUpdate()
    {
        Score.text = "Score:" + score;
        HealthBar.value = (float)Player.GetCurrentHP() / (float)Player.startHealth;
    }
}
