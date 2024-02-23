using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    // Start is called before the first frame update

    float velocity = 0.0f;
    float acceleration = 1.0f;
    float deceleration = 1.0f;
    int velocityHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        bool forwardKeyPress = Input.GetKey("w");
        bool runKeyPress = Input.GetKey("left shift");

        // if (!isWalking && forwardKeyPress) {
        //     animator.SetBool(isWalkingHash, true);
        // }

        // if (isWalking && !forwardKeyPress) {
        //     animator.SetBool(isWalkingHash, false);
        // }

        // if (!isRunning && forwardKeyPress && runKeyPress) {
        //     animator.SetBool(isRunningHash, true);
        // }

        // if (isRunning && (!forwardKeyPress || !runKeyPress)) {
        //     animator.SetBool(isRunningHash, false);
        // }

        if (forwardKeyPress && velocity <= 1.0f) {
            velocity += Time.deltaTime*acceleration;
        }

        if (!forwardKeyPress && velocity > 0.0f) {
            velocity -= Time.deltaTime*deceleration;
        }

        if (!forwardKeyPress && velocity <= 0.0f) {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);

    }
}
