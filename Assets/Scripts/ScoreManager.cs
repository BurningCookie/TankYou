using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int score = 0;
    private static int xp = 0;
    private static int level = 1;
    private static int requiredXp = 10;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text xpText;

    public static void AddXp(int value)
    {
        xp += value;
    }
    
    public static int GetScore()
    {
        return score;
    }

    public static void AddScore(int amount)
    {
        score += amount;
    }

    public static void SetScore(int value)
    {
        score = value;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (xp >= requiredXp)
        {
            xp = xp - requiredXp;
            level++;
            requiredXp = 10 * level;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (xpText != null)
        {
            xpText.text = xp.ToString() + " / " + requiredXp.ToString();
        }
        if (levelText != null)
        {
            levelText.text = "Level: " + level.ToString();
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

}
