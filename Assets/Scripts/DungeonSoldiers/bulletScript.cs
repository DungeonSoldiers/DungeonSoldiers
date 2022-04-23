using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Variável de controle do tempo de duração do projétil para que seja destruída
    [SerializeField] private float lifeTime;
    // Variável de velocidade do projétil
    [SerializeField] private float bulletSpeed;

    // A função é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Destruir o projétil
        Destroy(gameObject, lifeTime);
    }

    // A função é chamada a cada frame
    void Update()
    {
        // Atira na posição que a arma está apontada
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }
}