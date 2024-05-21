﻿<!--
# 
    <a href="#"><img align="center" src="documentation/readme/silkdotnet_v3_horizontal_96.svg">
    



<div align="center">

[![NuGet Version](https://img.shields.io/nuget/v/Silk.NET)](https://nuget.org/packages/Silk.NET)
[![Preview Feed](https://img.shields.io/badge/nuget-experimental%20feed-yellow)](https://gitlab.com/silkdotnet/Silk.NET/-/packages)
[![CI Build](https://github.com/Ultz/Silk.NET/workflows/CI%20Build/badge.svg)](https://github.com/dotnet/Silk.NET/actions/workflows/build.yml)
[![Join our Discord](https://img.shields.io/badge/chat%20on-discord-7289DA)](https://discord.gg/DTHHXRt)



 
-->

![Silk.NET Logo](https://raw.githubusercontent.com/dotnet/Silk.NET/main/documentation/readme/silkdotnet_v3_horizontal_96.svg)


Silk.NET is your one-stop-shop for high-speed .NET multimedia, graphics, and compute; providing bindings to popular low-level APIs such as OpenGL, OpenCL, OpenAL, OpenXR, GLFW, SDL, Vulkan, Assimp, WebGPU, and DirectX.

Use Silk.NET to spruce up applications with cross-platform 3D graphics, audio, compute and haptics!

Silk.NET works on any .NET Standard 2.0 compliant platform, including .NET 6.0, Xamarin, .NET Framework 4.6.1+, and .NET Core 2.0+.






<!--
<a href="https://dotnetfoundation.org" align="right"><img src="https://github.com/dotnet-foundation/swag/blob/main/logo/dotnetfoundation_v4.svg" alt=".NET Foundation" class="logo-footer" width="72" align="left">
-->

![.NET Foundation](https://raw.githubusercontent.com/dotnet/Silk.NET/main/documentation/readme/dotnetfoundation_v4_horizontal_64.svg)






Proud to be an official project under the benevolent [.NET Foundation](https://dotnetfoundation.org) umbrella.



# About This Package

Core functionality for the Silk.NET library. You likely never need to reference this package yourself, nor is it generally useful without another Silk.NET package.



# Features

### Performance

Having poured lots of hours into examining generated C# code and its JIT assembly, you can count on us to deliver blazing fast bindings with negligible overhead induced by Silk.NET!

### Up-to-date

With an efficient bindings regeneration mechanism, we are committed to ensuring our bindings reflect the latest specifications with frequent updates generated straight from the upstream sources.

### High-level utilities

In addition to providing high-speed, direct, and transparent bindings, we provide high-level utilities and wrappers to maximise productivity in common workloads such as platform-agnostic abstractions around Windowing and Input, bringing your apps to a vast number of platforms without changing a single line!

### Good-to-go

Silk.NET caters for anything you could need in swift development of multimedia, graphics, compute applications. Silk.NET is an all-in-one solution, complete with Graphics, Compute, Audio, Input, and Windowing.

<!--

# The team

We currently have the following maintainers:
- [Kai Jellinghaus](https://github.com/HurricanKai) [<img src="https://about.twitter.com/etc/designs/about2-twitter/public/img/favicon.ico" alt="Follow Kai on Twitter" width="16" />](https://twitter.com/intent/follow?screen_name=KJellinghaus)
- [Thomas Mizrahi](https://github.com/ThomasMiz)
- [Beyley Thomas](https://github.com/Beyley)

In addition, the Silk.NET working group help drive larger user-facing changes providing key consultation from the perspective of dedicated users and professionals.

# Building from source

Prerequisites
- **Must**: .NET 6 SDK
- **Should**: [NUKE](https://nuke.build) (build system). Install using `dotnet tool install Nuke.GlobalTool --global`
- **Should**: Android, iOS, and MAUI .NET 6 workloads (use `dotnet workload install android ios maccatalyst maui` to install them)
- **Should**: Android SDK version 30 with NDK tools installed. On Windows, for best results this should be installed into `C:/ProgramData/Android/android-sdk`.
- **Could**: Java JDK (for gradle)
- **Could**: Visual Studio 2022 Community version 17.0 or later

Instructions
- Clone the repository (recursively)
- Run build.sh, build.cmd, build.ps1, or `nuke compile`.
- Use the DLLs. To get nupkgs you can use with NuGet instead, use `nuke pack`.

There are more advanced build actions you can do too, such as FullBuild, Pack, FullPack, among others which you can view by doing `nuke --plan`.

Note: Some .NET 6 workloads are only supported on Windows and macOS today.

# Contributing

Silk.NET uses and encourages [Early Pull Requests](https://medium.com/practical-blend/pull-request-first-f6bb667a9b6). Please don't wait until you're done to open a PR!

1. [Fork Silk.NET](https://github.com/dotnet/Silk.NET/fork)
2. Add an empty commit to a new branch to start your work off: `git commit --allow-empty -m "start of [thing you're working on]"`
3. Once you've pushed a commit, open a [**draft pull request**](https://github.blog/2019-02-14-introducing-draft-pull-requests/). Do this **before** you actually start working.
4. Make your commits in small, incremental steps with clear descriptions.
5. Tag a maintainer when you're done and ask for a review!

The Silk.NET solution is **very large**. Learn about how you can combat this using our build process in [CONTRIBUTING.md](CONTRIBUTING.md).

-->

# Funding
Silk.NET requires significant effort to maintain, as such we greatly appreciate any financial support you are able to provide!

This helps ensure Silk.NET's long term viability, and to help support the developers who maintain Silk.NET in their free time. [Kai](https://github.com/sponsors/HurricanKai) is accepting GitHub Sponsorships.

# Further resources

- Several examples can be found in the [examples folder](https://github.com/dotnet/Silk.NET/tree/master/examples)
- Come chat with us on [Discord](https://discord.gg/DTHHXRt)!

# Licensing and governance

Silk.NET is distributed under the very permissive MIT/X11 license and all dependencies are distributed under MIT-compatible licenses.

Silk.NET is a [.NET Foundation](https://www.dotnetfoundation.org/projects) project, and has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) to clarify expected behavior in our community. For more information, see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct).

<!--

---


    <a href="https://www.jetbrains.com/?from=Silk.NET" align="right"><img src="https://raw.githubusercontent.com/dotnet/Silk.NET/main/documentation/readme/jetbrains.svg" alt="JetBrains" class="logo-footer" width="72" align="left">
    


        
Special thanks to [JetBrains](https://www.jetbrains.com/?from=Silk.NET) for supporting us with open-source licenses for their IDEs. 


-->
