using System;

namespace LitteCmsNet
{
	public class LcmsTransform : IDisposable
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

		public static LcmsTransform Create(LcmsProfile input, LcmsFormat inputFormat, LcmsProfile output, LcmsFormat outputFormat, LcmsRenderingIntent intent, LcmsFlags dwFlags)
		{
			IntPtr ptr = lcms2.CmsCreateTransform(input.Handle, inputFormat.Flags, output.Handle, outputFormat.Flags, (uint)intent, (uint)dwFlags);
			return new LcmsTransform(ptr, intent, dwFlags);
		}

		public LcmsFormat InputFormat
		{
			get { return new LcmsFormat(lcms2.CmsGetTransformInputFormat(ptr)); }
		}

		public LcmsFormat OutputFormat
		{
			get { return new LcmsFormat(lcms2.CmsGetTransformOutputFormat(ptr)); }
		}

		public unsafe void Transform(Span<float> input, Span<float> output)
		{
			uint length = (uint)input.Length / InputFormat.Channels;
			fixed (float* inPtr = input)
			{
				fixed (float* outPtr = output)
				{
					lcms2.CmsDoTransform(ptr, (IntPtr)inPtr, (IntPtr)outPtr, length);
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
