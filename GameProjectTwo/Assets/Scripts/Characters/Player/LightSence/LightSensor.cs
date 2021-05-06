using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] PlayerNotoriousLevels notoriousLevels;

    [SerializeField] RenderTexture lightTexA;
    private RenderTexture lightTexB;
    [SerializeField] Texture2D mask2DTexure;
    private int[] indexInCircle;
    // Start is called before the first frame update
    void Start()
    {
        notoriousLevels = GetComponentInParent<PlayerNotoriousLevels>();


        lightTexB = RenderTexture.GetTemporary(lightTexA.width, lightTexA.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        
        List<int> indexInC = new List<int>();
        Color[] colors = mask2DTexure.GetPixels();
        int length = colors.Length;


        for (int i = 0; i < length; i++)
        {
            if(colors[i].r > 0.9f)
            {
                indexInC.Add(i);
            }
        }

        indexInCircle = indexInC.ToArray();
        
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

        notoriousLevels.SetPlLuminosity(maxL);
    }
    
    void FollowTarget()
    {
        transform.position = follow.position;
    }

    Color[] GetColors()
    {
        //THX: https://www.youtube.com/watch?v=NYysvuyivc4
        //TODO : GET/RELICE TEMP IS NOT NESSSESERY ???
        lightTexB = RenderTexture.GetTemporary(lightTexA.width, lightTexA.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        Graphics.Blit(lightTexA, lightTexB);
        mask2DTexure = new Texture2D(lightTexB.width, lightTexB.height);
        mask2DTexure.ReadPixels(new Rect(0, 0, lightTexB.width, lightTexB.height), 0, 0);
        mask2DTexure.Apply();

        RenderTexture.ReleaseTemporary(lightTexB);

        return mask2DTexure.GetPixels();
    }

    float GetLuminosity()
    {
        Color avgColor = Color.black;
        Color[] colors = GetColors();

        for (int i = 0; i < indexInCircle.Length; i++)
        {
            avgColor += colors[indexInCircle[i]];
        }

        avgColor /= indexInCircle.Length;
        return Mathf.Max(avgColor.r, avgColor.g, avgColor.b);
    }
}
