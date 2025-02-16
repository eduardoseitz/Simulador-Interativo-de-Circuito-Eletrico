using UnityEngine;

public class LedComponentController : ComponentController
{

    #region Declaracoes

    [SerializeField] private GameObject offModel;
    [SerializeField] private GameObject onModel;
    
    #endregion
    
    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        UpdateState();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        
        // Liga ou desliga led.
        offModel.SetActive(!base.hasPower);
        onModel.SetActive(base.hasPower);
        base.ComponentUI.UpdateLabel((base.hasPower) ? " Ligado" : " Desligado");
    }

    #endregion
    
}
