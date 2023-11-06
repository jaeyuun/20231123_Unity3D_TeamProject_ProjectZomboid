using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Animator animator;

    // 오른손 IK 타겟 위치를 나타내는 Transform입니다. 
    // Unity 에디터에서 이 위치를 총의 핸들 위치에 맞게 설정합니다.
    public Transform rightHandIKTarget;

    // 왼손 IK 타겟 위치를 나타내는 Transform입니다. 
    // Unity 에디터에서 이 위치를 총의 총구 위치에 맞게 설정합니다.
    public Transform leftHandIKTarget;

    // 스크립트가 시작될 때 Animator 컴포넌트를 찾습니다.
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // OnAnimatorIK 메서드는 Unity의 IK 시스템에서 호출되며,
    // 여기서 캐릭터의 손 (왼손과 오른손)의 위치와 방향을 조정합니다.
    void OnAnimatorIK()
    {
        // 오른손의 IK 위치와 방향을 설정합니다.
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        // 왼손의 IK 위치와 방향을 설정합니다.
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 3);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 3);
    }
}
