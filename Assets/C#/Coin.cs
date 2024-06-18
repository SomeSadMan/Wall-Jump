using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    
    static int coinsCount;
    
    [SerializeField] private Text coinText;
    [SerializeField] private int coinIndex;
    public bool coinCollected;
    




    private void Start()
    {
 
        coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        coinText.text = "Cчет: " + coinsCount;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) 
        {
            coinsCount++;
            PlayerPrefs.SetInt("coinsCount", coinsCount);
            coinText.text = "Cчет: " + coinsCount;
            gameObject.SetActive(false);
            coinCollected = true;


        }
    }

      
  
}
