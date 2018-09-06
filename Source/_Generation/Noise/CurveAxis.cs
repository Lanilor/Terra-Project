using Verse.Noise;

namespace TerraCore
{

    public class CurveAxis : ModuleBase
	{

        private double a;
        private double b;
        private double c;
		
		public CurveAxis() : base(1)
		{
		}

        public CurveAxis(double a, double b, double c, ModuleBase source) : base(1)
		{
            this.a = a;
            this.b = b;
            this.c = c;
            modules[0] = source;
		}

		public override double GetValue(double x, double y, double z)
		{
			return modules[0].GetValue(x + a * z * z + b * z + c, y, z);
		}

	}

}
