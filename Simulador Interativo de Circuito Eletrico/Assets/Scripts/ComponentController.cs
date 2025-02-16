using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    
    #region Declaracoes
    
    [Header("Propriedades do componente")]
    [SerializeField] internal string label = "Componente";
    [SerializeField] internal TextMeshProUGUI labelText;
    [SerializeField] internal bool isOn;
    
    [SerializeField] internal List<ComponentController> connectedComponentsList;
    
    internal void Start()
    {
        connectedComponentsList = new List<ComponentController>();
        UpdateUI();
    }
    
    #endregion

    #region Funcoes ComponentController
    
    public virtual void Connect()
    {
        
    }
    
    public virtual void Disconnect()
    {
        
    }
    
    public virtual void Interact()
    {
        
    }

    internal virtual void UpdateUI()
    {
        labelText.text = label;
    }
    
    #endregion
    
    #region Funcoes Ajudantes
    
    #endregion
    
}
