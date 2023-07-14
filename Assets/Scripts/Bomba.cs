using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomba : MonoBehaviour
{
    Sequence sequence;
    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence.Pause();

        sequence.Append(GetComponent<SpriteRenderer>().DOColor(Color.red, 0.5f)
);
        sequence.SetLoops(-1);
    }

    public void Parpadear()
    {
        sequence.Play();
    }
    public void NoParpadear()
    {
        sequence.Pause();
        GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);
    }
}
