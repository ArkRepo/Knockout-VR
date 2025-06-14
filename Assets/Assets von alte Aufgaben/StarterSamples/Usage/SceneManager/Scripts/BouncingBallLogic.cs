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

using System.Collections;
using UnityEngine;

public class BouncingBallLogic : MonoBehaviour
{
    [SerializeField] private float TTL = 5.0f;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip bounce;
    [SerializeField] private AudioClip loadball;
    [SerializeField] private Material visibleMat;
    [SerializeField] private Material hiddenMat;
    private AudioSource audioSource;
    private Transform centerEyeCamera;
    private bool isVisible = true;

    private float timer = 0f;
    private bool isReleased = false;
    private bool isReadyForDestroy = false;

    private void OnCollisionEnter() => audioSource.PlayOneShot(bounce);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(loadball);
        centerEyeCamera = OVRManager.instance.GetComponentInChildren<OVRCameraRig>().centerEyeAnchor;
    }


    private void Update()
    {
        if (!isReleased) return;
        UpdateVisibility();
        timer += Time.deltaTime;
        if (!isReadyForDestroy && timer >= TTL)
        {
            isReadyForDestroy = true;
            float clipLength = pop.length;
            audioSource.PlayOneShot(pop);
            StartCoroutine(PlayPopCallback(clipLength));
        }
    }

    private void UpdateVisibility()
    {
        Vector3 displacement = centerEyeCamera.position - this.transform.position;
        Ray ray = new Ray(this.transform.position, displacement);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, displacement.magnitude))
        {
            if (info.collider.gameObject != this.gameObject)
            {
                SetVisible(false);
            }
        }
        else
        {
            SetVisible(true);
        }
    }

    private void SetVisible(bool setVisible)
    {
        if (isVisible && !setVisible)
        {
            GetComponent<MeshRenderer>().material = hiddenMat;
            isVisible = false;
        }

        if (!isVisible && setVisible)
        {
            GetComponent<MeshRenderer>().material = visibleMat;
            isVisible = true;
        }
    }

    public void Release(Vector3 pos, Vector3 vel, Vector3 angVel)
    {
        isReleased = true;
        transform.position = pos; // set the orign to match target
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = vel;
        GetComponent<Rigidbody>().angularVelocity = angVel;
    }

    private IEnumerator PlayPopCallback(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(gameObject);
    }
}
