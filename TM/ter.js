//****************************************************************************************
// Constants and variables
//***************************************************************************************
MENU_HEIGHT=20;

CurMenu=null;            // menu currently menu
toc=null;                // ter ActiveX control
IE = false;
  
// ***************************************************************************************
// Initialize the edit control
// ***************************************************************************************
function InitTE() 
{
    toc=WebTer1.object;

    TopMenu.style.background=toc.TerGetSysColorText(COLOR_MENU); 
    TopMenu.style.color=toc.TerGetSysColorText(COLOR_MENUTEXT); 

    toc.TerCommand(ID_SHOW_PAGE_BORDER);  // show page borders
   


    // Examples of adding or removing toolbar icons
    //ter.TerAddToolbarIcon(0,0,ID_SUPSCR_ON,"http://localhost/DmoTer/images/icon1.bmp","superscript"); 
    //ter.TerAddToolbarIcon(0,0,ID_SUBSCR_ON,"http://localhost/DmoTer/images/icon2.bmp","subscript");
    //ter.TerHideToolbarIcon(23,true);  // TLB_HELP = 23

    //ter.TerRecreateToolbar(true);

    // Example of removing a menu item, or modifying its description
    //toc.TerSetMenuItemDesc(ID_PASTE,"Paste From Clipboard");     // modify menu item description
    //toc.TerSetMenuItemDesc(ID_PASTE,"");                         // remove an item form menu
}

// ***************************************************************************************
// Loading page
// ***************************************************************************************
function LoadPage() 
{
    if (navigator.appName == "Microsoft Internet Explorer") IE=true;
    else                                                    IE=false;

    if (!IE) {
       // position messages
       BrowserCheck.style.left=document.body.clientLeft;
       BrowserCheck.style.top=document.body.clientTop+3*MENU_HEIGHT;
       BrowserCheck.style.width=document.body.clientWidth;
       BrowserCheck.style.height=120; // document.body.clientWidth;
       
       BrowserCheck.style.visibility="visible";
       TopMenu.style.visibility="hidden";
       return;
    }

    toc=WebTer1.object;
    toc.TerSetWinBorder(0);

    ResizeObjects();
    zigzag();
}  

function zigzag() {
            window.resizeTo(screen.availWidth,screen.availHeight)
            window.moveTo(0,0)
            var incrementX = 2
            var incrementY = 2
            var floor = screen.availHeight - window.outerHeight
            var rightEdge = screen.availWidth - window.outerWidth
            for (var i = 0; i < rightEdge; i += 2) {
                window.moveBy(incrementX, incrementY)
                if (i%60 == 0) {
                    incrementY = -incrementY
                }
            }
        }

// ***************************************************************************************
// Unloading page
// ***************************************************************************************
function UnloadPage() 
{
    if (IE && toc.TerQueryExit()==0) event.returnValue='Modification not saved.'; 
}
  
// ***************************************************************************************
// ShowMenu
// ***************************************************************************************
function ShowMenu(menu,MenuId) 
{
   CurMenu=menu;
   menu.className="ClsMenuSelect";

   toc.TerShowSubMenu(MenuId,menu.offsetLeft,0);
   
   menu.className="ClsMenu";  // set the regular menu class when no pop-up is active
   CurMenu=null;

   //menu.style.left=menu.offsetLeft;
   //menu.style.pixelTop=menu.offsetHeight + menu.offsetTop + 50; // marginheight

}  

// ***************************************************************************************
// HideMenu:  Hide any visible menu
// ***************************************************************************************
function ExitMenu(menu) 
{
   if (CurMenu==null) menu.className="ClsMenu";  // set the regular menu class when no pop-up is active
}

// ***************************************************************************************
// OverMenu:  Mouse over menu item
// ***************************************************************************************
function OverMenu(menu) 
{
   menu.className="ClsMenuOver"
}  

// ***************************************************************************************
// ResizeObjects:
// ***************************************************************************************
function ResizeObjects() 
{
    // position the editor
    WebTer1.style.left=document.body.clientLeft;
    WebTer1.style.top=document.body.clientTop+MENU_HEIGHT;

    WebTer1.style.width=document.body.clientWidth;
    WebTer1.style.height=document.body.clientHeight-MENU_HEIGHT;

    // postion the top menu
    TopMenu.style.width=document.body.clientWidth;
}  

