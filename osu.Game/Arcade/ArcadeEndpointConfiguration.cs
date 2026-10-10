// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Logging;
using osu.Game.Online;

namespace osu.Game.Arcade
{
    public class ArcadeEndpointConfiguration : EndpointConfiguration
    {
        public ArcadeEndpointConfiguration()
        {
            WebsiteUrl = @"https://osu.ppy.sh";

            string? envApiUrl = Environment.GetEnvironmentVariable(@"OSU_ARCADE_APIURL");

            // If osu-server-spectator / osu-web is running on a local computer with its own database,
            // there's not a guarantee the IP/secrets/ID is consistent.
            //
            // This allows the client, controlled by the operator, to specify
            // certain settings on relaunch instead of needing to recompile.
            APIUrl = envApiUrl ?? @"https://osu.ppy.sh";
            Logger.Log($@"[ARCADE] Initializing EndpointConfiguration with APIUrl {APIUrl}");

            APIClientSecret = Environment.GetEnvironmentVariable(@"OSU_ARCADE_APICLIENTSECRET") ?? @"3LP2mhUrV89xxzD1YKNndXHEhWWCRLPNKioZ9ymT";
            APIClientID = Environment.GetEnvironmentVariable(@"OSU_ARCADE_APICLIENTID") ?? @"5";

            // Assuming default docker setup.
            string spectatorServerRootUrl = Environment.GetEnvironmentVariable(@"OSU_ARCADE_SPECTATORSERVERURL") ?? "https://spectator.osu.ppy.sh";
            SpectatorUrl = $@"{spectatorServerRootUrl}/spectator";
            MultiplayerUrl = $@"{spectatorServerRootUrl}/multiplayer";
            MetadataUrl = $@"{spectatorServerRootUrl}/metadata";
            ArcadeUrl = $@"{spectatorServerRootUrl}/arcade";
            BeatmapSubmissionServiceUrl = $@"http://localhost:5089";

            Logger.Log(@$"ArcadeUrl = {ArcadeUrl}");
        }
    }
}
