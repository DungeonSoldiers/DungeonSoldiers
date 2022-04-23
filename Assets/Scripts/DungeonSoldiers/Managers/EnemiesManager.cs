using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Vari�vel com a pr�xima zona
    public int Scene;
    // Vari�vel com o "zoneManager"
    public zoneManager zManager;

    // A fun��o � chamada a cada frame
    void Update()
    {
        // Verifica se n�o existe inimigos na zona
        if (transform.childCount > 0)
            /* Caso exista, iremos utilizar um ciclo para
             * verificar se existem inimigos vivos */
            foreach (Transform child in transform)
                // Verifica se o inimigo est� vivo
                if (!child.GetComponent<MorteAnimacao>().enabled)
                    // Se estiver, a fun��o ser� avan�ada
                    return;

        // Caso os inimigos estejam todos mortos, a pr�xima zona ser� desbloqueada
        zManager.ClearBorder(Scene);
        // Desativa o "script"
        enabled = false;
    }
}
