using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Variável com o jogador
    public GameObject Player;
    // Variável com o canvas que indica as balas da pistola
    public GameObject BulletCanvas;
    // Variável com a pistola
    private GameObject Pistol;
    // Variável com a espada
    private GameObject Sword;

    // Esta função é chamada quando o jogo começa
    private void Awake()
    {
        // Obtêm a pistola
        Pistol = Player.GetComponentInChildren<WeaponAimScript>().gameObject;
        // Obtêm a espada
        Sword = Player.GetComponentInChildren<Espada>().gameObject;
    }

    // A função é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Desativa ambas as armas
        Pistol.SetActive(false);
        Sword.SetActive(false);
        BulletCanvas.SetActive(false);
    }

    // Função para dar a arma ao jogador após o diálogo
    public void giveWeapon()
    {
        // Verifica se o jogador escolheu a pistola
        if (PlayerPrefs.GetString("Weapon") == "Gun")
        {
            // Se escolheu a pistola, a espada será removida
            Destroy(Sword);
            // Ativa a pistola e o canvas
            Pistol.SetActive(true);
            BulletCanvas.SetActive(true);
        }
        // Caso o jogador tenha escolhida a espada
        else
        {
            // Destrói o canvas
            Destroy(BulletCanvas);
            // Destrói a pistola
            Destroy(Pistol);
            // Ativa a espada
            Sword.SetActive(true);
        }
    }
}
