﻿
namespace EasyTodoListApp.UI.Desktop;

public partial class App : Application
{
    public App() => InitializeComponent();

    protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());
}