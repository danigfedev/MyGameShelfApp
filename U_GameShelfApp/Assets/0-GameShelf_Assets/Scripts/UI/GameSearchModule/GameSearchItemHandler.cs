using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSearchItemHandler : MonoBehaviour
{
    public Sprite noCoverSprite;

    [Header("UI Items")]
    public TMPro.TextMeshProUGUI gameText;
    public Image gameCoverUI;

    private GameSearchData gameData;
    private Texture2D gameCover;

    public void InitializeItem(GameSearchData gameSearchData, Texture2D cover)
    {
        gameData = gameSearchData;
        gameCover = cover;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        gameText.text = gameData.name;
        if (gameCover == null)
        {
            gameCoverUI.sprite = noCoverSprite;
            return;
        }

        gameCoverUI.sprite = Sprite.Create(gameCover, new Rect(0.0f, 0.0f,
                                            gameCover.width, gameCover.height),
                                            new Vector2(0.5f, 0.5f),
                                            100.0f);
    }
}
