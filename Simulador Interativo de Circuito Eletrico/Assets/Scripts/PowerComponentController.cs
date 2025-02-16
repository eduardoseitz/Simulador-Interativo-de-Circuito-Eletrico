using UnityEngine;

public class PowerComponentController : ComponentController
{
    
    #region Funcoes ComponentController
    
    private void Start()
    {
        base.Start();
        base.hasPower = true;
    }
    
    public override void UpdateState()
    {
    }
    
    #endregion
    
}
