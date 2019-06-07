using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string levelToLoad;
    public int index;

    private float currentTime;
    public float startingTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentTime);
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentTime == 0)
        {
            if (other.CompareTag("Player"))
            {
                //SceneManager.LoadScene(index);
                SceneManager.LoadScene(levelToLoad);
            }

            currentTime = startingTime;

        }




    }
}
