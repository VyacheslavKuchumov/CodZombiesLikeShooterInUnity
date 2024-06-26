using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScore : MonoBehaviour
{
    private float score;
    public TextMeshProUGUI scoreText;
    public string scoreBaseText;

    private float testTimer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        score = Mathf.Clamp(score, 0, 9999999);
        UpdateScore();

        testTimer += Time.deltaTime;
        if (testTimer < 1)
        {
            EarnScore(1035);

        }
        /*else
        {
            SpendScore(600);
        }*/

    }

    public void SpendScore(float amount)
    {
        score -= amount;
        
    }

    public void EarnScore(float amount)
    {
        score += amount;
        
    }

    private void UpdateScore()
    {
        scoreText.text = scoreBaseText + score.ToString() + "$";
    }

    public bool CanPlayerBuyThis(float price)
    {
        if (score >= price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
