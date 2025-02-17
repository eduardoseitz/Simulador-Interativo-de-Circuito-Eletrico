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
    public GameObject groundCableLineRendererPrefab;
    [SerializeField] private AudioSource electricAudioSource;
    [SerializeField] private AudioSource completeAudioSource;
    
    private Dictionary<Tuple<ComponentController, ComponentController>, Tuple<LineRenderer,LineRenderer>> _cableConnectionDictionary;
        
    private bool _isCircuitValid; //
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

        // Pega referencias e reseta bibliotecas e listas.
        _components = GetComponentsInChildren<ComponentController>();
        _cableConnectionDictionary = new Dictionary<Tuple<ComponentController, ComponentController>, Tuple<LineRenderer, LineRenderer>>();
    }

    private void Start()
    {
        UpdateCircuitState();
    }

    #endregion

    #region Funcoes Ajudantes

    public void UpdateCircuitState()
    {
        // Corta a energia de todos os componentes.
        foreach (ComponentController _component in _components)
            _component.hasPower = false;
        
        // Alimenta todos os dispositivos conectador a uma fonte.
        bool _doesAnyPowerHaveConnection = false;
        foreach (ComponentController _component in _components)
        {
            if (_component.GetComponent<PowerComponentController>())
            {
                _component.hasPower = true;
                if (_component.connectedComponentsList.Count > 0)
                    _doesAnyPowerHaveConnection = true;
                for (int _i = 0; _i < _component.connectedComponentsList.Count; _i++)
                {
                    if (_component.connectedComponentsList[_i] != _component)
                    {
                        _component.connectedComponentsList[_i].hasPower = true;
                        if (_component.connectedComponentsList[_i].isPassingPower)
                        {
                            _component.connectedComponentsList[_i].hasPower = true;
                            for (int _j = 0; _j < _component.connectedComponentsList[_i].connectedComponentsList.Count; _j++)
                            {
                                if (_component.connectedComponentsList[_i].connectedComponentsList[_j] != _component.connectedComponentsList[_i])
                                {
                                    _component.connectedComponentsList[_i].connectedComponentsList[_j].hasPower = true;
                                    if (_component.connectedComponentsList[_i].connectedComponentsList[_j].isPassingPower)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        // Update componentes.
        foreach (ComponentController _component in _components)
            _component.UpdateState();
        
        // Checar se circuito complete.
        ValidateCircuit();
        
        // Tocar ou parar som de energia.
        if (_doesAnyPowerHaveConnection && !electricAudioSource.isPlaying)
            electricAudioSource.Play();
        else if (!_doesAnyPowerHaveConnection && electricAudioSource.isPlaying)
            electricAudioSource.Stop();
    }

    public void StartConnection(ComponentController component)
    {
        // Seleciona componente.
        _firstSelectedComponent = component;
        
        // Update ui.
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
        
        // Cria cabo visual.
        DrawCable(_firstSelectedComponent, _lastSelectedComponent);
        
        // Update circuito.
        UpdateCircuitState();
    }

    public void CancelConnection()
    {
        // Deseleciona tudo.
        _firstSelectedComponent = null;
        _lastSelectedComponent = null;
        
        // Update ui.
        for (int i = 0; i < _components.Length; i++)
            _components[i].ComponentUI.UpdateUI(ComponentUIState.Normal);
    }
    
    public void DisconnectComponentFromAllComponents(ComponentController component)
    {
        // Remove cabos visualemnte.
        EraseCable(component);
        
        // Remove coneccoes entre componentes.
        foreach (ComponentController otherComponent in component.connectedComponentsList)
        {
            if (otherComponent.connectedComponentsList.Contains(component))
                otherComponent.connectedComponentsList.Remove(component);
        }
        component.connectedComponentsList = new List<ComponentController>();

        // Update ui.
        foreach (var componentUI in _components)
            componentUI.ComponentUI.UpdateUI();
        
        // Update circuito.
        UpdateCircuitState();
    }
    
    private void DrawCable(ComponentController firstComponent, ComponentController lastComponent)
    {
        if (!_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(firstComponent, _lastSelectedComponent)) && !_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_lastSelectedComponent, firstComponent)))
        {
            // Desenha cabo fase
            LineRenderer _newPowerCableLineRenderer = Instantiate(powerCableLineRendererPrefab, transform).GetComponent<LineRenderer>();
            _newPowerCableLineRenderer.positionCount = 2;
            _newPowerCableLineRenderer.SetPosition(0, firstComponent.powerPole.transform.position);
            _newPowerCableLineRenderer.SetPosition(1, lastComponent.powerPole.transform.position);
            
            // Desenha cabo neutro
            LineRenderer _newGroundCableLineRenderer = Instantiate(groundCableLineRendererPrefab, transform).GetComponent<LineRenderer>();
            _newGroundCableLineRenderer.positionCount = 2;
            _newGroundCableLineRenderer.SetPosition(0, firstComponent.groundPole.transform.position);
            _newGroundCableLineRenderer.SetPosition(1, lastComponent.groundPole.transform.position);

            // Adiciona ambos os cabos a um direcionario de cabos.
            _cableConnectionDictionary.Add(new Tuple<ComponentController, ComponentController>(firstComponent, lastComponent), new Tuple<LineRenderer, LineRenderer>(_newPowerCableLineRenderer, _newGroundCableLineRenderer));
        }
    }
    
    private void EraseCable(ComponentController component)
    {
        foreach (ComponentController _connectedComponent in component.connectedComponentsList)
        {
            if (_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(component, _connectedComponent)))
            {
                // Apaga cabo fase do tela.
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(component, _connectedComponent)].Item1.gameObject);
                
                // Apaga cabo neutro do tela.
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(component, _connectedComponent)].Item2.gameObject);
                
                // Apaga cabos do dicionario.
                _cableConnectionDictionary.Remove(new Tuple<ComponentController, ComponentController>(component, _connectedComponent));
            }
            else if (_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_connectedComponent, component)))
            {
                // Apaga cabo fase do tela.
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(_connectedComponent, component)].Item1.gameObject);
                
                // Apaga cabo neutro do tela.
                Destroy(_cableConnectionDictionary[new Tuple<ComponentController, ComponentController>(_connectedComponent, component)].Item2.gameObject);
                
                // Apaga cabos do dicionario.
                _cableConnectionDictionary.Remove(new Tuple<ComponentController, ComponentController>(_connectedComponent, component));
            }
        }
    }

    private void ValidateCircuit()
    {
        // Checar se valido
        _isCircuitValid =
            ((_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[0], _components[1])) || _cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[1], _components[0])))
             && (_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[1], _components[2])) || _cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[2], _components[1])))
             && (!_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[0], _components[2])) && !_cableConnectionDictionary.ContainsKey(new Tuple<ComponentController, ComponentController>(_components[2], _components[0])))
             && _components[2].hasPower);
        
        // Update ui.
        circuitStateText.text = "Circuito" + ((_isCircuitValid) ? " Completo" : " Incompleto");
        circuitStateText.color = (_isCircuitValid) ? Color.green : Color.red;
        
        // Tocar som de vitoria.
        if (_isCircuitValid)
            completeAudioSource.Play();
    }

    #endregion
    
}
