using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    void OnEnable()
    {
        roundsText.text = "rounds survived: " + PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // т.к. в игре 1 уровень - перезагружаем его
    }

    public void Menu()
    {
        Debug.Log("MENU");
    }
}
