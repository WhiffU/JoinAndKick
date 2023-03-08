using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool MoveByTouch, gameState, attackToTheBoss;
    private Vector3 Direction;
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    [SerializeField] private Transform road;
    public List<Rigidbody> RbList = new List<Rigidbody>();
    public static PlayerManager PlayerManagerCls;

    void Start()
    {
        PlayerManagerCls = this;
        RbList.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        gameState = true;
    }

    void Update()
    {
        if (gameState)
        {
            if (Input.GetMouseButton(0))
            {
                MoveByTouch = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                MoveByTouch = false;
            }
            if (MoveByTouch)
            {
                Direction = new Vector3(Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), runSpeed * Time.deltaTime), 0f);

                Direction = Vector3.ClampMagnitude(Direction, 1f);

                road.position = new Vector3(0, 0, Mathf.SmoothStep(road.position.z, -100f, Time.deltaTime * roadSpeed));
                foreach (var stickman_anim in RbList)
                {
                    stickman_anim.GetComponent<Animator>().SetFloat("Run", 1f);

                }

            }
            else
            {
                foreach (var stickman_anim in RbList)
                {
                    stickman_anim.GetComponent<Animator>().SetFloat("Run", 0f);

                }
            }
            foreach (var stickman_rb in RbList)
            {
                if (stickman_rb.velocity.magnitude > 0.5f)
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation, Quaternion.LookRotation(stickman_rb.velocity, Vector3.up), Time.deltaTime * velocity);
                }
                else
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation, Quaternion.identity, Time.deltaTime * velocity);

                }
            }
        }


    }
    private void FixedUpdate()
    {
        if (gameState)
        {
            if (MoveByTouch)
            {
                Vector3 Displacement = new Vector3(Direction.x, 0f, 0f) * Time.fixedDeltaTime;
                foreach (var stickman_rb in RbList)
                {
                    stickman_rb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) + Displacement;
                }
            }
            else
            {
                foreach (var stickman_rb in RbList)
                    stickman_rb.velocity = Vector3.zero;
            }
        }
    }
}
