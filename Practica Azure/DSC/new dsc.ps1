configuration IIS {
    node ("localhost")
    {
        # Call Resource Provider
        # E.g: WindowsFeature, File
        WindowsFeature WebServer
        {
            Ensure = "Present"
            Name = "web-server"
            IncludeAllSubFeature = $true
        }

    }
}