[System.Serializable]
public class Item
{
    public string displayName;
    public bool interactable;
    public int stackSize = 1;
    public virtual void Interact() { }
}
