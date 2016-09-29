using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     
        public int attackDamage = 10;               


        Animator anim;                              
        GameObject player;                          
        PlayerHealth playerHealth;                 
        EnemyHealth enemyHealth;                   
        bool playerInRange;                         
        float timer;                                


        void Awake ()
        {
          
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
        }


        void OnTriggerEnter (Collider other)
        {
           
            if(other.gameObject == player)
            {
                
                playerInRange = true;
            }
        }


        void OnTriggerExit (Collider other)
        {
            
            if(other.gameObject == player)
            {
               
                playerInRange = false;
            }
        }

		IEnumerator WaitForAnimationToPlay()
		{
			GetComponent<AudioSource>().PlayOneShot(swordSwing, 0.99f);
			yield return new WaitForSeconds(3);
		}

		public AudioClip swordSwing;

        void Update ()
        {
            
            timer += Time.deltaTime;

            
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                
                Attack ();
				anim.SetBool("isAttacking", true );

            } else {
				anim.SetBool("isAttacking", false );
			}
        }


        void Attack ()
        {
            
            timer = 0f;

            
            if(playerHealth.currentHealth > 0)
            {
				StartCoroutine(WaitForAnimationToPlay());
                playerHealth.TakeDamage (attackDamage);
            }
        }
    }
}