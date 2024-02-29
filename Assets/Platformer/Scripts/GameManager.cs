using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI timeText;
    public GameObject gameOver;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    
    [Header("Animated Textures")]
    public List<Material> animatedMaterials;
    public List<Vector2> animationOffsets;

    private float _timestamp;
    private Camera _cam;
    private int _score;
    private int _coins;

    private void Start()
    {
        worldText.text = "World \n 1-1";
        characterText.text = "Mario";
        scoreText.text = "000000";
        coinsText.text = "x00";
        
        _timestamp = Time.realtimeSinceStartup;
        _cam = Camera.main;
        
        if (animatedMaterials.Count != animationOffsets.Count)
        {
            Debug.LogError("Animated materials and animation offsets lists must have the same size");
        }
        
        foreach (var material in animatedMaterials)
        {
            material.mainTextureOffset = new Vector2(0, 0);
        }
    }

    void Update()
    {
        UpdateGUI();
        UpdateAnimatedTexture();
    }
    
    public void AddCoin()
    {
        _coins++;
        coinsText.text = $"x{_coins:00}";
    }
    
    public void GameOver()
    {
        gameOver.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
    }
    
    public void AddScore(int score)
    {
        _score += score;
        scoreText.text = _score.ToString("D6");
    }
    
    public void TimeReset()
    {
        _timestamp = Time.realtimeSinceStartup;
    }

    private void UpdateGUI()
    {
        int intTime = 100 - (int)Time.realtimeSinceStartup;
        string timeStr = $"Time \n {intTime}";
        timeText.text = timeStr;
        
        if (intTime <= 0)
        {
            timeText.text = "Time \n 0000";
            GameOver();
        }
    }
    
    private void UpdateAnimatedTexture()
    {
        if (!(Time.realtimeSinceStartup > _timestamp)) return;
        _timestamp = Time.realtimeSinceStartup + 1;
        
        for (int i = 0; i < animatedMaterials.Count; i++)
        {
            animatedMaterials[i].mainTextureOffset += animationOffsets[i];
        }
    }

    /*legacy breakable bricks
    private void UpdateRayCast()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            //Debug.Log($"Hit object: {hitObject.name}");
            if (hitObject.CompareTag("Breakable"))
            {
                Destroy(hitObject);
            }
        }
    }
    */
}
