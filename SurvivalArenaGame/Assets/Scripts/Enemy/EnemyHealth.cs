using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;            
        public int currentHealth;                  
        public float sinkSpeed = 2.5f;             
        public int scoreValue = 10;                 
        public AudioClip deathClip;                 
		public int DamageCount = 10;

        Animator anim;                              
        AudioSource enemyAudio;                     
        ParticleSystem hitParticles;               
        CapsuleCollider capsuleCollider;            
        bool isDead;                                
        bool isSinking;

        void Awake ()
        {
           
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            
            currentHealth = startingHealth;
        }


        void Update ()
        {
			if(isSinking)
            {
                
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }

        }



		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag == "sword"){

				currentHealth -= DamageCount;
			}
			if(currentHealth <= 0)
			{

				Death ();
				StartCoroutine(WaitForAnimationToPlay()); 
			}
		}



		IEnumerator WaitForAnimationToPlay()
		{
			yield return new WaitForSeconds(1);
			StartSinking();
		}

        void Death ()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger ("Dead");
			anim.SetBool("isDead", true );
            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent <NavMeshAgent> ().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }
    }
}