
![CG Rust Unity Banner](https://github.com/blenderfan/CGRust-UnityWrapper/blob/master/cg_rust_unity_banner.png)

<a href="https://github.com/blenderfan/CGRust-UnityWrapper/stargazers"><img src="https://img.shields.io/github/stars/blenderfan/CGRust-UnityWrapper" alt="Stars Badge"/></a>
<a href="https://github.com/blenderfan/CGRust-UnityWrapper/network/members"><img src="https://img.shields.io/github/forks/blenderfan/CGRust-UnityWrapper" alt="Forks Badge"/></a>
<a href="https://github.com/blenderfan/CGRust-UnityWrapper/pulls"><img src="https://img.shields.io/github/issues-pr/blenderfan/CGRust-UnityWrapper" alt="Pull Requests Badge"/></a>
<a href="https://github.com/blenderfan/CGRust-UnityWrapper/issues"><img src="https://img.shields.io/github/issues/blenderfan/CGRust-UnityWrapper" alt="Issues Badge"/></a>
<a href="https://github.com/blenderfan/CGRust-UnityWrapper/graphs/contributors"><img alt="GitHub contributors" src="https://img.shields.io/github/contributors/blenderfan/CGRust-UnityWrapper?color=2b9348"></a>
<a href="https://github.com/blenderfan/CGRust-UnityWrapper/blob/master/LICENSE"><img src="https://img.shields.io/github/license/blenderfan/CGRust-UnityWrapper?color=2b9348" alt="License Badge"/></a>

## Table of contents
- [Introduction](#introduction)
- [Overview](#overview)
- [Documentation](#documentation)
- [Version](#version)

## <a id="introduction"></a>Introduction

A C# Wrapper for Unity for [CG Rust](https://github.com/blenderfan/cg-rust), enabling you to have very fast computational geometry right inside
Unity.

## <a id="overview"></a>Overview

Right now, the project only contains some call tests for the rust library creating a regular polygon and shaders for display ^^

![Hexagon from Rust](https://github.com/blenderfan/CGRust-UnityWrapper/blob/master/images/hexagon_from_rust.jpg)

The rust standard libary is the same as the one used in the [CG Rust](https://github.com/blenderfan/cg-rust) project. If you require a different
version, you will have to build that project first and copy the DLLs into the Plugins folder!

## <a id="Documentation"></a>Documentation

A lot of effort is going to be put into the documentation which you can find here:
[Documentation](https://blenderfan.github.io/CGRust-UnityWrapper/index.html)

In addition to the description of the API, it will hopefully soon feature some tutorials as well!

## <a id="version"></a>Version

The minimum version is [Unity 6](https://unity.com/de/releases/unity-6). All test scenes use the Universal Rendering Pipeline!

