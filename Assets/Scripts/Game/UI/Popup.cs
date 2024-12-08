using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        Vector3 endScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(endScale, 1).SetEase(Ease.InOutSine);
    }
}
