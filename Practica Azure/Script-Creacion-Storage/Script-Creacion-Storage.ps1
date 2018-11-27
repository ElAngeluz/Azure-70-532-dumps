# Set the name of the storage account and the SKU name. 
$storageAccountName = "coursestorage12345"
$skuName = "Standard_LRS"
$resourceGroup = "CustomVMsGroup"
$location = "westus"

# Create the storage account.
New-AzureRmStorageAccount -ResourceGroupName $resourceGroup `
  -Name $storageAccountName `
  -Location $location `
  -SkuName $skuName