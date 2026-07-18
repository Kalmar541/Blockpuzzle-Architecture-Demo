using System;

namespace KG.Viewports
{
    public interface IWindow
    {
        public event Action<IWindow> OnOpenStartedEvent;
        public event Action<IWindow> OnShowed;
        public event Action<IWindow> OnHided;

        void ToggleInteractable(bool interactable);
    }
}