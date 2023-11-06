using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RussSurvivor.Runtime
{
    public class MouthRandom : MonoBehaviour
    {
        public List<Sprite> sprites;
        public float duration;

        public Image img; 
        IEnumerator ChangeMouth()
        {
            img.sprite = sprites[Random.Range(0, sprites.Count)];
            yield return new WaitForSeconds(duration);
            StartCoroutine(ChangeMouth());
        }

        private void OnEnable()
        {
            StartCoroutine(ChangeMouth());
        }
    }
}
