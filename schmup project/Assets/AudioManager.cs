using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip playerShootSound, playerHealthSound, playerSpawn, starSound, enemyDie;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerShootSound = Resources.Load<AudioClip>("shoot");
        playerHealthSound = Resources.Load<AudioClip>("healthPickup");
        playerSpawn = Resources.Load<AudioClip>("spawn");
        starSound = Resources.Load<AudioClip>("starPickup");
        enemyDie = Resources.Load<AudioClip>("enemyDie");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shoot":
                audioSrc.PlayOneShot(playerShootSound);
                    break;
            case "healthPickup":
                audioSrc.PlayOneShot(playerHealthSound);
                break;
            case "starPickup":
                audioSrc.PlayOneShot(starSound);
                break;
            case "enemyDie":
                audioSrc.PlayOneShot(enemyDie);
                break;
        }
    }
}
