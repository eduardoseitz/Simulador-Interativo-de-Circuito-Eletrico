using System;
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
    public GameObject powerCableLineRendererPrefab;
    
    private Dictionary<Tuple<ComponentController, ComponentController>, LineRenderer> _cableConnectionDictionary;
        
    private bool _isCircuitComplete; //
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
        _cableConnectionDictionary = new Dictionary<Tuple<ComponentController, ComponentController>, LineRenderer>();
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
                _components[i].ComponentUI.UpdateUI(ComponentUIState.FirstConnection);
            else
                _components[i].ComponentUI.UpdateUI(ComponentUIState.LastConnection);
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
            _components[i].ComponentUI.UpdateUI(ComponentUIState.Normal);
        
        // Create cable.
        DrawCable(_firstSelectedComponent, _lastSelectedComponent);
    }

    public void CancelConnection()
    {
        _firstSelectedComponent = null;
        _lastSelectedComponent = null;
        
        // Update ui.
        for (int i = 0; i < _components.Length; i++)
            _components[i].ComponentUI.UpdateUI(ComponentUIState.Normal);
    }
    
    public void DisconnectComponentFromAllComponents(ComponentController component)
    {
        // Remove line cables.
        EraseCable(component);
        
        // Remove connections.
        foreach (ComponentController otherComponent in component.connectedComponentsList)
        {
            if (otherComponent.connectedComponentsList.Contains(component))
                otherComponent.connectedComponentsList.Remove(component);
        }
        component.connectedComponentsList = new List<ComponentController>();

        // Update ui.
        foreach (var componentUI in _components)
            componentUI.ComponentUI.UpdateUI();
    }
    
    private void DrawCable(ComponentController firstComponent, ComponentController lastComponent)
    {
        if (!_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(firstComponent, _lastSelectedComponent)) && !_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_lastSelectedComponent, firstComponent)))
        {
            LineRenderer _newPowerCableLineRenderer = Instantiate(powerCableLineRendererPrefab, transform).GetComponent<LineRenderer>();
            _newPowerCableLineRenderer.positionCount = 2;
            _newPowerCableLineRenderer.SetPosition(0, firstComponent.powerPole.transform.position);
            _newPowerCableLineRenderer.SetPosition(1, lastComponent.powerPole.transform.position);

            _cableConnectionDictionary.Add(new Tuple<ComponentController, ComponentController>(firstComponent, lastComponent), _newPowerCableLineRenderer);
        }

        Debug.Log(_cableConnectionDictionary.Count);
    }
    
    private void EraseCable(ComponentController component)
    {
        foreach (ComponentController _connectedComponent in component.connectedComponentsList)
        {
            if (_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(component, _connectedComponent)))
            {
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(component, _connectedComponent)].gameObject);
                _cableConnectionDictionary.Remove(new Tuple<ComponentController, ComponentController>(component, _connectedComponent));
            }
            else if (_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_connectedComponent, component)))
            {
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(_connectedComponent, component)].gameObject);
                _cableConnectionDictionary.Remove(new Tuple<ComponentController, ComponentController>(_connectedComponent, component));
            }
        }
        
        Debug.Log(_cableConnectionDictionary.Count);
    }

    #endregion
    
}
