using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable() //desativar perseguicao
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>(); //ir buscar a Node

        // Do nothing while the ghost is frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            //procurar o a direcao disponivel para mover perto do pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // se a distancia for menor que atual
                // entao esta direcao vai ser a mais perto e escolhida
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance) //
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction); //chamar a funcao SetDirection com a nova direcao
        }
    }

}