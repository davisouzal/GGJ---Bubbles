using UnityEngine;
using UnityEngine.UI;
 using TMPro;

public class PlayerUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;    
    public PlayerController playerController; // Referência ao PlayerController

    // Update é chamado uma vez por frame
    void Update()
    {
        if (playerController != null && healthText != null)
        {
            healthText.text = playerController.playerHealth.ToString();
        }
    }
}
