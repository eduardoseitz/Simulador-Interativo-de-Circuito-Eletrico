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
        base.ComponentUI.UpdateLabel((base.isOn) ? " Ligada" : " Desligada");
    }
    
    #endregion
    
}
