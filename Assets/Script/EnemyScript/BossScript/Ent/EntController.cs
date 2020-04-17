﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntController : MonoBehaviour
{
    /*
     * By: Parker Allen
     * Version: 2.0
     * 
     */

    private Skeleton[] skeleton;
    /*
     * 10
     * 
     * lower middle
     * lower upper
     * chest
     * 
     * front arm
     * front fore arm
     * front hand
     * 
     * head
     * 
     * back arm
     * back fore arm
     * back hand
     */

    private BossAnimation idle1, idle2;
    private BossAnimation[] idle;
    private BossAnimation basicAttack1, basicAttack2;
    private BossAnimation seedAttack1, seedAttack2;
    private BossAnimation spikeAttack1, spikeAttack2, spikeAttack3;
    private BossAnimation death1, death2, death3, death4, death5;

    private BossAnimationController bossAnimationController;
    private EntMovement entMovement;

    private void Awake()
    {
        skeleton = GetComponentsInChildren<Skeleton>();
        bossAnimationController = GetComponent<BossAnimationController>();
        entMovement = GetComponent<EntMovement>();
    }

    private void Start()
    {
        SetIdleAnimation();
        SetBasicAttackAnimation();
        SetSeedAttackAnimation();
        SetSpikeAttackAnimation();
        SetDeathAnimation();
        bossAnimationController.SetInterruptableActions(idle);
    }

    private void Update()
    {
        if (bossAnimationController.EmptyPriority())
        {
            //AddDeathPrioity();
        }
        bool isFlip = (entMovement.direction == Vector3.left) ? true : false;
        bossAnimationController.DoAction(skeleton, isFlip);
    }

    public void AddBasicAttackPriority()
    {
        bossAnimationController.AddPriorityAction(basicAttack1);
        bossAnimationController.AddPriorityAction(basicAttack2);
    }

    public void AddSeedAttackPriority()
    {
        bossAnimationController.AddPriorityAction(seedAttack1);
        bossAnimationController.AddPriorityAction(seedAttack2);
    }

    public void AddSpikeAttackPriority()
    {
        bossAnimationController.AddPriorityAction(spikeAttack1);
        bossAnimationController.AddPriorityAction(spikeAttack2);
        bossAnimationController.AddPriorityAction(spikeAttack3);
    }

    public void AddDeathPrioity()
    {
        bossAnimationController.AddPriorityAction(death1);
        bossAnimationController.AddPriorityAction(death2);
        bossAnimationController.AddPriorityAction(death3);
        bossAnimationController.AddPriorityAction(death4);
        bossAnimationController.SetInterruptableActions(new BossAnimation[] { death5 });
    }

    private void SetIdleAnimation()
    {
                                    //body      front arm   head    back arm
        float[] angle1 = new float[] {0, 0, 0,  5, 0, 0,    0,  0, 0, 0};
        float[] angle2 = new float[] {-5, 0, 5,  0, -5, -5,    -5,  0, 5, 0};

        idle1 = new BossAnimation(angle1, 2f);
        idle2 = new BossAnimation(angle2, 2f);

        idle = new BossAnimation[] { idle1, idle2 };
    }

    private void SetBasicAttackAnimation()
    {
        float[] angle1 = new float[] {-20, 30, -20,  -150, 0, 0,    0,  -140, 0, 0};
        float[] angle2 = new float[] {50, -80, 60,  -70, 40, -40,    -30,  -60, 35, -40};

        basicAttack1 = new BossAnimation(angle1, 1f);
        basicAttack2 = new BossAnimation(angle2, .4f);
    }

    private void SetSeedAttackAnimation()
    {
        float[] angle1 = new float[] {-50, 100, -10,  -50, 40, -60,    -30,  -40, 50, -60};

        seedAttack1 = new BossAnimation(angle1, 1f);
        seedAttack2 = new BossAnimation(angle1, 1.5f);
    }

    private void SetSpikeAttackAnimation()
    {
        float[] angle1 = new float[] {-10, 20, -10,  70, -50, 0,    0,  -30, 50, -20};
        float[] angle2 = new float[] {20, -30, 20,  -55, 80, 0,    -20,  30, -50, 20};

        spikeAttack1 = new BossAnimation(angle1, 1f);
        spikeAttack2 = new BossAnimation(angle2, .3f);
        spikeAttack3 = new BossAnimation(angle2, 1f);
    }

    private void SetDeathAnimation()
    {
        float[] angle1 = new float[] {-10, 0, -30,    -30, -50, -30,    -30,    -10, -60, -20};
        float[] angle2 = new float[] {80, -60, 10,    -90, -10, -30,    -10,    -20, 10, -90};
        float[] angle3 = new float[] {80, 50, -100,    -140, 90, -30,    40,    70, -70, -90};
        float[] angle4 = new float[] {80, 50, -90,    -160, 100, 0,    50,    60, -70, -90};
        float[] angle5 = new float[] {80, 30, -50,    -30, -70, -30,    -30,    -10, -60, -20};

        death1 = new BossAnimation(angle1, .3f);
        death2 = new BossAnimation(angle2, .7f);
        death3 = new BossAnimation(angle3, 1f);
        death4 = new BossAnimation(angle4, .5f);
        death5 = new BossAnimation(angle4, 2f);
    }
}
