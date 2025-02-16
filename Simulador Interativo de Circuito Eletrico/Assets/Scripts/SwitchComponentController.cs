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
        UpdateState();
    }
    
    public override void Interact()
    {
        base.isPassingPower = !base.isPassingPower;
        UpdateState();
        
        // Update circuit.
        base.Interact();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        
        offModel.SetActive(!base.isPassingPower);
        onModel.SetActive(base.isPassingPower);
        base.ComponentUI.UpdateLabel((base.isPassingPower) ? " Ligada" : " Desligada");
    }

    #endregion
    
}
