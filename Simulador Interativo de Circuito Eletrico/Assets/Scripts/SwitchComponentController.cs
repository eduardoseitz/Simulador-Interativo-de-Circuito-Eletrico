using UnityEngine;

public class SwitchComponentController : ComponentController
{
    
    #region Declaracoes
    
    [Header("Propriedades especificas do componente")]
    [SerializeField] private GameObject offModel;
    [SerializeField] private GameObject onModel;
    
    private bool _isOn;
    
    #endregion

    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
    }
    
    public override void UseComponent()
    {
        base.UseComponent();
        
        // Liga ou desliga chave.
        _isOn = !_isOn;
        offModel.SetActive(!_isOn);
        onModel.SetActive(_isOn);
        base.labelText.text = base.label + ((_isOn) ? " Ligada" : " Desligada");
        CircuitManager.instance.UpdateCircuitState();
    }
    #endregion
    
}
