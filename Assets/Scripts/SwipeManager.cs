using System;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager Instance { get; private set; }

    [Header("Swipe Settings")]
    [SerializeField] private float _minSwipeDistance; //Độ dài tối thiểu khi Swipe

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosistion;

    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResetSwipeFlags();

#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    //Giả lập xử lý vuốt bằng chuột
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosistion = Input.mousePosition;
            DetectSwipe();
        }
    }

    //Xử lý vuốt trên mobile
    private void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            _startTouchPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _endTouchPosistion = touch.position;
            DetectSwipe();
        }

    }

    //Kiểm tra xem người chơi có vuốt không?
    private void DetectSwipe()
    {
        float verticalDistance = _endTouchPosistion.y - _startTouchPosition.y;

        if (Mathf.Abs(verticalDistance) < _minSwipeDistance) return;

        if (verticalDistance > 0)
        {
            SwipeUp = true;
        }
        else
        {
            SwipeDown = true;
        }
    }


    private void ResetSwipeFlags()
    {
        SwipeUp = false;
        SwipeDown = false;
    }

}
