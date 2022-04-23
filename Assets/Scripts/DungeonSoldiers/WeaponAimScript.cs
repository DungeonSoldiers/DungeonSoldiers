using UnityEngine;

public class WeaponAimScript : MonoBehaviour
{
    // Variável de referência ao player
    [SerializeField] private Transform player;
    // Variável lógica com o estado da arma (Equipada ou guardada)
    public bool gunEquipped;
    // Variável de referência à arma
    private GameObject gun;

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Obtêm o gameObject da arma
        gun = GetComponentInChildren<WeaponScript>().gameObject;
    }

    // A função é chamada a cada frame
    void Update()
    {
        /* Verifica se o jogo está parado
         * Caso esteja, a função será avançada */
        if (Time.timeScale == 0) return;

        /* Função lógica de detecção de input da tecla T
         * Caso detete input, esta irá equipar ou guardar a arma */
        if (Input.GetKeyDown(KeyCode.T) && !gun.GetComponent<WeaponScript>().Reloading)
        {
            // Muda a variavel lógica
            gunEquipped = !gunEquipped;

            // Verifica se é para equipar a arma
            if (gunEquipped)
            {
                gun.SetActive(true);
                gun.GetComponent<WeaponScript>().bulletsText.gameObject.SetActive(true);
            } else
            {
                gun.SetActive(false);
                gun.GetComponent<WeaponScript>().bulletsText.gameObject.SetActive(false);
            }
        }

        /* Verifica se a arma está equipada para aplicar a rotação
         * Caso esteje, a rotação da arma será atualizada */
        if (gunEquipped)
            HandleAiming();
    }

    // Função para aplicar a rotação à arma de acordo com a posição do cursor
    private void HandleAiming()
    {
        // Obtêm a direção do cursor
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        // Converte a direção do cursor em um ângulo
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Aplica a rotação
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Girar
        Vector3 localScale = Vector3.one;

        /* Esta função lógica serve para guarantir
         * que a arma sempre fique em posição vertical */
        if (angle > 90 || angle < -90)
            localScale.y = -1f;
        else
            localScale.y = 1f;

        // Aplica este feito
        transform.localScale = localScale;
    }
}