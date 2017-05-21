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
        private Button _traditionalPerformance;
        private Button _elxfPerformance;
        private Button _playground;

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
            _traditionalPerformance.Clicked += TraditionalPerformance_Clicked;
            _elxfPerformance.Clicked += ElxfPerformance_Clicked;
            _playground.Clicked += Playground_Clicked;
        }

        protected override void OnDisappearing()
        {
            _layoutExampleButton.Clicked -= LayoutExampleButton_Clicked;
            _traditionalXamlRelativeLayout.Clicked -= TraditionalXamlRelativeLayoutOnClicked;
            _traditionalPerformance.Clicked -= TraditionalPerformance_Clicked;
            _elxfPerformance.Clicked -= ElxfPerformance_Clicked;
            _playground.Clicked -= Playground_Clicked;
        }

        private void Playground_Clicked(object sender, EventArgs e)
        {
            var playgroundPage = new PlaygroundPage();
            Navigation.PushAsync(playgroundPage);
        }

        private void TraditionalPerformance_Clicked(object sender, EventArgs e)
        {
            var traditionalPerformancePage = new TraditionalPerformancePage();
            Navigation.PushAsync(traditionalPerformancePage);
        }

        private void ElxfPerformance_Clicked(object sender, EventArgs e)
        {
            var elxfPerformancePage = new ElxfPerformancePage();
            Navigation.PushAsync(elxfPerformancePage);
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
                 _layoutExampleButton.Bounds.Top == _relativeLayout.Bounds.Top + 20
                && _layoutExampleButton.Bounds.Left == _relativeLayout.Bounds.Left + 10
                && _layoutExampleButton.Bounds.Right == _relativeLayout.Bounds.Right - 10

                && _traditionalXamlRelativeLayout.Bounds.Top == _layoutExampleButton.Bounds.Bottom + 10
                && _traditionalXamlRelativeLayout.Bounds.Left == _layoutExampleButton.Bounds.Left
                && _traditionalXamlRelativeLayout.Bounds.Width == _layoutExampleButton.Bounds.Width

                && _traditionalPerformance.Bounds.Top == _traditionalXamlRelativeLayout.Bounds.Bottom + 10
                && _traditionalPerformance.Bounds.Left == _layoutExampleButton.Bounds.Left
                && _traditionalPerformance.Bounds.Width == _layoutExampleButton.Bounds.Width

                && _elxfPerformance.Bounds.Top == _traditionalPerformance.Bounds.Bottom + 10
                && _elxfPerformance.Bounds.Left == _layoutExampleButton.Bounds.Left
                && _elxfPerformance.Bounds.Width == _layoutExampleButton.Bounds.Width

                && _playground.Bounds.Top == _elxfPerformance.Bounds.Bottom + 10
                && _playground.Bounds.Left == _layoutExampleButton.Bounds.Left
                && _playground.Bounds.Width == _layoutExampleButton.Bounds.Width
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _layoutExampleButton = _relativeLayout.AddButton("Layout Example");
            _traditionalXamlRelativeLayout = _relativeLayout.AddButton("Traditional RelativeLayout vs EasyLayout");
            _traditionalPerformance = _relativeLayout.AddButton("Traditional Performance");
            _elxfPerformance = _relativeLayout.AddButton("EasyLayout.Forms Performance");
            _playground = _relativeLayout.AddButton("Playground");
        }
    }

    public class MainPage : NavigationPage
    {
        public MainPage() : base(new RootPage())
        {
        }
    }
}
