function scanAppointment(eId, dId, pId, spId, csId) {
    var arrparams = {
        eId: eId,
        dId: dId,
        pId: pId,
        spId: spId,
        csId: csId,
        scansource: 'appointment'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanCase(nodetypeid, nodetype, inodeid) {
    var pId = $('[id$=hdnPatientId]').val();
    var csId = $('[id$=hdnCaseId]').val();
    var scansource;
    var arrparams = {
        pId: pId,
        csId: csId,
        nodetypeid: nodetypeid,
        nodetype: nodetype,
        inodeid: inodeid,
        scansource: 'case'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

//function scanVerification() {
//    //        document.getElementById("ctl00_ContentPlaceHolder1_pnlVerificationSend").style.display = "none";
//    document.getElementById("ctl00_ContentPlaceHolder1_pnlupdategrid").style.display = "none";
//    var scansource;
//    var arrparams = {
//        scansource: 'verification'
//    }
//    var arrObj = [];
//    arrObj.push(arrparams);
//    OpenPopUp(arrObj);
//    this.window.focus();
//}

function scanVerificationP(spId) {
    var scansource;
    var arrparams = {
        spId: spId,
        scansource: 'verification'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanVerificationPopup(spId, typeId) {
    var scansource;
    var arrparams = {
        spId: spId,
        typeId: typeId,
        scansource: 'verification'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}


//function scanDenial(csId, pId, denialId) {
//    var scansource;
//    var arrparams = {
//        csId: csId,
//        pId: pId,
//        denialId: denialId,
//        scansource: 'billdenial'
//    }
//    var arrObj = [];
//    arrObj.push(arrparams);
//    OpenPopUp(arrObj);
//}


function scanDenial(csId, spId, billNo, denialDate, verificationId) {
    var scansource;
    var arrparams = {
        csId: csId,
        spId: spId,
        billNo: billNo,
        denialDate: denialDate,
        verificationId: verificationId,
        scansource: 'billdenial'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanVerificationSent(csId, spId, vrId, billNo, maxcnt) {
    var scansource;
    var arrparams = {
        csId: csId,
        spId: spId,
        vrId: vrId,
        billNo: billNo,
        maxcnt: maxcnt,
        scansource: 'verificationsent'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

//function scanPom(pomID, pomStatus) {
//    var pId = $('[id$=hdnPatientId]').val();
//    var csId = $('[id$=hdnCaseId]').val();
//    var scansource;
//    var arrparams = {
//        pomID: pomID,
//        pomStatus: pomStatus,
//        scansource: 'billpom'
//    }
//    var arrObj = [];
//    arrObj.push(arrparams);
//    OpenPopUp(arrObj);
//}



function scanReqDoc(csid, docid, doctypeid, inodeid, notes, assingeto, username, maxcnt) {

    
    var ReqDoc = {
        csid: csid,
        docid: docid,
        doctypeid: doctypeid,
        inodeid: inodeid,
        notes: notes,
        assingeto: assingeto,
        username: username,
        maxcnt: maxcnt,
        scansource: 'reqdoc'
    }
    var arrObj = [];
    arrObj.push(ReqDoc);
    OpenPopUp(arrObj);
}

function scanVisit(caseid, caseno, eventid, specialityid, visittype, visittypeid, maxcnt, p_sRoomID, test_facility) {
    var appointment = {
        caseid: caseid,
        caseno: caseno,
        eventid: eventid,
        specialityid: specialityid,
        visittype: visittype,
        visittypeid: visittypeid,
        maxcnt: maxcnt,
        RoomID: p_sRoomID,
        scansource: 'appointment',
        testFacility:test_facility
    }
    var arrObj = [];
    arrObj.push(appointment);
    OpenPopUp(arrObj);
}

function scanPayment(caseid, specialty, nodetype, billno, paymentid, maxcnt) {
    
    var payment = {
        caseid: caseid,        
        specialty: specialty,
        nodetype: nodetype,
        billno: billno,
        paymentid: paymentid,
        maxcnt: maxcnt,
        scansource: 'payment'
    }
    var arrObj = [];
    arrObj.push(payment);
    OpenPopUp(arrObj);
}

function scanPOM(pomid, pomstatus, maxcnt) {
    var pom = {
        pomid: pomid,
        pomstatus: pomstatus,
        maxcnt: maxcnt,
        scansource: 'pom'
    }
    var arrObj = [];
    arrObj.push(pom);
    OpenPopUp(arrObj);
}

function scanDocManager(caseid, nodeid, caseno, maxcnt) {
    
    var docmanager = {
        caseid: caseid,
        nodeid: nodeid,
        caseno: caseno,
        maxcnt: maxcnt,
        scansource: 'docmgr'
    }
    var arrObj = [];
    arrObj.push(docmanager);
    OpenPopUp(arrObj);
}

function scanBillVerification(csId, spId, vrId, billNo, maxcnt) {
    var scansource;
    var arrparams = {
        csId:csId,
        spId: spId,
        vrId: vrId,
        billNo: billNo,
        maxcnt: maxcnt,
        scansource: 'billverification'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanBillEOR(csId, spId, vrId, billNo, maxcnt) {
    var scansource;
    var arrparams = {
        csId: csId,
        spId: spId,
        vrId: vrId,
        billNo: billNo,
        maxcnt: maxcnt,
        scansource: 'billeor'
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanVerificationDenial(csId, spId, billNo, verificationId, maxcnt) {
    var scansource;
    var arrparams = {
        csId: csId,
        spId: spId,
        billNo: billNo,
        verificationId: verificationId,
        scansource: 'verificationdenial',
        maxcnt: maxcnt,
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanVerificationPopup(csId, spId, billNo, verificationId, flag, maxcnt) {
    var scansource;
    var arrparams = {
        csId: csId,
        spId: spId,
        billNo: billNo,
        verificationId: verificationId,
        scansource: 'verificationpopup',
        flag:flag,
        maxcnt: maxcnt,
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanGeneralDenial(csId, caseNo, denialId, nodeId, maxcnt) {
    var scansource;
    var arrparams = {
        csId: csId,
        caseNo: caseNo,
        denialId: denialId,
        nodeId: nodeId,
        scansource: 'generaldenial',
        maxcnt: maxcnt
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function ScanJfkVisitDoc(csId, visitID, CaseNo, maxcnt) {
    debugger;

    var arrparams = {
        csId: csId,
        visitID:visitID,
        CaseNo: CaseNo,        
        scansource: 'JfkVisitDoc',
        maxcnt: maxcnt
    }
    var arrObj = [];
    arrObj.push(arrparams);
    OpenPopUp(arrObj);
}

function scanPayment_For_Invoice(billno, paymentid, maxcnt) {
    debugger;
    var payment = {
        
        billno: billno,
        paymentid: paymentid,
        maxcnt: maxcnt,
        scansource: 'invoicepayment'
    }
    var arrObj = [];
    arrObj.push(payment);
    OpenPopUp(arrObj);
}


