using UnityEngine;

public class head : MonoBehaviour
{
    // Variável com o GameController para o controlo do jogo
    public GameController gameController;

    // Método de colisão
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            // Teste de colisão entre a cabeça da cobra e a comida
            case "food":
                /* Caso a cobra coma um objeto, é adicionado um prefab "Tail"
                   ao "Head" */
                gameController.Eat();
                break;
            case "tail":
                // Caso a cobra entre em contacto com o rabo, ela morre
                gameController.gameOver();
                break;
        }    
    }
}
