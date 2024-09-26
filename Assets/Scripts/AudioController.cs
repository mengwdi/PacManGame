using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource collidesWithWall;
    public AudioSource dead;
    public AudioSource deadGhost;
    public AudioSource hitHurt;
    public AudioSource intro;
    public AudioSource pickupCoin;
    public AudioSource scare;
    public AudioSource walk;

    public void PlayCollidesWithWall()
    {
        collidesWithWall.Play();
    }

    public void PlayDead()
    {
        dead.Play();
    }

    public void PlayDeadGhost()
    {
        deadGhost.Play();
    }

    public void PlayHitHurt()
    {
        hitHurt.Play();
    }

    public void PlayIntro()
    {
        intro.Play();
    }

    public void PlayPickupCoin()
    {
        pickupCoin.Play();
    }

    public void PlayScare()
    {
        scare.Play();
    }

    public void PlayWalk()
    {
        walk.Play();
    }
}

}
