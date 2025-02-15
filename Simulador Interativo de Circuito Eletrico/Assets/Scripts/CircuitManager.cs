using TMPro;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    
    #region Declaracoes

    public static CircuitManager instance;
    
    [Header("Propriedade do circuito")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshProUGUI circuitStateText;

    private Ray _cameraRay;
    private RaycastHit _raycastHit;
    private ComponentController _selectedComponent;
    private bool _isCircuitComplete;

    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        // Padrao singleton.
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        UpdateCircuitState();
    }

    private void Update()
    {
        // Checa cliques do mouse esquerdo.
        if (Input.GetMouseButtonDown(0))
        {
            _cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_cameraRay, out _raycastHit, 100))
            {
                _selectedComponent = _raycastHit.collider.GetComponent<ComponentController>();
                if (_selectedComponent)
                {
                    // TODO:
                }
            }
        }
        
        // Checa cliques do mouse direito.
        if (Input.GetMouseButtonDown(1))
        {
            _cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_cameraRay, out _raycastHit, 100))
            {
                _selectedComponent = _raycastHit.collider.GetComponent<ComponentController>();
                if (_selectedComponent)
                    _selectedComponent.UseComponent();
            }
        }
    }

    #endregion

    #region Funcoes Ajudantes

    public void UpdateCircuitState()
    {
        circuitStateText.text = "Circuito" + ((_isCircuitComplete) ? " Completo" : " Incompleto");
        circuitStateText.color = (_isCircuitComplete) ? Color.green : Color.red;
    }

    #endregion
    
}
