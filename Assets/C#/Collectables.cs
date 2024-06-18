using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Collectables : MonoBehaviour
{
    [SerializeField] private Text coinText;
    private int coinsCount;
    

    private void Start()
    {
        coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        coinText.text = "Счет:" + coinsCount;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Coin"))
        {
            
            coinsCount++;
            PlayerPrefs.SetInt("coinsCount", coinsCount);
            Destroy(collider.gameObject);
            coinText.text = "Cчет: " + coinsCount;
            
        }
    }
}
