using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentUI : MonoBehaviour
{

    #region Declaracoes
    
    [SerializeField] private string label = "Componente";
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button cancelConnectionButton;
    [SerializeField] private Button confirmConnectionButton;

    private ComponentController _componentController;
    private ComponentUIState _componentUIState;
    
    #endregion
    
    #region Funcoes MonoBehaviour

    private void Awake()
    {
        _componentController = GetComponent<ComponentController>();
    }
    
    #endregion
    
    #region Funcoes Ajudantes
    
    public void UpdateUI(ComponentUIState newState = ComponentUIState.Normal)
    {
        // Atualiza estado da UI.
        _componentUIState = newState;
        
        // Atualiza botoes.
        HideAllButtons();
        switch (newState)
        {
            case ComponentUIState.Normal:
                connectButton.gameObject.SetActive(true);
                interactButton.gameObject.SetActive(_componentController.IsInteractable);
                if (_componentController.connectedComponentsList.Count > 0)
                    disconnectButton.gameObject.SetActive(true);
                break;
            case ComponentUIState.FirstConnection:
                cancelConnectionButton.gameObject.SetActive(true);
                break;
            case ComponentUIState.LastConnection:
                confirmConnectionButton.gameObject.SetActive(true);
                break;
        }
        
        // Atualiza titulo.
        UpdateLabel();
    }
    
    public void UpdateLabel(string otherLabel = "")
    {
        labelText.text = $"{label}{otherLabel}";
    }

    public void OnConnectButtonClick()
    {
        _componentController.Connect();
    }
    
    public void OnDisconnectButtonClick()
    {
        _componentController.Disconnect();
    }
    
    public void OnInteractButtonClick()
    {
        if (_componentController.isInteractable)
            _componentController.Interact();
    }
    
    public void OnCancelConnectionButtonClick()
    {
        _componentController.CancelConnection();
    }
    
    public void OnConfirmConnectionButtonClick()
    {
        _componentController.ConfirmConnection();
    }
    
    private void HideAllButtons()
    {
        connectButton.gameObject.SetActive(false);
        disconnectButton.gameObject.SetActive(false);
        interactButton.gameObject.SetActive(false);
        cancelConnectionButton.gameObject.SetActive(false);
        confirmConnectionButton.gameObject.SetActive(false);
    }
    
    #endregion
    
}

public enum ComponentUIState { Normal, FirstConnection, LastConnection }
