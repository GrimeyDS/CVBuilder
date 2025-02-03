param name string
param username string
@secure()
param password string

param dbName string

param location string = resourceGroup().location

param allowedIps array

targetScope = 'resourceGroup'

resource sqlServer 'Microsoft.Sql/servers@2024-05-01-preview' = {
  name: name
  location: location
  properties: {
    administratorLogin: username
    administratorLoginPassword: password
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2024-05-01-preview' = {
  parent: sqlServer
  name: dbName
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}

resource firewallRule 'Microsoft.Sql/servers/firewallRules@2024-05-01-preview' = [for ip in allowedIps: {
  parent: sqlServer
  name: 'AllowIp-${ip}'
  properties: {
    startIpAddress: ip
    endIpAddress: ip
  }
}]
