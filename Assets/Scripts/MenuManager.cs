using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Play;
    [SerializeField] private Ease MotionType;
    [SerializeField] private Image img_Hand;
    [SerializeField] private GameObject tapToPlayArea;
    private void Start()
    {
        txt_Play.transform.DOScale(1.2f, 0.5f).SetLoops(1000, LoopType.Yoyo).SetEase(MotionType);
        img_Hand.transform.DOMoveX(300f, 1f).SetLoops(1000, LoopType.Yoyo).SetEase(MotionType);
       
    }
    private void Update()
    {
        CloseTapToPlayArea();
    }

    private void CloseTapToPlayArea()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tapToPlayArea.gameObject.SetActive(false);
        }
    }
}
