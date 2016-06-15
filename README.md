# NSBTransportBridge
Bridging on-prem msmq transport endpoint to AzureServiceBus transport endpoint  

`Earth` is an endpoint on msmq transport, periodically publishing events.  

The `Bridge` starts 2 endpoints.  
 - "AzureMsmqBridge.azure" : subscribe listens to azure queues, forwards to msmq (TODO)
 - "AzureMsmqBridge.msmq" : subscribe and listens to msmq, forwards to azure

`SurfLeague` is an endpoint on AzureServiceBus transport. It will subscribe to the events forwarded by the bridge.

# Run

- Create an AzureServiceBus queue in the azure management portal
- Set the appsettings `Microsoft.ServiceBus.ConnectionString` in the Bridge app.config,
- Set the appsettings `Microsoft.ServiceBus.ConnectionString` in the SurfLeague app.config

Then

- Run Earth to publish message the ISwellSizeChanged event
- Run Bridge to subscribe to Earth ISwellSizeChanged
- [TODO] Run SurfLeague to receive the forwarded message from the Bridge
