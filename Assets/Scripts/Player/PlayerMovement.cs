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

    private int currentLine;
    private Coroutine moveCoroutine;

    public void MoveRight() {
        if (this.currentLine != lineCount - 1) {
            if (this.moveCoroutine != null) StopCoroutine(this.moveCoroutine);
            this.currentLine++;
            this.moveCoroutine = StartCoroutine(ChangeLine());
        }
    }
    public void MoveLeft() {
        if (this.currentLine != 0)
        {
            if (this.moveCoroutine != null) StopCoroutine(this.moveCoroutine);
            this.currentLine--;
            this.moveCoroutine = StartCoroutine(ChangeLine());
        }
    }

    public void Jump() {
        this.GetComponent<Rigidbody>().velocity = Vector3.up* this.JumpPower;
    }

    public void Dash() {
        this.GetComponent<Rigidbody>().AddForce(Vector3.down * this.DashPower, ForceMode.VelocityChange);
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
}
