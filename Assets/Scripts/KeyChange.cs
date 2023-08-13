using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyChange : MonoBehaviour
{
    [SerializeField] private Image imageComponent; // Reference to the Image component of the key
    [SerializeField] private GameControl score;    // Reference to the GameControl script to access the points
    private RectTransform rectTransform;            // Reference to the RectTransform component of the key
    [SerializeField] private Sprite[] keySprites;  // Array of key sprites corresponding to score thresholds

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        // Get the Image component of the key GameObject
        imageComponent = GetComponent<Image>();

        // Get the RectTransform component of the key GameObject
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateKeySprite();
    }

    private void UpdateKeySprite()
    {
        // Iterate through keySprites array to find a matching sprite based on the score
        for (int i = 0; i < keySprites.Length; i++)
        {
            if (score.points >= GetScoreThreshold(i))
            {
                imageComponent.sprite = keySprites[i];
            }
        }
    }

    private int GetScoreThreshold(int index)
    {
        // Calculate the score threshold for a specific index
        return (index + 1) * 5;
    }
}
