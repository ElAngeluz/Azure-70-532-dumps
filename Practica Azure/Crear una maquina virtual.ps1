# Variables
$resourceGroup = "CustomVMsGroup"
$location = "westus"
$vmName = "CustomVM1"
$subNet = "MySubnet1"
$virtualNetwork = "MYvNET1"
$networkSecurityGroup = "MyNetworkSecurityGroup1"
$nic = "MyNic1"
$publicIp = "MypublicDNS1"
$size = "Standard_D2_v2"
$credentials = Get-Credential -Message "Ingresa un usuario y Contrasena."

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