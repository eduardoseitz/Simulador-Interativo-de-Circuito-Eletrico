using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    
    #region Declaracoes

    public static CircuitManager instance;
    
    [Header("Propriedade do circuito")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshProUGUI circuitStateText;

    private bool _isCircuitComplete;
    [SerializeField] private ComponentController[] _components;
    [SerializeField] private ComponentController _firstSelectedComponent; //
    [SerializeField] private ComponentController _lastSelectedComponent; //

    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        // Padrao singleton.
        if (instance)
            Destroy(this);
        else
            instance = this;

        _components = GetComponentsInChildren<ComponentController>();
    }

    private void Start()
    {
        UpdateCircuitState();
    }

    #endregion

    #region Funcoes Ajudantes

    public void UpdateCircuitState()
    {
        circuitStateText.text = "Circuito" + ((_isCircuitComplete) ? " Completo" : " Incompleto");
        circuitStateText.color = (_isCircuitComplete) ? Color.green : Color.red;
    }

    public void StartConnection(ComponentController component)
    {
        _firstSelectedComponent = component;
        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] == component)
                _components[i].componentUI.UpdateUI(ComponentUIState.FirstConnection);
            else
                _components[i].componentUI.UpdateUI(ComponentUIState.LastConnection);
        }
    }

    public void CompleteConnection(ComponentController componentController)
    {
        // Make connection
        _lastSelectedComponent = componentController;
        if (!_firstSelectedComponent.connectedComponentsList.Contains(_lastSelectedComponent))
            _firstSelectedComponent.connectedComponentsList.Add(_lastSelectedComponent);
        if (!_lastSelectedComponent.connectedComponentsList.Contains(_firstSelectedComponent))
            _lastSelectedComponent.connectedComponentsList.Add(_firstSelectedComponent);
        
        // Update ui.
        for (int i = 0; i < _components.Length; i++)
            _components[i].componentUI.UpdateUI(ComponentUIState.Normal);
    }

    public void CancelConnection()
    {
        _firstSelectedComponent = null;
        _lastSelectedComponent = null;
        
        // Update ui.
        for (int i = 0; i < _components.Length; i++)
            _components[i].componentUI.UpdateUI(ComponentUIState.Normal);
    }
    
    public void DisconnectAllFromComponent(ComponentController component)
    {
        for (int i = 0; i < component.connectedComponentsList.Count; i++)
        {
            if (component.connectedComponentsList[i].connectedComponentsList.Contains(component))
                component.connectedComponentsList[i].connectedComponentsList.Remove(component);
        }

        component.connectedComponentsList = new List<ComponentController>();

        foreach (var componentUI in _components)
        {
            componentUI.componentUI.UpdateUI();
        }
    }

    #endregion
    
}
