using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reinicia el nivel
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menuscene"); 
    }
}
