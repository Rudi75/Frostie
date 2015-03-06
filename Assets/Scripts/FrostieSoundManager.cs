using UnityEngine;
using System.Collections;

public class FrostieSoundManager : MonoBehaviour 
{
    public AudioClip Jump;
    public AudioClip Walk;
    private bool isWalking;

    public AudioClip Melting;

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
                audio.PlayOneShot(Walk);
                isWalking = true;
            }
        }
        else
        {
            isWalking = false;
            if (isWalking && audio.clip.name.Equals(Walk.name))
            {
                audio.Stop();
            }
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

    public void playDeathSound()
    {
        if (audio.isPlaying)
        {
            if (audio.clip.name.Equals(Death.name))
                return;
            else
                audio.Stop();
        }
        audio.PlayOneShot(Death);
    }

    public void playDeathInFireSound()
    {
        if (audio.isPlaying)
        {
            if (audio.clip.name.Equals(DeathInFire.name))
                return;
            else
                audio.Stop();
        }
        audio.loop = true;
        audio.PlayOneShot(DeathInFire);
    }
}
