using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;                  
        public float timeBetweenBullets = 0.15f;        
        public float range = 100f;                     


        float timer;                                   
        Ray shootRay;                                   
        RaycastHit shootHit;                             
        int shootableMask;                              
        ParticleSystem gunParticles;                    
        LineRenderer gunLine;                           
        AudioSource gunAudio;                           
        Light gunLight;                                
        float effectsDisplayTime = 0.2f;                


        void Awake ()
        {
            
            shootableMask = LayerMask.GetMask ("Shootable");

            
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
        }


        void Update ()
        {
            
            timer += Time.deltaTime;
			if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
			{

				Shoot ();
			}
#if !MOBILE_INPUT
            
		
#else
            
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
              
                Shoot();
            }
#endif
            
            if(timer >= timeBetweenBullets * effectsDisplayTime)
            {
               
                DisableEffects ();
            }
        }


        public void DisableEffects ()
        {
            
            gunLine.enabled = false;
            gunLight.enabled = false;
        }

		void OnCollisionEnter (Collision collision){
			if(collision.gameObject.tag == "sword"){

				Debug.Log("reduced health to: ");
			 } 
		}

        void Shoot ()
        {
           
            timer = 0f;

            
            gunAudio.Play ();

           
            gunLight.enabled = true;

            
            gunParticles.Stop ();
            gunParticles.Play ();

            
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

           
           
        }
    }
}