param saName string

param sqlName string
param sqlUsername string

@secure()
param sqlPassword string
param sqldbName string

param appServiceName string
param frontendName string
param backendName string

param location string

targetScope = 'resourceGroup'

module sa './modules/storage-account.bicep' = {
  name: saName
  params: {
    name: saName
  }
}

module app './modules/application.bicep' = {
  name: appServiceName
  params: {
    name: appServiceName
    backendName: backendName
    frontendName: frontendName
    location: location
  }
}

module sqldb './modules/mssql-database.bicep' = {
  name: sqlName
  params: {
    allowedIps: app.outputs.backendOutboundIps
    name: sqlName
    username: sqlUsername
    password: sqlPassword
    dbName: sqldbName
    location: location
  }
}
