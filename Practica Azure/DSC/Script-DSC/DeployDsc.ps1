$resourceGroup ='CustomVMsGroup'
$location = 'eastus'
$vmName = 'Srv01'
$StorageName = 'customvmsgroupdisks'

Publish-AzureRmVMDscConfiguration -ConfigurationPath '.\Practica Azure\DSC\Script-DSC\Script-DSC.ps1' -ResourceGroupName $resourceGroup -StorageAccountName $StorageName -Force

Set-AzureRmVMDscExtension -Version 2.21 -ResourceGroupName $resourceGroup -VMName $vmName -ArchiveStorageAccountName $StorageName -ArchiveBlobName windowsWebServer.ps1.zip -AutoUpdate: $true -ConfigurationName CourseServerConfig