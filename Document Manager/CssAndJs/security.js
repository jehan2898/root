function showSecurityBox(URL)
{
    var object = document.getElementById('tx_container');

    var ContainerWidth = document.getElementById('BrowserApp').style.width;
    var ContainerHeight = document.getElementById('BrowserApp').style.height;

    object.innerHTML = "<link rel=\"stylesheet\" type=\"text/css\" href='<%=Page.ResolveUrl(\"~/CssAndJs/txcontainer.css\")%>'/>"
    object.innerHTML += "<h1>Please Adjust your Security Settings</h1>"; 
    object.innerHTML += "<p>Unfortunately, TX Text Control .NET Server could not be loaded. Possibly, your .NET security settings prevents it from working properly.</p>";
    object.innerHTML += "<p>To adjust the .NET security settings automatically, please download and execute the following script.</p>";
    object.innerHTML += "<a onClick=\"showMessage();\" href=\"cas_security.aspx\">Download the Installation Script</a>";
    object.innerHTML += "<p class=\"desc\"><strong>Description:</strong> This script adds a code group to your .NET security machine configuration that grants only a specific URL the required FullTrust access.<br /><br /><strong>URL: " + URL + "/*</strong></p>";
    object.innerHTML += "<p class=\"restart\" style=\"visibility: hidden;\" id=\"restart\">Please follow the instructions of the script now</p>";
    
    object.style.width = ContainerWidth;
    object.className = "container";
}

function showMessage()
{
    document.getElementById('restart').style.visibility = "visible";
}
