using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComponentUI : MonoBehaviour
{
    [SerializeField] private string label = "Componente";
    [SerializeField] private TextMeshProUGUI labelText;
    public Button connectButton;
    public Button disconnectButton;
    public Button interactButton;
    public Button cancelConnectionButton;
    public Button confirmConnectionButton;
    
    public virtual void UpdateUI(string otherLabel)
    {
        labelText.text = $"{label} {otherLabel}";
    }
    
}
