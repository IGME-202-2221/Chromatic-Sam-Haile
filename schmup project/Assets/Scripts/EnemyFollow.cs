using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    //public float lineOfSite;
    public float shootingRange;
    //public float fireRate = 1.5f;
    //private float nextFireTime;
    //public GameObject bulletFire;
    //public GameObject bulletParent;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
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
        // move towards player
        if (gameObject.tag == "Square")
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime, Space.Self); //rotates 50 degrees per second around z axis
        }
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
}
