
function callAjaxMethod(e) {

    //To prevent postback from happening as we are ASP.Net TextBox control

    //If we had used input html element, there is no need to use ' e.preventDefault()' as posback will not happen

    e.preventDefault();

    $.ajax({
        type: "POST",
        url: "Bill_Sys_GroupProcedureCodeNew.aspx/DataSource",
        data: '{CodeGroup: "PG000000000000001063",CompanyID: "CO000000000000000135",Flag:"LIST"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#<%=txtIsLeapYear.ClientID%>').val('Leap Year');
            }
            else {
                $('#<%=txtIsLeapYear.ClientID%>').val('Not a Leap Year');
            }
        },
        failure: function (response) {
            $('#<%=txtIsLeapYear.ClientID%>').val("Error in calling Ajax:" + response.d);
        }
    });
}
