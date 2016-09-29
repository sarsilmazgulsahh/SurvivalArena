using UnityEngine;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       


        Animator anim;                         


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            
            if(playerHealth.currentHealth <= 0)
            {
               
                anim.SetTrigger ("GameOver");
            }
        }
    }
}