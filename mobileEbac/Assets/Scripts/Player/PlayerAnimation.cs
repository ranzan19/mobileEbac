using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleStart = 1f;
    public Ease ease = Ease.OutBack;
    
    void Start()
    {
        transform.localScale = Vector3.zero;
        Invoke("ScalePlayer", 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ScalePlayer();
        }
    }

    public void ScalePlayer()
    {
        transform.DOScale(scaleStart, scaleDuration).SetEase(ease).SetLoops(1, LoopType.Incremental);
    }
}
