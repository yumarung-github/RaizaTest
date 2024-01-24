using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BuilderTest : ScriptableObject
{
    public string personName;
    public BuilderTest Name(string name)
    {
        this.personName = name;
        return this;
    }
}
