using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComponentUI))]
public class ComponentController : MonoBehaviour
{
    
    #region Declaracoes

    [Header("Propriedades do componente")] 
    public bool isInteractable;
    public Transform groundPole;
    public Transform powerPole;
    
    public bool isOn; //
    public List<ComponentController> connectedComponentsList; //
    private ComponentUI _componentUI;

    #endregion

    #region Getters e Setters

    internal bool IsInteractable => isInteractable;

    public ComponentUI ComponentUI => _componentUI;

    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        _componentUI = GetComponent<ComponentUI>();
    }

    internal void Start()
    {
        connectedComponentsList = new List<ComponentController>();
        _componentUI.UpdateUI();
    }
    
    #endregion

    #region Funcoes ComponentController
    
    public virtual void Connect()
    {
        CircuitManager.instance.StartConnection(this);
    }
    
    public virtual void Disconnect()
    {
        CircuitManager.instance.DisconnectComponentFromAllComponents(this);
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
