﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float damageRadius = 0.5f;
    [SerializeField] protected float knockbackForce = 5f;
    [SerializeField] protected float gravity = -9.8f;
    [SerializeField] protected bool destroyOnTTL = true;
    [SerializeField] protected float timeToLive = 5f;

    protected Vector2 velocity;
    protected Vector2 initialVelocity;
    protected BeingType ownerType;
    protected Being owner;

    protected LayerMask damageMask;

    protected Movement movement;

    protected virtual void Start()
    {
        movement = GetComponent<Movement>();
    }

    protected virtual void Update()
    {
        if (destroyOnTTL)
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive < 0f)
                Destroy(gameObject);
        }

        DetectVictim();
        movement.Move(velocity * Time.deltaTime);
    }

    public virtual void Init(Vector2 force,  Being owner)
    {
        AddForce(force);
        this.owner = owner;
        this.ownerType = owner.GetBeingType;
        if (ownerType == BeingType.ENEMY)
            damageMask = LayerMask.GetMask("Player");
        else if (ownerType == BeingType.PLAYER)
            damageMask = LayerMask.GetMask("Enemy");
    }

    protected virtual void DetectVictim()
    {
        Collider2D hit;
        hit = Physics2D.OverlapCircle((Vector2)transform.position, damageRadius, damageMask);
        if(hit)
        {
            Hit(hit);
        }
    }

    protected virtual void Hit(Collider2D hit) {
        Vector2 knockbackDirection = hit.transform.position - transform.position;
        Vector2 knockback = knockbackDirection * knockbackForce;
        hit.transform.GetComponent<Being>().GetDamage(damage, knockback);
    }

    private void AddForce(Vector2 force)
    {
        initialVelocity = force;
        velocity.x += force.x;
        velocity.y += force.y;
    }
}
