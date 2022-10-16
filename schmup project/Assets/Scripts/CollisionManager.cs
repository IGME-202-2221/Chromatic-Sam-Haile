using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class CollisionManager : MonoBehaviour
{

    public CollidableObject player;
    //Player Fields
    public GameObject playerObject;
    public RotateAround bullet;
    private Health health;
    //General Game Fields
    public GameOverScreen gameOverScreen;
    public AnimationManager animationManager;
    float timer = 1f;
    float timerTwo;

    //Enemy Fields
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    [SerializeField]
    private List<GameObject> enemies;
    private GameObject newEnemy;
    private int randomSpawnZone;
    private float randomXposition, randomYposition;
    private Vector3 spawnPosition;

    private void Awake()
    {
        health = player.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // calls spawn method every two seconds
        InvokeRepeating("SpawnNewEnemy", 0f, 1f);
    }

    void Update()
    {
        foreach (CollidableObject collidableObject in collidableObjects)
        {
            //reset collision
            collidableObject.ResetCollision();
        }

        // Check to see if any objects are colliding with one another,
        // tell ojects what they are colliding with
        #region PlayerXObject Collisions

        for (int collidableObject = 1; collidableObject < collidableObjects.Count; collidableObject++)
        {

            // if the enemy is a circle, use the circle collision and destroy object
            if (CircleCollision(collidableObject) == true
                && collidableObjects[collidableObject].tag == "Circle")
            {
                // Circle collision
                health.TakeDamage();
                DestroyObject(collidableObject);
            }
            else if (CircleCollision(collidableObject) == true
                && collidableObjects[collidableObject].tag == "item")
            {
                health.health = 4;
                DestroyObject(collidableObject);
            }
            else if (AABBCollision(collidableObject) == true
                && collidableObjects[collidableObject].tag == "Square")
            {
                health.TakeDamage();
                DestroyObject(collidableObject);
            }
        }
        #endregion

        #region BulletXEnemy Collisions
        // foreach enemy spawned in the game
        foreach (CollidableObject collidableObject in collidableObjects.ToList())
        {
            // get index of an enemy
            for (int sprite = 0; sprite < collidableObjects.Count; sprite++)
            {
                // for all green bullets
                foreach (GameObject item in bullet.greenBulletQueue.ToList())
                {
                    // if it hits circle, destroy it
                    if (item != null && CollisionCheck(item, sprite) == true &&
                        sprites[sprite].tag == "Circle")
                    {
                        collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                        animationManager.circleDie();
                        RainbowEffect();
                        Destroy(item);
                        DestroyObject(sprite);
                    }
                    // if it hits a square, make it bigger
                    else if (item != null && CollisionCheck(item, sprite) == true &&
                        sprites[sprite].tag == "Square")
                    {
                        // slightly lower speed to balance
                        collidableObjects[sprite].GetComponent<EnemyFollow>().speed = 2;
                        collidableObjects[sprite].transform.localScale = new Vector3(2f, 2f, 0f);
                    }
                }
                // for all red bullets
                foreach (GameObject item in bullet.redBulletQueue.ToList())
                {
                    // if it hits a square, destroy it
                    if (item != null && CollisionCheck(item, sprite) &&
                        sprites[sprite].tag == "Square")
                    {
                        collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                        Destroy(item);
                        DestroyObject(sprite);
                    }
                    // if it hits a circle, make it faster
                    else if (item != null && CollisionCheck(item, sprite) == true &&
                        sprites[sprite].tag == "Circle")
                    {
                        collidableObjects[sprite].GetComponent<EnemyFollow>().speed = 5;
                    }
                }
            }
        }
        #endregion

        // Once health reaches zero
        // play death animation and change to game over screen
        if (health.health == 0)
        {
            animationManager.Die();
            do
            {
                Timer(5f);

            } while (false);
            /*else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }*/
        }
    }

    public bool Timer(float time)
    {
        do
        {
            timer -= Time.deltaTime;
            Debug.Log(time);

        } while (time > 0);

        if (time <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RainbowEffect()
    {
        Debug.Log("Trying to color switrc");
        int i = 0;
        do
        {
            Color newColor = new Color(Random.value,
                                       Random.value,
                                       Random.value);
            playerObject.GetComponent<CollidableObject>().gameLight.gameObject.GetComponent<Light2D>().color = newColor;
            Debug.Log(i);
            i++;
        } while (i < 100);


    }
    /// <summary>
    /// Check for collision using circle collision
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns> boolean value if it is colliding</returns>
    public bool CircleCollision(int index)
    {
        // Radius of player
        float radius = sprites[0].bounds.size.x / 2;
        // Radius of other object
        float radius2 = sprites[index].bounds.size.x / 2;

        // Find distance between both centers
        float distance = Mathf.Sqrt(
            Mathf.Pow(sprites[index].transform.position.x - sprites[0].transform.position.x, 2) +
            Mathf.Pow(sprites[index].transform.position.y - sprites[0].transform.position.y, 2));

        // if distance is less then the sum of the radii
        // collision
        if (distance < radius + radius2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Using AABB, check if player is colliding with enemies
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public bool AABBCollision(int index)
    {
        if (sprites[0].bounds.min.x < sprites[index].bounds.max.x &&
            sprites[0].bounds.max.x > sprites[index].bounds.min.x &&
            sprites[0].bounds.max.y > sprites[index].bounds.min.y &&
            sprites[0].bounds.min.y < sprites[index].bounds.max.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Destroys game object and attached child object
    /// </summary>
    /// <param name="sprite"></param>
    public void DestroyObject(int index)
    {
        // Destroy the game object
        Destroy(collidableObjects[index].GetComponent<GameObject>());
        // Destroy the child object that creates light effect
        Destroy(collidableObjects[index].gameLight);
        collidableObjects[index] = null;
        collidableObjects.RemoveAt(index);
        // destroy the sprite renderer
        Destroy(sprites[index]);
        sprites[index] = null;
        sprites.RemoveAt(index);
    }

    /// <summary>
    /// Using AABB, check the collision between bullets and enemy sprites
    /// </summary>
    /// <param name="item"></param>
    /// <param name="sprite"></param>
    /// <returns> boolean value based on if colliding</returns>
    public bool CollisionCheck(GameObject bullet, int index)
    {
        if (bullet.GetComponent<SpriteRenderer>().bounds.min.x < sprites[index].bounds.max.x &&
            bullet.GetComponent<SpriteRenderer>().bounds.max.x > sprites[index].bounds.min.x &&
            bullet.GetComponent<SpriteRenderer>().bounds.max.y > sprites[index].bounds.min.y &&
            bullet.GetComponent<SpriteRenderer>().bounds.min.y < sprites[index].bounds.max.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Determine a random location outside of the screen to spawn an object
    /// </summary>
    /// <param name="valueOne">Used for restricting spawn areas</param>
    /// <param name="valueTwo">Used for restricting spawn areas</param>
    public void RandomSpawnZone(int minValue, int maxValue)
    {
        // Determines which sections of the map an item or enemy can spawn
        // For example: Method(3,3); will only make items spawn from the top of the screen
        randomSpawnZone = Random.Range(minValue, maxValue);

        // randomly choose spawns zone
        switch (randomSpawnZone)
        {
            case 0: // left of screen
                randomXposition = Random.Range(-11f, -10f);
                randomYposition = Random.Range(-8f, 8f);
                break;
            /*REMOVED SPAWNING FROM BOTTOM OF SCREEN
        case 1: // bottom of screen
            randomXposition = Random.Range(-6.25f, 6.25f);
            randomYposition = Random.Range(-7f, -8f);
            break;
            */
            case 1: // right of screen
                randomXposition = Random.Range(10f, 11f);
                randomYposition = Random.Range(-8f, 8f);
                break;
            case 2: // top of screen
                randomXposition = Random.Range(-6f, 6f);
                randomYposition = Random.Range(7f, 8f);
                break;
        }

    }

    /// <summary>
    /// Spawns an object at the random location
    /// </summary>
    private void SpawnNewEnemy()
    {
        int objectSelector = Random.Range(1, 101);
        if (objectSelector < 7)
        {
            // spawn health pack
            // but ONLY at the top of the screen
            RandomSpawnZone(2, 3);
            Spawn(randomXposition, randomYposition, 2, .25f, .25f);
        }
        else if (objectSelector >= 7 && objectSelector < 50)
        {
            // spawn square
            RandomSpawnZone(0, 3);
            Spawn(randomXposition, randomYposition, 0, 1f, 1.5f);
        }
        else if (objectSelector >= 50)
        {
            // spawn circle
            RandomSpawnZone(0, 3);
            Spawn(randomXposition, randomYposition, 1, 1f, 1.5f);

        }
    }

    /// <summary>
    /// Instantiate the object
    /// </summary>
    /// <param name="xPosition"> The random X position</param>
    /// <param name="yPosition"> The random Y position</param>
    /// <param name="objectSelector"> Get the correct object from the list of prefabs</param>
    /// <param name="minSize"> Used for variation in size of enemies</param>
    /// <param name="maxSize"> Used for variation in size of enemies</param>
    public void Spawn(float xPosition, float yPosition, int objectIndex, float minSize, float maxSize)
    {
        spawnPosition = new Vector3(xPosition, yPosition, 0f);
        newEnemy = Instantiate(enemies[objectIndex], spawnPosition, Quaternion.identity);
        float randomScale = Random.Range(minSize, maxSize);
        newEnemy.transform.localScale = new Vector3(randomScale, randomScale, 0f);
        collidableObjects.Add(newEnemy.GetComponent<CollidableObject>());
        sprites.Add(newEnemy.GetComponent<SpriteRenderer>());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.sprite.bounds.center, player.sprite.bounds.size.x / 2);

        foreach (SpriteRenderer item in sprites)
        {
            if (item.tag == "Circle" && item != null)
            {
                Gizmos.DrawWireSphere(item.bounds.center, item.bounds.size.x / 2);
            }
        }
        foreach (SpriteRenderer item in sprites)
        {
            if (item.tag == "Square")

                Gizmos.DrawWireCube(item.bounds.center, item.bounds.size);
        }

    }
}

