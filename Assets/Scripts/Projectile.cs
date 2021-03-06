﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(!collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("hit");
            Destroy(gameObject, 0.1f);
        }
    }
}
