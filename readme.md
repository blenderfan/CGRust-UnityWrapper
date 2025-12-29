
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

