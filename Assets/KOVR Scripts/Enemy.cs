using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   
   
    Animator animator;
    public GameObject player;


    public AudioSource audio;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    bool isHit = false;
    public float rotationSpeed = 70f;
    public float lookAtDelay = 30f;
    public float facingThreshold = 0.95f;
    private bool isRotating = false;
    private bool animationStarted = false;
    private bool rotationCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("idle");
        StartCoroutine(LookAtPlayerWithDelay());

    }

    public void PlayHitAnimation()
    {
        
        animator.Play("stomach_hit");


    }

    public void KnockoutAnimation() 
    {

        animator.Play("knocked_out");
    }

    public void Stopanima()
    {
        StopAllCoroutines();

    }

    public void playDamageAudio()
    {
        audio.Play();
    }

    public void playVictory()
    {
        Stopanima();
        animator.Play("victory");
    }

    //void OnCollisionEnter(Collision collision)
    //{
       
    //    if (collision.gameObject.CompareTag("Hands"))
    //    {
    //        // healthbar = collision.gameObject.GetComponent<Healthbar>();

    //            audio.Play();
    //            TakeDamage();
    //            Debug.Log("collison");
    //            //_currentHealth = Random.Range(0.5f, 1.5f);
    //            //healthbar.UpdateHealthBar(_currentHealth, _currentHealth);
                
            
    //    }


    //}

    //void TakeDamage()
    //{
        

    //    if (_healthbar != null)
    //    {
    //        isHit = true;
    //        _currentHealth -= Random.Range(0.5f, 1.5f);
    //        animator.Play("stomach_hit");
    //        if (_currentHealth <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //        else
    //        {
    //            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    //        }
    //        isHit = false;
           
    //    }

    //}

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LookAtPlayerWithDelay());
    }


    IEnumerator LookAtPlayerWithDelay()
    {
        yield return new WaitForSeconds(lookAtDelay);

        while (true)
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Check if the enemy is facing the player
            if (Vector3.Dot(transform.forward, targetDirection.normalized) > facingThreshold)
            {
                if (!isRotating)
                {
                    StartCoroutine(RotateTowardsPlayer(targetRotation));
                }

                if (!animationStarted)
                {
                    StartCoroutine(PlayRandomAnimations());
                    animationStarted = true; // Set the flag to true to indicate that random animations have started
                }
                else if (!rotationCompleted)
                {
                    yield break; // Exit the coroutine if the rotation is not completed and random animations have started
                }
            }

            yield return null;
        }
    }


    IEnumerator RotateTowardsPlayer(Quaternion targetRotation)
    {
        isRotating = true;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false;
        rotationCompleted = true; // Set the flag to true to indicate that rotation has completed
    }


    IEnumerator PlayRandomAnimations()
    {
        while (true)
        {
            if (!isHit)
            {
                yield return new WaitForSeconds(Random.Range(2f, 4f));

                int randomAnimation = Random.Range(0, 4);

                if (randomAnimation == 0)
                {
                    animator.Play("long_punch");
                    audio1.Play();
                }
                else if ((randomAnimation == 1))
                {
                    animator.Play("left_upper");
                    audio2.Play();
                }
                else if (randomAnimation == 2)
                {
                    animator.Play("cross_jab");
                    audio3.Play();
                }
                else
                {
                    animator.Play("right_punch");
                    audio4.Play();
                }
            }
            else yield return new WaitForSeconds(0.5f);
        }
    }
}
