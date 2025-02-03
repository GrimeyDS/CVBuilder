param name string
param backendName string
param frontendName string
param location string = resourceGroup().location

targetScope = 'resourceGroup'

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: name
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
    capacity: 1
  }
  properties: {
    reserved: true
  }
  kind: 'linux'
}

resource backendAppService 'Microsoft.Web/sites@2024-04-01' = {
  name: backendName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
    }
  }
}

resource frontendAppService 'Microsoft.Web/sites@2024-04-01' = {
  name: frontendName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'NODE|20-lts'
      appCommandLine: 'pm2 serve /home/site/wwwroot --no-daemon --spa'
    }
  }
}

output backendOutboundIps array = split(backendAppService.properties.outboundIpAddresses, ',')
