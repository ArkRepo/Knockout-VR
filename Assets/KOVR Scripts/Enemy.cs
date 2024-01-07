using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   [SerializeField] private float _maxHealth = 3;
     private float _currentHealth ;
    [SerializeField] private Healthbar _healthbar;
    // Start is called before the first frame update
    void Start()
    {
      


        if (_healthbar != null)
        {
            _currentHealth = _maxHealth;
            _healthbar.UpdateHealthBar(_currentHealth, _currentHealth);
        }

       
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.CompareTag("Hands"))
        {
            // healthbar = collision.gameObject.GetComponent<Healthbar>();
            
                

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
                Destroy(gameObject);
            }
            else
            {
                _healthbar.UpdateHealthBar(_currentHealth, _currentHealth);
            }
            
           
        }

    }      
    
    // Update is called once per frame
        void Update()
    {
        
    }
}
