using System;
using UnityEngine;

public class zoneManager : MonoBehaviour
{
    // Vari�vel com os inimigos das v�rias zonas
    private GameObject[] enemies;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Define um limite para a tabela
        enemies = new GameObject[10];

        // Utiliza um ciclo para obter acesso a todas as zonas
        foreach (Transform child in transform)
            // Verifica se a zona t�m inimigos
            if (child.Find("Enemies"))
            {
                // Se tiver, vamos meter os inimigos na tabela
                enemies[Convert.ToInt32(child.name)] = child.Find("Enemies").gameObject;
                // Desativa os inimigos
                enemies[Convert.ToInt32(child.name)].SetActive(false);
            }
    }

    // Fun��o para desbloquear uma zona
    public void ClearBorder(int Indice)
    {
        // Utiliza um ciclo para obter acesso a todas as zonas
        foreach (Transform child in transform)
            // Verifica se a zona � a zona definida em "Indice"
            if (child.name == Indice.ToString())
            {
                // Se for, vamos destruir a borda
                Destroy(child.Find("Fade").gameObject);

                // Verifica se a zona t�m inimigos
                if (enemies[Convert.ToInt32(child.name)])
                    // Se tiver, estes ser�o ativados
                    enemies[Convert.ToInt32(child.name)].SetActive(true);
            }
    }
}
