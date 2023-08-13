using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameControl : MonoBehaviour 
{

    [SerializeField] public GameObject[] avatarPrefabs;
    [SerializeField] public List<Image> goodRepImages = new List<Image>();
    [SerializeField] public List<Image> badRepImages = new List<Image>();
    [SerializeField] public GameObject Heart, Balloon, Star, Snow, Thunder, Candy, Rose;
    // last minute start
    [SerializeField] public Dice dice;
    // last minute end

    private static GameObject PointsCount, KeyIcon, Board, endnote, Dice, FloatingStone1, FloatingStone, next;

    public static int diceSideThrown = 0;
    public static int PlayerStartWaypoint = 0;
    public int points = 0;

    public static bool gameOver = false;

    audioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    void Start () {

        LoadAvatar(); // Load the character which is selected in Scene 1

        PlayerStartWaypoint = 0;
        diceSideThrown = 0;
        points = 0;
        gameOver = false;

        // Assign the gameObject
        PointsCount = GameObject.Find("PointsCount");
        KeyIcon = GameObject.Find("KeyIcon");
        Board = GameObject.Find("Board");
        endnote = GameObject.Find("endnote");
        Dice = GameObject.Find("Dice");
        FloatingStone1 = GameObject.Find("FloatingStone1");
        FloatingStone = GameObject.Find("FloatingStone");
        next = GameObject.Find("next");


        // New List of shuffled Images
        goodRepImages = Shuffle(goodRepImages);
        badRepImages = Shuffle(badRepImages);
        
        // Enable the GameObjects when game starts
        KeyIcon.gameObject.SetActive(true);
        endnote.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        avatarPrefabs[AvatarSelection.avatarIndex].gameObject.SetActive(true);

        avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().moveAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(points < 5)
        {
            PointsCount.GetComponent<Text>().text = "";
        }
        if (avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypointIndex > 
            PlayerStartWaypoint + diceSideThrown)
        {
            // To Move One Step Forward
            if(PlayerStartWaypoint+diceSideThrown == 4){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[0], 5, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 7){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[1], 8, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 19){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[3], 20, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 23){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[7], 24, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 31){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[4], 32, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 33){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[5], 34, 10);
            }
            if(PlayerStartWaypoint+diceSideThrown == 35){
                audioManager.PlaySFX(audioManager.goodRep);
                PopAndPoint(goodRepImages[6], 36, 10);
            }

            // To Move One Step Backward if we reach waypoint+1
            if(PlayerStartWaypoint+diceSideThrown == 10){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[0], 9, -5);
            }
            if(PlayerStartWaypoint+diceSideThrown == 14){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[1], 13, -5);
            }
            if(PlayerStartWaypoint+diceSideThrown == 16){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[2], 15, -5);
            }
            if(PlayerStartWaypoint+diceSideThrown == 28){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[3], 27, -5);
            }
            if(PlayerStartWaypoint+diceSideThrown == 37){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[4], 36, -5);
            }
            if(PlayerStartWaypoint+diceSideThrown == 40){
                audioManager.PlaySFX(audioManager.badRep);
                PopAndPoint(badRepImages[5], 39, -5);
            }
            avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().moveAllowed = false;
            PlayerStartWaypoint = avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypointIndex - 1;
            // last minute start
            Dice.gameObject.SetActive(true);
            dice.EnableDice();
            // last minute end
        }

        // Check If Avatar reach the last waypoint
        if (avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypointIndex == 
            avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypoints.Length)
        {
            gameOver = true; // Game Over
            StartCoroutine(ScaleAndMoveAvatarToZero());   
        }
    }

    // Function to Load the character selected in scene 1
    private void LoadAvatar()
    {
        int avatarIndex = PlayerPrefs.GetInt("avatarIndex");
        Instantiate(avatarPrefabs[avatarIndex]);
    }

    // Function to move the avatar through waypoints
    public void Move()
    {
        avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().moveAllowed = true;
        // Dice.gameObject.SetActive(false);
    }

    // Final shower
    public void Shower()
    {
        Candy.gameObject.SetActive(true);

        if (points > 4 && points <= 9) // Key 1 shower
        {
            Balloon.gameObject.SetActive(true);
        }

        if(points > 9 && points <= 14) // Key 2 shower
        {
            Snow.gameObject.SetActive(true);
            Balloon.gameObject.SetActive(true);
        }

        if(points > 14 && points <= 19) // Key 3 shower
        {
            Thunder.gameObject.SetActive(true);
            Snow.gameObject.SetActive(true);
            Balloon.gameObject.SetActive(true);      
        }

        if(points > 19 && points <= 24) // Key 4 shower
        {
            Rose.gameObject.SetActive(true);
            Thunder.gameObject.SetActive(true);
            Snow.gameObject.SetActive(true);
            Balloon.gameObject.SetActive(true);
        }

        if(points > 24 && points <= 29) // Key 5 shower
        {
            Rose.gameObject.SetActive(true);
            Heart.gameObject.SetActive(true);
            Thunder.gameObject.SetActive(true);
            Snow.gameObject.SetActive(true);
            Balloon.gameObject.SetActive(true);
        }

        if(points > 29 && points <= 120) // Key 5 shower
        {
            Star.gameObject.SetActive(true);
            Heart.gameObject.SetActive(true);
            Thunder.gameObject.SetActive(true);
            Snow.gameObject.SetActive(true);
            Balloon.gameObject.SetActive(true);
            Rose.gameObject.SetActive(true);
        }
    }


    // To Shuffle the list
    public static List<Image> Shuffle<Image>(List<Image> inputList)
    {
        List<Image> copyOfInputList = new List<Image>();
        copyOfInputList.AddRange(inputList);
        List<Image> shuffledList = new List<Image>();
        Random random = new Random();
        while (copyOfInputList.Count > 0)
        {
            int randomIndex = random.Next(0, copyOfInputList.Count);
            shuffledList.Add(copyOfInputList[randomIndex]);
            copyOfInputList.RemoveAt(randomIndex);
        }
        return shuffledList;
    }

    // To Move forward and backward + Image pop up
    public void PopAndPoint(Image image, int newWaypointIndex, int pointChange)
    {
        image.rectTransform.LeanScale(Vector2.one * 1.8f, 3.0f).setEaseInQuad();
        LeanTween.rotateAround(image.gameObject, Vector3.back, 360f, 3.0f);
        avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().transform.position = avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypoints[newWaypointIndex].transform.position;
        avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypointIndex = newWaypointIndex;
        avatarPrefabs[AvatarSelection.avatarIndex].GetComponent<FollowThePath>().waypointIndex += 1;
        Move();
        points += pointChange;
        PointsCount.GetComponent<Text>().text = "" + points;
    }

    private IEnumerator ScaleAndMoveAvatarToZero()
    {
        float duration = 3.0f; // Duration for scaling and moving
        float startTime = Time.time;

        Vector3 initialScale = avatarPrefabs[AvatarSelection.avatarIndex].transform.localScale;
        Vector3 targetScale = Vector3.one * 1.8f;

        Vector3 initialPosition = avatarPrefabs[AvatarSelection.avatarIndex].transform.position;
        Vector3 targetPosition = new Vector3(-0.2f, 0, 0);

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;

            // Scale the avatar
            avatarPrefabs[AvatarSelection.avatarIndex].transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

            // Move the avatar
            avatarPrefabs[AvatarSelection.avatarIndex].transform.position =
                Vector3.Lerp(initialPosition, targetPosition, progress);

            yield return null;
        }

        // Ensure the final values are set
        avatarPrefabs[AvatarSelection.avatarIndex].transform.localScale = targetScale;
        avatarPrefabs[AvatarSelection.avatarIndex].transform.position = targetPosition;

        // Display any final game elements (e.g., next button)
        Board.gameObject.SetActive(false);
        Dice.gameObject.SetActive(false);
        PointsCount.gameObject.SetActive(false);
        FloatingStone1.gameObject.SetActive(false);
        FloatingStone.gameObject.SetActive(false);
        endnote.gameObject.SetActive(true);
        Shower();
        StartCoroutine(waitForNextEnable());
    }

    private IEnumerator waitForNextEnable()
    {
        yield return new WaitForSeconds(5f);
        next.gameObject.SetActive(true);
    }
}