using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayWithAI()
    {
        // Store the choice in PlayerPrefs (1 means playing with AI)
        PlayerPrefs.SetInt("GameMode", 1);
        // Load the game scene
        SceneManager.LoadScene("GameScene");  // Replace with the actual name of your game scene
    }

    public void PlayWithPartner()
    {
        // Store the choice in PlayerPrefs (2 means playing with another player)
        PlayerPrefs.SetInt("GameMode", 2);
        // Load the game scene
        SceneManager.LoadScene("GameScene");  // Replace with the actual name of your game scene
    }
}
