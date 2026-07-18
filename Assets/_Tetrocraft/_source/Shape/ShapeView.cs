using System;
using GameProject.Game;
using UnityEngine;

public class ShapeView : MonoBehaviour
{
    public event Action OnPlaced;

    [SerializeField] private Transform _blocksContainer;

    public Shape Shape => _data;

    private BlockView[] _blockViews;
    private Shape _data;

    public void Init(Shape data, BlockView[] blockViews)
    {
        _data = data;
        _blockViews = blockViews;

        UpdatePositions();
        SetVisible(true);
    }

    public void UpdateFromData(Shape data)
    {
        _data = data;
        UpdatePositions();
    }

    public void UpdatePositions()
    {
        if (_data == null || _blockViews == null) return;

        var positions = _data.GetWorldPositions();
        for (int i = 0; i < positions.Length && i < _blockViews.Length; i++)
        {
            var pos = positions[i];
            _blockViews[i].SetPosition(new Vector3(pos.X, pos.Y, 0f));
        }
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void ReleaseBlocks()
    {
        foreach (var blockView in _blockViews)
        {
            blockView?.Reboot();
        }
        _blockViews = null;
        _data = null;
        SetVisible(false);
    }

    public BlockView[] GetBlockViews() => _blockViews;
}