using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes Setup")]
    [SerializeField] private int laneCount;
    [SerializeField] private int laneWidth;
    private float[] _lanePositions;
    private int _currentLane = 1;
    [SerializeField] private float _laneSwitchSpeed;

    [Header("Attack Setup")]
    [SerializeField] float attackCooldown;
    private float timeForNextAttack = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Cấp phát bộ nhớ cho biến lanePositions
        _lanePositions = new float[laneCount];

        // Lấy số nguyên từ số lượng lane
        int middleIndex = laneCount / 2;
        for (int i = 0; i < laneCount; i++)
        {
            _lanePositions[i] = (i - middleIndex) * laneWidth;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        LaneSwitching();
        HandlePlayerAttack();
    }

    private void HandlePlayerMovement()
    {
        Vector3 targetPos = new Vector3(transform.position.x,
        transform.position.y,
        _lanePositions[_currentLane]);

        transform.position = Vector3.Lerp(transform.position,
        targetPos,
        _laneSwitchSpeed * Time.deltaTime);
    }

    private void LaneSwitching()
    {
        if (InputManager.Instance.SwipeUp)
        {
            if (_currentLane < 2) _currentLane++;
            else return;

        }
        else if (InputManager.Instance.SwipeDown)
        {
            if (_currentLane > 0) _currentLane--;
            else return;
        }
    }

    private void HandlePlayerAttack()
    {
        if (InputManager.Instance.Tap && CanAttack())
        {
            Debug.Log("Player is attacking");
        }
    }

    private bool CanAttack()
    {
        if (Time.time < timeForNextAttack) return false;
        timeForNextAttack = Time.time + attackCooldown;
        return true;
    }
}
