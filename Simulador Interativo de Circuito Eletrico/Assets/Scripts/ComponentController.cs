using TMPro;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    [Header("Propriedades do componente")]
    [SerializeField] internal string label = "Componente";
    [SerializeField] internal TextMeshProUGUI labelText;
    
    internal void Start()
    {
        labelText.text = label;
    }

    public virtual void UseComponent()
    {
        
    }
}
