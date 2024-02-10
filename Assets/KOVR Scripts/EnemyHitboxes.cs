using UnityEngine;

public class EnemyHitboxes : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    private float _currentHealth;
    [SerializeField] private Healthbar _healthbar;
    bool isHit = false;
    public Enemy enemyScript;
    public AudioSource ring;
    public float minHitSpeed = 2.0f; // Minimum speed for a hit to be registered

    private void Start()
    {
        if (_healthbar != null)
        {
            _currentHealth = _maxHealth;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            HitGloves glovesController = other.GetComponent<HitGloves>();
            if (glovesController != null)
            {
                float gloveSpeed = glovesController.GetSpeed();
                Debug.Log("Glove Speed: " + gloveSpeed);
                // Check if the glove's speed exceeds the minimum hit speed
                if (gloveSpeed >= minHitSpeed)
                {
                    TakeDamage();
                    Debug.Log("Collision with enemy");
                }
                else
                {
                    Debug.Log("Glove speed below minHitSpeed");
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (_healthbar != null)
        {
            isHit = true;
            _currentHealth -= Random.Range(0.5f, 1.5f);

            if (_currentHealth <= 0)
            {
                enemyScript.Stopanima();
                enemyScript.KnockoutAnimation();
                ring.Play();
            }
            else
            {
                _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
                enemyScript.audio.Play();
                enemyScript.PlayHitAnimation();
            }
            isHit = false;
        }
    }
}
