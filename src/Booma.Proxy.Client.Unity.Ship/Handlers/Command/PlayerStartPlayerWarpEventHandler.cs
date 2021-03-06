﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class PlayerStartPlayerWarpEventHandler : ContextExtendedCommand60Handler<Sub60StartNewWarpCommand, INetworkPlayerNetworkMessageContext>
	{
		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60StartNewWarpCommand command, INetworkPlayerNetworkMessageContext commandContext)
		{
			//TODO: We should remove the player's physical representation from the current map if they're in it.
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player ID: {command.Identifier} starting warp to ZoneId: {command.ZoneId} - Unused: {command.Unused1} {command.Unused2}");
		}
	}
}
