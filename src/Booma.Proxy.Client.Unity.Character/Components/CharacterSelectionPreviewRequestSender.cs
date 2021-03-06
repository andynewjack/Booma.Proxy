﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class CharacterSelectionPreviewRequestSender : MonoBehaviour
	{
		/// <summary>
		/// The number of characters to request the preview for.
		/// </summary>
		[Tooltip("The number of supported characters.")]
		[MaxValue(byte.MaxValue)]
		[MinValue(1)]
		[SerializeField]
		private int CharacterCount;

		[Inject]
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		public void SendCharacterPreviewRequests()
		{
			//Send the request for all
			for(int i = 0; i < CharacterCount; i++)
				SendService.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)i, CharacterSelectionType.Preview));
		}
	}
}
