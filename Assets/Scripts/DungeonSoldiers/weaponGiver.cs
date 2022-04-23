using UnityEngine;

public class weaponGiver : MonoBehaviour
{
    // Variável com o "weaponManager"
    public WeaponManager wManager;

    // Deteta se algum objeto entrou em colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Se for, o jogador irá receber a sua arma
            wManager.giveWeapon();
            // Destrói o objeto
            Destroy(gameObject);
        }
    }
}
