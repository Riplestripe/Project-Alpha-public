using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName ="Trail Config", menuName ="Guns/Gun Trail Congig", order = 4)]
public class TrailConfigScritpbleObject : ScriptableObject
{
    public Material Material;
    public AnimationCurve WidthCurve;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;
    public Gradient Color;

    public float MissDistance = 100f;
    public float SimulationSpeed = 100f;
}
