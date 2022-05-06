using System;
using JetBrains.Annotations;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;

namespace SapphicDev.Terraria.SilentEnemyDefeat;

[ApiVersion(2, 1)]
[UsedImplicitly]
public class SilentEnemyDefeatPlugin : TerrariaPlugin
{
    public SilentEnemyDefeatPlugin(Main game) : base(game)
    {
    }

    public override string Name => "SilentEnemyDefeat";

    public override Version Version => new(1, 0);

    public override string Author => "Chloe Dawn";

    public override string Description => "Suppresses enemy defeat announcements";

    private static void OnServerBroadcast(ServerBroadcastEventArgs e)
    {
        if (e.Message is
            {
                _text: "Game.EnemiesDefeatedByAnnouncement",
                _mode: NetworkText.Mode.LocalizationKey
            })
        {
            e.Handled = true;
        }
    }

    public override void Initialize()
    {
        ServerApi.Hooks.ServerBroadcast.Register(this, OnServerBroadcast);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServerApi.Hooks.ServerBroadcast.Deregister(this, OnServerBroadcast);
        }

        base.Dispose(disposing);
    }
}