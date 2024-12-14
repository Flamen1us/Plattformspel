using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    VisualElement root;
    public ProgressBar progress;
    public GameSession pla;
    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
    void Start()
    {
        progress = root.Q<ProgressBar>("HealthBar");
    }
    private void Update()
    {
        progress.value = pla.playerHealth;
    }
}
