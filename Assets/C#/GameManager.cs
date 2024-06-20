using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//ctrl+K+C закомментировать , ctrl+K+U раскомментировать


public class GameManager : MonoBehaviour
{
    

    
    public Player player;
    [SerializeField] private Vector2 respawnPoint;
    [SerializeField] private Text playerLivesText;




    private void Start()
    {
        



    }



    private void Update()
    {
        playerLivesText.text = Player.PlayerLives.ToString();
        LoseGame();
        KillPlayer();



    }

    private void KillPlayer()
    {
        if(Player.PlayerLives == 0)
        {
            player.PlayerDeath();
            
        }
        
    }

    private void LoseGame()
    {
        if (Player.PlayerLives == 0)
        {
            //загружает сцену 1 ( сцена экрана проигрыша)
            SceneManager.LoadScene(1);
        }
    }

    

    

   




}
