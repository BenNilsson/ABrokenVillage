using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithRandomTexture : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    public SpriteRenderer rend;

    private void Start()
    {
        rend.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
