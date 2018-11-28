# Variables
$resourceGroup = "CustomVMsGroup"
$location = "westus"
$vmName = "Srv01"
$subNet = "MySubnet1"
$virtualNetwork = "MYvNET1"
$networkSecurityGroup = "MyNetworkSecurityGroup1"
$nic = "MyNic1"
$publicIp = "MypublicDNS1"
$size = "Standard_B1s"
$credentials = Get-Credential -Message "Ingresa un user y password."

New-AzureRmVM `
-ResourceGroupName $resourceGroup `
-Name $vmName `
-Location $location `
-Credential $credentials `
-VirtualNetworkName $virtualNetwork `
-SubnetName $subNet `
-SecurityGroupName $networkSecurityGroup `
-PublicIpAddressName "$publicIp$(Get-Random)" `
-OpenPorts 3389 `
-Size $size `
-ImageName Win2016Datacenter

New-AzureRmVM `
-ResourceGroupName $resourceGroup `
-Name "Srv02" `
-Location $location `
-Credential $credentials `
-VirtualNetworkName $virtualNetwork `
-SubnetName $subNet `
-SecurityGroupName $networkSecurityGroup `
-PublicIpAddressName "$publicIp$(Get-Random)" `
-OpenPorts 3389 `
-Size $size `
-ImageName Win2016Datacenter