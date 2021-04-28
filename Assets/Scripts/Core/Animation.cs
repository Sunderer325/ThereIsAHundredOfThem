﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Being))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Animation : MonoBehaviour
{
	Being controller;
	Animator animator;
	SpriteRenderer sprite;
	void Start()
    {
		controller = GetComponent<Being>();
		animator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
		if (controller.IsDie)
		{
			animator.SetBool("IsDie", true);
			return;
		}

		if (controller.Velocity.x > 0f || controller.ForcedMovementVelocity.x > 0f)
		{
			sprite.flipX = false;
			animator.SetBool("IsWalking", true);
		}
		else if (controller.Velocity.x < 0f || controller.ForcedMovementVelocity.x < 0f)
		{
			sprite.flipX = true;
			animator.SetBool("IsWalking", true);
		}
		else animator.SetBool("IsWalking", false);

		animator.SetBool("InAir", controller.InAir);
		animator.SetBool("Invulnerability", controller.Invulnerability);
	}

	public void GetHit()
	{
		animator.SetTrigger("GetHit");
	}
}
