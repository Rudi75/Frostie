using UnityEngine;
using System.Collections;

public class FrostieSoundManager : MonoBehaviour 
{
    public AudioClip Jump;
    public AudioClip Walk;
    private bool isWalking;

    public AudioClip Melting;
    public AudioClip FreezeGround;

    public AudioClip Death;
    public AudioClip DeathInFire;

    public void playJumpSound()
    {
        if (audio.isPlaying)
        {
            if (audio.clip.name.Equals(Jump.name))
                return;
            else
                audio.Stop();
        }
        audio.PlayOneShot(Jump);
      }

    public void playWalkingSound(float movement)
    {
        if (Mathf.Abs(movement) >= 0.1)
        {
            if (!isWalking)
            {
                audio.clip = Walk;
				audio.loop = true;
                audio.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking && audio.clip.name.Equals(Walk.name))
            {
                audio.Stop();
            }
			audio.loop = false;
            isWalking = false;
        } 
    }

    public void playMeltingSound()
    {
        if (audio.isPlaying)
        {
            if (audio.clip.name.Equals(Melting.name))
                return;
            else
                audio.Stop();
        }
		audio.PlayOneShot(Melting);
    }

    public void playFreezingGroundSound()
    {
        if (audio.isPlaying)
        {
            if (audio.clip.name.Equals(FreezeGround.name))
                return;
            else
                audio.Stop();
        }
        audio.PlayOneShot(FreezeGround);
    }

    public void playDeathSound()
    {
		isWalking = false;
		audio.Stop();
        audio.PlayOneShot(Death);
    }

    public void playDeathInFireSound()
    {
		isWalking = false;
		audio.Stop();
        audio.PlayOneShot(DeathInFire);
    }
}
