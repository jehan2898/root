;
Dynamsoft.Lib.images = [];
Dynamsoft.Lib.fetchImage = function(d, f, a, c) {
    var e = d.aryImages[f],
        b = [];
    b.push(e.src);
    b.push("&ticks=");
    b.push(e.ticks);
    a.src = b.join("")
};
var dialogPolyfill = (function() {
    var a = {};
    a.reposition = function(b) {
        var d = document.body.scrollTop || document.documentElement.scrollTop;
        var c = d + (window.innerHeight - b.offsetHeight) / 2;
        b.style.top = c + "px";
        b.dialogPolyfillInfo.isTopOverridden = true
    };
    a.inNodeList = function(b, d) {
        for (var c = 0; c < b.length; ++c) {
            if (b[c] == d) {
                return true
            }
        }
        return false
    };
    a.isInlinePositionSetByStylesheet = function(d) {
        for (var f = 0; f < document.styleSheets.length; ++f) {
            var m = document.styleSheets[f];
            var h = null;
            try {
                h = m.cssRules
            } catch (g) {}
            if (!h) {
                continue
            }
            for (var c = 0; c < h.length; ++c) {
                var l = h[c];
                var n = null;
                try {
                    n = document.querySelectorAll(l.selectorText)
                } catch (g) {}
                if (!n || !a.inNodeList(n, d)) {
                    continue
                }
                var b = l.style.getPropertyValue("top");
                var k = l.style.getPropertyValue("bottom");
                if ((b && b != "auto") || (k && k != "auto")) {
                    return true
                }
            }
        }
        return false
    };
    a.needsCentering = function(c) {
        var b = getComputedStyle(c);
        if (b.position != "absolute") {
            return false
        }
        if ((c.style.top != "auto" && c.style.top != "") || (c.style.bottom != "auto" && c.style.bottom != "")) {
            return false
        }
        return !a.isInlinePositionSetByStylesheet(c)
    };
    a.showDialog = function(c) {
        if (this.open) {
            throw "InvalidStateError: showDialog called on open dialog"
        }
        this.open = true;
        this.setAttribute("open", "open");
        if (c) {
            var b = null;
            var e = null;
            var d = function(f) {
                for (var g = 0; g < f.children.length; g++) {
                    var h = f.children[g];
                    if (b === null && !h.disabled && (h.nodeName == "BUTTON" || h.nodeName == "INPUT" || h.nodeName == "KEYGEN" || h.nodeName == "SELECT" || h.nodeName == "TEXTAREA")) {
                        b = h
                    }
                    if (h.autofocus) {
                        e = h;
                        return
                    }
                    d(h);
                    if (e !== null) {
                        return
                    }
                }
            };
            d(this);
            if (e !== null) {
                e.focus()
            } else {
                if (b !== null) {
                    b.focus()
                }
            }
        }
        if (a.needsCentering(this)) {
            a.reposition(this)
        }
        if (c) {
            this.dialogPolyfillInfo.modal = true;
            a.dm.pushDialog(this)
        }
    };
    a.close = function(b) {
        if (!this.open) {
            throw new InvalidStateError
        }
        this.open = false;
        this.removeAttribute("open");
        if (typeof b != "undefined") {
            this.returnValue = b
        }
        if (this.dialogPolyfillInfo.isTopOverridden) {
            this.style.top = "auto"
        }
        if (this.dialogPolyfillInfo.modal) {
            a.dm.removeDialog(this)
        }
        var c;
        if (document.createEvent) {
            c = document.createEvent("HTMLEvents");
            c.initEvent("close", true, true)
        } else {
            c = new Event("close")
        }
        this.dispatchEvent(c);
        return this.returnValue
    };
    a.registerDialog = function(b) {
        if (b.show) {}
        Dynamsoft.Lib.addEventListener(b, "dialog_submit", function(c) {
            b.close(c.detail.target.value);
            c.preventDefault();
            c.stopPropagation()
        });
        b.show = a.showDialog.bind(b, false);
        b.showModal = a.showDialog.bind(b, true);
        b.close = a.close.bind(b);
        b.dialogPolyfillInfo = {}
    };
    TOP_LAYER_ZINDEX = 100000;
    MAX_PENDING_DIALOGS = 100000;
    a.DialogManager = function() {
        this.pendingDialogStack = [];
        this.overlay = document.createElement("div");
        this.overlay.style.width = "100%";
        this.overlay.style.height = "100%";
        this.overlay.style.position = "fixed";
        this.overlay.style.left = "0px";
        this.overlay.style.top = "0px";
        this.overlay.style.backgroundColor = "rgba(0,0,0,0.0)";
        Dynamsoft.Lib.addEventListener(this.overlay, "click", function(b) {
            var c = document.createEvent("MouseEvents");
            c.initMouseEvent(b.type, b.bubbles, b.cancelable, window, b.detail, b.screenX, b.screenY, b.clientX, b.clientY, b.ctrlKey, b.altKey, b.shiftKey, b.metaKey, b.button, b.relatedTarget);
            document.body.dispatchEvent(c)
        });
        Dynamsoft.Lib.addEventListener(window, "load", function() {
            var b = document.getElementsByTagName("form");
            Array.prototype.forEach.call(b, function(c) {
                if (c.getAttribute("method") == "dialog") {
                    Dynamsoft.Lib.addEventListener(c, "click", function(f) {
                        if (f.target.type == "submit") {
                            var d;
                            if (CustomEvent) {
                                d = new CustomEvent("dialog_submit", {
                                    bubbles: true,
                                    detail: {
                                        target: f.target
                                    }
                                })
                            } else {
                                d = document.createEvent("HTMLEvents");
                                d.initEvent("dialog_submit", true, true);
                                d.detail = {
                                    target: f.target
                                }
                            }
                            this.dispatchEvent(d);
                            f.preventDefault()
                        }
                    })
                }
            })
        })
    };
    a.dm = new a.DialogManager();
    a.DialogManager.prototype.blockDocument = function() {
        if (!document.body.contains(this.overlay)) {
            document.body.appendChild(this.overlay)
        }
    };
    a.DialogManager.prototype.unblockDocument = function() {
        document.body.removeChild(this.overlay)
    };
    a.DialogManager.prototype.updateStacking = function() {
        if (this.pendingDialogStack.length == 0) {
            this.unblockDocument();
            return
        }
        this.blockDocument();
        var d = TOP_LAYER_ZINDEX;
        for (var c = 0; c < this.pendingDialogStack.length; c++) {
            if (c == this.pendingDialogStack.length - 1) {
                this.overlay.style.zIndex = d++
            }
            var b = this.pendingDialogStack[c];
            b.dialogPolyfillInfo.backdrop.style.zIndex = d++;
            b.style.zIndex = d++
        }
    };
    a.DialogManager.prototype.pushDialog = function(c) {
        if (this.pendingDialogStack.length >= MAX_PENDING_DIALOGS) {
            throw "Too many modal dialogs"
        }
        var b = document.createElement("div");
        b.classList.add("backdrop");
        Dynamsoft.Lib.addEventListener(b, "click", function(d) {
            var f = document.createEvent("MouseEvents");
            f.initMouseEvent(d.type, d.bubbles, d.cancelable, window, d.detail, d.screenX, d.screenY, d.clientX, d.clientY, d.ctrlKey, d.altKey, d.shiftKey, d.metaKey, d.button, d.relatedTarget);
            c.dispatchEvent(f)
        });
        c.parentNode.insertBefore(b, c.nextSibling);
        c.dialogPolyfillInfo.backdrop = b;
        this.pendingDialogStack.push(c);
        this.updateStacking()
    };
    a.DialogManager.prototype.removeDialog = function(d) {
        var c = this.pendingDialogStack.indexOf(d);
        if (c == -1) {
            return
        }
        this.pendingDialogStack.splice(c, 1);
        var b = d.dialogPolyfillInfo.backdrop;
        b.parentNode.removeChild(b);
        d.dialogPolyfillInfo.backdrop = null;
        this.updateStacking()
    };
    return a
})();
(function(a, d) {
    var b = [];
    var l = document,
        i = [],
        e = "eng",
        k = d.Event,
        m = 15000,
        c = 18,
        h = {
            btninfo: ["previous", "next", "rotateleft", "rotate", "rotateright", "deskew", "crop", "changeimagesize", "flip", "mirror", "zoomin", "originalsize", "zoomout", "stretch", "fit", "fitw", "fith", "print", "hand", "rectselect", "restore", "save"],
            titlelan: ["eng", "chi"],
            titles: [
                ["Previous Image", "Next Image", "Rotate Left", "Rotate", "Rotate Right", "De Skew", "Crop Selected Area", "Change Image Size", "Flip", "Mirror", "Zoom In", "Original Size", "Zoom Out", "Stretch Mode", "Fit Canvas", "Fit Vertically", "Fit Horizontally", "Print", "Hand", "Select", "Restore", "Save Changes"],
                ["Previous Image", "Next Image", "Rotate Left", "Rotate", "Rotate Right", "De Skew", "Crop Selected Area", "Change Image Size", "Flip", "Mirror", "Zoom In", "Original Size", "Zoom Out", "Stretch Mode", "Fit Canvas", "Fit Vertically", "Fit Horizontally", "Print", "Hand", "Select", "Restore", "Save Changes"]
            ]
        },
        n = {
            isFunction: function(p) {
                return p && typeof(p) === "function"
            },
            __adjustImageSize: function(r, u, q) {
                if (u <= r.width || q <= r.height) {
                    if (u <= r.width && q <= r.height) {
                        var t = u / r.width,
                            p = q / r.height,
                            s;
                        if (t < p) {
                            r.width = Math.floor(r.width * t);
                            r.height = Math.floor(r.height * t)
                        } else {
                            r.width = Math.floor(r.width * p);
                            r.height = Math.floor(r.height * p)
                        }
                    } else {
                        if (u <= r.width) {
                            r.width = u;
                            r.height = Math.floor((u / r.width) * r.height)
                        } else {
                            if (q <= r.height) {
                                r.width = Math.floor((q / r.height) * r.width);
                                r.height = q
                            }
                        }
                    }
                }
            },
            init: function(t, s, q) {
                var u = t,
                    r = q || {},
                    p = Math.floor(Math.random() * 100000 + 1);
                u._UIManager = s;
                u.container = r.container;
                u.containerWidth = r.width ? r.width : 0;
                u.containerHeight = r.height ? r.height : 0;
                u.width = u.containerWidth;
                u.height = u.containerHeight;
                u.defaultIndex = 0;
                u.bEdit = r.bEdit || false;
                u.scrollWidth = 5;
                u.evt_switch = r.evt_switch || "";
                u.evt_mouseMove = r.evt_mouseMove || "";
                u.evt_mouseIn = r.evt_mouseIn || "";
                u.evt_mouseOut = r.evt_mouseOut || "";
                u.evt_mouseClick = r.evt_mouseClick || "";
                u.evt_mouseDblClick = r.evt_mouseDblClick || "";
                u.selectionImageBorderColor = false;
                u.backgroundColor = false;
                u.onRefreshUI = r.onRefreshUI || false;
                u.onMouseRightClick = r.onMouseRightClick || false;
                u.onMouseClick = r.onMouseClick || false;
                u.onMouseDoubleClick = r.onMouseDoubleClick || false;
                u.onMouseMove = r.onMouseMove || false;
                i.push(u)
            },
            setImageUIViewSize: function(q, p, r) {
                var s = q;
                if (p >= 0) {
                    s.containerWidth = p
                }
                if (r >= 0) {
                    s.containerHeight = r
                }
                s.width = s.containerWidth - 15;
                if (s.width < 0) {
                    s.width = 0
                }
                s.height = s.containerHeight - 2;
                if (s.height < 0) {
                    s.height = 0
                }
                s.widthPerImage = Math.floor((s.width - s.scrollWidth) / s.imagesPerRow) - s.ImageMargin;
                if (s.widthPerImage < 0) {
                    s.widthPerImage = 0
                }
                s.heightPerImage = Math.floor(s.height / s.imagesPerColumn) - s.ImageMargin;
                if (s.heightPerImage < 0) {
                    s.heightPerImage = 0
                }
                s.UIViewList.css({
                    width: (s.containerWidth - 2) + "px",
                    height: s.height + "px",
                    "overflow-x": "hidden",
                    "overflow-y": "auto"
                })
            },
            addClass: function(q, p) {
                d.one(q).addClass(p)
            },
            removeClass: function(q, p) {
                d.one(q).removeClass(p)
            },
            print: function(u, z, x) {
                var t, s = 850,
                    q = 1169,
                    w = s / z,
                    A = q / x;
                if (w < A) {
                    q = x * w
                } else {
                    if (w > A) {
                        s = z * A
                    }
                }
                var v, r = ["alwaysRaised=yes,z-look=yes,top=0,left=0,toolbar=no,menubar=no,location=no,status=no,width=", z, ",height=", x].join(""),
                    t = '<html><body><img id="D_img" src="' + u + '" style="width: ' + s + "px;height: " + q + 'px" /></body></html>';
                var y = window.open("about:blank", "_blank", r);
                if (y) {
                    y.document.write(t);
                    y.document.close();
                    y.focus();
                    v = y.document.getElementById("D_img");
                    if (v) {
                        v.onload = function() {
                            y.print();
                            y.close();
                            a.log("print image...ok")
                        }
                    } else {
                        a.log("cannot find image")
                    }
                }
            },
            getToolBar: function(x, p, z) {
                var w;
                for (var t = 0; t < h.titlelan.length; t++) {
                    if (h.titlelan[t] == e) {
                        w = h.titles[t];
                        break
                    } else {
                        w = h.titles[0]
                    }
                }
                var r = document.createElement("ul");
                var s = h.btninfo.length;
                var q = p / s > z ? z * 0.8 : p / s * 0.8;
                q = Math.floor(q);
                var y = document.createElement("li");
                y.style.padding = "0";
                y.style.margin = "0";
                y.style.width = p + "px";
                y.style.height = z + "px";
                for (var v = 0; v < s; v++) {
                    var u = document.createElement("img");
                    u.src = Dynamsoft.WebTwainEnv.ResourcesPath + "/reference/imgs/" + h.btninfo[v] + ".png";
                    u.className = "Class_D_DWT_Editor_Buttons_" + h.btninfo[v];
                    u.title = w[v];
                    u.style.width = q + "px";
                    u.style.height = q + "px";
                    u.style.paddingTop = q / 8 + "px";
                    switch (h.btninfo[v]) {
                        case "previous":
                            u.onclick = function() {
                                x.previous_btn()
                            };
                            break;
                        case "next":
                            u.onclick = function() {
                                x.next_btn()
                            };
                            break;
                        case "rotateleft":
                            u.style.marginLeft = q + "px";
                            u.onclick = function() {
                                x.RotateLeft()
                            };
                            break;
                        case "rotate":
                            u.onclick = function() {
                                x.RotateAnyAngle(this)
                            };
                            break;
                        case "rotateright":
                            u.onclick = function() {
                                x.RotateRight()
                            };
                            break;
                        case "deskew":
                            u.onclick = function() {
                                x.Deskew()
                            };
                            break;
                        case "crop":
                            u.onclick = function() {
                                x.Crop_btn()
                            };
                            break;
                        case "changeimagesize":
                            u.onclick = function() {
                                x.ChangeImageSizeGetinput(this)
                            };
                            break;
                        case "flip":
                            u.onclick = function() {
                                x.Flip()
                            };
                            break;
                        case "mirror":
                            u.onclick = function() {
                                x.Mirror()
                            };
                            break;
                        case "zoomin":
                            u.style.marginLeft = q + "px";
                            u.onclick = function() {
                                x.ZoomIn()
                            };
                            break;
                        case "originalsize":
                            u.onclick = function() {
                                x.OriginalSize()
                            };
                            break;
                        case "zoomout":
                            u.onclick = function() {
                                x.ZoomOut()
                            };
                            break;
                        case "stretch":
                            u.onclick = function() {
                                x.StrechMode()
                            };
                            break;
                        case "fit":
                            u.onclick = function() {
                                x.FitsWindowSize()
                            };
                            break;
                        case "fitw":
                            u.onclick = function() {
                                x.FitsWindowWidth()
                            };
                            break;
                        case "fith":
                            u.onclick = function() {
                                x.FitsWindowHeight()
                            };
                            break;
                        case "print":
                            u.onclick = function() {
                                x.print()
                            };
                            break;
                        case "hand":
                            u.style.marginLeft = q + "px";
                            u.onclick = function() {
                                x.setMouseShape(true)
                            };
                            break;
                        case "rectselect":
                            u.style.marginLeft = q + "px";
                            u.onclick = function() {
                                x.setMouseShape(false)
                            };
                            break;
                        case "restore":
                            u.style.display = "none";
                            u.style.marginLeft = q + "px";
                            u.onclick = function() {
                                x.RestoreImage()
                            };
                            break;
                        case "save":
                            u.style.display = "none";
                            u.onclick = function() {
                                x.SaveImage()
                            };
                            break
                    }
                    y.appendChild(u)
                }
                var A = document.createElement("img");
                A.src = Dynamsoft.WebTwainEnv.ResourcesPath + "/reference/imgs/close.png";
                A.style.width = q + "px";
                A.style.height = q + "px";
                A.style.paddingTop = q / 8 + "px";
                A.title = "Click to Close Window";
                A.className = "floatRight";
                A.onclick = function() {
                    x.HideImageEditor()
                };
                y.appendChild(A);
                r.appendChild(y);
                return r
            },
            SetTag: function(p, r) {
                var s = r * 1,
                    q;
                p.html(s + 1);
                d.DOM.data(p, "n", s);
                if (s >= 99 && s < 999) {
                    q = 35
                } else {
                    if (s >= 999 && s < 9999) {
                        q = 40
                    } else {
                        if (s >= 9999 && s < 99999) {
                            q = 50
                        } else {
                            if (s >= 99999) {
                                q = 60
                            } else {
                                q = 30
                            }
                        }
                    }
                }
                p.style("width", q + "px")
            },
            caculateSliderHeight: function(q) {
                var r = q,
                    p = 0;
                if (r.totalImagesCount > 1) {
                    r.scrollbar.glider.height = (r.scrollbar.height - 2) / r.totalImagesCount;
                    if (r.scrollbar.glider.height < 18) {
                        r.scrollbar.glider.height = 18;
                        p = (r.scrollbar.height - 2 - r.scrollbar.glider.height) / (r.totalImagesCount - 1)
                    } else {
                        p = r.scrollbar.glider.height
                    }
                }
                return p
            },
            getOffset: function(q) {
                var r = q.target,
                    p = 0,
                    s = 0;
                while (r && !isNaN(r.offsetLeft) && !isNaN(r.offsetTop)) {
                    p += r.offsetLeft - r.scrollLeft;
                    s += r.offsetTop - r.scrollTop;
                    r = r.offsetParent
                }
                p = q.clientX - p;
                s = q.clientY - s;
                return {
                    x: p,
                    y: s
                }
            },
            replaceUrlByNewIndex: function(s, t) {
                var v, u, r, q, p;
                v = s.indexOf("index=");
                if (v >= 0) {
                    u = s.indexOf("&", v);
                    r = s.substring(0, v);
                    q = s.substring(u);
                    p = [r, "index=", t, q].join("")
                } else {
                    p = ""
                }
                return p
            },
            output: function(q, p) {
                if (Dynamsoft.WebTwainEnv.Debug) {
                    console.log(p)
                }
            }
        };

    function g(s, q) {
        var u = this,
            r = q || {},
            p, t = '<ul class="D_ImageUIView_List"></ul>';
        n.init(u, s, r);
        u.onSelected = r.onSelected || false;
        u.cIndex = -1;
        u.bFocus = false;
        u.AllowMultiSelect = true;
        u.selectedClass = "D-ImageUI-selected";
        u.highlightClass = "D-ImageUI-highlight";
        u.imagesPerRow = 1;
        u.imagesPerColumn = 1;
        u.bYScroll = true;
        u.ImageMargin = 10;
        p = u.container;
        p.append(t);
        u.UIViewList = p.one(".D_ImageUIView_List");
        u.UIViewList.attr("class", "noPaddingnoMargin thinborder thumbContainer");
        n.setImageUIViewSize(u, r.width, r.height);
        k.on(u.UIViewList, "mousewheel", function(v) {
            if (!u.bFocus) {
                return true
            }
            var w = v.delta;
            if (w < 0) {
                u.next();
                u.fire("onRefreshUI", u.cIndex)
            } else {
                if (w > 0) {
                    u.previous();
                    u.fire("onRefreshUI", u.cIndex)
                }
            }
            return false
        })
    }
    g.prototype.unregistEvents = function() {
        var q = this,
            p = i.indexOf(q);
        if (p >= 0) {
            i.splice(p, 1)
        }
        k.detach(q.UIViewList)
    };
    g.prototype.handlerKeyDown = function(q) {
        var r = this,
            p = true;
        if (!r.bFocus) {
            return p
        }
        switch (q.keyCode) {
            case 37:
                p = false;
                r.previous();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 39:
                p = false;
                r.next();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 38:
                p = false;
                r.previousUp();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 40:
                p = false;
                r.nextDown();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 33:
                p = false;
                r.pageUp();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 34:
                p = false;
                r.pageDown();
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 35:
                p = false;
                r.go(r.count() - 1);
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 36:
                p = false;
                r.go(0);
                r.fire("onRefreshUI", r.cIndex);
                break;
            case 82:
                break;
            case 107:
                break;
            case 109:
                break
        }
        return p
    };
    g.prototype.fullscreen = function() {
        var p = this;
        n.output(p, "fullscreen")
    };
    g.prototype.previous = function(p) {
        var r = this,
            q = r.count();
        if (q == 0) {
            r.cIndex = -1;
            return
        }
        if (r.cIndex <= 0) {
            r.cIndex = 0
        } else {
            r.cIndex--
        }
        r._UIManager.selectedIndexes = [];
        if (r.cIndex >= 0 && r.cIndex < q) {
            r._UIManager.selectedIndexes.push(r.cIndex)
        }
        r._refreshSelection();
        r._refreshScroll();
        if (n.isFunction(p)) {
            p()
        }
    };
    g.prototype.previousUp = function() {
        var q = this,
            p = q.count();
        if (p == 0) {
            q.cIndex = -1;
            return
        }
        if (q.bYScroll) {
            q.cIndex -= q.imagesPerRow
        } else {
            q.cIndex--
        }
        if (q.cIndex <= 0) {
            q.cIndex = 0
        }
        q._UIManager.selectedIndexes = [];
        if (q.cIndex >= 0 && q.cIndex < p) {
            q._UIManager.selectedIndexes.push(q.cIndex)
        }
        q._refreshSelection();
        q._refreshScroll()
    };
    g.prototype.pageUp = function() {
        var q = this,
            p = q.count();
        if (p == 0) {
            q.cIndex = -1;
            return
        }
        if (q.bYScroll) {
            q.cIndex -= q.imagesPerRow * q.imagesPerColumn
        } else {
            q.cIndex--
        }
        if (q.cIndex <= 0) {
            q.cIndex = 0
        }
        q._UIManager.selectedIndexes = [];
        if (q.cIndex >= 0 && q.cIndex < p) {
            q._UIManager.selectedIndexes.push(q.cIndex)
        }
        q._refreshSelection();
        q._refreshScroll()
    };
    g.prototype.next = function(p) {
        var r = this,
            q = r.count();
        if (q == 0) {
            r.cIndex = -1;
            return
        }
        if (r.cIndex >= q - 1) {
            r.cIndex = q - 1
        } else {
            r.cIndex++
        }
        r._UIManager.selectedIndexes = [];
        if (r.cIndex >= 0 && r.cIndex < q) {
            r._UIManager.selectedIndexes.push(r.cIndex)
        }
        r._refreshSelection();
        r._refreshScroll();
        if (n.isFunction(p)) {
            p()
        }
    };
    g.prototype.nextDown = function() {
        var q = this,
            p = q.count();
        if (p == 0) {
            q.cIndex = -1;
            return
        }
        if (q.bYScroll) {
            q.cIndex += q.imagesPerRow
        } else {
            q.cIndex++
        }
        if (q.cIndex >= p - 1) {
            q.cIndex = p - 1
        }
        q._UIManager.selectedIndexes = [];
        if (q.cIndex >= 0 && q.cIndex < p) {
            q._UIManager.selectedIndexes.push(q.cIndex)
        }
        q._refreshSelection();
        q._refreshScroll()
    };
    g.prototype.pageDown = function() {
        var q = this,
            p = q.count();
        if (p == 0) {
            q.cIndex = -1;
            return
        }
        if (q.bYScroll) {
            q.cIndex += q.imagesPerRow * q.imagesPerColumn
        } else {
            q.cIndex++
        }
        if (q.cIndex >= p - 1) {
            q.cIndex = p - 1
        }
        q._UIManager.selectedIndexes = [];
        if (q.cIndex >= 0 && q.cIndex < p) {
            q._UIManager.selectedIndexes.push(q.cIndex)
        }
        q._refreshSelection();
        q._refreshScroll()
    };
    g.prototype.clear = function() {
        var p = this;
        p.cIndex = -1;
        p.UIViewList.children().remove()
    };
    g.prototype.remove = function(r, q) {
        var u = this,
            t = r * 1,
            p = q,
            s;
        if (d.isUndefined(t) || t < 0) {
            return
        }
        s = u.UIViewList.children().item(t);
        if (s) {
            s.remove()
        }
        if (p >= u._UIManager.aryImages.length) {
            u.cIndex = u._UIManager.aryImages.length - 1
        } else {
            u.cIndex = p
        }
        d.each(u.UIViewList.children(), function(x, w) {
            var y = d.one(x);
            if (y) {
                var v = y.one(".imgTag");
                if (v) {
                    n.SetTag(v, w)
                }
            }
        });
        u._refreshSelection();
        u._refreshScroll()
    };
    g.prototype.move = function(s, r) {
        var v = this,
            p = v.count(),
            q = s * 1,
            u = r * 1,
            t;
        if (q < 0 || q >= p || u < 0 || u >= p || q == u) {
            return
        }
        srcDiv = v.UIViewList.children().item(q);
        t = v.UIViewList.children().item(u);
        if (q < u) {
            d.DOM.insertAfter(srcDiv, t)
        } else {
            d.DOM.insertBefore(srcDiv, t)
        }
        d.each(v.UIViewList.children(), function(y, x) {
            var z = d.one(y);
            if (z) {
                var w = z.one(".imgTag");
                if (w) {
                    n.SetTag(w, x)
                }
            }
        })
    };
    g.prototype.refresh = function() {
        var q = this;
        q.UIViewList.children().remove();
        Dynamsoft.Lib.images.splice(0);
        for (var p = 0; p < q._UIManager.aryImages.length; p++) {
            q._UIManager.aryImages[p].bNew = true;
            q.showImage(p)
        }
    };
    g.prototype.print = function() {
        var v = this,
            u = v.cIndex,
            q, t, p = [];
        if (u < 0) {
            return
        }
        t = v._UIManager.aryImages[u];
        if (!t) {
            n.output(v, "The index out of range.(" + u + ")");
            return
        }
        p.push(t.urlPrefix);
        p.push("&index=");
        p.push(u);
        p.push("&width=-1&height=-1&ticks=");
        p.push(t.ticks);
        var s, r = function() {
            n.print(p.join(""), s.width, s.height);
            s.src = "";
            s = null
        };
        s = new Image();
        s.onload = r;
        s.src = p.join("")
    };
    g.prototype.getMousePosition = function() {
        var p = this;
        n.output(p, "getMousePosition")
    };
    g.prototype.show = function() {
        var q = this,
            p = q.UIViewList.style("display");
        q.UIViewList.style("display", "");
        if (p == "none") {
            q.refresh()
        }
    };
    g.prototype.hide = function() {
        var p = this;
        p.UIViewList.style("display", "none")
    };
    g.prototype.SetViewMode = function(v, t) {
        var w = this,
            s = w.cIndex,
            u, r, q;
        r = w.imagesPerRow * w.imagesPerColumn;
        if (r < 0) {
            r = 1
        }
        q = v * t;
        if (q < 0) {
            q = 1
        }
        w.imagesPerRow = v;
        w.imagesPerColumn = t;
        if (t == -1) {
            w.bYScroll = false;
            w.imagesPerRow = 1;
            w.imagesPerColumn = 1;
            w.width = w.containerWidth - 2;
            w.height = w.containerHeight - 15;
            if (w.selectionImageBorderColor) {
                w.width -= 2;
                w.height -= 2
            }
            w.UIViewList.style("overflow-x", "auto");
            w.UIViewList.style("overflow-y", "hidden");
            w.widthPerImage = Math.floor((w.width) / w.imagesPerRow) - w.ImageMargin;
            w.heightPerImage = Math.floor((w.height - w.scrollWidth) / w.imagesPerColumn) - w.ImageMargin;
            var p = w.container;
            p.style("white-space", "nowrap")
        } else {
            w.bYScroll = true;
            w.width = w.containerWidth - 15;
            w.height = w.containerHeight - 2;
            if (w.selectionImageBorderColor) {
                w.width -= 2;
                w.height -= 2
            }
            w.UIViewList.style("overflow-y", "auto");
            w.UIViewList.style("overflow-x", "hidden");
            w.widthPerImage = Math.floor((w.width - w.scrollWidth) / w.imagesPerRow) - w.ImageMargin;
            w.heightPerImage = Math.floor(w.height / w.imagesPerColumn) - w.ImageMargin;
            var p = w.container;
            p.style("white-space", "")
        }
        d.each(w.UIViewList.children(), function(C, z) {
            var E = d.one(C);
            if (E) {
                var y = E.one(".imgwrap"),
                    B = y.one("img"),
                    x = E.one(".imgTag");
                if (w.bYScroll) {
                    E.style("float", "left").style("display", "inline");
                    x.style("display", "inline")
                } else {
                    E.style("float", "").style("display", "inline-block");
                    x.style("display", "inline-block")
                }
                E.style("height", w.heightPerImage + "px");
                E.style("width", w.widthPerImage + "px");
                y.style("height", (w.heightPerImage - 2) + "px");
                y.style("width", (w.widthPerImage - 2) + "px");
                if (x) {
                    n.SetTag(x, z)
                }
                if (B) {
                    var A, D;
                    if (r < q) {
                        A = {
                            width: B[0].width,
                            height: B[0].height
                        }
                    } else {
                        D = d.DOM.data(y);
                        if (!D) {
                            return true
                        }
                        A = {
                            width: D.w,
                            height: D.h
                        }
                    }
                    if (A) {
                        n.__adjustImageSize(A, (w.widthPerImage - 2), (w.heightPerImage - 2));
                        B[0].width = A.width;
                        B[0].height = A.height
                    }
                }
            }
        });
        u = w.UIViewList.children().item(s);
        if (u) {
            if (w.bYScroll) {
                d.DOM.scrollIntoView(u, w.UIViewList, {
                    onlyScrollIfNeeded: true,
                    allowHorizontalScroll: false
                })
            } else {
                d.DOM.scrollIntoView(u, w.UIViewList, {
                    onlyScrollIfNeeded: true,
                    allowHorizontalScroll: true
                })
            }
        }
    };
    g.prototype.GetViewModeH = function() {
        var p = this;
        return p.imagesPerRow
    };
    g.prototype.GetViewModeV = function() {
        var p = this;
        return p.imagesPerColumn
    };
    g.prototype.go = function(r, p) {
        var t = this,
            s = r,
            q = t.count();
        if (d.isUndefined(s) || s < 0 || s >= q) {
            s = t.cIndex
        }
        t.cIndex = s;
        t._UIManager.selectedIndexes = [];
        if (t.cIndex >= 0 && t.cIndex < q) {
            t._UIManager.selectedIndexes.push(t.cIndex)
        }
        if (t.UIViewList.style("display") == "") {
            t._refreshSelection();
            t._refreshScroll()
        }
        if (n.isFunction(p)) {
            p()
        }
    };
    g.prototype.isFirst = function() {
        var p = this;
        return p.cIndex === 0
    };
    g.prototype.isLast = function() {
        var q = this,
            p = q.count();
        return q.cIndex !== -1 && q.cIndex === (p - 1)
    };
    g.prototype._refreshSelection = function() {
        var p = this;
        p.unHighlightAll();
        p.hightlight(p._UIManager.selectedIndexes)
    };
    g.prototype.fire = function(p, q) {
        var r = this;
        if (n.isFunction(r[p])) {
            return r[p](q)
        }
        return true
    };
    g.prototype.hightlight = function(p) {
        var t = this,
            s, r;
        if (typeof p === "object") {
            for (var q = 0; q < p.length; q++) {
                r = p[q];
                if (r < 0) {
                    continue
                }
                s = t.UIViewList.children().item(r);
                if (s) {
                    s.addClass(t.selectedClass);
                    if (t.selectionImageBorderColor) {
                        s.one(".imgwrap").style("border", ["1px solid ", t.selectionImageBorderColor].join(""))
                    }
                }
            }
        }
    };
    g.prototype.unHighlightAll = function() {
        var p = this;
        p.UIViewList.all("." + p.selectedClass).removeClass(p.selectedClass);
        p.UIViewList.all(".imgwrap").style("border", "")
    };
    g.prototype.showImage = function(B, q) {
        var A = this,
            B = parseInt(B),
            w, u, s, z;
        w = A.widthPerImage / 25 < 30 ? 30 : A.widthPerImage / 25;
        u = A._UIManager.aryImages[B];
        s = u.bNew;
        if (s) {
            var p = document.createElement("div"),
                x = d.one(p);
            x.addClass("imgwrap").addClass("imgBox");
            if (A.backgroundColor) {
                p.style.backgroundColor = A.backgroundColor
            }
            z = new Image();
            z.onload = function() {
                var E = d.DOM.data(x);
                E.w = z.width;
                E.h = z.height;
                n.__adjustImageSize(z, (A.widthPerImage - 2), (A.heightPerImage - 2));
                if (x.children().length == 0) {
                    x.append(z)
                }
                z = null
            };
            Dynamsoft.Lib.fetchImage(A._UIManager, B, z, true);
            var y = document.createElement("div");
            y.className = "imgTag";
            n.SetTag(d.one(y), B);
            var D = document.createElement("li");
            D.className = "thumb";
            D.style.height = A.heightPerImage + "px";
            D.style.width = A.widthPerImage + "px";
            D.style.margin = [A.ImageMargin / 2, "px"].join("");
            x.style("height", (A.heightPerImage - 2) + "px");
            x.style("width", (A.widthPerImage - 2) + "px");
            if (A.bYScroll) {
                d.one(D).style("float", "left").style("display", "inline");
                y.style.display = "inline"
            } else {
                D.style.display = "inline-block";
                y.style.display = "inline-block"
            }
            D.appendChild(p);
            D.appendChild(y);
            k.on(D, "mouseenter", function(F) {
                var E = d.DOM.data(y, "n");
                A.bFocus = true;
                A.fire("onMouseMove", E)
            });
            k.on(D, "mouseleave", function(E) {
                A.bFocus = false;
                A.fire("onMouseMove", -1)
            });
            k.on(D, "dblclick", function(F) {
                var E = d.DOM.data(y, "n");
                A.fire("onMouseDoubleClick", E)
            });
            k.on(D, "contextmenu", function(F) {
                var E = d.DOM.data(y, "n");
                return A.fire("onMouseRightClick", E)
            });
            k.on(D, "click", function(I) {
                var H = d.DOM.data(y, "n");
                var K = A._UIManager.selectedIndexes.indexOf(H),
                    E = I.ctrlKey,
                    J = I.shiftKey;
                if (K == -1) {
                    if (E || J || A._UIManager.selectedIndexes.length < 1) {
                        A._UIManager.selectedIndexes.push(H)
                    } else {
                        if (A._UIManager.selectedIndexes.length == 1) {
                            A._UIManager.selectedIndexes[0] = H
                        } else {
                            A._UIManager.selectedIndexes = [];
                            A._UIManager.selectedIndexes.push(H)
                        }
                    }
                } else {
                    if (E && A._UIManager.selectedIndexes.length > 1) {
                        A._UIManager.selectedIndexes.splice(K, 1)
                    } else {
                        if (J) {
                            A._UIManager.selectedIndexes.push(H)
                        } else {
                            A._UIManager.selectedIndexes = [];
                            A._UIManager.selectedIndexes.push(H)
                        }
                    }
                }
                if (J) {
                    var M = A._UIManager.selectedIndexes[0],
                        G = A._UIManager.selectedIndexes[A._UIManager.selectedIndexes.length - 1];
                    A._UIManager.selectedIndexes = [];
                    if (G == M) {
                        A._UIManager.selectedIndexes.push(parseInt(M))
                    } else {
                        if (G > M) {
                            var L = G - M + 1;
                            for (var F = 0; F < L; F++) {
                                A._UIManager.selectedIndexes.push(parseInt(M + F))
                            }
                        } else {
                            var L = M - G + 1;
                            A._UIManager.selectedIndexes.push(parseInt(M));
                            for (var F = 0; F < L - 1; F++) {
                                A._UIManager.selectedIndexes.push(parseInt(G + F))
                            }
                        }
                    }
                }
                if (A._UIManager.selectedIndexes.length == 1) {
                    A.cIndex = H
                }
                A.fire("onSelected", A._UIManager.selectedIndexes);
                A._refreshSelection();
                if (I.which ? I.which == 3 : I.button == 2) {
                    A.fire("onMouseRightClick", H)
                } else {
                    A.fire("onMouseClick", H)
                }
            });
            if (q) {
                var C = A.UIViewList.children().item(B);
                if (C) {
                    d.DOM.insertBefore(D, C)
                }
                d.each(A.UIViewList.children(), function(G, F) {
                    var H = d.one(G);
                    if (H) {
                        var E = H.one(".imgTag");
                        if (E) {
                            n.SetTag(E, F)
                        }
                    }
                })
            } else {
                A.UIViewList.append(D)
            }
            u.bNew = false
        } else {
            var t = A.UIViewList.children().item(B);
            if (t) {
                var v = t.one(".imgwrap"),
                    r = t.one(".imgTag");
                t.style("height", A.heightPerImage + "px");
                t.style("width", A.widthPerImage + "px");
                if (r) {
                    n.SetTag(r, B)
                }
                if (v) {
                    v.style("height", (A.heightPerImage - 2) + "px");
                    v.style("width", (A.widthPerImage - 2) + "px");
                    z = new Image();
                    z.onload = function() {
                        var E = d.DOM.data(v);
                        E.w = z.width;
                        E.h = z.height;
                        n.__adjustImageSize(z, (A.widthPerImage - 2), (A.heightPerImage - 2));
                        v.html("");
                        v.append(z);
                        z = null
                    };
                    Dynamsoft.Lib.fetchImage(A._UIManager, B, z, false)
                }
            }
        }
        if (A.UIViewList.children().length == A._UIManager.aryImages.length) {
            A._refreshSelection()
        }
        A._refreshScroll()
    };
    g.prototype._refreshScroll = function() {
        var s = this,
            r = 0,
            p = d.DOM,
            q;
        if (s.cIndex >= 0 && s.cIndex < s.count()) {
            q = s.UIViewList.children().item(s.cIndex)
        }
        if (q) {
            if (s.bYScroll) {
                p.scrollIntoView(q, s.UIViewList, {
                    onlyScrollIfNeeded: true,
                    allowHorizontalScroll: false
                })
            } else {
                p.scrollIntoView(q, s.UIViewList, {
                    onlyScrollIfNeeded: true,
                    allowHorizontalScroll: true
                })
            }
        } else {
            if (s.bYScroll) {
                p.scrollTop(s.UIViewList, 0)
            } else {
                p.scrollLeft(s.UIViewList, 0)
            }
        }
    };
    g.prototype.count = function() {
        var p = this;
        return p._UIManager.aryImages.length
    };
    g.prototype.add = function(r, s) {
        var t = this,
            q = parseInt(s),
            p = true;
        if (q >= t._UIManager.aryImages.length) {
            t._UIManager.aryImages.push(r);
            t.cIndex = t._UIManager.aryImages.length - 1
        } else {
            if (q < 0) {
                q = 0
            }
            t.cIndex = q;
            t._UIManager.aryImages.splice(q, 0, r);
            p = false
        }
        if (t.cIndex >= t._UIManager.aryImages.length) {
            t.cIndex = t._UIManager.aryImages.length - 1
        }
        t._UIManager.selectedIndexes = [];
        t._UIManager.selectedIndexes.push(t.cIndex);
        if (t.UIViewList.style("display") == "") {
            if (p) {
                t.showImage(t.cIndex)
            } else {
                t.showImage(t.cIndex, true)
            }
        }
        return true
    };
    g.prototype.refreshIndex = function(r) {
        var u = this,
            p, t, q, s;
        u.cIndex = r, tmp = [];
        if (u.UIViewList.style("display") == "") {
            t = u.UIViewList.children().item(r);
            q = t.one(".imgwrap");
            p = new Image();
            p.onload = function() {
                var v = d.DOM.data(q);
                v.w = p.width;
                v.h = p.height;
                n.__adjustImageSize(p, (u.widthPerImage - 2), (u.heightPerImage - 2));
                q.html("");
                q.append(p);
                p = null
            };
            s = u._UIManager.aryImages[r];
            tmp.push(s.src);
            tmp.push("&ticks=");
            tmp.push(s.ticks);
            p.src = tmp.join("")
        }
        return true
    };
    g.prototype.RemoveAllSelectedImages = function() {
        var q = this,
            p = q.count();
        q._UIManager.selectedIndexes = [];
        if (q.cIndex >= 0 && q.cIndex < p) {
            q._UIManager.selectedIndexes.push(q.cIndex)
        }
        q._refreshSelection();
        q._refreshScroll()
    };
    g.prototype.SwitchImage = function(x, w) {
        var v = this,
            y = v.count(),
            t, s, r, q, u, p;
        if (x >= 0 && x < y && w >= 0 && w < y) {
            t = v.UIViewList.children().item(x);
            s = v.UIViewList.children().item(w);
            if (t && s) {
                r = t.one("img");
                u = r.parent();
                q = s.one("img");
                p = q.parent();
                p.append(r);
                u.append(q);
                t = s = r = q = u = p = null
            }
        }
    };
    g.prototype.getImageMargin = function() {
        var p = this;
        return p.ImageMargin
    };
    g.prototype.setImageMargin = function(p) {
        var r = this,
            q = -1;
        r.ImageMargin = p;
        if (r.bYScroll) {
            q = r.imagesPerColumn
        }
        r.SetViewMode(r.imagesPerRow, q)
    };
    g.prototype.getSelectionImageBorderColor = function() {
        var p = this;
        return p.selectionImageBorderColor
    };
    g.prototype.setSelectionImageBorderColor = function(q) {
        var s = this,
            p = q,
            r = -1;
        if (d.isNumber(p)) {
            p = Dynamsoft.Lib.getColor(p)
        }
        s.selectionImageBorderColor = p;
        if (s.bYScroll) {
            r = s.imagesPerColumn
        }
        s.SetViewMode(s.imagesPerRow, r)
    };
    g.prototype.setBackgroundColor = function(q) {
        var r = this,
            p = q;
        if (d.isNumber(p)) {
            p = Dynamsoft.Lib.getColor(p)
        }
        r.backgroundColor = p;
        r.UIViewList.all(".imgwrap").style("background-color", p)
    };
    g.prototype.setAllowMultiSelect = function(p) {
        var q = this;
        q.AllowMultiSelect = p
    };
    g.prototype.getAllowMultiSelect = function() {
        var p = this;
        return p.AllowMultiSelect
    };
    g.prototype.ChangeSize = function(p, r) {
        var s = this;
        n.setImageUIViewSize(s, p, r);
        if (s.containerWidth > 0 && s.containerHeight > 0) {
            for (var q = 0; q < s._UIManager.aryImages.length; q++) {
                s.showImage(q)
            }
        }
    };

    function f(s, q) {
        var u = this,
            r = q || {},
            p, t = '<div class="D_ImageUIEditor noPaddingnoMarginInside thinborder"></div>';
        n.init(u, s, r);
        u.onMouseMove = r.onMouseMove || false;
        u.onImageAreaSelected = r.onImageAreaSelected || false;
        u.onImageAreaDeSelected = r.onImageAreaDeSelected || false;
        if (Dynamsoft.Lib.env.bFirefox) {
            d.one(t).addClass("relativePositioning")
        }
        u.DWObject = s._stwain;
        p = u.container;
        p.append(t);
        u.dialog = null;
        u.UIEditor = p.one(".D_ImageUIEditor");
        u.bShow = false;
        u.defaultImageUrl = r.defaultImageUrl;
        u.mode = "initial";
        u.originalMode = u.mode;
        u.originalModeBeforeFullScreen = u.mode;
        u.isfullscreen = false;
        u.isFitWindow = false;
        u.cIndex = -1;
        u.totalImagesCount = 0;
        u.zoom = 1;
        u.zoomInStep = 1.2;
        u.zoomOutStep = 0.8;
        u.aspectRatio = 1;
        u.__fitWindowType = 0;
        u.overlayInfo = {
            reApply: false,
            exist: false,
            count: 0,
            info: []
        };
        u.outEditorStatus = {
            width: 0,
            height: 0,
            maxwide: false,
            maxhigh: false,
            resize: false
        };
        u.Image = {
            url: u.defaultImageUrl,
            urlPrefix: u.defaultImageUrl,
            width: 480,
            height: 590,
            imgAspectRatio: 590 / 480,
            act: 0,
            changed: false,
            obj: false
        };
        u.toolbar = {
            self: null,
            filled: false,
            width: 0,
            height: 0
        };
        u.scrollbar = {
            self: null,
            bgcolor: "#f0f0f0",
            rectcolor: "#ddd",
            dragging: false,
            mousePos: {
                zero_x: 0,
                zero_y: 0,
                x: 0,
                y: 0
            },
            mouseStartDrag: {
                y: 0,
                scrollY: 0,
                cIndex: -1
            },
            glider: {
                color: "#bbb",
                hovercolor: "#aaa",
                dragcolor: "#999",
                height: 0
            },
            width: 0,
            height: 0,
            Canvas: {
                self: null,
                ctx: null
            }
        };
        u.canvasDIV = {
            self: null,
            width: 0,
            height: 0,
            overflow: false,
            scrollTop_zero: 0,
            scrollLeft_zero: 0,
            baseCanvas: {
                self: null,
                ctx: null,
                backgroundColor: "#FFFFFF",
                drawArea: {
                    width: 0,
                    height: 0,
                    baseCoordinates: {
                        x: 0,
                        y: 0
                    }
                }
            },
            upperCanvas: {
                self: null,
                ctx: null,
                MouseShape: false,
                drawingRect: false,
                mouse_zero_x: 0,
                mouse_zero_y: 0,
                mouse_x: 0,
                mouse_y: 0
            },
            selectedCanvasArea: {
                top: 0,
                right: 0,
                bottom: 0,
                left: 0
            },
            selectedImageArea: {
                top: 0,
                right: 0,
                bottom: 0,
                left: 0
            }
        };
        u.changeMode("view");
        window.onresize = function() {
            if (u.bShow && u.mode != "fullscreen") {
                if (u.outEditorStatus.maxhigh && u.outEditorStatus.maxwide) {
                    u.outEditorStatus.resize = true;
                    u.ShowImageEditorEx(-1, -1, -1, -1, 1)
                } else {
                    u.outEditorStatus.resize = false
                }
            } else {
                return
            }
        };
        Dynamsoft.Lib.addEventListener(l.documentElement, "fullscreenchange", function() {
            if (!document.fullscreen) {
                u.exitFullScreen()
            }
        }, false);
        if (Dynamsoft.Lib.env.bChrome) {
            Dynamsoft.Lib.addEventListener(l.documentElement, "webkitfullscreenchange", function() {
                if (!document.webkitIsFullScreen) {
                    u.exitFullScreen()
                }
            }, false)
        } else {
            if (Dynamsoft.Lib.env.bFirefox) {
                Dynamsoft.Lib.addEventListener(l.documentElement, "mozfullscreenchange", function() {
                    if (!document.mozFullScreen) {
                        u.exitFullScreen()
                    }
                }, false)
            } else {
                if (Dynamsoft.Lib.env.bIE) {
                    Dynamsoft.Lib.addEventListener(l.documentElement, "msfullscreenchange", function() {
                        if (!document.msFullScreen) {
                            u.exitFullScreen()
                        }
                    }, false)
                }
            }
        }
    }
    f.prototype.unregistEvents = function() {
        var q = this,
            p = i.indexOf(q);
        if (p >= 0) {
            i.splice(p, 1)
        }
    };
    f.prototype.handlerKeyDown = function(q) {
        var r = this,
            p = true;
        if (r.bShow) {
            switch (q.keyCode) {
                case 37:
                    if (r.canvasDIV.upperCanvas.MouseShape) {
                        p = false;
                        if (r.canvasDIV.overflow) {
                            if (r.canvasDIV.self.scrollLeft >= 10) {
                                r.canvasDIV.self.scrollLeft -= 10
                            }
                        }
                    }
                    break;
                case 38:
                    if (r.canvasDIV.upperCanvas.MouseShape) {
                        p = false;
                        if (r.canvasDIV.overflow) {
                            if (r.canvasDIV.self.scrollTop >= 10) {
                                r.canvasDIV.self.scrollTop -= 10
                            }
                        }
                    }
                    break;
                case 39:
                    if (r.canvasDIV.upperCanvas.MouseShape) {
                        p = false;
                        if (r.canvasDIV.overflow) {
                            if (r.canvasDIV.self.scrollLeft <= r.canvasDIV.self.scrollWidth - 10) {
                                r.canvasDIV.self.scrollLeft += 10
                            }
                        }
                    }
                    break;
                case 40:
                    if (r.canvasDIV.upperCanvas.MouseShape) {
                        p = false;
                        if (r.canvasDIV.overflow) {
                            if (r.canvasDIV.self.scrollTop <= r.canvasDIV.self.scrollHeight - 10) {
                                r.canvasDIV.self.scrollTop += 10
                            }
                        }
                    }
                    break;
                case 107:
                    p = false;
                    r.ZoomIn();
                    break;
                case 109:
                    p = false;
                    r.ZoomOut();
                    break;
                case 45:
                    p = false;
                    if (r.canvasDIV.upperCanvas.MouseShape) {
                        r.setMouseShape(false)
                    } else {
                        r.setMouseShape(true)
                    }
                    break
            }
        }
        return p
    };
    f.prototype.getFitWindowType = function() {
        var p = this;
        return p.__fitWindowType
    };
    f.prototype.setFitWindowType = function(q) {
        var r = this,
            p = 1 * q;
        r.__fitWindowType = p;
        switch (p) {
            case 0:
                r.FitsWindowSize();
                break;
            case 1:
                r.FitsWindowHeight();
                break;
            case 2:
                r.FitsWindowWidth();
                break;
            case 3:
                r.OriginalSize();
                break
        }
    };
    f.prototype.setBackgroundColor = function(q) {
        var r = this,
            p = q;
        if (d.isNumber(p)) {
            p = Dynamsoft.Lib.getColor(p)
        }
        r.backgroundColor = p;
        r.canvasDIV.baseCanvas.backgroundColor = p;
        r.drawImageOnBaseCanvas()
    };
    f.prototype.getZoom = function() {
        var p = this;
        return p.zoom
    };
    f.prototype.setZoom = function(p) {
        var q = this;
        q.zoom = p;
        q.updateMode()
    };
    f.prototype.getMouseShape = function() {
        var p = this;
        return p.canvasDIV.upperCanvas.MouseShape
    };
    f.prototype.setMouseShape = function(p) {
        var q = this;
        if (!q.bShow) {
            n.output(q, "You can not set mouseshape under view mode.");
            return
        }
        if (typeof p == "boolean") {
            q.canvasDIV.upperCanvas.MouseShape = p;
            n.output(q, "MouseShaped is changed to " + p.toString());
            if (q.canvasDIV.upperCanvas.MouseShape) {
                q.canvasDIV.upperCanvas.self.style.cursor = "pointer";
                if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_hand")[0]) {
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_hand")[0].style.display = "none";
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_rectselect")[0].style.display = ""
                }
                q.SetSelectedImageArea(0, 0, 0, 0)
            } else {
                if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_hand")[0]) {
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_hand")[0].style.display = "";
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_rectselect")[0].style.display = "none"
                }
                q.canvasDIV.upperCanvas.self.style.cursor = "crosshair"
            }
        } else {
            n.output(q, 'You need to use "ture" or "false" to set MouseShape.')
        }
    };
    f.prototype.HideImageEditor = function() {
        var p = this;
        Dynamsoft.Lib.hide("J_ImgSizeEditor");
        Dynamsoft.Lib.hide("J_RotateAnyAngle");
        if (p.Image.changed) {
            p.ShowDialogForSaveImage(-1);
            return
        }
        p.__HideImageEditorInner()
    };
    f.prototype.__HideImageEditorInner = function() {
        var q = this;
        if (q.mode == "fullscreen") {
            q.exitFullScreen();
            return
        }
        q.width = q.containerWidth;
        q.height = q.containerHeight;
        q.outEditorStatus.maxwide = false;
        q.outEditorStatus.maxhigh = false;
        var p = function(r) {
            r.UIEditor.css({
                width: r.width + "px",
                height: r.height + "px",
                position: ""
            });
            r.isFitWindow = true;
            r.container.style("z-index", "");
            r.changeMode(r.originalMode)
        };
        if (q.originalMode == "view" || q.originalMode == "initial") {
            q.UIEditor.style("none");
            q.bShow = false;
            p(q)
        } else {
            q.UIEditor.style("");
            q.bShow = true;
            if (q.Image.width == 0 || q.Image.height == 0) {
                q.__refreshImageSize(p)
            } else {
                p(q)
            }
        }
    };
    f.prototype.__refreshImageSize = function(q) {
        var p = this;
        p.Image.width = p.DWObject.GetImageWidth(p.cIndex);
        p.Image.height = p.DWObject.GetImageHeight(p.cIndex);
        p.Image.imgAspectRatio = (p.Image.width == 0) ? 1 : p.Image.height / p.Image.width;
        if (n.isFunction(q)) {
            q(p)
        }
    };
    f.prototype.ShowImageEditorEx = function(q, u, p, t, r) {
        var s = this;
        if (r == 4) {
            if (s.mode == "editor_out") {
                return true
            } else {
                return false
            }
        }
        if (r == 1) {
            p = -1;
            t = -1
        } else {
            if (r == 2) {
                s.UIEditor.style("display", "");
                s.SetViewMode(-2, -2);
                return
            } else {
                if (r == 3) {
                    s.HideImageEditor();
                    return
                } else {
                    s.container.style("z-index", "999")
                }
            }
        }
        if (p == -1) {
            if (s.outEditorStatus.resize && s.outEditorStatus.maxwide) {
                p = window.innerWidth
            } else {
                if (!s.outEditorStatus.resize) {
                    s.outEditorStatus.maxwide = true;
                    p = window.innerWidth
                }
            }
        }
        if (t == -1) {
            if (s.outEditorStatus.resize && s.outEditorStatus.maxhigh) {
                t = window.innerHeight
            } else {
                if (!s.outEditorStatus.resize) {
                    t = window.innerHeight;
                    s.outEditorStatus.maxhigh = true
                }
            }
        }
        if (q == -1) {
            q = (window.innerWidth - p) / 2
        }
        if (u == -1) {
            u = (window.innerHeight - t) / 2
        }
        if (s.mode != "editor_out" || s.outEditorStatus.resize) {
            s.width = p - 2;
            s.height = t - 2;
            s.outEditorStatus.width = p;
            s.outEditorStatus.height = t
        } else {
            n.output(s, "The editor is already shown");
            return
        }
        s.UIEditor.css({
            display: "",
            width: s.width + "px",
            height: s.height + "px",
            position: "fixed",
            left: q,
            top: u
        });
        s.isFitWindow = true;
        s.changeMode("editor_out")
    };
    f.prototype.fire = function(r, s, q, p) {
        var t = this;
        if (n.isFunction(t[r])) {
            return t[r](s, q, p)
        }
        return true
    };
    f.prototype.SetViewMode = function(q, p) {
        var r = this;
        if (q == -1 && p == -1) {
            r.changeMode("edit")
        } else {
            if (q == 1 && p == 1) {
                r.changeMode("multi_view");
                r.updateScrollBar()
            } else {
                if (q == -2 && p == -2) {
                    r.changeMode("editor")
                }
            }
        }
    };
    f.prototype.changeMode = function(r, p) {
        var s = this;
        n.output(s, "changeMode to: " + r);
        if (r == "view" || r == "multi_view" || r == "edit" || r == "editor" || r == "fullscreen" || r == "editor_out") {
            if (s.mode != r || s.outEditorStatus.resize) {
                s.outEditorStatus.resize = false;
                if (s.mode != "editor_out" && s.mode != "editor" && s.mode != "fullscreen") {
                    s.originalMode = s.mode
                }
                s.mode = r;
                if (r == "view") {
                    s._UIManager.getView().show();
                    s.UIEditor.hide()
                } else {
                    s._UIManager.getView().hide();
                    s.UIEditor.show()
                }
            } else {
                if (!p) {
                    n.output(s, "The current mode is already " + r);
                    return
                }
            }
        } else {
            n.output(s, 'The parameter "newMode" in the function changeMode(newMode) is empty or incorrect, please check.');
            return
        }
        var q = false;
        document.body.style.overflow = "auto";
        switch (s.mode) {
            case "view":
                break;
            case "multi_view":
                q = true;
                s.isFitWindow = true;
            case "edit":
                if (s.originalMode == "view" || s.originalMode == "multi_view") {
                    s.isFitWindow = true
                }
                s.toolbar.width = 0;
                s.toolbar.height = 0;
                if (q) {
                    s.scrollbar.width = c;
                    s.scrollbar.height = s.height - s.toolbar.height;
                    if (s.scrollbar.height < 0) {
                        s.scrollbar.height = 0
                    }
                } else {
                    s.scrollbar.width = 0;
                    s.scrollbar.height = 0
                }
                s.width = s.containerWidth - 2;
                s.canvasDIV.width = s.width;
                s.canvasDIV.height = s.height - s.toolbar.height - 2;
                break;
            case "editor_out":
                document.body.style.overflow = "hidden";
            case "editor":
                if (s.toolbar.filled) {
                    s.toolbar.filled = false
                }
                s.toolbar.width = s.width;
                s.toolbar.height = Math.floor(s.height / 20 < 20 ? 20 : s.height / 20);
                s.scrollbar.width = 0;
                s.scrollbar.height = 0;
                s.canvasDIV.width = s.width;
                s.canvasDIV.height = s.height - s.toolbar.height;
                break;
            case "fullscreen":
                if (s.toolbar.filled) {
                    s.toolbar.filled = false
                }
                document.body.style.overflow = "hidden";
                s.toolbar.width = screen.width;
                s.toolbar.height = Math.floor(screen.height / 20 < 20 ? 20 : screen.height / 20);
                s.scrollbar.width = 0;
                s.scrollbar.height = 0;
                s.canvasDIV.width = screen.width;
                s.canvasDIV.height = screen.height;
                break;
            default:
                break
        }
        if (s.canvasDIV.width < 0) {
            s.canvasDIV.width = 0
        }
        if (s.canvasDIV.height < 0) {
            s.canvasDIV.height = 0
        }
        if (s.canvasDIV.width - c > 0) {
            s.aspectRatio = s.canvasDIV.height / (s.canvasDIV.width - c)
        } else {
            s.aspectRatio = 1
        }
        this.updateEditor()
    };
    f.prototype.updateEditor = function() {
        var u = this;
        n.output(u, 'Editor is under mode "' + u.mode + '"');
        if (u.toolbar.self) {
            if (!u.bShow) {
                return
            }
            if (u.toolbar.width + u.toolbar.height == 0) {
                n.addClass(u.toolbar.self, "notdisplayed");
                if (Dynamsoft.Lib.env.bFirefox) {
                    u.canvasDIV.upperCanvas.self.style.top = "0"
                }
            } else {
                if (Dynamsoft.Lib.env.bFirefox) {
                    if (u.toolbar.height != 0 && u.mode != "fullscreen") {
                        u.canvasDIV.upperCanvas.self.style.top = u.toolbar.height + "px"
                    } else {
                        u.canvasDIV.upperCanvas.self.style.top = "0"
                    }
                }
                u.toolbar.self.style.width = u.toolbar.width + "px";
                u.toolbar.self.style.height = u.toolbar.height + "px";
                if (!u.toolbar.filled) {
                    u.toolbar.filled = true;
                    while (u.toolbar.self.firstChild) {
                        u.toolbar.self.removeChild(u.toolbar.self.firstChild)
                    }
                    u.toolbar.self.appendChild(n.getToolBar(u, u.toolbar.width, u.toolbar.height));
                    if (u.Image.changed) {
                        document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0].style.display = "";
                        document.getElementsByClassName("Class_D_DWT_Editor_Buttons_restore")[0].style.display = ""
                    }
                }
                if (u.mode == "fullscreen") {
                    n.addClass(u.toolbar.self, "notdisplayed")
                } else {
                    n.removeClass(u.toolbar.self, "notdisplayed");
                    n.removeClass(u.toolbar.self, "overlay")
                }
            }
            if (u.scrollbar.width == 0) {
                u.scrollbar.self.style.display = "none"
            } else {
                u.scrollbar.self.style.width = u.scrollbar.width + "px";
                u.scrollbar.self.style.height = u.scrollbar.height + "px";
                u.scrollbar.self.style.display = "";
                u.restoreCanvas(u.scrollbar.Canvas.ctx, u.scrollbar.width, u.scrollbar.height)
            }
            u.canvasDIV.self.style.width = u.canvasDIV.width + "px";
            u.canvasDIV.self.style.height = u.canvasDIV.height + "px";
            u.restoreCanvas(u.canvasDIV.baseCanvas.ctx, u.canvasDIV.width, u.canvasDIV.height);
            u.restoreCanvas(u.canvasDIV.upperCanvas.ctx, u.canvasDIV.width, u.canvasDIV.height);
            u.updateMode()
        } else {
            var r = (Math.floor(Math.random() * 1000 + 1)).toString();
            var s, q, t, v, p, w, x = ["D_ImageUIEditor", "noPaddingnoMarginInside", "thinborder"];
            s = document.createElement("div");
            s.id = "ds-dwt-viewerToolbar" + r;
            s.style.width = u.toolbar.width + "px";
            s.style.height = u.toolbar.height + "px";
            s.style.whiteSpace = "normal";
            s.className = "ds-dwt-viewerToolbar";
            q = document.createElement("div");
            q.id = "ds-dwt-imageViewer" + r;
            q.style.width = u.canvasDIV.width + "px";
            q.style.height = u.canvasDIV.height + "px";
            q.className = "floatLeft ds-dwt-imageViewer";
            v = document.createElement("canvas");
            if (Dynamsoft.Lib.env.bFirefox) {
                v.style.left = "0";
                v.style.top = "0"
            }
            v.id = "ds-dwt-editorCover" + r;
            v.className = "ds-dwt-editorCover";
            p = document.createElement("canvas");
            p.id = "ds-dwt-editorBase" + r;
            p.className = "ds-dwt-editorBase";
            q.appendChild(v);
            q.appendChild(p);
            t = d.DOM.create("<div>", {
                css: {
                    width: u.scrollbar.width + "px",
                    height: u.scrollbar.height + "px",
                    position: "absolute",
                    right: 0,
                    top: 0,
                    "z-index": 1,
                    cursor: "default",
                    "text-align": "center"
                }
            });
            w = document.createElement("canvas");
            w.id = "ds-dwt-scollerBar" + r;
            t.appendChild(w);
            u.container.append(t);
            u.UIEditor.css({
                width: u.width + "px",
                height: u.height + "px"
            });
            u.UIEditor.append(s);
            u.UIEditor.append(q);
            if (Dynamsoft.Lib.env.bFirefox) {
                x.push("relativePositioning")
            }
            u.UIEditor.attr("class", x.join(" "));
            u.toolbar.self = document.getElementById("ds-dwt-viewerToolbar" + r);
            u.canvasDIV.self = document.getElementById("ds-dwt-imageViewer" + r);
            u.canvasDIV.self.style.backgroundColor = u.canvasDIV.baseCanvas.backgroundColor;
            u.canvasDIV.baseCanvas.self = document.getElementById("ds-dwt-editorBase" + r);
            u.canvasDIV.baseCanvas.ctx = u.canvasDIV.baseCanvas.self.getContext("2d");
            u.canvasDIV.upperCanvas.self = document.getElementById("ds-dwt-editorCover" + r);
            u.canvasDIV.upperCanvas.ctx = u.canvasDIV.upperCanvas.self.getContext("2d");
            u.scrollbar.self = t;
            u.scrollbar.Canvas.self = document.getElementById("ds-dwt-scollerBar" + r);
            u.scrollbar.Canvas.ctx = u.scrollbar.Canvas.self.getContext("2d");
            u.bindEventsToCanvas();
            u.updateEditor()
        }
    };
    f.prototype.bindEventsToCanvas = function() {
        var q = this,
            p;
        k.on(q.canvasDIV.self, "dblclick", function(s) {
            var r = q.cIndex;
            q.fire("onMouseDoubleClick", r)
        });
        k.on(q.canvasDIV.self, "contextmenu", function(s) {
            var r = q.fire("onMouseRightClick", q.cIndex);
            return r
        });
        k.on(q.canvasDIV.self, "click", function(s) {
            var r = q.cIndex;
            return q.fire("onMouseClick", r)
        });
        Dynamsoft.Lib.addEventListener(q.canvasDIV.self, "mousedown", function(s) {
            q.canvasDIV.upperCanvas.drawingRect = true;
            var r = n.getOffset(s);
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                r.x += window.pageXOffset;
                r.y += window.pageYOffset
            }
            q.canvasDIV.upperCanvas.mouse_zero_x = r.x;
            q.canvasDIV.upperCanvas.mouse_zero_y = r.y;
            if (q.canvasDIV.upperCanvas.mouse_zero_x - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) < 0) {
                q.canvasDIV.upperCanvas.mouse_zero_x = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x)
            }
            if (q.canvasDIV.upperCanvas.mouse_zero_y - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) < 0) {
                q.canvasDIV.upperCanvas.mouse_zero_y = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y)
            }
            if (q.canvasDIV.upperCanvas.mouse_zero_x + parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) > q.canvasDIV.upperCanvas.ctx.canvas.width) {
                q.canvasDIV.upperCanvas.mouse_zero_x = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) + q.canvasDIV.baseCanvas.drawArea.width
            }
            if (q.canvasDIV.upperCanvas.mouse_zero_y + parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) > q.canvasDIV.upperCanvas.ctx.canvas.height) {
                q.canvasDIV.upperCanvas.mouse_zero_y = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) + q.canvasDIV.baseCanvas.drawArea.height
            }
        });
        Dynamsoft.Lib.addEventListener(q.canvasDIV.self, "mousemove", function(w) {
            var u = n.getOffset(w);
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                u.x += window.pageXOffset;
                u.y += window.pageYOffset
            }
            q.canvasDIV.upperCanvas.mouse_x = u.x;
            q.canvasDIV.upperCanvas.mouse_y = u.y;
            if (q.mode == "fullscreen") {
                if (u.y < 10 && u.y > 5) {
                    n.addClass(q.toolbar.self, "overlay");
                    n.removeClass(q.toolbar.self, "notdisplayed")
                } else {
                    n.addClass(q.toolbar.self, "notdisplayed");
                    n.removeClass(q.toolbar.self, "overlay")
                }
            }
            if (q.canvasDIV.upperCanvas.drawingRect) {
                if (q.canvasDIV.upperCanvas.mouse_x - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) < 0) {
                    q.canvasDIV.upperCanvas.mouse_x = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x)
                }
                if (q.canvasDIV.upperCanvas.mouse_y - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) < 0) {
                    q.canvasDIV.upperCanvas.mouse_y = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y)
                }
                if (q.canvasDIV.upperCanvas.mouse_x + parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) > q.canvasDIV.upperCanvas.ctx.canvas.width) {
                    q.canvasDIV.upperCanvas.mouse_x = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x) + q.canvasDIV.baseCanvas.drawArea.width
                }
                if (q.canvasDIV.upperCanvas.mouse_y + parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) > q.canvasDIV.upperCanvas.ctx.canvas.height) {
                    q.canvasDIV.upperCanvas.mouse_y = parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y) + q.canvasDIV.baseCanvas.drawArea.height
                }
                q.fire("onMouseMove", q.cIndex, {
                    x: q.canvasDIV.upperCanvas.mouse_x,
                    y: q.canvasDIV.upperCanvas.mouse_y
                });
                q.canvasDIV.selectedCanvasArea.left = q.canvasDIV.upperCanvas.mouse_zero_x;
                q.canvasDIV.selectedCanvasArea.right = q.canvasDIV.upperCanvas.mouse_x;
                q.canvasDIV.selectedCanvasArea.top = q.canvasDIV.upperCanvas.mouse_zero_y;
                q.canvasDIV.selectedCanvasArea.bottom = q.canvasDIV.upperCanvas.mouse_y;
                if (q.canvasDIV.upperCanvas.MouseShape) {
                    if (q.canvasDIV.overflow) {
                        q.canvasDIV.self.scrollLeft = q.canvasDIV.scrollLeft_zero + (q.canvasDIV.selectedCanvasArea.left - q.canvasDIV.selectedCanvasArea.right) / q.zoom;
                        q.canvasDIV.self.scrollTop = q.canvasDIV.scrollTop_zero + (q.canvasDIV.selectedCanvasArea.top - q.canvasDIV.selectedCanvasArea.bottom) / q.zoom
                    }
                } else {
                    var v, x, y, s;
                    v = (q.canvasDIV.self.scrollLeft + q.canvasDIV.selectedCanvasArea.left - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x));
                    x = (q.canvasDIV.self.scrollTop + q.canvasDIV.selectedCanvasArea.top - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y));
                    y = (q.canvasDIV.self.scrollLeft + q.canvasDIV.selectedCanvasArea.right - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.x));
                    s = (q.canvasDIV.self.scrollTop + q.canvasDIV.selectedCanvasArea.bottom - parseInt(q.canvasDIV.baseCanvas.drawArea.baseCoordinates.y));
                    q.SetSelectedImageArea(v / q.zoom, x / q.zoom, y / q.zoom, s / q.zoom)
                }
            }
        });
        Dynamsoft.Lib.addEventListener(q.canvasDIV.self, "mouseleave", function(r) {
            q.canvasDIV.upperCanvas.drawingRect = false
        });
        Dynamsoft.Lib.addEventListener(q.canvasDIV.self, "mouseup", function(A) {
            q.canvasDIV.upperCanvas.drawingRect = false;
            q.canvasDIV.scrollLeft_zero = q.canvasDIV.self.scrollLeft;
            q.canvasDIV.scrollTop_zero = q.canvasDIV.self.scrollTop;
            var B = n.getOffset(A);
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                B.x += window.pageXOffset;
                B.y += window.pageYOffset
            }
            q.canvasDIV.upperCanvas.mouse_x = B.x;
            q.canvasDIV.upperCanvas.mouse_y = B.y;
            var z = q.canvasDIV.upperCanvas.mouse_x;
            var v = q.canvasDIV.upperCanvas.mouse_y;
            var u = q.canvasDIV.selectedCanvasArea.right - q.canvasDIV.selectedCanvasArea.left;
            var s = q.canvasDIV.selectedCanvasArea.bottom - q.canvasDIV.selectedCanvasArea.top;
            if (Math.abs(u) + Math.abs(s) < 10) {
                q.SetSelectedImageArea(0, 0, 0, 0);
                return
            }
            if (u < 0) {
                var C = q.canvasDIV.selectedCanvasArea.right;
                var r = q.canvasDIV.selectedCanvasArea.left
            } else {
                var C = q.canvasDIV.selectedCanvasArea.left;
                var r = q.canvasDIV.selectedCanvasArea.right
            }
            if (s < 0) {
                var t = q.canvasDIV.selectedCanvasArea.bottom;
                var w = q.canvasDIV.selectedCanvasArea.top
            } else {
                var t = q.canvasDIV.selectedCanvasArea.top;
                var w = q.canvasDIV.selectedCanvasArea.bottom
            }
            if (!(z < C) && !(z > r) && !(v < t) && !(v > w)) {} else {
                q.SetSelectedImageArea(0, 0, 0, 0)
            }
        });
        Dynamsoft.Lib.addEventListener(q.canvasDIV.self, "dblclick", function(r) {
            q.canvasDIV.upperCanvas.mouse_x = (parseInt(r.pageX) - parseInt(q.canvasDIV.upperCanvas.self.offsetLeft));
            q.canvasDIV.upperCanvas.mouse_y = (parseInt(r.pageY) - parseInt(q.canvasDIV.upperCanvas.self.offsetTop))
        });
        Dynamsoft.Lib.addEventListener(q.scrollbar.self, "mouseover", function(s) {
            if (q.scrollbar.dragging) {
                return
            }
            var r = n.getOffset(s),
                t = false,
                v, u;
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                r.x += window.pageXOffset;
                r.y += window.pageYOffset
            }
            v = n.caculateSliderHeight(q);
            u = q.cIndex * v;
            if (r.y < u) {} else {
                if (r.y > (u + q.scrollbar.glider.height)) {} else {
                    q.scrollbar.mousePos.x = r.x;
                    q.scrollbar.mousePos.y = r.y;
                    q.updateScrollBar("mouseover")
                }
            }
        });
        Dynamsoft.Lib.addEventListener(q.scrollbar.Canvas.self, "mousedown", function(s) {
            var r = n.getOffset(s),
                t = false,
                v, u;
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                r.x += window.pageXOffset;
                r.y += window.pageYOffset
            }
            v = n.caculateSliderHeight(q);
            u = q.cIndex * v;
            q.scrollbar.mouseStartDrag.cIndex = q.cIndex;
            if (r.y < u) {
                q.cIndex--;
                if (q.cIndex < 0) {
                    q.cIndex = 0
                }
            } else {
                if (r.y > (u + q.scrollbar.glider.height)) {
                    q.cIndex++;
                    if (q.cIndex >= q.totalImagesCount) {
                        q.cIndex = q.totalImagesCount - 1
                    }
                } else {
                    t = true
                }
            }
            if (t) {
                q.scrollbar.dragging = true;
                q.scrollbar.mousePos.zero_x = r.x;
                q.scrollbar.mousePos.zero_y = r.y;
                q.scrollbar.mousePos.x = r.x;
                q.scrollbar.mousePos.y = r.y;
                q.scrollbar.mouseStartDrag.y = r.y;
                q.scrollbar.self.style.width = (c * 5) + "px";
                q.scrollbar.self.style.right = -(c * 2) + "px";
                q.updateScrollBar("mousedown")
            } else {}
        });
        Dynamsoft.Lib.addEventListener(q.scrollbar.self, "mousemove", function(s) {
            if (q.scrollbar.dragging) {
                var r = n.getOffset(s);
                if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                    r.x += window.pageXOffset;
                    r.y += window.pageYOffset
                }
                q.scrollbar.mousePos.x = r.x;
                q.scrollbar.mousePos.y = r.y;
                q.updateScrollBar("mousemove")
            }
        });
        k.on(q.scrollbar.self, "mouseleave", function(r) {
            var s = q.scrollbar.mouseStartDrag.cIndex;
            q.scrollbar.dragging = false;
            q.scrollbar.self.style.width = c + "px";
            q.scrollbar.self.style.right = "0";
            if (s != -1 && s != q.cIndex) {
                q.go()
            }
            q.scrollbar.mousePos.zero_x = 0;
            q.scrollbar.mousePos.zero_y = 0;
            q.scrollbar.mouseStartDrag.scrollY = 0;
            q.scrollbar.mouseStartDrag.cIndex = -1;
            q.scrollbar.mousePos.x = 0;
            q.scrollbar.mousePos.y = 0;
            q.updateScrollBar("mouseleave")
        });
        Dynamsoft.Lib.addEventListener(q.scrollbar.self, "mouseup", function(s) {
            var t = q.scrollbar.mouseStartDrag.cIndex,
                r = n.getOffset(s);
            q.scrollbar.dragging = false;
            q.scrollbar.mouseStartDrag.cIndex = -1;
            q.scrollbar.self.style.width = c + "px";
            q.scrollbar.self.style.right = "0";
            if (Dynamsoft.Lib.env.bFirefox && q.mode != "fullscreen" && q.mode != "editor_out") {
                r.x += window.pageXOffset;
                r.y += window.pageYOffset
            }
            q.scrollbar.mousePos.zero_x = 0;
            q.scrollbar.mousePos.zero_y = 0;
            q.scrollbar.mouseStartDrag.scrollY = 0;
            q.scrollbar.mousePos.x = r.x;
            q.scrollbar.mousePos.y = r.y;
            q.updateScrollBar("mouseup");
            if (t != q.cIndex) {
                q.go()
            }
        })
    };
    f.prototype.SetSelectedImageArea = function(v, B, C, q) {
        var y = this,
            z, s;
        y.canvasDIV.selectedImageArea.left = v;
        y.canvasDIV.selectedImageArea.top = B;
        y.canvasDIV.selectedImageArea.right = C;
        y.canvasDIV.selectedImageArea.bottom = q;
        y.canvasDIV.selectedCanvasArea.left = v * y.zoom - y.canvasDIV.self.scrollLeft + parseInt(y.canvasDIV.baseCanvas.drawArea.baseCoordinates.x);
        y.canvasDIV.selectedCanvasArea.top = B * y.zoom - y.canvasDIV.self.scrollTop + parseInt(y.canvasDIV.baseCanvas.drawArea.baseCoordinates.y);
        y.canvasDIV.selectedCanvasArea.right = C * y.zoom - y.canvasDIV.self.scrollLeft + parseInt(y.canvasDIV.baseCanvas.drawArea.baseCoordinates.x);
        y.canvasDIV.selectedCanvasArea.bottom = q * y.zoom - y.canvasDIV.self.scrollTop + parseInt(y.canvasDIV.baseCanvas.drawArea.baseCoordinates.y);
        z = y.canvasDIV.selectedCanvasArea.right - y.canvasDIV.selectedCanvasArea.left;
        s = y.canvasDIV.selectedCanvasArea.bottom - y.canvasDIV.selectedCanvasArea.top;
        if (v - q == 0 || B - q == 0) {
            z = 0;
            s = 0
        }
        y.restoreCanvas(y.canvasDIV.upperCanvas.ctx, y.canvasDIV.upperCanvas.ctx.canvas.width, y.canvasDIV.upperCanvas.ctx.canvas.height);
        if (Math.abs(z) + Math.abs(s) > 10) {
            var w, D, p, A, u = d.one(".Class_D_DWT_Editor_Buttons_crop");
            if (u && u[0]) {
                var x = u[0].src;
                x = x.replace("crop_grey.", "crop.");
                u[0].src = x;
                u.style("cursor", "pointer")
            }
            y.canvasDIV.upperCanvas.ctx.dashedLineRect(y.canvasDIV.selectedCanvasArea.left, y.canvasDIV.selectedCanvasArea.top, z, s);
            if (C < v && q < B) {
                w = C;
                D = q;
                p = v;
                A = B
            } else {
                p = C;
                A = q;
                w = v;
                D = B
            }
            y.fire("onImageAreaSelected", {
                cIndex: y.cIndex,
                left: w,
                top: D,
                right: p,
                bottom: A
            })
        } else {
            var u = d.one(".Class_D_DWT_Editor_Buttons_crop");
            if (u && u[0]) {
                var x = u[0].src;
                x = x.replace("crop.", "crop_grey.");
                u[0].src = x;
                u.style("cursor", "auto")
            }
            y.fire("onImageAreaDeSelected", y.cIndex)
        }
    };
    f.prototype.exitFullScreen = function() {
        var p = this;
        if (p.isfullscreen) {
            p.isfullscreen = false;
            if (document.exitFullscreen) {
                document.exitFullscreen()
            } else {
                if (document.msExitFullscreen) {
                    document.msExitFullscreen()
                } else {
                    if (document.mozCancelFullScreen) {
                        document.mozCancelFullScreen()
                    } else {
                        if (document.webkitExitFullscreen) {
                            document.webkitExitFullscreen()
                        }
                    }
                }
            }
            if (typeof p.originalModeBeforeFullScreen === "undefined") {
                p.originalModeBeforeFullScreen = "view"
            }
            p.changeMode(p.originalModeBeforeFullScreen)
        }
    };
    f.prototype.fullscreen = function() {
        var q = this;
        if (!q.bShow || q.mode == "fullscreen" || q.mode == "editor") {
            return
        }
        q.originalModeBeforeFullScreen = q.mode;
        q.isfullscreen = true;
        var p = q.canvasDIV.self.parentElement;
        if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
            if (p.requestFullscreen) {
                p.requestFullscreen()
            } else {
                if (p.msRequestFullscreen) {
                    p.msRequestFullscreen()
                } else {
                    if (p.mozRequestFullScreen) {
                        p.mozRequestFullScreen()
                    } else {
                        if (p.webkitRequestFullscreen) {
                            p.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT)
                        }
                    }
                }
            }
        } else {
            if (document.exitFullscreen) {
                document.exitFullscreen()
            } else {
                if (document.msExitFullscreen) {
                    document.msExitFullscreen()
                } else {
                    if (document.mozCancelFullScreen) {
                        document.mozCancelFullScreen()
                    } else {
                        if (document.webkitExitFullscreen) {
                            document.webkitExitFullscreen()
                        }
                    }
                }
            }
        }
        q.changeMode("fullscreen")
    };
    f.prototype.previous_btn = function() {
        var q = this,
            p;
        if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0]) {
            p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
            if (p.indexOf("grey") != -1) {
                return
            } else {
                q.previous()
            }
        }
    };
    f.prototype.next_btn = function() {
        var q = this,
            p;
        if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0]) {
            p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
            if (p.indexOf("grey") != -1) {
                return
            } else {
                q.next()
            }
        }
    };
    f.prototype.previous = function() {
        var p = this;
        p.go(p.cIndex - 1)
    };
    f.prototype.next = function() {
        var p = this;
        p.go(p.cIndex + 1)
    };
    f.prototype.go = function(p) {
        var q = this;
        if (q.Image.changed) {
            q.ShowDialogForSaveImage(p);
            return
        }
        q.__goInner(p)
    };
    f.prototype.__goInner = function(q) {
        var r = this;
        if (!d.isUndefined(q)) {
            if (q < 0 || q >= r.totalImagesCount) {
                return
            }
            r.cIndex = q
        }
        if (r.cIndex == 0) {
            if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0]) {
                var p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
                p = p.replace("previous.", "previous_grey.");
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src = p;
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].style.cursor = "auto";
                if (r.totalImagesCount > 1) {
                    p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
                    p = p.replace("next_grey.", "next.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src = p;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].style.cursor = "pointer"
                }
            }
        } else {
            if (r.cIndex == r.totalImagesCount - 1) {
                if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0]) {
                    var p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
                    p = p.replace("next.", "next_grey.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src = p;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].style.cursor = "auto";
                    if (r.totalImagesCount > 1) {
                        p = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
                        p = p.replace("previous_grey.", "previous.");
                        document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src = p;
                        document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].style.cursor = "pointer"
                    }
                }
            }
        }
        if (r.Image.changed) {
            return
        }
        r.refresh();
        r.fire("onRefreshUI", r.cIndex)
    };
    f.prototype.show = function() {
        var p = this;
        p.bShow = true;
        p.UIEditor.style("display", "");
        if (p.mode == "edit") {
            p.scrollbar.self.style.display = "none"
        } else {
            p.scrollbar.self.style.display = ""
        }
        return true
    };
    f.prototype.hide = function() {
        var p = this;
        p.bShow = false;
        p.mode = "view";
        p.originalMode = "view";
        p.UIEditor.style("display", "none");
        p.scrollbar.self.style.display = "none";
        return true
    };
    f.prototype.clear = function() {
        var p = this;
        p.cIndex = -1;
        p.refresh()
    };
    f.prototype.refresh = function(s, p, r) {
        var t = this;
        t.SetSelectedImageArea(0, 0, 0, 0);
        if (typeof s === "undefined") {
            s = t.cIndex
        } else {
            s = parseInt(s);
            if (p) {
                t.totalImagesCount = p
            }
            if (s >= t._UIManager.aryImages.length) {
                s = -1
            }
            t.cIndex = s;
            if (t.mode == "multi_view") {
                t.updateScrollBar()
            }
        }
        if (r && document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0]) {
            if (t.mode == "editor" || t.mode == "editor_out") {
                t.Image.changed = true
            }
            document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0].style.display = "";
            document.getElementsByClassName("Class_D_DWT_Editor_Buttons_restore")[0].style.display = ""
        }
        if (t.cIndex == -1) {
            t.totalImagesCount = 0;
            t.Image.url = t.defaultImageUrl;
            t.Image.urlPrefix = t.defaultImageUrl;
            t.Image.height = 350;
            t.Image.width = 270;
            t.Image.imgAspectRatio = 350 / 270;
            if (t.bShow) {
                t.updateMode()
            }
        } else {
            var q = t._UIManager.aryImages[s];
            if (q) {
                t.Image.urlPrefix = q.urlPrefix;
                t.Image.height = 0;
                t.Image.width = 0;
                t.Image.url = t.__getUrlByAct(0);
                if (t.bShow) {
                    t.__refreshImageSize(function(v) {
                        if (parseInt(v.Image.width * v.zoom) > m || parseInt(v.Image.height * v.zoom) > m) {
                            n.output(v, "You have reached the limit of the canvas.");
                            var u = v.Image.width > v.Image.height ? v.Image.width : v.Image.height;
                            if (u == 0) {
                                v.zoom = 1
                            } else {
                                v.zoom = m / u
                            }
                        }
                        v.updateMode()
                    })
                }
            }
        }
    };
    f.prototype.restoreScrollbar = function() {
        var p = this;
        p.scrollbar.Canvas.ctx.clearRect(0, 0, p.scrollbar.width + 2, p.scrollbar.height);
        p.scrollbar.Canvas.ctx.fillStyle = p.scrollbar.bgcolor;
        p.scrollbar.Canvas.ctx.fillRect(0, 0, p.scrollbar.width + 2, p.scrollbar.height);
        p.scrollbar.Canvas.ctx.strokeStyle = p.scrollbar.rectcolor;
        p.scrollbar.Canvas.ctx.rect(0, 0, p.scrollbar.width + 2, p.scrollbar.height - 1);
        p.scrollbar.Canvas.ctx.stroke()
    };
    f.prototype.updateScrollBar = function(r) {
        var v = this,
            p = 0,
            u, t, q = r || "";
        if (v.totalImagesCount > 1) {
            v.scrollbar.Canvas.self.parentNode.style.display = "";
            t = n.caculateSliderHeight(v);
            if (q == "mousedown" && v.scrollbar.dragging) {
                u = v.cIndex * t;
                v.scrollbar.mouseStartDrag.scrollY = u
            } else {
                if (q == "mousemove" && v.scrollbar.dragging) {
                    u = (v.scrollbar.mousePos.y - v.scrollbar.mouseStartDrag.y + v.scrollbar.mouseStartDrag.scrollY);
                    if (u + v.scrollbar.glider.height > v.scrollbar.height) {
                        u = v.scrollbar.height - v.scrollbar.glider.height
                    } else {
                        if (u < 0) {
                            u = 0
                        }
                    }
                    v.cIndex = Math.round(u / t);
                    if (v.cIndex > v.totalImagesCount - 1) {
                        v.cIndex = v.totalImagesCount - 1
                    }
                } else {
                    u = v.cIndex * t
                }
            }
            var s = v.scrollbar.mousePos.y;
            v.restoreScrollbar();
            v.scrollbar.Canvas.ctx.fillStyle = v.scrollbar.glider.color;
            if (v.cIndex * v.scrollbar.glider.height < s && s < v.cIndex * v.scrollbar.glider.height + v.scrollbar.glider.height) {
                if (q == "mouseover") {
                    v.scrollbar.Canvas.ctx.fillStyle = v.scrollbar.glider.hovercolor
                } else {
                    if (q == "mousedown" || q == "mousemove") {
                        v.scrollbar.Canvas.ctx.fillStyle = v.scrollbar.glider.dragcolor
                    }
                }
            }
            v.scrollbar.Canvas.ctx.fillRect(2, u + 1, v.scrollbar.width - 2, v.scrollbar.glider.height)
        } else {
            v.scrollbar.Canvas.self.parentNode.style.display = "none"
        }
    };
    f.prototype.updateMode = function(p) {
        var s = this,
            r;
        if (s.totalImagesCount > 1) {
            if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0]) {
                if (s.cIndex == s.totalImagesCount - 1) {
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
                    r = r.replace("next.", "next_grey.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].style.cursor = "auto"
                } else {
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
                    r = r.replace("next_grey.", "next.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].style.cursor = "pointer"
                }
                if (s.cIndex == 0) {
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
                    r = r.replace("previous.", "previous_grey.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].style.cursor = "auto"
                } else {
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
                    r = r.replace("previous_grey.", "previous.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].style.cursor = "pointer"
                }
            }
        } else {
            if (s.mode == "multi_view") {} else {
                if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0]) {
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src;
                    r = r.replace("next.", "next_grey.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_next")[0].style.cursor = "auto";
                    r = document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src;
                    r = r.replace("previous.", "previous_grey.");
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].src = r;
                    document.getElementsByClassName("Class_D_DWT_Editor_Buttons_previous")[0].style.cursor = "auto"
                }
            }
        }
        switch (s.mode) {
            case "view":
                break;
            case "multi_view":
                s.updateScrollBar();
            default:
                s.setMouseShape(s.canvasDIV.upperCanvas.MouseShape);
                if (s.cIndex == -1) {
                    s.drawImageOnBaseCanvas();
                    break
                }
                s.SetSelectedImageArea(0, 0, 0, 0);
                var q = function(t) {
                    if (s.isFitWindow) {
                        t.FitsWindowSize()
                    } else {
                        t.canvasDIV.baseCanvas.drawArea.width = t.Image.width * t.zoom;
                        t.canvasDIV.baseCanvas.drawArea.height = t.Image.height * t.zoom;
                        t.drawImageOnBaseCanvas(p)
                    }
                };
                if (s.Image.width == 0 || s.Image.height == 0) {
                    s.__refreshImageSize(q)
                } else {
                    q(s)
                }
                break
        }
    };
    f.prototype.print = function() {
        var p = this;
        if (p.cIndex < 0) {
            return
        }
        n.print(p.Image.url, p.Image.width, p.Image.height)
    };
    f.prototype.getMousePosition = function() {
        var q = this;
        n.output(q, "getMousePosition");
        var p = {
            x: q.canvasDIV.upperCanvas.mouse_x,
            y: q.canvasDIV.upperCanvas.mouse_y
        };
        return p
    };
    f.prototype.restoreCanvas = function(q, p, r) {
        var s = this;
        q.canvas.width = p;
        q.canvas.height = r
    };
    f.prototype.drawImageOnBaseCanvas = function(w) {
        var t = this,
            z = false,
            q = false,
            u = 0,
            r = 0;
        t.restoreCanvas(t.canvasDIV.baseCanvas.ctx, t.canvasDIV.width, t.canvasDIV.height);
        if (t.cIndex == -1) {
            t.canvasDIV.baseCanvas.ctx.clearRect(0, 0, t.canvasDIV.width, t.canvasDIV.height);
            n.removeClass(t.canvasDIV.self, "allowOverFlow");
            n.output(t, "no image to draw");
            return
        }
        t.canvasDIV.baseCanvas.drawArea.baseCoordinates.x = 0;
        t.canvasDIV.baseCanvas.drawArea.baseCoordinates.y = 0;
        n.removeClass(t.canvasDIV.self, "allowOverFlow");
        if (t.canvasDIV.baseCanvas.drawArea.width > t.canvasDIV.width || t.canvasDIV.baseCanvas.drawArea.height > t.canvasDIV.height) {
            t.canvasDIV.overflow = true
        } else {
            t.canvasDIV.overflow = false
        }
        if (t.canvasDIV.baseCanvas.drawArea.width > t.canvasDIV.width) {
            z = true;
            n.addClass(t.canvasDIV.self, "allowOverFlowX");
            t.canvasDIV.baseCanvas.ctx.canvas.width = t.canvasDIV.baseCanvas.drawArea.width;
            if (t.canvasDIV.baseCanvas.drawArea.height <= t.canvasDIV.height) {
                r = t.canvasDIV.self.offsetHeight - t.canvasDIV.self.clientHeight;
                if (t.canvasDIV.baseCanvas.drawArea.height == t.canvasDIV.height) {
                    t.canvasDIV.baseCanvas.drawArea.height = t.canvasDIV.height - r;
                    t.canvasDIV.baseCanvas.drawArea.width = parseInt(t.canvasDIV.baseCanvas.drawArea.height / t.Image.imgAspectRatio)
                }
                t.restoreCanvas(t.canvasDIV.baseCanvas.ctx, t.canvasDIV.baseCanvas.drawArea.width, t.canvasDIV.height - r);
                t.restoreCanvas(t.canvasDIV.upperCanvas.ctx, t.canvasDIV.width, t.canvasDIV.height - r)
            }
        } else {
            n.removeClass(t.canvasDIV.self, "allowOverFlowX")
        }
        if (t.canvasDIV.baseCanvas.drawArea.height > t.canvasDIV.height) {
            q = true;
            n.addClass(t.canvasDIV.self, "allowOverFlowY");
            t.canvasDIV.baseCanvas.ctx.canvas.height = t.canvasDIV.baseCanvas.drawArea.height;
            if (t.canvasDIV.baseCanvas.drawArea.width <= t.canvasDIV.width) {
                u = t.canvasDIV.self.offsetWidth - t.canvasDIV.self.clientWidth;
                if (t.canvasDIV.baseCanvas.drawArea.width == t.canvasDIV.width) {
                    t.canvasDIV.baseCanvas.drawArea.width = t.canvasDIV.width - u;
                    t.canvasDIV.baseCanvas.drawArea.height = parseInt(t.canvasDIV.baseCanvas.drawArea.width * t.Image.imgAspectRatio)
                }
                t.restoreCanvas(t.canvasDIV.baseCanvas.ctx, t.canvasDIV.width - u, t.canvasDIV.baseCanvas.drawArea.height);
                t.restoreCanvas(t.canvasDIV.upperCanvas.ctx, t.canvasDIV.width - u, t.canvasDIV.height)
            }
        } else {
            n.removeClass(t.canvasDIV.self, "allowOverFlowY")
        }
        if (z && q) {
            n.removeClass(t.canvasDIV.self, "allowOverFlowX");
            n.removeClass(t.canvasDIV.self, "allowOverFlowY");
            n.addClass(t.canvasDIV.self, "allowOverFlow");
            u = t.canvasDIV.self.offsetWidth - t.canvasDIV.self.clientWidth;
            r = t.canvasDIV.self.offsetHeight - t.canvasDIV.self.clientHeight;
            t.restoreCanvas(t.canvasDIV.upperCanvas.ctx, t.canvasDIV.width - u, t.canvasDIV.height - r);
            t.restoreCanvas(t.canvasDIV.baseCanvas.ctx, t.canvasDIV.baseCanvas.drawArea.width, t.canvasDIV.baseCanvas.drawArea.height)
        } else {
            if (!(z || q)) {
                n.removeClass(t.canvasDIV.self, "allowOverFlowX");
                n.removeClass(t.canvasDIV.self, "allowOverFlowY");
                t.restoreCanvas(t.canvasDIV.baseCanvas.ctx, t.canvasDIV.width, t.canvasDIV.height);
                t.restoreCanvas(t.canvasDIV.upperCanvas.ctx, t.canvasDIV.width, t.canvasDIV.height)
            }
        }
        t.canvasDIV.baseCanvas.ctx.fillStyle = t.canvasDIV.baseCanvas.backgroundColor;
        t.canvasDIV.baseCanvas.ctx.fillRect(0, 0, t.canvasDIV.baseCanvas.ctx.canvas.width, t.canvasDIV.baseCanvas.ctx.canvas.height);
        t.canvasDIV.baseCanvas.ctx.font = "12px Helvetica";
        t.canvasDIV.baseCanvas.ctx.textAlign = "center";
        t.canvasDIV.baseCanvas.ctx.strokeText("Loading ...", t.canvasDIV.baseCanvas.ctx.canvas.width / 2, t.canvasDIV.baseCanvas.ctx.canvas.height / 2);
        if (t.canvasDIV.width - t.canvasDIV.baseCanvas.drawArea.width - u > 0) {
            t.canvasDIV.baseCanvas.drawArea.baseCoordinates.x = (t.canvasDIV.width - t.canvasDIV.baseCanvas.drawArea.width - u) / 2
        }
        if (t.canvasDIV.height - t.canvasDIV.baseCanvas.drawArea.height - r > 0) {
            t.canvasDIV.baseCanvas.drawArea.baseCoordinates.y = (t.canvasDIV.height - t.canvasDIV.baseCanvas.drawArea.height - r) / 2
        }
        var x, v = function() {
            t.canvasDIV.baseCanvas.ctx.clearRect(0, 0, t.canvasDIV.width, t.canvasDIV.height);
            if (t.cIndex == -1) {
                return
            }
            t.canvasDIV.baseCanvas.ctx.drawImage(x, t.canvasDIV.baseCanvas.drawArea.baseCoordinates.x, t.canvasDIV.baseCanvas.drawArea.baseCoordinates.y, t.canvasDIV.baseCanvas.drawArea.width, t.canvasDIV.baseCanvas.drawArea.height);
            t.overlayInfo.reApply = true;
            t.ReApplyOverlayRectangle();
            t.overlayInfo.reApply = false;
            x.src = "";
            x = null
        };
        if (w) {
            x = w;
            v();
            x = null
        } else {
            var p = false;
            x = new Image();
            x.onload = v;
            if (t.mode == "multi_view") {
                var y, s;
                if (t.cIndex < t._UIManager.aryImages.length && t.cIndex >= 0) {
                    y = t._UIManager.aryImages[t.cIndex];
                    if (y) {
                        s = [y.urlPrefix];
                        s.push("&index=");
                        s.push(t.cIndex);
                        s.push("&width=");
                        s.push(t.width);
                        s.push("&height=");
                        s.push(t.height);
                        s.push("&ticks=");
                        s.push(y.ticks);
                        p = s.join("")
                    }
                }
            } else {
                p = t.Image.url
            }
            if (p) {
                x.src = p
            }
        }
    };
    f.prototype.StrechMode = function() {
        var q = this,
            p;
        p = function(t) {
            t.canvasDIV.baseCanvas.drawArea.width = parseInt(t.canvasDIV.baseCanvas.ctx.canvas.width);
            t.canvasDIV.baseCanvas.drawArea.height = parseInt(t.canvasDIV.baseCanvas.ctx.canvas.height);
            if (t.Image.width == 0 || t.Image.height == 0) {
                t.zoom = 1
            } else {
                var s, r;
                s = t.canvasDIV.baseCanvas.drawArea.width / t.Image.width;
                r = t.canvasDIV.baseCanvas.drawArea.height / t.Image.height;
                if (s < r) {
                    t.zoom = s
                } else {
                    t.zoom = r
                }
            }
            t.drawImageOnBaseCanvas();
            t.isFitWindow = true
        };
        if (q.Image.width == 0 || q.Image.height == 0) {
            q.__refreshImageSize(p)
        } else {
            p(q)
        }
    };
    f.prototype.FitsWindowSize = function() {
        var q = this,
            p;
        p = function(r) {
            r.isFitWindow = true;
            if (r.aspectRatio < r.Image.imgAspectRatio) {
                r.FitsWindowHeight(true)
            } else {
                r.FitsWindowWidth(true)
            }
        };
        if (q.Image.width == 0 || q.Image.height == 0) {
            q.__refreshImageSize(p)
        } else {
            p(q)
        }
    };
    f.prototype.FitsWindowWidth = function(p) {
        var r = this,
            q;
        if (typeof p === "undefined" || !p) {
            if (r.mode == "view") {
                n.output(r, "Change to edit mode before using this function. Current modee is  " + r.mode);
                return
            }
        }
        q = function(u) {
            u.canvasDIV.baseCanvas.drawArea.width = u.canvasDIV.width - c;
            u.canvasDIV.baseCanvas.drawArea.height = parseInt(u.canvasDIV.baseCanvas.drawArea.width * u.Image.imgAspectRatio);
            if (u.canvasDIV.baseCanvas.drawArea.height > u.canvasDIV.height) {}
            if (u.Image.width == 0 || u.Image.height == 0 || u.Image.width < u.canvasDIV.baseCanvas.drawArea.width && u.Image.height < u.canvasDIV.baseCanvas.drawArea.height) {
                u.zoom = 1;
                r.canvasDIV.baseCanvas.drawArea.width = u.Image.width;
                r.canvasDIV.baseCanvas.drawArea.height = u.Image.height
            } else {
                var t, s;
                t = u.canvasDIV.baseCanvas.drawArea.width / u.Image.width;
                s = u.canvasDIV.baseCanvas.drawArea.height / u.Image.height;
                if (t < s) {
                    u.zoom = t
                } else {
                    u.zoom = s
                }
            }
            if (u.bShow) {
                u.drawImageOnBaseCanvas()
            }
        };
        if (r.Image.width == 0 || r.Image.height == 0) {
            r.__refreshImageSize(q)
        } else {
            q(r)
        }
    };
    f.prototype.FitsWindowHeight = function(p) {
        var r = this,
            q;
        if (typeof p === "undefined" || !p) {
            if (r.mode == "view") {
                n.output(r, "Change to edit mode before using this function. Current modee is  " + r.mode);
                return
            }
        }
        q = function(u) {
            u.canvasDIV.baseCanvas.drawArea.height = u.canvasDIV.height;
            u.canvasDIV.baseCanvas.drawArea.width = parseInt(u.canvasDIV.baseCanvas.drawArea.height / u.Image.imgAspectRatio);
            if (u.canvasDIV.baseCanvas.drawArea.width > u.canvasDIV.width) {}
            if (u.Image.width == 0 || u.Image.height == 0 || u.Image.width < u.canvasDIV.baseCanvas.drawArea.width && u.Image.height < u.canvasDIV.baseCanvas.drawArea.height) {
                u.zoom = 1;
                r.canvasDIV.baseCanvas.drawArea.width = u.Image.width;
                r.canvasDIV.baseCanvas.drawArea.height = u.Image.height
            } else {
                var t, s;
                t = u.canvasDIV.baseCanvas.drawArea.width / u.Image.width;
                s = u.canvasDIV.baseCanvas.drawArea.height / u.Image.height;
                if (t < s) {
                    u.zoom = t
                } else {
                    u.zoom = s
                }
            }
            if (u.bShow) {
                u.drawImageOnBaseCanvas()
            }
        };
        if (r.Image.width == 0 || r.Image.height == 0) {
            r.__refreshImageSize(q)
        } else {
            q(r)
        }
    };
    f.prototype.ZoomIn = function() {
        var q = this,
            p;
        if (q.mode != "view" && q.mode != "multi_view") {
            p = function(s) {
                var r = (Math.floor(s.zoom * 100 * s.zoomInStep) + 1);
                if (1 < r && r < 2501) {
                    if (parseInt(s.Image.width * s.zoom) > m || parseInt(s.Image.height * s.zoom) > m) {
                        n.output(s, "You have reached the limit for zooming in")
                    } else {
                        s.zoom = r / 100;
                        s.canvasDIV.baseCanvas.drawArea.width = parseInt(s.Image.width * s.zoom);
                        s.canvasDIV.baseCanvas.drawArea.height = parseInt(s.Image.height * s.zoom)
                    }
                } else {
                    n.output(s, "You have reached the limit for zooming in");
                    alert("You have reached the limit for zooming in")
                }
                s.drawImageOnBaseCanvas();
                s.isFitWindow = false
            };
            if (q.Image.width == 0 || q.Image.height == 0) {
                q.__refreshImageSize(p)
            } else {
                p(q)
            }
        } else {
            n.output(q, "Zoom is not allowed under the mode " + q.mode)
        }
    };
    f.prototype.ZoomOut = function() {
        var q = this,
            p;
        if (q.mode != "view" && q.mode != "multi_view") {
            p = function(s) {
                var r = (Math.floor(s.zoom * 100 * s.zoomOutStep) - 1);
                if (1 < r && r < 6501) {
                    s.zoom = r / 100;
                    s.canvasDIV.baseCanvas.drawArea.width = parseInt(s.Image.width * s.zoom);
                    s.canvasDIV.baseCanvas.drawArea.height = parseInt(s.Image.height * s.zoom)
                } else {
                    n.output(s, "You have reached the limit for zooming out")
                }
                s.drawImageOnBaseCanvas();
                s.isFitWindow = false
            };
            if (q.Image.width == 0 || q.Image.height == 0) {
                q.__refreshImageSize(p)
            } else {
                p(q)
            }
        } else {
            n.output(q, "Zoom is not allowed under the mode " + q.mode)
        }
    };
    f.prototype.OriginalSize = function() {
        var q = this,
            p;
        if (q.mode != "view") {
            p = function(r) {
                r.zoom = 1;
                r.canvasDIV.baseCanvas.drawArea.width = parseInt(r.Image.width);
                r.canvasDIV.baseCanvas.drawArea.height = parseInt(r.Image.height);
                if (r.bShow) {
                    r.drawImageOnBaseCanvas()
                }
                r.isFitWindow = false
            };
            if (q.Image.width == 0 || q.Image.height == 0) {
                q.__refreshImageSize(p)
            } else {
                p(q)
            }
        } else {
            n.output(q, "Change to -1*-1 view mode before using this function. Current modee is  " + q.mode)
        }
    };
    f.prototype.CopyToClipboard = function() {
        var p = this;
        p.DWObject.CopyToClipboard(p.cIndex)
    };
    f.prototype.__refreshImageSizeWhenChanged = function(s) {
        var r = this,
            p, q = function() {
                r.Image.obj = p;
                r.Image.width = p.width;
                r.Image.height = p.height;
                r.Image.imgAspectRatio = (r.Image.width == 0) ? 1 : r.Image.height / r.Image.width;
                if (n.isFunction(s)) {
                    s(r, p)
                }
                p.src = "";
                p = null
            };
        p = new Image();
        p.onload = q;
        p.src = r.Image.url
    };
    f.prototype.RestoreImage = function() {
        var q = this,
            p;
        if (q.cIndex == -1) {
            q.refresh();
            return
        }
        q.SetSelectedImageArea(0, 0, 0, 0);
        p = q.__getUrlByAct(1);
        q.Image.url = p;
        q.Image.height = 0;
        q.Image.width = 0;
        q.__refreshImageSizeWhenChanged(function(t, s) {
            var r = t._UIManager.get(t.cIndex);
            r.ticks = Math.floor(Math.random() * 100000 + 1);
            t.Image.url = t.__getUrlByAct(0);
            t.Image.changed = false;
            if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0]) {
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0].style.display = "none";
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_restore")[0].style.display = "none"
            }
            if (t.bShow) {
                t.updateMode(s)
            }
            t.fire("onRefreshUI", t.cIndex)
        })
    };
    f.prototype.SaveImage = function() {
        var q = this,
            p;
        if (q.cIndex == -1) {
            q.refresh();
            return
        }
        if (!q.Image.changed) {
            alert("You have not changed the current image");
            return
        }
        q.SetSelectedImageArea(0, 0, 0, 0);
        p = q.__getUrlByAct(2);
        q.Image.url = p;
        q.Image.height = 0;
        q.Image.width = 0;
        q.__refreshImageSizeWhenChanged(function(t, s) {
            var r = t._UIManager.get(t.cIndex);
            r.ticks = Math.floor(Math.random() * 100000 + 1);
            t.Image.url = t.__getUrlByAct(0);
            t.Image.changed = false;
            if (document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0]) {
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_save")[0].style.display = "none";
                document.getElementsByClassName("Class_D_DWT_Editor_Buttons_restore")[0].style.display = "none"
            }
            if (t.bShow) {
                t.updateMode(s)
            }
            t.fire("onRefreshUI", t.cIndex)
        })
    };
    f.prototype.__getUrlByAct = function(p) {
        var s = this,
            q = s._UIManager.get(s.cIndex),
            r = [q.urlPrefix];
        r.push("&index=");
        r.push(s.cIndex);
        if (p) {
            r.push("&act=");
            r.push(p)
        } else {
            r.push("&width=-1");
            r.push("&height=-1")
        }
        r.push("&ticks=");
        r.push(q.ticks);
        return r.join("")
    };
    f.prototype.Erase = function(s, r, q, p) {
        var t = this;
        n.output(t, "Erase");
        if (typeof s === "undefined" || typeof r === "undefined" || typeof q === "undefined" || typeof p === "undefined") {
            n.output(t, "Not enough paramters")
        } else {
            t.canvasDIV.selectedImageArea.left = parseInt(s) ? parseInt(s) : 0;
            t.canvasDIV.selectedImageArea.top = parseInt(r) ? parseInt(r) : 0;
            t.canvasDIV.selectedImageArea.right = parseInt(q) ? parseInt(q) : 0;
            t.canvasDIV.selectedImageArea.bottom = parseInt(p) ? parseInt(p) : 0
        }
        if (t.canvasDIV.selectedImageArea.left - t.canvasDIV.selectedImageArea.right != 0 && t.canvasDIV.selectedImageArea.top - t.canvasDIV.selectedImageArea.bottom != 0) {
            t.DWObject.Erase(t.cIndex, t.canvasDIV.selectedImageArea.left, t.canvasDIV.selectedImageArea.top, t.canvasDIV.selectedImageArea.right, t.canvasDIV.selectedImageArea.bottom)
        } else {
            n.output(t, "No area selected for erasing");
            return
        }
    };
    f.prototype.Rotate = function(p, q) {
        var r = this;
        if (typeof p === "undefined" || typeof q === "undefined") {
            alert("Not enough paramters")
        } else {
            r.DWObject.Rotate(r.cIndex, p, q)
        }
    };
    f.prototype.RotateEx = function(p, r, q) {
        var s = this;
        if (typeof p === "undefined" || typeof r === "undefined" || typeof q === "undefined") {
            alert("Not enough paramters")
        } else {
            s.DWObject.RotateEx(s.cIndex, p, r, q)
        }
    };
    f.prototype.RotateLeft = function() {
        var p = this;
        p.DWObject.RotateLeft(p.cIndex)
    };
    f.prototype.RotateAnyAngle = function(t) {
        var v = this;
        if (!Dynamsoft.Lib.get("J_RotateAnyAngle")) {
            var s = ['<div id="J_RotateAnyAngle" class="ds-dwt-imgSizeEditor" style="display:none">', "<ul>", '<li><label for="J_Angle"><b>Angle :</b>', '<input type="text" id="J_Angle" style="width:50%;" size="10"/></label></li>', '<li><label for="J_Angle_InterpolationMethod"><b>Interpolation:</b>&nbsp;', '<select size="1" id="J_Angle_InterpolationMethod"><option value = ""></option></select></li>', '<li><label for="J_KeepSize"><b></b>', '<input type="checkbox" id="J_KeepSize"/>Keep size</label></li>', "</ul>", "<div>", '<input id="J_btnRotateAnyAngleOK" type="button" value="  OK  "/>', '<span><input id= "J_btnCancelRotateAnyAngle" type="button" value="Cancel" /></span>', "</div>", "</div>"];
            KISSY.one("body").append(s.join(""))
        }
        var r = a.get("J_Angle_InterpolationMethod");
        r.options.length = 0;
        r.options.add(new Option("NearestNeighbor", 1));
        r.options.add(new Option("Bilinear", 2));
        r.options.add(new Option("Bicubic", 3));
        var p = a.get("J_btnRotateAnyAngleOK");
        p.onclick = function() {
            var z = a.get("J_Angle").value;
            a.get("J_Angle").className = "";
            re = /^\d+$/;
            if (!re.test(z) || z <= 0) {
                a.get("J_Angle").className += " invalid";
                a.get("J_Angle").focus();
                alert("Error: The angle you entered is invalid.");
                return
            }
            var y = a.get("J_KeepSize");
            var x = y.checked;
            var w = v.RotateEx(z, x, a.get("J_Angle_InterpolationMethod").selectedIndex + 1);
            if (v.DWObject.ErrorCode == 0) {
                Dynamsoft.Lib.hide("J_RotateAnyAngle");
                return
            }
        };
        var q = a.get("J_btnCancelRotateAnyAngle");
        q.onclick = function() {
            Dynamsoft.Lib.hide("J_RotateAnyAngle")
        };
        var u = a.get("J_RotateAnyAngle");
        Dynamsoft.Lib.toggle("J_RotateAnyAngle");
        u.style.top = t.y + t.offsetHeight + "px";
        u.style.left = t.x + "px";
        a.get("J_Angle").value = "45"
    };
    f.prototype.RotateRight = function() {
        var p = this;
        p.DWObject.RotateRight(p.cIndex)
    };
    f.prototype.Deskew = function() {
        var p = this;
        p.RotateEx(p.DWObject.GetSkewAngle(p.cIndex), true, 1)
    };
    f.prototype.Flip = function() {
        var p = this;
        p.DWObject.Flip(p.cIndex)
    };
    f.prototype.Mirror = function() {
        var p = this;
        p.DWObject.Mirror(p.cIndex)
    };
    f.prototype.ChangeImageSize = function(q, p, r) {
        var s = this;
        s.DWObject.ChangeImageSize(s.cIndex, q, p, r)
    };
    f.prototype.ChangeImageSizeGetinput = function(r) {
        var v = this;
        if (!Dynamsoft.Lib.get("J_ImgSizeEditor")) {
            var q = ['<div id="J_ImgSizeEditor" class="ds-dwt-imgSizeEditor" style="display:none">', "<ul>", '<li><label for="J_img_height"><b>New Height :</b>', '<input type="text" id="J_img_height" style="width:50%;" size="10"/>pixel</label></li>', '<li><label for="J_img_width"><b>New Width :</b>&nbsp;', '<input type="text" id="J_img_width" style="width:50%;" size="10"/>pixel</label></li>', "<li>Interpolation method:", '<select size="1" id="J_InterpolationMethod"><option value = ""></option></select></li>', "</ul>", "<div>", '<input id="J_btnChangeImageSizeOK" type="button" value="  OK  "/>', '<span><input id= "J_btnCancelChange" type="button" value="Cancel" /></span>', "</div>", "</div>"];
            KISSY.one("body").append(q.join(""))
        }
        var p = a.get("J_InterpolationMethod");
        p.options.length = 0;
        p.options.add(new Option("NearestNeighbor", 1));
        p.options.add(new Option("Bilinear", 2));
        p.options.add(new Option("Bicubic", 3));
        var t = a.get("J_btnChangeImageSizeOK");
        t.onclick = function() {
            var y = a.get("J_img_width").value,
                w = a.get("J_img_height").value;
            a.get("J_img_width").className = "";
            a.get("J_img_height").className = "";
            re = /^\d+$/;
            if (!re.test(w) || w <= 0) {
                a.get("J_img_height").className += " invalid";
                a.get("J_img_height").focus();
                alert("Error: The height you entered is invalid.");
                return
            }
            if (!re.test(y) || y <= 0) {
                a.get("J_img_width").className += " invalid";
                a.get("J_img_width").focus();
                alert("Error: The width you entered is invalid.");
                return
            }
            var x = v.ChangeImageSize(y, w, a.get("J_InterpolationMethod").selectedIndex + 1);
            if (v.DWObject.ErrorCode == 0) {
                Dynamsoft.Lib.hide("J_ImgSizeEditor");
                return
            }
        };
        var u = a.get("J_btnCancelChange");
        u.onclick = function() {
            Dynamsoft.Lib.hide("J_ImgSizeEditor")
        };
        var s = a.get("J_ImgSizeEditor");
        Dynamsoft.Lib.toggle("J_ImgSizeEditor");
        s.style.top = r.y + r.offsetHeight + "px";
        s.style.left = r.x + "px";
        a.get("J_img_width").value = v.Image.width;
        a.get("J_img_height").value = v.Image.height;
        return false
    };
    f.prototype.Crop_btn = function() {
        var r = this,
            p, q = d.one(".Class_D_DWT_Editor_Buttons_crop");
        if (q && q[0]) {
            p = q[0].src;
            if (p.indexOf("grey") != -1) {
                return
            } else {
                r.Crop()
            }
        }
    };
    f.prototype.Crop = function(s, r, q, p) {
        var t = this;
        if (typeof s === "undefined" || typeof r === "undefined" || typeof q === "undefined" || typeof p === "undefined") {
            n.output(t, "Not enough paramters")
        } else {
            t.canvasDIV.selectedImageArea.left = parseInt(s) ? parseInt(s) : 0;
            t.canvasDIV.selectedImageArea.top = parseInt(r) ? parseInt(r) : 0;
            t.canvasDIV.selectedImageArea.right = parseInt(q) ? parseInt(q) : 0;
            t.canvasDIV.selectedImageArea.bottom = parseInt(p) ? parseInt(p) : 0
        }
        if (t.canvasDIV.selectedImageArea.left - t.canvasDIV.selectedImageArea.right != 0 && t.canvasDIV.selectedImageArea.top - t.canvasDIV.selectedImageArea.bottom != 0) {
            t.DWObject.Crop(t.cIndex, t.canvasDIV.selectedImageArea.left, t.canvasDIV.selectedImageArea.top, t.canvasDIV.selectedImageArea.right, t.canvasDIV.selectedImageArea.bottom)
        } else {
            n.output(t, "No area selected for cropping");
            return
        }
    };
    f.prototype.CropToClipboard = function(s, r, q, p) {
        var t = this;
        if (typeof s === "undefined" || typeof r === "undefined" || typeof q === "undefined" || typeof p === "undefined") {
            n.output(t, "Not enough paramters")
        } else {
            t.canvasDIV.selectedImageArea.left = parseInt(s) ? parseInt(s) : 0;
            t.canvasDIV.selectedImageArea.top = parseInt(r) ? parseInt(r) : 0;
            t.canvasDIV.selectedImageArea.right = parseInt(q) ? parseInt(q) : 0;
            t.canvasDIV.selectedImageArea.bottom = parseInt(p) ? parseInt(p) : 0
        }
        if (t.canvasDIV.selectedImageArea.left - t.canvasDIV.selectedImageArea.right != 0 && t.canvasDIV.selectedImageArea.top - t.canvasDIV.selectedImageArea.bottom != 0) {
            t.DWObject.CropToClipboard(t.cIndex, t.canvasDIV.selectedImageArea.left, t.canvasDIV.selectedImageArea.top, t.canvasDIV.selectedImageArea.right, t.canvasDIV.selectedImageArea.bottom)
        } else {
            n.output(t, "No area selected for cropping");
            return
        }
    };
    f.prototype.CutFrameToClipboard = function(s, r, q, p) {
        var t = this;
        if (typeof s === "undefined" || typeof r === "undefined" || typeof q === "undefined" || typeof p === "undefined") {
            n.output(t, "Not enough paramters")
        } else {
            t.canvasDIV.selectedImageArea.left = parseInt(s) ? parseInt(s) : 0;
            t.canvasDIV.selectedImageArea.top = parseInt(r) ? parseInt(r) : 0;
            t.canvasDIV.selectedImageArea.right = parseInt(q) ? parseInt(q) : 0;
            t.canvasDIV.selectedImageArea.bottom = parseInt(p) ? parseInt(p) : 0
        }
        if (t.canvasDIV.selectedImageArea.left - t.canvasDIV.selectedImageArea.right != 0 && t.canvasDIV.selectedImageArea.top - t.canvasDIV.selectedImageArea.bottom != 0) {
            t.DWObject.CutFrameToClipboard(t.cIndex, t.canvasDIV.selectedImageArea.left, t.canvasDIV.selectedImageArea.top, t.canvasDIV.selectedImageArea.right, t.canvasDIV.selectedImageArea.bottom)
        } else {
            n.output(t, "No area selected for cutting");
            return
        }
    };
    f.prototype.CutToClipboard = function() {
        var p = this;
        p.DWObject.CutToClipboard(p.cIndex)
    };
    f.prototype.OverlayRectangle = function(v, t, s, z, q, p, r) {
        var u = this;
        if (v && v == -1) {
            var x = u.overlayInfo.info;
            while (x.length) {
                x.pop()
            }
            u.overlayInfo.count = 0;
            u.overlayInfo.exist = false;
            return true
        }
        if (v === "undefined" || !u.bShow || u.totalImagesCount < 1) {
            return false
        }
        if (!u.overlayInfo.reApply) {
            var y = {
                index: v,
                l: t,
                t: s,
                r: z,
                b: q,
                c: p,
                o: r
            };
            u.overlayInfo.info.push(y);
            u.overlayInfo.count = u.overlayInfo.info.length;
            if (!u.overlayInfo.exist && u.overlayInfo.count > 0) {
                u.overlayInfo.exist = true
            }
        }
        if (v == u.cIndex) {
            if (t < 0) {
                t = 0
            }
            if (s < 0) {
                s = 0
            }
            if (z > u.Image.width) {
                z = u.Image.width
            }
            if (q > u.Image.height) {
                q = u.Image.height
            }
            t *= u.zoom;
            s *= u.zoom;
            z *= u.zoom;
            q *= u.zoom;
            var w = {
                width: z - t,
                height: q - s
            };
            if (d.isNumber(p)) {
                p = Dynamsoft.Lib.getColor(p)
            }
            if (p.toLowerCase().indexOf("0x") == 0) {
                p = "#" + p.substring(2)
            }
            u.canvasDIV.baseCanvas.ctx.fillStyle = p;
            u.canvasDIV.baseCanvas.ctx.globalAlpha = parseFloat(r);
            u.canvasDIV.baseCanvas.ctx.fillRect(u.canvasDIV.baseCanvas.drawArea.baseCoordinates.x + t, u.canvasDIV.baseCanvas.drawArea.baseCoordinates.y + s, w.width, w.height);
            u.canvasDIV.baseCanvas.ctx.globalAlpha = 1
        }
        return true;
        n.output(u, "OverlayRectangle")
    };
    f.prototype.ReApplyOverlayRectangle = function() {
        var s = this;
        if (!s.overlayInfo.exist) {
            return false
        } else {
            var r = s.overlayInfo.count;
            for (var p = 0; p < r; p++) {
                var q = s.overlayInfo.info[p];
                if (s.cIndex == q.index) {
                    s.OverlayRectangle(q.index, q.l, q.t, q.r, q.b, q.c, q.o)
                }
            }
        }
    };
    f.prototype.SetDPI = function(r, s, p, q) {
        var t = this;
        n.output(t, "SetDPI")
    };
    f.prototype.AddText = function(p, w, t, s, u, r, q) {
        var v = this;
        n.output(v, "AddText")
    };
    f.prototype.CreateTextFont = function(p) {
        var q = this;
        n.output(q, "CreateTextFont")
    };
    f.prototype.ChangeSize = function(p, q) {
        var r = this;
        if (p >= 0) {
            r.containerWidth = p
        }
        if (q >= 0) {
            r.containerHeight = q
        }
        r.width = r.containerWidth - 2;
        if (r.width < 0) {
            r.width = 0
        }
        r.height = r.containerHeight - 2;
        if (r.height < 0) {
            r.height = 0
        }
        r.UIEditor.css({
            width: r.width + "px",
            height: r.height + "px"
        });
        r.changeMode(r.mode, true)
    };
    f.prototype.test = function(r) {
        var t = this,
            p = t._UIManager.aryImages[0].urlPrefix,
            q = [],
            s = 0;
        q.push(p);
        q.push("&index=0");
        q.push("&width=-1");
        q.push("&height=-1");
        q.push("&ticks=");
        s = Math.floor(Math.random() * 100000 + 1);
        q.push(s);
        console.log(r);
        t.Image.url = q.join("");
        t.drawImageOnBaseCanvas();
        if (!r) {
            r = 0
        }
        r++;
        if (r > 30) {
            return true
        }
        setTimeout(function() {
            t.test(r)
        }, 1000);
        return false
    };

    function o(q, r, p) {
        var s = this;
        s._stwain = q;
        s.selectedIndexes = [];
        s.aryImages = [];
        s._UIView = new Dynamsoft.Lib.UI.ImageUIView(s, r);
        s._UIEditor = new Dynamsoft.Lib.UI.ImageUIEditor(s, p);
        s._UIView.hide();
        s._UIEditor.show()
    }
    o.prototype.getView = function() {
        var p = this;
        return p._UIView
    };
    o.prototype.getEditor = function() {
        var p = this;
        return p._UIEditor
    };
    o.prototype.count = function() {
        var p = this;
        return p.aryImages.length
    };
    o.prototype.setBackgroundColor = function(p) {
        var q = this;
        q.getView().setBackgroundColor(p);
        q.getEditor().setBackgroundColor(p)
    };
    o.prototype.ChangeSize = function(p, q) {
        var r = this;
        r.getView().ChangeSize(p, q);
        r.getEditor().ChangeSize(p, q)
    };
    o.prototype.MoveImage = function(s, r) {
        var v = this,
            p;
        var q = s,
            u, t;
        t = v.aryImages[s];
        for (; q < v.aryImages.length; q++) {
            u = v.aryImages[q];
            u.src = n.replaceUrlByNewIndex(u.src, q - 1)
        }
        v.aryImages.splice(s, 1);
        q = r;
        for (; q < v.aryImages.length; q++) {
            u = v.aryImages[q];
            u.src = n.replaceUrlByNewIndex(u.src, q + 1)
        }
        v.aryImages.splice(r, 0, t);
        t.src = n.replaceUrlByNewIndex(t.src, r);
        v.getView().move(s, r);
        if (v._stwain.__cIndex == s || v._stwain.__cIndex == r) {
            v.getEditor().refresh(v._stwain.__cIndex, v._stwain._HowManyImagesInBuffer)
        }
    };
    o.prototype.SwitchImage = function(r, q) {
        var s = this,
            p;
        if (r < s.aryImages.length && r >= 0 && q < s.aryImages.length && q >= 0) {
            s.aryImages[r].width = s.aryImages[r].height = 0;
            s.aryImages[q].width = s.aryImages[q].height = 0;
            s.getView().SwitchImage(r, q);
            if (s._stwain.__cIndex == r || s._stwain.__cIndex == q) {
                s.getEditor().refresh(s._stwain.__cIndex, s._stwain._HowManyImagesInBuffer)
            }
        }
    };
    o.prototype.SetViewMode = function(s, q) {
        var t = this,
            r = false,
            p;
        if (s == -1 && q == -1) {
            r = true
        } else {
            if (s == 1 && q == 1) {
                r = true
            } else {
                r = false
            }
        }
        if (r) {
            t.getEditor().go(t._stwain.__cIndex);
            t.getEditor().show();
            t.getView().hide();
            p = t.getEditor().SetViewMode(s, q)
        } else {
            t.getEditor().hide();
            t.getView().show();
            p = t.getView().SetViewMode(s, q)
        }
        return p
    };
    o.prototype.add = function(s, q, r, t) {
        var w = this,
            u = parseInt(q);
        if (u < 0 || u > w.aryImages.length) {
            return false
        }
        if (t == 1) {
            w._stwain._UIManager.getView().add(s, u + 1);
            if (!w._stwain.__bLoadingImage) {
                w.getEditor().refresh(u, r)
            }
        } else {
            if (t == 2) {
                var p = u,
                    v;
                for (; p < w.aryImages.length; p++) {
                    v = w.aryImages[p];
                    v.src = n.replaceUrlByNewIndex(v.src, p + 1)
                }
                w.getView().add(s, u);
                if (!w._stwain.__bLoadingImage) {
                    w.getEditor().refresh(u, r)
                }
            } else {
                a.log("invalid parameter _op : " + _op)
            }
        }
    };
    o.prototype.set = function(q, p) {
        var s = this,
            r = parseInt(p);
        if (r < 0 || r > s.aryImages.length) {
            return false
        }
        s.aryImages[r] = q;
        s.getView().refreshIndex(r)
    };
    o.prototype.get = function(p) {
        var q = this;
        if (p < 0 || p > q.aryImages.length) {
            return false
        }
        return q.aryImages[p]
    };
    o.prototype.clear = function() {
        var p = this;
        p.selectedIndexes.splice(0);
        p.aryImages.splice(0);
        p.getView().clear();
        p.getEditor().clear()
    };
    o.prototype.remove = function(t, s) {
        var w = this,
            u = t * 1,
            p = s * 1,
            q = w.count();
        if (d.isUndefined(u) || u < 0) {
            return
        }
        if (q == 1) {
            w.clear();
            return
        }
        if (u >= q) {
            u = q - 1
        } else {
            var r = u,
                v;
            for (; r < w.aryImages.length; r++) {
                v = w.aryImages[r];
                v.src = n.replaceUrlByNewIndex(v.src, r - 1)
            }
        }
        w.aryImages.splice(u, 1);
        q--;
        w.selectedIndexes = [];
        if (p >= 0 && p < q) {
            w.selectedIndexes.push(p)
        } else {
            p = 0;
            w.selectedIndexes.push(0)
        }
        w.getView().remove(u, p)
    };
    f.prototype.ShowDialogForSaveImage = function(q) {
        var t = this;
        var r = ['<div id="J_waiting">', '<P style="white-space:nowrap;">', "You have changed the image, do you want to keep the change(s)?", "</P>", '<div  style="width: 200px;white-space:nowrap;margin-left: auto;margin-right: auto;">', '<input id="J_btnSave" style="width: 100px; height:30px;margin-right: 10px; margin-top: 10px;"  type="button" value="  OK  "/>', '<input id= "J_btnCancel" style="width: 100px; height:30px;"  type="button" value="  NO  " />', "</div>", "</div>"];
        Dynamsoft.WebTwainEnv.ShowDialog(422, 107, r.join(""), true);
        var p = a.get("J_btnSave");
        p.onclick = function() {
            t.SaveImage();
            Dynamsoft__OnclickCloseInstallEx();
            Dynamsoft.Lib.bChangeImage = 1;
            if (q == -1) {
                t.__HideImageEditorInner()
            } else {
                t.__goInner(q)
            }
        };
        var s = a.get("J_btnCancel");
        s.onclick = function() {
            t.RestoreImage();
            Dynamsoft__OnclickCloseInstallEx();
            Dynamsoft.Lib.bChangeImage = 0;
            if (q == -1) {
                t.__HideImageEditorInner()
            } else {
                t.__goInner(q)
            }
        }
    };
    o.prototype.ShowImageEditorEx = function(q, v, p, u, r) {
        var t = this,
            s = t.getEditor();
        s.bShow = false;
        s.refresh(t._stwain.__cIndex, t._stwain._HowManyImagesInBuffer);
        s.bShow = true;
        return s.ShowImageEditorEx(q, v, p, u, r)
    };
    a.UI = {
        ImageUIView: g,
        ImageUIEditor: f,
        ImageUIManager: o
    };
    var j = function(q) {
        var p = true;
        d.each(i, function(r) {
            if (r instanceof g) {
                if (r.bFocus) {
                    p = r.handlerKeyDown(q);
                    if (!p) {
                        return false
                    }
                }
            } else {
                if (r instanceof f) {
                    if (r.bShow) {
                        p = r.handlerKeyDown(q);
                        if (!p) {
                            return false
                        }
                    }
                }
            }
        });
        return p
    };
    k.on(l.documentElement, "keydown", j)
})(Dynamsoft.Lib, KISSY);
CanvasRenderingContext2D.prototype.dashedLineRect = function(m, h, b, n, e) {
    if (typeof e === "undefined") {
        e = 3
    }
    var k = [m, m, m + b, m];
    var j = [h, h, h, h + n];
    var l = [m + b, m, m + b, m + b];
    var g = [h, h + n, h + n, h + n];
    for (var c = 0; c < 4; c++) {
        var r = (l[c] - k[c]);
        var p = (g[c] - j[c]);
        var a = Math.floor(Math.sqrt(r * r + p * p));
        var f = (e <= 0) ? a : (a / e);
        var o = (p / a) * e;
        var q = (r / a) * e;
        this.beginPath();
        for (var d = 0; d < f; d++) {
            if (d % 2) {
                this.lineTo(k[c] + d * q, j[c] + d * o)
            } else {
                this.moveTo(k[c] + d * q, j[c] + d * o)
            }
        }
        this.stroke()
    }
};

function Dynamsoft_fetchSmallImageLoop() {
    var b, a = 500;
    if (Dynamsoft.Lib.images.length >= 1) {
        b = Dynamsoft.Lib.images.pop();
        if (Dynamsoft.Lib.images.length > 1) {
            if (b.bNew) {
                a = 0
            } else {
                a = 0
            }
        }
        b.image.src = b.url
    }
    setTimeout("Dynamsoft_fetchSmallImageLoop()", a)
};
(function(a) {
    Dynamsoft.Lib.env.WSVersion = "10.0.1";
    Dynamsoft.Lib.Errors = {
        Server_Restarted: function(b) {
            b._errorCode = -2208;
            b._errorString = "The connection with the local scanning service (Dynamsoft WebTWAIN Service) encountered a problem and has been reset."
        },
        HttpServerCannotEmpty: function(b) {
            b._errorCode = -2300;
            b._errorString = "Http upload error: the HTTP Server cannot empty."
        },
        NetworkError: function(b) {
            b._errorCode = -2301;
            b._errorString = "Network error"
        },
        InvalidResultFormat: function(b) {
            b._errorCode = -2302;
            b._errorString = "The result format is invalid."
        },
        UploadError: function(d, c, b) {
            d._errorCode = -2303;
            if (c) {
                d._errorString = "Upload canceled"
            } else {
                d._errorString = "Upload error.";
                if (b) {
                    d._errorString += b
                }
            }
        },
        HttpDownloadUrlError: function(b) {
            b._errorCode = -2304;
            b._errorString = "Http download error: the url is invalid."
        },
        HttpDownloadError: function(b) {
            b._errorCode = -2305;
            b._errorString = "Http download error."
        },
        UploadFileCannotEmpty: function(b) {
            b._errorCode = -2306;
            b._errorString = "Upload Error: the upload file cannot be empty."
        },
        InvalidWidthOrHeight: function(b) {
            b._errorCode = -2307;
            b._errorString = "The width or height you entered is invalid."
        },
        Server_Invalid: function(b) {
            if (b._errorCode == 0) {
                b._errorCode = -2308;
                b._errorString = "The local scanning service (Dynamsoft WebTWAIN Service) has been stopped."
            }
        },
        InvalidLocalFilename: function(c, b) {
            c._errorCode = -2309;
            c._errorString = "The LocalFile is emtpy in " + b + " Function."
        },
        BarCode_InvalidIndex: function(c, b) {
            c._errorCode = -2310;
            c._errorString = "The index out of range."
        },
        BarCode_InvalidRemoteFilename: function(b) {
            b._errorCode = -2311;
            b._errorString = "The RemoteFile is emtpy in Barcode Download Function."
        },
        ImageFileLengthCannotZero: function(b) {
            b._errorCode = -2312;
            b._errorString = "The file length is emtpy."
        },
        HTML5NotSupport: function(b) {
            b._errorCode = -2209;
            b._errorString = "The HTML5 version does not support this method or property."
        }
    };
    Dynamsoft.Lib.LS = (function() {
        var b = (window.localStorage) ? true : false,
            c = {
                isSupportLS: function() {
                    return b
                },
                item: function(d, e) {
                    var f = null;
                    if (this.isSupportLS()) {
                        if (e) {
                            localStorage.setItem(d, e);
                            f = e
                        } else {
                            f = localStorage.getItem(d)
                        }
                        if (f === null) {
                            return false
                        }
                        return f
                    } else {
                        return false
                    }
                },
                removeItem: function(d) {
                    if (this.isSupportLS()) {
                        localStorage.removeItem(d)
                    } else {
                        return false
                    }
                    return true
                }
            };
        return c
    })();
    Dynamsoft.Lib.getHttpUrl = function(c) {
        var b = Dynamsoft.Lib.product.ip;
        if (c.ssl) {
            return ["https://", b, ":", c.port, "/"].join("")
        } else {
            return ["http://", b, ":", c.port, "/"].join("")
        }
    };
    Dynamsoft.Lib.getWSUrl = function(c) {
        var b = Dynamsoft.Lib.product.ip;
        if (c.ssl) {
            return ["wss://", b, ":", c.port].join("")
        } else {
            return ["ws://", b, ":", c.port].join("")
        }
    };
    Dynamsoft.Lib.getWS = function(b) {
        var f = this,
            d, e = Dynamsoft.Lib.getWSUrl(b),
            c = Dynamsoft.Lib.product.wsProtocol;
        if (f.detect.OnCreatWS) {
            f.detect.OnCreatWS(e, c)
        }
        if (typeof MozWebSocket != "undefined") {
            d = new MozWebSocket(e, c)
        } else {
            d = new WebSocket(e, c)
        }
        return d
    };
    Dynamsoft.Lib._init = function() {
        var h = Dynamsoft.Lib,
            e = h.LS.item("D_port"),
            f = h.LS.item("D_ssl"),
            b = null;
        h.detect.urls.splice(0);
        h.detect.cUrlIndex = 0;
        if (e && f) {
            b = {
                port: e,
                ssl: (f == "true")
            }
        }
        var d = function(i) {
            if (!b) {
                return true
            }
            if (i.port != b.port || i.ssl != b.ssl) {
                return true
            }
            return false
        };
        var g;
        if (h.detect.detectType === 1) {
            if (b !== null && b.ssl) {
                h.detect.urls.push(b)
            }
            for (var c in h.detect.ports) {
                g = h.detect.ports[c];
                if (g.ssl) {
                    if (d(g)) {
                        h.detect.urls.push(g)
                    }
                }
            }
        } else {
            if (h.detect.detectType === 0) {
                if (b !== null && !b.ssl) {
                    h.detect.urls.push(b)
                }
                for (var c in h.detect.ports) {
                    g = h.detect.ports[c];
                    if (g.ssl === false) {
                        if (d(g)) {
                            h.detect.urls.push(g)
                        }
                    }
                }
            } else {
                if (b !== undefined && b !== null) {
                    h.detect.urls.push(b)
                }
                for (var c in h.detect.ports) {
                    g = h.detect.ports[c];
                    if (d(g)) {
                        h.detect.urls.push(g)
                    }
                }
            }
        }
        g = null
    };
    Dynamsoft.Lib._reconnect = function() {
        var h = Dynamsoft.Lib,
            d = h.LS.item("D_port"),
            f = h.LS.item("D_ssl"),
            c = null;
        h.detect.urls.splice(0);
        h.detect.cUrlIndex = 0;
        if (d && f) {
            c = {
                port: d,
                ssl: (f == "true")
            };
            h.detect.urls.push(c);
            var g;
            if (h.detect.cTwainIndex < h.detect.arySTwains.length) {
                g = h.detect.arySTwains[h.detect.cTwainIndex]
            }
            if (!g) {
                return
            }
            try {
                var b = h.getWS(c);
                g._objWS = b;
                b.onopen = function() {
                    if (!g.bReady) {
                        g._OnReady(true)
                    }
                    Dynamsoft.Lib.detect.hideMask();
                    h.detect.bOK = true
                };
                b.onclose = function() {
                    b.onopen = null;
                    if (!g.bReady) {
                        g.__wsRetry++;
                        if (g.__wsRetry < 5) {
                            setTimeout(Dynamsoft.Lib._reconnect, 1000)
                        } else {
                            Dynamsoft.Lib.closeProgress("Reconnect");
                            Dynamsoft.Lib.Errors.Server_Restarted(g)
                        }
                    } else {
                        Dynamsoft.Lib.closeProgress("Reconnect");
                        Dynamsoft.Lib.detect.hideMask()
                    }
                }
            } catch (e) {
                Dynamsoft.Lib.log(e);
                if (!g.bReady) {
                    g.__wsRetry++;
                    if (g.__wsRetry < 5) {
                        setTimeout(Dynamsoft.Lib._reconnect, 1000)
                    }
                } else {
                    Dynamsoft.Lib.detect.hideMask()
                }
            }
        }
    };
    Dynamsoft.Lib.startWS = function() {
        var i = Dynamsoft.Lib;
        if (!Dynamsoft.Lib.product.bChromeEdition) {
            Dynamsoft.Lib.appendMessage("This browser is currently not supported. Please try Chrome or Firefox!");
            return
        }
        if (!i.detect.bNoControlEvent) {
            Dynamsoft.Lib.detect.showMask()
        }
        if (i.detect.OnDetectNext) {
            i.detect.OnDetectNext()
        }
        if (i.detect.tryTimes == 0 && i.detect.cUrlIndex > 0) {
            if (!i.detect.bNoControlEvent) {
                Dynamsoft.Lib.detect.hideMask();
                if (KISSY.isFunction(i.detect.onNoControl)) {
                    var g = i.detect.arySTwains[i.detect.cTwainIndex],
                        b, e;
                    b = g._iWidth;
                    e = g._iHeight;
                    if (b <= 0) {
                        b = 270
                    }
                    if (e <= 0) {
                        e = 350
                    }
                    i.detect.onNoControl(g._strDWTControlContainerID, b, e)
                }
                i.detect.bNoControlEvent = true
            }
        }
        if (i.detect.cUrlIndex >= i.detect.urls.length) {
            i.detect.cUrlIndex = 0;
            i.detect.tryTimes++
        }
        var f;
        if (i.detect.cTwainIndex < i.detect.arySTwains.length) {
            f = i.detect.arySTwains[i.detect.cTwainIndex]
        } else {
            i.detect.bOK = true;
            return
        }
        try {
            var h = i.detect.urls[i.detect.cUrlIndex],
                c = i.getWS(h);
            Dynamsoft.Lib.log(["connecting ... [port:", h.port, "]"].join(""));
            f._objWS = c;
            c.onopen = function() {
                if (!f.bReady) {
                    f._OnReady()
                }
                i.detect.cTwainIndex++;
                if (i.detect.cTwainIndex >= i.detect.arySTwains.length) {
                    Dynamsoft.Lib.detect.hideMask();
                    i.detect.bOK = true
                } else {
                    setTimeout(Dynamsoft.Lib.startWS, 100)
                }
            };
            c.onclose = function() {
                c.onopen = null;
                if (!f.bReady) {
                    i.detect.cUrlIndex++;
                    setTimeout(Dynamsoft.Lib.startWS, 100)
                } else {
                    Dynamsoft.Lib.detect.hideMask()
                }
            }
        } catch (d) {
            Dynamsoft.Lib.log(d);
            if (!f.bReady) {
                i.detect.cUrlIndex++;
                setTimeout(Dynamsoft.Lib.startWS, 100)
            } else {
                Dynamsoft.Lib.detect.hideMask()
            }
        }
    };
    Dynamsoft.Lib.closeAll = function() {
        var e = this,
            d, c, b;
        for (d = 0; d < e.detect.arySTwains.length; d++) {
            c = e.detect.arySTwains[d];
            if (c.bReady) {
                c._innerSend("RemoveAllImages", null, true);
                b = c._objWS;
                if (b) {
                    b.close();
                    c._objWS = null;
                    c.bReady = false
                }
            }
        }
        e.detect.bOK = false;
        e.detect.arySTwains.splice(0);
        e.detect.cTwainIndex = 0;
        e.detect.bFirst = true
    };
    Dynamsoft.Lib.bio = false;
    Dynamsoft.Lib.progressMessage = "";
    Dynamsoft.Lib.dialogShowStatus = false;
    Dynamsoft.Lib.needShowTwiceShowDialog = false;
    Dynamsoft.Lib.dlgProgress = false;
    Dynamsoft.Lib.cancelFrome = 0;
    Dynamsoft.Lib.dlgRef = 0;
    Dynamsoft.Lib.showProgress = function(e, b, d) {
        var i = e;
        Dynamsoft.Lib.log("showProgress:" + b + ",bCancel:" + d);
        if ((Dynamsoft.Lib.cancelFrome == 0 && i.__IfShowProgressBar == true) || (Dynamsoft.Lib.cancelFrome != 0 && i.__IfShowCancelDialogWhenImageTransfer == true)) {
            var g = a.one("#btnCancel");
            if (d == false) {
                g[0].style.display = "none"
            } else {
                g[0].style.display = ""
            }
            if (Dynamsoft.Lib.dialogShowStatus == false) {
                Dynamsoft.Lib.dlgProgress.showModal();
                Dynamsoft.Lib.dialogShowStatus = true;
                var h = a.one("#finalMessage");
                var f = a.one("#progressBar");
                var c = a.one("#status");
                h.html("");
                c.html("0%");
                f[0].value = 0
            }
            Dynamsoft.Lib.dlgRef++
        }
    };
    Dynamsoft.Lib.closeProgress = function(b) {
        Dynamsoft.Lib.log("closeProgress:" + b);
        var c = a.one("#btnCancel");
        if (Dynamsoft.Lib.needShowTwiceShowDialog == true && Dynamsoft.Lib.cancelFrome == 1) {
            c.html("Canceling")
        } else {
            c.html("Cancel");
            if (Dynamsoft.Lib.dialogShowStatus == true) {
                Dynamsoft.Lib.dlgProgress.close();
                Dynamsoft.Lib.dialogShowStatus = false
            }
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            Dynamsoft.Lib.cancelFrome = 0
        }
        Dynamsoft.Lib.dlgRef--;
        if (Dynamsoft.Lib.dlgRef <= 0) {
            Dynamsoft.Lib.dlgRef = 0
        }
    }
})(KISSY);
(function(a, d) {
    var f = 1,
        h = {
            notSupportProperty: function(j) {},
            notSupportMethod: function(j) {},
            getRandom: function() {
                var k = new Date().getTime() % 10000,
                    j = [],
                    m;
                for (var l = 0; l < 5; l++) {
                    m = Math.floor(Math.random() * 10);
                    if (l == 0 && m == 0) {
                        l = -1;
                        continue
                    }
                    j.push(m)
                }
                if (k < 10) {
                    j.push("000")
                } else {
                    if (k < 100) {
                        j.push("00")
                    } else {
                        if (k < 1000) {
                            j.push("0")
                        }
                    }
                }
                j.push(k);
                return j.join("")
            },
            generateCmdId: function() {
                f++;
                return f
            },
            sendData: function(k, u, p, s) {
                var t, o, l, m, j, r, n, q;
                if (!s) {
                    k.send(u);
                    return
                }
                m = new ArrayBuffer(12);
                o = p && p.size ? p.size : 0;
                q = new Blob([u]);
                t = q.size;
                l = t + o;
                j = new DataView(m);
                for (n = 0; n < 8; n++) {
                    if (l) {
                        j.setUint8(n, l % 256);
                        l = parseInt(l / 256)
                    } else {
                        break
                    }
                }
                for (n = 0; n < 4; n++) {
                    if (t) {
                        j.setUint8(8 + n, t % 256);
                        t = parseInt(t / 256)
                    } else {
                        break
                    }
                }
                r = new Blob([m, q]);
                k.send(r);
                if (o > 0) {
                    k.send(p)
                }
            },
            init: function(n, u) {
                var k, l, s, j, q, p, r, t = '<dialog id="dialogProgress" class="ds-dwt-dialogProgress" style="top:30%"><h3 id="finalMessage"></h3><p id="status">0%</p><progress id="progressBar" value="" max="100"></progress><br><button id="btnCancel" value="cancel" autofcus >cancel</button></dialog>',
                    m = '<div class="ds-dwt-container-box"></div>';
                l = d.one("#" + u);
                l.attr("class", n.containerClass);
                l.append(m);
                r = l.parent();
                r[0].style["-moz-user-select"] = "none";
                r[0].onselectstart = function() {
                    return false
                };
                p = l.one(".ds-dwt-container-box");
                if (p) {
                    p.style("width", n._iWidth + "px");
                    p.style("height", n._iHeight + "px");
                    p.style("position", "relative")
                }
                if (!Dynamsoft.Lib.dlgProgress) {
                    l.append(t);
                    q = l.one("#dialogProgress");
                    if (q && q[0]) {
                        Dynamsoft.Lib.dlgProgress = q[0];
                        dialogPolyfill.registerDialog(Dynamsoft.Lib.dlgProgress)
                    }
                    Dynamsoft.Lib.dlgRef = 0;
                    Dynamsoft.Lib.dialogShowStatus = false;
                    Dynamsoft.Lib.cancelFrome = 0;
                    var o = l.one("#btnCancel");
                    o[0].onclick = function() {
                        var v = Dynamsoft.Lib.cancelFrome;
                        Dynamsoft.Lib.closeProgress(0);
                        if (Dynamsoft.Lib.bio) {
                            Dynamsoft.Lib.bio.abort();
                            Dynamsoft.Lib.bio = false
                        } else {
                            if (v == 3 || v == 4) {
                                n.SetCancel()
                            }
                        }
                    }
                }
                h.initImageUI(n, p)
            },
            initImageUI: function(l, m) {
                var j = {
                        container: m,
                        width: l._iWidth,
                        height: l._iHeight
                    },
                    k = {
                        container: m,
                        width: l._iWidth,
                        height: l._iHeight,
                        defaultImageUrl: Dynamsoft.WebTwainEnv.ResourcesPath + "/reference/imgs/Dynamsoft_bg.png"
                    };
                h.bindUIViewEvents(l, j);
                h.bindUIEditorEvents(l, k);
                l._UIManager = new Dynamsoft.Lib.UI.ImageUIManager(l, j, k)
            },
            changeImageUISize: function(o, j, m) {
                var k, n, l = -1,
                    p = -1;
                k = d.one("#" + o._strDWTControlContainerID);
                if (d.isString(j) && j.indexOf("%") >= 0) {
                    l = d.DOM.width(k.parent()) * parseInt(j) / 100
                } else {
                    l = parseInt(j)
                }
                if (d.isString(m) && m.indexOf("%") >= 0) {
                    p = d.DOM.height(k.parent()) * parseInt(m) / 100
                } else {
                    p = parseInt(m)
                }
                if (k) {
                    n = k.one(".ds-dwt-container-box");
                    if (n) {
                        if (l >= 0) {
                            n.style("width", l + "px")
                        }
                        if (p >= 0) {
                            n.style("height", p + "px")
                        }
                    }
                }
                o._UIManager.ChangeSize(l, p);
                if (l >= 0) {
                    o._iWidth = l
                }
                if (p >= 0) {
                    o._iHeight = p
                }
            },
            bindUIViewEvents: function(l, j) {
                var m = l,
                    k;
                j.onSelected = function(n) {
                    var p = n.length,
                        o;
                    if (p > 0) {
                        m.__SelectedImagesCount = p;
                        o = n[p - 1];
                        m.__cIndex = o;
                        k = c.__SetSelectedImages(m, m.__cIndex, n);
                        if (k && h.isFunction(m.__OnRefreshUI)) {
                            m.__OnRefreshUI(o)
                        }
                    } else {
                        m.__SelectedImagesCount = 0
                    }
                };
                j.onRefreshUI = function(n) {
                    h.__innerRefreshFromUIView(m, n)
                };
                j.onMouseMove = function(n) {
                    if (h.isFunction(m.__OnMouseMove)) {
                        m.__OnMouseMove(n)
                    }
                };
                j.onMouseClick = function(n) {
                    if (h.isFunction(m.__OnMouseClick)) {
                        m.__OnMouseClick(n)
                    }
                };
                j.onMouseDoubleClick = function(n) {
                    if (h.isFunction(m.__OnMouseDoubleClick)) {
                        m.__OnMouseDoubleClick(n)
                    }
                };
                j.onMouseRightClick = function(n) {
                    if (h.isFunction(m.__OnMouseRightClick)) {
                        return m.__OnMouseRightClick(n)
                    }
                    return true
                }
            },
            bindUIEditorEvents: function(k, j) {
                var l = k;
                j.onMouseMove = function(o, n) {
                    var m = n.x,
                        p = n.y;
                    l.__MouseX = m;
                    l.__MouseY = p;
                    if (h.isFunction(l.__OnMouseMove)) {
                        l.__OnMouseMove(o)
                    }
                };
                j.onMouseClick = function(m) {
                    if (h.isFunction(l.__OnMouseClick)) {
                        l.__OnMouseClick(m)
                    }
                };
                j.onMouseDoubleClick = function(m) {
                    if (h.isFunction(l.__OnMouseDoubleClick)) {
                        l.__OnMouseDoubleClick(m)
                    }
                };
                j.onMouseRightClick = function(m) {
                    if (h.isFunction(l.__OnMouseRightClick)) {
                        return l.__OnMouseRightClick(m)
                    }
                    return true
                };
                j.onRefreshUI = function(m) {
                    h.__innerRefreshFromUIEditor(l, m)
                };
                j.onImageAreaSelected = function(m) {
                    if (h.isFunction(l.__OnImageAreaSelected)) {
                        l.__OnImageAreaSelected(m.cIndex, m.left, m.top, m.right, m.bottom)
                    }
                };
                j.onImageAreaDeSelected = function(m) {
                    if (h.isFunction(l.__OnImageAreaDeSelected)) {
                        l.__OnImageAreaDeSelected(m)
                    }
                }
            },
            isFunction: function(j) {
                return j && typeof(j) === "function"
            },
            __pushElement: function(k, j) {
                if (d.isString(j)) {
                    k.push('"');
                    k.push(j);
                    k.push('"')
                } else {
                    if (d.isArray(j)) {
                        var l = true;
                        k.push("[");
                        d.each(j, function(m) {
                            if (l) {
                                l = false
                            } else {
                                k.push(",")
                            }
                            h.__pushElement(k, m)
                        });
                        k.push("]")
                    } else {
                        k.push(j)
                    }
                }
            },
            makeParams: function() {
                var k = arguments;
                if (k === undefined || k.length === 0) {
                    return undefined
                } else {
                    var j = [];
                    if (k.length === 1) {
                        var m = k[0];
                        j.push("[");
                        h.__pushElement(j, m);
                        j.push("]")
                    } else {
                        if (k.length === 2) {
                            var m = k[0],
                                l = k[1];
                            j.push("[");
                            h.__pushElement(j, m);
                            j.push(",");
                            h.__pushElement(j, l);
                            j.push("]")
                        } else {
                            h.__pushElement(j, Array.prototype.slice.call(k))
                        }
                    }
                    return j.join("")
                }
            },
            getJson: function(n, k, o, j) {
                var l = [];
                l.push("{");
                l.push('"id":"');
                l.push(n.clientId);
                l.push('"');
                if (j) {
                    l.push(',"cmdId":"');
                    l.push(j);
                    l.push('"')
                }
                l.push(',"method":"');
                l.push(k);
                l.push('"');
                if (o !== undefined && o !== null) {
                    l.push(',"parameter":');
                    l.push(o)
                }
                l.push("}");
                return l.join("")
            },
            refreshImageAfterInvokeFun: function(j, l, k) {
                return (k == 1)
            },
            loadHttpBlob: function(n, s, l, o, t, j, r) {
                var k = "loadHttpBlob",
                    u, p, q = false;
                u = function(x, w, m) {
                    if (m.state == 2 && m.status == 0) {
                        Dynamsoft.Lib.Errors.HttpDownloadUrlError(n)
                    } else {
                        Dynamsoft.Lib.Errors.HttpDownloadError(n)
                    }
                    if (h.isFunction(j)) {
                        j()
                    }
                };
                p = {
                    type: s,
                    url: l,
                    async: o,
                    success: t,
                    error: u,
                    complete: function() {
                        Dynamsoft.Lib.bio = false
                    }
                };
                if (n.HTTPUserName != "") {
                    p.username = n.HTTPUserName;
                    p.password = n.HTTPPassword
                }
                if (o) {
                    p.responseType = "blob";
                    if (h.isFunction(r)) {
                        p.beforeSend = function(m) {
                            var w = m.getNativeXhr();
                            w.addEventListener("progress", function(x) {
                                r(x)
                            }, false)
                        }
                    }
                    q = true
                } else {
                    p.contentType = "text/plain; charset=x-user-defined";
                    p.mimeType = "text/plain; charset=x-user-defined";
                    p.success = function(A, z, y) {
                        var B, w, m;
                        B = y.responseText;
                        w = B.length;
                        m = new Uint8Array(w);
                        for (var x = 0; x < w; x++) {
                            m[x] = B.charCodeAt(x)
                        }
                        n._errorCode = 0;
                        n._errorString = "";
                        t(m);
                        q = true
                    }
                }
                var v = new KISSY.IO(p);
                if (o) {
                    Dynamsoft.Lib.bio = v
                }
                return q
            },
            getServerImageUrlPrefix: function(m, n) {
                var k = m.httpUrl,
                    j = false,
                    l = [k, "img?id=", m.clientId];
                return l.join("")
            },
            getServerSmallImageUrl: function(n, o, l) {
                var k = n.httpUrl,
                    j = false,
                    m = [k, "img?id=", n.clientId];
                if (o !== undefined && o !== null) {
                    m.push("&index=");
                    m.push(o)
                }
                if (l) {
                    m.push("&act=");
                    m.push(l)
                } else {
                    m.push("&width=");
                    m.push(j ? "-1" : n._iWidth / 2);
                    m.push("&height=");
                    m.push(j ? "-1" : n._iHeight / 2)
                }
                return m.join("")
            },
            combineUrl: function(m, k, l) {
                var j = [];
                if (k.indexOf("http://") == 0 || k.indexOf("https://") == 0) {} else {
                    if (m.IfSSL) {
                        j.push("https://")
                    } else {
                        j.push("http://")
                    }
                }
                j.push(k);
                if (k.indexOf(":") < 0) {
                    if (m.HTTPPort == "") {
                        m.HTTPPort = Dynamsoft.Lib.detect.ssl ? 443 : 80
                    }
                    if ((!Dynamsoft.Lib.detect.ssl && m.HTTPPort != 80) || (Dynamsoft.Lib.detect.ssl && m.HTTPPort != 443)) {
                        j.push(":");
                        j.push(m.HTTPPort)
                    }
                }
                if (l.indexOf("/") !== 0) {
                    j.push("/")
                }
                j.push(l);
                return j.join("")
            },
            httpPostUpload: function(n, m, l, j, k, o) {
                if (!l) {
                    Dynamsoft.Lib.Errors.UploadFileCannotEmpty(n);
                    if (h.isFunction(o)) {
                        o()
                    }
                    return
                }
                l.remoteFilename = n.__remoteFilename;
                Dynamsoft.Lib.bio = new KISSY.BIO({
                    action: m,
                    data: n.httpFormFields,
                    async: j
                });
                n.__HTTPPostResponseString = false;
                Dynamsoft.Lib.bio.on(d.BIO.event.SUCCESS, function(p) {
                    var q;
                    Dynamsoft.Lib.bio = false;
                    if (p && p.result && d.isString(p.result.response)) {
                        n.__HTTPPostResponseString = p.result.response
                    } else {
                        n.__HTTPPostResponseString = ""
                    }
                    if (p && p.result && p.result.json) {
                        q = json
                    } else {
                        q = n.__HTTPPostResponseString
                    }
                    if (h.isFunction(k)) {
                        k(q)
                    }
                });
                Dynamsoft.Lib.bio.on(d.BIO.event.ERROR, function(r) {
                    var s, q = (r && r.canceled),
                        p;
                    Dynamsoft.Lib.bio = false;
                    if (r && r.result && d.isString(r.result.response)) {
                        n.__HTTPPostResponseString = r.result.response
                    } else {
                        n.__HTTPPostResponseString = ""
                    }
                    if (r && r.result && r.result.json) {
                        s = json
                    } else {
                        s = n.__HTTPPostResponseString
                    }
                    if (r && r.result && r.result.status) {
                        p = r.result.status
                    }
                    Dynamsoft.Lib.Errors.UploadError(n, q, p);
                    if (h.isFunction(o)) {
                        o(s)
                    }
                });
                Dynamsoft.Lib.bio.on(d.BIO.event.PROGRESS, function(q) {
                    var r = (q.total === 0) ? 100 : Math.round(q.loaded * 100 / q.total),
                        s = [q.loaded, " / ", q.total].join("");
                    n._OnPercentDone([0, r, "", "http"])
                });
                n._OnPercentDone([0, -1, "Uploading file. Please wait.....", "http"]);
                Dynamsoft.Lib.bio.addBlob(l);
                Dynamsoft.Lib.bio.uploadFiles(d.BIO.status.WAITING);
                return true
            },
            httpPutImage: function(p, n, m, j, o, q, l) {
                var k = {
                    type: "PUT",
                    url: n,
                    hasContent: true,
                    processData: false,
                    async: j,
                    data: m,
                    success: o,
                    error: q,
                    complete: function() {
                        Dynamsoft.Lib.bio = false
                    },
                    beforeSend: function(r) {
                        if (h.isFunction(l)) {
                            var s = r.getNativeXhr();
                            s.upload.addEventListener("progress", function(t) {
                                l(t)
                            }, false)
                        }
                    }
                };
                if (p.HTTPUserName != "") {
                    k.username = p.HTTPUserName;
                    k.password = p.HTTPPassword
                }
                Dynamsoft.Lib.bio = new KISSY.IO(k);
                return true
            },
            dialog: false,
            dialogRef: 0,
            showMask: function(k) {
                var j = this;
                a.log("showMask:" + k + "--" + j.dialogRef);
                if (j.dialogRef == 0) {
                    var n = [Dynamsoft.WebTwainEnv.ResourcesPath, "/reference/loading.gif"].join(""),
                        l = ['<img src="', n, '" />'].join("");
                    j.dialog = new KISSY.Overlay({
                        width: 400,
                        content: l,
                        mask: true,
                        align: {
                            points: ["cc", "cc"]
                        }
                    });
                    j.dialog.show()
                }
                j.dialogRef++
            },
            hideMask: function(k) {
                var j = this;
                a.log("hideMask:" + k + "--" + j.dialogRef);
                if (j.dialogRef > 1) {
                    j.dialogRef--
                } else {
                    if (j.dialogRef <= 1) {
                        if (j.dialog) {
                            j.dialog.destroy()
                        }
                        j.dialog = false;
                        j.dialogRef = 0
                    }
                }
            },
            setBtnCancelVisibleForProgress: function(l, k) {
                var j = this;
                if ((Dynamsoft.Lib.cancelFrome == 0 && l.__IfShowProgressBar == true) || (Dynamsoft.Lib.cancelFrome != 0 && l.__IfShowCancelDialogWhenImageTransfer == true)) {
                    var m = d.one("#btnCancel");
                    if (k == false) {
                        m[0].style.display = "none"
                    } else {
                        m[0].style.display = ""
                    }
                }
            },
            EVENTS: ["OnPostAllTransfers", "OnPostTransfer", "OnPostLoad", "OnPreTransfer", "OnPreAllTransfers", "OnResult", "OnTransferCancelled", "OnTransferError", "OnSourceUIClose", "OnBitmapChanged", "OnGetFilePath", "OnPercentDone"],
            SEND_BACK_EVENTS: ["OnPreAllTransfers", "OnPreTransfer", "OnPostTransfer", "OnGetFilePath", "OnPostLoad"],
            handleEvent: function(m, k) {
                var o = this,
                    l = k.result[0],
                    j = m._objWS,
                    n = false;
                d.each(h.EVENTS, function(r, q) {
                    if (k.event === r) {
                        var p = ["_", r].join("");
                        n = true;
                        if (h.isFunction(m[p])) {
                            m[p](k.result)
                        }
                        return false
                    }
                });
                if (n) {
                    a.log("handled event:" + k.event);
                    d.each(h.SEND_BACK_EVENTS, function(q, p) {
                        if (h.isServerInvalid(m)) {
                            return false
                        }
                        if (k.event === q) {
                            a.log("sendback event:" + k.event);
                            h.sendData(j, h.getJson(m, k.event, h.makeParams(l), 0), false, true);
                            return false
                        }
                    })
                }
                return n
            },
            getImageType: function(k) {
                var j = k.length;
                if (j < 4) {
                    return -1
                }
                var l = k.lastIndexOf(".");
                if (l === -1) {
                    return -1
                }
                var m = k.slice(l).toLowerCase();
                if (m === ".bmp" || m === ".dib") {
                    return 0
                }
                if (m === ".jpg" || m === ".jpe" || m === ".jpeg" || m === ".jfif") {
                    return 1
                }
                if (m === ".tif" || m === ".tiff") {
                    return 2
                }
                if (m === ".png") {
                    return 3
                }
                if (m === ".pdf") {
                    return 4
                }
                if (m === ".gif") {
                    return 5
                }
                return -1
            },
            autoDiscardBlankImage: function(m, j) {
                if (a.config.bDiscardBlankImage) {
                    var k = m.CurrentImageIndexInBuffer;
                    if (m.IsBlankImage(k)) {
                        m.RemoveImage(k)
                    }
                    var l = ["Blank Discard (", j, "): ", m.ErrorString].join("");
                    if (h.isFunction(m.__OnPrintMsg)) {
                        m.__OnPrintMsg(l)
                    }
                }
            },
            replaceLocalFilename: function(j) {
                var k;
                if (Dynamsoft.Lib.env.bWin) {
                    k = j.replace(/\\/g, "\\\\")
                } else {
                    k = j
                }
                if (Dynamsoft.Lib.env.bFileSystem) {
                    k = decodeURI(k)
                }
                return k
            },
            __innerRefreshFromUIView: function(k, l, j) {
                var m = k;
                if (l >= 0) {
                    if (!j) {
                        m.CurrentImageIndexInBuffer = l
                    }
                    m._UIManager.getEditor().refresh(m.__cIndex, m._HowManyImagesInBuffer)
                }
                if (h.isFunction(m.__OnRefreshUI)) {
                    m.__OnRefreshUI(l)
                }
            },
            __innerRefreshFromUIEditor: function(k, m) {
                var n = k;
                n.CurrentImageIndexInBuffer = m;
                if (n._UIManager.getEditor().mode == "editor_out" || n._UIManager.getEditor().mode == "fullscreen") {
                    if (m >= 0 && m < n._UIManager.count()) {
                        var j = n._UIManager.get(m),
                            l;
                        j.ticks = h.getRandom();
                        n._UIManager.set(j, m);
                        n._UIManager.getView().go(m)
                    }
                }
                if (h.isFunction(n.__OnRefreshUI)) {
                    n.__OnRefreshUI(m)
                }
            },
            isServerInvalid: function(k) {
                var l = k,
                    j = !l.bReady || l._objWS === null;
                if (j) {
                    Dynamsoft.Lib.Errors.Server_Invalid(l)
                }
                return j || l._errorCode == -2208 || l._errorCode == -2308
            },
            isHttpServerInvalid: function(k) {
                var l = k,
                    j = !l.bReady || l.httpUrl === null;
                if (j) {
                    Dynamsoft.Lib.Errors.Server_Invalid(l)
                }
                return j || l._errorCode == -2208
            },
            callbacks: [],
            pushCallback: function(j, n, k) {
                var l = {
                    method: j,
                    callback: n,
                    ticks: k
                };
                h.callbacks(l)
            },
            refreshUIRef: 0
        },
        c = {
            __SetSelectedImages: function(l, k, j) {
                l.__SelectedImagesCount = j.length;
                if (k === undefined) {
                    k = ""
                }
                return l._innerFun("SetSelectedImages", h.makeParams(k, j.join(",")))
            }
        };

    function g() {
        var j = this;
        j._errorCode = 0;
        j._errorString = "";
        j._Count = 0;
        j._resultlist = []
    }
    g.prototype.GetErrorCode = function() {
        var j = this;
        return j._errorCode
    };
    g.prototype.GetErrorString = function() {
        var j = this;
        return j._errorString
    };
    g.prototype.GetCount = function() {
        var j = this;
        return j._Count
    };
    g.prototype.GetContent = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetContent");
            return ""
        }
        return l._resultlist[k].content
    };
    g.prototype.GetFormat = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetFormat");
            return ""
        }
        return l._resultlist[k].format
    };
    g.prototype.GetContentType = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetContentType");
            return ""
        }
        return l._resultlist[k].contentType
    };
    g.prototype.GetX1 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetX1");
            return ""
        }
        return l._resultlist[k].point[0]
    };
    g.prototype.GetX2 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetX2");
            return ""
        }
        return l._resultlist[k].point[2]
    };
    g.prototype.GetX3 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetX3");
            return ""
        }
        return l._resultlist[k].point[4]
    };
    g.prototype.GetX4 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetX4");
            return ""
        }
        return l._resultlist[k].point[6]
    };
    g.prototype.GetY1 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetY1");
            return ""
        }
        return l._resultlist[k].point[1]
    };
    g.prototype.GetY2 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetY2");
            return ""
        }
        return l._resultlist[k].point[3]
    };
    g.prototype.GetY3 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetY3");
            return ""
        }
        return l._resultlist[k].point[5]
    };
    g.prototype.GetY4 = function(k) {
        var l = this,
            j = l._resultlist.length;
        if (l._errorCode < 0) {
            return ""
        }
        if (k >= j || k < 0) {
            Dynamsoft.Lib.Errors.BarCode_InvalidIndex(l, "GetY4");
            return ""
        }
        return l._resultlist[k].point[7]
    };
    var b = function(j) {
        var k = {
            Barcode: {
                Read: function(n, o, l, m) {
                    return j.__innerBarcodeReadFunction("ReadBarcode", n, h.makeParams(n, 0, 0, 0, 0, o), l, m)
                },
                ReadRect: function(p, s, r, o, m, q, l, n) {
                    return j.__innerBarcodeReadFunction("ReadBarcode", p, h.makeParams(p, s, r, o, m, q), l, n)
                },
                GetLocalVersion: function() {
                    return j._innerFun("BarcodeVersion")
                },
                Download: function(p, s, l) {
                    var o = j,
                        q;
                    Dynamsoft.Lib.cancelFrome = 2;
                    var n = function() {
                            if (h.isFunction(s)) {
                                s()
                            }
                            return true
                        },
                        t = function() {
                            if (h.isFunction(l)) {
                                l(o.ErrorCode, o.ErrorString)
                            }
                            return false
                        };
                    if (!p || p == "") {
                        Dynamsoft.Lib.Errors.BarCode_InvalidRemoteFilename(o);
                        t();
                        return false
                    }
                    if (h.isServerInvalid(o)) {
                        t();
                        return false
                    }
                    q = "get";
                    a.showProgress(o, "Download", true);
                    var r = function(u) {
                            var v = (u.total === 0) ? 100 : Math.round(u.loaded * 100 / u.total),
                                w = [u.loaded, " / ", u.total].join("");
                            o._OnPercentDone([0, v, "", "http"])
                        },
                        m = true;
                    o._OnPercentDone([0, -1, "HTTP Downloading...", "http"]);
                    if (!h.isFunction(s)) {
                        m = false
                    }
                    h.loadHttpBlob(o, q, p, m, function(u) {
                        o._OnPercentDone([0, -1, "Loading..."]);
                        var v = 100;
                        o.__LoadImageFromBytes(u, v, "", m, n, t)
                    }, function() {
                        a.closeProgress("Download");
                        t()
                    }, r)
                }
            }
        };
        return k
    };

    function e(k) {
        var j = this;
        j._parent = k;
        j._w = k._iWidth;
        j._h = k._iHeight
    }
    e.prototype = {get width() {
            return this._w
        },
        get height() {
            return this._h
        },
        set width(j) {
            var k = this;
            k._w = parseInt(j);
            h.changeImageUISize(k._parent, k._w, k._h)
        },
        set height(j) {
            var k = this;
            k._h = parseInt(j);
            h.changeImageUISize(k._parent, k._w, k._h)
        }
    };
    e.prototype.SetSize = function(j, k) {
        var l = this;
        l._w = parseInt(j);
        l._h = parseInt(k);
        h.changeImageUISize(l._parent, l._w, l._h);
        return l
    };

    function i(j) {
        var k = this,
            l = j || {};
        k.clientId = h.getRandom();
        k._strDWTControlContainerID = l.containerID || ("dwt-container-" + k.clientId);
        k._iWidth = l.width;
        k._iHeight = l.height;
        k.containerClass = "ds-dwt-container noPadding";
        h.init(k, k._strDWTControlContainerID);
        k.objectName = k._strDWTControlContainerID + "_obj";
        k.bReady = false;
        k.httpFormFields = {};
        k._objWS = null;
        k.__wsRetry = 0;
        k.curCommand = [];
        k.curCommand_SaveImagesToBytes = [];
        Dynamsoft.Lib.__innerInitEvents(k, l);
        k.__OnMouseClick = l.onMouseClick || "";
        k.__OnMouseDoubleClick = l.onMouseDoubleClick || "";
        k.__OnMouseRightClick = l.onMouseRightClick || "";
        k.__OnMouseMove = l.onMouseMove || "";
        k.__OnImageAreaSelected = l.onImageAreaSelected || "";
        k.__OnImageAreaDeSelected = l.onImageAreaDeSelected || "";
        k.__OnInternetTransferPercentage = l.onInternetTransferPercentage || "";
        k.__OnInternetTransferPercentageEx = l.onInternetTransferPercentageEx || "";
        k.__OnWSReady = l.onWSReady || null;
        k.__OnWSReconnect = l.onWSReconnect || null;
        k.__OnWSClose = l.onWSClose || null;
        k.__OnWSMessage = l.onWSMessage || null;
        k.__OnWSError = l.onWSError || null;
        k.__OnPercentDone = l.onPercentDone || "";
        k.__OnRefreshUI = l.onRefreshUI || "";
        k.__OnPrintMsg = l.onPrintMsg || "";
        k.__OnResult = l.onResult || "";
        k.HTTPPassword = l.HTTPPassword || "";
        k.HTTPPort = l.HTTPPort || (Dynamsoft.Lib.detect.ssl ? 443 : 80);
        k.HTTPUserName = l.HTTPUserName || "";
        k.__HTTPPostResponseString = "";
        k.__IfShowProgressBar = true;
        k.__IfShowCancelDialogWhenImageTransfer = true;
        k.__cIndex = -1;
        k._HowManyImagesInBuffer = 0;
        k._errorCode = 0;
        k._errorString = "";
        k.__bLoadingImage = false;
        k.__backgroundColor = false;
        k.__MouseX = 0;
        k.__MouseY = 0;
        k.__style = new e(k);
        k.__addon = new b(k);
        k.__remoteFilename = "RemoteFile";
        k.__sourceName = [];
        k.__defaultSourceName = "";
        k.__sourceCount = 0;
        k.__SelectedImagesCount = 0;
        k.__serverId = ""
    }
    i.prototype = {get ErrorCode() {
            return this._errorCode
        },
        get ErrorString() {
            if (this._errorCode != 0) {
                return this._errorString
            }
            return "Successful."
        },
        get LogLevel() {
            return this._innerFun("LogLevel")
        },
        set LogLevel(k) {
            var l = this,
                j = k * 1;
            return l._innerFun("LogLevel", h.makeParams(j))
        },
        get Manufacturer() {
            return this._innerFun("Manufacturer")
        },
        set Manufacturer(j) {
            return this._innerFun("Manufacturer", h.makeParams(j))
        },
        get ProductFamily() {
            return this._innerFun("ProductFamily")
        },
        set ProductFamily(j) {
            return this._innerFun("ProductFamily", h.makeParams(j))
        },
        get ProductKey() {
            return Dynamsoft.WebTwainEnv.ProductKey
        },
        set ProductKey(j) {
            Dynamsoft.WebTwainEnv.ProductKey = j;
            return this._innerFun("ProductKey", h.makeParams(j))
        },
        get ProductName() {
            return this._innerFun("ProductName")
        },
        set ProductName(j) {
            return this._innerFun("ProductName", h.makeParams(j))
        },
        get VersionInfo() {
            return this._innerFun("VersionInfo")
        },
        get BitDepth() {
            return this._innerFun("BitDepth")
        },
        set BitDepth(j) {
            return this._innerFun("BitDepth", h.makeParams(j))
        },
        get Brightness() {
            return this._innerFun("Brightness")
        },
        set Brightness(j) {
            return this._innerFun("Brightness", h.makeParams(j))
        },
        get Contrast() {
            return this._innerFun("Contrast")
        },
        set Contrast(j) {
            return this._innerFun("Contrast", h.makeParams(j))
        },
        get CurrentSourceName() {
            return this._innerFun("CurrentSourceName")
        },
        get DataSourceStatus() {
            return this._innerFun("DataSourceStatus")
        },
        get DefaultSourceName() {
            var k = this,
                j;
            if (k.__defaultSourceName == "" || k.__sourceCount == 0) {
                j = k.SourceCount
            }
            return k.__defaultSourceName
        },
        get Duplex() {
            return this._innerFun("Duplex")
        },
        get IfAppendImage() {
            return this._innerFun("IfAppendImage")
        },
        set IfAppendImage(j) {
            return this._innerFun("IfAppendImage", h.makeParams(j))
        },
        get IfAutoBright() {
            return this._innerFun("IfAutoBright")
        },
        set IfAutoBright(j) {
            return this._innerFun("IfAutoBright", h.makeParams(j))
        },
        get IfAutoDiscardBlankpages() {
            return this._innerFun("IfAutoDiscardBlankpages")
        },
        set IfAutoDiscardBlankpages(j) {
            return this._innerFun("IfAutoDiscardBlankpages", h.makeParams(j))
        },
        get IfAutoFeed() {
            return this._innerFun("IfAutoFeed")
        },
        set IfAutoFeed(j) {
            return this._innerFun("IfAutoFeed", h.makeParams(j))
        },
        get IfAutomaticBorderDetection() {
            return this._innerFun("IfAutomaticBorderDetection")
        },
        set IfAutomaticBorderDetection(j) {
            return this._innerFun("IfAutomaticBorderDetection", h.makeParams(j))
        },
        get IfAutomaticDeskew() {
            return this._innerFun("IfAutomaticDeskew")
        },
        set IfAutomaticDeskew(j) {
            return this._innerFun("IfAutomaticDeskew", h.makeParams(j))
        },
        get IfAutoScan() {
            return this._innerFun("IfAutoScan")
        },
        set IfAutoScan(j) {
            return this._innerFun("IfAutoScan", h.makeParams(j))
        },
        get IfDisableSourceAfterAcquire() {
            return this._innerFun("IfDisableSourceAfterAcquire")
        },
        set IfDisableSourceAfterAcquire(j) {
            return this._innerFun("IfDisableSourceAfterAcquire", h.makeParams(j))
        },
        get IfDuplexEnabled() {
            return this._innerFun("IfDuplexEnabled")
        },
        set IfDuplexEnabled(j) {
            return this._innerFun("IfDuplexEnabled", h.makeParams(j))
        },
        get IfFeederEnabled() {
            return this._innerFun("IfFeederEnabled")
        },
        set IfFeederEnabled(j) {
            return this._innerFun("IfFeederEnabled", h.makeParams(j))
        },
        get IfFeederLoaded() {
            return this._innerFun("IfFeederLoaded")
        },
        get IfModalUI() {
            return this._innerFun("IfModalUI")
        },
        set IfModalUI(j) {
            return this._innerFun("IfModalUI", h.makeParams(j))
        },
        get IfPaperDetectable() {
            return this._innerFun("IfPaperDetectable")
        },
        get IfScanInNewThread() {
            return this._innerFun("IfScanInNewThread")
        },
        set IfScanInNewThread(j) {
            return this._innerFun("IfScanInNewThread", h.makeParams(j))
        },
        get IfShowCancelDialogWhenImageTransfer() {
            return this.__IfShowCancelDialogWhenImageTransfer
        },
        set IfShowCancelDialogWhenImageTransfer(j) {
            return this.__IfShowCancelDialogWhenImageTransfer = j
        },
        get IfShowUI() {
            return this._innerFun("IfShowUI")
        },
        set IfShowUI(j) {
            return this._innerFun("IfShowUI", h.makeParams(j))
        },
        get IfShowIndicator() {
            return this._innerFun("IfShowIndicator")
        },
        set IfShowIndicator(j) {
            return this._innerFun("IfShowIndicator", h.makeParams(j))
        },
        get IfUseTwainDSM() {
            return this._innerFun("IfUseTwainDSM")
        },
        set IfUseTwainDSM(j) {
            return this._innerFun("IfUseTwainDSM", h.makeParams(j))
        },
        get IfUIControllable() {
            return this._innerFun("IfUIControllable")
        },
        get ImageBitsPerPixel() {
            return this._innerFun("ImageBitsPerPixel")
        },
        get ImageCaptureDriverType() {
            return this._innerFun(ImageCaptureDriverType)
        },
        set ImageCaptureDriverType(j) {
            return this._innerFun("ImageCaptureDriverType", h.makeParams(j))
        },
        get ImageLayoutDocumentNumber() {
            return this._innerFun("ImageLayoutDocumentNumber")
        },
        get ImageLayoutFrameBottom() {
            return this._innerFun("ImageLayoutFrameBottom")
        },
        get ImageLayoutFrameLeft() {
            return this._innerFun("ImageLayoutFrameLeft")
        },
        get ImageLayoutFrameNumber() {
            return this._innerFun("ImageLayoutFrameNumber")
        },
        get ImageLayoutFrameRight() {
            return this._innerFun("ImageLayoutFrameRight")
        },
        get ImageLayoutFrameTop() {
            return this._innerFun("ImageLayoutFrameTop")
        },
        get ImageLayoutPageNumber() {
            return this._innerFun("ImageLayoutPageNumber")
        },
        get ImageLength() {
            return this._innerFun("ImageLength")
        },
        get ImagePixelType() {
            return this._innerFun("ImagePixelType")
        },
        get ImageWidth() {
            return this._innerFun("ImageWidth")
        },
        get ImageXResolution() {
            return this._innerFun("ImageXResolution")
        },
        get ImageYResolution() {
            return this._innerFun("ImageYResolution")
        },
        get MagData() {
            return this._innerFun("MagData")
        },
        get MagType() {
            return this._innerFun("MagType")
        },
        get PageSize() {
            return this._innerFun("PageSize")
        },
        set PageSize(k) {
            var l = this,
                j = k * 1;
            return l._innerFun("PageSize", h.makeParams(j))
        },
        get PendingXfers() {
            return this._innerFun("PendingXfers")
        },
        get PixelFlavor() {
            return this._innerFun("PixelFlavor")
        },
        set PixelFlavor(j) {
            return this._innerFun("PixelFlavor", h.makeParams(j))
        },
        get PixelType() {
            return this._innerFun("PixelType")
        },
        set PixelType(j) {
            return this._innerFun("PixelType", h.makeParams(j))
        },
        get Resolution() {
            return this._innerFun("Resolution")
        },
        set Resolution(k) {
            var l = this,
                j = k * 1;
            return l._innerFun("Resolution", h.makeParams(j))
        },
        get SourceCount() {
            var l = this,
                k = l._innerFunRaw("GetSourceNames"),
                j = k.length - 1;
            if (l._errorCode != 0) {
                return 0
            }
            if (j <= 0) {
                l.__sourceName = [];
                l.__defaultSourceName = "";
                l.__sourceCount = 0;
                return 0
            } else {
                if (j == 1) {
                    if (k[0] == "") {
                        l.__sourceName = [];
                        l.__defaultSourceName = "";
                        l.__sourceCount = 0;
                        return 0
                    }
                }
            }
            l.__defaultSourceName = k[k.length - 1];
            k.splice(k.length - 1, 1);
            l.__sourceName = k;
            l.__sourceCount = j;
            return j
        },
        get TransferMode() {
            return this._innerFun("TransferMode")
        },
        set TransferMode(j) {
            return this._innerFun("TransferMode", h.makeParams(j))
        },
        get Unit() {
            return this._innerFun("Unit")
        },
        set Unit(j) {
            return this._innerFun("Unit", h.makeParams(j))
        },
        get XferCount() {
            return this._innerFun("XferCount")
        },
        set XferCount(k) {
            var l = this,
                j = k * 1;
            return l._innerFun("XferCount", h.makeParams(j))
        },
        get Capability() {
            return this._innerFun("Capability")
        },
        set Capability(j) {
            return this._innerFun("Capability", h.makeParams(j))
        },
        get CapCurrentIndex() {
            return this._innerFun("CapCurrentIndex")
        },
        set CapCurrentIndex(j) {
            return this._innerFun("CapCurrentIndex", h.makeParams(j))
        },
        get CapCurrentValue() {
            return this._innerFun("CapCurrentValue")
        },
        set CapCurrentValue(j) {
            return this._innerFun("CapCurrentValue", h.makeParams(j))
        },
        get CapDefaultIndex() {
            return this._innerFun("CapDefaultIndex")
        },
        get CapDefaultValue() {
            return this._innerFun("CapDefaultValue")
        },
        get CapDescription() {
            return this._innerFun("CapDescription")
        },
        set CapDescription(j) {
            return this._innerFun("CapDescription", h.makeParams(j))
        },
        get CapMaxValue() {
            return this._innerFun("CapMaxValue")
        },
        set CapMaxValue(j) {
            return this._innerFun("CapMaxValue", h.makeParams(j))
        },
        get CapMinValue() {
            return this._innerFun("CapMinValue")
        },
        set CapMinValue(j) {
            return this._innerFun("CapMinValue", h.makeParams(j))
        },
        get CapNumItems() {
            return this._innerFun("CapNumItems")
        },
        set CapNumItems(j) {
            return this._innerFun("CapNumItems", h.makeParams(j))
        },
        get CapStepSize() {
            return this._innerFun("CapStepSize")
        },
        set CapStepSize(j) {
            return this._innerFun("CapStepSize", h.makeParams(j))
        },
        get CapType() {
            return this._innerFun("CapType")
        },
        set CapType(j) {
            return this._innerFun("CapType", h.makeParams(j))
        },
        get CapValue() {
            return this._innerFun("CapValue")
        },
        set CapValue(j) {
            return this._innerFun("CapValue", h.makeParams(j))
        },
        get CapValueString() {
            return this._innerFun("CapValueString")
        },
        set CapValueString(j) {
            return this._innerFun("CapValueString", h.makeParams(j))
        },
        get CapValueType() {
            return this._innerFun("CapValueType")
        },
        set CapValueType(j) {
            return this._innerFun("CapValueType", h.makeParams(j))
        },
        get AllowMultiSelect() {
            var j = this;
            return j._UIManager.getView().getAllowMultiSelect()
        },
        set AllowMultiSelect(j) {
            var k = this;
            k._UIManager.getView().setAllowMultiSelect(j)
        },
        get BackgroundColor() {
            var j = this;
            return j.__backgroundColor
        },
        set BackgroundColor(k) {
            var n = this,
                j, m;
            n.__backgroundColor = k;
            n._UIManager.setBackgroundColor(k);
            j = d.one("#" + n._strDWTControlContainerID);
            if (j) {
                m = j.one(".ds-dwt-container-box");
                if (m) {
                    var l = k;
                    if (d.isNumber(l)) {
                        l = Dynamsoft.Lib.getColor(l)
                    }
                    m.style("background-color", l)
                }
            }
        },
        get BackgroundFillColor() {
            return this._innerFun("BackgroundFillColor")
        },
        set BackgroundFillColor(j) {
            return this._innerFun("BackgroundFillColor", h.makeParams(j))
        },
        get BlankImageCurrentStdDev() {
            return this._innerFun("BlankImageCurrentStdDev")
        },
        set BlankImageCurrentStdDev(j) {
            return this._innerFun("BlankImageCurrentStdDev", h.makeParams(j))
        },
        get BlankImageMaxStdDev() {
            return this._innerFun("BlankImageMaxStdDev")
        },
        set BlankImageMaxStdDev(j) {
            return this._innerFun("BlankImageMaxStdDev", h.makeParams(j))
        },
        get BlankImageThreshold() {
            return this._innerFun("BlankImageThreshold")
        },
        set BlankImageThreshold(j) {
            return this._innerFun("BlankImageThreshold", h.makeParams(j))
        },
        get CurrentImageIndexInBuffer() {
            return this.__cIndex
        },
        set CurrentImageIndexInBuffer(k) {
            var l = this,
                j = k * 1;
            if (j >= 0 && j < l._HowManyImagesInBuffer && j != l.__cIndex) {
                l._innerSendCmd("CurrentImageIndexInBuffer", h.makeParams(j));
                l.__cIndex = j;
                l._UIManager.getView().go(l.__cIndex, function() {
                    h.__innerRefreshFromUIView(l, l.__cIndex, true)
                }, true)
            }
            return true
        },
        get FitWindowType() {
            var j = this;
            return j._UIManager.getEditor().getFitWindowType()
        },
        set FitWindowType(k) {
            var l = this,
                j = k * 1;
            return l._UIManager.getEditor().setFitWindowType(j)
        },
        get HowManyImagesInBuffer() {
            var j = this;
            return j._HowManyImagesInBuffer
        },
        set HowManyImagesInBuffer(k) {
            var l = this,
                j = k * 1;
            l._HowManyImagesInBuffer = j;
            return true
        },
        get IfFitWindow() {
            var j = this;
            return j._UIManager.getEditor().getFitWindowType() == 0
        },
        set IfFitWindow(j) {
            var k = this;
            if (j) {
                return k._UIManager.getEditor().setFitWindowType(0)
            } else {
                return k._UIManager.getEditor().setFitWindowType(3)
            }
        },
        get ImageMargin() {
            var j = this;
            return j._UIManager.getView().getImageMargin()
        },
        set ImageMargin(j) {
            var k = this;
            return k._UIManager.getView().setImageMargin(j)
        },
        get MaxImagesInBuffer() {
            return this._innerFun("MaxImagesInBuffer")
        },
        set MaxImagesInBuffer(j) {
            return this._innerFun("MaxImagesInBuffer", h.makeParams(j))
        },
        get MouseX() {
            var j = this;
            return j.__MouseX
        },
        get MouseY() {
            var j = this;
            return j.__MouseY
        },
        get SelectedImagesCount() {
            var j = this;
            if (j._HowManyImagesInBuffer <= 0) {
                return 0
            }
            if (j.__SelectedImagesCount == 0) {
                return 1
            }
            return j.__SelectedImagesCount
        },
        set SelectedImagesCount(k) {
            var m = this,
                l, j = k * 1;
            if (m._HowManyImagesInBuffer == 0) {
                j = 0
            } else {
                if (j < 1) {
                    j = 1
                }
            }
            m.__SelectedImagesCount = j;
            m._UIManager.selectedIndexes.splice(j);
            if (m.__cIndex >= 0) {
                m._UIManager.selectedIndexes[0] = m.__cIndex
            }
            for (l = 1; l < m.__SelectedImagesCount; l++) {
                m._UIManager.selectedIndexes[l] = -1
            }
            m._UIManager.getView().unHighlightAll();
            m._UIManager.getView().hightlight(m._UIManager.selectedIndexes);
            return m._innerFun("SelectedImagesCount", h.makeParams(j))
        },
        get SelectionImageBorderColor() {
            var j = this;
            return j._UIManager.getView().getSelectionImageBorderColor()
        },
        set SelectionImageBorderColor(j) {
            var k = this;
            return k._UIManager.getView().setSelectionImageBorderColor(j)
        },
        get Zoom() {
            var j = this;
            return j._UIManager.getEditor().getZoom()
        },
        set Zoom(j) {
            var k = this;
            return k._UIManager.getEditor().setZoom(j)
        },
        get MouseShape() {
            var j = this;
            return j._UIManager.getEditor().getMouseShape()
        },
        set MouseShape(j) {
            var k = this;
            return k._UIManager.getEditor().setMouseShape(j)
        },
        get HTTPPostResponseString() {
            if (this.__HTTPPostResponseString) {
                return this.__HTTPPostResponseString
            } else {
                return ""
            }
        },
        get FTPPassword() {
            return this._innerFun("FTPPassword")
        },
        set FTPPassword(j) {
            return this._innerFun("FTPPassword", h.makeParams(j))
        },
        get FTPPort() {
            return this._innerFun("FTPPort")
        },
        set FTPPort(k) {
            if (k !== "") {
                var l = this,
                    j = k * 1;
                return l._innerFun("FTPPort", h.makeParams(j))
            }
        },
        get FTPUserName() {
            return this._innerFun("FTPUserName")
        },
        set FTPUserName(j) {
            return this._innerFun("FTPUserName", h.makeParams(j))
        },
        get IfPASVMode() {
            return this._innerFun("IfPASVMode")
        },
        set IfPASVMode(j) {
            return this._innerFun("IfPASVMode", h.makeParams(j))
        },
        get IfShowProgressBar() {
            return this.__IfShowProgressBar
        },
        set IfShowProgressBar(j) {
            return this.__IfShowProgressBar = j
        },
        get ProxyServer() {
            return this._innerFun("ProxyServer")
        },
        set ProxyServer(j) {
            return this._innerFun("ProxyServer", h.makeParams(j))
        },
        get IfShowFileDialog() {
            return this._innerFun("IfShowFileDialog")
        },
        set IfShowFileDialog(j) {
            return this._innerFun("IfShowFileDialog", h.makeParams(j))
        },
        get IfTiffMultiPage() {
            return this._innerFun("IfTiffMultiPage")
        },
        set IfTiffMultiPage(j) {
            return this._innerFun("IfTiffMultiPage", h.makeParams(j))
        },
        get PDFAuthor() {
            return this._innerFun("PDFAuthor")
        },
        set PDFAuthor(j) {
            return this._innerFun("PDFAuthor", h.makeParams(j))
        },
        get PDFCompressionType() {
            return this._innerFun("PDFCompressionType")
        },
        set PDFCompressionType(j) {
            return this._innerFun("PDFCompressionType", h.makeParams(j))
        },
        get PDFCreationDate() {
            return this._innerFun("PDFCreationDate")
        },
        set PDFCreationDate(j) {
            return this._innerFun("PDFCreationDate", h.makeParams(j))
        },
        get PDFCreator() {
            return this._innerFun("PDFCreator")
        },
        set PDFCreator(j) {
            return this._innerFun("PDFCreator", h.makeParams(j))
        },
        get PDFKeywords() {
            return this._innerFun("PDFKeywords")
        },
        set PDFKeywords(j) {
            return this._innerFun("PDFKeywords", h.makeParams(j))
        },
        get PDFModifiedDate() {
            return this._innerFun("PDFModifiedDate")
        },
        set PDFModifiedDate(j) {
            return this._innerFun("PDFModifiedDate", h.makeParams(j))
        },
        get PDFProducer() {
            return this._innerFun("PDFProducer")
        },
        set PDFProducer(j) {
            return this._innerFun("PDFProducer", h.makeParams(j))
        },
        get PDFSubject() {
            return this._innerFun("PDFSubject")
        },
        set PDFSubject(j) {
            return this._innerFun("PDFSubject", h.makeParams(j))
        },
        get PDFTitle() {
            return this._innerFun("PDFTitle")
        },
        set PDFTitle(j) {
            return this._innerFun("PDFTitle", h.makeParams(j))
        },
        get PDFVersion() {
            return this._innerFun("PDFVersion")
        },
        set PDFVersion(j) {
            return this._innerFun("PDFVersion", h.makeParams(j))
        },
        get TIFFCompressionType() {
            return this._innerFun("TIFFCompressionType")
        },
        set TIFFCompressionType(j) {
            return this._innerFun("TIFFCompressionType", h.makeParams(j))
        },
        get JPEGQuality() {
            return this._innerFun("JPEGQuality")
        },
        set JPEGQuality(k) {
            var l = this,
                j = k * 1;
            if (j >= 100) {
                j = 100
            }
            return l._innerFun("JPEGQuality", h.makeParams(j))
        },
        get SelectionRectAspectRatio() {
            return this._innerFun("SelectionRectAspectRatio")
        },
        set SelectionRectAspectRatio(j) {
            return this._innerFun("SelectionRectAspectRatio", h.makeParams(j))
        },
        get IfAllowLocalCache() {
            return this._innerFun("IfAllowLocalCache")
        },
        set IfAllowLocalCache(j) {
            return this._innerFun("IfAllowLocalCache", h.makeParams(j))
        },
        get BrokerProcessType() {
            return true
        },
        set BrokerProcessType(j) {
            return true
        },
        get BorderStyle() {
            return this._innerFun("BorderStyle")
        },
        set BorderStyle(j) {
            return this._innerFun("BorderStyle", h.makeParams(j))
        },
        get IfSSL() {
            return this._innerFun("IfSSL")
        },
        set IfSSL(j) {
            return this._innerFun("IfSSL", h.makeParams(j))
        },
        get AllowPluginAuthentication() {
            return this._innerFun("AllowPluginAuthentication")
        },
        set AllowPluginAuthentication(j) {
            return this._innerFun("AllowPluginAuthentication", h.makeParams(j))
        },
        get HttpFieldNameOfUploadedImage() {
            return this.__remoteFilename
        },
        set HttpFieldNameOfUploadedImage(j) {
            return this.__remoteFilename = j
        },
        get MaxInternetTransferThreads() {
            return this._innerFun("MaxInternetTransferThreads")
        },
        set MaxInternetTransferThreads(j) {
            return this._innerFun("MaxInternetTransferThreads", h.makeParams(j))
        },
        get MaxUploadImageSize() {
            return this._innerFun("MaxUploadImageSize")
        },
        set MaxUploadImageSize(j) {
            return this._innerFun("MaxUploadImageSize", h.makeParams(j))
        },
        get IfOpenImageWithGDIPlus() {
            return this._innerFun("IfOpenImageWithGDIPlus")
        },
        set IfOpenImageWithGDIPlus(j) {
            return this._innerFun("IfOpenImageWithGDIPlus", h.makeParams(j))
        },
        get style() {
            return this.__style
        },
        get Addon() {
            return this.__addon
        },
        get BufferMemoryLimit() {
            return this._innerFun("BufferMemoryLimit")
        },
        set BufferMemoryLimit(j) {
            return this._innerFun("BufferMemoryLimit", h.makeParams(j))
        },
        get Width() {
            var j = this;
            return j._iWidth
        },
        set Width(j) {
            var m = this,
                l;
            if (d.isString(j)) {
                if (j.length > 0) {
                    if (j.charAt(j.length - 1) === "%") {
                        l = j
                    }
                }
            }
            if (!l) {
                var k = parseInt(j);
                if (k) {
                    l = k + "px"
                }
            }
            if (l) {
                h.changeImageUISize(m, l, -1)
            }
        },
        get Height() {
            var j = this;
            return j._iHeight
        },
        set Height(j) {
            var m = this,
                l;
            if (d.isString(j)) {
                if (j.length > 0) {
                    if (j.charAt(j.length - 1) === "%") {
                        l = j
                    }
                }
            }
            if (!l) {
                var k = parseInt(j);
                if (k) {
                    l = k + "px"
                }
            }
            if (l) {
                h.changeImageUISize(m, -1, l)
            }
        },
        get ImageEditorIfEnableEnumerator() {
            return true
        },
        set ImageEditorIfEnableEnumerator(j) {
            return true
        },
        get ImageEditorIfReadonly() {
            return true
        },
        set ImageEditorIfReadonly(j) {
            return true
        },
        get ImageEditorIfModal() {
            return true
        },
        set ImageEditorIfModal(j) {
            return true
        },
        get ImageEditorWindowTitle() {
            return true
        },
        set ImageEditorWindowTitle(j) {
            return true
        },
        get EnableInteractiveZoom() {
            return true
        },
        set EnableInteractiveZoom(j) {
            return true
        },
        get IfShowPrintUI() {
            return true
        },
        set IfShowPrintUI(j) {
            return true
        },
        get VScrollBar() {
            return true
        },
        set VScrollBar(j) {
            return true
        },
        get AsyncMode() {
            return true
        },
        set AsyncMode(j) {
            return true
        },
        get IfThrowException() {
            return true
        },
        set IfThrowException(j) {
            return true
        }
    };
    i.prototype.getInstance = function() {
        return this
    };
    i.prototype.RegisterEvent = function(j, l) {
        var k;
        if (j === "OnTopImageInTheViewChanged") {
            k = "__OnRefreshUI"
        } else {
            k = ["__", j].join("")
        }
        this[k] = l
    };
    i.prototype.onEvent = function(j, k) {
        this.RegisterEvent(j, k)
    };
    i.prototype.on = function(j, k) {
        this.RegisterEvent(j, k)
    };
    i.prototype.first = function() {
        var k = this,
            j = k._UIManager.count();
        if (j > 0) {
            k._UIManager.getView().go(0, function() {
                h.__innerRefreshFromUIView(k, k._UIManager.getView().cIndex)
            })
        }
    };
    i.prototype.previous = function() {
        var k = this,
            j = k._UIManager.count();
        if (j > 0) {
            k._UIManager.getView().previous(function() {
                h.__innerRefreshFromUIView(k, k._UIManager.getView().cIndex)
            })
        }
    };
    i.prototype.next = function() {
        var k = this,
            j = k._UIManager.count();
        if (j > 0) {
            k._UIManager.getView().next(function() {
                h.__innerRefreshFromUIView(k, k._UIManager.getView().cIndex)
            })
        }
    };
    i.prototype.last = function() {
        var k = this,
            j = k._UIManager.count();
        if (j > 0) {
            k._UIManager.getView().go(j - 1, function() {
                h.__innerRefreshFromUIView(k, k._UIManager.getView().cIndex)
            })
        }
    };
    i.prototype.CancelAllPendingTransfers = function() {
        return this._innerFun("CancelAllPendingTransfers")
    };
    i.prototype.CloseSource = function() {
        return this._innerFun("CloseSource")
    };
    i.prototype.CloseSourceManager = function() {
        return this._innerFun("CloseSourceManager")
    };
    i.prototype.DisableSource = function() {
        return this._innerFun("DisableSource")
    };
    i.prototype.FeedPage = function() {
        return this._innerFun("FeedPage")
    };
    i.prototype.GetDeviceType = function() {
        return this._innerFun("GetDeviceType")
    };
    i.prototype.GetSourceNameItems = function(j) {
        if (j < this.__sourceCount && j >= 0) {
            return this.__sourceName[j]
        } else {
            return ""
        }
    };
    i.prototype.SourceNameItems = function(j) {
        if (j < this.__sourceCount && j >= 0) {
            return this.__sourceName[j]
        } else {
            return ""
        }
    };
    i.prototype.GetSourceNames = function() {
        var k = this,
            j = k._innerFunRaw("GetSourceNames");
        if (k._errorCode != 0) {
            return []
        }
        return j
    };
    i.prototype.GetSourceType = function(j) {
        return this._innerFun("GetSourceType", h.makeParams(j))
    };
    i.prototype.OpenSource = function() {
        return this._innerFun("OpenSource")
    };
    i.prototype.OpenSourceManager = function() {
        return this._innerFun("OpenSourceManager")
    };
    i.prototype.ResetImageLayout = function() {
        return this._innerFun("ResetImageLayout")
    };
    i.prototype.RewindPage = function() {
        return this._innerFun("RewindPage")
    };
    i.prototype.SelectSource = function() {
        return this._innerFun("SelectSource")
    };
    i.prototype.SelectSourceByIndex = function(j) {
        return this._innerFun("SelectSourceByIndex", h.makeParams(j))
    };
    i.prototype.SetFileXferInfo = function(j, l) {
        var k = h.replaceLocalFilename(j);
        return this._innerFun("SetFileXferInfo", h.makeParams(k, l))
    };
    i.prototype.SetImageLayout = function(m, l, k, j) {
        return this._innerFun("SetImageLayout", h.makeParams(m, l, k, j))
    };
    i.prototype.CapGet = function() {
        return this._innerFun("CapGet")
    };
    i.prototype.CapGetCurrent = function() {
        return this._innerFun("CapGetCurrent")
    };
    i.prototype.CapGetDefault = function() {
        return this._innerFun("CapGetDefault")
    };
    i.prototype.CapGetFrameBottom = function(j) {
        return this._innerFun("CapGetFrameBottom", h.makeParams(j))
    };
    i.prototype.CapGetFrameLeft = function(j) {
        return this._innerFun("CapGetFrameLeft", h.makeParams(j))
    };
    i.prototype.CapGetFrameRight = function(j) {
        return this._innerFun("CapGetFrameRight", h.makeParams(j))
    };
    i.prototype.CapGetFrameTop = function(j) {
        return this._innerFun("CapGetFrameTop", h.makeParams(j))
    };
    i.prototype.CapGetHelp = function() {
        return this._innerFun("CapGetHelp")
    };
    i.prototype.CapGetLabel = function() {
        return this._innerFun("CapGetLabel")
    };
    i.prototype.CapGetLabels = function() {
        return this._innerFun("CapGetLabels")
    };
    i.prototype.CapIfSupported = function(j) {
        return this._innerFun("CapIfSupported", h.makeParams(j))
    };
    i.prototype.CapReset = function() {
        return this._innerFun("CapReset")
    };
    i.prototype.CapSet = function() {
        return this._innerFun("CapSet")
    };
    i.prototype.CapSetFrame = function(k, n, m, l, j) {
        return this._innerFun("CapSetFrame", h.makeParams(k, n, m, l, j))
    };
    i.prototype.GetCapItems = function(j) {
        return this._innerFun("GetCapItems", h.makeParams(j))
    };
    i.prototype.GetCapItemsString = function(j) {
        return this._innerFun("GetCapItemsString", h.makeParams(j))
    };
    i.prototype.SetCapItems = function(k, j) {
        return this._innerFun("SetCapItems", h.makeParams(k, j))
    };
    i.prototype.SetCapItemsString = function(k, j) {
        return this._innerFun("SetCapItemsString", h.makeParams(k, j))
    };
    i.prototype.AddText = function(o, j, q, p, n, l, m, k) {
        return this._innerFun("AddText", h.makeParams(o, j, q, p, n, l, m, k))
    };
    i.prototype.CreateTextFont = function(l, w, q, p, s, t, m, u, j, r, n, o, v, k) {
        return this._innerFun("CreateTextFont", h.makeParams(l, w, q, p, s, t, m, u, j, r, n, o, v, k))
    };
    i.prototype.CopyToClipboard = function(j) {
        return this._innerFun("CopyToClipboard", h.makeParams(j))
    };
    i.prototype.Erase = function(l, n, m, k, j) {
        return this._innerFun("Erase", h.makeParams(l, n, m, k, j))
    };
    i.prototype.GetImageBitDepth = function(j) {
        return this._innerFun("GetImageBitDepth", h.makeParams(j))
    };
    i.prototype.GetImageWidth = function(j) {
        return this._innerFun("GetImageWidth", h.makeParams(j))
    };
    i.prototype.GetImageHeight = function(j) {
        return this._innerFun("GetImageHeight", h.makeParams(j))
    };
    i.prototype.GetImageSize = function(k, j, l) {
        return this._innerFun("GetImageSize", h.makeParams(k, j, l))
    };
    i.prototype.GetImageSizeWithSpecifiedType = function(j, k) {
        return this._innerFun("GetImageSizeWithSpecifiedType", h.makeParams(j, k))
    };
    i.prototype.GetImageXResolution = function(j) {
        return this._innerFun("GetImageXResolution", h.makeParams(j))
    };
    i.prototype.GetImageYResolution = function(j) {
        return this._innerFun("GetImageYResolution", h.makeParams(j))
    };
    i.prototype.GetSelectedImageIndex = function(j) {
        var k = this;
        if (j < k.__SelectedImagesCount && j >= 0) {
            return k._UIManager.selectedIndexes[j]
        }
        return -1
    };
    i.prototype.SetSelectedImageIndex = function(l, k) {
        var m = this,
            j = m.__SelectedImagesCount;
        if (l < m.__SelectedImagesCount && l >= 0) {
            m._UIManager.selectedIndexes[l] = k;
            m._UIManager.getView().unHighlightAll();
            m._UIManager.getView().hightlight(m._UIManager.selectedIndexes);
            return this._innerFun("SetSelectedImageIndex", h.makeParams(l, k))
        }
        return false
    };
    i.prototype.GetSelectedImagesSize = function(k) {
        var l = this,
            j;
        j = c.__SetSelectedImages(l, l.__cIndex, l._UIManager.selectedIndexes);
        if (j) {
            return this._innerFun("GetSelectedImagesSize", h.makeParams(k))
        }
        return false
    };
    i.prototype.GetSkewAngle = function(j) {
        return this._innerFun("GetSkewAngle", h.makeParams(j))
    };
    i.prototype.GetSkewAngleEx = function(l, n, m, k, j) {
        return this._innerFun("GetSkewAngleEx", h.makeParams(l, n, m, k, j))
    };
    i.prototype.IsBlankImageEx = function(l, n, m, k, j, o) {
        return this._innerFun("IsBlankImageEx", h.makeParams(l, n, m, k, j, o))
    };
    i.prototype.Mirror = function(j) {
        return this._innerFun("Mirror", h.makeParams(j))
    };
    i.prototype.OverlayRectangle = function(n, p, o, l, k, j, m) {
        var q = this;
        if (h.isServerInvalid(q)) {
            return false
        }
        return this._UIManager.getEditor().OverlayRectangle(n, p, o, l, k, j, m)
    };
    i.prototype.RemoveAllImages = function() {
        var k = this,
            j;
        j = k._innerFun("RemoveAllImages");
        if (j) {
            k._UIManager.clear()
        }
        return j
    };
    i.prototype.RemoveAllSelectedImages = function() {
        var k = this,
            j;
        j = k._innerFun("RemoveAllSelectedImages");
        if (j) {
            k._UIManager.getView().RemoveAllSelectedImages();
            k.__SelectedImagesCount = 0;
            if (k._HowManyImagesInBuffer == 0) {
                k._UIManager.clear()
            }
        }
        return j
    };
    i.prototype.RemoveImage = function(k) {
        var l = this,
            j;
        j = l._innerFun("RemoveImage", h.makeParams(k));
        if (j) {
            if (l._HowManyImagesInBuffer == 0) {
                l._UIManager.clear()
            }
        }
        return j
    };
    i.prototype.Rotate = function(m, k, l) {
        var p = this,
            j = parseInt(m),
            n = p._innerFun("Rotate", h.makeParams(j, k, l)),
            o = (n == 1);
        if (o) {
            h.refreshImageAfterInvokeFun(p, j)
        }
        return o
    };
    i.prototype.RotateEx = function(n, l, m, j) {
        var q = this,
            k = parseInt(n),
            o = q._innerFun("RotateEx", h.makeParams(k, l, m, j)),
            p = (o == 1);
        if (p) {
            h.refreshImageAfterInvokeFun(q, k)
        }
        return p
    };
    i.prototype.RotateLeft = function(k) {
        var n = this,
            j = parseInt(k),
            l = n._innerFun("RotateLeft", h.makeParams(j)),
            m = (l == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, j)
        }
        return m
    };
    i.prototype.RotateRight = function(k) {
        var n = this,
            j = parseInt(k),
            l = n._innerFun("RotateRight", h.makeParams(j)),
            m = (l == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, j)
        }
        return m
    };
    i.prototype.ChangeImageSize = function(t, k, p, s) {
        var m = this,
            n = parseInt(t),
            q = 1 * k,
            o = 1 * p,
            j, l;
        if (q <= 0 || o <= 0) {
            Dynamsoft.Lib.Errors.InvalidWidthOrHeight(m);
            return false
        }
        j = m._innerFun("ChangeImageSize", h.makeParams(n, q, o, s));
        l = (j == 1);
        if (l) {
            h.refreshImageAfterInvokeFun(m, n)
        }
        return l
    };
    i.prototype.Flip = function(k) {
        var n = this,
            j = parseInt(k),
            l = n._innerFun("Flip", h.makeParams(j)),
            m = (l == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, j)
        }
        return m
    };
    i.prototype.Crop = function(s, l, p, q, k) {
        var n = this,
            o = parseInt(s),
            j = n._innerFun("Crop", h.makeParams(o, l, p, q, k)),
            m = (j == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, o)
        }
        return m
    };
    i.prototype.CropToClipboard = function(s, l, p, q, k) {
        var n = this,
            o = parseInt(s),
            j = n._innerFun("CropToClipboard", h.makeParams(o, l, p, q, k)),
            m = (j == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, o)
        }
        return m
    };
    i.prototype.CutFrameToClipboard = function(s, l, p, q, k) {
        var n = this,
            o = parseInt(s),
            j = n._innerFun("CutFrameToClipboard", h.makeParams(o, l, p, q, k)),
            m = (j == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, o)
        }
        return m
    };
    i.prototype.CutToClipboard = function(k) {
        var n = this,
            j = parseInt(k),
            l = n._innerFun("CutToClipboard", h.makeParams(j)),
            m = (l == 1);
        if (m) {
            h.refreshImageAfterInvokeFun(n, j)
        }
        return m
    };
    i.prototype.SetDPI = function(n, k, m, j, l) {
        return this._innerFun("SetDPI", h.makeParams(n, k, m, j, l))
    };
    i.prototype.SetViewMode = function(k, j) {
        var l = this;
        l._UIManager.SetViewMode(k, j)
    };
    i.prototype.MoveImage = function(l, k) {
        var m = this,
            j = m._innerFun("MoveImage", h.makeParams(l, k));
        if (j) {
            m._UIManager.MoveImage(l, k)
        }
        return j
    };
    i.prototype.SwitchImage = function(l, k) {
        var m = this,
            j = m._innerFun("SwitchImage", h.makeParams(l, k));
        if (j) {
            m._UIManager.SwitchImage(l, k)
        }
        return j
    };
    i.prototype.Print = function() {
        var j = this;
        j._UIManager.getView().print()
    };
    i.prototype.SetSelectedImageArea = function(l, n, m, k, j) {
        var o = this;
        if (h.isServerInvalid(o)) {
            return false
        }
        return o._UIManager.getEditor().SetSelectedImageArea(n, m, k, j)
    };
    i.prototype.OnRefreshUI = function(n, l, o) {
        var q = this,
            j = "On RefreshUI";
        if (o) {
            var p = n * 1,
                k = q._UIManager.get(p);
            k.ticks = h.getRandom();
            q._UIManager.set(k, p);
            q._UIManager.getEditor().refresh(p, l, true)
        }
        if (h.isFunction(q.__OnRefreshUI)) {
            q.__OnRefreshUI(q._UIManager.getView().cIndex, l)
        }
        return true
    };
    i.prototype._OnBitmapChanged = function(l) {
        var q = this,
            k = l[1].split(","),
            p = l[2],
            n = l[3],
            o = l[4],
            j = "OnBitmapChanged:";
        q.__cIndex = n;
        q._HowManyImagesInBuffer = o;
        a.log(j + l);
        if (p == 1 || p == 2) {
            d.each(k, function(t) {
                var v = parseInt(t),
                    r = "item_" + v;
                if (isNaN(v) || v < 0) {
                    return true
                }
                var m = h.getServerSmallImageUrl(q, v, 0),
                    u = h.getServerImageUrlPrefix(q, v),
                    s = {
                        id: r,
                        src: m,
                        urlPrefix: u,
                        width: 0,
                        height: 0,
                        ticks: h.getRandom(),
                        bNew: true
                    };
                q._UIManager.add(s, v, o, p)
            });
            if (h.isFunction(q.__OnRefreshUI)) {
                q.__OnRefreshUI(undefined, o)
            }
        } else {
            if (p == 3) {
                if (k.length == 1 && k[0] == -1 || o == 0) {
                    q._UIManager.clear()
                } else {
                    d.each(k, function(r, m) {
                        var s = parseInt(r);
                        if (isNaN(s) || s < 0) {
                            return true
                        }
                        q._UIManager.remove(s, n)
                    });
                    if (n >= 0 && !q.__bLoadingImage && q._UIManager.count() > 0) {
                        q._UIManager.getEditor().refresh(n, o)
                    }
                }
                if (h.isFunction(q.__OnRefreshUI)) {
                    q.__OnRefreshUI(n, o)
                }
            } else {
                if (p == 4) {
                    d.each(k, function(r, m) {
                        var s = parseInt(r);
                        if (isNaN(s) || s < 0) {
                            return true
                        }
                        if (s >= 0 && !q.__bLoadingImage) {
                            q.OnRefreshUI(s, o, true)
                        }
                    })
                }
            }
        }
        if (h.isFunction(q.__OnBitmapChanged)) {
            q.__OnBitmapChanged(k, p, n, o)
        }
    };
    i.prototype._OnPostLoad = function(j) {
        var q = this,
            l, o, m, n, k;
        a.log("OnPostLoad Results:");
        n = j[1].split(",");
        o = a.base64.decode(n[0]);
        name = a.base64.decode(n[1]);
        a.log(o);
        a.log(name);
        a.log(n[2]);
        h.autoDiscardBlankImage(q, "On PostLoad");
        k = q.CurrentImageIndexInBuffer;
        if (k >= 0) {
            q.OnRefreshUI(k)
        }
        if (h.isFunction(q.__OnPostLoad)) {
            q.__OnPostLoad(o, name, n[2])
        }
    };
    i.prototype._OnPostTransfer = function(j) {
        var k = this;
        a.log("On PostTransfer Results:" + j);
        h.autoDiscardBlankImage(k, "On PostTransfer");
        if (h.isFunction(k.__OnPostTransfer)) {
            k.__OnPostTransfer()
        }
    };
    i.prototype._OnPostAllTransfers = function(j) {
        var m = this,
            k = m.__cIndex,
            l = m._HowManyImagesInBuffer;
        a.log("On PostAllTransfers Results:" + j);
        m.__bLoadingImage = false;
        if (h.isFunction(m.__OnPostAllTransfers)) {
            m.__OnPostAllTransfers()
        }
    };
    i.prototype._OnResult = function(j) {
        var l = this,
            k = j[1];
        if (h.isFunction(l.__OnResult)) {
            l.__OnResult(k)
        }
    };
    i.prototype._OnTransferCancelled = function(j) {
        var m = this,
            k = m.__cIndex,
            l = m._HowManyImagesInBuffer;
        m.__bLoadingImage = false;
        m._UIManager.getEditor().refresh(k, l);
        if (h.isFunction(m.__OnTransferCancelled)) {
            m.__OnTransferCancelled()
        }
    };
    i.prototype._OnTransferError = function(j) {
        var k = this;
        if (h.isFunction(k.__OnTransferError)) {
            k.__OnTransferError()
        }
    };
    i.prototype._OnSourceUIClose = function(j) {
        var k = this;
        if (h.isFunction(k.__OnSourceUIClose)) {
            k.__OnSourceUIClose()
        }
    };
    i.prototype._OnPreTransfer = function(j) {
        var k = this;
        if (h.isFunction(k.__OnPreTransfer)) {
            k.__OnPreTransfer()
        }
    };
    i.prototype._OnPreAllTransfers = function(j) {
        var k = this;
        if (h.isFunction(k.__OnPreAllTransfers)) {
            k.__OnPreAllTransfers()
        }
    };
    i.prototype._OnPercentDone = function(s) {
        var o = this;
        var t = false;
        if ((Dynamsoft.Lib.cancelFrome == 0 && o.__IfShowProgressBar == true) || (Dynamsoft.Lib.cancelFrome != 0 && o.__IfShowCancelDialogWhenImageTransfer == true)) {
            var r = d.one("#progressBar");
            var n = d.one("#status");
            var p, q;
            if (Dynamsoft.Lib.cancelFrome == 3 || Dynamsoft.Lib.cancelFrome == 4) {
                var l = s[3];
                if (l == 0) {
                    h.setBtnCancelVisibleForProgress(o, false)
                } else {
                    if (l == 1) {
                        h.setBtnCancelVisibleForProgress(o, true)
                    }
                }
            }
            if (s[1] == -1) {
                p = 0;
                Dynamsoft.Lib.progressMessage = s[2];
                q = Dynamsoft.Lib.progressMessage
            } else {
                p = s[1];
                if (s[2] != "") {
                    q = Dynamsoft.Lib.progressMessage + "(" + s[2] + ")"
                } else {
                    q = Dynamsoft.Lib.progressMessage
                }
                if (Dynamsoft.Lib.cancelFrome == 1 || Dynamsoft.Lib.cancelFrome == 2) {
                    if (s[3] == "http") {
                        t = true
                    }
                }
                if (Dynamsoft.Lib.cancelFrome == 3 || Dynamsoft.Lib.cancelFrome == 4) {
                    if (s[4] == "ftp") {
                        t = true
                    }
                }
            }
            var j = d.one("#finalMessage");
            j.html(q);
            if (p) {
                n.html(p + "%");
                r[0].value = p;
                if (t == true) {
                    if (h.isFunction(o.__OnInternetTransferPercentage)) {
                        o.__OnInternetTransferPercentage(p, false)
                    }
                    if (h.isFunction(o.__OnInternetTransferPercentageEx)) {
                        o.__OnInternetTransferPercentageEx(p, false)
                    }
                }
            }
            if (p && (p >= 100 || Dynamsoft.Lib.dlgRef <= 0)) {
                if (Dynamsoft.Lib.dlgRef <= 0) {
                    var k = "OnPercentDone";
                    Dynamsoft.Lib.closeProgress(k)
                }
            }
        }
        if (h.isFunction(o.__OnPercentDone)) {
            o.__OnPercentDone()
        }
    };
    i.prototype._OnGetFilePath = function(j) {
        var o = this,
            k, n, l, m;
        m = j[1].split(",");
        a.log("On GetFilePath Results:" + j);
        n = a.base64.decode(m[3]);
        name = a.base64.decode(m[4]);
        if (h.isFunction(o.__OnGetFilePath)) {
            o.__OnGetFilePath(m[0], m[1], m[2], n, name)
        }
    };
    i.prototype.checkErrorString = function() {
        var l = this,
            k = l.ErrorCode;
        if (k === -2115) {
            return true
        }
        if (k === -2003) {
            var j = window.open("", "ErrorMessage", "height=500,width=750,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
            j.document.writeln(l.__HTTPPostResponseString)
        }
        return (k === 0)
    };
    i.prototype.FileExists = function(j) {
        var k = h.replaceLocalFilename(j);
        return this._innerFun("FileExists", h.makeParams(k))
    };
    i.prototype.SaveAllAsMultiPageTIFF = function(j, m, k) {
        var l = h.replaceLocalFilename(j);
        return this.__innerSaveImage("SaveAllAsMultiPageTIFF", h.makeParams(l), m, k)
    };
    i.prototype.SaveAllAsPDF = function(j, m, k) {
        var l = h.replaceLocalFilename(j);
        return this.__innerSaveImage("SaveAllAsPDF", h.makeParams(l), m, k)
    };
    i.prototype.SaveAsBMP = function(j, k) {
        var l = h.replaceLocalFilename(j);
        return this._innerFun("SaveAsBMP", h.makeParams(l, k))
    };
    i.prototype.SaveAsJPEG = function(j, k) {
        var l = h.replaceLocalFilename(j);
        return this._innerFun("SaveAsJPEG", h.makeParams(l, k))
    };
    i.prototype.SaveAsPDF = function(j, k) {
        var l = h.replaceLocalFilename(j);
        return this._innerFun("SaveAsPDF", h.makeParams(l, k))
    };
    i.prototype.SaveAsPNG = function(j, k) {
        var l = h.replaceLocalFilename(j);
        return this._innerFun("SaveAsPNG", h.makeParams(l, k))
    };
    i.prototype.SaveAsTIFF = function(j, k) {
        var l = h.replaceLocalFilename(j);
        return this._innerFun("SaveAsTIFF", h.makeParams(l, k))
    };
    i.prototype.SaveSelectedImagesAsMultiPagePDF = function(k, n, l) {
        var o = this,
            m = h.replaceLocalFilename(k),
            j;
        j = c.__SetSelectedImages(o, o.__cIndex, o._UIManager.selectedIndexes);
        if (j) {
            return o.__innerSaveImage("SaveSelectedImagesAsMultiPagePDF", h.makeParams(m), n, l)
        }
        return false
    };
    i.prototype.SaveSelectedImagesAsMultiPageTIFF = function(k, n, l) {
        var o = this,
            m = h.replaceLocalFilename(k),
            j;
        j = c.__SetSelectedImages(o, o.__cIndex, o._UIManager.selectedIndexes);
        if (j) {
            return o.__innerSaveImage("SaveSelectedImagesAsMultiPageTIFF", h.makeParams(m), n, l)
        }
        return false
    };
    i.prototype.SaveSelectedImagesToBase64Binary = function() {
        var k = this,
            j;
        j = c.__SetSelectedImages(k, k.__cIndex, k._UIManager.selectedIndexes);
        if (j) {
            return k._innerFun("SaveSelectedImagesToBase64Binary")
        }
        return false
    };
    i.prototype.ShowFileDialog = function(m, k, q, p, o, j, l, r) {
        var n = h.replaceLocalFilename(o);
        return this._innerSend("ShowFileDialog", h.makeParams(m, k, q, p, n, j, l, r), true)
    };
    i.prototype.FTPDownload = function(m, l, k, j) {
        return this.__innerFTPDownload("FTPDownload", h.makeParams(m, l), k, j)
    };
    i.prototype.FTPDownloadDirectly = function(o, n, j, m, k) {
        var l = h.replaceLocalFilename(j);
        return this.__innerFTPDownloadDirectly("FTPDownloadDirectly", h.makeParams(o, n, l), m, k)
    };
    i.prototype.FTPDownloadEx = function(n, m, k, l, j) {
        return this.__innerFTPDownload("FTPDownloadEx", h.makeParams(n, m, k), l, j)
    };
    i.prototype.FTPUpload = function(n, k, m, l, j) {
        return this.__innerFTPUpload("FTPUpload", h.makeParams(n, k, m), l, j)
    };
    i.prototype.FTPUploadDirectly = function(o, j, n, m, k) {
        var l = h.replaceLocalFilename(j);
        return this.__innerFTPUpload("FTPUploadDirectly", h.makeParams(o, l, n), m, k)
    };
    i.prototype.FTPUploadEx = function(o, k, n, l, m, j) {
        return this.__innerFTPUpload("FTPUploadEx", h.makeParams(o, k, n, l), m, j)
    };
    i.prototype.FTPUploadAllAsMultiPageTIFF = function(m, l, k, j) {
        return this.__innerFTPUpload("FTPUploadAllAsMultiPageTIFF", h.makeParams(m, l), k, j)
    };
    i.prototype.FTPUploadAllAsPDF = function(m, l, k, j) {
        return this.__innerFTPUpload("FTPUploadAllAsPDF", h.makeParams(m, l), k, j)
    };
    i.prototype.FTPUploadAsMultiPagePDF = function(m, l, k, j) {
        return this.__innerFTPUpload("FTPUploadAsMultiPagePDF", h.makeParams(m, l), k, j)
    };
    i.prototype.FTPUploadAsMultiPageTIFF = function(m, l, k, j) {
        return this.__innerFTPUpload("FTPUploadAsMultiPageTIFF", h.makeParams(m, l), k, j)
    };
    i.prototype.HTTPDownload = function(l, k, n, j) {
        var m = h.getImageType(k);
        return this.HTTPDownloadThroughGet(l, k, m, n, j)
    };
    i.prototype.HTTPDownloadDirectly = function(t, j, n, s, l) {
        var p = this,
            k = h.combineUrl(p, t, j),
            q, o;
        Dynamsoft.Lib.cancelFrome = 2;
        if (!n || n == "") {
            Dynamsoft.Lib.Errors.InvalidLocalFilename(p, "HTTPDownloadDirectly");
            if (h.isFunction(l)) {
                l(p.ErrorCode, p.ErrorString)
            }
            return false
        }
        if (h.isServerInvalid(p)) {
            if (h.isFunction(l)) {
                l(p.ErrorCode, p.ErrorString)
            }
            return false
        }
        o = h.replaceLocalFilename(n);
        q = "get";
        a.showProgress(p, "HTTPDownloadDirectly", true);
        var r = function(u) {
                var v = (u.total === 0) ? 100 : Math.round(u.loaded * 100 / u.total),
                    w = [u.loaded, " / ", u.total].join("");
                p._OnPercentDone([0, v, "", "http"])
            },
            m = true;
        p._OnPercentDone([0, -1, "HTTP Downloading...", "http"]);
        if (!h.isFunction(s)) {
            m = false
        }
        ret = h.loadHttpBlob(p, q, k, m, function(u) {
            p._OnPercentDone([0, -1, "Loading..."]);
            var v = 3;
            p.__LoadImageFromBytes(u, v, o, m, s, l)
        }, function() {
            a.closeProgress("HTTPDownloadDirectly");
            if (h.isFunction(l)) {
                l(p.ErrorCode, p.ErrorString)
            }
        }, r);
        return ret
    };
    i.prototype.HTTPUploadThroughPostDirectly = function(u, q, j, o, t, n) {
        var s = this,
            k = "HTTPUploadThroughPostDirectly",
            l = h.combineUrl(s, u, j),
            p = true,
            r;
        if (!h.isFunction(t)) {
            p = false
        }
        Dynamsoft.Lib.cancelFrome = 1;
        var v = function() {
            if (h.isFunction(n)) {
                n(s.ErrorCode, s.ErrorString, s.__HTTPPostResponseString)
            }
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            a.closeProgress(k);
            return true
        };
        Dynamsoft.Lib.needShowTwiceShowDialog = true;
        a.showProgress(s, k, false);
        if (u === "") {
            Dynamsoft.Lib.Errors.HttpServerCannotEmpty(s);
            v();
            return false
        }
        r = h.replaceLocalFilename(q);
        s.__SaveLocalFileToBytes(r, p, function(m) {
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            if (m && (Dynamsoft.Lib.dlgRef > 0 || s.__IfShowCancelDialogWhenImageTransfer == false || p == false)) {
                m.name = o;
                h.setBtnCancelVisibleForProgress(s, true);
                h.httpPostUpload(s, l, m, p, function(w) {
                    a.closeProgress(k);
                    if (h.isFunction(t)) {
                        t(w)
                    }
                }, v)
            } else {
                a.closeProgress(k)
            }
        }, v);
        return true
    };
    i.prototype.HTTPUploadThroughPost = function(m, l, j, p, o, k) {
        var n = h.getImageType(p);
        return this.HTTPUploadThroughPostEx(m, l, j, p, n, o, k)
    };
    i.prototype.HTTPUploadThroughPostEx = function(v, x, j, o, q, u, n) {
        var r = this,
            k = "HTTPUploadThroughPostEx",
            l = h.combineUrl(r, v, j),
            p = true,
            s = false;
        if (!h.isFunction(u)) {
            p = false
        }
        Dynamsoft.Lib.cancelFrome = 1;
        var w = function() {
            if (h.isFunction(n)) {
                n(r.ErrorCode, r.ErrorString, r.__HTTPPostResponseString)
            }
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            a.closeProgress(k);
            if (r.curCommand_SaveImagesToBytes.length > 0) {
                Dynamsoft.Lib.cancelFrome = 1;
                Dynamsoft.Lib.needShowTwiceShowDialog = true;
                a.showProgress(r, k, false);
                var m = r.curCommand_SaveImagesToBytes[0];
                h.sendData(m.objWS, m.json, false, m.binary)
            }
            return true
        };
        Dynamsoft.Lib.needShowTwiceShowDialog = true;
        a.showProgress(r, k, false);
        if (v === "") {
            Dynamsoft.Lib.Errors.HttpServerCannotEmpty(r);
            w();
            return false
        }
        if (x == -2) {
            var t;
            t = c.__SetSelectedImages(r, r.__cIndex, r._UIManager.selectedIndexes);
            if (!t) {
                w(r._errorString);
                return false
            }
        }
        r.__SaveSelectedImagesToBytes(q, x, p, function(m) {
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            if (m && (Dynamsoft.Lib.dlgRef > 0 || r.__IfShowCancelDialogWhenImageTransfer == false || p == false)) {
                m.name = o;
                h.setBtnCancelVisibleForProgress(r, true);
                h.httpPostUpload(r, l, m, p, function(y) {
                    a.closeProgress(k);
                    s = true;
                    if (h.isFunction(u)) {
                        u(y)
                    }
                    if (r.curCommand_SaveImagesToBytes.length > 0) {
                        Dynamsoft.Lib.cancelFrome = 1;
                        Dynamsoft.Lib.needShowTwiceShowDialog = true;
                        a.showProgress(r, k, false);
                        var z = r.curCommand_SaveImagesToBytes[0];
                        h.sendData(z.objWS, z.json, false, z.binary)
                    }
                }, w)
            } else {
                a.closeProgress(k)
            }
        }, w);
        return s
    };
    i.prototype.ClearAllHTTPFormField = function() {
        var j = this;
        j.httpFormFields = {}
    };
    i.prototype.SetHTTPFormField = function(m, j) {
        var l = this,
            k = m;
        if (!d.isString(k)) {
            k = "" + k
        }
        l.httpFormFields[k] = j
    };
    i.prototype.HTTPUploadAllThroughPostAsMultiPageTIFF = function(m, j, p, o, l) {
        var k = -1,
            n = 2;
        return this.HTTPUploadThroughPostEx(m, k, j, p, n, o, l)
    };
    i.prototype.HTTPUploadThroughPostAsMultiPageTIFF = function(m, j, q, o, l) {
        var p = this,
            k = -2,
            n = 2;
        return p.HTTPUploadThroughPostEx(m, k, j, q, n, o, l)
    };
    i.prototype.HTTPUploadAllThroughPostAsPDF = function(m, j, q, o, l) {
        var p = this,
            k = -1,
            n = 4;
        return this.HTTPUploadThroughPostEx(m, k, j, q, n, o, l)
    };
    i.prototype.HTTPUploadThroughPostAsMultiPagePDF = function(m, j, q, o, l) {
        var p = this,
            k = -2,
            n = 4;
        return p.HTTPUploadThroughPostEx(m, k, j, q, n, o, l)
    };
    i.prototype.HTTPUploadThroughPutDirectly = function(m, k, j, n, l) {
        Dynamsoft.Lib.Errors.HTML5NotSupport(this);
        return false
    };
    i.prototype.HTTPUploadThroughPut = function(m, l, j, o, k) {
        var n = h.getImageType(j);
        return this.HTTPUploadThroughPutEx(m, l, j, n, o, k)
    };
    i.prototype.HTTPUploadThroughPutEx = function(u, w, s, o, t, l) {
        var p = this,
            j = "HTTPUploadThroughPutEx",
            k = h.combineUrl(p, u, s),
            n = true,
            q = false;
        if (h.isFunction(t)) {
            n = false
        }
        Dynamsoft.Lib.cancelFrome = 1;
        var v = function(m) {
                if (h.isFunction(l)) {
                    l(p.ErrorCode, p.ErrorString, p.__HTTPPostResponseString)
                }
                Dynamsoft.Lib.needShowTwiceShowDialog = false;
                a.closeProgress(j);
                if (p.curCommand_SaveImagesToBytes.length > 0) {
                    Dynamsoft.Lib.cancelFrome = 1;
                    Dynamsoft.Lib.needShowTwiceShowDialog = true;
                    a.showProgress(p, j, false);
                    var x = p.curCommand_SaveImagesToBytes[0];
                    h.sendData(x.objWS, x.json, false, x.binary)
                }
                return true
            },
            r;
        if (u === "") {
            Dynamsoft.Lib.Errors.HttpServerCannotEmpty(p);
            v(p._errorString);
            return false
        }
        Dynamsoft.Lib.needShowTwiceShowDialog = true;
        a.showProgress(p, j, false);
        if (w == -2) {
            var q;
            q = c.__SetSelectedImages(p, p.__cIndex, p._UIManager.selectedIndexes);
            if (!q) {
                v(p._errorString);
                return false
            }
        }
        p.__SaveSelectedImagesToBytes(o, w, n, function(m) {
            Dynamsoft.Lib.needShowTwiceShowDialog = false;
            if (m && (Dynamsoft.Lib.dlgRef > 0 || p.__IfShowCancelDialogWhenImageTransfer == false || n == false)) {
                r = function(x) {
                    var y = (x.total === 0) ? 100 : Math.round(x.loaded * 100 / x.total),
                        z = [x.loaded, " / ", x.total].join("");
                    p._OnPercentDone([0, y, "", "http"])
                };
                p._OnPercentDone([0, -1, "uploading...", "http"]);
                h.setBtnCancelVisibleForProgress(p, true);
                h.httpPutImage(p, k, m, n, function(x) {
                    p.__HTTPPostResponseString = x;
                    a.closeProgress(j);
                    q = true;
                    if (h.isFunction(t)) {
                        t(p.__HTTPPostResponseString)
                    }
                    if (p.curCommand_SaveImagesToBytes.length > 0) {
                        Dynamsoft.Lib.cancelFrome = 1;
                        Dynamsoft.Lib.needShowTwiceShowDialog = true;
                        a.showProgress(p, j, false);
                        var y = p.curCommand_SaveImagesToBytes[0];
                        h.sendData(y.objWS, y.json, false, y.binary)
                    }
                }, v, r)
            } else {
                a.closeProgress(j)
            }
        }, v);
        return q
    };
    i.prototype.HTTPUploadAllThroughPutAsMultiPageTIFF = function(m, j, o, l) {
        var k = -1,
            n = 2;
        return this.HTTPUploadThroughPutEx(m, k, j, n, o, l)
    };
    i.prototype.HTTPUploadThroughPutAsMultiPageTIFF = function(m, j, o, l) {
        var p = this,
            k = -2,
            n = 2;
        return p.HTTPUploadThroughPutEx(m, k, j, n, o, l)
    };
    i.prototype.HTTPUploadAllThroughPutAsPDF = function(m, j, o, l) {
        var k = -1,
            n = 4;
        return this.HTTPUploadThroughPutEx(m, k, j, n, o, l)
    };
    i.prototype.HTTPUploadThroughPutAsMultiPagePDF = function(m, j, o, l) {
        var p = this,
            k = -2,
            n = 4;
        return p.HTTPUploadThroughPutEx(m, k, j, n, o, l)
    };
    i.prototype.ShowImageEditor = function() {
        var j = this;
        if (h.isServerInvalid(j)) {
            return false
        }
        return j.ShowImageEditorEx(-1, -1, -1, -1, 0)
    };
    i.prototype.SetCookie = function(j) {
        return this._innerFun("SetCookie", h.makeParams(j))
    };
    i.prototype.LoadImageFromBase64Binary = function(j, k) {
        return this._innerFun("LoadImageFromBase64Binary", h.makeParams(j, k))
    };
    i.prototype.SaveSelectedImagesToBytes = function(k, j) {
        Dynamsoft.Lib.Errors.HTML5NotSupport(this);
        return false
    };
    i.prototype.LoadImageFromBytes = function(l, j, k) {
        Dynamsoft.Lib.Errors.HTML5NotSupport(this);
        return false
    };
    i.prototype.UnregisterEvent = function(j, l) {
        var k = ["__", j].join("");
        this[k] = null
    };
    i.prototype.LoadDibFromClipboard = function(k, j) {
        return this.__innerLoadImage("LoadDibFromClipboard", null, true, k, j)
    };
    i.prototype.LoadImage = function(k, m, j) {
        var l = h.replaceLocalFilename(k);
        return this.__innerLoadImage("LoadImage", h.makeParams(l), true, m, j)
    };
    i.prototype.LoadImageEx = function(k, m, n, j) {
        var l = h.replaceLocalFilename(k);
        return this.__innerLoadImage("LoadImageEx", h.makeParams(l, m), true, n, j)
    };
    i.prototype.EnableSource = function(j) {
        return this.AcquireImage(j)
    };
    i.prototype.AcquireImage = function(k) {
        var j;
        if (k !== undefined && k !== null) {
            j = ["[", d.JSON.stringify(k), "]"].join("")
        } else {
            j = null
        }
        return this.__innerAcquireImage("AcquireImage", j, false)
    };
    i.prototype.__innerLoadImage = function(j, o, n, l, k) {
        if (h.isFunction(l)) {
            return this.__innerAcquireImage(j, o, n, l, k)
        } else {
            return this.__innerLoadImageFun(j, o)
        }
    };
    i.prototype.__innerLoadImageFun = function(j, n) {
        var o = this;
        if (h.isServerInvalid(o)) {
            return false
        }
        var k = o._innerFun(j, n);
        var l = o.CurrentImageIndexInBuffer;
        if (l >= 0) {
            if (k) {
                o._UIManager.getEditor().refresh(l, o._HowManyImagesInBuffer)
            }
            o.OnRefreshUI(l)
        }
        return k
    };
    i.prototype.__innerAcquireImage = function(k, r, o, n, l) {
        var s = this,
            j = (k == "LoadImage" || k == "LoadImageEx");
        var q = function(m) {
            s.__bLoadingImage = false;
            var p = s.CurrentImageIndexInBuffer;
            if (p >= 0) {
                s.OnRefreshUI(p)
            }
            if (h.isFunction(l)) {
                l(s.ErrorCode, s.ErrorString)
            }
            if (j) {
                a.closeProgress(k)
            } else {
                h.hideMask(k)
            }
            if (m) {
                a.log(m)
            }
            return false
        };
        if (h.isServerInvalid(s)) {
            q();
            return false
        }
        if (j) {
            a.showProgress(s, k, false)
        } else {
            h.showMask(k)
        }
        s._innerSend(k, r, true, function() {
            s.__bLoadingImage = false;
            if (j) {
                a.closeProgress(k);
                var m = s.CurrentImageIndexInBuffer;
                if (m >= 0) {
                    s._UIManager.getEditor().refresh(m, s._HowManyImagesInBuffer);
                    s.OnRefreshUI(m)
                }
            } else {
                h.hideMask(k)
            }
            if (h.isFunction(n)) {
                n()
            }
        }, q);
        return true
    };
    i.prototype.__innerSaveImage = function(j, l, k, n) {
        return this.__innerProgressFunction(j, l, false, k, n)
    };
    i.prototype.__innerFTPDownloadDirectly = function(j, l, k, n) {
        Dynamsoft.Lib.cancelFrome = 4;
        return this.__innerProgressFunction(j, l, true, k, n)
    };
    i.prototype.__innerFTPDownload = function(j, o, n, r) {
        var q = this;
        Dynamsoft.Lib.cancelFrome = 4;
        if (h.isFunction(r)) {
            return this.__innerProgressFunction(j, o, true, function() {
                var m = q.CurrentImageIndexInBuffer;
                if (m >= 0) {
                    q.OnRefreshUI(m)
                }
                var m = q.CurrentImageIndexInBuffer;
                if (h.isFunction(n)) {
                    n()
                }
            }, r)
        } else {
            var l = this._innerFun(j, o);
            var k = q.CurrentImageIndexInBuffer;
            if (k >= 0) {
                q.OnRefreshUI(k)
            }
            var k = q.CurrentImageIndexInBuffer
        }
    };
    i.prototype.__innerFTPUpload = function(j, l, k, n) {
        Dynamsoft.Lib.cancelFrome = 3;
        return this.__innerProgressFunction(j, l, true, k, n)
    };
    i.prototype.__innerProgressFunction = function(j, o, k, l, r) {
        var q = this;
        if (!h.isFunction(r)) {
            return this._innerFun(j, o)
        }
        var n = function(m) {
            if (h.isFunction(r)) {
                r(q.ErrorCode, q.ErrorString)
            }
            a.closeProgress(j);
            if (m) {
                a.log(m)
            }
            return true
        };
        a.showProgress(q, j, k);
        this._innerSend(j, o, true, function() {
            a.closeProgress(j);
            if (h.isFunction(l)) {
                l()
            }
        }, n);
        return true
    };
    i.prototype.__innerGetBarcodeResultAsyncFunction = function(k) {
        var j = new g();
        if (k) {
            j._errorCode = k.exception;
            j._errorString = k.description;
            j._resultlist = k.result;
            j._Count = k.result.length
        }
        return j
    };
    i.prototype.__innerBarcodeReadFunction = function(j, l, n, o, k) {
        var q = this;
        if (!h.isFunction(o)) {
            return this.__innerBarcodeSyncReadFunction(j, n)
        } else {
            return this.__innerBarcodeAsyncReadFunction(j, l, n, o, k)
        }
    };
    i.prototype.__innerBarcodeAsyncReadFunction = function(j, n, q, r, l) {
        var s = this;
        var k = function(m) {
                if (m.exception == 0) {
                    if (h.isFunction(r)) {
                        r(n, s.__innerGetBarcodeResultAsyncFunction(m))
                    }
                } else {
                    if (h.isFunction(l)) {
                        l(m.exception, m.description)
                    }
                }
                h.hideMask(j)
            },
            o = function(m) {
                h.hideMask(j)
            };
        h.showMask(j);
        this._innerSend(j, q, true, k, o);
        return true
    };
    i.prototype.__innerBarcodeSyncReadFunction = function(k, n) {
        var o = this,
            l, j;
        l = this._innerFunRaw(k, n, false, false);
        j = new g();
        j._errorCode = o._errorCode;
        j._errorString = o._errorString;
        if (l && d.isArray(l)) {
            j._resultlist = l;
            j._Count = l.length
        }
        return j
    };
    i.prototype._innerSendCmd = function(j, k) {
        var l = this;
        if (h.isHttpServerInvalid(l)) {
            return false
        }
        l._innerFunRaw(j, k, true, false)
    };
    i.prototype._innerFun = function(j, l) {
        var n = this,
            k;
        if (h.isHttpServerInvalid(n)) {
            return false
        }
        k = n._innerFunRaw(j, l, false, false);
        if (d.isArray(k)) {
            return k[0]
        }
        return k
    };
    i.prototype._innerFunRaw = function(l, k, u, n) {
        var s = this,
            j, v, o = "text",
            r;
        if (h.isHttpServerInvalid(s)) {
            return false
        }
        s._errorCode = 0;
        s._errorString = "";
        j = [s.httpUrl, "f/", l, "?", h.getRandom()].join("");
        v = h.getJson(s, l, k, 0);
        var t, q = false;
        if (u) {
            q = true
        } else {}
        r = {
            type: "post",
            url: j,
            data: v,
            async: q,
            success: function(m) {
                if (m !== undefined && m !== null) {
                    if (m.exception !== undefined && m.description !== undefined) {
                        s._errorCode = m.exception;
                        s._errorString = m.description
                    } else {
                        s._errorCode = 0;
                        s._errorString = ""
                    }
                    t = m.result
                } else {
                    Dynamsoft.Lib.Errors.InvalidResultFormat(s);
                    t = ""
                }
            },
            error: function() {
                Dynamsoft.Lib.Errors.NetworkError(s);
                t = ""
            }
        };
        if (n) {
            r.contentType = "text/plain; charset=x-user-defined";
            r.mimeType = "text/plain; charset=x-user-defined";
            r.success = function(p, D, w) {
                var A, x, B;
                A = w.responseText;
                x = A.length;
                s._errorCode = 0;
                s._errorString = "";
                if (x > 0) {
                    var C = false,
                        m;
                    if (A.charAt(0) == "{") {
                        try {
                            m = d.JSON.parse(A);
                            C = true
                        } catch (y) {
                            Dynamsoft.Lib.log(A)
                        }
                    }
                    if (C) {
                        if (m.exception !== undefined && m.description !== undefined) {
                            s._errorCode = m.exception;
                            s._errorString = m.description;
                            t = false
                        } else {
                            t = true
                        }
                    } else {
                        B = new Uint8Array(x);
                        for (var z = 0; z < x; z++) {
                            B[z] = A.charCodeAt(z)
                        }
                        t = new Blob([B], {
                            type: "image/octet-stream"
                        })
                    }
                } else {
                    t = ""
                }
            }
        }
        new KISSY.IO(r);
        return t
    };
    i.prototype._innerSend = function(o, k, n, r, t) {
        var s = this,
            l = s._objWS,
            u = false,
            q = false,
            j = false;
        if (o != "ActiveUI") {
            j = h.isServerInvalid(s)
        }
        if (j) {
            if (h.isFunction(t)) {
                t()
            }
            return
        }
        if (h.isFunction(r)) {
            q = r
        }
        if (h.isFunction(t)) {
            u = t
        }
        var v = h.generateCmdId();
        if (o == "SaveSelectedImagesToBytes") {
            s.curCommand_SaveImagesToBytes.push({
                cmdId: v,
                sFun: q,
                fFun: u,
                objWS: l,
                json: h.getJson(s, o, k, v),
                binary: n
            });
            if (s.curCommand_SaveImagesToBytes.length == 1) {
                h.sendData(l, h.getJson(s, o, k, v), false, n)
            }
        } else {
            s.curCommand.push({
                cmd: o,
                cmdId: v,
                sFun: q,
                fFun: u
            });
            h.sendData(l, h.getJson(s, o, k, v), false, n)
        }
    };
    i.prototype.__SaveSelectedImagesToBytes = function(m, p, n, r, k) {
        var o = this;
        if (n) {
            return o._innerSend("SaveSelectedImagesToBytes", h.makeParams(m, p), true, r, k)
        } else {
            var q = false,
                l = true,
                j = o._innerFunRaw("SaveSelectedImagesToBytes", h.makeParams(m, p), q, l);
            if (o.ErrorCode == 0) {
                r(j);
                return true
            } else {
                k();
                return false
            }
        }
    };
    i.prototype.__SaveLocalFileToBytes = function(l, j, n, q) {
        var p = this;
        if (j) {
            return p._innerSend("SaveSelectedImagesToBytes", h.makeParams(0, 0, l), true, n, q)
        } else {
            var o = false,
                m = true,
                k = p._innerFunRaw("SaveSelectedImagesToBytes", h.makeParams(0, 0, l), o, m);
            if (p.ErrorCode == 0) {
                n(k);
                return true
            } else {
                q();
                return false
            }
        }
    };
    i.prototype.__LoadImageFromBytes = function(w, o, s, q, v, l) {
        var r = this,
            n = "LoadImageFromBytes",
            y = o || 3,
            u = w.size || w.length,
            k = ["[", u, ",", 0, ",", y, ',"', s, '"]'].join(""),
            j = h.isServerInvalid(r);
        var x = function() {
            a.closeProgress(n);
            r.__bLoadingImage = false;
            if (!j) {
                var m = r.CurrentImageIndexInBuffer;
                if (m >= 0) {
                    r.OnRefreshUI(m)
                }
            }
            if (h.isFunction(l)) {
                l()
            }
        };
        if (j) {
            x();
            return false
        }
        if (u === 0) {
            Dynamsoft.Lib.Errors.ImageFileLengthCannotZero(r);
            x();
            return false
        }
        h.setBtnCancelVisibleForProgress(r, false);
        return this._innerSendBlob(n, k, w, q, function() {
            a.closeProgress(n);
            r.__bLoadingImage = false;
            var m = r.CurrentImageIndexInBuffer;
            if (q && m >= 0) {
                r._UIManager.getEditor().refresh(m, r._HowManyImagesInBuffer);
                r.OnRefreshUI(m)
            }
            if (h.isFunction(v)) {
                v()
            }
        }, x)
    };
    i.prototype._innerSendBlob = function(n, k, x, s, r, w) {
        var u = this,
            l = u._objWS,
            q = false,
            y = false;
        if (h.isServerInvalid(u)) {
            if (h.isFunction(w)) {
                w()
            }
            return
        }
        if (h.isFunction(r)) {
            q = r
        }
        if (h.isFunction(w)) {
            y = w
        }
        if (s) {
            var z = h.generateCmdId();
            u.curCommand.push({
                cmd: n,
                cmdId: z,
                sFun: q,
                fFun: y
            });
            h.sendData(l, h.getJson(u, n, k, z), x, true)
        } else {
            var v, j, A, o = "text",
                t;
            if (h.isHttpServerInvalid(u)) {
                return false
            }
            u._errorCode = 0;
            u._errorString = "";
            j = [u.httpUrl, "f/", n, "?", h.getRandom()].join("");
            A = h.getJson(u, n, k, 0);
            A += "\r\n\r\n";
            A += Dynamsoft.Lib.base64.encodeArray(x);
            t = {
                type: "post",
                url: j,
                processData: false,
                data: A,
                async: false,
                success: function(m) {
                    if (m !== undefined && m !== null) {
                        if (m.exception !== undefined && m.description !== undefined) {
                            u._errorCode = m.exception;
                            u._errorString = m.description
                        } else {
                            u._errorCode = 0;
                            u._errorString = ""
                        }
                        v = m.result
                    } else {
                        Dynamsoft.Lib.Errors.InvalidResultFormat(u);
                        v = ""
                    }
                },
                error: function() {
                    Dynamsoft.Lib.Errors.NetworkError(u);
                    v = ""
                }
            };
            new KISSY.IO(t);
            if (u._errorCode == 0) {
                if (h.isFunction(q)) {
                    q()
                }
                u._HowManyImagesInBuffer = u._innerFun("HowManyImagesInBuffer");
                u.__cIndex = u._innerFun("CurrentImageIndexInBuffer")
            } else {
                if (h.isFunction(y)) {
                    y()
                }
            }
            return v
        }
    };
    i.prototype._innerActiveUI = function(k, j) {
        this._innerSend("ActiveUI", h.makeParams(Dynamsoft.Lib.env.WSVersion), false, k, j)
    };
    i.prototype.SetCancel = function(k, j) {
        this._innerSend("SetCancel", null, true, k, j)
    };
    i.prototype.SetImageWidth = function(j, k) {
        return this._innerFun("SetImageWidth", h.makeParams(j, k))
    };
    i.prototype.HttpDownloadStreamThroughPost = function(k, j) {
        Dynamsoft.Lib.Errors.HTML5NotSupport(this);
        return false
    };
    i.prototype.HttpUploadStreamThroughPost = function(m, l, j, n, k) {
        Dynamsoft.Lib.Errors.HTML5NotSupport(this);
        return false
    };
    i.prototype.HttpDownloadThroughPost = function(t, j, n, s, l) {
        var o = this,
            k = h.combineUrl(o, t, j),
            q, p;
        if (h.isServerInvalid(o)) {
            if (h.isFunction(l)) {
                l(o.ErrorCode, o.ErrorString)
            }
            return false
        }
        Dynamsoft.Lib.cancelFrome = 2;
        a.showProgress(o, "HttpDownloadThroughPost", true);
        q = "post";
        var r = function(u) {
                var v = (u.total === 0) ? 100 : Math.round(u.loaded * 100 / u.total),
                    w = [u.loaded, " / ", u.total].join("");
                o._OnPercentDone([0, v, "", "http"])
            },
            m = true;
        o._OnPercentDone([0, -1, "HTTP Downloading...", "http"]);
        if (!h.isFunction(s)) {
            m = false
        }
        p = h.loadHttpBlob(o, q, k, m, function(u) {
            o._OnPercentDone([0, -1, "Loading..."]);
            var w = "",
                v = n;
            if (v < -1 || v > 5) {
                v = -1
            }
            o.__LoadImageFromBytes(u, v, w, m, s, l)
        }, function() {
            a.closeProgress("HttpDownloadThroughPost");
            if (h.isFunction(l)) {
                l(o.ErrorCode, o.ErrorString)
            }
        }, r);
        return p
    };
    i.prototype.HTTPDownloadEx = function(l, k, m, n, j) {
        var o = this;
        return o.HTTPDownloadThroughGet(l, k, m, n, j)
    };
    i.prototype.HTTPDownloadThroughGet = function(t, j, n, s, l) {
        var o = this,
            k = h.combineUrl(o, t, j),
            q, p;
        if (h.isServerInvalid(o)) {
            if (h.isFunction(l)) {
                l(o.ErrorCode, o.ErrorString)
            }
            return false
        }
        Dynamsoft.Lib.cancelFrome = 2;
        q = "get";
        a.showProgress(o, "HTTPDownloadThroughGet", true);
        var r = function(u) {
                var v = (u.total === 0) ? 100 : Math.round(u.loaded * 100 / u.total),
                    w = [u.loaded, " / ", u.total].join("");
                o._OnPercentDone([0, v, "", "http"])
            },
            m = true;
        o._OnPercentDone([0, -1, "HTTP Downloading...", "http"]);
        if (!h.isFunction(s)) {
            m = false
        }
        p = h.loadHttpBlob(o, q, k, m, function(u) {
            o._OnPercentDone([0, -1, "Loading..."]);
            var w = "",
                v = n;
            if (v < -1 || v > 5) {
                v = -1
            }
            o.__LoadImageFromBytes(u, v, w, m, s, l)
        }, function() {
            a.closeProgress("HTTPDownloadThroughGet");
            if (h.isFunction(l)) {
                l(o.ErrorCode, o.ErrorString)
            }
        }, r);
        return p
    };
    i.prototype.SetCustomDSDataEx = function(j) {
        return this._innerFun("SetCustomDSDataEx", h.makeParams(j))
    };
    i.prototype.SetCustomDSData = function(j) {
        var k = h.replaceLocalFilename(j);
        return this._innerFun("SetCustomDSData", h.makeParams(k))
    };
    i.prototype.GetCustomDSDataEx = function() {
        return this._innerFun("GetCustomDSDataEx")
    };
    i.prototype.GetCustomDSData = function(j) {
        var k = h.replaceLocalFilename(j);
        return this._innerFun("GetCustomDSData", h.makeParams(k))
    };
    i.prototype.ChangeBitDepth = function(k, j, l) {
        return this._innerFun("ChangeBitDepth", h.makeParams(k, j, l))
    };
    i.prototype.ConvertToGrayScale = function(j) {
        return this._innerFun("ConvertToGrayScale", h.makeParams(j))
    };
    i.prototype.ShowImageEditorEx = function(k, p, j, o, l) {
        var n = this;
        var m = n._UIManager.ShowImageEditorEx(k, p, j, o, l);
        if (l == 4) {
            return m
        } else {
            return true
        }
    };
    i.prototype.ClearTiffCustomTag = function() {
        return this._innerFun("ClearTiffCustomTag")
    };
    i.prototype.SetTiffCustomTag = function(k, j, l) {
        return this._innerFun("SetTiffCustomTag", h.makeParams(k, j, l))
    };
    i.prototype.IsBlankImage = function(j) {
        return this._innerFun("IsBlankImage", h.makeParams(j, false))
    };
    i.prototype.IsBlankImageExpress = function(j) {
        return this._innerFun("IsBlankImage", h.makeParams(j, true))
    };
    i.prototype.GetVersionInfoAsync = function(k, j) {
        return this._innerSend("VersionInfo", null, true, k, j)
    };
    i.prototype._OnReady = function(n) {
        var m = this,
            l = a.detect.urls[a.detect.cUrlIndex],
            k = a.getHttpUrl(l),
            j = m._objWS;
        if (j === null) {
            return
        }
        Dynamsoft.Lib.detect.starting = false;
        Dynamsoft.Lib.detect.bOK = true;
        m.bReady = true;
        m.__wsRetry = 0;
        j.onopen = null;
        m.httpUrl = k;
        j.onmessage = function(s) {
            var p = s.data;
            if (typeof(p) === "object") {
                var z = m.curCommand_SaveImagesToBytes.shift() || {};
                if (z.sFun) {
                    z.sFun(p)
                }
                return
            }
            if (m.__OnWSMessage) {
                m.__OnWSMessage(p)
            }
            var q = p.replace(/\0/g, "");
            if (q.indexOf("Exception:") >= 0 || q.indexOf("Error") == 0) {
                var y = p,
                    v = false,
                    z = m.curCommand.pop() || {};
                Dynamsoft.Lib.log(y + "@" + z.cmd);
                if (z.fFun) {
                    v = z.fFun(y)
                }
                if (!v && h.isFunction(m.__OnPrintMsg)) {
                    m.__OnPrintMsg(y)
                }
                return
            }
            try {
                q = d.JSON.parse(q)
            } catch (t) {
                Dynamsoft.Lib.log(p)
            }
            if (q.event) {
                h.handleEvent(m, q);
                return
            }
            var z;
            if (q.cmdId) {
                var x = m.curCommand.length,
                    u, o = -1;
                for (u = x - 1; u >= 0; u--) {
                    if (m.curCommand[u].cmdId == q.cmdId) {
                        o = u;
                        z = m.curCommand.splice(o, 1)[0];
                        break
                    }
                }
                if (o < 0) {
                    x = m.curCommand_SaveImagesToBytes.length;
                    for (u = x - 1; u >= 0; u--) {
                        if (m.curCommand_SaveImagesToBytes[u].cmdId == q.cmdId) {
                            o = u;
                            z = m.curCommand_SaveImagesToBytes.splice(o, 1)[0];
                            break
                        }
                    }
                    if (o < 0) {
                        return
                    }
                }
            } else {
                z = m.curCommand.pop() || {}
            }
            if (q.exception !== undefined && q.description !== undefined) {
                m._errorCode = q.exception;
                m._errorString = q.description
            } else {
                m._errorCode = 0;
                m._errorString = ""
            }
            if (q.method == "ReadBarcode") {
                if (z.sFun) {
                    z.sFun(q)
                }
                return
            } else {
                if (q.method == "ActiveUI" || q.method == "VersionInfo") {
                    if (z.sFun && q.result.length > 0) {
                        z.sFun(q.result);
                        return
                    }
                }
            }
            if (q.result.length && q.result[0] == "1") {
                if (z.sFun) {
                    z.sFun()
                }
            } else {
                if (q.exception) {
                    var v = false;
                    if (z.fFun) {
                        v = z.fFun(m._errorString)
                    }
                    if (!v && q.exception && q.exception < 0 && q.exception != -2115) {
                        if (h.isFunction(m.__OnPrintMsg)) {
                            m.__OnPrintMsg(m._errorString)
                        }
                    }
                } else {
                    var w = ["result:"];
                    if (z.fFun) {
                        z.fFun(h.__pushElement(w, q.result))
                    }
                }
            }
        };
        j.onclose = function() {
            m.bReady = false;
            m.__wsRetry = 0;
            Dynamsoft.Lib.Errors.Server_Invalid(m);
            Dynamsoft.Lib.detect.bOK = false;
            if (!Dynamsoft.Lib.detect.starting) {
                for (var o = 0; o < a.detect.arySTwains.length; o++) {
                    if (a.detect.arySTwains[o] == m) {
                        a.detect.cTwainIndex = o;
                        break
                    }
                }
                setTimeout(Dynamsoft.Lib._reconnect, 1000);
                Dynamsoft.Lib.detect.starting = true
            }
            if (m.__OnWSClose) {
                m.__OnWSClose()
            }
            h.hideMask(true)
        };
        j.onerror = function(p) {
            m.bReady = false;
            m.__wsRetry = 0;
            Dynamsoft.Lib.Errors.Server_Invalid(m);
            Dynamsoft.Lib.detect.bOK = false;
            if (!Dynamsoft.Lib.detect.starting) {
                for (var o = 0; o < a.detect.arySTwains.length; o++) {
                    if (a.detect.arySTwains[o] == m) {
                        a.detect.cTwainIndex = o;
                        break
                    }
                }
                setTimeout(Dynamsoft.Lib._reconnect, 1000);
                Dynamsoft.Lib.detect.starting = true
            }
            if (p) {
                Dynamsoft.Lib.log("websocket error: " + p)
            }
            if (m.__OnWSError) {
                m.__OnWSError()
            }
            h.hideMask(true)
        };
        if (n) {
            m._innerActiveUI(function(q) {
                var s = m.__serverId,
                    o;
                if (q && q.length >= 2) {
                    o = q[1]
                } else {
                    Dynamsoft.Lib.log("ActiveUI return parameters error.")
                }
                m.__serverId = o;
                Dynamsoft.Lib.closeProgress("Reconnect");
                if (m.ErrorCode == 0 && Dynamsoft.WebTwainEnv.ProductKey !== "" && Dynamsoft.WebTwainEnv.ProductKey !== "******") {
                    m.ProductKey = Dynamsoft.WebTwainEnv.ProductKey
                }
                if (s != o) {
                    Dynamsoft.Lib.Errors.Server_Restarted(m);
                    if (h.isFunction(m.__OnWSReconnect)) {
                        var p = a.getWSUrl(l);
                        m.__OnWSReconnect(p, k)
                    }
                } else {
                    m._errorCode = 0;
                    m._errorString = ""
                }
            });
            return
        }
        a.LS.item("D_port", l.port);
        a.LS.item("D_ssl", l.ssl ? "true" : "false");
        m.SetViewMode(1, 1);
        m._innerActiveUI(function(q) {
            var o;
            if (q && q.length >= 2) {
                o = q[1]
            } else {
                Dynamsoft.Lib.log("ActiveUI return parameters error.")
            }
            m.__serverId = o;
            if (m.ErrorCode == 0 && Dynamsoft.WebTwainEnv.ProductKey !== "" && Dynamsoft.WebTwainEnv.ProductKey !== "******") {
                m.ProductKey = Dynamsoft.WebTwainEnv.ProductKey
            }
            if (h.isFunction(m.__OnWSReady)) {
                var p = a.getWSUrl(l);
                m.__OnWSReady(p, k)
            }
            if (Dynamsoft.Lib.detect.OnReady) {
                Dynamsoft.Lib.detect.OnReady()
            }
        })
    };
    Dynamsoft.Lib.DynamicWebTwain = function(j) {
        var l = Dynamsoft.Lib,
            k = new i(j);
        l.detect.arySTwains.push(k);
        if (l.detect.bFirst) {
            l._init()
        }
        l.detect.bFirst = false;
        return k
    }
})(Dynamsoft.Lib, KISSY);