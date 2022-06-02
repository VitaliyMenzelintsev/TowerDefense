using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour

{
    public Text livesText;
  
    void Update()
    {
        livesText.text = PlayerStats.Lives + "LIVES";
    }
}
