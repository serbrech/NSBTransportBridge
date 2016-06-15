# NSBTransportBridge
Bridging on-prem msmq based endpoint to azure service bus transport  

Earth is an endpoint on msmq publishing events on msmq.  

The Bridge starts 2 endpoints.  
 - "AzureMsmqBridge.azure" : listens to azure queues, forwards to msmq
 - "AzureMsmqBridge.msmq" : listens to msmq, forwards to azure

SurfLeague is an AzureServiceBus endpoint. will listen to the events forwarded by the bridge.
