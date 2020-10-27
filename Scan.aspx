﻿<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Scan.aspx.cs" Inherits="Scan" %>

<%@ Register Src="~/UserControl/OnlingLogControl.ascx" TagName="MessageControl" TagPrefix="LogControl" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Web Scanner</title>
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="X-UA-Compatible" content="requiresActiveX=true" />
    <meta name="viewport" content="width=device-width, maximum-scale=1.0" />
    <link href="Css/style_scan.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/scan/online_demo_operation.js?version=2" type="text/javascript"></script>
    <style type="text/css">
        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            background-color: #5bc0de;
            border-color: #46b8da;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            -o-user-select: none;
            user-select: none;
        }
    </style>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-19660825-1']);
        _gaq.push(['_setDomainName', 'dynamsoft.com']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
        
        function CheckTextbox() {
            var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";      // do not allow characters in the textbox
            for (var i = 0; i < document.getElementById("txt_fileName").length; i++) {
                if (iChars.indexOf(document.getElementById("txt_fileName").value.charAt(i)) != -1) {
                    return false;
                }
            }
            return true;
        }

        function MyFunction() {
            $('#divScan').css('display', 'none');
            $('#divUploadFiles').css('display', 'inline');
        } 
        function DisableUpload() {
            setTimeout(function () {
                document.getElementById('btnUploadFiles').disabled = true;

            }, 50);
           
           
            //sender.style.backgroundColor = "#b0b8c4";
           
           
            return true;

        }
        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

       

        function closeWindow() {
            var reload = getParameterByName('newWindow');
            if (reload != null && reload == "1") {
                window.parent.location.reload(true);
            } else {

                if (window.parent.frames != null) {
                    var frm = getParameterByName("refresh", window.location.href);
                    window.parent.frames[frm].location.reload(true);
                    parent.$("#dialog").dialog("close");
                }
            }
        }
      
      

    </script>
</head>
<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <form id="frmScan" runat="server">
        <div id="divlog" runat="server" visible="true">
            <table style="width: 100%; text-align: right">
                <tr>
                    <td style="text-align: right; width: 85%">
                        <LogControl:MessageControl runat="server" />
                    </td>
                    <td style="text-align: right; width: 20%">
                        <a style="font-family: Arial; font-size: 12px; color: blue; text-decoration: none; cursor: pointer;" href="#" onclick="javascript:closeWindow();">[Close & Refresh]</a>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divHeader">
            <center>
                <input type="radio" name="rdbScan" id="rdbScan" value="scan" checked="checked" />
                Scan
                <input type="radio" name="rdbScan" id="rdbFileUpload1" value="upload" />
                Upload File(s)
            </center>
        </div>

        <div style="text-align: left">
            <asp:Label ID="lblMsg" runat="server" Font-Italic="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
        </div>

        <div id="divScan" runat="server">
            <table width="100%">
                <tr>
                    <td width="45%">
                        <asp:Label ID="lblCompanyname" Visible="true" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:Label ID="lblCaseNO" Visible="true" runat="server"></asp:Label>
                    </td>
                    <td width="35%">
                        <asp:Label ID="lblPatientName" Visible="true" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="text-align: right;">
                        <asp:HiddenField ID="txtCaseDocID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtCompanyName" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtUserName" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtDocType" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtCompanyID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtUserID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtCaseID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtDocID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtRecieved" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtNotes" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtAssignTo" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtAssignOn" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtOtherDoc" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtNodeId" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtflag" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForDocMgr" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForReqDoc" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtNodeType" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForReqVeri" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForReqPay" runat="server"></asp:HiddenField>
                        <%--<asp:HiddenField ID="txtParentUrlForBillsearch" runat="server" ></asp:HiddenField>--%>
                        <asp:HiddenField ID="txtParentUrlForReqPom" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForReqVeriRadio" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForViewBills" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtUploadDoc" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtUploadDocPop" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtDate" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtCaseType" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtSpeciality" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtProcess" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForBillSearch1" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForBillsearch2" runat="server" />
                        <asp:HiddenField ID="txtParentUrlForBillInvoiceReport" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtInVoicePaymentID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtInvoiceFilePath" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtemptyCaseno" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtParentUrlForBillStorageInvoiceReport" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtStorageInVoicePaymentID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtStorageInvoiceFilePath" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtdenial" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtGeneralDenials" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="txtDenialID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="uploadFilename" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="srcFlag" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnQueryString" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
            <div id="container" class="body_Broad_width" style="margin: 0 auto;">
                <div class="DWTHeader">
                    <!-- header.aspx is used to initiate the head of the sample page. Not necessary!-->
                </div>
                <div id="DWTcontainer" class="body_Broad_width" style="background-color: #ffffff; height: 900px; border: 0;">
                    <div id="dwtcontrolContainer" style="height: 420px;">
                    </div>
                    <div id="DWTNonInstallContainerID" style="width: 380px">
                    </div>
                    <div id="DWTemessageContainer" style="margin-left: 50px; width: 580px">
                    </div>
                    <div id="ScanWrapper">
                        <div id="divScanner" class="divinput">
                            <ul class="PCollapse">
                                <li>
                                    <div class="divType">
                                        <div class="mark_arrow expanded">
                                        </div>
                                        Custom Scan
                                    </div>
                                    <div id="div_ScanImage" class="divTableStyle">
                                        <ul id="ulScaneImageHIDE">
                                            <li style="padding-left: 15px;">
                                                <label for="source">
                                                    Select Source:</label>
                                                <select size="1" id="source" style="position: relative; width: 280px;" onchange="source_onchange()">
                                                    <option value=""></option>
                                                </select></li>
                                            <li style="display: none;" id="pNoScanner"><a href="javascript: void(0)" class="ShowtblLoadImage"
                                                style="font-size: 11px; background-color: #f0f0f0; position: relative" id="aNoScanner">
                                                <b>What if I don't have a scanner/webcam connected?</b> </a></li>
                                            <li id="divProductDetail"></li>
                                            <li style="text-align: center;">
                                                <input id="btnScan"
                                                    class="bigbutton"
                                                    style="color: #C0C0C0;"
                                                    disabled="disabled"
                                                    type="button"
                                                    value="Scan"
                                                    onclick="acquireImage();" />
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                                <li>
                                    <div class="divType" style="display: none; visibility: hidden">
                                        <div class="mark_arrow collapsed">
                                        </div>
                                        Load the Sample Images
                                    </div>
                                    <div id="div_SampleImage" style="display: none; visibility: hidden" class="divTableStyle">
                                        <ul>
                                            <li><b>Samples:</b></li>
                                            <li style="text-align: center;">
                                                <table style="border-spacing: 2px; width: 100%;">
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <input name="SampleImage3" type="image" src="Images/icon_associate3.png" style="width: 50px; height: 50px"
                                                                onclick="loadSampleImage(3);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate3.png');"
                                                                onmouseout="Over_Out_DemoImage(this,'Images/icon_associate3.png');" />
                                                        </td>
                                                        <td style="width: 33%">
                                                            <input name="SampleImage2" type="image" src="Images/icon_associate2.png" style="width: 50px; height: 50px"
                                                                onclick="loadSampleImage(2);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate2.png');"
                                                                onmouseout="Over_Out_DemoImage(this,'Images/icon_associate2.png');" />
                                                        </td>
                                                        <td style="width: 33%">
                                                            <input name="SampleImage1" type="image" src="Images/icon_associate1.png" style="width: 50px; height: 50px"
                                                                onclick="loadSampleImage(1);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate1.png');"
                                                                onmouseout="Over_Out_DemoImage(this,'Images/icon_associate1.png');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>B&W Image
                                                        </td>
                                                        <td>Gray Image
                                                        </td>
                                                        <td>Color Image
                                                        </td>
                                                    </tr>
                                                </table>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                                <li>
                                    <div class="divType" style="display: none; visibility: hidden">
                                        <div class="mark_arrow collapsed">
                                        </div>
                                        Load a Local Image
                                    </div>
                                    <div id="div_LoadLocalImage" style="display: none; visibility: hidden" class="divTableStyle">
                                        <ul>
                                            <li style="text-align: center; height: 35px; padding-top: 8px;">
                                                <input type="button" value="Load Image" style="width: 130px; height: 30px; font-size: medium;"
                                                    onclick="return btnUpload_onclick(this)" />
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div id="divBlank" style="height: 20px; display: none; visibility: hidden">
                            <ul>
                                <li></li>
                            </ul>
                        </div>
                        <div id="tblLoadImage" style="visibility: hidden; height: 80px">
                            <ul>
                                <li><b>You can:</b><a href="javascript: void(0)" style="text-decoration: none; padding-left: 200px"
                                    class="ClosetblLoadImage">X</a></li>
                            </ul>
                            <div id="notformac1" style="background-color: #f0f0f0; padding: 5px;">
                                <ul>
                                    <li>
                                        <img alt="arrow" src="Images/arrow.gif" width="9" height="12" /><b>Install a Virtual
                                        Scanner:</b></li>
                                    <li style="text-align: center;"><a id="samplesource32bit" href="http://www.dynamsoft.com/demo/DWT/Sources/twainds.win32.installer.2.1.3.msi">32-bit Sample Source</a> <a id="samplesource64bit" style="display: none;" href="http://www.dynamsoft.com/demo/DWT/Sources/twainds.win64.installer.2.1.3.msi">64-bit Sample Source</a> from <a href="http://www.twain.org">TWG</a></li>
                                </ul>
                            </div>
                        </div>
                        <div id="divEdit" class="divinput" style="position: relative; display: none; visibility: hidden">
                            <ul>
                                <li>
                                    <img alt="arrow" src="Images/arrow.gif" width="9" height="12" /><b>Edit Image</b></li>
                                <li style="padding-left: 9px;">
                                    <img src="Images/ShowEditor.png" title="Show Image Editor" alt="Show Image Editor"
                                        id="btnEditor" onclick="btnShowImageEditor_onclick()" />
                                    <img src="Images/RotateLeft.png" title="Rotate Left" alt="Rotate Left" id="btnRotateL"
                                        onclick="btnRotateLeft_onclick()" />
                                    <img src="Images/RotateRight.png" title="Rotate Right" alt="Rotate Right" id="btnRotateR"
                                        onclick="btnRotateRight_onclick()" />
                                    <img src="Images/Rotate180.png" alt="Rotate 180" title="Rotate 180" onclick="btnRotate180_onclick()" />
                                    <img src="Images/Mirror.png" title="Mirror" alt="Mirror" id="btnMirror" onclick="btnMirror_onclick()" />
                                    <img src="Images/Flip.png" title="Flip" alt="Flip" id="btnFlip" onclick="btnFlip_onclick()" />
                                    <img src="Images/ChangeSize.png" title="Change Image Size" alt="Change Image Size"
                                        id="btnChangeImageSize" onclick="btnChangeImageSize_onclick();" />
                                    <img src="Images/Crop.png" title="Crop" alt="Crop" id="btnCrop" onclick="btnCrop_onclick();" />
                                </li>
                            </ul>
                        </div>
                        <div id="divUpload" class="divinput" style="position: relative">
                            <ul>
                                <li>
                                    <img alt="arrow" src="Images/arrow.gif" width="9" height="12" /><b>Upload Image</b></li>
                                <li style="padding-left: 9px;">
                                    <label for="txt_fileName">
                                        File Name:
                                    <asp:TextBox ID="txtEnterFileName" runat="server" Visible="true" Width="264px"></asp:TextBox>
                                        <%--<input type="text" size="20" id="txt_fileName" />--%></label></li>
                                <li style="padding-left: 9px;">
                                    <label for="imgTypejpeg2">
                                        <input type="radio" disabled="disabled" value="jpg" name="ImageType" id="imgTypejpeg2"
                                            onclick="rd_onclick();" />JPEG</label>
                                    <label for="imgTypetiff2">
                                        <input type="radio" disabled="disabled" value="tif" name="ImageType" id="imgTypetiff2"
                                            onclick="rdTIFF_onclick();" />TIFF</label>
                                    <label for="imgTypepng2">
                                        <input type="radio" disabled="disabled" value="png" name="ImageType" id="imgTypepng2"
                                            onclick="rd_onclick();" />PNG</label>
                                    <label for="imgTypepdf2">
                                        <input type="radio" checked="checked" value="pdf" name="ImageType" id="imgTypepdf2"
                                            onclick="rdPDF_onclick();" />PDF</label></li>
                                <li style="padding-left: 9px;">
                                    <label for="MultiPageTIFF">
                                        <input type="checkbox" id="MultiPageTIFF" />Multi-Page TIFF</label>
                                    <label for="MultiPagePDF">
                                        <input type="checkbox" id="MultiPagePDF" />Multi-Page PDF
                                    </label>
                                </li>
                                <li style="text-align: center">
                                    <%--<input id="btnUpload" type="button" value="Upload Image" onclick="btnUpload_onclick()" />--%>
                                    <input
                                        id="btnUpload"
                                        class="bigbutton"
                                        onclick="return btnUpload_onclick(this)"
                                        runat="server"
                                        type="button"
                                        value="Upload" />
                                    <%-- <asp:Button  ID="btnUpload" runat="server" Text="Upload"   OnClientClick="return btnUpload_onclick()" CssClass="bigbutton" />--%>
                            
                                </li>
                            </ul>
                        </div>
                        <div id="divSave" class="divinput" style="position: relative; visibility: hidden">
                            <ul>
                                <li>
                                    <img alt="arrow" src="Images/arrow.gif" width="9" height="12" /><b>Save Image</b></li>
                                <li style="padding-left: 15px;">
                                    <label for="txt_fileNameforSave">
                                        File Name:
                                    </label>
                                    <input type="text" size="20" id="txt_fileNameforSave" /></li>
                                <li style="padding-left: 12px;">
                                    <label for="imgTypebmp">
                                        <input type="radio" value="bmp" name="imgType_save" id="imgTypebmp" onclick="rdsave_onclick();" />BMP</label>
                                    <label for="imgTypejpeg">
                                        <input type="radio" value="jpg" name="imgType_save" id="imgTypejpeg" onclick="rdsave_onclick();" />JPEG</label>
                                    <label for="imgTypetiff">
                                        <input type="radio" value="tif" name="imgType_save" id="imgTypetiff" onclick="rdTIFFsave_onclick();" />TIFF</label>
                                    <label for="imgTypepng">
                                        <input type="radio" value="png" name="imgType_save" id="imgTypepng" onclick="rdsave_onclick();" />PNG</label>
                                    <label for="imgTypepdf">
                                        <input type="radio" value="pdf" name="imgType_save" id="imgTypepdf" onclick="rdPDFsave_onclick();" />PDF</label></li>
                                <li style="padding-left: 12px;">
                                    <label for="MultiPageTIFF_save">
                                        <input type="checkbox" id="MultiPageTIFF_save" />Multi-Page TIFF</label>
                                    <label for="MultiPagePDF_save">
                                        <input type="checkbox" id="MultiPagePDF_save" />Multi-Page PDF
                                    </label>
                                </li>
                                <li style="text-align: center">
                                    <input id="btnSave" type="button" value="Save Image" onclick="btnSave_onclick()" /></li>
                            </ul>
                        </div>
                        <div id="divNoteMessage" style="display: none; visibility: hidden">
                        </div>
                    </div>
                </div>
                <%-- <div class="DWTTail">
            </div>--%>
            </div>
            <div id="ImgSizeEditor" style="visibility: hidden; text-align: left;">
                <ul>
                    <li>
                        <label for="img_height">
                            <b>New Height :</b>
                            <input type="text" id="img_height" style="width: 50%;" size="10" />pixel</label></li>
                    <li>
                        <label for="img_width">
                            <b>New Width :</b>&nbsp;
                        <input type="text" id="img_width" style="width: 50%;" size="10" />pixel</label></li>
                    <li>Interpolation method:
                    <select size="1" id="InterpolationMethod">
                        <option value=""></option>
                    </select></li>
                    <li style="text-align: center;">
                        <input type="button" value="   OK   " id="btnChangeImageSizeOK" onclick="btnChangeImageSizeOK_onclick();" />
                        <input type="button" value=" Cancel " id="btnCancelChange" onclick="btnCancelChange_onclick();" /></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divUploadFiles" runat="server" style="display: none;">
                    <center>
                        <table cellpadding="10" cellspacing="10" style="border-color: black; margin-top: 70px;" border="0">
                            <tr>
                                <td>File 1
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>File 2
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload2" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>File 3
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload3" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>File 4
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload4" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>File 5
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload5" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button 
                                        ID="btnUploadFiles" 
                                        runat="server"
                                        Text="Upload"
                                        CssClass="bigbutton" OnClientClick="return DisableUpload();"
                                        OnClick="btnUploadFiles_Click" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUploadFiles" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript" language="javascript" src="Resources/dynamsoft.webtwain.initiate.js"></script>
    <script type="text/javascript" language="javascript" src="Resources/dynamsoft.webtwain.config.js"></script>
    <script type="text/javascript" src="js/scan/online_demo_initpage.js"></script>
    <script type="text/javascript" src="js/scan/online_demo_operation.js?version=2"></script>
    <script type="text/javascript" src="js/scan/jquery.js"></script>
    <script type="text/javascript">
        $("ul.PCollapse li>div").click(function () {
            if ($(this).next().css("display") == "none") {
                $(".divType").next().hide("normal");
                $(".divType").children(".mark_arrow").removeClass("expanded");
                $(".divType").children(".mark_arrow").addClass("collapsed");
                $(this).next().show("normal");
                $(this).children(".mark_arrow").removeClass("collapsed");
                $(this).children(".mark_arrow").addClass("expanded");
            }
        });
    </script>
    <script type="text/javascript" language="javascript">
        // Assign the page onload fucntion.
        $(function () {
            pageonload();
        });

        $('#DWTcontainer').hover(function () {
            $(document).bind('mousewheel DOMMouseScroll', function (event) {
                stopWheel(event);
            });
        }, function () {
            $(document).unbind('mousewheel DOMMouseScroll');
        });

        function acquireImage() {
            DWObject.SelectSourceByIndex(document.getElementById("source").selectedIndex);
            DWObject.CloseSource();
            DWObject.OpenSource();
            DWObject.IfShowUI = document.getElementById("ShowUI").checked;
            var visible_lbl = document.getElementById("ShowUI");
            visible_lbl.disabled;

            var i;
            for (i = 0; i < 3; i++) {
                if (document.getElementsByName("PixelType").item(i).checked == true)
                    DWObject.PixelType = i;
            }
            DWObject.Resolution = document.getElementById("Resolution").value;
            DWObject.IfFeederEnabled = document.getElementById("ADF").checked;
            DWObject.IfDuplexEnabled = document.getElementById("Duplex").checked;
            if (Dynamsoft.Lib.env.bWin || (!Dynamsoft.Lib.env.bWin && DWObject.ImageCaptureDriverType == 0))
                appendMessage("Pixel Type: " + DWObject.PixelType + "<br />Resolution: " + DWObject.Resolution + "<br />");

            DWObject.IfDisableSourceAfterAcquire = true;
            DWObject.AcquireImage();
        }

        $('input:radio[name="rdbScan"]').change(function () {
            if ($(this).is(':checked') && $(this).val() == 'scan') {
                $('#divUploadFiles').css('display', 'none');
                $('#divScan').css('display', 'inline');
                $('#FileUpload1').val(null);
                $('#FileUpload2').val(null);
                $('#FileUpload3').val(null);
                $('#FileUpload4').val(null);
                $('#FileUpload5').val(null);
            }
            if ($(this).is(':checked') && $(this).val() == 'upload') {
                $('#divScan').css('display', 'none');
                $('#divUploadFiles').css('display', 'inline');
            }
        });

        $("#txtEnterFileName").keydown(function () {
            var regex = new RegExp("^[a-zA-Z0-9._]+$");
            var key = event.key;
            if (!regex.test(key)) {
                event.preventDefault();
                alert('Only AlphaNumric . and  _ allowed in file name.')
                return false;
            }
        });
    </script>
</body>
</html>
