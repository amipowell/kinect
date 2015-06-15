using UnityEngine;
using System.Collections;

public class MammothScript : MonoBehaviour
{

    public Material material;
    public Renderer rend;

    public void ChangeBackground()
    {
        rend.material = material;
    }
}
