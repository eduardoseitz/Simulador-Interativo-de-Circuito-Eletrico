using TMPro;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    
    #region Declaracoes
    
    [Header("Propriedades do componente")]
    [SerializeField] internal string label = "Componente";
    [SerializeField] internal TextMeshProUGUI labelText;
    
    internal void Start()
    {
        labelText.text = label;
    }
    
    #endregion

    #region Funcoes Ajudantes
    
    public virtual void UseComponent()
    {
        
    }
    
    #endregion
    
}
