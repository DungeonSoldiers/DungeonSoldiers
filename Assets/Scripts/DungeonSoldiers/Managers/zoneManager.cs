using System;
using UnityEngine;

public class zoneManager : MonoBehaviour
{
    // Variável com os inimigos das várias zonas
    private GameObject[] enemies;

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Define um limite para a tabela
        enemies = new GameObject[10];

        // Utiliza um ciclo para obter acesso a todas as zonas
        foreach (Transform child in transform)
            // Verifica se a zona têm inimigos
            if (child.Find("Enemies"))
            {
                // Se tiver, vamos meter os inimigos na tabela
                enemies[Convert.ToInt32(child.name)] = child.Find("Enemies").gameObject;
                // Desativa os inimigos
                enemies[Convert.ToInt32(child.name)].SetActive(false);
            }
    }

    // Função para desbloquear uma zona
    public void ClearBorder(int Indice)
    {
        // Utiliza um ciclo para obter acesso a todas as zonas
        foreach (Transform child in transform)
            // Verifica se a zona é a zona definida em "Indice"
            if (child.name == Indice.ToString())
            {
                // Se for, vamos destruir a borda
                Destroy(child.Find("Fade").gameObject);

                // Verifica se a zona têm inimigos
                if (enemies[Convert.ToInt32(child.name)])
                    // Se tiver, estes serão ativados
                    enemies[Convert.ToInt32(child.name)].SetActive(true);
            }
    }
}
