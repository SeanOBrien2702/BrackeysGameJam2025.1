using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectileSpritePicker : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        int random = Random.Range(0,spriteList.Count);
        sr.sprite = spriteList[random];
    }
}
