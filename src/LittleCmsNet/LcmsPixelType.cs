namespace LittleCmsNet
{
	/// <summary>
	/// Pixel types
	/// </summary>
	public enum LcmsPixelType
	{
		/// <summary>
		/// Don't check colorspace
		/// </summary>
		ANY = 0,
		// 1 & 2 are reserved
		GRAY = 3,
		RGB = 4,
		CMY = 5,
		CMYK = 6,
		YCbCr = 7,
		/// <summary>
		/// Lu'v'
		/// </summary>
		YUV = 8,
		XYZ = 9,
		Lab = 10,
		/// <summary>
		/// Lu'v'K
		/// </summary>
		YUVK = 11,
		HSV = 12,
		HLS = 13,
		Yxy = 14,

		MCH1 = 15,
		MCH2 = 16,
		MCH3 = 17,
		MCH4 = 18,
		MCH5 = 19,
		MCH6 = 20,
		MCH7 = 21,
		MCH8 = 22,
		MCH9 = 23,
		MCH10 = 24,
		MCH11 = 25,
		MCH12 = 26,
		MCH13 = 27,
		MCH14 = 28,
		MCH15 = 29,

		/// <summary>
		/// Identical to PT_Lab, but using the V2 old encoding
		/// </summary>
		LabV2 = 30

	}
}
