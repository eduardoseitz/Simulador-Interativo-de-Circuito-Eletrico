using UnityEngine;

public class SwitchComponentController : ComponentController
{
    
    #region Declaracoes
    
    [Header("Propriedades especificas do componente")]
    [SerializeField] private GameObject offModel;
    [SerializeField] private GameObject onModel;
    
    #endregion

    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        base.componentUI.UpdateUI((base.isOn) ? "Ligada" : "Desligada");
    }
    
    public override void Interact()
    {
        base.Interact();
        
        // Liga ou desliga chave.
        base.isOn = !base.isOn;
        offModel.SetActive(!base.isOn);
        onModel.SetActive(base.isOn);
        base.componentUI.UpdateUI((base.isOn) ? "Ligada" : "Desligada");
        CircuitManager.instance.UpdateCircuitState();
    }

    #endregion
    
}
