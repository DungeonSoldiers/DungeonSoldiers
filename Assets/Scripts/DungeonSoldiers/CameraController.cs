using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Vari�vel com o "Player"
    public Transform lookAt;
    // Vari�veis com os valores da dist�ncia m�xima a que o "Player" pode estar do centro da c�mera at� que est� atualize a sua posi��o
    public float boundX;
    public float boundY;

    // Fun��o executada ap�s todas as fun��es Update()
    void LateUpdate()
    {
        // Vari�vel do tipo "Vector3" com o valor predefinido de (0, 0, 0)
        Vector3 delta = Vector3.zero;

        // Determina a distancia entre o "Player" e o centro da tela (C�mera)
        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        // Verifica se o "Player" ultrapassou a dist�ncia m�xima no eixo X
        if (deltaX > boundX || deltaX < -boundX)
            /* Caso ele tenha ultrapassado, ser� feito um teste l�gico para
             * verificar se ele ultrapassou no lado esquerdo ou direito.
             * 
             * Dependendo do lado ultrapassado, a vari�vel "delta" ser� atualizada
             * com o novo valor do eixo x para a c�mera se posicionar corretamente */
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;

        // Verifica se o "Player" ultrapassou a dist�ncia m�xima no eixo Y
        if (deltaY > boundY || deltaY < -boundY)
            /* Caso ele tenha ultrapassado, ser� feito um teste l�gico para
             * verificar se ele ultrapassou para cima ou para baixo
             * 
             * Dependendo do lado ultrapassado, a vari�vel "delta" ser� atualizada
             * com o novo valor do eixo y para a c�mera se posicionar corretamente */
            if (transform.position.y < lookAt.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;

        // Aplica a nova posi��o da c�mera caso esta tenha que ser atualizada
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}