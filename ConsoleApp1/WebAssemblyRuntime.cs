using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WebAssembly
{
	[Obfuscation(Feature = "renaming", Exclude = true)]
	public sealed class Runtime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InvokeJS(string str, out int exceptional_result);

		public static string InvokeJS(string str)
		{
			var r = InvokeJS(str, out int exceptionResult);

			if (exceptionResult != 0)
				Console.Error.WriteLine($"Error #{exceptionResult} \"{r}\" executing javascript: \"{str}\"");

			return r;
		}
	}
}
