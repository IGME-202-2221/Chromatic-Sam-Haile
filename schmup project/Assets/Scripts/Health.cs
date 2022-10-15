using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator animator;
    public EnemyFollow speed;
    public CollisionManager collisionManager;
    public Vehicle vehicle;
    public GameObject arrow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        vehicle = GetComponent<Vehicle>();
    }
    /// <summary>
    /// Checks whether a heart icon can be removed
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (health > numOfHearts)
            {
                health = numOfHearts;
            }

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    /// <summary>
    /// Reduce health when damage is taken
    /// </summary>
    public void TakeDamage()
    {
        if (health > 0)
        {
            health -= 1;
        }
        if (health == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Before the game over screen
    /// player cannot move and arrow dissapears
    /// </summary>
    public void GameOver()
    {
        //hide arrow and prevent shooting
        arrow.SetActive(false);
        // stop moving
        vehicle.speed = 0;
    }
}
