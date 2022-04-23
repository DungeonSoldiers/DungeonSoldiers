using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variável com o "Player"
    public Transform lookAt;
    // Variáveis com os valores da distância máxima a que o "Player" pode estar do centro da câmera até que está atualize a sua posição
    public float boundX;
    public float boundY;

    // Função executada após todas as funções Update()
    void LateUpdate()
    {
        // Variável do tipo "Vector3" com o valor predefinido de (0, 0, 0)
        Vector3 delta = Vector3.zero;

        // Determina a distancia entre o "Player" e o centro da tela (Câmera)
        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        // Verifica se o "Player" ultrapassou a distância máxima no eixo X
        if (deltaX > boundX || deltaX < -boundX)
            /* Caso ele tenha ultrapassado, será feito um teste lógico para
             * verificar se ele ultrapassou no lado esquerdo ou direito.
             * 
             * Dependendo do lado ultrapassado, a variável "delta" será atualizada
             * com o novo valor do eixo x para a câmera se posicionar corretamente */
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;

        // Verifica se o "Player" ultrapassou a distância máxima no eixo Y
        if (deltaY > boundY || deltaY < -boundY)
            /* Caso ele tenha ultrapassado, será feito um teste lógico para
             * verificar se ele ultrapassou para cima ou para baixo
             * 
             * Dependendo do lado ultrapassado, a variável "delta" será atualizada
             * com o novo valor do eixo y para a câmera se posicionar corretamente */
            if (transform.position.y < lookAt.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;

        // Aplica a nova posição da câmera caso esta tenha que ser atualizada
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}