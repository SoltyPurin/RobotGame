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

/// <summary>
/// IntReactivePropertyを直接インスペクタからいじれるようにする（Toggleなしにできる）
/// </summary>
[CustomPropertyDrawer(typeof(FloatReactiveProperty))]
public class AddInspectorDisplayDrawer : InspectorDisplayDrawer
{ }
