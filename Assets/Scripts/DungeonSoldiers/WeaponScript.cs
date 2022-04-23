using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    // Posição de onde vamos atirar
    [SerializeField] private Transform barrel;
    // Cadência de tiro
    [SerializeField] private float fireRate;
    // Projétil
    [SerializeField] private GameObject bullet;

    // Animator
    private Animator weaponAnimator;
    // Controle da cadência
    private float fireTimer;

    // Variável com o valor máximo de balas
    public int MaxBullets = 7;
    // Variável com o número atual de balas
    private int Bullets;
    // Variável com o texto a indicar as balas da arma
    public Text bulletsText;
    // Variável que deteta se a arma está a ser recarregada
    public bool Reloading = false;
    // Variável com o tempo para recarregar a arma
    private float endReloading;

    // Variável com o componente "Audio Source"
    private AudioSource _audioSource;
    // Variável com o efeito sonoro de recarregar a arma
    public AudioClip reloadSound;
    // Variável com o efeito sonoro de disparar com munição
    public AudioClip shootAudio;
    // Variável com o efeito sonoro de disparar sem munição
    public AudioClip emptyShootAudio;

    // A função é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Obtêm o componente "Animator"
        weaponAnimator = GetComponent<Animator>();
        // Obtêm o componente "Audio Source"
        _audioSource = GetComponent<AudioSource>();
        // Atualiza o número atual de balas
        Bullets = MaxBullets;
        // Atualiza o texto
        bulletsText.text = Bullets + "/" + MaxBullets;
    }

    // A função é chamada a cada frame
    void Update()
    {
        /* Verifica se a arma precisa de recarregar
         * Caso precise e já tenha passado o tempo de recarregar, está irá recarregar */
        if (Reloading && Time.time > endReloading)
        {
            // Recarrega a arma
            Bullets = MaxBullets;
            // Atualiza o texto
            bulletsText.text = Bullets + "/" + MaxBullets;
            // Atualiza a variável lógica
            Reloading = false;
        }

        /* Função lógica de detecção de input do botão esquerdo do rato
         * Caso seja detetado o input, será verificado se é possível disparar */
        if (Input.GetMouseButton(0) && Time.timeScale == 1 && !Reloading && CanShoot())
            // Dispara a arma
            Shoot();

        /* Função lógica de detecção de input da tecla R
         * Caso detete input e seja possível recarregar a arma, esta irá ser recarregada após 1 segundo */
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1 && Bullets < MaxBullets && !Reloading)
        {
            // Define o tempo para recarregar a arma
            endReloading = Time.time + 1;
            // Atualiza o texto
            bulletsText.text = "...";
            // Atualiza a variável lógica
            Reloading = true;
            // Inicia o aúdio de recarregar
            PlayAudio(reloadSound);
        }
    }

    // Função para iniciar um novo aúdio
    private void PlayAudio(AudioClip ac)
    {
        // Para o aúdio a dar
        _audioSource.Stop();
        // Muda a propriedade "Audio Clip" para o novo efeito sonoro
        _audioSource.clip = ac;
        // Inicia o aúdio
        _audioSource.Play();
    }

    // Função para disparar a arma
    private void Shoot()
    {
        // Controlo da cadência
        fireTimer = Time.time + fireRate;

        /* Verifica se a arma possui munição
         * Se possuir, esta irá disparar */
        if (Bullets > 0)
        {
            // Retira uma bala da variável "Bullets"
            Bullets--;
            // Atualiza o texto
            bulletsText.text = Bullets + "/" + MaxBullets;
            // Cria uma bala de acordo com a posição e rotação do cano da arma
            Instantiate(bullet, barrel.position, barrel.rotation);
            // Realiza a animação de tiro
            weaponAnimator.SetTrigger("Fire");
            // Inicia o aúdio de disparar com munição
            PlayAudio(shootAudio);
        } else
            /* Caso contrário, irá indicar ao utilizador que
             * a arma está sem munição com um efeito sonoro */
            PlayAudio(emptyShootAudio);
    }

    /* Função para fazer o controle da cadência
     * (Teste para verificar se é possivel atirar novamente) */
    private bool CanShoot()
    {
        return Time.time > fireTimer;
    }
}