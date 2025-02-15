using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    [Header("Propriedade do circuito")]
    [SerializeField] private Camera mainCamera;

    private Ray _cameraRay;
    private RaycastHit _raycastHit;
    private ComponentController _selectedComponent;

    void Update()
    {
        // Checa cliques do mouse esquerdo.
        if (Input.GetMouseButtonDown(0))
        {
            _cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_cameraRay, out _raycastHit, 100))
            {
                _selectedComponent = _raycastHit.collider.GetComponent<ComponentController>();
                if (_selectedComponent)
                {
                    // TODO:
                }
            }
        }
        
        // Checa cliques do mouse direito.
        if (Input.GetMouseButtonDown(1))
        {
            _cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_cameraRay, out _raycastHit, 100))
            {
                _selectedComponent = _raycastHit.collider.GetComponent<ComponentController>();
                if (_selectedComponent)
                    _selectedComponent.UseComponent();
            }
        }
    }
}
