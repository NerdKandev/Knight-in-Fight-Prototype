using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxLives;
    public int CurrrentLives {get; private set;}

    private void Awake()
    {
        CurrrentLives = _maxLives;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoseLife()
    {
        CurrrentLives--;
        Debug.Log($"Player lives: {CurrrentLives}");

        if (CurrrentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player out of lives");
    }
}
