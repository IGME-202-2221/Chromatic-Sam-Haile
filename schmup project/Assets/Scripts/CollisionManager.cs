using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    //Store all collidable ojbects in scene
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    public CollidableObject player;
    public GameObject playerObject;
    public RotateAround bullet;


    private Health health;
    public GameOverScreen gameOverScreen;
    #region EnemySpawnFields
    [SerializeField]
    //Type of enemies
    private List<GameObject> enemies;
    private GameObject newEnemy;

    private int randomSpawnZone;
    private float randomXposition, randomYposition;
    private Vector3 spawnPosition;
    #endregion

    private void Awake()
    {
        health = player.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // calls spawn method every two seconds
        InvokeRepeating("SpawnNewEnemy", 0f, 2f);
    }

    void Update()
    {
        foreach (CollidableObject collidableObject in collidableObjects)
        {
            //reset collision
            collidableObject.ResetCollision();
        }
        #region PlayerXEnemy Collisions
        // Check to see if any objects are colliding with one another,
        // tell ojects what they are colliding with
        for (int firstIndex = 0; firstIndex < collidableObjects.Count; firstIndex++)
        {
            for (int secondIndex = firstIndex + 1; secondIndex < collidableObjects.Count; secondIndex++)
            {
                foreach(CollidableObject enemy in collidableObjects.ToList())
                {
                    // if the enemy is a circle, use the circle collision and destroy object
                    // also damage player
                    if (enemy.tag == "Circle")
                    {
                        // do circle collision
                        for (int sprite = 1; sprite < sprites.Count; sprite++)
                        {
                            // Radius of player
                            float radius = sprites[0].bounds.size.x / 2;
                            // Radius of other object
                            float radius2 = sprites[sprite].bounds.size.x / 2;

                            // Find distance between both centers
                            float distance = Mathf.Sqrt(
                                Mathf.Pow(sprites[sprite].transform.position.x - sprites[0].transform.position.x, 2) +
                                Mathf.Pow(sprites[sprite].transform.position.y - sprites[0].transform.position.y, 2));

                            // if distance is less then the sum of the radii
                            // collision
                            if (distance < radius + radius2)
                            {
                                //collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                                health.TakeDamage();
                                //destroy the game object
                                Destroy(collidableObjects[sprite].GetComponent<GameObject>());
                                //destroy the light child object attached
                                Destroy(collidableObjects[sprite].GetComponent<Light>());
                                collidableObjects[sprite] = null;
                                collidableObjects.RemoveAt(sprite);
                                // destroy the sprite renderer
                                Destroy(sprites[sprite]);
                                sprites[sprite] = null;
                                sprites.RemoveAt(sprite);


                            }
                        }

                    }
                    //If the enemy is a square, use the rectangle collision and destory object
                    // also damage the player
                    else if (enemy.tag == "Square")
                    {
                        //AABB collision
                        for (int sprite = 1; sprite < sprites.Count; sprite++)
                        {
                            if (sprites[0].bounds.min.x < sprites[sprite].bounds.max.x &&
                                    sprites[0].bounds.max.x > sprites[sprite].bounds.min.x &&
                                    sprites[0].bounds.max.y > sprites[sprite].bounds.min.y &&
                                    sprites[0].bounds.min.y < sprites[sprite].bounds.max.y)
                            {
                                //collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                                health.TakeDamage();
                                //destroy the game object
                                Destroy(collidableObjects[sprite].GetComponent<GameObject>());
                                //destroy the light child object attached
                                Destroy(collidableObjects[sprite].GetComponent<Light>());
                                collidableObjects[sprite] = null;
                                collidableObjects.RemoveAt(sprite);
                                // destroy the sprite renderer
                                Destroy(sprites[sprite]);
                                sprites[sprite] = null;
                                sprites.RemoveAt(sprite);
                            }
                        }
                    }
                }
            }
            #endregion

            #region BulletXEnemy Collisions
            // for ever enemy spawned in the game
            foreach (CollidableObject collidableObject in collidableObjects.ToList())
            {
                // for each enemy instantiated in the game
                for (int sprite = 0; sprite < collidableObjects.Count; sprite++)
                {
                    // and for each bullet in the queue
                    foreach (GameObject item in bullet.greenBulletQueue.ToList())
                    {
                        if (item != null && item.GetComponent<SpriteRenderer>().bounds.min.x < sprites[sprite].bounds.max.x &&
                            item.GetComponent<SpriteRenderer>().bounds.max.x > sprites[sprite].bounds.min.x &&
                            item.GetComponent<SpriteRenderer>().bounds.max.y > sprites[sprite].bounds.min.y &&
                            item.GetComponent<SpriteRenderer>().bounds.min.y < sprites[sprite].bounds.max.y &&
                            sprites[sprite].tag == "Circle")
                        {
                            collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                            Destroy(item);
                            //destroy the game object
                            Destroy(collidableObjects[sprite].GetComponent<GameObject>());
                            //destroy the light child object attached
                            Destroy(collidableObjects[sprite].gameLight);
                            collidableObjects[sprite] = null;
                            collidableObjects.RemoveAt(sprite);
                            // destroy the sprite renderer
                            Destroy(sprites[sprite]);
                            sprites[sprite] = null;
                            sprites.RemoveAt(sprite);
                        }
                    }
                    foreach (GameObject item in bullet.redBulletQueue.ToList())
                    {
                        if (item != null && item.GetComponent<SpriteRenderer>().bounds.min.x < sprites[sprite].bounds.max.x &&
                            item.GetComponent<SpriteRenderer>().bounds.max.x > sprites[sprite].bounds.min.x &&
                            item.GetComponent<SpriteRenderer>().bounds.max.y > sprites[sprite].bounds.min.y &&
                            item.GetComponent<SpriteRenderer>().bounds.min.y < sprites[sprite].bounds.max.y &&
                            sprites[sprite].tag == "Square")
                        {

                            collidableObjects[0].RegisterCollision(collidableObjects[sprite]);
                            Destroy(item);
                            //destroy the game object
                            Destroy(collidableObjects[sprite].GetComponent<GameObject>());
                            //destroy the light child object attached
                            Destroy(collidableObjects[sprite].gameLight);
                            collidableObjects[sprite] = null;
                            collidableObjects.RemoveAt(sprite);
                            // destroy the sprite renderer
                            Destroy(sprites[sprite]);
                            sprites[sprite] = null;
                            sprites.RemoveAt(sprite);
                        }
                    }
                }
                #endregion
            }
        }
        if (health.health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }



    private void SpawnNewEnemy()
    {
        randomSpawnZone = Random.Range(0, 4);

        // randomly choose spawns zone
        switch (randomSpawnZone)
        {
            case 0: // left of screen
                randomXposition = Random.Range(-11f, -10f);
                randomYposition = Random.Range(-8f, 8f);
                break;
            case 1: // bottom of screen
                randomXposition = Random.Range(-10f, 10f);
                randomYposition = Random.Range(-7f, -8f);
                break;
            case 2: // right of screen
                randomXposition = Random.Range(10f, 11f);
                randomYposition = Random.Range(-8f, 8f);
                break;
            case 3: // top of screen
                randomXposition = Random.Range(-10f, 10f);
                randomYposition = Random.Range(7f, 8f);
                break;
        }

        // determine randomly which enemy in the list to instantiate
        // 0 = square enemies
        // 1 = circle enemies
        int enemySelector = Random.Range(0, 2);

        spawnPosition = new Vector3(randomXposition, randomYposition, 0f);
        newEnemy = Instantiate(enemies[enemySelector], spawnPosition, Quaternion.identity);
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
            if(item.tag == "Square")
                
                Gizmos.DrawWireCube(item.bounds.center, item.bounds.size);
            }

    }
}

