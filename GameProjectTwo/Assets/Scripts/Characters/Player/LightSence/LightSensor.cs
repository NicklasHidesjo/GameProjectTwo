using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This can be optimised by changein to only one colorchanel.
/// </summary>
public class LightSensor : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] PlayerNotoriousLevels notoriousLevels;

    [SerializeField] RenderTexture lightTexA;
    private RenderTexture lightTexB;
    [SerializeField] Texture2D mask2DTexure;
    private int[] indexInCircle;
    Color[] colors;


    // Start is called before the first frame update
    void Start()
    {
        //  notoriousLevels = GetComponentInParent<PlayerNotoriousLevels>();
        notoriousLevels = FindObjectOfType<PlayerNotoriousLevels>();

        lightTexB = RenderTexture.GetTemporary(lightTexA.width, lightTexA.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        
        List<int> indexInC = new List<int>();
        Color[] colors = mask2DTexure.GetPixels();
        int length = colors.Length;

        //TODO : rewright this function to math
        for (int i = 0; i < length; i++)
        {
            if(colors[i].r > 0.9f)
            {
                indexInC.Add(i);
            }
        }

        indexInCircle = indexInC.ToArray();

        
        mask2DTexure = new Texture2D(lightTexB.width, lightTexB.height);
        colors = new Color[lightTexB.width * lightTexB.height];
    }


    public void SetFollowTarget(Transform t)
    {
        follow = t;
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        float maxL = GetLuminosity();
        //print(maxL);
        
        notoriousLevels.SetPlLuminosity(maxL);
    }
    
    void FollowTarget()
    {
        transform.parent.position = follow.position;
        transform.position = follow.position + Vector3.up * 1.5f;
    }

    Color[] GetColors()
    {
        //THX: https://www.youtube.com/watch?v=NYysvuyivc4
        //TODO : GET/RELICE TEMP IS NOT NESSSESERY ???
        
        lightTexB = RenderTexture.GetTemporary(lightTexA.width, lightTexA.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightTexA, lightTexB);
        
        mask2DTexure.ReadPixels(new Rect(0, 0, lightTexB.width, lightTexB.height), 0, 0);
        mask2DTexure.Apply();

        RenderTexture.ReleaseTemporary(lightTexB);

        return mask2DTexure.GetPixels();
    }

    float GetLuminosity()
    {
        Color avgColor = Color.black;
        colors = GetColors();

        for (int i = 0; i < indexInCircle.Length; i++)
        {
            avgColor += colors[indexInCircle[i]];
        }

        avgColor /= indexInCircle.Length;
        return Mathf.Max(avgColor.r, avgColor.g, avgColor.b);
    }
}
