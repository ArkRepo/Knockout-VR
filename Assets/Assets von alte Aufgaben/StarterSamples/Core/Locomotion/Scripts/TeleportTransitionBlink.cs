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

/// <summary>
/// This transition will cause the screen to quickly fade to black, perform the repositioning, and then fade
/// the view back to normal.
/// </summary>
public class TeleportTransitionBlink : TeleportTransition
{
    /// <summary>
    /// How long the transition takes. Usually this is greater than Teleport Delay.
    /// </summary>
    [Tooltip("How long the transition takes. Usually this is greater than Teleport Delay.")]
    [Range(0.01f, 2.0f)]
    public float TransitionDuration = 0.5f;

    /// <summary>
    /// At what percentage of the elapsed transition time does the teleport occur?
    /// </summary>
    [Tooltip("At what percentage of the elapsed transition time does the teleport occur?")]
    [Range(0.0f, 1.0f)]
    public float TeleportDelay = 0.5f;

    /// <summary>
    /// Fade to black over the duration of the transition.
    /// </summary>
    [Tooltip("Fade to black over the duration of the transition")]
    public AnimationCurve FadeLevels = new AnimationCurve(new Keyframe[3]
        { new Keyframe(0, 0), new Keyframe(0.5f, 1.0f), new Keyframe(1.0f, 0.0f) });

    /// <summary>
    /// When the teleport state is entered, start a coroutine that will handle the
    /// actual transition effect.
    /// </summary>
    protected override void LocomotionTeleportOnEnterStateTeleporting()
    {
        StartCoroutine(BlinkCoroutine());
    }

    /// <summary>
    /// This coroutine will fade out the view, perform the teleport, and then fade the view
    /// back in.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator BlinkCoroutine()
    {
        LocomotionTeleport.IsTransitioning = true;
        float elapsedTime = 0;
        var teleportTime = TransitionDuration * TeleportDelay;
        var teleported = false;
        while (elapsedTime < TransitionDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (!teleported && elapsedTime >= teleportTime)
            {
                teleported = true;
                LocomotionTeleport.DoTeleport();
            }
            //float fadeLevel = FadeLevels.Evaluate(elapsedTime / TransitionDuration);
            //OVRInspector.instance.fader.SetFadeLevel(fadeLevel);
        }

        //OVRInspector.instance.fader.SetFadeLevel(0);

        LocomotionTeleport.IsTransitioning = false;
    }
}
