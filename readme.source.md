# <img src="/src/icon.png" height="30px"> TextCopy

[![Build status](https://ci.appveyor.com/api/projects/status/axnys9xch290ut79/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/textcopy)
[![Build status](https://travis-ci.org/SimonCropp/TextCopy.svg?branch=master)](https://travis-ci.org/SimonCropp/TextCopy)
[![NuGet Status](https://img.shields.io/nuget/v/TextCopy.svg)](https://www.nuget.org/packages/TextCopy/)

A cross platform package to copy text to and from the clipboard.

Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-textcopy?utm_source=nuget-textcopy&utm_medium=referral&utm_campaign=enterprise).

toc


## NuGet package

https://nuget.org/packages/TextCopy/


## Usage


### SetTextAsync

snippet: SetTextAsync


### SetText

snippet: SetText


### GetTextAsync

snippet: GetTextAsync


### GetText

snippet: GetText


## Instance API

In adition to the above static API, there is an instance API exposed:

snippet: SetTextInstance


### Dependency Injection

An instance of `IClipboard` can be injected into `IServiceCollection`:

snippet: InjectClipboard


## Supported on

 * Windows with .NET Framework 4.6.1 and up
 * Windows with .NET Core 2.0 and up
 * Windows with Mono 5.0 and up
 * OSX with .NET Core 2.0 and up
 * OSX with Mono 5.20.1 and up
 * Linux with .NET Core 2.0 and up
 * Linux with Mono 5.20.1 and up
 * Xamarin.Android 9.0 and up
 * Xamarin.iOS 10.0 and up
 * Universal Windows Platform version 10.0.16299 and up
 * Blazor WebAssembly


## Blazor WebAssembly 

Due to the dependency on `JSInterop` the static `ClipboardService` is not supported on Blazor.

Instead inhect an `IClipboard`:

snippet: BlazorStartup

Then consume it:

snippet: Inject

Blazor support requires the browser APIs [clipboard.readText](https://caniuse.com/#feat=mdn-api_clipboard_readtext) and [clipboard.writeText](https://caniuse.com/#feat=mdn-api_clipboard_writetext).


## Linux

Linux uses [xclip](https://github.com/astrand/xclip) to access the clipboard. As such it needs to be installed and callable.


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Clone](https://thenounproject.com/term/Clone/207435/) designed by [Wes Breazell](https://thenounproject.com/wes13/) from [The Noun Project](https://thenounproject.com).