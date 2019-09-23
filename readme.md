<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /readme.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# <img src="/src/icon.png" height="30px"> TextCopy

[![Build status](https://ci.appveyor.com/api/projects/status/lsw1b1olku8tg9d1/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/TextCopy)
[![NuGet Status](https://img.shields.io/nuget/v/TextCopy.svg?cacheSeconds=86400)](https://www.nuget.org/packages/TextCopy/)


A netstandard package to copy text to and from the clipboard.

<!-- toc -->
## Contents

  * [NuGet](#nuget)
  * [Usage](#usage)
    * [SetText](#settext)
    * [GetText](#gettext)
  * [Compatibility](#compatibility)
    * [Supported on](#supported-on)
    * [Not verified](#not-verified)
    * [Not supported](#not-supported)
  * [Notes on Linux](#notes-on-linux)
<!-- endtoc -->



## NuGet

https://nuget.org/packages/TextCopy/


## Usage


### SetText

<!-- snippet: SetText -->
<a id='snippet-settext'/></a>
```cs
TextCopy.Clipboard.SetText("Text to place in clipboard");
```
<sup>[snippet source](/src/Tests/Snippets.cs#L5-L9) / [anchor](#snippet-settext)</sup>
<!-- endsnippet -->


### GetText

<!-- snippet: GetText -->
<a id='snippet-gettext'/></a>
```cs
var text = TextCopy.Clipboard.GetText();
```
<sup>[snippet source](/src/Tests/Snippets.cs#L14-L18) / [anchor](#snippet-gettext)</sup>
<!-- endsnippet -->


## Compatibility


### Supported on

 * Windows with .NET Framework 4.6.1 and up
 * Windows with .NET Core 2.0 and up
 * Windows with Mono 5.0 and up
 * OSX with .NET Core 2.0 and up
 * OSX with Mono 5.20.1 and up
 * Linux with .NET Core 2.0 and up
 * Linux with Mono 5.20.1 and up
 * Universal Windows Platform version 10.0.16299 and up


### Not verified

The following may work but have not been verified:

 * Xamarin.iOS

If anyone verifies any of the above, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) to the readme with the outcome.


### Not supported

 * Xamarin.Android
 * Xamarin.Mac
 ** Xamarin.Mac fails when calling GetText with:
 <!-- snippet -->
 System.DllNotFoundException: User32.dll
  at at (wrapper managed-to-native) WindowsClipboard.IsClipboardFormatAvailable(uint)
  at WindowsClipboard.GetText () [0x00000] in <b38d6b05b12944c3a3542953bdff5fbe>:0
  at TextCopy.Clipboard.GetText () [0x00000] in <b38d6b05b12944c3a3542953bdff5fbe>:0
<!-- endsnippet -->

If support is required, please submit a [Pull Request](https://help.github.com/articles/about-pull-requests/) that adds support.


## Notes on Linux

Linux uses [xclip](https://github.com/astrand/xclip) to access the clipboard. As such it needs to be installed and callable.


## Release Notes

See [closed milestones](../../milestones?state=closed).


## Icon

[Clone](https://thenounproject.com/term/Clone/207435/) designed by [Wes Breazell](https://thenounproject.com/wes13/) from [The Noun Project](https://thenounproject.com).
