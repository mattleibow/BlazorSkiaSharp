using SkiaSharp;
using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello There!");

			Console.WriteLine("COLORS: " + SKImageInfo.PlatformColorType);

			InvalidateCanvas(null, null, 0, 0);
		}

		static void InvalidateCanvas(string base64, string canvasId, int width, int height)
		{
			InvalidateCanvas();

			Console.WriteLine("START");

			if (base64 == null)
				return;

			Console.WriteLine("REC base64: " + base64);

			var data = Convert.FromBase64String(base64);

			Console.WriteLine("REC data.Length = " + data.Length);

			var info = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

			var pixels = new byte[info.BytesSize];
			var pixelsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);

			using (var picture = SKPicture.Deserialize(data))
			using (var surface = SKSurface.Create(info, pixelsHandle.AddrOfPinnedObject(), info.RowBytes))
			{
				Console.WriteLine("REC picture = " + picture);

				var canvas = surface.Canvas;

				canvas.DrawPicture(picture);
			}

			WebAssembly.Runtime.InvokeJS($"invalidateCanvas({pixelsHandle.AddrOfPinnedObject()}, \"{canvasId}\", {width}, {height});");

			pixelsHandle.Free();
		}

		private static void InvalidateCanvas()
		{
			var w = 1024;
			var h = 1024;

			using var recorder = new SKPictureRecorder();
			using var canvas = recorder.BeginRecording(SKRect.Create(w, h));

			{
				canvas.Clear(SKColors.Red);

				using var paint = new SKPaint
				{
					Color = SKColors.Blue,
				};
				canvas.DrawRect(SKRect.Create(10, 10, 999, 999), paint);
			}

			using var picture = recorder.EndRecording();

			using var data = picture.Serialize();
			Console.WriteLine("SND data.Length = " + data.Size);

			var base64 = Convert.ToBase64String(data.AsSpan());
			Console.WriteLine("SND base64 = " + base64);

			var deData = Convert.FromBase64String(base64);
			var p = SKPicture.Deserialize(deData);

			Console.WriteLine("SND Deserialize = " + p);
		}
	}
}
