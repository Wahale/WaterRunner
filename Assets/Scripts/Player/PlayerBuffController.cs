using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBuffController : MonoBehaviour
{
    [SerializeField] private MoveRoads MoveRoadsController;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private LayerMask BuffLayer;
    [SerializeField] private Text buffTimeText;

    private PlayerMovement movement;

    private void Awake()
    {
        this.movement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Pow(2,collision.gameObject.layer) == BuffLayer.value) {
            if (this.collectSound != null)
            {
                if (Camera.main.gameObject.GetComponent<AudioSource>() == null) Camera.main.gameObject.AddComponent<AudioSource>();
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(this.collectSound);
            }
            BuffObject buff = collision.transform.GetComponent<BuffObject>();
            ActivateBuff(buff);
            GameObject.Destroy(collision.gameObject);
        }
    }


    private void ActivateBuff(BuffObject buff) {
        if (buff.Type == BuffType.JumpBlock)
        {
            StartCoroutine(BlockJump(buff.Time));
        }
        else if (buff.Type == BuffType.SpeedIncrease)
        {
            if (buff.IsTimeBuff)
            {
                StartCoroutine(RoadSpeed(buff.Value, buff.Time));
            }
            else
            {
                this.MoveRoadsController.speed = buff.Value;
            }
        }
        else if (buff.Type == BuffType.MoveReverse) 
        {
            StartCoroutine(MoveReverse(buff.Time));
        }
    }


    private IEnumerator BlockJump(float time) {
        float esimated = 0f;
        this.movement.CanJump = false;
        while (esimated <= time) {
            if (this.buffTimeText!=null) this.buffTimeText.text = (time - esimated).ToString("f2");
            esimated += Time.deltaTime;
            yield return null;
        }
        this.movement.CanJump = true;
    }

    private IEnumerator MoveReverse(float time)
    {
        float esimated = 0f;
        this.movement.IsReverse = true;
        while (esimated <= time)
        {
            if (this.buffTimeText != null) this.buffTimeText.text = (time - esimated).ToString("f2");
            esimated += Time.deltaTime;
            yield return null;
        }
        this.movement.IsReverse = false;
    }


    private IEnumerator RoadSpeed(float value,float time)
    {
        float esimated = 0f;
        float currentSpeed = this.MoveRoadsController.speed;
        this.MoveRoadsController.speed = value;
        while (esimated <= time)
        {
            if (this.buffTimeText != null) this.buffTimeText.text = (time - esimated).ToString("f2");
            esimated += Time.deltaTime;
            yield return null;
        }
        this.MoveRoadsController.speed = currentSpeed;
    }
}
