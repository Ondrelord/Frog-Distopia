using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Product", menuName ="Product")]
public class Product : ScriptableObject
{
    [SerializeField] float timeToProduce = 0f;
    [SerializeField] GameObject result = null;
    [SerializeField] Image icon = null;

    public float GetTimeToProduce() => timeToProduce;
    public GameObject GetResult() => result;
    public Image GetIcon() => icon;
}
