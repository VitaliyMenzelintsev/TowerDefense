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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // �.�. � ���� 1 ������� - ������������� ���
    }

    public void Menu()
    {
        Debug.Log("MENU");
    }
}
