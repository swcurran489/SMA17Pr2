using NUnit.Framework;
using System;

namespace herpderp_UT
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase()
		{
			herpderp.herpderp h = new herpderp.herpderp();
			Assert.AreEqual(h.derppp(), 1);
		}
	}
}
