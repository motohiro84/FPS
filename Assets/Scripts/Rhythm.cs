using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    [ColorUsage(false, true, 0f, 8f, 0.125f, 3f)]
    public Color coloring1;
    [ColorUsage(false, true, 0f, 8f, 0.125f, 3f)]
    public Color coloring2;
    private MeshRenderer r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<MeshRenderer>();
        r.material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if ( Music.IsJustChangedBar() )
        {
            r.material.SetColor("_EmissionColor", coloring2);
        }
        else if ( Music.IsJustChangedBeat() )
        {
            r.material.SetColor("_EmissionColor", coloring1);
        }
    }
}
