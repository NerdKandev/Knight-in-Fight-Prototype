using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes Setup")]
    [SerializeField] private int laneCount;
    [SerializeField] private int laneWidth;
    private float[] _lanePositions;
    private int _currentLane = 1;
    [SerializeField] private float _laneSwitchSpeed;

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
        HandleLaneSwitch();
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

    private void HandleLaneSwitch()
    {
        if (SwipeManager.Instance.SwipeUp)
        {
            if (_currentLane < 2) _currentLane++;
            else return;

        }
        else if (SwipeManager.Instance.SwipeDown)
        {
            if (_currentLane > 0) _currentLane--;
            else return;
        }
    }
}
