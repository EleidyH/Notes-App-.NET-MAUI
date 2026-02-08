using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models;

internal class About
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    //urlhttps://aka.ms/maui
    //urlhttps://youtu.be/Aq5WXmQQooo?list=RDAq5WXmQQooo
    public string MoreInfoUrl => "https://aka.ms/maui";
    public string Message => "This app is written in XAML and C# with .NET MAUI.";
}