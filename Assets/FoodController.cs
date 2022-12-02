using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    [SerializeField] private SnakeController snakeController;
    
    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.position =RandomSpawnPos();
    }
    
    private Vector3 RandomSpawnPos()
    {
        var xPos = Random.Range(1, 20);
        var yPos = Random.Range(1, 20);
        var spawnPos = new Vector3(xPos, yPos, 0);
        
        foreach (var snake in snakeController.snake)
        {
            if (spawnPos == snake.position)
            {
                Debug.Log("YILANIN ÜSTÜNDE SPAWN OLMAMMMM");
                return RandomSpawnPos();
            }
        }

        return spawnPos;
    }
}
