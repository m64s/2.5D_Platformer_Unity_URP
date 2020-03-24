﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MeleeWeapon : MonoBehaviour
    {
        public CharacterControl control;
        public BoxCollider PickUpCollider;
        public BoxCollider AttackCollider;
        public TriggerDetector triggerDetector;
        public Vector3 CustomPosition = new Vector3();
        public Vector3 CustomRotation = new Vector3();

        [Header("WeaponThrow")]
        public Vector3 ThrowOffset = new Vector3();
        public bool IsThrown;
        public bool FlyForward;
        public float FlightSpeed;
        public float RotationSpeed;
        public CharacterControl Thrower;

        private void Start()
        {
            IsThrown = false;
        }

        private void Update()
        {
            if (control != null)
            {
                PickUpCollider.enabled = false;
                AttackCollider.enabled = true;
            }
            else
            {
                PickUpCollider.enabled = true;
                AttackCollider.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (IsThrown)
            {
                if (FlyForward)
                {
                    this.transform.position += (Vector3.forward * FlightSpeed * Time.deltaTime);
                }
                else
                {
                    this.transform.position -= (Vector3.forward * FlightSpeed * Time.deltaTime);
                }

                this.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
            }
        }

        public static bool IsWeapon(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<MeleeWeapon>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DropWeapon()
        {
            MeleeWeapon w = control.animationProgress.HoldingWeapon;

            if (w != null)
            {
                w.transform.parent = null;

                if (control.IsFacingForward())
                {
                    w.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else
                {
                    w.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                }
                
                w.transform.position = control.transform.position + (Vector3.up * 0.0225f);

                control.animationProgress.HoldingWeapon = null;
                control = null;
                w.triggerDetector.control = null;
            }
        }

        public void ThrowWeapon()
        {
            MeleeWeapon w = control.animationProgress.HoldingWeapon;

            if (w != null)
            {
                w.transform.parent = null;

                if (control.IsFacingForward())
                {
                    w.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else
                {
                    w.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }

                FlyForward = control.IsFacingForward();

                w.transform.position = control.transform.position + (Vector3.up * ThrowOffset.y);
                w.transform.position += (control.transform.forward * ThrowOffset.z);

                Thrower = control;
                control.animationProgress.HoldingWeapon = null;
                control = null;
                w.triggerDetector.control = null;

                IsThrown = true;
            }
        }
    }
}