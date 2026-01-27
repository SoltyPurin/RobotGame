using UniRx;
using UnityEditor;

/// <summary>
/// インスペクタに表示できるReactiveProperty<int>
/// </summary>
[System.Serializable]
public class FloatReactiveProperty : ReactiveProperty<float>
{
    public FloatReactiveProperty()
    {

    }
}

#if UNITY_EDITOR
/// <summary>
/// IntReactivePropertyを直接インスペクタからいじれるようにする（Toggleなしにできる）
/// </summary>
[CustomPropertyDrawer(typeof(FloatReactiveProperty))]
public class AddInspectorDisplayDrawer : InspectorDisplayDrawer
{ }

#endif