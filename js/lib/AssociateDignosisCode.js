$(document).ready(function () {
    var arrDGCode = [];
    var arrSpeciality = [];

    $('[id$=lnkAddDignosisCode]').click(function () {
       
        //var objTableID = $('[id$=divDiagnosis]');
        var objTableID = $('[id$=divDiagnosis]');
        var selectedValue = $('[id$=txtSearchDignosisCode]').val();

        var array = (selectedValue).split("~");

        var DGType = array[0];
        var CodeDesc = array[1];
        var DGID = array[2];

        if (selectedValue) {
            var Speciality = $('[id$=hdnSpeciality]').val();
            var arrSelectedSpeciality = (Speciality).split(",");

            if (Speciality != "") {
                var str = "";
                for (var i = 0; i < arrSelectedSpeciality.length; i++) {

                    var arrAdd = (arrSelectedSpeciality[i]).split("~");

                    str += "<table><tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='../Images/icon_delete.png' /></a></td>";
                    str += "<td>&nbsp;&nbsp;&nbsp;" + arrAdd[0] + "</td>";
                    str += "<td>&nbsp;&nbsp;&nbsp;" + DGType + "</td>";
                    str += "<td>&nbsp;&nbsp;&nbsp;" + CodeDesc + "</td>";
                    str += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + DGID + "</td>";
                    str += "</tr></table>";

                    // var DGPush = arrAdd[0] + '~' + arrAdd[1] + '~' + DGType + '~' + CodeDesc + '~' + DGID;
                    var DGPush = arrAdd[1] + '~' + DGID;

                    arrDGCode.push(DGPush + "--");
                    $('[id$=hdnDiagnosis]').val(arrDGCode);
                }

                objTableID[0].innerHTML += str;
                $('[id$=txtSearchDignosisCode]').val("");
            }
            else {
                alert('Select Speciality');
            }
        }
        return false;
    });


    $(document).on('click', 'input[type="checkbox"]', function (e) {
       

        var tableName = $(this).closest('table').attr('id');

        if (tableName.indexOf("grdDiagonosisCode") > 1) {

            var Speciality = $('[id$=hdnSpeciality]').val();
            var arrSelectedSpeciality = (Speciality).split(",");

            if ($(this).prop("checked") == true) {

                var DGSpeciality = arrSelectedSpeciality[i];
                var DGType = $(this).closest('td').next('td').text();
                var DGID = $(this).closest('td').next('td').next('td').text();
                var DGCode = $(this).closest('td').next('td').next('td').next('td').text();
                var DGDesc = $(this).closest('td').next('td').next('td').next('td').next('td').text();
                var CodeDesc = DGCode + '-' + DGDesc
               

                var objTableID = $('[id$=divDiagnosis]');
                //var objTableID = document.getElementById("tblDignosis");

                if (Speciality != "") {
                    var str = "";
                    for (var i = 0; i < arrSelectedSpeciality.length; i++) {

                        var arrAdd = (arrSelectedSpeciality[i]).split("~");

                        str += "<table><tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='../Images/icon_delete.png' /></a></td>";
                        str += "<td>&nbsp;&nbsp;&nbsp;" + arrAdd[0] + "</td>";
                        str += "<td>&nbsp;&nbsp;&nbsp;" + DGType + "</td>";
                        str += "<td>&nbsp;&nbsp;&nbsp;" + CodeDesc + "</td>";
                        str += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + DGID + "</td>";
                        str += "</tr></table>";

                        //var DGPush = arrAdd[0] + '~' + arrAdd[1] + '~' + DGType + '~' + CodeDesc + '~' + DGID;
                        var DGPush = arrAdd[1] + '~' + DGID;

                        arrDGCode.push(DGPush + "--");
                        $('[id$=hdnDiagnosis]').val(arrDGCode);
                    }
                    //$('[id$=divDiagnosis]').append += str;
                    objTableID[0].innerHTML+=str;
                }
                else {
                    alert('Select Speciality');
                }
            }
        }
        else if (tableName.indexOf("grdSpeciality") > 1) {
            
            if ($(this).prop("checked") == true) {
                var SpecialityName = $(this).closest('td').next('td').text();
                var PGID = $(this).closest('td').next('td').next('td').text();
                var Speciality = SpecialityName + "~" + PGID;
                arrSpeciality.push(Speciality);
                $('[id$=hdnSpeciality]').val(arrSpeciality);
            }
            else if ($(this).prop("checked") == false) {
                var SpecialityName = $(this).closest('td').next('td').text();

                for (var i = 0; i < arrSpeciality.length; i++) {

                    var arrDGDelete = (arrSpeciality[i]).split("~");

                    if (arrDGDelete[0] == $.trim(SpecialityName)) {
                        var deleteData = arrSpeciality[i];
                        arrSpeciality = $.grep(arrSpeciality, function (n) {
                            return n != deleteData;
                        });

                        $('[id$=hdnSpeciality]').val(arrSpeciality);
                    }
                }

            }
        }
    });


    $(document).on('click', '.btnDeleteDignosis', function () {
        debugger;
        var dgid = $(this).closest('td').next('td').next('td').next('td').next('td').text();

        for (var i = 0; i < arrDGCode.length; i++) {

            var arrDGDelete = (arrDGCode[i]).split("~");

            if ((arrDGDelete[1].replace("--", "")) == $.trim(dgid)) {

                var deleteData = arrDGCode[i];
                arrDGCode = $.grep(arrDGCode, function (n) {
                    return n != deleteData;
                });

                $('[id$=hdnDiagnosis]').val(arrDGCode);
            }
        }
        $(this).closest('tr').remove();
        return false;

    });

});