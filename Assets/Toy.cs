
using UnityEngine;

public class Toy : MonoBehaviour
{
    public string toyType;
    private bool isSorted;

    public bool IsSorted => isSorted;

    public void MarkAsSorted()
    {
        isSorted = true;
    }
}
