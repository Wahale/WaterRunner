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


    private PlayerAnimationController animation = default;
    private bool isGrounded;
    private bool _canJump, _isReverse;

    private int currentLine;
    private Coroutine moveCoroutine;

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
        this.animation = this.GetComponent<PlayerAnimationController>();
        if (this.animation == null) Debug.LogError("Player Animator is null at PlayerMovement Script");
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

    private void OnCollisionEnter(Collision collision)
    {
        CheckGround();
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
