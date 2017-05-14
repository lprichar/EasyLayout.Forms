﻿using EasyLayout.Forms.Sample;
using EasyLayout.Forms;
using Xamarin.Forms;

namespace EasyLayout.Sample
{
    public class RootPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Button _layoutExampleButton;

        public RootPage()
        {
            AddViews();
            ConstrainLayout();
            SetPageProperties();
        }

        private void SetPageProperties()
        {
            Title = "EasyLayout.Forms";
            Content = _relativeLayout;
        }

        protected override void OnAppearing()
        {
            _layoutExampleButton.Clicked += LayoutExampleButton_Clicked;
        }

        protected override void OnDisappearing()
        {
            _layoutExampleButton.Clicked -= LayoutExampleButton_Clicked;
        }

        private void LayoutExampleButton_Clicked(object sender, System.EventArgs e)
        {
            var layoutExamplePage = new LayoutExamplePage();
            Navigation.PushAsync(layoutExamplePage);
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _layoutExampleButton.Bounds.Top == _relativeLayout.Bounds.Top + 20 &&
                _layoutExampleButton.Bounds.Left == _relativeLayout.Bounds.Left + 10 &&
                _layoutExampleButton.Bounds.Right == _relativeLayout.Bounds.Right - 10
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _layoutExampleButton = _relativeLayout.AddButton("Layout Example");
        }
    }

    public class MainPage : NavigationPage
    {
        public MainPage() : base(new RootPage())
        {
        }
    }
}
