using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartGame(string _level) => SceneManager.LoadScene(_level);
    public void LoadLevel(string _level) => SceneManager.LoadScene(_level);
    public void QuitTheGame() => Application.Quit();
}
