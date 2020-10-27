//////////////////////////////////////////////////
// DMB DHTML ENGINE 1.7.005                     //
// (c)xFX JumpStart                             //
//                                              //
// PSN: 4058-161142-XFX-3424                    //
//                                              //
// GENERATED: 5/26/2003 - 5:51:52 PM            //
// -------------------------------------------- //
//  Config: Local                               //
//   AddIn: RollerEffect                        //
// JS Name: menuart                             //
//////////////////////////////////////////////////

	
	var nStyle = new Array;
	var hStyle = new Array;
	var nLayer = new Array;
	var hLayer = new Array;
	var nTCode = new Array;
	
	var AnimStep = 0;
	var AnimHnd = 0;
	var NSDelay = 0;
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
	var dxFilter=null;

	var AnimSpeed = 36;
	var TimerHideDelay = 4190;
	var smDelay = 1368;
	var rmDelay = 548;

	var cntxMenu = '';
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
	
	var hid = new Array;
	var hidc = 0;
	
	var BV=parseFloat(navigator.appVersion.indexOf("MSIE")>0?navigator.appVersion.split(";")[1].substr(6):navigator.appVersion);
	var BN=navigator.appName;
	var IsWin=(navigator.userAgent.indexOf('Win')!=-1);
	var IsMac=(navigator.userAgent.indexOf('Mac')!=-1);
	var KQ=(BN.indexOf('Konqueror')!=-1&&(BV>=5))||(navigator.userAgent.indexOf('Safari')!=-1);
	var OP=(navigator.userAgent.indexOf('Opera')!=-1&&BV>=4);
	var NS=(BN.indexOf('Netscape')!=-1&&(BV>=4&&BV<5)&&!OP);
	var SM=(BN.indexOf('Netscape')!=-1&&(BV>=5)||OP);
	var IE=(BN.indexOf('Explorer')!=-1&&(BV>=4)||SM||KQ);
	var IX=(IE&&IsWin&&!SM&&!OP&&(BV>=5.5)&&(dxFilter!=null));
	
	if(!eval(frames['self'])) {
	frames.self = window;
	frames.top = top;
	}
	
var lmcHS = null;
	var tbNum = 0;



	var fx = 1;


	hStyle[0]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[1]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[2]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[3]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[4]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[5]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[6]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[7]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[8]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[9]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[10]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[11]="border: 0px outset #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[12]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[13]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[14]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[15]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[16]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[17]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[18]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[19]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[20]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[21]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	hStyle[22]="border: 0px solid #050620; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #FFFFFF; background-color: #00287B; cursor: hand;";
	nTCode[1]="cFrame.execURL('%%REP%%DataEntry.asp?qry=addclaim&screen=dentry&alphabet=A&ins=A&clientis=A', 'this');";
	nTCode[2]="cFrame.execURL('%%REP%%editMatter.asp?qry=em&screen=dentry&alphabet=A&ins=A&clientis=A', 'this');";
	nTCode[3]="cFrame.execURL('%%REP%%clientEntry.asp?qry=ac&screen=dentry', 'this');";
	nTCode[4]="cFrame.execURL('%%REP%%clientEdit.asp?qry=ec&screen=dentry&alphabet=All', 'this');";
	nTCode[5]="cFrame.execURL('%%REP%%insurerEntry.asp?qry=addins&screen=dentry', 'this');";
	nTCode[6]="cFrame.execURL('%%REP%%insurerEdit.asp?qry=editIns&screen=dentry&alphabet=A', 'this');";
	nTCode[7]="cFrame.execURL('%%REP%%addFirm.asp?qry=addFirm&screen=dentry', 'this');";
	nTCode[8]="cFrame.execURL('%%REP%%editFirm.asp?qry=ef&screen=dentry&alphabet=A', 'this');";
	nTCode[9]="cFrame.execURL('%%REP%%ClaimRepEntry.asp?qry=rep&screen=dentry', 'this');";
	nTCode[10]="cFrame.execURL('%%REP%%editClaimRep.asp?qry=editclaimrep&screen=dentry&alphabet=A', 'this');";
	nTCode[11]="cFrame.execURL('%%REP%%attorneyEntry.asp?qry=addattorney&screen=dentry', 'this');";
	nTCode[12]="cFrame.execURL('%%REP%%attorneyEdit.asp?qry=editAttorney&screen=dentry', 'this');";
	nTCode[13]="cFrame.execURL('%%REP%%reqletter.asp?qry=notes&page=rl', 'this');";
	nTCode[14]="cFrame.execURL('%%REP%%relatedCases.asp?qry=notes&page=rc', 'this');";
	nTCode[15]="cFrame.execURL('%%REP%%postings.asp?qry=notes&page=postings', 'this');";
	nTCode[16]="cFrame.execURL('%%REP%%settlements.asp?qry=notes', 'this');";
	nTCode[17]="cFrame.execURL('%%REP%%hearing.asp?qry=notes', 'this');";
	nTCode[18]="cFrame.execURL('%%REP%%addStatus.asp?qry=addstatus&screen=config', 'this');";
	nTCode[19]="cFrame.execURL('%%REP%%adddesk.asp?qry=adddesk&screen=config', 'this');";
	nTCode[20]="cFrame.execURL('%%REP%%addFeeType.asp?qry=addfeetype&screen=config', 'this');";
	nTCode[21]="cFrame.execURL('%%REP%%manageUsers.asp?qry=users&screen=config', 'this');";
	nTCode[22]="cFrame.execURL('%%REP%%manageDocs.asp?qry=docs&screen=config', 'this');";
	nTCode[23]="cFrame.execURL('%%REP%%deskAssign.asp?qry=desk&screen=config', 'this');";



	function GetCurCmd(e) {
		//IE,SM,OP,KQ
		//This function will return the current command under the mouse pointer.
		//It will return null if the mouse is not over any command.
		//------------------------------
		//Version 1.6
		//
		var cc = e;
		while(cc.id=="") {
			cc = cc.parentElement;
			if(cc==null) break;
		}
		return cc;
	}

	function HoverSel(mode, imgLName, imgRName, e) {
		//IE,SM,OP,KQ
		//This is the function called every time the mouse pointer is moved over a command.
		//------------------------------
		//mode: 0 if the mouse is moving over the command and 1 if is moving away.
		//imgLName: Name of the left image object, if any.
		//imgRName: Name of the right image object, if any.
		//------------------------------
		//Version 16.5
		//
		var nStyle;
		var mc;
		var lc;
		
		if(nOM==0) return false;
		ClearTimer(smHnd);smHnd = 0;
		IsOverHS = false;
		
		if(mode==0) {
			if(OpenMenus[nOM].SelCommand!=null) HoverSel(1);
			mc = GetCurCmd(e);
			if(nOM>1) {
				if(mc==OpenMenus[nOM-1].SelCommand)	return false;
				lc = (BV>=5?mc.parentNode.parentNode.id:mc.parentElement.parentElement.id);
				while(true) {
					if(!nOM) return false;
					if(lc==OpenMenus[nOM].mName) break;
					Hide();
				}
			}
			if(imgLName) imgLRsc = eval(imgLName+"On");
			if(imgRName) imgRRsc = eval(imgRName+"On");
			if(OP&&BV<7)
				mc.opw = OpenMenus[nOM].width - 2*mc.style.left;
			else {
				mc.opw = mc.style.width;
				mc.b = mc.style.borderLeft;
				mc.hasBorder = mc.b.split(" ").length>1;
			}
		
			if(!mc.nStyle) {
				if(OP&&BV<7)
					mc.nStyle = GetOPStyle(mc);
				else
					mc.nStyle = SM?mc.getAttribute("style"):mc.style.cssText;
				mc.hStyle = GetCStyle(mc.style) + ((SM||KQ)?xrep(hStyle[mc.id],"hand","pointer"):hStyle[mc.id]);
			}
		
			OpenMenus[nOM].SelCommand = mc;
			OpenMenus[nOM].SelCommandPar = [imgLName,imgRName,mc.nStyle];
			
			if(SM||KQ) {
				if(OP&&BV<7)
					SetOPStyle(mc, mc.hStyle);
				else
					mc.setAttribute("style", mc.hStyle);
			} else
				mc.style.cssText = mc.hStyle;
			if(!OP) FixCursor(mc.style, mc.hStyle);
		} else {
			mc = (mode==1)?OpenMenus[nOM].SelCommand:OpenMenus[nOM].Opener;
			imgLName = (mode==1)?OpenMenus[nOM].SelCommandPar[0]:OpenMenus[nOM].OpenerPar[0];
			imgRName = (mode==1)?OpenMenus[nOM].SelCommandPar[1]:OpenMenus[nOM].OpenerPar[1];
			nStyle = (mode==1)?OpenMenus[nOM].SelCommandPar[2]:OpenMenus[nOM].OpenerPar[2];
			mc.style.background = (SM?new Image():"");
			if(IsMac) mc.style.border = "0px none";
			if(SM||KQ) {
				if(OP&&BV<7)
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
		
		if(!OP||(OP&&BV>=7)) FixHover(mc, mode);
		
		return true;
	}

	function FixHover(mc, mode) {
		//IE,SM
		//This function fixes the position of the commands' contents when using special highlighting effects.
		//------------------------------
		//Version 2.6
		//
		var hasBorder;
		var bw;
		var s;
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
		//------------------------------
		//Version 13.0
		//
		var mcN;
		
		ClearTimer(parseInt(HTHnd[nOM]));HTHnd[nOM] = 0;
		ClearTimer(smHnd);smHnd = 0;
		if(!nOM) return false;
		
		if(mode==0 && OpenMenus[nOM].SelCommand!=null) NSHoverSel(1);
		
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

	function Hide(chk) {
		//IE,NS,SM,OP,KQ
		//This function hides the last opened group and it keeps hiding all the groups until
		//no more groups are opened or the mouse is over one of them.
		//Also takes care of reseting any highlighted commands.
		//------------------------------
		//Version 4.6
		//
		var m;
		
		ClearTimer(HTHnd[nOM]);HTHnd[nOM] = 0;
		ClearTimer(AnimHnd);AnimHnd = 0;
		ClearTimer(tmrHideHnd);
		
		if(chk)	if(InMenu()) return false;
		
		if(nOM) {
			m = OpenMenus[nOM];
			if(m.SelCommand!=null) {
				if(IE) HoverSel(1);
				if(NS) NSHoverSel(1);
			}
			if(m.Opener!=null) {
				if(IE) HoverSel(3);
				if(NS) NSHoverSel(3);
			}

			ToggleMenu(m, "hidden");
			nOM--;

		}
		
		if(nOM==0) {
			ClearTimer(smHnd);smHnd = 0;
			if(tbNum && lmcHS) {
				if(IE) hsHoverSel(1);
				if(NS) hsNSHoverSel(1);
			}
			status = "";
		} else
			if(!InMenu()) HTHnd[nOM] = window.setTimeout("Hide(1)", TimerHideDelay/20);
		
		return true;
	}

	function ToggleMenu(m, s) {
		//IE,NS,SM,OP,KQ
		//This function controls how the menus are displayed
		//and hidden from the screen.
		//------------------------------
		//Version 1.2
		//
		if(IX)
			if(document.readyState=="complete")
				with(m.obj) {
					if(!m.fs) {
						m.filter = dxFilter + m.filter;
						m.fs = true;
					}
					for(var i=0; i<filters.length; i++) {
						filters[i].apply();
						m.visibility = s;
						filters[i].play();
					}
				}
		if(s=="hidden")
			animBack(1);
		else
			m.visibility = s;
		FormsTweak(s=="visible"?"hidden":"visible");
	}

	function ShowMenu(mName, x, y, isCascading, hsImgName, algn) {
		//IE,NS,SM,OP,KQ
		//This function controls the way menus and submenus are displayed.
		//It also applies a delay to display submenus.
		//------------------------------
		//Version 1.0
		//
		for(var i=1; i<=hidc; i++)
			if(hid[i].o.mName == mName) return false;
		
		ClearTimer(smHnd);smHnd = 0;
		if(isCascading) {
			lsc = OpenMenus[nOM].SelCommand;
			smHnd = window.setTimeout("if(nOM)if(lsc==OpenMenus[nOM].SelCommand)ShowMenu2('"+mName+"',0,0,true,'',"+algn+")", smDelay);
		} else {
			IsOverHS = true;
			if(!algn) algn = 0;
			smHnd = window.setTimeout("ShowMenu2('"+mName+"',"+x+","+y+",false,'"+hsImgName+"',"+algn+")",rmDelay);
		}
		return true;
	}

	function ShowMenu2(mName, x, y, isCascading, hsImgName, algn) {
		//IE,NS,SM,OP,KQ
		//This is the main function to show the menus when a hotspot is triggered or a cascading command is activated.
		//------------------------------
		//mName: Name of the <div> or <layer> to be shown.
		//x: Left position of the menu.
		//y: Top position of the menu.
		//isCascading: True if the menu has been triggered from a command.
		//hsImgName: Image to which the menu is attached to.
		//algn: Alignment setting for the menu.
		//------------------------------
		//Version 20.6
		//
		var xy;
		ClearTimer(parseInt(HTHnd[nOM]));HTHnd[nOM] = 0;
		if(!IsOverHS && !isCascading && !IsContext) return false;
		x = parseInt(x);y = parseInt(y);
		
		var Menu = GetObj(mName);
		if(!Menu) return false;
		if(IE) {
			Menu = Menu.style;
			Menu.obj = GetObj(mName);
			Menu.frmt = GetObj(mName+"frmt").style;
		}
		
		if(nOM>0)
			if(OpenMenus[1].mName == mName && !isCascading) return false;
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
					if(tbMode&2) x = AutoPos(Menu, imgObj, algn)[0] + (IsFrames?GetLeftTop()[0]:0) + MacOffset()[0];
					if(tbMode&1) y = AutoPos(Menu, imgObj, algn)[1] + (IsFrames?GetLeftTop()[1]:0) + MacOffset()[1];
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

var gs = GetObj(OpenMenus[nOM].mName).gs; if(gs) y += parseInt(gs.top);
			}
			Menu.left = FixPos(x,parseInt(Menu.width),pW,0) + "px";
			Menu.top = FixPos(y,parseInt(Menu.height),pH,1) + (OP?"":"px");
			if(!IX) {
				Menu.overflow = "hidden";
				m = GetObj(mName+"frmt").style;
				if(!algn)
					m.a = 0;
				else
					m.a = algn;
				m.d = 1;
				switch(m.a) {
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 9:
						m.rtop = parseInt(m.top);
						m.top = -parseInt(m.height) + "px";
						break;
					case 6:
					case 7:
					case 9:
						m.rleft = parseInt(m.left);
						m.left = -parseInt(m.width) + "px";
						break;
				}
				Menu.m = m;
			}
		}
		if(NS) {
			if(isCascading) {
				xy = GetSubMenuPos(Menu, algn);
				x = xy[0];y = xy[1];
				x += 13;
				y += 95;

			}
			Menu.moveToAbsolute(FixPos(x,Menu.w,pW),FixPos(y,Menu.h,pH));
		}
		Menu.zIndex = 1000+tbNum+nOM;
		OpenMenus[++nOM] = Menu;
		
		if(!NS) FixCommands(mName);
		if(SM) Menu.display = "inline";
		
		if(!IX&&!NS) {
			HTHnd[nOM] = 0;
			AnimHnd = window.setTimeout("Animate()", 10);
		}
		ToggleMenu(Menu, "visible");
		IsContext = false;
		ClearTimer(tmrHideHnd);
		tmrHideHnd = window.setTimeout("AutoHide()", TimerHideDelay);
		
		return true;
	}

	function MacOffset(f) {
		//IE
		//This function calculates the margins for the body under IE/Mac.
		//------------------------------
		//Version 1.0
		//
		if(!f) f = mFrame;
		if(IsMac&&IE&&!SM&&(BV>=5))
			return [parseInt(f.document.body.leftMargin),parseInt(f.document.body.topMargin)];
		return [0,0];
	}

	function GetSubMenuPos(mg, a) {
		//IE,NS,SM,OP,KQ
		//This function calculates the position of a submenu based on its alignment.
		//------------------------------
		//Version 1.1
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
			if(OP) {
				sc.left = sc.style.left;
				sc.top = sc.style.top;
				sc.width = sc.style.pixelWidth;
				sc.height = sc.style.pixelHeight;
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
			case 8:
				x = lp - parseInt(mg.width);
				y = tp + (parseInt(sc.height) - parseInt(mg.height))/2;
				break;
			case 9:
				x = lp + parseInt(sc.width);
				y = tp + (parseInt(sc.height) - parseInt(mg.height))/2;
				break;
			case 10:
				x = lp + (parseInt(sc.width) - parseInt(mg.width))/2;
				y = tp - parseInt(mg.height);
				break;
			case 11:
				x = lp + (parseInt(sc.width) - parseInt(mg.width))/2;
				y = tp + parseInt(sc.height);
				break;
		}
		return [x,y];
	}

	function FixCommands(mName, f, t) {
		//IE, SM
		//This function is used to fix the way the Gecko engine calculates
		//the borders and the way they affect the size of block elements.
		//It also fixes the way IE renderes pages while running CSS1Compat mode.
		//------------------------------
		//Version 2.3
		//
		var en = !OP;
		var m = GetObj(mName, f);
		if(!m.Fixed) {
			if(!f) f = mFrame;
			if(!t) t = 0;
			if(IE&&!SM) en = (OP?false:(f.document.compatMode=="CSS1Compat"));
			if(en) {
				var sd = m.getElementsByTagName("DIV");
				for(var i=0;i<(sd.length);(t>0?i+=2:i++))
					with(sd[i].style) {
						var b = GetBorderWidth(borderLeft);
						if(borderLeft.indexOf("none")==-1) {
							if(parseInt(width) && parseInt(height)) {
								width = parseInt(width) - 2*b + "px";
								height = parseInt(height) - 2*b + "px";
							}
						}
					}
			}
		}
		m.Fixed = true;
	}

	function Animate() {
		//IE,NS,SM,OP,KQ
		//This function is called by ShowMenu every time a new group must be displayed and produces the predefined unfolding effect.
		//Currently is disabled for Navigator, because of some weird bugs we found with the clip property of the layers.
		//------------------------------
		//Version 1.9
		//
		var refresh = false;
		
		for(var i=1; i<=nOM; i++) {
			var m = OpenMenus[i].m;
			if(m.d==1) {
				switch(m.a) {
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 9:
						m.top = parseInt(m.top) + 5 + "px";
						if(parseInt(m.top)>=m.rtop) {
							m.top = m.rtop + "px";
							m.d = 0;
						} else
							refresh = true;
						break;
					case 6:
					case 7:
					case 9:
						m.left = parseInt(m.left) + 5 + "px";
						if(parseInt(m.left)>=m.rleft) {
							m.left = m.rleft + "px";
							m.d = 0;
						} else
							refresh = true;
						break;
				}
			}
		}
		if(refresh) window.setTimeout("Animate()", 1);
	}

	function InTBHotSpot() {
		//IE,NS,SM,OP,KQ
		//This function returns true if the mouse pointer is over a toolbar item.
		//------------------------------
		//Version 1.1
		//
		if(!tbNum) return false;
		var m = lmcHS;
		if(!m) return false;
		if(IE) m = m.style;
		var l = parseInt(m.left);
		var r = l+(IE?parseInt(m.width):m.clip.width);
		var t = parseInt(m.top);
		var b = t+(IE?parseInt(m.height):m.clip.height);
		return ((mX>=l && mX<=r) && (mY>=t && mY<=b)) || IsOverHS;
	}

	function InMenu() {
		//IE,NS,SM,OP,KQ
		//This function returns true if the mouse pointer is over the last opened menu.
		//------------------------------
		//Version 2.1
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
		//IE,NS,SM,OP,KQ
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
		//IE,NS,SM,OP,KQ
		//This function checks if the mouse pointer is on a valid position and if the current menu should be kept visible.
		//The function is called every time the mouse pointer is moved over the document area.
		//------------------------------
		//e: Only used under Navigator, corresponds to the Event object.
		//------------------------------
		//Version 25.2
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
		//Version 3.2
		//
		var fe;
		if(IE&&(!SM||OP)&&DoFormsTweak) {
			var m = OpenMenus[nOM];
			if((BV>=5.5)&&!OP&&m)
				cIF(state=="visible"?"hidden":"visible");
			else
				if(nOM==1)
					for(var f=0; f<mFrame.document.forms.length; f++)
						for(var e=0; e<mFrame.document.forms[f].elements.length; e++) {
							fe = mFrame.document.forms[f].elements[e];
							if(fe.type) if(fe.type.indexOf("select")==0) fe.style.visibility = state;
						}
		}
	}

	function cIF(state) {
		//IE
		//------------------------------
		//Version 1.4
		//
		var mfd = mFrame.document;
		if(mfd.readyState=="complete") {
			if(mfd.getElementsByTagName("SELECT").length==0) return;
			var m = OpenMenus[nOM];
			var iname = m.obj.id + "iframe";
			var i = GetObj(iname);
			if(!i) {
				i = mfd.createElement("?");
				i.id = iname + "pobj";
				mfd.body.insertBefore(i);
				i = mfd.createElement("IFRAME");
				if(location.protocol=="https:") i.src = "/ifo.htm";
				i.id = iname;
				i.style.position = "absolute";
				i.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=0)";
				mfd.getElementById(iname + "pobj").insertBefore(i);
			}		
			with(i.style) {
				left = m.left;
				top = m.top;
				width = m.width;
				height = m.height;
				zIndex = m.zIndex-1;
				visibility = state;
			}
		}
	}

	function execURL(url, tframe) {
		//IE,NS,SM,OP,KQ
		//This function is called every time a command is triggered to jump to another page or execute some javascript code.
		//------------------------------
		//url: Encrypted URL that must be opened or executed.
		//tframe: If the url is a document location, tframe is the target frame where this document will be opened.
		//------------------------------
		//Version 1.3
		//
		HideAll();
		window.setTimeout("execURL2('" + escape(_purl(url)) + "', '" + tframe + "')", 100);
	}

	function execURL2(url, tframe) {
		//IE,NS,SM,OP,KQ
		//This function is called every time a command is triggered to jump to another page or execute some javascript code.
		//------------------------------
		//url: Encrypted URL that must be opened or executed.
		//tframe: If the url is a document location, tframe is the target frame where this document will be opened.
		//------------------------------
		//Version 2.1
		//
		var f = rStr(tframe);
	var fObj = (tframe=='_blank'?window.open(''):(tframe=='_parent'?mFrame.parent:eval(f)));
		if(typeof(fObj)=="undefined")
			fObj = findFrame(top.frames, f.substr(8,f.length-10));
		url = rStr(unescape(url));
		url.indexOf("javascript:")!=url.indexOf("vbscript:")?eval(url):fObj.location.href = url;
	}

	function findFrame(fc, fn) {
		//IE,NS,SM,OP,KQ
		//This recursive function is used to find a frame when the target frame path specified
		//on a menu item does not exists.
		//------------------------------
		//Version 1.0
		//
		var ff;
		for(var i=0; i<fc.length; i++) {
			if(fc[i].name == fn)
				ff = fc[i];
			else
				if(fc[i].frames.length) ff = findFrame(fc[i].frames, fn);
			if(ff) return ff;
		}
		return null;
	}

	function rStr(s) {
		//IE,NS,SM,OP,KQ
		//This function is used to decrypt the URL parameter from the triggered command.
		//------------------------------
		//Version 1.2
		//
		s = xrep(s,"%1E", "'");
		s = xrep(s,"\x1E", "'");
		if(OP&&s.indexOf("frames[")!=-1)
			s = xrep(s,String.fromCharCode(s.charCodeAt(7)), "'");
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
		//IE,NS,SM,OP,KQ
		//This function will hide all the currently opened menus.
		//------------------------------
		//Version 1.1
		//
		if(nOM)
			while(nOM>0) Hide();
		else Hide();
	}

	function tHideAll() {
		//IE,NS,SM,OP,KQ
		//This function is called when the mouse is moved away from a hotspot to close any opened menu.
		//------------------------------
		//Version 1.2
		//
		ClearTimer(HTHnd[nOM]);
		IsOverHS = false;
		HTHnd[nOM] = window.setTimeout("if(!InMenu()&&!InTBHotSpot())HideAll(); else {ClearTimer(HTHnd[nOM]);HTHnd[nOM]=0;}", TimerHideDelay);
	}

	function GetLeftTop(f) {
		//IE,NS,SM,OP,KQ
		//This function returns the scroll bars position on the menus frame.
		//------------------------------
		//Version 2.7
		//
		if(!f) f = mFrame;
		if(IE)
			if(SM)
				return [OP?f.pageXOffset:f.scrollX,OP?pageYOffset:f.scrollY];
			else
				return [GetBodyObj(f).scrollLeft,GetBodyObj(f).scrollTop];
		if(NS)
			return [f.pageXOffset,f.pageYOffset];
	}

	function GetWidthHeight(f) {
		//IE,NS,SM,OP,KQ
		//This function returns the width and height of the menus frame.
		//------------------------------
		//Version 2.5
		//
		if(!f) f = mFrame;
		if(NS||SM) {
			var k = (OP||KQ?0:f.scrollbars.visible?20:0);
			return [f.innerWidth-k,f.innerHeight-k];
		} else
			return [GetBodyObj(f).clientWidth,GetBodyObj(f).clientHeight];
	}

	function GetBodyObj(f) {
		//IE
		//
		//------------------------------
		//Version 1.0
		//
		return (f.document.compatMode=="BackCompat"||BV<6||IsMac)?f.document.body:f.document.documentElement;
	}

	function GetBorderWidth(b) {
		//IE,SM
		//This functions returns the width of a border
		//------------------------------
		//Version 1.3
		//
		if(OP&&BV<7) return 0;
		var w;
		var l = b.split(" ");
		for(var i=0; i<l.length; i++)
			if(l[i].indexOf("px")!=-1) {
				w = parseInt(l[i]);
				if(w>0) return w;
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
		return "position: absolute; white-space: nowrap; left:" + cmc.left + 
			   "; top: " + cmc.top + 
			   "; width: " + (OP?cmc.pixelWidth:cmc.width) + 
			   "; height: " + (OP?cmc.pixelHeight:cmc.height) + "; ";
	}

	function AutoPos(m, img, arl) {
		//IE,NS,SM,OP,KQ
		//This function finds the image-based hotspot and returns the position at which 
		//the menu should be displayed based on the alignment setting.
		//------------------------------
		//Version 1.5
		//
		var x = GetImgXY(img)[0];
		var y = GetImgXY(img)[1];
		var iWH = GetImgWH(img);
		var mW = parseInt(NS?m.w:m.width);
		var mH = parseInt(NS?m.h:m.height);
			
		switch(arl) {
			case 0:
				y += iWH[1];
				break;
			case 1:
				x += iWH[0] - mW;
				y += iWH[1];
				break;
			case 2:
				y -= mH;
				break;
			case 3:
				x += iWH[0] - mW;
				y -= mH;
				break;
			case 4:
				x -= mW;
				break;
			case 5:
				x -= mW;
				y -= mH - iWH[1];
				break;
			case 6:
				x += iWH[0];
				break;
			case 7:
				x += iWH[0];
				y -= mH - iWH[1];
				break;
			case 8:
				x -= mW;
				y += (iWH[1] - mH)/2;
				break;
			case 9:
				x += iWH[0];
				y += (iWH[1] - mH)/2;
				break;
			case 10:
				x += (iWH[0] - mW)/2;
				y -= mH;
				break;
			case 11:
				x += (iWH[0] - mW)/2;
				y += iWH[1];
				break;
		}		
		return [x, y];
	}

	function GetImgXY(img) {
		//IE,NS,SM,OP,KQ
		//This function returns the x,y coordinates of an image.
		//------------------------------
		//Version 1.3
		//
		var x;
		var y;
			
		if(IE)	{
			x = getOffset(img)[0];
			y = getOffset(img)[1];
		} else	{
			y = GetImgOffset(cFrame, img.name, 0, 0);
			x = img.x + y[0];
			y = img.y + y[1];
		}			
		return [x, y];		
	}

	function GetImgWH(img) {
		//IE,NS,SM,OP,KQ
		//This function returns the width and height of an image.
		//------------------------------
		//Version 1.2
		//
		return [parseInt(img.width), parseInt(img.height)];
	}

	function getOffset(img) {
		//IE,NS,SM,OP,KQ
		//This function returns the horizontal and vertical offset of an object.
		//------------------------------
		//Version 1.1
		//
		x = img.offsetLeft;
		y = img.offsetTop;
		ce= img.offsetParent;
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
		//IE,NS,SM,OP,KQ
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
		if(OktoClose&&!IsOverHS) Hide();
		
		if(nOM) if(!InMenu()) tmrHideHnd = window.setTimeout("AutoHide()", TimerHideDelay);
	}

	function ShowContextMenu(e) {
		//IE,NS,SM
		//This function is called when a user rightclicks on the document and it will show a predefined menu.
		//------------------------------
		//Version 1.3
		//
		if(cntxMenu!='') {
			if(IE) {
				IsContext = true;
				SetPointerPos(e);
			}			
			if(NS)
				if(e.which==3) {
					IsContext = true;
					mX = e.x;
					mY = e.y;
				}
		}
		if(IsContext) {
			HideAll();
			cFrame.ShowMenu2(cntxMenu, mX-1, mY-1, false);
			return false;
		}
		return true;
	}

	function SetUpEvents() {
		//IE,NS,SM,OP,KQ
		//This function initializes the frame variables and setups the event handling.
		//------------------------------
		//Version 2.8
		//
		if(!SM) onerror = errHandler;
		if(!mFrame) mFrame = cFrame;
		if(typeof(mFrame)=="undefined" || (NS && (++NSDelay<2)))
			window.setTimeout("SetUpEvents()",10);
		else {
			IsFrames = (cFrame!=mFrame);
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
				if(IsFrames) mFrame.onunload = new Function("mFrame=null;SetUpEvents()");
				if(SM&&!OP) {
					var i = 0;
					var m;
					while(GetObj(i)) {
						m = GetObj(i++);
						m.style.width = parseInt(m.style.width) - 2*parseInt(m.style.paddingLeft) + "px";
						m.style.height = parseInt(m.style.height) - 2*parseInt(m.style.paddingTop) + "px";
					}
				}
			}

			MenusReady = true;
			if(IE) FixImages();
		}


		
		return true;
	}

	function errHandler(sMsg,sUrl,sLine) {
		//IE,NS
		//This function will trap any errors generated by the scripts and filter the unhandled ones.
		//------------------------------
		//Version 1.1
		//
		if(sMsg.substr(0,16)!="Access is denied"&&sMsg!="Permission denied"&&sMsg.indexOf("cursor")==-1)
			alert("Java Script Error\n" +
			      "\nDescription: " + sMsg +
			      "\nSource: " + sUrl +
			      "\nLine: "+sLine);
		return true;
	}

	function FixPos(p, s, r, k) {
		//IE,NS,SM,OP,KQ
		//This function optimizes the position of the menus in order to ensure that they are always
		//displayed inside the browser's visible area.
		//------------------------------
		//Version 2.0
		//
		var n;
		if(nOM==0||k==1)
			n = (p+s>r)?r-s:p;
		else
			n = (p+s>r)?parseInt(OpenMenus[nOM].left)-s:p;
		return (n<0)?0:n;
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
		//IE,NS,SM,OP,KQ
		//This is a helper function used to overcome a bug in very
		//old versions of Navigator 4
		//------------------------------
		//Version 1.0
		//
		if(t) window.clearTimeout(t);
	}

	function xrep(s, f, n) {
		//IE,NS,SM,OP,KQ
		//This function looks for any occurrence of the f string and replaces it with the n string.
		//------------------------------
		//Version 1.2
		//
		if(s) s = s.split(f).join(n);
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

	function FixCursor(mcs, s) {
		//IE
		//This function fixes a bug that affects IE6 and custom cursors.
		//------------------------------
		//Version 1.0
		//
		if(mcs.cursor=="")
			mcs.cursor = (BV<6?"default":s.split("cursor: url(")[1].split(")")[0]);
	}

	function GetObj(oName, f) {
		//IE,NS,SM,OP,KQ
		//This function returns the object whose name is oName and its located in the f frame.
		//------------------------------
		//Version 1.1
		//
		var obj = null;
		if(!f) f = mFrame;
		if(NS)
			obj = f.document.layers[oName];
		else {
			if(BV>=5)
				obj = f.document.getElementById(oName);
			else
				obj = f.document.all[oName];
			if(obj)
				if(obj.id!=oName) obj = null;
		}
		return obj;
	}

	function PrepareEvents() {
		//NS
		//This function is called right after the menus are rendered.
		//It has been designed to attach the event handlers to the <layer> tag and
		//fix the font size problems with Navigator under the Mac and Linux.
		//------------------------------
		//Version 4.1
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

	function animBack(add) {
		//
		//
		//
		if(add) {
			hidc++;
			hid[hidc] = GetObj(OpenMenus[nOM].mName + "frmt").style;
			hid[hidc].o = OpenMenus[nOM];
		}
		if(hidc>0) {
			m = hid[1];
			switch(m.a) {
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					m.top = parseInt(m.top) - 5 + "px";
					if(Math.abs(parseInt(m.top))>=parseInt(m.height)) {
						m.top = m.rtop + "px";
						rmAB(m);
					}
					break;
				case 6:
				case 7:
				case 9:
					m.left = parseInt(m.left) - 5 + "px";
					if(Math.abs(parseInt(m.left))>=parseInt(m.width)) {
						m.left = m.rleft + "px";
						rmAB(m);
					}
					break;
			}
			window.setTimeout("animBack()", 1);
		}
	}

	function rmAB(m) {
		//
		//
		//
		m.d = 1;
		m.o.visibility = "hidden";
		for(var j=1; j<=hidc-1; j++) hid[j]=hid[j+1];
		hidc--;
		animBack();
	}



	if(IE)
		with(document) {
			open();
			write(xrep("<div id=\"grpDentry\" style=\"position: absolute; top:0px; left:0px;; width:857px; height:28px; visibility: hidden; filter: progid:DXImageTransform.Microsoft.Shadow(direction=135, strength=3, color=#F7F7F7)\"><div id=\"grpDentryfrmt\" style=\"position: absolute; top:0px; left:0px; width: 854px; height: 25px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 1px; width: 78px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=0 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%DataEntry.asp?qry=addclaim&screen=dentry&alphabet=A&ins=A&clientis=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 62px; height: 15px;\"><div style=\"position:absolute; top:0px; width:62px; left:0px;\" align=left>Add Matter</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 79px; width: 77px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=1 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%editMatter.asp?qry=em&screen=dentry&alphabet=A&ins=A&clientis=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 61px; height: 15px;\"><div style=\"position:absolute; top:0px; width:61px; left:0px;\" align=left>Edit Matter</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 156px; width: 73px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=2 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%clientEntry.asp?qry=ac&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 57px; height: 15px;\"><div style=\"position:absolute; top:0px; width:57px; left:0px;\" align=left>Add Client</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 229px; width: 72px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=3 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%clientEdit.asp?qry=ec&screen=dentry&alphabet=All\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 56px; height: 15px;\"><div style=\"position:absolute; top:0px; width:56px; left:0px;\" align=left>Edit Client</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 301px; width: 58px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=4 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%insurerEntry.asp?qry=addins&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 42px; height: 15px;\"><div style=\"position:absolute; top:0px; width:42px; left:0px;\" align=left>Add Ins</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 359px; width: 57px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=5 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%insurerEdit.asp?qry=editIns&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 41px; height: 15px;\"><div style=\"position:absolute; top:0px; width:41px; left:0px;\" align=left>Edit Ins</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 416px; width: 66px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=6 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%addFirm.asp?qry=addFirm&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 50px; height: 15px;\"><div style=\"position:absolute; top:0px; width:50px; left:0px;\" align=left>Add Firm</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 482px; width: 65px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=7 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%editFirm.asp?qry=ef&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 49px; height: 15px;\"><div style=\"position:absolute; top:0px; width:49px; left:0px;\" align=left>Edit Firm</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 547px; width: 88px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=8 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%ClaimRepEntry.asp?qry=rep&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 72px; height: 15px;\"><div style=\"position:absolute; top:0px; width:72px; left:0px;\" align=left>Add Clm Rep</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 635px; width: 87px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=9 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%editClaimRep.asp?qry=editclaimrep&screen=dentry&alphabet=A\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 71px; height: 15px;\"><div style=\"position:absolute; top:0px; width:71px; left:0px;\" align=left>Edit Clm Rep</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 722px; width: 66px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=10 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%attorneyEntry.asp?qry=addattorney&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 50px; height: 15px;\"><div style=\"position:absolute; top:0px; width:50px; left:0px;\" align=left>Add Att\'y</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 788px; width: 65px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=11 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%attorneyEdit.asp?qry=editAttorney&screen=dentry\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 49px; height: 15px;\"><div style=\"position:absolute; top:0px; width:49px; left:0px;\" align=left>Edit Att\'y</div></span></div></div></div><div id=\"grpWArea\" style=\"position: absolute; top:0px; left:0px;; width:377px; height:28px; visibility: hidden; filter: progid:DXImageTransform.Microsoft.Shadow(direction=135, strength=3, color=#F7F7F7)\"><div id=\"grpWAreafrmt\" style=\"position: absolute; top:0px; left:0px; width: 374px; height: 25px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 1px; width: 78px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=12 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%reqletter.asp?qry=notes&page=rl\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 62px; height: 15px;\"><div style=\"position:absolute; top:0px; width:62px; left:0px;\" align=left>Req. Letter</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 79px; width: 76px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=13 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%relatedCases.asp?qry=notes&page=rc\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 60px; height: 15px;\"><div style=\"position:absolute; top:0px; width:60px; left:0px;\" align=left>Rel. Cases</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 155px; width: 66px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=14 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%postings.asp?qry=notes&page=postings\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 50px; height: 15px;\"><div style=\"position:absolute; top:0px; width:50px; left:0px;\" align=left>Postings</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 221px; width: 85px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=15 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'Settlements\';\" OnClick=\"cFrame.execURL(\'%%REP%%settlements.asp?qry=notes\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 69px; height: 15px;\"><div style=\"position:absolute; top:0px; width:69px; left:0px;\" align=left>Settlements</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 306px; width: 67px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=16 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%hearing.asp?qry=notes\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 51px; height: 15px;\"><div style=\"position:absolute; top:0px; width:51px; left:0px;\" align=left>Hearings</div></span></div></div></div><div id=\"grpConfig\" style=\"position: absolute; top:0px; left:0px;; width:516px; height:28px; visibility: hidden; filter: progid:DXImageTransform.Microsoft.Shadow(direction=135, strength=3, color=#F7F7F7)\"><div id=\"grpConfigfrmt\" style=\"position: absolute; top:0px; left:0px; width: 513px; height: 25px; background-color: #C0C0C0; \"><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 1px; width: 78px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=17 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%addStatus.asp?qry=addstatus&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 62px; height: 15px;\"><div style=\"position:absolute; top:0px; width:62px; left:0px;\" align=left>Add Status</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 79px; width: 70px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=18 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%adddesk.asp?qry=adddesk&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 54px; height: 15px;\"><div style=\"position:absolute; top:0px; width:54px; left:0px;\" align=left>Add Desk</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 149px; width: 85px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=19 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%addFeeType.asp?qry=addfeetype&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 69px; height: 15px;\"><div style=\"position:absolute; top:0px; width:69px; left:0px;\" align=left>Add Feetype</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 234px; width: 98px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=20 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%manageUsers.asp?qry=users&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 82px; height: 15px;\"><div style=\"position:absolute; top:0px; width:82px; left:0px;\" align=left>Manage Users</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 332px; width: 93px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=21 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%manageDocs.asp?qry=docs&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 77px; height: 15px;\"><div style=\"position:absolute; top:0px; width:77px; left:0px;\" align=left>Manage Docs</div></span></div><div nowrap style=\"position: absolute; white-space: nowrap; top: 1px; left: 425px; width: 87px; height: 23px; font-family: Arial; font-size: 12px; font-weight: bold; font-style: normal; text-decoration: none; color: #999999; ; background-color: #FFFFFF; cursor: hand;\" id=22 OnMouseOver=\"cFrame.HoverSel(0,\'\',\'\',this);status=\'\';\" OnClick=\"cFrame.execURL(\'%%REP%%deskAssign.asp?qry=desk&screen=config\', \'this\');\"><span style=\"position:absolute; top: 4px; left: 8px; width: 71px; height: 15px;\"><div style=\"position:absolute; top:0px; width:71px; left:0px;\" align=left>Assign Desk</div></span></div></div></div>", '%' + '%REL%%', rimPath));
			close();
		}
	if(NS)
		with(document) {
			open();
			write(xrep(FixPointSize("<layer name=grpDentry top=0 left=0 width=854 height=25 z-index=1000 bgColor=#E0E0E0 visibility=hidden><layer bgColor=#C0C0C0 left=0 top=0 width=854 height=25 z-index=1001><layer name=MC1EH1 left=1 top=1 width=78 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC1N left=1 top=1 width=78 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Add Matter</div></layer></layer></font></b></ilayer></layer><layer name=MC1O left=1 top=1 width=78 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Add Matter</div></layer></layer></font></b></ilayer></layer><layer name=MC2EH2 left=79 top=1 width=77 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC2N left=79 top=1 width=77 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=61 height=15><layer top=0 width=61 left=0><div align=left>Edit Matter</div></layer></layer></font></b></ilayer></layer><layer name=MC2O left=79 top=1 width=77 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=61 height=15><layer top=0 width=61 left=0><div align=left>Edit Matter</div></layer></layer></font></b></ilayer></layer><layer name=MC3EH3 left=156 top=1 width=73 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC3N left=156 top=1 width=73 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=57 height=15><layer top=0 width=57 left=0><div align=left>Add Client</div></layer></layer></font></b></ilayer></layer><layer name=MC3O left=156 top=1 width=73 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=57 height=15><layer top=0 width=57 left=0><div align=left>Add Client</div></layer></layer></font></b></ilayer></layer><layer name=MC4EH4 left=229 top=1 width=72 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC4N left=229 top=1 width=72 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=56 height=15><layer top=0 width=56 left=0><div align=left>Edit Client</div></layer></layer></font></b></ilayer></layer><layer name=MC4O left=229 top=1 width=72 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=56 height=15><layer top=0 width=56 left=0><div align=left>Edit Client</div></layer></layer></font></b></ilayer></layer><layer name=MC5EH5 left=301 top=1 width=58 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC5N left=301 top=1 width=58 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=42 height=15><layer top=0 width=42 left=0><div align=left>Add Ins</div></layer></layer></font></b></ilayer></layer><layer name=MC5O left=301 top=1 width=58 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=42 height=15><layer top=0 width=42 left=0><div align=left>Add Ins</div></layer></layer></font></b></ilayer></layer><layer name=MC6EH6 left=359 top=1 width=57 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC6N left=359 top=1 width=57 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=41 height=15><layer top=0 width=41 left=0><div align=left>Edit Ins</div></layer></layer></font></b></ilayer></layer><layer name=MC6O left=359 top=1 width=57 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=41 height=15><layer top=0 width=41 left=0><div align=left>Edit Ins</div></layer></layer></font></b></ilayer></layer><layer name=MC7EH7 left=416 top=1 width=66 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC7N left=416 top=1 width=66 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Add Firm</div></layer></layer></font></b></ilayer></layer><layer name=MC7O left=416 top=1 width=66 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Add Firm</div></layer></layer></font></b></ilayer></layer><layer name=MC8EH8 left=482 top=1 width=65 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC8N left=482 top=1 width=65 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=49 height=15><layer top=0 width=49 left=0><div align=left>Edit Firm</div></layer></layer></font></b></ilayer></layer><layer name=MC8O left=482 top=1 width=65 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=49 height=15><layer top=0 width=49 left=0><div align=left>Edit Firm</div></layer></layer></font></b></ilayer></layer><layer name=MC9EH9 left=547 top=1 width=88 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC9N left=547 top=1 width=88 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=72 height=15><layer top=0 width=72 left=0><div align=left>Add Clm Rep</div></layer></layer></font></b></ilayer></layer><layer name=MC9O left=547 top=1 width=88 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=72 height=15><layer top=0 width=72 left=0><div align=left>Add Clm Rep</div></layer></layer></font></b></ilayer></layer><layer name=MC10EH10 left=635 top=1 width=87 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC10N left=635 top=1 width=87 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=71 height=15><layer top=0 width=71 left=0><div align=left>Edit Clm Rep</div></layer></layer></font></b></ilayer></layer><layer name=MC10O left=635 top=1 width=87 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=71 height=15><layer top=0 width=71 left=0><div align=left>Edit Clm Rep</div></layer></layer></font></b></ilayer></layer><layer name=MC11EH11 left=722 top=1 width=66 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC11N left=722 top=1 width=66 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Add Att\'y</div></layer></layer></font></b></ilayer></layer><layer name=MC11O left=722 top=1 width=66 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Add Att\'y</div></layer></layer></font></b></ilayer></layer><layer name=MC12EH12 left=788 top=1 width=65 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC12N left=788 top=1 width=65 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=49 height=15><layer top=0 width=49 left=0><div align=left>Edit Att\'y</div></layer></layer></font></b></ilayer></layer><layer name=MC12O left=788 top=1 width=65 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=49 height=15><layer top=0 width=49 left=0><div align=left>Edit Att\'y</div></layer></layer></font></b></ilayer></layer></layer></layer><layer name=grpWArea top=0 left=0 width=374 height=25 z-index=1000 bgColor=#E0E0E0 visibility=hidden><layer bgColor=#C0C0C0 left=0 top=0 width=374 height=25 z-index=1001><layer name=MC13EH13 left=1 top=1 width=78 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC13N left=1 top=1 width=78 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Req. Letter</div></layer></layer></font></b></ilayer></layer><layer name=MC13O left=1 top=1 width=78 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Req. Letter</div></layer></layer></font></b></ilayer></layer><layer name=MC14EH14 left=79 top=1 width=76 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC14N left=79 top=1 width=76 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=60 height=15><layer top=0 width=60 left=0><div align=left>Rel. Cases</div></layer></layer></font></b></ilayer></layer><layer name=MC14O left=79 top=1 width=76 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=60 height=15><layer top=0 width=60 left=0><div align=left>Rel. Cases</div></layer></layer></font></b></ilayer></layer><layer name=MC15EH15 left=155 top=1 width=66 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC15N left=155 top=1 width=66 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Postings</div></layer></layer></font></b></ilayer></layer><layer name=MC15O left=155 top=1 width=66 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=50 height=15><layer top=0 width=50 left=0><div align=left>Postings</div></layer></layer></font></b></ilayer></layer><layer name=MC16EH16 left=221 top=1 width=85 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'Settlements\';\"></layer><layer name=MC16N left=221 top=1 width=85 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=69 height=15><layer top=0 width=69 left=0><div align=left>Settlements</div></layer></layer></font></b></ilayer></layer><layer name=MC16O left=221 top=1 width=85 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=69 height=15><layer top=0 width=69 left=0><div align=left>Settlements</div></layer></layer></font></b></ilayer></layer><layer name=MC17EH17 left=306 top=1 width=67 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC17N left=306 top=1 width=67 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=51 height=15><layer top=0 width=51 left=0><div align=left>Hearings</div></layer></layer></font></b></ilayer></layer><layer name=MC17O left=306 top=1 width=67 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=51 height=15><layer top=0 width=51 left=0><div align=left>Hearings</div></layer></layer></font></b></ilayer></layer></layer></layer><layer name=grpConfig top=0 left=0 width=513 height=25 z-index=1000 bgColor=#E0E0E0 visibility=hidden><layer bgColor=#C0C0C0 left=0 top=0 width=513 height=25 z-index=1001><layer name=MC18EH18 left=1 top=1 width=78 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC18N left=1 top=1 width=78 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Add Status</div></layer></layer></font></b></ilayer></layer><layer name=MC18O left=1 top=1 width=78 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=62 height=15><layer top=0 width=62 left=0><div align=left>Add Status</div></layer></layer></font></b></ilayer></layer><layer name=MC19EH19 left=79 top=1 width=70 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC19N left=79 top=1 width=70 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=54 height=15><layer top=0 width=54 left=0><div align=left>Add Desk</div></layer></layer></font></b></ilayer></layer><layer name=MC19O left=79 top=1 width=70 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=54 height=15><layer top=0 width=54 left=0><div align=left>Add Desk</div></layer></layer></font></b></ilayer></layer><layer name=MC20EH20 left=149 top=1 width=85 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC20N left=149 top=1 width=85 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=69 height=15><layer top=0 width=69 left=0><div align=left>Add Feetype</div></layer></layer></font></b></ilayer></layer><layer name=MC20O left=149 top=1 width=85 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=69 height=15><layer top=0 width=69 left=0><div align=left>Add Feetype</div></layer></layer></font></b></ilayer></layer><layer name=MC21EH21 left=234 top=1 width=98 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC21N left=234 top=1 width=98 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=82 height=15><layer top=0 width=82 left=0><div align=left>Manage Users</div></layer></layer></font></b></ilayer></layer><layer name=MC21O left=234 top=1 width=98 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=82 height=15><layer top=0 width=82 left=0><div align=left>Manage Users</div></layer></layer></font></b></ilayer></layer><layer name=MC22EH22 left=332 top=1 width=93 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC22N left=332 top=1 width=93 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=77 height=15><layer top=0 width=77 left=0><div align=left>Manage Docs</div></layer></layer></font></b></ilayer></layer><layer name=MC22O left=332 top=1 width=93 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=77 height=15><layer top=0 width=77 left=0><div align=left>Manage Docs</div></layer></layer></font></b></ilayer></layer><layer name=MC23EH23 left=425 top=1 width=87 height=23 z-index=1003 OnMouseOver=\"cFrame.NSHoverSel(0,this);status=\'\';\"></layer><layer name=MC23N left=425 top=1 width=87 height=23 z-index=1002 bgColor=#FFFFFF><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#999999><layer left=0 top=0 width=71 height=15><layer top=0 width=71 left=0><div align=left>Assign Desk</div></layer></layer></font></b></ilayer></layer><layer name=MC23O left=425 top=1 width=87 height=23 z-index=1002 bgColor=#00287B visibility=hidden><ilayer left=8 top=4><b><font face=Arial point-size=9 color=#FFFFFF><layer left=0 top=0 width=71 height=15><layer top=0 width=71 left=0><div align=left>Assign Desk</div></layer></layer></font></b></ilayer></layer></layer></layer>"), '%' + '%REL%%', rimPath));
			close();
		}
SetUpEvents();

