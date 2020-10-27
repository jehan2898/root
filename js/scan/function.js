
var pageSrc;
var pageUrl;
function OpenPopUp(arrObj) {
    
    debugger;
    if (arrObj[0].scansource == 'appointment') {
        pageSrc = "Scan.aspx?caseid=" + arrObj[0].caseid + "&caseno=" + arrObj[0].caseno + "&eventid=" + arrObj[0].eventid + "&specialityid=" + arrObj[0].specialityid + "&visittype=" + arrObj[0].visittype + "&visittypeid=" + arrObj[0].visittypeid + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&roomid=" + arrObj[0].RoomID + "&testfacility=" + arrObj[0].testFacility + "&newWindow=1";
        pageUrl = pageSrc;
    } else if (arrObj[0].scansource == 'payment') {
        pageSrc = "../Scan.aspx?caseid=" + arrObj[0].caseid + "&specialty=" + arrObj[0].specialty + "&nodetype=" + arrObj[0].nodetype + "&billno=" + arrObj[0].billno + "&paymentid=" + arrObj[0].paymentid + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&refresh=iFrmMakePayment" + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'pom') {
        pageSrc = "../Scan.aspx?pomid=" + arrObj[0].pomid + "&pomstatus=" + arrObj[0].pomstatus + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    } else if (arrObj[0].scansource == 'docmgr') {
        pageSrc = "../../Scan.aspx?caseid=" + arrObj[0].caseid + "&nodeid=" + arrObj[0].nodeid + "&caseno=" + arrObj[0].caseno + "&scansource=" + arrObj[0].scansource + "&maxcnt="+arrObj[0].maxcnt+"&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'case') {
        pageSrc = "Scan.aspx?pid=" + arrObj[0].pId + "&csid=" + arrObj[0].csId + "&nodetypeid=" + arrObj[0].nodetypeid + "&nodetype=" + arrObj[0].nodetype + "&inodeid=" + arrObj[0].inodeid + "&scansource=" + arrObj[0].scansource + "";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'billverification') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&vrId=" + arrObj[0].vrId + "&billNo=" + arrObj[0].billNo + "&scansource=" + arrObj[0].scansource + "&refresh=iFrmVerificationPopUp&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'billeor') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&vrId=" + arrObj[0].vrId + "&billNo=" + arrObj[0].billNo + "&scansource=" + arrObj[0].scansource + "&refresh=iFrmVerificationPopUp&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'verificationsent') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&vrId=" + arrObj[0].vrId + "&billNo=" + arrObj[0].billNo + "&scansource=" + arrObj[0].scansource + "&refresh=iFrmVerificationSent&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'verificationdenial') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&billNo=" + arrObj[0].billNo + "&verificationId=" + arrObj[0].verificationId + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'billdenial') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&billNo=" + arrObj[0].billNo + "&denialDate=" + arrObj[0].denialDate + "&verificationId=" + arrObj[0].verificationId + "&scansource=" + arrObj[0].scansource + "&refresh=iFrmDenialPopUp&maxcnt=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'verificationpopup') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&spId=" + arrObj[0].spId + "&billNo=" + arrObj[0].billNo + "&verificationId=" + arrObj[0].verificationId + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&flag=" + arrObj[0].flag + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'generaldenial') {
        pageSrc = "../Scan.aspx?csId=" + arrObj[0].csId + "&caseNo=" + arrObj[0].caseNo + "&denialId=" + arrObj[0].denialId + "&nodeId=" + arrObj[0].nodeId + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'reqdoc') {
        pageSrc = "Scan.aspx?csid=" + arrObj[0].csid + "&docid=" + arrObj[0].docid + "&doctypeid=" + arrObj[0].doctypeid + "&inodeid=" + arrObj[0].inodeid + "&notes=" + arrObj[0].notes + "&assingeto=" + arrObj[0].assingeto + "&username=" + arrObj[0].username + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    }
    else if (arrObj[0].scansource == 'JfkVisitDoc') {      
        pageSrc = "../Scan.aspx?caseid=" + arrObj[0].csId + "&visitID=" + arrObj[0].visitID + "&CaseNo=" + arrObj[0].CaseNo + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&newWindow=1";
        pageUrl = pageSrc;
    } else if (arrObj[0].scansource == 'invoicepayment') {
        pageSrc = "../Scan.aspx?billno=" + arrObj[0].billno + "&paymentid=" + arrObj[0].paymentid + "&scansource=" + arrObj[0].scansource + "&maxcnt=" + arrObj[0].maxcnt + "&refresh=iFrmMakePayment" + "&newWindow=1";
        pageUrl = pageSrc;
    }
	
    else {
        pageSrc = "../Scan.aspx?csid=" + arrObj[0].csId + "&pid=" + arrObj[0].pId + "&spid=" + arrObj[0].spId + "&scansource=" + arrObj[0].scansource + "";
        pageUrl = pageSrc;
    }
    //alert(pageUrl);
    ShowDialog('dialog', 'scanIframe', pageUrl, 'Scan / Upload Documents', '740', '630');
}