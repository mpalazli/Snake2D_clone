using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SnakeController : MonoBehaviour
{
    public GameObject apple;
    public TextMeshProUGUI scoreText;
    public int score;
    public GameObject tailPrefab;
    [SerializeField] private float speed=1;
    private Vector2 _direction= Vector2.down;
    private Vector2 areaLimit=new Vector2(10f,10f);
    private List<Transform> _snake= new List<Transform>();
    void Start()
    {
        StartCoroutine(MoveSnake());
        ChangePositionApple();
        _snake.Add(this.transform);
        
        
    }

   
   private void Update() 
   {
     
       
        if(Input.GetKeyDown(KeyCode.UpArrow)&&_direction!=Vector2.down)
        {
            _direction=Vector2.up;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)&&_direction!=Vector2.up)
        {
            _direction=Vector2.down;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&_direction!=Vector2.right)
        {
            _direction=Vector2.left;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)&&_direction!=Vector2.left)
        {
            _direction=Vector2.right;
        }
   }
   public void update_score(){

    score +=1;
    scoreText.text = score.ToString();
   }

    IEnumerator MoveSnake()
    {
       
        while(true)
        {
            for (int i =_snake.Count-1; i > 0; i--)
            {
                _snake[i].position=_snake[i-1].position;
            }
            Vector3 position = transform.position;
            position+=(Vector3)_direction;
            transform.position=position;
            yield return new WaitForSeconds(speed);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Apple"))
        {
            Grow();
        }
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("DEAD");
        }
    }

    private void Grow()
    {
        Debug.Log("Grow");
        update_score();
        ChangePositionApple();
        var tail = Instantiate(tailPrefab);
        _snake.Add(tail.transform);
        _snake[_snake.Count-1].position=_snake[_snake.Count-2].position;
    }

    private void ChangePositionApple()
    {
        Vector2 newApplePos;
        do
        {
            float randomX = (int)Random.Range(-10, areaLimit.x);
            float randomY = (int)Random.Range(-10, areaLimit.y); 
             apple.transform.position = new Vector3(randomX, randomY, 0);
            newApplePos=new Vector2(randomX,randomY);
        } while (!CanSpawnApple(newApplePos));
       
       
    }
    private bool CanSpawnApple(Vector2 newPos)
    {
        foreach (var item in _snake)
        {
            var x = Mathf.RoundToInt(item.position.x);
            var y = Mathf.RoundToInt(item.position.y);
            if(new Vector2(x,y)==newPos)
            {
                return false;
            }
        }
        return true;
    }
}
