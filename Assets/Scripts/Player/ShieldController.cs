using ggj25;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static ShieldController Instance { get; private set; }

    [SerializeField] private Transform shield; // Referencia al jugador (personaje principal)
    private HeroController _heroController;

    private void Awake()
    {
        // Configurar Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destruir el objeto si ya existe una instancia
            return;
        }
        Instance = this;

        _heroController = GetComponentInParent<HeroController>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aseg�rate de que est� en 2D (sin profundidad)

        // Calcula la direcci�n desde la posici�n del jugador hacia el mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calcula el �ngulo de rotaci�n basado en la direcci�n
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calcula la nueva posici�n del escudo en base al �ngulo y la distancia
        Vector3 shieldPosition = transform.position + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * _heroController.Config.ShieldDistance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * _heroController.Config.ShieldDistance,
            0
        );
        // Actualiza la posici�n del escudo

        // Aplica la rotaci�n para que el escudo apunte hacia el rat�n
        shield.SetPositionAndRotation(shieldPosition, Quaternion.Euler(0, 0, angle));

    }

    //player collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Escudo detecta colisi�n con proyectiles
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Player bloque� el proyectil: " + collision.gameObject.name);
            //Destroy(collision.gameObject); // Destruir proyectil al impactar
        }
    }

    //player trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar colisiones con triggers
        if (other.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Trigger en el escudo: " + other.gameObject.name);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger en el jugador: " + other.gameObject.name);
        }
        else
        {
            Debug.Log("Trigger desconocido: " + other.gameObject.name);
        }
    }



}