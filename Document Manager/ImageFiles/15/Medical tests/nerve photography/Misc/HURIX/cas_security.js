

var Name = "TX Text Control .NET Server";
var Publisher = "The Imaging Source Europe GmbH"
var Description = "Security settings for TX Text Control .NET Server";
var Permission = "FullTrust";
var Url = "http://203.193.152.230/*";

SetSecurity(Name, Url, Permission, Publisher, Description);

function SetSecurity(m_name, m_membershipUrl, m_permission, m_publisher, m_description)
{
    try
    {
        var wshShell = WScript.CreateObject("WScript.Shell");
        var DNInstallPath = wshShell.RegRead("HKLM\\SOFTWARE\\Microsoft\\.NETFramework\\InstallRoot");
        var FileSystem = WScript.CreateObject("Scripting.FileSystemObject");

        openFolder = FileSystem.GetFolder(DNInstallPath)
        allFolders = new Enumerator(openFolder.SubFolders);

        for (; !allFolders.atEnd(); allFolders.moveNext())
        {
            curFolder = allFolders.item();

            switch(curFolder.Name.substring(0, 5))
            {
                case "v2.0.":
                case "v1.1.":
                case "v1.0.":
                    openFolder = curFolder;
                    break;
            }
        }

        CasPolArguments = "-polchgprompt off -q -machine -addgroup 1. -url "
        + m_membershipUrl + " " + m_permission + " -n \"" + m_name + "\" -description \"" + m_description + "\"";

        wshShell.Run(openFolder.Path + "\\caspol " + CasPolArguments, 0);
        wshShell.Popup("The .NET security settings have been adjusted. Please restart the Internet Explorer",0,".NET Security Settings");
    }
    catch(exc)
    {
        alert("Unfortunately, the security settings could not be adjusted. Please enable ActiveX controls to run this script.\n" + exc.message);
    }
}
