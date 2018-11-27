configuration CourseServerConfig
{   
    Import-DscResource -ModuleName PSDesiredStateConfiguration;
    node ("localhost")
    {     
        WindowsFeature IIS
        {
           Ensure = "Present"
           Name   = "Web-Server"
           IncludeAllSubFeature = $true
        }

        File RequiredDirectory
        {
            Ensure          = "Present"            
            DestinationPath = "C:\Required"
            Type            = "Directory"
            DependsOn       = "[WindowsFeature]IIS"
        }       
    }
}

CourseServerConfig

Start-DscConfiguration -Path .\CourseServerConfig -Wait -force
