﻿using System;
 using EasyLayout.Forms.Sample;
using EasyLayout.Forms;
 using EasyLayout.Sample.Views;
 using Xamarin.Forms;

namespace EasyLayout.Sample
{
    public class RootPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Button _layoutExampleButton;
        private Button _traditionalXamlRelativeLayout;

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
            _traditionalXamlRelativeLayout.Clicked += TraditionalXamlRelativeLayoutOnClicked;
        }

        protected override void OnDisappearing()
        {
            _layoutExampleButton.Clicked -= LayoutExampleButton_Clicked;
            _traditionalXamlRelativeLayout.Clicked -= TraditionalXamlRelativeLayoutOnClicked;
        }

        private void TraditionalXamlRelativeLayoutOnClicked(object sender, EventArgs eventArgs)
        {
            var traditionalRelativeLayout = new TraditionalRelativeLayout();
            Navigation.PushAsync(traditionalRelativeLayout);
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
                _layoutExampleButton.Bounds.Right == _relativeLayout.Bounds.Right - 10 &&
                
                _traditionalXamlRelativeLayout.Bounds.Top == _layoutExampleButton.Bounds.Bottom + 10 &&
                _traditionalXamlRelativeLayout.Bounds.Left == _layoutExampleButton.Bounds.Left &&
                _traditionalXamlRelativeLayout.Bounds.Width == _layoutExampleButton.Bounds.Width
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _layoutExampleButton = _relativeLayout.AddButton("Layout Example");
            _traditionalXamlRelativeLayout = _relativeLayout.AddButton("Traditional XAML RelativeLayout");
        }
    }

    public class MainPage : NavigationPage
    {
        public MainPage() : base(new RootPage())
        {
        }
    }
}
