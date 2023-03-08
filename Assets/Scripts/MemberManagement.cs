using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MemberManagement : MonoBehaviour
{
    public Animator character_anim;
    public GameObject Particle_Death;
    private Transform Boss;
    public int Health;
    public float MinDistanceOfEnemy, MaxDistanceOfEnemy, moveSpeed;
    public bool fight, member;
    private Rigidbody rb;
    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        character_anim= GetComponent<Animator>();
        Boss = GameObject.FindWithTag("Boss").transform;
        Health = 5;
        rb = GetComponent<Rigidbody>();
        _capsuleCollider= GetComponent<CapsuleCollider>();  
    }
    private void Update()
    {
        var bossDistance = Boss.position - transform.position;

        if (!fight)
        {
            if(bossDistance.sqrMagnitude<=MaxDistanceOfEnemy*MaxDistanceOfEnemy)
            {
                PlayerManager.PlayerManagerCls.attackToTheBoss = true;
                PlayerManager.PlayerManagerCls.gameState = false;
            }
            if(PlayerManager.PlayerManagerCls.attackToTheBoss&&member)
            {
                transform.position = Vector3.MoveTowards(transform.position,Boss.position, moveSpeed*Time.deltaTime);

                var stickManRotation = new Vector3(Boss.position.x,transform.position.y,Boss.position.z)-transform.position;
                    
                transform.rotation = Quaternion.Slerp(transform.rotation,quaternion.LookRotation(stickManRotation,Vector3.up),10f*Time.deltaTime);

                character_anim.SetFloat("run", 1f);
            }   
        }
        if(bossDistance.sqrMagnitude<=MinDistanceOfEnemy*MinDistanceOfEnemy)
        {
            var stickManRotation = new Vector3(Boss.position.x, transform.position.y, Boss.position.z) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion.LookRotation(stickManRotation, Vector3.up), 10f * Time.deltaTime);

            // character_anim.SetFloat("fight", 1f);
            MinDistanceOfEnemy = MaxDistanceOfEnemy; 
        }
        else
        {
            fight = false;
        }

    }
}
