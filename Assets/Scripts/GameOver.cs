using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text RoundsText;

    private void OnEnable()
    {
        RoundsText.text = "rounds survived: " + PlayerStats.Rounds.ToString();
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // т.к. в игре 1 уровень - перезагружаем его
    }

    private void Menu()
    {
        Debug.Log("MENU");
    }
}
