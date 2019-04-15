using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Assertions;
using TMPro;

public class TourStateEditPanel : MonoBehaviour
{
    public TMP_InputField urlInput;
    public Button imageUpdateButton;

    void Awake()
    {
        Assert.IsNotNull(urlInput);
        Assert.IsNotNull(imageUpdateButton);

        imageUpdateButton.onClick.AddListener(() => StartCoroutine(UpdatePanoramaImage(urlInput.text)));
    }

    IEnumerator UpdatePanoramaImage(string url)
    {
        EditorScene.instance.panorama.mainTexture = EditorScene.instance.panorama.defaultTexture;

        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(url);

        yield return textureRequest.SendWebRequest();

        if (textureRequest.isHttpError || textureRequest.isNetworkError)
            yield break;

        Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);

        EditorScene.instance.panorama.mainTexture = texture;
    }
}
