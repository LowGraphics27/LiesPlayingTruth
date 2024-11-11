using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Commands;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class ParticleDust : StoryboardObjectGenerator
    {
        StoryboardLayer Back, Main, Verse;
        public override void Generate()
        {
            Back = GetLayer("Back");
            Main = GetLayer("Main");
            Verse = GetLayer("Verse");

            //----------------------------------------- Verse 1 ---------------------------------------------------//
            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 22999, EndTime: 32746, 
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 22999, EndTime: 32746, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 42644, EndTime: 46223, 
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 42644, EndTime: 46223, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 47517, EndTime: 52390, 
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 47517, EndTime: 52390, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            //----------------------------------------- Chorus 1 --------------------------------------------------//
            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 59700, EndTime: 64192, 
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 59700, EndTime: 64192, 
                ParticleAmount: 16, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Back, Path: "sb/pl.png", StartTime: 64192, EndTime: 66933,
                ParticleAmount: 64,ParticleDuration: 2000,RandomDuration: 500,Scale: 0.025f,RandomScale: 0.01f,Opacity: 0.7f,ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 64192, EndTime: 66933,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 66933, EndTime: 69370,
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 66933, EndTime: 69370,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 69370, EndTime: 71807, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 69370, EndTime: 71807, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 71807, EndTime: 74243, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.5f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 71807, EndTime: 74243, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 2, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Back, Path: "sb/pl.png", StartTime: 74243, EndTime: 77593, 
                ParticleAmount: 64, ParticleDuration: 4000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 74243, EndTime: 77593, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 77593, EndTime: 79116, 
                ParticleAmount: 64, ParticleDuration: 4000, RandomDuration: 500, Scale: 0.015f, RandomScale: 0.005f, Opacity: 0.7f, ParticleColour: Color4.Black, 
                RandomSeed: 2
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 79116, EndTime: 88862, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 79116, EndTime: 88862, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 15, Rotation: true, Additive: true
            );

            //----------------------------------------- Verse 2 ---------------------------------------------------//
            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 88939, EndTime: 98532, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 88939, EndTime: 98532, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.05f, ParticleColour: Color4.White, 
                RandomSeed: 5, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 113152, EndTime: 118177,
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 113152, EndTime: 118177,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );

            //----------------------------------------- Chorus 2 --------------------------------------------------//
            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 135233, EndTime: 139725,
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 135233, EndTime: 139725,
                ParticleAmount: 16, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Back, Path: "sb/pl.png", StartTime: 139725, EndTime: 142466,
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 139725, EndTime: 142466,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 142466, EndTime: 144903,
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 142466, EndTime: 144903,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White,
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 144903, EndTime: 147340, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 144903, EndTime: 147340, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 147340, EndTime: 153126, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.5f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 147340, EndTime: 153126, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 2, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 153126, EndTime: 154649, 
                ParticleAmount: 64, ParticleDuration: 4000, RandomDuration: 500, Scale: 0.015f, RandomScale: 0.005f, Opacity: 0.7f, ParticleColour: Color4.Black, 
                RandomSeed: 2
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 154649, EndTime: 164472, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 154649, EndTime: 164472, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 15, Rotation: true, Additive: true
            );
            
            //----------------------------------------- Solo ------------------------------------------------------//
            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 164472, EndTime: 174218, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 164472, EndTime: 174218, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 15, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 174218, EndTime: 179091, 
                ParticleAmount: 128, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 0
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 174218, EndTime: 179091, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 179091, EndTime: 181527, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 179091, EndTime: 181527, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 181527, EndTime: 183964, 
                ParticleAmount: 128, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 0
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 181527, EndTime: 183964, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 183964, EndTime: 193253, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.3f, ParticleColour: Color4.White, 
                RandomSeed: 0
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 183964, EndTime: 193253, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            //----------------------------------------- Chorus 3 --------------------------------------------------//
            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 194928, EndTime: 199421,
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 194928, EndTime: 199421,
                ParticleAmount: 16, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Back, Path: "sb/pl.png", StartTime: 199421, EndTime: 202162,
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 199421, EndTime: 202162,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 202162, EndTime: 204598,
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 202162, EndTime: 204598, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 204598, EndTime: 207035, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 204598, EndTime: 207035, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.01f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 0,
                Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 207035, EndTime: 212822, 
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.5f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 207035, EndTime: 212822, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 2, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 212822, EndTime: 214345, 
                ParticleAmount: 64, ParticleDuration: 4000, RandomDuration: 500, Scale: 0.015f, RandomScale: 0.005f, Opacity: 0.7f, ParticleColour: Color4.Black, 
                RandomSeed: 2
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 214345, EndTime: 233913, 
                ParticleAmount: 64, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 214345, EndTime: 233913, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 15, Rotation: true, Additive: true
            );

            //----------------------------------------- Ending ----------------------------------------------------//
            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 233913, EndTime: 243659, 
                ParticleAmount: 64, ParticleDuration: 2500, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.5f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 233913, EndTime: 243659, 
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 0, Scale: 0.03f, RandomScale: 0.02f, Opacity: 0.1f, ParticleColour: Color4.White, 
                RandomSeed: 17814, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 253253, EndTime: 258050,
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White, 
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 253253, EndTime: 258050, 
                ParticleAmount: 16, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Back, Path: "sb/pl.png", StartTime: 258050, EndTime: 262999,
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 258050, EndTime: 262999,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Main, Path: "sb/pl.png", StartTime: 262999, EndTime: 267796,
                ParticleAmount: 48, ParticleDuration: 3000, RandomDuration: 1000, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 1
            );
            GenerateDust(
                Layer: Main, Path: "sb/lb.png", StartTime: 262999, EndTime: 267796,
                ParticleAmount: 16,ParticleDuration: 1000,RandomDuration: 500,Scale: 0.02f,RandomScale: 0.01f,Opacity: 0.2f,ParticleColour: Color4.White,
                RandomSeed: 0, Rotation: true, Additive: true
            );

            GenerateDust(
                Layer: Verse, Path: "sb/pl.png", StartTime: 267873, EndTime: 272898,
                ParticleAmount: 64, ParticleDuration: 2000, RandomDuration: 500, Scale: 0.025f, RandomScale: 0.01f, Opacity: 0.7f, ParticleColour: Color4.White,
                RandomSeed: 2
            );
            GenerateDust(
                Layer: Verse, Path: "sb/lb.png", StartTime: 267873, EndTime: 272898,
                ParticleAmount: 8, ParticleDuration: 1000, RandomDuration: 500, Scale: 0.02f, RandomScale: 0.01f, Opacity: 0.2f, ParticleColour: Color4.White,
                RandomSeed: 5, Rotation: true, Additive: true
            );
        }

        public void GenerateDust(StoryboardLayer Layer, string Path, int StartTime, int EndTime, int ParticleAmount, int ParticleDuration, int RandomDuration, float Scale, float RandomScale,
            float Opacity, Color4 ParticleColour ,int? RandomSeed = null, bool? Rotation = false, bool? Additive = false)
        {
            Random random = RandomSeed.HasValue ? new Random(RandomSeed.Value) : new Random();

            double duration = EndTime - StartTime;
            var Start = StartTime - ParticleDuration;
            var RealStart = StartTime;
            var RealEnd = EndTime;

            for (var i = 0; i < ParticleAmount; i++)
            {
                var loopDuration = ParticleDuration + Random(-RandomDuration, RandomDuration);
                var loopCount = Math.Max(1, (int)Math.Floor(duration / loopDuration)) + 1;

                var startTime = Random(Start, Start + loopDuration);

                var startPosition = new Vector2(Random(-107, 747), Random(100, 500));
                var endPosition = startPosition + new Vector2(Random(-100, 100), -Random(100, 200));

                var sprite = Layer.CreateSprite(Path);
                sprite.Fade(RealStart, RealStart, 0, Opacity);
                sprite.Fade(RealEnd, RealEnd, Opacity, 0);
                if (ParticleColour != Color4.White) sprite.Color(startTime, ParticleColour);
                if (Rotation.Value) sprite.Rotate(startTime, RandomFloat((float)-Math.PI, (float)Math.PI));
                if (Additive.Value) sprite.Additive(startTime);

                var ParticleScale = Scale + RandomFloat(-RandomScale, RandomScale);
                sprite.StartLoopGroup(startTime, loopCount);
                sprite.Scale(OsbEasing.OutSine, 0, 250, 0, ParticleScale);
                sprite.Scale(OsbEasing.InSine, loopDuration - 250, loopDuration, ParticleScale, 0);
                sprite.MoveX(OsbEasing.InOutSine, 0, loopDuration, startPosition.X, endPosition.X);
                sprite.MoveY(0, loopDuration, startPosition.Y, endPosition.Y);
                sprite.EndGroup();
            }

            int Random(int min, int max) => random.Next(min, max);
            float RandomFloat(float min, float max) => (float)(min + random.NextDouble() * (max - min));
        }
    }
}
