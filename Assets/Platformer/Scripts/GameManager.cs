using System;
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
    public Material questionBoxMaterial;

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
        questionBoxMaterial.mainTextureOffset = new Vector2(-1, -0.2f);
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
        questionBoxMaterial.mainTextureOffset -= new Vector2 (0, 0.2f);
    }

    private void UpdateRayCast()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log($"Hit object: {hitObject.name}");
            if (hitObject.CompareTag("Breakable"))
            {
                Destroy(hitObject);
            }
        }
    }
}
