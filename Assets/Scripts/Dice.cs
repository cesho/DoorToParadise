using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private bool coroutineAllowed = true;
    public GameControl move;

    audioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

	// Use this for initialization
	private void Start () {
        coroutineAllowed = true;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];
	}

    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            audioManager.PlaySFX(audioManager.dice);
            yield return new WaitForSeconds(0.05f);
        }

        GameControl.diceSideThrown = randomDiceSide + 1;
        move.Move();
        // last minute changes start
        //coroutineAllowed = true; 
        StartCoroutine(waitForDiceDisable());
        // last minute changes end
    }

    private IEnumerator waitForDiceDisable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    // last minute changes start
    public void EnableDice()
    {
        coroutineAllowed = true;
    }
    // last minute changes end
}
