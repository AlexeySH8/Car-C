using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public static ChangeColor Instance { get; private set; }

    [SerializeField] private Material[] _cityMaterials;
    [SerializeField] private Material[] _sceneMaterials;
    [SerializeField] private Material _explodedMaterial;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ChangeSceneMaterial();
        ChangeAdsMaterials();
    }

    public Material[] ChangeMaterialAfterExplosion(int length)
    {
        var materials = new Material[length];
        for (int i = 0; i < length; i++)
            materials[i] = _explodedMaterial;
        return materials;
    }

    public void ChangeAdsMaterials()
    {
        foreach (var item in SpawnAds.Instance.AllAds)
        {
            if (item.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.material = _cityMaterials[RepeatBackground.CurrentCity];
            }
        }
    }

    public void ChangeSceneMaterial() =>
        RenderSettings.skybox = _sceneMaterials[RepeatBackground.CurrentCity];
}
