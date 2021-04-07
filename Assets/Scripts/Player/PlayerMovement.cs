using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float LineSize = 2f;
    [SerializeField] private Transform FirstLinePosition;

    [SerializeField] private float JumpHeight;
    [SerializeField] private float ChangeLineTime;

    [Tooltip("Numbering start with 0")]
    [SerializeField] private int startLine = 1;
    [SerializeField] private int lineCount = 3;

    private int currentLine;
    private Coroutine coroutine;

    public void MoveRight() {
        if (this.currentLine != lineCount-1) {
            if (this.coroutine != null) StopCoroutine(this.coroutine);
            this.currentLine++;
            this.coroutine = StartCoroutine(ChangeLine());
        }
    }
    public void MoveLeft() {
        if (this.currentLine != 0)
        {
            if (this.coroutine != null) StopCoroutine(this.coroutine);
            this.currentLine--;
            this.coroutine = StartCoroutine(ChangeLine());
        }
    }

    public void Jump() {
        Debug.Log("Jumped!");
    }

    private IEnumerator ChangeLine() {
        float pos = this.transform.position.x;
        float target = this.FirstLinePosition.position.x + this.currentLine * this.LineSize;
        float elapsed = 0f;
        while (this.transform.position.x != target) {
            Vector3 p = this.transform.position;
            p.x = Mathf.Lerp(pos, target, elapsed / this.ChangeLineTime);
            this.transform.position = p;
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.coroutine = null;
    }
}
