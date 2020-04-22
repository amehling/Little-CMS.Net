using System;
using System.Runtime.InteropServices;

namespace LittleCmsNet
{
	public class LcmsProfile : IDisposable
	{

		private IntPtr ptr;

		private LcmsProfile(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new Exception("Profile creation failed");
			}
			this.ptr = ptr;
		}

		~LcmsProfile()
		{
			Dispose(false);
		}

		internal IntPtr Handle
		{
			get { return ptr; }
		}

		public static LcmsProfile Load(string path)
		{
			IntPtr ptrProfile = lcms2.CmsOpenProfileFromFile(path, "r");
			return new LcmsProfile(ptrProfile);
		}

		public static LcmsProfile CreateSRGBProfile()
		{
			IntPtr ptrProfile = lcms2.CmsCreateSRGBProfile();
			return new LcmsProfile(ptrProfile);
		}

		public CmsColorSpaceSignature ColorSpace
		{
			get { return lcms2.CmsGetColorSpace(ptr); }
		}

		public CmsProfileClassSignature DeviceClass
		{
			get { return lcms2.CmsGetDeviceClass(ptr); }
		}

		public uint Channels
		{
			get { return lcms2.CmsChannelsOf(ColorSpace); }
		}

		public uint EncodedICCversion
		{
			get { return lcms2.CmsGetEncodedICCversion(ptr); }
		}

		public uint HeaderRenderingIntent
		{
			get { return lcms2.CmsGetHeaderRenderingIntent(ptr); }
		}

		public CmsColorSpaceSignature ProfileConnectionSpace
		{
			get { return lcms2.CmsGetPCS(ptr); }
		}

		public unsafe string GetProfileInfo(CmsInfoType infoType)
		{
			sbyte[] language = { (sbyte)'e', (sbyte)'n', (sbyte)'g' };
			sbyte[] region = { (sbyte)'U', (sbyte)'S', (sbyte)'A' };
			uint length = 256;
			char* buffer = stackalloc char[(int)length];
			uint err = lcms2.CmsGetProfileInfo(ptr, infoType, language, region, buffer, length);
			if (err == 0)
			{
				return null;
			}
			else
			{
				return new string(buffer, 0, (int)err);
			}
		}

		private static bool IsFloat<X>() where X : struct
		{
			if (typeof(X) == typeof(float) || typeof(X) == typeof(double))
			{
				return true;
			}
			if (typeof(X) == typeof(byte) || typeof(X) == typeof(ushort))
			{
				return false;
			}
			throw new Exception($"Invalid color conversion data type {typeof(X).Name}. Allowed are: byte, ushort, float, double");
		}

		public LcmsFormat CreateFormatterForColorspaceOfProfile<X>() where X : struct
		{
			bool isFloat = IsFloat<X>();
			uint bytesPerChannel = (uint)Marshal.SizeOf<X>();
			if (bytesPerChannel == 8) bytesPerChannel = 0; // avoid overflow
			uint flags = lcms2.CmsFormatterForColorspaceOfProfile(this.Handle, bytesPerChannel, isFloat ? 1 : 0);
			LcmsFormat format = new LcmsFormat(flags);
			return format;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDispose)
		{
			lcms2.CmsCloseProfile(ptr);
		}

	}
}
