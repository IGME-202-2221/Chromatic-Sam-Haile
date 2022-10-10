using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class RotateAround : MonoBehaviour
{

    public GameObject player;
    private Vector3 v3Pos;
    private float angle;
    private float distance = 0.25f;

    // Bullet fields
    public GameObject greenBullet;
    public GameObject redBullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    private float secondTimer;
    public float timeBetweenFiring;

    public Queue<GameObject> greenBulletQueue = new Queue<GameObject>();
    public Queue<GameObject> redBulletQueue = new Queue<GameObject>();


    public EnemyFollow enemyFollow;

    void Update()
    {
        #region MouseArrow
        v3Pos = Input.mousePosition;
        v3Pos.z = (player.transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - player.transform.position;
        angle = Mathf.Atan2(v3Pos.y, v3Pos.x)* Mathf.Rad2Deg;

        if (angle < 0.0f) angle += 360.0f;
        transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        transform.localPosition = new Vector3(player.transform.position.x + xPos * 4f, player.transform.position.y + yPos * 4f, 0);
        #endregion

        if (!canFire)
        {
            //timer for how long player CANT shoot
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                // if there are green bullets instantiated
                if (greenBulletQueue.Count > 0)
                {
                    // remove the bullet after 2 seconds if it has not collided with enemy
                    secondTimer += Time.deltaTime;
                    if (secondTimer>2f)
                    {
                       greenBulletQueue.Dequeue();
                    }
                    
                }
                // if there are red bullets
                else if (redBulletQueue.Count > 0)
                {
                    // remove the bullet after 2 seconds if it has not collided with enemy
                    secondTimer += Time.deltaTime;
                    if (secondTimer > 2f)
                    {
                        redBulletQueue.Dequeue();
                    }
                }
                //reset timers and fire again
                canFire = true;
                timer = 0;
                secondTimer = 0;
            }
        }

        // if left click fire green bullet
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            var cloneBullet = Instantiate(greenBullet, bulletTransform.position, Quaternion.identity);
            greenBulletQueue.Enqueue(cloneBullet);
            //greenBulletSprites.Add(cloneBullet);
            //bulletSprites.Add(cloneBullet.GetComponent<SpriteRenderer>());
            //greenColliadble.Add(cloneBullet.GetComponent<CollidableObject>());
        }
        // if right click fire red bullet
        else if (Input.GetMouseButton(1) && canFire)
        {
            canFire = false;
            var cloneBullet = Instantiate(redBullet, bulletTransform.position, Quaternion.identity);
            redBulletQueue.Enqueue(cloneBullet);
            //redBulletSprites.Add(cloneBullet);
            //redBulletSprites.Add(cloneBullet.GetComponent<SpriteRenderer>());
            //redCollidable.Add(cloneBullet.GetComponent<CollidableObject>());
        }
    }
}
