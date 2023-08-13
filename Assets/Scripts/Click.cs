using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
       gameObject.SetActive(false);
       GetComponent<Image>().rectTransform.localScale = Vector2.zero;
    }
}