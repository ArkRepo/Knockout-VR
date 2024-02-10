using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 3;
    private float _currentHealth;
    [SerializeField] private Healthbar _healthbar;
   public AudioSource audio;
    public AudioSource defeat;
    public GameObject gl;
    public GameObject gr;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {



        if (_healthbar != null)
        {
            _currentHealth = _maxHealth;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
       
        defeat = GetComponent<AudioSource>();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // healthbar = collision.gameObject.GetComponent<Healthbar>();
            audio = GetComponent<AudioSource>();

           
            TakeDamage();
            Debug.Log("collison");
            //_currentHealth = Random.Range(0.5f, 1.5f);
            //healthbar.UpdateHealthBar(_currentHealth, _currentHealth);


        }


    }

    void TakeDamage()
    {


        if (_healthbar != null)
        {
            _currentHealth -= Random.Range(0.5f, 1.5f);
            if (_currentHealth <= 0)
            {
                defeat.Play();
                Destroy(gameObject);
                Destroy(gl);
                Destroy(gr);
                enemy.playVictory();

                

                
            }
            else
            {
                audio.Play();
                _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
                
            }


        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
