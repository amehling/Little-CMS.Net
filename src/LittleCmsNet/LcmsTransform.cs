using System;
using System.Runtime.InteropServices;

namespace LittleCmsNet
{
	public class LcmsTransform<S, T> : IDisposable
		where S : struct
		where T : struct
	{

		private readonly IntPtr ptr;

		private LcmsTransform(IntPtr ptr, LcmsRenderingIntent intent, LcmsFlags dwFlags)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new Exception("Transform creation failed");
			}
			this.ptr = ptr;
			this.Intent = intent;
			this.Flags = dwFlags;
		}

		~LcmsTransform()
		{
			Dispose(false);
		}

		public LcmsRenderingIntent Intent { get; }

		public LcmsFlags Flags { get; }

		public static LcmsTransform<S, T> Create(LcmsProfile input, LcmsProfile output, LcmsRenderingIntent intent = LcmsRenderingIntent.PERCEPTUAL, LcmsFlags dwFlags = LcmsFlags.NONE)
		{
			LcmsFormat inputFormat = input.CreateFormatterForColorspaceOfProfile<S>();
			LcmsFormat outputFormat = output.CreateFormatterForColorspaceOfProfile<T>();
			return Create(input, inputFormat, output, outputFormat, intent, dwFlags);
		}

		public static LcmsTransform<S, T> Create(LcmsProfile input, LcmsFormat inputFormat, LcmsProfile output, LcmsFormat outputFormat, LcmsRenderingIntent intent = LcmsRenderingIntent.PERCEPTUAL, LcmsFlags dwFlags = LcmsFlags.NONE)
		{
			IntPtr ptr = lcms2.CmsCreateTransform(input.Handle, inputFormat.Flags, output.Handle, outputFormat.Flags, (uint)intent, (uint)dwFlags);
			return new LcmsTransform<S, T>(ptr, intent, dwFlags);
		}

		public LcmsFormat InputFormat
		{
			get { return new LcmsFormat(lcms2.CmsGetTransformInputFormat(ptr)); }
		}

		public LcmsFormat OutputFormat
		{
			get { return new LcmsFormat(lcms2.CmsGetTransformOutputFormat(ptr)); }
		}

		public unsafe void Transform(Span<S> input, Span<T> output)
		{
			uint inputLen = (uint)input.Length / InputFormat.Channels;
			uint outputLen = (uint)output.Length / OutputFormat.Channels;
			if (inputLen != outputLen)
			{
				throw new Exception($"Number of input and output samples do not match: {inputLen} != {outputLen}");
			}
			Span<byte> x = MemoryMarshal.Cast<S, byte>(input);
			Span<byte> y = MemoryMarshal.Cast<T, byte>(output);
			fixed (void* inPtr = x)
			{
				fixed (void* outPtr = y)
				{
					lcms2.CmsDoTransform(ptr, (IntPtr)inPtr, (IntPtr)outPtr, inputLen);
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDispose)
		{
			lcms2.CmsDeleteTransform(ptr);
		}

	}
}
