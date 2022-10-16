using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    float secondTime;
    float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        secondTime = 0f;
        timeDelay = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        secondTime = secondTime + 1f * Time.deltaTime;
        if (secondTime >= timeDelay)
        {
            secondTime = 0f;
            Color newColor = new Color(Random.value,
                           Random.value,
                           Random.value);
            //playerObject.GetComponent<CollidableObject>().gameLight.gameObject.GetComponent<Light2D>().color = newColor;
            //playerObject.GetComponent<CollidableObject>().gameLight.gameObject.GetComponent<Light2D>().intensity = 10;

        }
    }
}
