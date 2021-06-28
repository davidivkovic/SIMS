using ElectronNET.API;
using System.Collections.Generic;
using System.Linq;

namespace TailwindBlazorElectron.Services
{
	public class WindowService
	{
		public void Minimize() => Electron.WindowManager.BrowserWindows.FirstOrDefault()?.Minimize();
		public void Close() => Electron.WindowManager.BrowserWindows.FirstOrDefault()?.Close();

		public async void Maximize()
		{
			var window = Electron.WindowManager.BrowserWindows.FirstOrDefault();

			if (window is null) return;

			if (await window.IsMaximizedAsync())
			{
				window.Restore();
			}
			else
			{
				window.Maximize();
			}
		}
	}
}