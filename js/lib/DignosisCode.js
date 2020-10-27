$(document).ready(function () {
    debugger;
    var arrDGCode = [];

    $('[id$=lnkAddDignosisCode]').click(function () {
        debugger;
        var objTableID = document.getElementById("tblDignosis");
        var selectedValue = $('[id$=txtSearchDignosisCode]').val();
        if (selectedValue) {
            //            $('#headerInsurance').html('<b>Selected code(s):</b>');
            var array = (selectedValue).split("~");

            var DGType = array[0];
            var CodeDesc = array[1];
            var DGID = array[2];

            var str = "";
            str += "<tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='../Images/icon_delete.png' /></a></td>";
            str += "<td>&nbsp;&nbsp;&nbsp;" + DGType + "</td>";
            str += "<td>&nbsp;&nbsp;&nbsp;" + CodeDesc + "</td>";
            str += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + DGID + "</td>";
            str += "</tr>"

            objTableID.innerHTML += str;

            //arrDGCode.push(selectedValue.replace(/\,/g, '~'));
            arrDGCode.push(selectedValue + "--");
            $('[id$=hdnDiagnosis]').val(arrDGCode);
            $('[id$=txtSearchDignosisCode]').val("");
        }
        return false;
    });


    $(document).on('click', 'input[type="checkbox"]', function (e) {

        debugger;

        if ($(this).prop("checked") == true) {

            var DGType = $(this).closest('td').next('td').text();
            var DGID = $(this).closest('td').next('td').next('td').text();
            var DGCode = $(this).closest('td').next('td').next('td').next('td').text();
            var DGDesc = $(this).closest('td').next('td').next('td').next('td').next('td').text();
            var CodeDesc = DGCode + '-' + DGDesc
            var DGPush = DGType + '~' + CodeDesc + '~' + DGID

            var arrDGSelect = [];
            arrDGSelect.push(DGType);
            arrDGSelect.push(CodeDesc);
            arrDGSelect.push(DGID);

            var objTableID = document.getElementById("tblDignosis");
            var str = "";
            str += "<tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='../Images/icon_delete.png' /></a></td>";
            str += "<td>&nbsp;&nbsp;&nbsp;" + DGType + "</td>";
            str += "<td>&nbsp;&nbsp;&nbsp;" + CodeDesc + "</td>";
            str += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + DGID + "</td>";
            str += "</tr>"

            objTableID.innerHTML += str;
            //arrDGCode.push(DGPush.replace(/\,/g, '~'));
            arrDGCode.push(DGPush + "--");
            $('[id$=hdnDiagnosis]').val(arrDGCode);
            // $('input:checkbox').removeAttr('checked');
        }
        else {

        }

    });


    $(document).on('click', '.btnDeleteDignosis', function () {
        debugger;
        var dgid = $(this).closest('td').next('td').next('td').next('td').text();

        for (var i = 0; i < arrDGCode.length; i++) {

            var arrDGDelete = (arrDGCode[i]).split("~");

            if (((arrDGDelete[2]).replace("--","")) == $.trim(dgid)) {

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