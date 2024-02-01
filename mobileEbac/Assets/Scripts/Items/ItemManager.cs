using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt especialCoins;
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextEspecialCoins;

    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins.value = 0;
        especialCoins.value = 0;
        UpdateUI();
    }

    public void AddEspecialCoins(int amount = 1)
    {
        especialCoins.value += amount;
        UpdateUI();
    }

    public void AddCoins(int amout = 1)
    {
        coins.value += amout;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //uiTextCoins.text = coins.ToString();
        //UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }
}
