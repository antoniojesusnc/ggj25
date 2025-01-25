using ggj25;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static ShieldController Instance { get; private set; }

    [SerializeField] private Transform shield; // Referencia al jugador (personaje principal)
    [SerializeField] private float shieldDistance = 1.5f;
    private HeroController _heroController;
    private HeroInput _heroInput;

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
        _heroInput = GetComponentInParent<HeroInput>();
    }

    void Update()
    {
        Vector3 pointPosition = GetMousePosition();
        if (_heroInput != null)
        {
            pointPosition = _heroInput.ShieldPosition;
        }
        
        
        // Calcula la direcci�n desde la posici�n del jugador hacia el mouse
        Vector3 direction = (pointPosition - transform.position).normalized;

        // Calcula el �ngulo de rotaci�n basado en la direcci�n
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calcula la nueva posici�n del escudo en base al �ngulo y la distancia
        Vector3 shieldPosition = transform.position + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * _heroController?.Config?.ShieldDistance ?? shieldDistance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * _heroController?.Config?.ShieldDistance ?? shieldDistance,
            0
        );
        // Actualiza la posici�n del escudo

        // Aplica la rotaci�n para que el escudo apunte hacia el rat�n
        shield.SetPositionAndRotation(shieldPosition, Quaternion.Euler(0, 0, angle));

    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aseg�rate de que est� en 2D (sin profundidad)

        return mousePosition;
    }

}