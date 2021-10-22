using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;

public class CameraShake : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private CinemachineVirtualCamera virtualCamera;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float amplitude = 0f;
        
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    NoiseShake();
        //}

        amplitude -= Time.deltaTime * 6f;
        if (amplitude <= 0f)
        {
            amplitude = 0f;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
    }

    public void LeftShake()
    {
        
        

    }
    
    [Button]
    public void RightShake()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 5f, transform.rotation.eulerAngles.z), 0.2f));
    }

    public void NoiseShake()
    {
        DOTween.To(() => amplitude, x => amplitude = x, 3f, 0.1f);
    }
}
