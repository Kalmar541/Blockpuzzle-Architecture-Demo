using System;

namespace KG.Viewports
{
    public interface IViewportHandler
    {
        event Action<IWindow> OnWindowStartShow;
        event Action<IWindow> OnWindowShow;
        event Action<IWindow> OnWindowHide;

        int ActiveWindowAmount { get; }

        T GetWindow<T>() where T : Window;
        void HideAll();
        void HideTop();
        void Hide<T>() where T : Window;
        void Show<T>() where T : Window;
        void Show(Window parentWindow);
        void Init();
        bool CanBeShown(IWindow window);
    }
}