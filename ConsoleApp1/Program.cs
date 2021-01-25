using SkiaSharp;
using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			// for the linker
			InvalidateCanvas(null, null, 0, 0);
		}

		static void InvalidateCanvas(string base64, string canvasId, int width, int height)
		{
			if (base64 == null)
				return;

			var data = Convert.FromBase64String(base64);

			var info = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

			var pixels = new byte[info.BytesSize];
			var pixelsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);

			using (var picture = SKPicture.Deserialize(data))
			using (var surface = SKSurface.Create(info, pixelsHandle.AddrOfPinnedObject(), info.RowBytes))
			{
				var canvas = surface.Canvas;

				canvas.DrawPicture(picture);
			}

			Interop.Runtime.InvokeJS($"invalidateCanvas({pixelsHandle.AddrOfPinnedObject()}, \"{canvasId}\", {width}, {height});");

			pixelsHandle.Free();
		}
	}
}
