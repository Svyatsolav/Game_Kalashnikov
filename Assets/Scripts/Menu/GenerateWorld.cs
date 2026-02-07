using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateWorld : MonoBehaviour
{
    [SerializeField] SpriteRenderer firstBlock;
    [SerializeField] SpriteRenderer lastBlock;
    [SerializeField] SpriteRenderer[] otherBlocks;

    [SerializeField] Sprite[] firstBlock_sprites;
    [SerializeField] Sprite[] lastBlock_sprites;
    [SerializeField] Sprite[] otherBlocks_sprites;

    void Awake()
    {
        firstBlock.sprite = firstBlock_sprites[PlayerPrefs.GetInt("WorldType")];
        lastBlock.sprite = lastBlock_sprites[PlayerPrefs.GetInt("WorldType")];
        for(int i = 0; i < otherBlocks.Length; i++)
        {
            otherBlocks[i].sprite = otherBlocks_sprites[PlayerPrefs.GetInt("WorldType")];
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
