﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBody : MonoBehaviour {
    private SpriteRenderer SR;
    private float transparency = 1.0f;
    int speedCount = 0;
    float speed = 0.4f;
    Vector3 targetPosition;
    [HideInInspector]
    public int state;
    public Sprite frontUpper;
    public Sprite backUpper;
    public Sprite sideUpper;//左向き
    public Sprite sideUpper2;//右向き
    public enum MoveStatus  //町人の各移動状態　変数名は向き
    {
        Back = 1,
        Front = 2,
        Left = 3,
        Right = 4
    }
    // Use this for initialization
    void Start () {
        targetPosition= this.transform.position + new Vector3(0.15f, 0.2f, 0f);
        SR = GetComponent<SpriteRenderer>();      
        switch (state)
        {
            case (int)MoveStatus.Front:
                this.GetComponent<SpriteRenderer>().sprite = frontUpper;   //正面に切り替え
                return;
            case (int)MoveStatus.Back:
                this.GetComponent<SpriteRenderer>().sprite = backUpper;   //背面に切り替え
                return;
            case (int)MoveStatus.Left:
                this.GetComponent<SpriteRenderer>().sprite = sideUpper;   //左向きに切り替え
                return;
            case (int)MoveStatus.Right:
                this.GetComponent<SpriteRenderer>().sprite = sideUpper2;   //右向きに切り替え
                return;
        }

    }

    private void Update()
    {
        speed = speedCount * -0.2f + 1.75f;
        if (speed < 0.15f)
        {
            speedCount = 8;
        }
        else { speedCount++; }
        float Step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,targetPosition, Step);
        if(transform.position==targetPosition)
        {
            get_transparency();
        }
    }

    void get_transparency()
    {
            SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, transparency);
            transparency -= 0.04f;
        
        Invoke("get_transparency", 0.05f);
        if (transparency <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
