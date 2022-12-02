using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private int score;
    
    private Vector3 disaredDirection;
    private Vector3 direction;
    
    public List<Transform> snake = new List<Transform>();

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float speed;
    [SerializeField] private Transform tail;
    [SerializeField] private Canvas canvas;
    
    

    private void Awake()
    {
        snake.Add(transform);
        disaredDirection = Vector3.up;
    }

    void Start()
    {
        score = 0;
        canvas.gameObject.SetActive(false);
        StartCoroutine(Move());
    }
    
    private void Update()
    {
        UpdateDirection();
        scoreText.text = score.ToString();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/speed);
            
            for (int i = snake.Count -1 ; i > 0; i--)
            {
                snake[i].position = snake[i - 1].position;
            }
            
            direction = disaredDirection;
            var position = transform.position + direction;
            transform.position = position;
            
        }
    }

    private void UpdateDirection()
    {
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
        {
            disaredDirection = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
        {
            disaredDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.down)
        {
            disaredDirection = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.up)
        {
            disaredDirection = Vector3.down;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall") || col.CompareTag("Tail"))
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (col.TryGetComponent(out FoodController foodController))
        {
            score++;
            
            foodController.gameObject.SetActive(false);
            foodController.Spawn();

            var newTail = Instantiate(tail,snake[^1].transform.position, Quaternion.identity);
            snake.Add(newTail);
            snake[1].gameObject.GetComponent<Collider2D>().enabled = false;
            
        }
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
