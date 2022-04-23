using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    // Posi��o de onde vamos atirar
    [SerializeField] private Transform barrel;
    // Cad�ncia de tiro
    [SerializeField] private float fireRate;
    // Proj�til
    [SerializeField] private GameObject bullet;

    // Animator
    private Animator weaponAnimator;
    // Controle da cad�ncia
    private float fireTimer;

    // Vari�vel com o valor m�ximo de balas
    public int MaxBullets = 7;
    // Vari�vel com o n�mero atual de balas
    private int Bullets;
    // Vari�vel com o texto a indicar as balas da arma
    public Text bulletsText;
    // Vari�vel que deteta se a arma est� a ser recarregada
    public bool Reloading = false;
    // Vari�vel com o tempo para recarregar a arma
    private float endReloading;

    // Vari�vel com o componente "Audio Source"
    private AudioSource _audioSource;
    // Vari�vel com o efeito sonoro de recarregar a arma
    public AudioClip reloadSound;
    // Vari�vel com o efeito sonoro de disparar com muni��o
    public AudioClip shootAudio;
    // Vari�vel com o efeito sonoro de disparar sem muni��o
    public AudioClip emptyShootAudio;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    void Start()
    {
        // Obt�m o componente "Animator"
        weaponAnimator = GetComponent<Animator>();
        // Obt�m o componente "Audio Source"
        _audioSource = GetComponent<AudioSource>();
        // Atualiza o n�mero atual de balas
        Bullets = MaxBullets;
        // Atualiza o texto
        bulletsText.text = Bullets + "/" + MaxBullets;
    }

    // A fun��o � chamada a cada frame
    void Update()
    {
        /* Verifica se a arma precisa de recarregar
         * Caso precise e j� tenha passado o tempo de recarregar, est� ir� recarregar */
        if (Reloading && Time.time > endReloading)
        {
            // Recarrega a arma
            Bullets = MaxBullets;
            // Atualiza o texto
            bulletsText.text = Bullets + "/" + MaxBullets;
            // Atualiza a vari�vel l�gica
            Reloading = false;
        }

        /* Fun��o l�gica de detec��o de input do bot�o esquerdo do rato
         * Caso seja detetado o input, ser� verificado se � poss�vel disparar */
        if (Input.GetMouseButton(0) && Time.timeScale == 1 && !Reloading && CanShoot())
            // Dispara a arma
            Shoot();

        /* Fun��o l�gica de detec��o de input da tecla R
         * Caso detete input e seja poss�vel recarregar a arma, esta ir� ser recarregada ap�s 1 segundo */
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1 && Bullets < MaxBullets && !Reloading)
        {
            // Define o tempo para recarregar a arma
            endReloading = Time.time + 1;
            // Atualiza o texto
            bulletsText.text = "...";
            // Atualiza a vari�vel l�gica
            Reloading = true;
            // Inicia o a�dio de recarregar
            PlayAudio(reloadSound);
        }
    }

    // Fun��o para iniciar um novo a�dio
    private void PlayAudio(AudioClip ac)
    {
        // Para o a�dio a dar
        _audioSource.Stop();
        // Muda a propriedade "Audio Clip" para o novo efeito sonoro
        _audioSource.clip = ac;
        // Inicia o a�dio
        _audioSource.Play();
    }

    // Fun��o para disparar a arma
    private void Shoot()
    {
        // Controlo da cad�ncia
        fireTimer = Time.time + fireRate;

        /* Verifica se a arma possui muni��o
         * Se possuir, esta ir� disparar */
        if (Bullets > 0)
        {
            // Retira uma bala da vari�vel "Bullets"
            Bullets--;
            // Atualiza o texto
            bulletsText.text = Bullets + "/" + MaxBullets;
            // Cria uma bala de acordo com a posi��o e rota��o do cano da arma
            Instantiate(bullet, barrel.position, barrel.rotation);
            // Realiza a anima��o de tiro
            weaponAnimator.SetTrigger("Fire");
            // Inicia o a�dio de disparar com muni��o
            PlayAudio(shootAudio);
        } else
            /* Caso contr�rio, ir� indicar ao utilizador que
             * a arma est� sem muni��o com um efeito sonoro */
            PlayAudio(emptyShootAudio);
    }

    /* Fun��o para fazer o controle da cad�ncia
     * (Teste para verificar se � possivel atirar novamente) */
    private bool CanShoot()
    {
        return Time.time > fireTimer;
    }
}