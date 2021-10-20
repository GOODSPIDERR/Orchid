using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class UIFunctions : MonoBehaviour
{
    public Transform lbTop, lbBottom;

    public float initialYPositionTop, initialYPositionBottom;
    // Start is called before the first frame update
    private void Start()
    {
        initialYPositionTop = lbTop.localPosition.y;
        initialYPositionBottom = lbBottom.localPosition.y;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    [Button]
    public void LetterBoxing()
    {
        lbTop.DOLocalMoveY(500f, 0.5f);
        lbBottom.DOLocalMoveY(-500f, 0.5f);
    }

    [Button]
    public void UnletterBox()
    {
        lbTop.DOLocalMoveY(initialYPositionTop, 0.5f);
        lbBottom.DOLocalMoveY(initialYPositionBottom, 0.5f);
    }
}
