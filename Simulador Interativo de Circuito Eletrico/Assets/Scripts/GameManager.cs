using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    #region Declaracoes

    public static GameManager instance;

    #endregion

    #region Funcoes MonoBehaviour

    private void Awake()
    {
        // Padrao singleton.
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    #endregion

    #region Funcoes Ajudantes

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
    
}
