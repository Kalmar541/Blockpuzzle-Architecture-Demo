using static UnityEngine.Terrain;

public class Block
{
    public string Id { get; }
    public MaterialType MaterialType { get; }

    public Block(MaterialType materialType, string id)
    {
        MaterialType = materialType;
        Id = id;
    }

    public Block(Block other)
    {
        MaterialType = other.MaterialType;
        Id = other.Id;
    }

    public override string ToString()
    {
        return $"[Block] Material: {MaterialType}, ID: {Id}";
    }
}