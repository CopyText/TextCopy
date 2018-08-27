# TextCopy

A netstandard package to copy text to and from the clipboard.


## NuGet  [![NuGet Status](http://img.shields.io/nuget/v/TextCopy.svg?style=flat)](https://www.nuget.org/packages/TextCopy/)

https://nuget.org/packages/TextCopy/

    PM> Install-Package TextCopy


### Usage

#### SetText

```csharp
TextCopy.Clipboard.SetText("Text to place in clipboard");
```

#### GetText

```csharp
var text = TextCopy.Clipboard.GetText();
```

## Compatability

### Supported on:

 * Windows with .NET Framework 4.6.1 and up
 * Windows with .NET Core 2.0 and up
 * OSX with  .NET Core 2.0 and up
 * Linux with  .NET Core 2.0 and up


### Not verified

The following may work but have not been verified:

 * Windows with Mono
 * Linux with Mono
 * Xamarin.iOS
 * Xamarin.Mac
 * Universal Windows Platform

If anyone verifies any of the above, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) to the readme with the outcome.


### Not supported

 * Xamarin.Android

If support is required, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) that adds support.


## Notes on Linux

Linux uses [xclip](https://github.com/astrand/xclip) to access the clipboard. As such it needs to be installed and callable.

## Icon

<a href="https://thenounproject.com/term/Clone/207435/" target="_blank">Clone</a> designed by Wes Breazell from The Noun Project
