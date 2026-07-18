using System;
using System.Collections.Generic;
using System.Linq;

public class TetraminoContainer 
{
    public event Action<ShapeType> OnSpawnTetramino;
    private LevelHandler _levelHandler;

    private Queue<ShapeType> _queueTetramino;

    public TetraminoContainer(int sizeVariants, LevelHandler levelHandler)
    {
        _levelHandler = levelHandler;
        CreateQueueTetraminos(sizeVariants);
        _levelHandler.OnStartLevel += DropNextTetramino;
    }

    public void DropNextTetramino()
    {
        OnSpawnTetramino?.Invoke(_queueTetramino.Dequeue());
    }

    private void CreateQueueTetraminos(int capacity)
    {
        if(capacity<1) capacity = 1;

        _queueTetramino = new();

        for (int i = 0; i < capacity; i++)
        {
            _queueTetramino.Enqueue(GenerateTetraminoType());
        }
    }

    private ShapeType _previousTypeTetramino;

    private ShapeType GenerateTetraminoType()
    {
        var types = System.Enum.GetValues(typeof(ShapeType))
                               .Cast<ShapeType>()
                               .Where(t => t != _previousTypeTetramino)
                               .ToList();

        _previousTypeTetramino = types[UnityEngine.Random.Range(0, types.Count)];
        return _previousTypeTetramino;
    }
}