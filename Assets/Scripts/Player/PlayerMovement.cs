using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float LineSize = 2f;
    [SerializeField] private Transform FirstLinePosition;

    [SerializeField] private float JumpPower;
    [SerializeField] private float DashPower;


    [SerializeField] private float ChangeLineTime;

    [Tooltip("Numbering start with 0")]
    [SerializeField] private int startLine = 1;
    [SerializeField] private int lineCount = 3;

    [SerializeField] private LayerMask GroundLayer;
    [HideInInspector][SerializeField] private bool UseAlternateTouchInput;

    private PlayerAnimationController animation = default;
    private bool isGrounded;
    private bool _canJump, _isReverse;

    private int currentLine;
    private Coroutine moveCoroutine;
    private InputSwipe inputSwipe;

    public bool CanJump {
        get => _canJump;
        set => _canJump = value;
    }

    public bool IsReverse {
        get => _isReverse;
        set => _isReverse = value;
    }

    private void Awake()
    {
        this.isGrounded = true;
        this.CanJump = true;
        this.animation = this.GetComponent<PlayerAnimationController>();
        this.inputSwipe = new InputSwipe();
        this.currentLine = this.startLine;
        if (this.animation == null) Debug.LogError("Player Animator is null at PlayerMovement Script");
    }

    private void Update()
    {
        CheckGround();
        if (this.UseAlternateTouchInput) {
            this.inputSwipe.CheckPosition();
            SwipeDirection dir = this.inputSwipe.GetSwipeDirection();
            if (dir!=SwipeDirection.NONE) {
                if (dir == SwipeDirection.UP) JumpAction();
                if (dir == SwipeDirection.DOWN) DashAction();
                if (dir == SwipeDirection.LEFT) MoveLeftAction();
                if (dir == SwipeDirection.RIGHT) MoveRightAction();

            }

        }
    }


    public void DashAction()
    {
        if (this.IsReverse) Jump();
        else Dash();
    }

    public void JumpAction()
    {
        if (this.IsReverse) Dash();
        else Jump();
    }

    public void MoveRightAction() {
        if (this.IsReverse) MoveLeft();
        else MoveRight();
    }

    public void MoveLeftAction()
    {
        if (this.IsReverse) MoveRight();
        else MoveLeft();
    }


    private void MoveRight() {
        if (this.currentLine != lineCount - 1) {
            animation?.MoveRight();
            if (this.moveCoroutine != null) StopCoroutine(this.moveCoroutine);
            this.currentLine++;
            this.moveCoroutine = StartCoroutine(ChangeLine());
        }
    }

    private void MoveLeft() {
        if (this.currentLine != 0)
        {
            animation?.MoveLeft();
            if (this.moveCoroutine != null) StopCoroutine(this.moveCoroutine);
            this.currentLine--;
            this.moveCoroutine = StartCoroutine(ChangeLine());
        }
    }

    private void Jump() {
        if (this.isGrounded && CanJump)
        {
            animation?.Jump();
            this.isGrounded = false;
            this.GetComponent<Rigidbody>().velocity = Vector3.up * this.JumpPower;
        }
    }

    private void Dash() {
        if (!this.isGrounded)
        {
            animation?.Dash();
            this.GetComponent<Rigidbody>().AddForce(Vector3.down * this.DashPower, ForceMode.VelocityChange);
        }
    }

    private IEnumerator ChangeLine() {
        float pos = this.transform.position.x;
        float target = this.FirstLinePosition.position.x + this.currentLine * this.LineSize;

        float len = Mathf.Abs(pos - target);
        float animtime = this.ChangeLineTime * (len / this.LineSize);

        float elapsed = 0f;
        while (this.transform.position.x != target) {
            Vector3 p = this.transform.position;
            p.x = Mathf.Lerp(pos, target, elapsed / animtime);
            this.transform.position = p;
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.moveCoroutine = null;
    }

    private void CheckGround() {
        RaycastHit hit;
        float distance = 2f;
        Vector3 dir = Vector3.down;

        if (Physics.Raycast(transform.position, dir, out hit, distance, this.GroundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}


public class InputSwipe
{
    private Vector3 startPos, endPos;

    public InputSwipe() {
        startPos = Vector3.zero;
        endPos = Vector3.zero;
    }

    public SwipeDirection GetSwipeDirection()
    {
        if (this.startPos!=Vector3.zero && this.endPos != Vector3.zero)
        {
            if (startPos.x > endPos.x && Mathf.Abs(startPos.y - endPos.y) < Mathf.Abs(startPos.x - endPos.x)) { Clear(); return SwipeDirection.LEFT; }
            if (startPos.x < endPos.x && Mathf.Abs(startPos.y - endPos.y) < Mathf.Abs(startPos.x - endPos.x)) { Clear(); return SwipeDirection.RIGHT; }
            if (startPos.y < endPos.y && Mathf.Abs(startPos.x - endPos.x) < Mathf.Abs(startPos.y - endPos.y)) { Clear(); return SwipeDirection.UP; }
            if (startPos.y > endPos.y && Mathf.Abs(startPos.x - endPos.x) < Mathf.Abs(startPos.y - endPos.y)) { Clear(); return SwipeDirection.DOWN; }

        }

        return SwipeDirection.NONE;
    }

    private void Clear()
    {
        this.startPos = Vector3.zero;
        this.endPos = Vector3.zero;
    }
    public void CheckPosition() {
        if (Input.touches.Length > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                this.startPos = Input.touches[0].deltaPosition;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                this.endPos = Input.touches[0].deltaPosition;
            }
        }
    }

}

public enum SwipeDirection { 
    UP,DOWN,
    LEFT,RIGHT,

    NONE
}
