using 'main.bicep'

param location = 'westeurope'

param saName = 'oncorecvbuilder'

param sqlName = 'sql-cvbuilder-prod-weu-001'
param sqldbName = 'sqldb-cvbuilder-prod-weu-001'
param sqlUsername = 'sqladmin'
@secure()
param sqlPassword = ''


param appServiceName = 'asp-cvbuilder-prod-weu-001'
param frontendName = 'app-cvbuilderclient-prod-weu-001'
param backendName = 'app-cvbuilderapi-prod-weu-001'
