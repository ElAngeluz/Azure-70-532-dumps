$directoryPath = "c:\Required"
$filePath = "c:\Required\required.txt"
if(!(Test-Path $directoryPath)){
    New-Item -ItemType Directory -Path $directoryPath -Force
}
if(!(Test-Path $filePath)){
    New-Item -ItemType File -Path $directoryPath -Name requiere.txt -Value $env:COMPUTERNAME
}