﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class SharedCreateMessageBoxEventPayloadHandler : GameMessageHandler<SharedCreateMessageBoxEventPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedCreateMessageBoxEventPayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved MessageBox: {payload.Message}");

			return Task.CompletedTask;
		}
	}
}
