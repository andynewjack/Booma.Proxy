﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore.Serializer;
using SceneJect.Common;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Module responsible for registering the dependencies associated with the login client.
	/// </summary>
	public sealed class LoginClientDependencyRegisterModule : NonBehaviourDependency
	{
		[SerializeField]
		private LogLevel LoggingLevel;

		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			EncryptionLazyWithoutKeyDecorator<byte[]> encrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val);
				return encryptionService;
			});
			EncryptionLazyWithoutKeyDecorator<byte[]> decrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			});

			//Create the serializer and register all the needed types
			SerializerService Serializer = new SerializerService();

			//Registers all the types.
				PacketCommonServerMetadataMarker.SerializableTypes
				.Concat(PacketLoginServerMetadataMarker.SerializableTypes)
				.ToList().ForEach(t => Serializer.RegisterType(t));

			Serializer.Compile();

			IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer> client = new PSOBBNetworkClient()
				.AddCryptHandling(encrypt, decrypt, 8)
				.AddHeaderReading(Serializer, 8)
				.AddNetworkMessageReading(Serializer)
				.For<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>()
				.AsManaged(new UnityLoggingService(LoggingLevel));

			register.RegisterTransient<DefaultMessageContextFactory, IClientMessageContextFactory>();

			register.RegisterInstance<IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer>,
				IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer>>(client);

			//Also need to register the crypto service associated with the client.
			register.RegisterInstance<IFullCryptoInitializationService<byte[]>, IFullCryptoInitializationService<byte[]>>(new SeperateAggregateCryptoInitializationService<byte[]>(encrypt, decrypt));
		}
	}
}
