using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoins : MonoBehaviour
{
    public Text scoreTxt;
    private int score;
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreTxt.text = score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Coin") == true)
        {
            score = score + 1;
            Destroy(col.gameObject);
        }
    }
}
