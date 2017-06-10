using System;
using NUnit.Framework;
using Xamarin.Forms;

namespace EasyLayout.Forms.Test
{
    [TestFixture]
    public class EasyLayoutTest
    {
        [Test]
        public void GivenWrongLeftHandProperty_WhenConstrainLayout_ThenNiceErrorMessage()
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

        [Test]
        public void GivenWrongRightHandProperty_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<NotSupportedException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.Bounds.Left == label.FontSize
                )
            );
            Assert.AreEqual("Property FontSize is not recognized.", ex.Message);
        }

        [Test]
        public void GivenWrongLeftHandExpression_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<ArgumentException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.Bounds.Top == label.Bounds.Left
                )
            );
            Assert.AreEqual("Unsupported relative positioning combination: label.Top with Bounds.Left", ex.Message);
        }

        [Test]
        public void GivenDuplicateAssertions_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<ArgumentException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.Bounds.Width == 50 &&
                    label.Bounds.Width == 80
                )
            );
            Assert.AreEqual("Multiple width assertions found.  Element name: label", ex.Message);
        }

        [Test]
        public void GivenUnsupportedRelativePositionCombination_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<ArgumentException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.Bounds.GetCenterX() == label.Bounds.GetCenterY()
                )
            );
            Assert.AreEqual("Unsupported relative positioning combination: Bounds.CenterX with Bounds.CenterY", ex.Message);
        }

        [Test]
        public void GivenUnsupportedParentPositionCombination_WhenConstrainLayout_ThenNiceErrorMessage()
        {
            var relativeLayout = new RelativeLayout();
            var label = new Label();
            var ex = Assert.Throws<ArgumentException>(() =>
                relativeLayout.ConstrainLayout(() =>
                    label.Bounds.GetCenterX() == relativeLayout.Bounds.GetCenterY()
                )
            );
            Assert.AreEqual("Unsupported parent positioning combination: Bounds.CenterX with parent.CenterY", ex.Message);
        }
    }
}
