# <img src="/src/icon.png" height="30px"> TextCopy

[![Build status](https://ci.appveyor.com/api/projects/status/lsw1b1olku8tg9d1/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/TextCopy)
[![NuGet Status](https://img.shields.io/nuget/v/TextCopy.svg?cacheSeconds=86400)](https://www.nuget.org/packages/TextCopy/)


A netstandard package to copy text to and from the clipboard.

toc


## NuGet

https://nuget.org/packages/TextCopy/


## Usage


### SetText

snippet: SetText


### GetText

snippet: GetText


## Compatibility


### Supported on

 * Windows with .NET Framework 4.6.1 and up
 * Windows with .NET Core 2.0 and up
 * Windows with Mono 5.0 and up
 * OSX with .NET Core 2.0 and up
 * OSX with Mono 5.20.1 and up
 * Xamarin.Mac Modern, .NET Core 2.0
 * Linux with .NET Core 2.0 and up
 * Linux with Mono 5.20.1 and up
 * Universal Windows Platform version 10.0.16299 and up


### Not verified

The following may work but have not been verified:

 * Xamarin.iOS

If anyone verifies this, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) to the readme with the outcome.


### Not supported

 * Xamarin.Android
 * Xamarin.Mac Full, .NET Framework 4.7.2
     ```
    System.DllNotFoundException: User32.dll
      at at (wrapper managed-to-native)WindowsClipboard.IsClipboardFormatAvailable(uint)
      at WindowsClipboard.GetText()
      at TextCopy.Clipboard.GetText()
    ```
 
If support is required, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) that adds support.


## Notes on Linux

Linux uses [xclip](https://github.com/astrand/xclip) to access the clipboard. As such it needs to be installed and callable.


## Release Notes

See [closed milestones](../../milestones?state=closed).


## Icon

[Clone](https://thenounproject.com/term/Clone/207435/) designed by [Wes Breazell](https://thenounproject.com/wes13/) from [The Noun Project](https://thenounproject.com).