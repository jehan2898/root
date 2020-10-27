

_menuCloseDelay=500           // The time delay for menus to remain visible on mouse out
_menuOpenDelay=150            // The time delay before menus open on mouse over
_subOffsetTop=10              // Sub menu top offset
_subOffsetLeft=-10            // Sub menu left offset



with(menuStyle=new mm_style()){
onbgcolor="#4F8EB6";
oncolor="#ffffff";
offbgcolor="#DCE9F0";
offcolor="#515151";
bordercolor="#296488";
borderstyle="solid";
borderwidth=1;
separatorcolor="#2D729D";
separatorsize="1";
padding=5;
fontsize="75%";
fontstyle="normal";
fontfamily="Verdana, Tahoma, Arial";
pagecolor="black";
pagebgcolor="#82B6D7";
headercolor="#000000";
headerbgcolor="#ffffff";
subimage="arrow.gif";
subimagepadding="2";
overfilter="Fade(duration=0.2);Alpha(opacity=90);Shadow(color='#777777', Direction=135, Strength=5)";
outfilter="randomdissolve(duration=0.3)";
}


with(milonic=new menuname("Milonic")){
style=menuStyle;
top=262;
left=166;
aI("text=Product Purchasing Page;url=http://www.milonic.com/cbuy.php;");
aI("text=Contact Us;url=http://www.milonic.com/contactus.php;");
aI("text=Newsletter Subscription;url=http://www.milonic.com/newsletter.php;");
aI("text=FAQ;url=http://www.milonic.com/menufaq.php;");
aI("text=Discussion Forum;url=http://www.milonic.com/forum/;");
aI("text=Software License Agreement;url=http://www.milonic.com/license.php;");
aI("text=Privacy Policy;url=http://www.milonic.com/privacy.php;");
}

with(milonic=new menuname("DataEntry")){
style=menuStyle;
top=90;
left=209;
orientation="horizontal";
aI("text=Matter;showmenu=MatterMenu;url=;");
aI("text=Client;showmenu=ClientMenu;url=;status=;");
aI("text=Insurer;showmenu=InsurerMenu;url=;status=;");
aI("text=Firm;showmenu=FirmMenu;url=;status=;");
aI("text=Claim;showmenu=ClaimMenu;url=;status=;");
aI("text=Attorney;showmenu=AttorneyMenu;url=;status=;");
aI("text=View Mailing Proof;url=;status=;");
aI("text=View Important Cases;url=;status=;");

}

with(milonic=new menuname("SearchEntry")){
style=menuStyle;
top=90;
left=309;
orientation="horizontal";
aI("text=Search;showmenu=SearchMenu;url=;status=;");

}
with(milonic=new menuname("Admin")){
style=menuStyle;
top=90;
left=348;
orientation="horizontal";
aI("text=Search;showmenu=AdminMenu;url=;status=;");
}

with(milonic=new menuname("AdminMenu")){
style=menuStyle;
top=90;
left=348;
orientation="horizontal";
aI("text=Status;url=;status=;");
aI("text=Desk;showmenu=DeskMenu; url=;status=;");
aI("text=Fee Type;url=;status=;");
aI("text=Manage Docs;url=;status=;");
aI("text=Manage Users;url=;status=;");
}

with(milonic=new menuname("DeskMenu")){
style=menuStyle;
orientation="horizontal";
aI("text=Add Desk;url=;status=;");
aI("text=Assign Desk;url=;status=;");
}

with(milonic=new menuname("MatterMenu")){
style=menuStyle;
aI("text=Add Matter;url=indexPage.asp?Tab=DATA%20ENTRY&qry=addclaim&screen=dentry&alphabet=All&ins=All;status=Add Matter;status=;");
aI("text=Edit Matter;url=indexPage.asp?Tab=DATA%20ENTRY&qry=em&screen=dentry&alphabet=All&ins=All;status=;");
}

with(milonic=new menuname("ClientMenu")){
style=menuStyle;
aI("text=Add Client;url=;status=;");
aI("text=Edit Client;url=;status=;");
}

with(milonic=new menuname("InsurerMenu")){
style=menuStyle;
aI("text=Add Insurer;url=;status=;");
aI("text=Edit Insurer;url=;status=;");
}

with(milonic=new menuname("FirmMenu")){
style=menuStyle;
aI("text=Add Firm;url=;status=;");
aI("text=Edit Firm;url=;status=;");
}

with(milonic=new menuname("ClaimMenu")){
style=menuStyle;
aI("text=Add Claim;url=;status=;");
aI("text=Edit Claim;url=;status=;");
}

with(milonic=new menuname("AttorneyMenu")){
style=menuStyle;
aI("text=Add Attorney;url=;status=;");
aI("text=Edit Attorney;url=;status=;");
}

with(milonic=new menuname("SearchMenu")){ 
style=menuStyle; 
top=90;
left=397;
keepalive=1; 
aI("text=<table border=0 height=1><tr valign=top><td class=moduleHeader><form method=get action=indexPage.asp?Tab=SEARCH><label for=q><input type=text name=gs id=gs size=15/></label><input type=submit value=Search class=buttonsizerSmall /></form>Enter text to search  <img border=0 src=images/search.gif width=24 height=24></td></tr></table>;type=form;offbgcolor=#FFFCF3;"); 
}

drawMenus();

