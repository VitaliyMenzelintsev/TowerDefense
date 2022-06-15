using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    public Text LivesText;
  
    private void Update()
    {
        LivesText.text = PlayerStats.Lives + "LIVES";
    }
}
