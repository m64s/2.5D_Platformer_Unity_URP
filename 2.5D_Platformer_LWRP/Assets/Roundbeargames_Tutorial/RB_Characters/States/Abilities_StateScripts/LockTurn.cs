﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/LockTurn")]
    public class LockTurn : StateData
    {
        [Range(0f, 1f)]
        public float LockTiming;

        [Range(0f, 1f)]
        public float UnlockTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= LockTiming && !characterState.ROTATION_DATA.LockTurn)
            {
                characterState.ROTATION_DATA.LockTurn = true;
                characterState.ROTATION_DATA.UnlockTiming = UnlockTiming;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}