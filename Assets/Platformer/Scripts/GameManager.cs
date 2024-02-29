using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    
    [Header("Animated Textures")]
    public List<Material> animatedMaterials;
    public List<Vector2> animationOffsets;

    private float _timestamp;
    private Camera _cam;

    private void Start()
    {
        worldText.text = "World \n 1-1";
        characterText.text = "Mario";
        scoreText.text = "000000";
        coinsText.text = "00";
        
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
        UpdateRayCast();
    }

    private void UpdateGUI()
    {
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeStr = $"Time \n {intTime}";
        timeText.text = timeStr;
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
}
