using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[Obfuscation(Feature = "renaming", Exclude = true)]
internal static partial class Interop
{
	internal static partial class Runtime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InvokeJS(string str, out int exceptionalResult);

		public static string InvokeJS(string str)
		{
			string res = InvokeJS(str, out int exception);

			if (exception != 0)
				throw new Exception(res);

			return res;
		}
	}
}
