$resourceGroup = "CustomVMsGroup"
$location = "westus"
$vmName = "CustomVM1"
$storageName = "coursestorage12345"

Publish-AzureRmVMDscConfiguration -ConfigurationPath .\Script-DSC.ps1  `
-ResourceGroupName $resourceGroup `
-StorageAccountName $storageName -force

Set-AzureRmVMDscExtension -Version 2.21  `
-ResourceGroupName $resourceGroup `
-VMName $vmName `
-ArchiveStorageAccountName $storageName `
-ArchiveBlobName Script-DSC.ps1.zip `
-AutoUpdate:$true `
-ConfigurationName CourseServerConfig 
