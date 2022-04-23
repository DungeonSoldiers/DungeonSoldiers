using UnityEngine;

public class weaponGiver : MonoBehaviour
{
    // Vari�vel com o "weaponManager"
    public WeaponManager wManager;

    // Deteta se algum objeto entrou em colis�o
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se esse objeto � o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Se for, o jogador ir� receber a sua arma
            wManager.giveWeapon();
            // Destr�i o objeto
            Destroy(gameObject);
        }
    }
}
