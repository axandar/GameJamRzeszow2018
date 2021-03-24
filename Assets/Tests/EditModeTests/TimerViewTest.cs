using NUnit.Framework;

namespace Tests.EditModeTests{
	public class TimerViewTest{

		[Test]
		public void TextFromTimer(){
			var zero = Timeleft.TextFromTimer(0);
			Assert.AreEqual(zero, "00:00");
			var oneHundred = Timeleft.TextFromTimer(100);
			Assert.AreEqual(oneHundred, "01:40");
			var fifteen = Timeleft.TextFromTimer(15);
			Assert.AreEqual(fifteen, "00:15");
			var sixty = Timeleft.TextFromTimer(60);
			Assert.AreEqual(sixty, "01:00");
		}
	}
}