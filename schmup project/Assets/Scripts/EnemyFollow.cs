using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float shootingRange;
    private Transform player;
    public Light2D objectLight;
    float secondTime;
    float timeDelay;
    //ENEMY SHOOTING FIELDS
    //May or may not implement this
    //public float lineOfSite;
    //public float fireRate = 1.5f;
    //private float nextFireTime;
    //public GameObject bulletFire;
    //public GameObject bulletParent;


    // Start is called before the first frame update
    void Start()
    {
        secondTime = 0f;
        timeDelay = .5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //objectLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        #region Unfinished enemy shooting code
        /*if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            // moves toward player if close enough
            // should move this outside of the if statement
            // and make it SHOOT here instead of move
        }*/
        /*if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bulletFire, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }*/
        #endregion

        // move towards player
        if (gameObject.tag == "Square" || gameObject.tag == "Circle")
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (gameObject.tag == "Square")
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime, Space.Self); //rotates 50 degrees per second around z axis
        }
        else if (gameObject.tag == "healthPack" || gameObject.tag == "item")
        {
            transform.Translate(new Vector2(0f, -1f) * speed * Time.deltaTime);

            // create the rainbow effect on the star item
            if (gameObject.tag == "item")
            {
                secondTime = secondTime + 1f * Time.deltaTime;
                if (secondTime >= timeDelay)
                {
                    secondTime = 0f;
                    Color newColor = new Color(Random.value,
                                          Random.value,
                                          Random.value);
                    objectLight.color = newColor;
                    objectLight.intensity = 7;

                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
