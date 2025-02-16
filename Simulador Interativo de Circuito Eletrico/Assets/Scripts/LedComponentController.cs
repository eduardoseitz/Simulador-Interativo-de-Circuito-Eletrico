using UnityEngine;

public class LedComponentController : ComponentController
{
    
    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        base.componentUI.UpdateLabel((base.isOn) ? " Ligada" : " Desligada");
    }
    
    #endregion
    
}
