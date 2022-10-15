using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject gameLight;
    public ScoreScript scoreScript;
    [HideInInspector]
    public List<CollidableObject> collisions = new List<CollidableObject>();
    public bool isCurrentlyColliding = false;

    void Update()
    {
        // if im currently colliding, turn me red  
        if (isCurrentlyColliding == true )
        {
            //play hurt animation
            //sprite.color = Color.red;
        }
        else if (sprite)
        {
            sprite.color = Color.white;
        }
    }
    /// <summary>
    /// Add to score when enemies are destroyed
    /// Only triggers when shot, player colliding does not award points
    /// </summary>
    /// <param name="other"> The object the player is colliding with</param>
    public void RegisterCollision(CollidableObject other)
    {
        //100 points for circle
        if (other.tag == "Circle")
        {
            ScoreScript.scoreNum += 100;
            if (ScoreScript.highscoreNum < ScoreScript.scoreNum)
            {
                ScoreScript.highscoreNum = ScoreScript.scoreNum;
            }
        }
        // 200 points for square
        else if(other.tag == "Square")
        {
            ScoreScript.scoreNum += 200;
            if (ScoreScript.highscoreNum < ScoreScript.scoreNum)
            {
                ScoreScript.highscoreNum = ScoreScript.scoreNum;
            }
        }
        isCurrentlyColliding = true;
        collisions.Add(other);
    }

    public void ResetCollision()
    {
        isCurrentlyColliding = false;
        collisions.Clear();
    }
}
