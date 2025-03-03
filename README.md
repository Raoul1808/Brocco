### Archival notice
I'm very unhappy with this "super-framework" I made for game jams. On one hand, it provides me with tools I would need to make a fully fleshed-out game with less effort than no tools at all, but on the other, the first and only game jam I used Brocco in turned the framework into a complete mess. I know the saying "if it's stupid but it works, then it's not stupid", or very successful games with extremely messy code (the most notorious example is UNDERTALE), but I'm still unhappy about the mess I made. Besides, I moved on to other languages like C and Rust, and nowadays I only use C# for modding (as of March 2025, may change in the future).

This repository will be archived for preservation. You may learn a thing or two from this framework, but I recommend you don't.

# Brocco
A small .NET Library based on FNA to kickstart game projects.
 
The structure of the project is pretty much experimental. It might evolve over time.

## Features
This is a non-exhaustive list of Brocco's currently implemented features.
- Scene Manager
- Automatic Entity Rendering
  - Manual access to rendering code is still possible
- Sound, Font and Texture loading
- Text Rendering
- Keyboard Input Handling
- Gamepad Input Handling
- Mouse Input Handling
- Partial Text Input Handling
- High-Level Menu Builder API
- Automatic Keyboard Menu Navigation Management
- Separate ImGui module

## Building
1. Download and install the .NET 6 SDK
2. Clone this repo and its submodules
3. Build the project

### Running the example
1. Build the example project
2. Grab the dependencies in the [deps](deps) directory for your platform
3. Follow [these instructions](https://github.com/FNA-XNA/FNA/wiki/Appendix-E:-Modern-.NET-and-NativeAOT#when-developing) to get the native libraries correctly in place
4. Run

### For usage in other projects, please check the wiki (WIP)

## About Auto-Systems

Brocco features something I like to call "auto-systems". These are basically user-defined systems you can optionally add to the Brocco Game Loop. Unlike scenes, auto-systems update directly from the loop, and at all time.

Auto-Systems are supposed to be used to add something to the base game loop, they are NOT to be used for regular game logic.

The feature is still Work In Progress, so expect the API to grow in the future.

Brocco comes with a Dear ImGui auto-system. You can use it by adding a reference to Brocco.ImGui.csproj in your project. **Make sure you also have [cimgui](deps) in the output folder of your project when running it.**

## License

This project is licensed under the MIT License.
