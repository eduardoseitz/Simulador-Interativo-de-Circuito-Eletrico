using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    
    #region Declaracoes
    
    [Header("Propriedades do componente")]
    [SerializeField] internal bool isOn;

    internal ComponentUI componentUI;
    [SerializeField] internal List<ComponentController> connectedComponentsList;
    
    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        componentUI = GetComponent<ComponentUI>();
    }

    internal void Start()
    {
        connectedComponentsList = new List<ComponentController>();
        componentUI.UpdateUI("");
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
    
    #endregion
    
    #region Funcoes Ajudantes
    
    #endregion
    
}
