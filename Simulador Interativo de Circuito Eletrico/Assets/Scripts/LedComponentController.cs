using UnityEngine;

public class LedComponentController : ComponentController
{
    
    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        UpdateUI();
    }
    
    internal override void UpdateUI()
    {
        base.UpdateUI();
        base.labelText.text += ((base.isOn) ? " Ligada" : " Desligada");
    }
    
    #endregion
    
}
