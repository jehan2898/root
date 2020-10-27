function __showContextMenu(menu, rowID)
{
	var menuOffset = 2
    menu.style.left = window.event.x - menuOffset;
    menu.style.top = window.event.y - menuOffset;
    menu.style.display = "";
 
    var _rowID = document.getElementById('__ROWID');
    if (_rowID != null)
       _rowID.value = rowID;
	
    window.event.cancelBubble = true;
    
    return false;
}

function __trapESC(menu)
{
	var key = window.event.keyCode;
	if (key == 27)
	{
		menu.style.display = 'none';
	}
}