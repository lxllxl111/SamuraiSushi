﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyDown : MonoBehaviour {
    public GameObject ninjaFlyFrontUpperBody;
    public GameObject ninjaFlyFrontlowerBody;

    private float WaitTime = 0.3f;
    GameObject GameController;
    LifeCounter lifeCounter;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;
    bool firstAttack = false;
    void Start()
    {
        GameController = GameObject.Find("GameController");
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
    }
    private void Update()
    {
        if (this.GetComponent<Floating2>().isMoving == false && firstAttack == false)
        {
            firstAttack = true;
            Invoke("attack", AttackTimeDelay);
        }
    }
    //攻撃発動
    void attack()
    {
        lifeCounter.Damage();
        AttackTimeDelay = Random.Range(2.0f, 4.0f);
        Vector3 pos = new Vector3(0, 0, 2);
        Instantiate(AttackEffect, pos, Quaternion.identity);
        Invoke("attack", AttackTimeDelay);
    }
    //切られた
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            die();
        }
    }
    void die()
    {
        Destroy(gameObject);
        GameObject tempObject1 = Instantiate(ninjaFlyFrontUpperBody, this.transform.position, Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        GameObject tempObject2 = Instantiate(ninjaFlyFrontlowerBody, this.transform.position, Quaternion.identity);
        tempObject2.transform.localScale = this.transform.lossyScale;
    }
}
