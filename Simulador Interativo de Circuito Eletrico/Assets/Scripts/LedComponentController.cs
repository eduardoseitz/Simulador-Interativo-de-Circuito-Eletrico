using UnityEngine;

public class LedComponentController : ComponentController
{
    
    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        base.componentUI.UpdateUI((base.isOn) ? " Ligada" : " Desligada");
    }
    
    #endregion
    
}
