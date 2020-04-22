using System;

namespace LittleCmsNet
{
	[Flags]
	public enum LcmsFlags : uint
	{

		NONE = 0,
		// Flags

		NOCACHE = 0x0040,    // Inhibit 1-pixel cache
		NOOPTIMIZE = 0x0100,    // Inhibit optimizations
		NULLTRANSFORM = 0x0200,    // Don't transform anyway

		// Proofing flags
		GAMUTCHECK = 0x1000,   // Out of Gamut alarm
		SOFTPROOFING = 0x4000,   // Do softproofing

		// Misc
		BLACKPOINTCOMPENSATION = 0x2000,
		NOWHITEONWHITEFIXUP = 0x0004,   // Don't fix scum dot
		HIGHRESPRECALC = 0x0400,   // Use more memory to give better accurancy
		LOWRESPRECALC = 0x0800,   // Use less memory to minimize resources

		// For devicelink creation
		EIGHT_BITS_DEVICELINK = 0x0008,  // Create 8 bits devicelinks
		GUESSDEVICECLASS = 0x0020,  // Guess device class (for transform2devicelink)
		KEEP_SEQUENCE = 0x0080,  // Keep profile sequence for devicelink creation

		// Specific to a particular optimizations
		FORCE_CLUT = 0x0002,   // Force CLUT optimization
		CLUT_POST_LINEARIZATION = 0x0001,   // create postlinearization tables if possible
		CLUT_PRE_LINEARIZATION = 0x0010,   // create prelinearization tables if possible

		// Specific to unbounded mode
		NONEGATIVES = 0x8000,    // Prevent negative numbers in floating point transforms

		// Copy alpha channels when transforming           
		COPY_ALPHA = 0x04000000 // Alpha channels are copied on cmsDoTransform()

	}
}
