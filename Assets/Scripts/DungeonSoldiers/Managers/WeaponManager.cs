using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Vari�vel com o jogador
    public GameObject Player;
    // Vari�vel com o canvas que indica as balas da pistola
    public GameObject BulletCanvas;
    // Vari�vel com a pistola
    private GameObject Pistol;
    // Vari�vel com a espada
    private GameObject Sword;

    // Esta fun��o � chamada quando o jogo come�a
    private void Awake()
    {
        // Obt�m a pistola
        Pistol = Player.GetComponentInChildren<WeaponAimScript>().gameObject;
        // Obt�m a espada
        Sword = Player.GetComponentInChildren<Espada>().gameObject;
    }

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    void Start()
    {
        // Desativa ambas as armas
        Pistol.SetActive(false);
        Sword.SetActive(false);
        BulletCanvas.SetActive(false);
    }

    // Fun��o para dar a arma ao jogador ap�s o di�logo
    public void giveWeapon()
    {
        // Verifica se o jogador escolheu a pistola
        if (PlayerPrefs.GetString("Weapon") == "Gun")
        {
            // Se escolheu a pistola, a espada ser� removida
            Destroy(Sword);
            // Ativa a pistola e o canvas
            Pistol.SetActive(true);
            BulletCanvas.SetActive(true);
        }
        // Caso o jogador tenha escolhida a espada
        else
        {
            // Destr�i o canvas
            Destroy(BulletCanvas);
            // Destr�i a pistola
            Destroy(Pistol);
            // Ativa a espada
            Sword.SetActive(true);
        }
    }
}
