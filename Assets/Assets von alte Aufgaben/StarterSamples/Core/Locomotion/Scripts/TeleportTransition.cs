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

using UnityEngine;
using System.Collections;

/// <summary>
/// Teleport transitions manage the actual relocation of the player from the current position and orientation
/// to the teleport destination.
/// All teleport transition behaviors derive from this class, primarily for type safety
/// within the LocomotionTeleport to track the current transition type.
/// </summary>
public abstract class TeleportTransition : TeleportSupport
{
    protected override void AddEventHandlers()
    {
        LocomotionTeleport.EnterStateTeleporting += LocomotionTeleportOnEnterStateTeleporting;
        base.AddEventHandlers();
    }

    protected override void RemoveEventHandlers()
    {
        LocomotionTeleport.EnterStateTeleporting -= LocomotionTeleportOnEnterStateTeleporting;
        base.RemoveEventHandlers();
    }

    /// <summary>
    /// When the teleport state is entered, simply move the player to the new location
    /// without any delay or other side effects.
    /// If the transition is not immediate, the transition handler will need to set the LocomotionTeleport.IsTeleporting
    /// to true for the duration of the transition, setting it to false when the transition is finished which will
    /// then allow the teleport state machine to switch to the PostTeleport state.
    /// </summary>
    protected abstract void LocomotionTeleportOnEnterStateTeleporting();
}
