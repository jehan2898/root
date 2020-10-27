function ShowDialog(divId, iframeId, url, title, width, height) {
    debugger;
    var target = null;
    if (parent.parent)
        target = parent.parent;
    else
        target = parent;

    target.$("#" + divId).dialog({
        autoOpen: false,
        show: "fade",
        hide: "fade",
        modal: true,
        open: function (ev, ui) {
            target.$("#" + iframeId).attr("src", url);
        },
        height: height,
        width: width,
        resizable: true,
        zIndex: 99900000,
        title: title
    });
    target.$("#" + divId).dialog("open");
}

function ShowDialog1(divId, iframeId, url, title, width, height) {
    debugger;
    var target = $(document);
    

    target.$("#" + divId).dialog({
        autoOpen: false,
        show: "fade",
        hide: "fade",
        modal: true,
        open: function (ev, ui) {
            target.$("#" + iframeId).attr("src", url);
        },
        height: height,
        width: width,
        resizable: true,
        zIndex: 99900000,
        title: title
    });
    target.$("#" + divId).dialog("open");
}