using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComponentUI))]
public class ComponentController : MonoBehaviour
{
    
    #region Declaracoes

    [Header("Propriedades do componente")] 
    [SerializeField] internal bool isInteractable;
    [SerializeField] internal bool isOn;
    public List<ComponentController> connectedComponentsList;
    internal ComponentUI componentUI;
    

    #endregion

    #region Getters e Setters

    internal bool IsInteractable => isInteractable;

    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        componentUI = GetComponent<ComponentUI>();
    }

    internal void Start()
    {
        connectedComponentsList = new List<ComponentController>();
        componentUI.UpdateUI();
    }
    
    #endregion

    #region Funcoes ComponentController
    
    public virtual void Connect()
    {
        CircuitManager.instance.StartConnection(this);
    }
    
    public virtual void Disconnect()
    {
        CircuitManager.instance.DisconnectAllFromComponent(this);
    }
    
    public virtual void Interact()
    {
        
    }
    
    public virtual void ConfirmConnection()
    {
        CircuitManager.instance.CompleteConnection(this);
    }
    
    public virtual void CancelConnection()
    {
        CircuitManager.instance.CancelConnection();    
    }
    
    #endregion
    
    #region Funcoes Ajudantes
    
    #endregion
    
}
