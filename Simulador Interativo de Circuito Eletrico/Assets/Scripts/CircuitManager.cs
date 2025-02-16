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
