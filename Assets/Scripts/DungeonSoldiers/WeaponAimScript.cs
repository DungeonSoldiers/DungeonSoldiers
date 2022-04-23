using UnityEngine;

public class WeaponAimScript : MonoBehaviour
{
    // Vari�vel de refer�ncia ao player
    [SerializeField] private Transform player;
    // Vari�vel l�gica com o estado da arma (Equipada ou guardada)
    public bool gunEquipped;
    // Vari�vel de refer�ncia � arma
    private GameObject gun;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Obt�m o gameObject da arma
        gun = GetComponentInChildren<WeaponScript>().gameObject;
    }

    // A fun��o � chamada a cada frame
    void Update()
    {
        /* Verifica se o jogo est� parado
         * Caso esteja, a fun��o ser� avan�ada */
        if (Time.timeScale == 0) return;

        /* Fun��o l�gica de detec��o de input da tecla T
         * Caso detete input, esta ir� equipar ou guardar a arma */
        if (Input.GetKeyDown(KeyCode.T) && !gun.GetComponent<WeaponScript>().Reloading)
        {
            // Muda a variavel l�gica
            gunEquipped = !gunEquipped;

            // Verifica se � para equipar a arma
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

        /* Verifica se a arma est� equipada para aplicar a rota��o
         * Caso esteje, a rota��o da arma ser� atualizada */
        if (gunEquipped)
            HandleAiming();
    }

    // Fun��o para aplicar a rota��o � arma de acordo com a posi��o do cursor
    private void HandleAiming()
    {
        // Obt�m a dire��o do cursor
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        // Converte a dire��o do cursor em um �ngulo
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Aplica a rota��o
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Girar
        Vector3 localScale = Vector3.one;

        /* Esta fun��o l�gica serve para guarantir
         * que a arma sempre fique em posi��o vertical */
        if (angle > 90 || angle < -90)
            localScale.y = -1f;
        else
            localScale.y = 1f;

        // Aplica este feito
        transform.localScale = localScale;
    }
}