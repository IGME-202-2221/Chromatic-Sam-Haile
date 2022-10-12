using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public bool isCurrentlyColliding = false;

    public SpriteRenderer sprite;

    [HideInInspector]
    public List<CollidableObject> collisions = new List<CollidableObject>();

    private EnemyFollow range;
    public GameObject gameLight;

    private void Awake()
    {
        range = GetComponent<EnemyFollow>();
    }

    void Update()
    {
        // if im currently colliding, turn me red  
        if (isCurrentlyColliding == true )
        {
            sprite.color = Color.red;
        }
        else if (sprite)
        {
            sprite.color = Color.white;
        }
    }

    public void RegisterCollision(CollidableObject other)
    {
        isCurrentlyColliding = true;
        collisions.Add(other);
    }
    public void ResetCollision()
    {
        isCurrentlyColliding = false;
        collisions.Clear();
    }
}
