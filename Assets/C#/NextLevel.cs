using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private int toNextScene;
    
    void Start()
    {
        toNextScene = SceneManager.GetActiveScene().buildIndex +1;
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(toNextScene);
        }
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
        Player.playerLives += 3;
    }


    public void Play()
    {
        SceneManager.LoadScene(2);
    }
}
