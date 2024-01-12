using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class SkyboxHandler : MonoBehaviour
{
    #region constants
    private const string ZENITHCOLOR = "_ZenithColor";
    private const string HORIZONCOLOR = "_HorizonColor";
    private const string NADIRCOLOR = "_NadirColor";
    #endregion
    public static SkyboxHandler Instance;
    [Range(0f, 1f)]
    [SerializeField] private float dayNightProgress;
    [SerializeField] private float dayNightSpeed;

    [SerializeField] private Material defaultSkyBox;
    [SerializeField] private Material skyBox;
    [SerializeField] private bool useCustomSkybox;
    [SerializeField] private Light sun;
    [SerializeField] private Gradient zenithGradient;
    [SerializeField] private Gradient horizonGradient;
    [SerializeField] private Gradient nadirGradient;


    private void OnEnable()
    {
        useCustomSkybox = false;
        SetSkybox(skyBox);
    }
    private void OnDisable()
    {
        useCustomSkybox = true;
        SetSkybox(defaultSkyBox);
    }

    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNightProgress >= 1f)
        {
            dayNightProgress = 0;
        }
        else
        {
            dayNightProgress += Time.deltaTime * dayNightSpeed;
        }
        SetSkyboxColors();
        
    }

    [Button]
    private void SetSkyboxColors()
    {
        skyBox.SetColor(ZENITHCOLOR, zenithGradient.Evaluate(dayNightProgress));
        skyBox.SetColor(HORIZONCOLOR, horizonGradient.Evaluate(dayNightProgress));
        skyBox.SetColor(NADIRCOLOR, nadirGradient.Evaluate(dayNightProgress));
    }

    [Button]
    private void ToggleSkybox()
    {
        useCustomSkybox = !useCustomSkybox;
        SetSkybox(useCustomSkybox ? skyBox : defaultSkyBox);
    }

    private void SetSkybox(Material skyboxMaterial)
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}
