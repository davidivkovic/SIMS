using ElectronNET.API;
using System.Collections.Generic;
using System.Linq;

namespace TailwindBlazorElectron.Services
{
    public class WindowService
    {
        public BrowserWindow ElectronWindow { get; }

        public WindowService(WindowManager windowManager)
        {
            ElectronWindow = windowManager.BrowserWindows.ElementAtOrDefault(0);
        }

        public void Minimize() => ElectronWindow?.Minimize();
        public void Close() => ElectronWindow?.Close();

        public async void Maximize()
        {
            if (await ElectronWindow?.IsMaximizedAsync())
            {
                ElectronWindow?.Restore();
            }
            else
            {
                ElectronWindow?.Maximize();
            }
        }
    }
}