/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

/// <summary>
/// Simply aggregates accessors.
/// </summary>
public class LocomotionController : MonoBehaviour
{
    public OVRCameraRig CameraRig;

    //public CharacterController CharacterController;
    public CapsuleCollider CharacterController;

    //public OVRPlayerController PlayerController;
    public SimpleCapsuleWithStickMovement PlayerController;

    void Start()
    {
        /*
        if (CharacterController == null)
        {
            CharacterController = GetComponentInParent<CharacterController>();
        }
        Assert.IsNotNull(CharacterController);
        */
        //if (PlayerController == null)
        //{
        //PlayerController = GetComponentInParent<OVRPlayerController>();
        //}
        //Assert.IsNotNull(PlayerController);
        if (CameraRig == null)
        {
            CameraRig = FindObjectOfType<OVRCameraRig>();
        }

        Assert.IsNotNull(CameraRig);
#if UNITY_EDITOR
        OVRPlugin.SendEvent("locomotion_controller", (SceneManager.GetActiveScene().name == "Locomotion").ToString(),
            "sample_framework");
#endif
    }
}
