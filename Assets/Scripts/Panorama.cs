using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Panorama : MonoBehaviour
{
    public Texture defaultTexture;

    public Texture mainTexture = null;
    public Quaternion mainTextureOrientation = Quaternion.identity;

    public Texture nextTexture = null;
    public Quaternion nextTextureOrientation = Quaternion.identity;

    public float transition = 0.0f;

    private Renderer _renderer;
    private MaterialPropertyBlock _materialProperties;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialProperties = new MaterialPropertyBlock();
    }

    void Update()
    {
        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        _renderer.GetPropertyBlock(_materialProperties);

        // main texture info
        _materialProperties.SetTexture("_MainTex", mainTexture ? mainTexture : defaultTexture);

        var o = mainTextureOrientation;
        _materialProperties.SetVector("_MainOrientation", new Vector4(
            o.x, o.y, o.z, o.w));

        // next texture info
        _materialProperties.SetTexture("_NextTex", nextTexture ? nextTexture : defaultTexture);

        o = nextTextureOrientation;
        _materialProperties.SetVector("_NextOrientation", new Vector4(
            o.x, o.y, o.z, o.w));

        // transition
        _materialProperties.SetFloat("_Transition", transition);

        _renderer.SetPropertyBlock(_materialProperties);
    }
}
