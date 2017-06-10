using System;
using NUnit.Framework;
using Xamarin.Forms;

namespace EasyLayout.Forms.Test
{
    [TestFixture]
    public class EasyLayoutTest
    {
        [Test]
        public void GivenWrongLeftHandMethodName_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<NotSupportedException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.FontSize == label.Bounds.Left
                )
            );
            Assert.AreEqual("Property FontSize is not recognized.", ex.Message);
        }
    }
}
