using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Variável com a próxima zona
    public int Scene;
    // Variável com o "zoneManager"
    public zoneManager zManager;

    // A função é chamada a cada frame
    void Update()
    {
        // Verifica se não existe inimigos na zona
        if (transform.childCount > 0)
            /* Caso exista, iremos utilizar um ciclo para
             * verificar se existem inimigos vivos */
            foreach (Transform child in transform)
                // Verifica se o inimigo está vivo
                if (!child.GetComponent<MorteAnimacao>().enabled)
                    // Se estiver, a função será avançada
                    return;

        // Caso os inimigos estejam todos mortos, a próxima zona será desbloqueada
        zManager.ClearBorder(Scene);
        // Desativa o "script"
        enabled = false;
    }
}
