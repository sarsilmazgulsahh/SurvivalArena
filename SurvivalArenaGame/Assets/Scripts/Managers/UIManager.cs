using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Animator anim;
	public AudioClip swordSwing;
	public AudioSource hitSound;

	void Start(){
		hitSound.clip = swordSwing;
	}

	void Update(){
	}

	public void StartAttack () {
		anim.SetBool("isAttacking", true );


	}

	public void StopAttack () {
		anim.SetBool("isAttacking", false );
	}
}