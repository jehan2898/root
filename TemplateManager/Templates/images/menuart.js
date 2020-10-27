//////////////////////////////////////////////////
// DMB DHTML ENGINE 1.3.003                     //
// (c)xFX JumpStart                             //
//                                              //
// PSN: 4058-161142-XFX-3424                    //
//                                              //
// GENERATED: 12/24/2002 - 1:38:53 PM           //
// -------------------------------------------- //
//  Config: Local                               //
//   AddIn: DropShadow PRO                      //
// JS Name: menuart                             //
//////////////////////////////////////////////////

	
	var nStyle = new Array;
	var hStyle = new Array;
	var nLayer = new Array;
	var hLayer = new Array;
	var nTCode = new Array;
	
	var AnimStep = 0;
	var AnimHnd = 0;
	var HTHnd = new Array;
	var MenusReady = false;
	var imgLRsc = new Image;
	var imgRRsc = new Image;
	var smHnd = 0;
	var lsc = null;
	var tmrHideHnd = 0;
	var IsOverHS = false;
	var IsContext = false;
	var IsFrames = false;
	var AnimSpeed = 23;
	var TimerHideDelay = 3805;
	var SubMenusDelay = 2000;

	var cntxMenu = 'grpDentry';
	var DoFormsTweak = true;

	function GetOPStyle(){;}function SetOPStyle(){;}
	
	var nsOW;
	var nsOH;
	
	var mFrame;
	var cFrame = self;
	
	var OpenMenus = new Array;
	var nOM = 0;
	
	var mX;
	var mY;
	
	var BV=parseFloat(navigator.appVersion.indexOf("MSIE")>0?navigator.appVersion.split(";")[1].substr(6):navigator.appVersion);
	var BN=navigator.appName;
	var IsWin=(navigator.userAgent.indexOf('Win')!=-1);
	var IsMac=(navigator.userAgent.indexOf('Mac')!=-1);
	var KQ=(BN.indexOf('Konqueror')!=-1&&(BV>=5))?true:false;
	var OP=(navigator.userAgent.indexOf('Opera')!=-1&&BV>=4)?true:false;
	var NS=(BN.indexOf('Netscape')!=-1&&(BV>=4&&BV<5)&&!OP)?true:false;
	var SM=(BN.indexOf('Netscape')!=-1&&(BV>=5)||OP)?true:false;
	var IE=(BN.indexOf('Explorer')!=-1&&(BV>=4)||SM||KQ)?true:false;
	
	var CanDoShadow = (IE&&(BV>=5.5))&&!(SM||OP);
	
	if(!eval(frames['self'])) {
	frames.self = window;
	frames.top = top;
	}
	
	function DMB_changeImages(){if(document.images&&(preloadFlag==true))for(var i=0;i<arguments.length;i+=2)document[arguments[i]].src=arguments[i+1];}

	

	
	var fx = 3;


	

	
	hStyle[0]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[1]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[2]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[3]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[4]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[5]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[6]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[7]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[8]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[9]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[10]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[11]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[12]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[13]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[14]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[15]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[16]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[17]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[18]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[19]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[20]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[21]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	hStyle[22]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #00287B; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;";
	var Add_MatterLImgOff = new Image;
	var Add_MatterLImgOn = new Image;
	Add_MatterLImgOff.src = rimPath+'folder_closed.gif';
	Add_MatterLImgOn.src = rimPath+'folder_open.gif';
	var Add_MatterBImgOff = new Image;
	var Add_MatterBImgOn = new Image;
	Add_MatterBImgOff.src = rimPath+'oxpback.jpg';
	Add_MatterBImgOn.src = rimPath+'oxpback.jpg';
	var Edit_MatterLImgOff = new Image;
	var Edit_MatterLImgOn = new Image;
	Edit_MatterLImgOff.src = rimPath+'folder_closed.gif';
	Edit_MatterLImgOn.src = rimPath+'folder_open.gif';
	var Edit_MatterBImgOff = new Image;
	var Edit_MatterBImgOn = new Image;
	Edit_MatterBImgOff.src = rimPath+'oxpback.jpg';
	Edit_MatterBImgOn.src = rimPath+'oxpback.jpg';
	var Add_ClientLImgOff = new Image;
	var Add_ClientLImgOn = new Image;
	Add_ClientLImgOff.src = rimPath+'folder_closed.gif';
	Add_ClientLImgOn.src = rimPath+'folder_open.gif';
	var Add_ClientBImgOff = new Image;
	var Add_ClientBImgOn = new Image;
	Add_ClientBImgOff.src = rimPath+'oxpback.jpg';
	Add_ClientBImgOn.src = rimPath+'oxpback.jpg';
	var View_Edit_ClientLImgOff = new Image;
	var View_Edit_ClientLImgOn = new Image;
	View_Edit_ClientLImgOff.src = rimPath+'folder_closed.gif';
	View_Edit_ClientLImgOn.src = rimPath+'folder_open.gif';
	var View_Edit_ClientBImgOff = new Image;
	var View_Edit_ClientBImgOn = new Image;
	View_Edit_ClientBImgOff.src = rimPath+'oxpback.jpg';
	View_Edit_ClientBImgOn.src = rimPath+'oxpback.jpg';
	var Add_InsurerLImgOff = new Image;
	var Add_InsurerLImgOn = new Image;
	Add_InsurerLImgOff.src = rimPath+'folder_closed.gif';
	Add_InsurerLImgOn.src = rimPath+'folder_open.gif';
	var Add_InsurerBImgOff = new Image;
	var Add_InsurerBImgOn = new Image;
	Add_InsurerBImgOff.src = rimPath+'oxpback.jpg';
	Add_InsurerBImgOn.src = rimPath+'oxpback.jpg';
	var Edit_InsurerLImgOff = new Image;
	var Edit_InsurerLImgOn = new Image;
	Edit_InsurerLImgOff.src = rimPath+'folder_closed.gif';
	Edit_InsurerLImgOn.src = rimPath+'folder_open.gif';
	var Edit_InsurerBImgOff = new Image;
	var Edit_InsurerBImgOn = new Image;
	Edit_InsurerBImgOff.src = rimPath+'oxpback.jpg';
	Edit_InsurerBImgOn.src = rimPath+'oxpback.jpg';
	var Add_FirmLImgOff = new Image;
	var Add_FirmLImgOn = new Image;
	Add_FirmLImgOff.src = rimPath+'folder_closed.gif';
	Add_FirmLImgOn.src = rimPath+'folder_open.gif';
	var Add_FirmBImgOff = new Image;
	var Add_FirmBImgOn = new Image;
	Add_FirmBImgOff.src = rimPath+'oxpback.jpg';
	Add_FirmBImgOn.src = rimPath+'oxpback.jpg';
	var Edit_FirmLImgOff = new Image;
	var Edit_FirmLImgOn = new Image;
	Edit_FirmLImgOff.src = rimPath+'folder_closed.gif';
	Edit_FirmLImgOn.src = rimPath+'folder_open.gif';
	var Edit_FirmBImgOff = new Image;
	var Edit_FirmBImgOn = new Image;
	Edit_FirmBImgOff.src = rimPath+'oxpback.jpg';
	Edit_FirmBImgOn.src = rimPath+'oxpback.jpg';
	var Add_Claim_RepLImgOff = new Image;
	var Add_Claim_RepLImgOn = new Image;
	Add_Claim_RepLImgOff.src = rimPath+'folder_closed.gif';
	Add_Claim_RepLImgOn.src = rimPath+'folder_open.gif';
	var Add_Claim_RepBImgOff = new Image;
	var Add_Claim_RepBImgOn = new Image;
	Add_Claim_RepBImgOff.src = rimPath+'oxpback.jpg';
	Add_Claim_RepBImgOn.src = rimPath+'oxpback.jpg';
	var Edit_Claim_RepLImgOff = new Image;
	var Edit_Claim_RepLImgOn = new Image;
	Edit_Claim_RepLImgOff.src = rimPath+'folder_closed.gif';
	Edit_Claim_RepLImgOn.src = rimPath+'folder_open.gif';
	var Edit_Claim_RepBImgOff = new Image;
	var Edit_Claim_RepBImgOn = new Image;
	Edit_Claim_RepBImgOff.src = rimPath+'oxpback.jpg';
	Edit_Claim_RepBImgOn.src = rimPath+'oxpback.jpg';
	var Add_AttorneyLImgOff = new Image;
	var Add_AttorneyLImgOn = new Image;
	Add_AttorneyLImgOff.src = rimPath+'folder_closed.gif';
	Add_AttorneyLImgOn.src = rimPath+'folder_open.gif';
	var Add_AttorneyBImgOff = new Image;
	var Add_AttorneyBImgOn = new Image;
	Add_AttorneyBImgOff.src = rimPath+'oxpback.jpg';
	Add_AttorneyBImgOn.src = rimPath+'oxpback.jpg';
	var Edit_AttorneyLImgOff = new Image;
	var Edit_AttorneyLImgOn = new Image;
	Edit_AttorneyLImgOff.src = rimPath+'folder_closed.gif';
	Edit_AttorneyLImgOn.src = rimPath+'folder_open.gif';
	var Edit_AttorneyBImgOff = new Image;
	var Edit_AttorneyBImgOn = new Image;
	Edit_AttorneyBImgOff.src = rimPath+'oxpback.jpg';
	Edit_AttorneyBImgOn.src = rimPath+'oxpback.jpg';
	var Request_LetterLImgOff = new Image;
	var Request_LetterLImgOn = new Image;
	Request_LetterLImgOff.src = rimPath+'folder_closed.gif';
	Request_LetterLImgOn.src = rimPath+'folder_open.gif';
	var Request_LetterBImgOff = new Image;
	var Request_LetterBImgOn = new Image;
	Request_LetterBImgOff.src = rimPath+'oxpback.jpg';
	Request_LetterBImgOn.src = rimPath+'oxpback.jpg';
	var Related_CasesLImgOff = new Image;
	var Related_CasesLImgOn = new Image;
	Related_CasesLImgOff.src = rimPath+'folder_closed.gif';
	Related_CasesLImgOn.src = rimPath+'folder_open.gif';
	var Related_CasesBImgOff = new Image;
	var Related_CasesBImgOn = new Image;
	Related_CasesBImgOff.src = rimPath+'oxpback.jpg';
	Related_CasesBImgOn.src = rimPath+'oxpback.jpg';
	var PostingsLImgOff = new Image;
	var PostingsLImgOn = new Image;
	PostingsLImgOff.src = rimPath+'folder_closed.gif';
	PostingsLImgOn.src = rimPath+'folder_open.gif';
	var PostingsBImgOff = new Image;
	var PostingsBImgOn = new Image;
	PostingsBImgOff.src = rimPath+'oxpback.jpg';
	PostingsBImgOn.src = rimPath+'oxpback.jpg';
	var SettlementsLImgOff = new Image;
	var SettlementsLImgOn = new Image;
	SettlementsLImgOff.src = rimPath+'folder_closed.gif';
	SettlementsLImgOn.src = rimPath+'folder_open.gif';
	var SettlementsBImgOff = new Image;
	var SettlementsBImgOn = new Image;
	SettlementsBImgOff.src = rimPath+'oxpback.jpg';
	SettlementsBImgOn.src = rimPath+'oxpback.jpg';
	var HearingsLImgOff = new Image;
	var HearingsLImgOn = new Image;
	HearingsLImgOff.src = rimPath+'folder_closed.gif';
	HearingsLImgOn.src = rimPath+'folder_open.gif';
	var HearingsBImgOff = new Image;
	var HearingsBImgOn = new Image;
	HearingsBImgOff.src = rimPath+'oxpback.jpg';
	HearingsBImgOn.src = rimPath+'oxpback.jpg';
	var Add_StatusLImgOff = new Image;
	var Add_StatusLImgOn = new Image;
	Add_StatusLImgOff.src = rimPath+'folder_closed.gif';
	Add_StatusLImgOn.src = rimPath+'folder_open.gif';
	var Add_StatusBImgOff = new Image;
	var Add_StatusBImgOn = new Image;
	Add_StatusBImgOff.src = rimPath+'oxpback.jpg';
	Add_StatusBImgOn.src = rimPath+'oxpback.jpg';
	var Add_DeskLImgOff = new Image;
	var Add_DeskLImgOn = new Image;
	Add_DeskLImgOff.src = rimPath+'folder_closed.gif';
	Add_DeskLImgOn.src = rimPath+'folder_open.gif';
	var Add_DeskBImgOff = new Image;
	var Add_DeskBImgOn = new Image;
	Add_DeskBImgOff.src = rimPath+'oxpback.jpg';
	Add_DeskBImgOn.src = rimPath+'oxpback.jpg';
	var Add_FeetypeLImgOff = new Image;
	var Add_FeetypeLImgOn = new Image;
	Add_FeetypeLImgOff.src = rimPath+'folder_closed.gif';
	Add_FeetypeLImgOn.src = rimPath+'folder_open.gif';
	var Add_FeetypeBImgOff = new Image;
	var Add_FeetypeBImgOn = new Image;
	Add_FeetypeBImgOff.src = rimPath+'oxpback.jpg';
	Add_FeetypeBImgOn.src = rimPath+'oxpback.jpg';
	var Manage_UsersLImgOff = new Image;
	var Manage_UsersLImgOn = new Image;
	Manage_UsersLImgOff.src = rimPath+'folder_closed.gif';
	Manage_UsersLImgOn.src = rimPath+'folder_open.gif';
	var Manage_UsersBImgOff = new Image;
	var Manage_UsersBImgOn = new Image;
	Manage_UsersBImgOff.src = rimPath+'oxpback.jpg';
	Manage_UsersBImgOn.src = rimPath+'oxpback.jpg';
	var Manage_DocsLImgOff = new Image;
	var Manage_DocsLImgOn = new Image;
	Manage_DocsLImgOff.src = rimPath+'folder_closed.gif';
	Manage_DocsLImgOn.src = rimPath+'folder_open.gif';
	var Manage_DocsBImgOff = new Image;
	var Manage_DocsBImgOn = new Image;
	Manage_DocsBImgOff.src = rimPath+'oxpback.jpg';
	Manage_DocsBImgOn.src = rimPath+'oxpback.jpg';
	var Assign_DeskLImgOff = new Image;
	var Assign_DeskLImgOn = new Image;
	Assign_DeskLImgOff.src = rimPath+'folder_closed.gif';
	Assign_DeskLImgOn.src = rimPath+'folder_open.gif';
	var Assign_DeskBImgOff = new Image;
	var Assign_DeskBImgOn = new Image;
	Assign_DeskBImgOff.src = rimPath+'oxpback.jpg';
	Assign_DeskBImgOn.src = rimPath+'oxpback.jpg';

	
var tbUseToolbar = false;
var lmcHS = null;


	function GetCurCmd(e) {
		//IE,SM,OP
		//This function will return the current command under the mouse pointer.
		//It will return null if the mouse is not over any command.
		//------------------------------
		//Version 1.5
		//
		if(SM)
			var cc = e;
		else {
			var cc = mFrame.window.event;
			if(!cc)
				cc = cFrame.window.event;
			cc = cc.srcElement;
		}
		while(cc.id=="") {
			cc = cc.parentElement;
			if(cc==null)
				break;
		}
		return cc;
	}

	function HoverSel(mode, imgLName, imgRName, e) {
		//IE,SM,OP
		//This is the function called every time the mouse pointer is moved over a command.
		//------------------------------
		//mode: 0 if the mouse is moving over the command and 1 if is moving away.
		//imgLName: Name of the left image object, if any.
		//imgRName: Name of the right image object, if any.
		//------------------------------
		//Version 15.0
		//
		var nStyle;
		var mc;
		
		if(mode==0 && OpenMenus[nOM].SelCommand!=null)
			HoverSel(1);
		
		if(mode==0) {
			mc = GetCurCmd(e);
			if(nOM>1) {
				if(mc==OpenMenus[nOM-1].SelCommand)
					return false;
				while(((BV>=5)?mc.parentNode.parentNode.id!=OpenMenus[nOM].mName:mc.parentElement.parentElement.id!=OpenMenus[nOM].mName))
						Hide();
			}
			if(imgLName) imgLRsc = eval(imgLName+"On");
			if(imgRName) imgRRsc = eval(imgRName+"On");
			if(OP)
				mc.opw = OpenMenus[nOM].width - 2*mc.style.left;
			else {
				mc.opw = mc.style.width;
				mc.b = mc.style.borderLeft;
				mc.hasBorder = mc.b.split(" ").length>1;
			}
		
			if(!mc.nStyle) {
				if(OP)
					mc.nStyle = GetOPStyle(mc);
				else
					mc.nStyle = SM?mc.getAttribute("style"):mc.style.cssText;
				mc.hStyle = GetCStyle(mc.style) + ((SM||KQ)?xrep(hStyle[mc.id],"hand","pointer"):hStyle[mc.id]);
			}
		
			OpenMenus[nOM].SelCommand = mc;
			OpenMenus[nOM].SelCommandPar = [imgLName,imgRName,mc.nStyle];
			
			if(SM||KQ) {
				IsOverHS = false;
				if(OP)
					SetOPStyle(mc, mc.hStyle);
				else
					mc.setAttribute("style", mc.hStyle);
			} else
				mc.style.cssText = mc.hStyle;
		} else {
			mc = (mode==1)?OpenMenus[nOM].SelCommand:OpenMenus[nOM].Opener;
			imgLName = (mode==1)?OpenMenus[nOM].SelCommandPar[0]:OpenMenus[nOM].OpenerPar[0];
			imgRName = (mode==1)?OpenMenus[nOM].SelCommandPar[1]:OpenMenus[nOM].OpenerPar[1];
			nStyle = (mode==1)?OpenMenus[nOM].SelCommandPar[2]:OpenMenus[nOM].OpenerPar[2];
			mc.style.background = "";
			if(IsMac) mc.style.border = "0px none";
			if(SM||KQ) {
				if(OP)
					SetOPStyle(mc, nStyle);
				else
					mc.setAttribute("style", nStyle);
			} else
				mc.style.cssText = ((BV<5)?GetCStyle(mc.style):"") + nStyle;
			if(imgLName) imgLRsc = eval(imgLName+"Off");
			if(imgRName) imgRRsc = eval(imgRName+"Off");
			OpenMenus[nOM].SelCommand = null;
		}
		
		if(imgLName) mFrame.document.images[imgLName].src = _fip(imgLRsc);
		if(imgRName) mFrame.document.images[imgRName].src = _fip(imgRRsc);
		
		if(!OP) FixHover(mc, mode);
		
		return true;
	}

	function FixHover(mc, mode) {
		//IE,SM
		//This function fixes the position of the commands' contents when using special highlighting effects.
		//------------------------------
		//Version 2.0
		//
		var hasBorder;
		var bw;
		if(mode==0) {
			if(BV>=5)
				s = mc.getElementsByTagName("SPAN")[0];
			else
				s = mc.document.all.tags("SPAN")[0];
			mc.s = s;
			mc.stop = s.style.top;
			mc.sleft = s.style.left;
			
			hasBorder = mc.style.borderLeft.split(" ").length>1;
			if(hasBorder != mc.hasBorder) {
				bw = (hasBorder?-GetBorderWidth(mc.style.borderLeft):GetBorderWidth(mc.b));
				s.style.left = parseInt(s.style.left) + bw + "px";
				s.style.top = parseInt(s.style.top) + bw + "px";
				if(SM)
					with(mc.style) {
						mc.cwidth = width;
						mc.cheight = height;
						width = parseInt(width) + 2*bw + "px";
						height = parseInt(height) + 2*bw + "px";
					}
			}			
		} else {
			mc.s.style.top = mc.stop;
			mc.s.style.left = mc.sleft;
			if(SM) {
				mc.style.width = mc.cwidth;
				mc.style.height = mc.cheight;
			}
		}
	}

	function NSHoverSel(mode, mc) {
		//NS
		//This is the function called every time the mouse pointer is moved over or away from a command.
		//------------------------------
		//mode: 0 if the mouse is moving over the command and 1 if is moving away.
		//mc: Name of the layer that corresponds to the selected command.
		//n: Unique ID that identifies this command. Used to retrieve the data from the nLayer or hLayer array.
		//bcolor: Background color of the command. Ignored if the group uses a background image.
		//w: Width of the command's layer.
		//h: Height of the command's layer.
		//------------------------------
		//Version 11.2
		//
		var mcN;
		
		ClearTimer(parseInt(HTHnd[nOM]));HTHnd[nOM] = 0;
		if(!nOM) return false;
		
		if(mode==0 && OpenMenus[nOM].SelCommand!=null)
			NSHoverSel(1);
		
		if(mode==0) {
			mcN = mc.parentLayer.layers[mc.name.substr(0, mc.name.indexOf("EH")) + "N"];
			mcN.mcO = mc.parentLayer.layers[mc.name.substr(0, mc.name.indexOf("EH")) + "O"];
			if(nOM>1) if(mc==OpenMenus[nOM-1].SelCommand) return false;
			while(!InMenu()&&nOM>1) Hide();
			OpenMenus[nOM].SelCommand = mcN;
			mcN.mcO.visibility = "show";
			mcN.visibility = "hide";
		} else {
			mcN = (mode==1)?OpenMenus[nOM].SelCommand:OpenMenus[nOM].Opener;
			mcN.visibility = "show";
			mcN.mcO.visibility = "hide";						
			OpenMenus[nOM].SelCommand = null;
		}
		return true;
	}

	function Hide() {
		//IE,NS,SM,OP
		//This function hides the last opened group and it keeps hiding all the groups until
		//no more groups are opened or the mouse is over one of them.
		//Also takes care of reseting any highlighted commands.
		//------------------------------
		//Version 4.1
		//
		ClearTimer(HTHnd[nOM]);HTHnd[nOM] = 0;
		ClearTimer(AnimHnd);AnimHnd = 0;
		ClearTimer(tmrHideHnd);
		
		if(nOM) {
			if(OpenMenus[nOM].SelCommand!=null) {
				if(IE) HoverSel(1);
				if(NS) NSHoverSel(1);
			}
			if(OpenMenus[nOM].Opener!=null) {
				if(IE) HoverSel(3);
				if(NS) NSHoverSel(3);
			}
			if(CanDoShadow) {
				var m = mFrame.document.getElementById(OpenMenus[nOM].mName);
				var ms = m.style;
				for(var i=1; i<m.ds.length; i++)
					m.ds[i].visibility = "hidden";
			}
			if(nOM==1)if(OpenMenus[nOM].irp)DMB_changeImages(OpenMenus[nOM].irp[0], OpenMenus[nOM].irp[1]);

			OpenMenus[nOM].visibility = "hidden";
			nOM--;

		}
		
		if(nOM==0) {
			if(tbUseToolbar && lmcHS) {
				if(IE) hsHoverSel(1);
				if(NS) hsNSHoverSel(1);
			}
			FormsTweak("visible");
			status = "";
		} else
			if(!InMenu()) HTHnd[nOM] = window.setTimeout("Hide()", TimerHideDelay/20);
	}

	function ShowMenu(mName, x, y, isCascading, hsImgName, algn) {
		//IE,NS,SM,OP
		//This is the main function to show the menus when a hotspot is triggered or a cascading command is activated.
		//------------------------------
		//mName: Name of the <div> or <layer> to be shown.
		//x: Left position of the menu.
		//y: Top position of the menu.
		//isCascading: True if the menu has been triggered from a command, and not from a hotspot.
		//------------------------------
		//Version 15.0
		//
		ClearTimer(smHnd);smHnd = 0;
		if(isCascading) {
			lsc = OpenMenus[nOM].SelCommand;
			smHnd = window.setTimeout("if(nOM)if(lsc==OpenMenus[nOM].SelCommand)ShowMenu2('" + mName + "',0,0,true,''," + algn + ")", SubMenusDelay);
		} else
			ShowMenu2(mName, x, y, false, hsImgName, algn);
	}

	function ShowMenu2(mName, x, y, isCascading, hsImgName, algn) {
		//IE,NS,SM,OP
		//This is the main function to show the menus when a hotspot is triggered or a cascading command is activated.
		//------------------------------
		//mName: Name of the <div> or <layer> to be shown.
		//x: Left position of the menu.
		//y: Top position of the menu.
		//isCascading: True if the menu has been triggered from a command.
		//hsImgName: Image to which the menu is attached to.
		//algn: Alignment setting for the menu.
		//------------------------------
		//Version 20.0
		//
		var xy;
		ClearTimer(parseInt(HTHnd[nOM]));HTHnd[nOM] = 0;
		x = parseInt(x);y = parseInt(y);
		
		if(IE)
			if(BV>=5)
				var Menu = mFrame.document.getElementById(mName);
			else
				var Menu = mFrame.document.all[mName];
		if(NS)
			var Menu = mFrame.document.layers[mName];
		if(!Menu)
			return false;		
		if(IE) {
			Menu = Menu.style;
			if(BV>=5)
				Menu.frmt = mFrame.document.getElementById(mName+"frmt").style;
			else
				Menu.frmt = mFrame.document.all[mName+"frmt"].style;
		}
		
		if(nOM>0)
			if(OpenMenus[1].mName == mName && !isCascading) {
				IsOverHS = true;
				return false;
			}
		if(Menu==OpenMenus[nOM]) return false;
			
		if(AnimHnd && nOM>0) {
			AnimStep=100;
			Animate();
		}
			
		if(!isCascading) {
			var oldlmcHS = lmcHS;
			lmcHS = null;
			HideAll();
			lmcHS = oldlmcHS;
		}
		
		Menu.mName = mName;
		Menu.Opener = nOM>0?OpenMenus[nOM].SelCommand:null;
		Menu.OpenerPar = nOM>0?OpenMenus[nOM].SelCommandPar:null;
		Menu.SelCommand = null;
		if(OP) {
			Menu.width = Menu.pixelWidth;
			Menu.height = Menu.pixelHeight;
		}
		
		if(!isCascading) {
			if(hsImgName) {
				var imgObj = NS?FindImage(cFrame.document, hsImgName.split("|")[0]):cFrame.document.images[hsImgName.split("|")[0]];
				if(imgObj) {
					var tbMode = hsImgName.split("|")[1];
					if(tbMode&2) x = AutoPos(Menu, imgObj, algn)[0] + (IsFrames?GetLeftTop()[0]:0);
					if(tbMode&1) y = AutoPos(Menu, imgObj, algn)[1] + (IsFrames?GetLeftTop()[1]:0);
					if(IsMac&&IE&&!SM&&(BV>=5)) {
						x += parseInt(mFrame.document.body.leftMargin);
						y += parseInt(mFrame.document.body.topMargin);
					}
				}
			}

		}
		
		var pW = GetWidthHeight()[0] + GetLeftTop()[0];
		var pH = GetWidthHeight()[1] + GetLeftTop()[1];
		
		if(IE) {
			if(SM) Menu.display = "none";
			if(isCascading) {
				xy = GetSubMenuPos(Menu, algn);
				x = xy[0];y = xy[1];
				x += 13;
				y += 95;

			}
			Menu.left = ((x+parseInt(Menu.width)>pW)?(isCascading?parseInt(OpenMenus[nOM].left):pW) - parseInt(Menu.width):x) + "px";
			Menu.top =  ((y+parseInt(Menu.height)>pH)?pH - parseInt(Menu.height):y) + (OP?"":"px");
			if(IsWin&&!SM)
				Menu.clip = "rect(0 0 0 0)";
		}
		if(NS) {
			if(isCascading) {
				xy = GetSubMenuPos(Menu, algn);
				x = xy[0];y = xy[1];
				x += 13;
				y += 95;

			}
			Menu.clip.width = 0;
			Menu.clip.height = 0;

			Menu.moveToAbsolute((x+Menu.w>pW)?(isCascading?OpenMenus[nOM].left:pW) - Menu.w:x,(y+Menu.h>pH)?pH - Menu.h:y);
		}
		if(isCascading)
			Menu.zIndex = parseInt(OpenMenus[nOM].zIndex) + 1;
		OpenMenus[++nOM] = Menu;
		if(SM&&!OP) FixCommands(mName);
		if(SM) Menu.display = "inline";
		Menu.visibility = "visible";
		HTHnd[nOM] = 0;
		if((IE&&IsWin&&!SM)||(NS&&Menu.clip.width==0))
			AnimHnd = window.setTimeout("Animate()", 10);
		FormsTweak("hidden");
		
		if(!isCascading&&!IsContext)
			IsOverHS = true;
		IsContext = false;
		ClearTimer(tmrHideHnd);
		tmrHideHnd = window.setTimeout("AutoHide()", TimerHideDelay);
		
		if(CanDoShadow) CoolShadow();
		
		return true;
	}

	function GetSubMenuPos(mg, a) {
		//IE,NS,SM,OP
		//This function calculates the position of a submenu based on its alignment.
		//------------------------------
		//Version 1.0
		//
		var x;
		var y;
		var pg = OpenMenus[nOM];
		var sc = pg.SelCommand;
		
		if(NS) {
			pg.width = pg.w;
			pg.height = pg.h;
			mg.width = mg.w;
			mg.height = mg.h;
			sc.width = sc.clip.width;
			sc.height = sc.clip.height;
		} else
			sc = sc.style;
		
		var lp = parseInt(pg.left) + parseInt(sc.left);
		var tp = parseInt(pg.top) + parseInt(sc.top);
		
		switch(a) {
			case 0:
				x = lp;
				y = tp + parseInt(sc.height);
				break;
			case 1:
				x = lp + parseInt(sc.width) - parseInt(mg.width);
				y = tp + parseInt(sc.height);
				break;
			case 2:
				x = lp;
				y = tp - parseInt(mg.height);
				break;
			case 3:
				x = lp + parseInt(sc.width) - parseInt(mg.width);
				y = tp - parseInt(mg.height);
				break;
			case 4:
				x = lp - parseInt(mg.width);
				y = tp;
				break;
			case 5:
				x = lp - parseInt(mg.width);
				y = tp + parseInt(sc.height) - parseInt(mg.height);
				break;
			case 6:
				x = lp + parseInt(sc.width);
				y = tp;
				break;
			case 7:
				x = lp + parseInt(sc.width);
				y = tp + parseInt(sc.height) - parseInt(mg.height);
				break;
		}
		return [x,y];		
	}

	function FixCommands(mName) {
		//SM
		//This function is used to fix the way the Gecko engine calculates
		//the borders and the way they affect the size of divs
		//------------------------------
		//Version 1.6
		//
		var m = mFrame.document.getElementById(mName);
		if(!m.Fixed) {
			var sd = m.getElementsByTagName("DIV");
			with(sd[0].style) {
				var b = GetBorderWidth(borderLeft);
				width = parseInt(width) - 2*b + "px";
				height = parseInt(height) - 2*b + "px";
			}
			for(i=1;i<(sd.length);i++)
				with(sd[i].style) {
					if(borderLeft.indexOf("none")==-1) {
						width = parseInt(width) - 2*GetBorderWidth(borderLeft) + "px";
						height = parseInt(height) - 2*GetBorderWidth(borderTop) + "px";
					}
				}
				
		}
		m.Fixed = true;
	}

	function Animate() {
		//IE,NS
		//This function is called by ShowMenu every time a new group must be displayed and produces the predefined unfolding effect.
		//Currently is disabled for Navigator, because of some weird bugs we found with the clip property of the layers.
		//------------------------------
		//Version 1.9
		//
		var r = '';
		var nw = nh = 0;
		if(AnimStep+AnimSpeed>100) AnimStep = 100;
		switch(fx) {
			case 1:
				if(IE) r = "0 " + AnimStep + "% " + AnimStep + "% 0";
				if(NS) nw = AnimStep; nh = AnimStep;
				break;
			case 2:
				if(IE) r = "0 100% " + AnimStep + "% 0";
				if(NS) nw = 100; nh = AnimStep;
				break;
			case 3:
				if(IE) r = "0 " + AnimStep + "% 100% 0";
				if(NS) nw = AnimStep; nh = 100;
				break;
			case 0:
				if(IE) r = "0 100% 100% 0";
				if(NS) nw = 100; nh = 100;
				break;
		}
		if(OpenMenus[nOM]) {
			with(OpenMenus[nOM]) {
				if(IE)
					clip =  "rect(" + r + ")";
				if(NS) {
					clip.width = w*(nw/100);
					clip.height = h*(nh/100);
				}
			}
			AnimStep += AnimSpeed;
			if(AnimStep<=100)
				AnimHnd = window.setTimeout("Animate()",25);
			else {
				ClearTimer(AnimHnd);
				AnimStep = 0;
				AnimHnd = 0;
			}
		}
	}

	function InTBHotSpot() {
		//IE,NS,SM,OP
		//This function returns true if the mouse pointer is over a toolbar item.
		//------------------------------
		//Version 1.0
		//
		if(!tbUseToolbar) return false;
		var m = lmcHS;
		if(!m) return false;
		var l = parseInt(m.left);
		var r = l+(IE?parseInt(m.width):m.clip.width);
		var t = parseInt(m.top);
		var b = t+(IE?parseInt(m.height):m.clip.height);
		return ((mX>=l && mX<=r) && (mY>=t && mY<=b)) || IsOverHS || (nOM>0);
	}

	function InMenu() {
		//IE,NS,SM,OP
		//This function returns true if the mouse pointer is over the last opened menu.
		//------------------------------
		//Version 1.8
		//
		var m = OpenMenus[nOM];
		if(!m) return false;
		var l = parseInt(m.left);
		var r = l+(IE?parseInt(m.width):m.clip.width);
		var t = parseInt(m.top);
		var b = t+(IE?parseInt(m.height):m.clip.height);
		return ((mX>=l && mX<=r) && (mY>=t && mY<=b)) || IsOverHS;
	}

	function SetPointerPos(e) {
		//IE,NS,SM,OP
		//This function sets the mX and mY variables with the current position of the mouse pointer.
		//------------------------------
		//e: Only used under Navigator, corresponds to the Event object.
		//------------------------------
		//Version 1.6
		//
		if(IE) {
			if(!SM) {
				if(mFrame!=cFrame||event==null)
					if(mFrame.window.event==null)
						return;
					else
						e = mFrame.window.event;
				else
					e = event;
			}
			mX = e.clientX + GetLeftTop()[0];
			mY = e.clientY + GetLeftTop()[1];
		}
		if(NS) {
			mX = e.pageX;
			mY = e.pageY;
		}
	}

	function HideMenus(e) {
		//IE,NS,SM,OP
		//This function checks if the mouse pointer is on a valid position and if the current menu should be kept visible.
		//The function is called every time the mouse pointer is moved over the document area.
		//------------------------------
		//e: Only used under Navigator, corresponds to the Event object.
		//------------------------------
		//Version 25.1
		//
		if(nOM>0) {
			SetPointerPos(e);
			if(OpenMenus[nOM].SelCommand!=null)
				if(!InMenu()&&!HTHnd[nOM])
					HTHnd[nOM] = window.setTimeout("if(nOM>0)if(!InMenu())Hide()", TimerHideDelay);
		}
	}

	function FormsTweak(state) {
		//IE,SM,OP
		//This is an undocumented function, which can be used to hide every listbox (or combo) element on a page.
		//This can be useful if the menus will be displayed over an area where is a combo box, which is an element that cannot be placed behind the menus and it will always appear over the menus resulting in a very undesirable effect.
		//------------------------------
		//Version 2.0
		//
		var fe;
		if(DoFormsTweak && IE)
			for(var f = 0; f <= (mFrame.document.forms.length - 1); f++)
				for(var e = 0; e <= (mFrame.document.forms[f].elements.length - 1); e++) {
					fe = mFrame.document.forms[f].elements[e];
					if(fe.type) if(fe.type.indexOf("select")==0) fe.style.visibility = state;
				}
	}

	function execURL(url, tframe) {
		//IE,NS,SM,OP
		//This function is called every time a command is triggered to jump to another page or execute some javascript code.
		//------------------------------
		//url: Encrypted URL that must be opened or executed.
		//tframe: If the url is a document location, tframe is the target frame where this document will be opened.
		//------------------------------
		//Version 1.1
		//
		HideAll();
		window.setTimeout("execURL2('" + escape(_purl(url)) + "', '" + tframe + "')", 100);
	}

	function execURL2(url, tframe) {
		//IE,NS,SM,OP
		//This function is called every time a command is triggered to jump to another page or execute some javascript code.
		//------------------------------
		//url: Encrypted URL that must be opened or executed.
		//tframe: If the url is a document location, tframe is the target frame where this document will be opened.
		//------------------------------
		//Version 1.1
		//
		var fObj = (tframe=="_blank"?window.open(""):(tframe=="_parent"?mFrame.parent:eval(rStr(tframe))));
		url = rStr(unescape(url));
		url.indexOf("javascript:")!=url.indexOf("vbscript:")?eval(url):fObj.location.href = url;
	}

	function rStr(s) {
		//IE,NS,SM,OP
		//This function is used to decrypt the URL parameter from the triggered command.
		//------------------------------
		//Version 1.1
		//
		s = xrep(s,"%1E", "'");
		s = xrep(s,"\x1E", "'");
		if(OP&&s.indexOf("frames[")!=-1) {
			s = xrep(s,String.fromCharCode(s.charCodeAt(7)), "'");
		}
		return xrep(s,"\x1D", "\x22");
	}

	function hNSCClick(e) {
		//NS
		//This function executes the selected command's trigger code.
		//------------------------------
		//Version 1.0
		//
		eval(this.TCode);
	}

	function HideAll() {
		//IE,NS,SM,OP
		//This function will hide all the currently opened menus.
		//------------------------------
		//Version 1.1
		//
		if(nOM)
			while(nOM>0) Hide();
		else Hide();
	}

	function GetLeftTop(f) {
		//IE,NS,SM,OP
		//This function returns the scroll bars position on the menus frame.
		//------------------------------
		//Version 2.2
		//
		if(!f) f = mFrame;
		if(IE)
			if(SM)
				return [OP?0:f.scrollX,OP?0:f.scrollY];
			else
				if(f.document.body)
					return [f.document.body.scrollLeft,f.document.body.scrollTop];
				else
					return [0, 0];
		if(NS)
			return [f.pageXOffset,f.pageYOffset];
	}

	function tHideAll() {
		//IE,NS,SM,OP
		//This function is called when the mouse is moved away from a hotspot to close any opened menu.
		//------------------------------
		//Version 1.2
		//
		IsOverHS = false;
		HTHnd[nOM] = window.setTimeout("if(!InMenu()&&!InTBHotSpot())HideAll(); else HTHnd[nOM]=0;", TimerHideDelay);
	}

	function GetWidthHeight(f) {
		//IE,NS,SM,OP
		//This function returns the width and height of the menus frame.
		//------------------------------
		//Version 2.1
		//
		if(!f) f = mFrame;
		if(IE&&!SM)
			return [f.document.body.clientWidth,f.document.body.clientHeight];
		if(NS||SM)
			return [f.innerWidth,f.innerHeight];
	}

	function GetBorderWidth(b) {
		//IE,SM,SM,OP
		//This functions returns the width of a border
		//------------------------------
		//Version 1.1
		//
		if(OP) return 0;
		var w;
		var l = b.split(" ");
		for(var i=0; i<l.length; i++) {
			w = parseInt(l[i]);
			if(w>0)
				return w;
		}
		return 0;
	}

	function GetCStyle(cmc) {
		//IE,SM,OP
		//This function completes the style of command with all the common
		//parameters from the original style code.
		//------------------------------
		//Version 1.0
		//
		return "position: absolute; left:" + cmc.left + 
			   "; top: " + cmc.top + 
			   "; width: " + (OP?cmc.pixelWidth:cmc.width) + 
			   "; height: " + (OP?cmc.pixelHeight:cmc.height) + "; ";
	}

	function AutoPos(Menu, imgObj, arAlignment) {
		//IE,NS,SM,OP
		//This function finds the image-based hotspot and returns the position at which 
		//the menu should be displayed based on the alignment setting.
		//------------------------------
		//Version 1.1
		//
		var x = GetImgXY(imgObj)[0];
		var y = GetImgXY(imgObj)[1];
		var mW = parseInt(NS?Menu.w:Menu.width);
		var mH = parseInt(NS?Menu.h:Menu.height);
			
		switch(arAlignment) {
			case 0:
				y += GetImgWH(imgObj)[1];
				break;
			case 1:
				x += GetImgWH(imgObj)[0] - mW;
				y += GetImgWH(imgObj)[1];
				break;
			case 2:
				y -= mH;
				break;
			case 3:
				x += GetImgWH(imgObj)[0] - mW;
				y -= mH;
				break;
			case 4:
				x -= mW;
				break;
			case 5:
				x -= mW;
				y -= mH - GetImgWH(imgObj)[1];
				break;
			case 6:
				x += GetImgWH(imgObj)[0];
				break;
			case 7:
				x += GetImgWH(imgObj)[0];
				y -= mH - GetImgWH(imgObj)[1];
				break;
		}
		
		return [x, y];
	}

	function GetImgXY(imgObj) {
		//IE,NS,SM,OP
		//This function returns the x,y coordinates of an image.
		//------------------------------
		//Version 1.2
		//
		var x;
		var y;
			
		if(IE)	{
			x = getOffset(imgObj)[0];
			y = getOffset(imgObj)[1];
		} else	{
			y = GetImgOffset(cFrame, imgObj.name, 0, 0);
			x = imgObj.x + y[0];
			y = imgObj.y + y[1];
		}
			
		return [x, y];
		
	}

	function GetImgWH(imgObj) {
		//IE,NS,SM,OP
		//This function returns the width and height of an image.
		//------------------------------
		//Version 1.1
		//
		return [parseInt(imgObj.width), parseInt(imgObj.height)];
	}

	function getOffset(imgObj) {
		//IE,NS,SM,OP
		//This function returns the horizontal and vertical offset of an object.
		//------------------------------
		//Version 1.0
		//
		x = imgObj.offsetLeft;
		y = imgObj.offsetTop;
		ce =imgObj.offsetParent;
		while (ce!=null)	{
			y += ce.offsetTop;
			x += ce.offsetLeft;
			ce = ce.offsetParent;
		}
		return [x,y];
		
	}

	function FindImage(d, img) {
		//NS
		//This function finds an image regardless of its location in the document structure.
		//------------------------------
		//Version 1.0
		//
		var i;
		var tmp;
		
		if(d.images[img]) return d.images[img];
		
		for(i=0; i<d.layers.length; i++) {
			tmp = FindImage(d.layers[i].document, img);
			if(tmp) return tmp;
		}
		
		return null;
	}

	function GetImgOffset(d, img, ox, oy) {
		//NS
		//This function finds the offset to an image regardless of its location in the document structure.
		//------------------------------
		//Version 1.0
		//
		var i;
		var tmp;
		
		if(d.left) {
			ox += d.left;
			oy += d.top;
		}
				
		if(d.document.images[img]) return [ox, oy];
		
		for(i=0; i<d.document.layers.length; i++) {
			tmp = GetImgOffset(d.document.layers[i], img, ox, oy);
			if(tmp) return [tmp[0], tmp[1]];
		}
		
		return null;
	}

	function AutoHide() {
		//IE,NS,SM,OP
		//This function hides the menus, even when a submenu is open and no
		//command has been selected.
		//------------------------------
		//Version 1.1
		//
		var original_nOM = nOM;
		var OktoClose = true;
		for(;nOM>0;nOM--)
			if(InMenu()) {
				OktoClose = false;
				break;
			}
		nOM = original_nOM;
		if(OktoClose&&!IsOverHS)
			Hide();
		
		if(nOM) if(!InMenu()) tmrHideHnd = window.setTimeout("AutoHide()", TimerHideDelay);
	}

	function ShowContextMenu(e) {
		//IE,NS
		//This function is called when a user rightclicks on the document and it will show a predefined menu.
		//------------------------------
		//Version 1.2
		//
		if(cntxMenu!='') {
			if(IE) {
				SetPointerPos(e);
				IsContext = true;
				cFrame.ShowMenu(cntxMenu, mX-1, mY-1, false);
				return false;
			}
			
			if(NS)
				if(e.which==3) {
					IsContext = true;
					cFrame.ShowMenu(cntxMenu, e.x-1, e.y-1, false);
					return false;
				}
		}		
		return true;
	}

	function SetUpEvents() {
		//IE,NS,SM,OP
		//This function initializes the frame variables and setups the event handling.
		//------------------------------
		//Version 2.5
		//
		if(!SM) onerror = errHandler;
		if(!mFrame) mFrame = cFrame;
		if(typeof(mFrame)=="undefined")
			window.setTimeout("SetUpEvents()",10);
		else {
			if(NS) {
				mFrame.captureEvents(Event.MOUSEMOVE);
				mFrame.onmousemove = HideMenus;
				if(cntxMenu!="") {
					mFrame.window.captureEvents(Event.MOUSEDOWN);
					mFrame.window.onmousedown = ShowContextMenu;
				}
				nsOW = GetWidthHeight()[0];
				nsOH = GetWidthHeight()[1];
				window.onresize = rHnd;
				PrepareEvents();
			}
			if(IE) {
				document.onmousemove = HideMenus;
				mFrame.document.onmousemove = document.onmousemove;
				mFrame.document.oncontextmenu = ShowContextMenu;
				if(SM&&!OP) {
					var i = 0;
					var m;
					while(mFrame.document.getElementById(i)) {
						m = mFrame.document.getElementById(i++);
						m.style.width = parseInt(m.style.width) - 2*parseInt(m.style.paddingLeft) + "px";
						m.style.height = parseInt(m.style.height) - 2*parseInt(m.style.paddingTop) + "px";
					}
				}
			}
			IsFrames = (cFrame!=mFrame);
			MenusReady = true;
			if(IE) FixImages();
		}
		

		changeImages = new Function("obj", "img", "{if(nOM>0){if(OpenMenus[nOM].irp!=true){DMB_changeImages(obj, img);OpenMenus[nOM].irp=true;}else OpenMenus[nOM].irp=[obj,img];}else DMB_changeImages(obj, img);}")

	}

	function errHandler(sMsg,sUrl,sLine) {
		//IE,NS
		//This function will trap any errors generated by the scripts and filter the unhandled ones.
		//------------------------------
		//Version 1.1
		//
		if(sMsg.substr(0,16)!="Access is denied"&&sMsg!="Permission denied")
			alert("Java Script Error\n" +
			      "\nDescription: " + sMsg +
			      "\nSource: " + sUrl +
			      "\nLine: "+sLine);
		return true;
	}

	function FixPointSize(s) {
		//NS
		//This function increases the point-size value for Navigator 4.
		//------------------------------
		//Version 2.0
		//
		if(IsWin||!NS) return s;
		for(var i=54; i>1; i--)
			if(s.indexOf("point-size=" + i)!=-1)
				s = xrep(s, "point-size=" + i, "point-size=" + (i+3));
		return s;
	}

	function ClearTimer(t) {
		//NS,IE
		//This function is used to overcome a bug in very
		//old versions of Navigator 4
		//------------------------------
		//Version 1.0
		//
		if(t) window.clearTimeout(t);
	}

	function xrep(s, f, n) {
		//IE,NS,SM,OP
		//This function looks for any occurrence of the f string and replaces it with the n string.
		//------------------------------
		//Version 1.0
		//
		if((s.length>100)&&SM)
			while(s.indexOf(f)!=-1)
				s = s.substr(0, s.indexOf(f)) + n + s.substr(s.indexOf(f) + f.length);
		else
			s = s.split(f).join(n);
		return s;
	}

	function rHnd() {
		//NS
		//This function is used to reload the page when Navigator window is resized.
		//Original Code from DHTML Lab
		//------------------------------
		//Version 1.0
		//
		if((GetWidthHeight()[0]!=nsOW) || (GetWidthHeight()[1]!=nsOH))
			frames["top"].location.reload();
	}

	function PrepareEvents() {
		//NS
		//This function is called right after the menus are rendered.
		//It has been designed to attach the event handlers to the <layer> tag and
		//fix the font size problems with Navigator under the Mac and Linux.
		//------------------------------
		//Version 4.0
		//
		for(var l=0; l<mFrame.document.layers.length; l++) {
			var lo = mFrame.document.layers[l];
			if(lo.layers.length) {
				lo.w = lo.clip.width;
				lo.h = lo.clip.height;
				for(var sx=0; sx<lo.layers.length; sx++)
					for(var sl=0; sl<lo.layers[sx].layers.length; sl++) {
						var slo = mFrame.document.layers[l].layers[sx].layers[sl];
						if(slo.name.indexOf("EH")>0) {
							slo.document.onmouseup = hNSCClick;
							slo.document.TCode = nTCode[slo.name.split("EH")[1]];
						}
					}

			}
		}
	}

	function CoolShadow() {
		//
		//NOTE: This code is based on Microsoft's dropsahdow effect code found at their web site.
		//
		var i;
		var m = mFrame.document.getElementById(OpenMenus[nOM].mName);
		var ms = m.style;
		if(m.ds) {
			for(var i=1; i<m.ds.length; i++) {
				m.ds[i].left = (ms.pixelLeft + i) + 'px';
				m.ds[i].top = (ms.pixelTop + i) + 'px';
				m.ds[i].visibility = "visible";
			}
		} else {
			m.ds = new Array();
			for (i=5; i>0; i--) {
				var d = document.createElement('div');
				var ds = d.style;
				ds.position = 'absolute';
				ds.left = (ms.pixelLeft + i) + 'px';
				ds.top = (ms.pixelTop + i) + 'px';
				ds.width = ms.pixelWidth + 'px';
				ds.height = ms.pixelHeight + 'px';
				ds.zIndex = ms.zIndex - i;
				ds.backgroundColor = "#CFCFCF";
				var opacity = 1 - i / (i + 1);
				ds.filter = 'alpha(opacity=' + (100 * opacity) + ')';
				m.insertAdjacentElement('afterEnd', d);
				m.ds[i] = ds;
			}
		}
	}

	if(IE)
		with(document) {
			open();
			write(xrep("<div id=\"grpDentry\" style=\"position: absolute; top:0px; left:0px;; width:130px; height:295px; filter: dropShadow(offX=5,offY=5,color=#B3B3B3);; z-index: 1000; visibility: hidden\"><div id=\"grpDentryfrmt\" style=\"position: absolute; top:0px; left:0px; width: 125px; height: 290px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute;; top: 1px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=0 OnMouseOver=\"cFrame.HoverSel(0,\'Add_MatterLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'DataEntry.asp?qry=addclaim&screen=dentry&alphabet=A&ins=A&clientis=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_MatterLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Matter</div></span></div><div nowrap style=\"position: absolute;; top: 25px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=1 OnMouseOver=\"cFrame.HoverSel(0,\'Edit_MatterLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'editMatter.asp?qry=em&screen=dentry&alphabet=A&ins=A&clientis=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Edit_MatterLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Edit Matter</div></span></div><div nowrap style=\"position: absolute;; top: 49px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=2 OnMouseOver=\"cFrame.HoverSel(0,\'Add_ClientLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'clientEntry.asp?qry=ac&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_ClientLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Client</div></span></div><div nowrap style=\"position: absolute;; top: 73px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=3 OnMouseOver=\"cFrame.HoverSel(0,\'View_Edit_ClientLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'clientEdit.asp?qry=ec&screen=dentry&alphabet=All\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=View_Edit_ClientLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>View/Edit Client</div></span></div><div nowrap style=\"position: absolute;; top: 97px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=4 OnMouseOver=\"cFrame.HoverSel(0,\'Add_InsurerLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'insurerEntry.asp?qry=addins&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_InsurerLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Insurer</div></span></div><div nowrap style=\"position: absolute;; top: 121px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=5 OnMouseOver=\"cFrame.HoverSel(0,\'Edit_InsurerLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'insurerEdit.asp?qry=editIns&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Edit_InsurerLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Edit Insurer</div></span></div><div nowrap style=\"position: absolute;; top: 145px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=6 OnMouseOver=\"cFrame.HoverSel(0,\'Add_FirmLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'addFirm.asp?qry=addFirm&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_FirmLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Firm</div></span></div><div nowrap style=\"position: absolute;; top: 169px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=7 OnMouseOver=\"cFrame.HoverSel(0,\'Edit_FirmLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'editFirm.asp?qry=ef&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Edit_FirmLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Edit Firm</div></span></div><div nowrap style=\"position: absolute;; top: 193px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=8 OnMouseOver=\"cFrame.HoverSel(0,\'Add_Claim_RepLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'ClaimRepEntry.asp?qry=rep&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_Claim_RepLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Claim Rep</div></span></div><div nowrap style=\"position: absolute;; top: 217px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=9 OnMouseOver=\"cFrame.HoverSel(0,\'Edit_Claim_RepLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'editClaimRep.asp?qry=editclaimrep&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Edit_Claim_RepLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Edit Claim Rep</div></span></div><div nowrap style=\"position: absolute;; top: 241px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=10 OnMouseOver=\"cFrame.HoverSel(0,\'Add_AttorneyLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'attorneyEntry.asp?qry=addattorney&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_AttorneyLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Add Attorney</div></span></div><div nowrap style=\"position: absolute;; top: 265px; left: 1px; width: 123px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=11 OnMouseOver=\"cFrame.HoverSel(0,\'Edit_AttorneyLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'attorneyEdit.asp?qry=editAttorney&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 107px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Edit_AttorneyLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:87px; left:20px;\" align=left>Edit Attorney</div></span></div></div></div><div id=\"grpWArea\" style=\"position: absolute; top:0px; left:0px;; width:127px; height:127px; filter: dropShadow(offX=5,offY=5,color=#B3B3B3);; z-index: 1000; visibility: hidden\"><div id=\"grpWAreafrmt\" style=\"position: absolute; top:0px; left:0px; width: 122px; height: 122px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute;; top: 1px; left: 1px; width: 120px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=12 OnMouseOver=\"cFrame.HoverSel(0,\'Request_LetterLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'reqletter.asp?qry=notes&page=rl\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 104px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Request_LetterLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:84px; left:20px;\" align=left>Request Letter</div></span></div><div nowrap style=\"position: absolute;; top: 25px; left: 1px; width: 120px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=13 OnMouseOver=\"cFrame.HoverSel(0,\'Related_CasesLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'relatedCases.asp?qry=notes&page=rc\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 104px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Related_CasesLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:84px; left:20px;\" align=left>Related Cases</div></span></div><div nowrap style=\"position: absolute;; top: 49px; left: 1px; width: 120px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=14 OnMouseOver=\"cFrame.HoverSel(0,\'PostingsLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'postings.asp?qry=notes&page=postings\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 104px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=PostingsLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:84px; left:20px;\" align=left>Postings</div></span></div><div nowrap style=\"position: absolute;; top: 73px; left: 1px; width: 120px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=15 OnMouseOver=\"cFrame.HoverSel(0,\'SettlementsLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'settlements.asp?qry=notes\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 104px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=SettlementsLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:84px; left:20px;\" align=left>Settlements</div></span></div><div nowrap style=\"position: absolute;; top: 97px; left: 1px; width: 120px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=16 OnMouseOver=\"cFrame.HoverSel(0,\'HearingsLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'hearing.asp?qry=notes\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 104px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=HearingsLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:84px; left:20px;\" align=left>Hearings</div></span></div></div></div><div id=\"grpConfig\" style=\"position: absolute; top:0px; left:0px;; width:125px; height:151px; filter: dropShadow(offX=5,offY=5,color=#B3B3B3);; z-index: 1000; visibility: hidden\"><div id=\"grpConfigfrmt\" style=\"position: absolute; top:0px; left:0px; width: 120px; height: 146px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute;; top: 1px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=17 OnMouseOver=\"cFrame.HoverSel(0,\'Add_StatusLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'addStatus.asp?qry=addstatus&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_StatusLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Add Status</div></span></div><div nowrap style=\"position: absolute;; top: 25px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=18 OnMouseOver=\"cFrame.HoverSel(0,\'Add_DeskLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'qry=adddesk&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_DeskLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Add Desk</div></span></div><div nowrap style=\"position: absolute;; top: 49px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=19 OnMouseOver=\"cFrame.HoverSel(0,\'Add_FeetypeLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'addFeeType.asp?qry=addfeetype&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Add_FeetypeLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Add Feetype</div></span></div><div nowrap style=\"position: absolute;; top: 73px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=20 OnMouseOver=\"cFrame.HoverSel(0,\'Manage_UsersLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'manageUsers.asp?qry=users&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Manage_UsersLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Manage Users</div></span></div><div nowrap style=\"position: absolute;; top: 97px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=21 OnMouseOver=\"cFrame.HoverSel(0,\'Manage_DocsLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'manageDocs.asp?qry=docs&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Manage_DocsLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Manage Docs</div></span></div><div nowrap style=\"position: absolute;; top: 121px; left: 1px; width: 118px; height: 24px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; background-image: url(%%REL%%oxpback.jpg); background-color: #FFFFFF; cursor: help;\" id=22 OnMouseOver=\"cFrame.HoverSel(0,\'Assign_DeskLImg\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'deskAssign.asp?qry=desk&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 102px; height: 16px;\"><span style=\"position:absolute; top:0px; left:0px;\"><img name=Assign_DeskLImg src=\"%%REL%%folder_closed.gif\" width=16 height=16></span><div style=\"position:absolute; top:0px; width:82px; left:20px;\" align=left>Assign Desk</div></span></div></div></div>", '%' + '%REL%%', rimPath));
			close();
		}
SetUpEvents();


