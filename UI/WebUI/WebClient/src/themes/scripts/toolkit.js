window.loadToolKit = (isFromContainter) => {
    ! function (e) {
        function t(i) {
            if (n[i]) return n[i].exports;
            var s = n[i] = {
                exports: {},
                id: i,
                loaded: !1
            };
            return e[i].call(s.exports, s, s.exports, t), s.loaded = !0, s.exports
        }
        var n = {};
        return t.m = e, t.c = n, t.p = "", t(0)
    }([function (e, t, n) {
        "use strict";
        var i = n(18),
            s = n(57),
            r = n(20),
            a = n(52),
            o = n(51),
            l = n(21),
            c = n(54),
            d = n(56),
            u = n(64),
            m = n(19),
            h = n(27),
            v = n(32),
            f = n(33),
            p = n(31),
            y = n(29),
            E = n(30),
            g = n(66),
            C = n(62),
            _ = n(63),
            w = n(25),
            b = n(65),
            S = n(23),
            k = n(55),
            T = n(48),
            x = n(17),
            L = n(59),
            A = n(46),
            I = n(61),
            F = n(49),
            H = n(16),
            O = n(15),
            q = n(24),
            W = n(13),
            M = n(14),
            P = n(11),
            j = n(41),
            N = n(44),
            B = n(47),
            R = n(58),
            D = n(39),
            Q = n(40),
            X = n(53),
            V = n(12),
            z = n(43),
            Y = n(10),
            U = n(60),
            G = n(22),
            K = n(45),
            $ = n(26),
            J = n(28),
            Z = n(35),
            ee = n(34),
            te = (n(3), n(2), n(4), n(37)),
            ne = n(42),
            ie = n(38),
            se = n(36),
            re = n(50);
        (0, i.bsGridOverlay)(48), (0, s.xCarousel)(), (0, r.classListPolyfill)(), (0, a.selectElement)(), (0, o.rangeInputOne)(), (0, l.contentAccordian)(), (0, c.exploreSearch)(), (0, d.headerScroll)(), (0, u.searchScroll)(), (0, m.cardItems)(), (0, h.headerMenu)(), (0, v.contactInfo)(), (0, f.resourceTab)(), (0, p.clientLogos)(), (0, g.tabAction)(), (0, C.savedSearch)(), (0, _.scrollTabsInit)(), (0, w.formSlider)(), (0, b.challenegeList)(), (0, S.clientTestimonial)(), (0, k.formActions)(), (0, T.dashboard)(), (0, x.breadcrumbLink)(), (0, L.manageClient)(), (0, A.tooltip)(), (0, I.multiToggle)(), (0, F.customSelect)(), (0, H.basicForm)(), (0, W.addClient)(), (0, M.addAsset)(), (0, P.accordian)(), (0, j.spendingAssessment)(), (0, N.stickyNav)(), (0, B.workspacetiles)(), (0, R.logocard)(), (0, X.existingcc)(), (0, D.dataAssets)(), (0, Q.data)(), (0, V.addChallenges)(), (0, O.basicFormFull)(), (0, z.viewmore)(), (0, Y.accordianChlng)(), (0, U.moreOptions)(), (0, G.domainbtn)(), (0, E.bannerImg)(), (0, y.assetDemo)(), (0, K.deliverytbl)(), (0, $.clientrole)(), (0, J.addInfoTab)(), (0, Z.techTab)(), (0, ee.securityTab)(), (0, te.discKick)(), (0, ne.moredetails)(), (0, ie.discNav)(), (0, q.assetcomposermodel)(), (0, se.modalWindow)(), (0, re.multiselectElement)()
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        var n = t.htmlCollectionToArray = function (e) {
            for (var t = [], n = 0; n < e.length; n++) t.push(e[n]);
            return t
        },
            i = t.getHtmlElmentsByClass = function (e) {
                var t = document.querySelectorAll(e);
                return n(t)
            },
            s = (t.getElementById = function (e) {
                return document.getElementById(e)
            }, t.getElementsByTagName = function (e) {
                var t = document.getElementsByTagName(e);
                return n(t)
            }, t.addEventListener = function (e, t, n) {
                e && e.addEventListener ? e.addEventListener(t, n, !1) : e && e.attachEvent && e.attachEvent("on" + t, n)
            }, t.removeClassFromElements = function (e, t) {
                var n = i(e);
                n.map(function (e) {
                    e.classList.remove(t)
                })
            }, t.removeClassFromSeletor = function (e, t) {
                var n = s(e);
                n.map(function (e) {
                    e.classList.remove(t)
                })
            }, t.addClassFromElements = function (e, t) {
                var n = i(e);
                n.map(function (e) {
                    e.classList.add(t)
                })
            }, t.addClassToElement = function (e, t) {
                e && e.classList.add(t)
            }, t.removeClassFromElement = function (e, t) {
                e && e.classList.remove(t)
            }, t.toggleClassElement = function (e, t) {
                e && e.classList.toggle(t)
            }, t.elementHasClass = function (e, t) {
                return e.classList.contains(t)
            }, t.getChildElements = function (e) {
                return n(e.children)
            }, t.getQueryHtmlElements = function (e) {
                var t = document.querySelectorAll(e);
                return n(t)
            })
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.discData = function () {
            var e = window.IndustryXCarousel,
                t = new e("disc-data", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 2
                    }, {
                        maxWidth: 1250,
                        items: 2
                    }, {
                        maxWidth: 1180,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }, window.additionalInfoCarousel = t
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.discTask = void 0;
        var i = n(1);
        t.discTask = function () {
            var e = (0, i.getHtmlElmentsByClass)("c-disc-task__task-head");
            e.map(function (t, n) {
                (0, i.addEventListener)(t, "click", function () {
                    var s = t.parentNode,
                        r = void 0,
                        a = !1;
                    if (n < e.length - 1 ? r = e[n + 1].parentNode : a = !0, (0, i.elementHasClass)(s, "-active") ? ((0, i.removeClassFromElement)(s, "-active"), a ? (0, i.removeClassFromElement)(s, "-last") : (0, i.removeClassFromElement)(r, "-sec")) : ((0, i.addClassToElement)(s, "-active"), a ? (0, i.addClassToElement)(s, "-last") : (0, i.addClassToElement)(r, "-sec")), (0, i.elementHasClass)(t, "-child")) {
                        var o = (0, i.getHtmlElmentsByClass)("c-disc-task__child-head");
                        o.map(function (e, t) {
                            (0, i.addEventListener)(e, "click", function () {
                                var n = e.parentNode,
                                    s = void 0,
                                    r = !1;
                                t < o.length - 1 ? s = o[t + 1].parentNode : r = !0, (0, i.elementHasClass)(e, "-active") ? ((0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(n, "-active"), r ? (0, i.removeClassFromElement)(n, "-last") : (0, i.removeClassFromElement)(s, "-sec")) : ((0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(n, "-active"), r ? (0, i.addClassToElement)(n, "-last") : (0, i.addClassToElement)(s, "-sec"))
                            })
                        })
                    }
                })
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.discTools = function () {
            var e = window.IndustryXCarousel,
                t = new e("disc-tools", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 2
                    }, {
                        maxWidth: 1250,
                        items: 2
                    }, {
                        maxWidth: 1180,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }, window.additionalInfoCarousel = t
        }
    }, , , , , , function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.accordianChlng = void 0;
        var i = n(1);
        t.accordianChlng = function () {
            var e = (0, i.getQueryHtmlElements)(".c-accordian-chlng__item-label");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.nextElementSibling,
                        n = e.parentElement;
                    (0, i.elementHasClass)(t, "-expand") ? ((0, i.removeClassFromElement)(t, "-expand"), (0, i.removeClassFromElement)(n, "-active")) : ((0, i.addClassToElement)(t, "-expand"), (0, i.addClassToElement)(n, "-active"))
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.accordian = void 0;
        var i = n(1),
            s = function (e, t) {
                var n = (0, i.getHtmlElmentsByClass)(e);
                n.map(function (n) {
                    var s = n.nextElementSibling;
                    (0, i.addEventListener)(n, "click", function () {
                        (0, i.elementHasClass)(n, "-active") ? ((0, i.removeClassFromElements)(e, "-active"), (0, i.removeClassFromElements)(t, "-active")) : ((0, i.removeClassFromElements)(e, "-active"), (0, i.removeClassFromElements)(t, "-active"), (0, i.addClassToElement)(n, "-active"), (0, i.addClassToElement)(s, "-active"))
                    })
                })
            };
        t.accordian = function () {
            s("c-access-check__accordian-head", "c-access-check__accordian-content"), s("c-change-history__accordian-head", "c-change-history__accordian-content")
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.addChallenges = void 0;
        var i = n(1),
            s = (document.querySelectorAll(".c-add-challenges__list_item"), function (e) {
                e.className = "c-add-challenges__list_item -active"
            }),
            r = function (e) {
                e.className = "c-add-challenges__list_item"
            };
        t.addChallenges = function () {
            var e = (0, i.getHtmlElmentsByClass)("c-add-challenges__list_item");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    e.className.includes("-active") ? r(e) : s(e)
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.addClient = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelectorAll(".-data-block"),
                    t = window.pageYOffset,
                    n = document.querySelector(".c-add-new-account__wrapper") ? document.querySelector(".c-add-new-account__wrapper") : {},
                    s = n.offsetTop,
                    r = n.offsetHeight;
                e.forEach(function (e) {
                    var n = e.offsetHeight,
                        a = e.offsetTop - 50,
                        o = e.getAttribute("id");
                    if (t >= s + r || t < s);
                    else if (t > a && t <= a + n && t) {
                        (0, i.removeClassFromElements)("c-adding-details__list-item", "-active");
                        var l = document.querySelector(".c-adding-details__list-item[data-href*=" + o + "]");
                        (0, i.addClassToElement)(l, "-active")
                    }
                })
            },
            r = function () {
                var e = (0, i.getHtmlElmentsByClass)("c-adding-details__list-item");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.getAttribute("data-href"),
                            n = (0, i.getElementById)(t),
                            s = n.offsetTop;
                        window.scrollTo({
                            top: s - 40,
                            left: 0,
                            behavior: "smooth"
                        }), (0, i.removeClassFromElements)("c-adding-details__list-item", "-active"), (0, i.addClassToElement)(e, "-active")
                    })
                })
            },
            a = t.addClient = function () {
                s(), r(), (0, i.addEventListener)(window, "scroll", function () {
                    s()
                })
            };
        window.addClient = a
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.addAsset = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelectorAll(".-data-block"),
                    t = window.pageYOffset,
                    n = document.querySelector(".c-asset-tab__wrapper") ? document.querySelector(".c-asset-tab__wrapper") : {},
                    s = n.offsetTop,
                    r = n.offsetHeight;
                e.forEach(function (e) {
                    var n = e.offsetHeight,
                        a = e.offsetTop - 50,
                        o = e.getAttribute("id");
                    if (t >= s + r || t < s);
                    else if (t > a && t <= a + n && t) {
                        (0, i.removeClassFromElements)("c-asset-tab__item", "-active");
                        var l = document.querySelector(".c-asset-tab__item[data-href*=" + o + "]");
                        (0, i.addClassToElement)(l, "-active")
                    }
                })
            },
            r = function () {
                var e = (0, i.getHtmlElmentsByClass)("c-asset-tab__item");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.getAttribute("data-href"),
                            n = (0, i.getElementById)(t);
                        if (n) {
                            var s = n.offsetTop;
                            window.scrollTo({
                                top: s - 40,
                                left: 0,
                                behavior: "smooth"
                            }), (0, i.removeClassFromElements)("c-asset-tab__item", "-active"), (0, i.addClassToElement)(e, "-active")
                        }
                    })
                })
            },
            a = t.addAsset = function () {
                s(), r(), (0, i.addEventListener)(window, "scroll", function () {
                    s()
                })
            };
        window.addAsset = a
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.basicFormFull = void 0;
        var i = n(1),
            s = document.querySelector(".c-basic-form-full__modal"),
            r = document.querySelector(".c-manage-access__close"),
            a = document.querySelector("body"),
            o = function () {
                (0, i.addClassToElement)(s, "-active"), a.style.overflow = "hidden"
            },
            l = function () {
                (0, i.removeClassFromElement)(s, "-active"), a.style.overflow = "visible"
            };
        t.basicFormFull = function () {
            var e = document.querySelector(".c-basic-form-full__add-user");
            (0, i.addEventListener)(e, "click", function () {
                o()
            }), (0, i.addEventListener)(r, "click", function () {
                l()
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.basicForm = void 0;
        var i = n(1),
            s = document.querySelector(".c-basic-form__modal"),
            r = document.querySelector(".c-manage-access__close"),
            a = document.querySelector("body"),
            o = function () {
                (0, i.addClassToElement)(s, "-active"), a.style.overflow = "hidden"
            },
            l = function () {
                (0, i.removeClassFromElement)(s, "-active"), a.style.overflow = "visible"
            },
            c = function () {
                var e = document.querySelector(".site-wrap");
                if (e) var t = document.querySelector(".site-wrap").offsetHeight;
                var n = (document.querySelector(".c-basic-form"), document.querySelector(".c-workspace-tabs__filters"));
                n && (n.style.height = t + "px", document.querySelector(".c-workspace-tabs__filter").addEventListener("click", function () {
                    e.classList.toggle("-active")
                }))
            };
        t.basicForm = function () {
            var e = document.querySelector(".c-basic-form__add-user");
            (0, i.addEventListener)(e, "click", function () {
                o()
            }), (0, i.addEventListener)(r, "click", function () {
                l()
            }), c()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.breadcrumbLink = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelector(".-breadcrumbdots"),
                    t = document.querySelector(".c-breadcrumbs-dropdown__sub-list");
                (0, i.addEventListener)(e, "click", function () {
                    e.classList.contains("-show") ? ((0, i.removeClassFromElement)(e, "-show"), (0, i.removeClassFromElement)(t, "-display")) : ((0, i.addClassToElement)(e, "-show"), (0, i.addClassToElement)(t, "-display"))
                })
            };
        t.breadcrumbLink = function () {
            s()
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.bsGridOverlay = function () {
            for (var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : 48, t = !1, n = "", i = document.querySelector("body"), s = 0; s < e; s += 1) n += '<div class="col-xs-1"><div class="fill"></div></div>';
            var r = '\n  <div class="bs-grid-overlay">\n    <div class="container">\n      <div class="row">\n        ' + n + "\n      </div>\n    </div>\n  </div>",
                a = document.createElement("div");
            document.addEventListener("keydown", function (e) {
                e.altKey && 71 == e.keyCode && 0 == t ? (a.innerHTML = r, i.appendChild(a), t = !0) : (a.innerHTML = "", t = !1)
            }, !1)
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.cardItems = void 0;
        var i = n(1),
            s = function (e) {
                var t = (0, i.getQueryHtmlElements)("." + e + " .c-checkbox__input");
                t.map(function (t) {
                    (0, i.addEventListener)(t, "click", function () {
                        var n = t.checked,
                            s = t.closest("." + e);
                        n ? (0, i.addClassToElement)(s, "-active") : (0, i.removeClassFromElement)(s, "-active")
                    })
                })
            },
            r = function (e) {
                var t = 0,
                    n = (0, i.getQueryHtmlElements)("." + e + " .c-checkbox__input");
                return n.map(function (e) {
                    var n = e.checked;
                    n && t++
                }), t
            },
            a = function () {
                var e = (0, i.getQueryHtmlElements)(".c-explore-assets .c-checkbox__input"),
                    t = document.querySelector(".c-select-card");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var e = r("c-explore-assets");
                        e > 0 ? (0, i.addClassToElement)(t, "-active") : (0, i.removeClassFromElement)(t, "-active")
                    })
                })
            },
            o = function () {
                var e = (0, i.getQueryHtmlElements)(".c-explore-card .c-checkbox__input"),
                    t = document.querySelector(".c-select-card");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var e = r("c-explore-card");
                        e > 0 ? (0, i.addClassToElement)(t, "-active") : (0, i.removeClassFromElement)(t, "-active")
                    })
                })
            },
            l = function (e) {
                var t = (0, i.getQueryHtmlElements)("." + e + " .c-radio__input"),
                    n = document.querySelector(".c-select-card");
                t.map(function (t) {
                    (0, i.addEventListener)(t, "change", function () {
                        var s = t.checked,
                            r = t.closest("." + e);
                        1 == s && ((0, i.removeClassFromElements)(e, "-active"), (0, i.addClassToElement)(r, "-active"), (0, i.addClassToElement)(n, "-active"))
                    })
                })
            };
        t.cardItems = function () {
            a(), s("c-recom-card"), s("c-solution-card"), l("c-explore-card"), s("c-explore-card"), o()
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.classListPolyfill = function () {
            "document" in self && ("classList" in document.createElement("_") && (!document.createElementNS || "classList" in document.createElementNS("http://www.w3.org/2000/svg", "g")) || ! function (e) {
                if ("Element" in e) {
                    var t = "classList",
                        n = "prototype",
                        i = e.Element[n],
                        s = Object,
                        r = String[n].trim || function () {
                            return this.replace(/^\s+|\s+$/g, "")
                        },
                        a = Array[n].indexOf || function (e) {
                            for (var t = 0, n = this.length; n > t; t++)
                                if (t in this && this[t] === e) return t;
                            return -1
                        },
                        o = function (e, t) {
                            this.name = e, this.code = DOMException[e], this.message = t
                        },
                        l = function (e, t) {
                            if ("" === t) throw new o("SYNTAX_ERR", "The token must not be empty.");
                            if (/\s/.test(t)) throw new o("INVALID_CHARACTER_ERR", "The token must not contain space characters.");
                            return a.call(e, t)
                        },
                        c = function (e) {
                            for (var t = r.call(e.getAttribute("class") || ""), n = t ? t.split(/\s+/) : [], i = 0, s = n.length; s > i; i++) this.push(n[i]);
                            this._updateClassName = function () {
                                e.setAttribute("class", this.toString())
                            }
                        },
                        d = c[n] = [],
                        u = function () {
                            return new c(this)
                        };
                    if (o[n] = Error[n], d.item = function (e) {
                        return this[e] || null
                    }, d.contains = function (e) {
                        return ~l(this, e + "")
                    }, d.add = function () {
                        var e, t = arguments,
                            n = 0,
                            i = t.length,
                            s = !1;
                        do e = t[n] + "", ~l(this, e) || (this.push(e), s = !0); while (++n < i);
                        s && this._updateClassName()
                    }, d.remove = function () {
                        var e, t, n = arguments,
                            i = 0,
                            s = n.length,
                            r = !1;
                        do
                            for (e = n[i] + "", t = l(this, e); ~t;) this.splice(t, 1), r = !0, t = l(this, e); while (++i < s);
                        r && this._updateClassName()
                    }, d.toggle = function (e, t) {
                        var n = this.contains(e),
                            i = n ? t !== !0 && "remove" : t !== !1 && "add";
                        return i && this[i](e), t === !0 || t === !1 ? t : !n
                    }, d.replace = function (e, t) {
                        var n = l(e + "");
                        ~n && (this.splice(n, 1, t), this._updateClassName())
                    }, d.toString = function () {
                        return this.join(" ")
                    }, s.defineProperty) {
                        var m = {
                            get: u,
                            enumerable: !0,
                            configurable: !0
                        };
                        try {
                            s.defineProperty(i, t, m)
                        } catch (e) {
                            void 0 !== e.number && -2146823252 !== e.number || (m.enumerable = !1, s.defineProperty(i, t, m))
                        }
                    } else s[n].__defineGetter__ && i.__defineGetter__(t, u)
                }
            }(self), function () {
                var e = document.createElement("_");
                if (e.classList.add("c1", "c2"), !e.classList.contains("c2")) {
                    var t = function e(t) {
                        var e = DOMTokenList.prototype[t];
                        DOMTokenList.prototype[t] = function (t) {
                            var n, i = arguments.length;
                            for (n = 0; i > n; n++) t = arguments[n], e.call(this, t)
                        }
                    };
                    t("add"), t("remove")
                }
                if (e.classList.toggle("c3", !1), e.classList.contains("c3")) {
                    var n = DOMTokenList.prototype.toggle;
                    DOMTokenList.prototype.toggle = function (e, t) {
                        return 1 in arguments && !this.contains(e) == !t ? t : n.call(this, e)
                    }
                }
                "replace" in document.createElement("_").classList || (DOMTokenList.prototype.replace = function (e, t) {
                    var n = this.toString().split(" "),
                        i = n.indexOf(e + "");
                    ~i && (n = n.slice(i), this.remove.apply(this, n), this.add(t), this.add.apply(this, n.slice(1)))
                }), e = null
            }())
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.contentAccordian = function () {
            for (var e = document.querySelectorAll("c-accordian__item-header"), t = function (t) {
                e[t].addEventListener("click", function () {
                    var n = e[t].parentElement.classList;
                    n.contains("-show") ? n.remove("-show") : n.add("-show")
                })
            }, n = 0; n < e.length; n++) t(n)
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.domainbtn = void 0;
        var i = n(1);
        t.domainbtn = function () {
            var e = (0, i.getQueryHtmlElements)(".c-domain-usage-card__content-item-more-value");
            e.map(function (e) {
                var t = e.firstElementChild;
                (0, i.addEventListener)(e, "click", function () {
                    t && t.scrollTo(0, 0), (0, i.elementHasClass)(e, "-active") ? (0, i.removeClassFromElement)(e, "-active") : (0, i.addClassToElement)(e, "-active")
                })
            });
            var t = (0, i.getQueryHtmlElements)(".c-technology__content-item-more-value");
            t.map(function (e) {
                var t = e.firstElementChild;
                (0, i.addEventListener)(e, "click", function () {
                    t && t.scrollTo(0, 0), (0, i.elementHasClass)(e, "-active") ? (0, i.removeClassFromElement)(e, "-active") : (0, i.addClassToElement)(e, "-active")
                })
            });
            var n = (0, i.getQueryHtmlElements)(".c-security__content-item-more-value");
            n.map(function (e) {
                var t = e.firstElementChild;
                (0, i.addEventListener)(e, "click", function () {
                    t && t.scrollTo(0, 0), (0, i.elementHasClass)(e, "-active") ? (0, i.removeClassFromElement)(e, "-active") : (0, i.addClassToElement)(e, "-active")
                })
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.clientTestimonial = function () {
            var e = window.IndustryXCarousel,
                t = new e("client-testimonial", {
                    items: 1,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !0,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 1
                    }, {
                        maxWidth: 1250,
                        items: 1
                    }, {
                        maxWidth: 1e3,
                        items: 1
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.assetcomposermodel = void 0;
        var i = n(1),
            s = document.querySelector(".c-asset-composer__modal"),
            r = document.querySelector(".c-asset-contact__close"),
            a = document.querySelector("body"),
            o = function () {
                (0, i.addClassToElement)(s, "-active"), a.style.overflow = "hidden"
            },
            l = function () {
                (0, i.removeClassFromElement)(s, "-active"), a.style.overflow = "visible"
            };
        t.assetcomposermodel = function () {
            var e = document.querySelector(".c-asset-composer__add-user");
            (0, i.addEventListener)(e, "click", function () {
                o()
            }), (0, i.addEventListener)(r, "click", function () {
                l()
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.formSlider = function () {
            var e = window.IndustryXCarousel,
                t = new e("form-slider", {
                    items: 6,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-chevron-left"></i>',
                    rightArrow: '<i class="icon-chevron-right"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 6
                    }, {
                        maxWidth: 1250,
                        items: 6
                    }, {
                        maxWidth: 600,
                        items: 4
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.clientrole = void 0;
        var i = n(1);
        t.clientrole = function () {
            var e = (0, i.getQueryHtmlElements)(".c-client-role__arr-link");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.parentElement.parentElement,
                        n = e.parentElement,
                        s = (0, i.getChildElements)(t);
                    s.map(function (t) {
                        (0, i.elementHasClass)(t, "-expand") && (0, i.elementHasClass)(t, "c-client-role__child") ? ((0, i.removeClassFromElement)(t, "-expand"), (0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(n, "-nodropdown")) : !(0, i.elementHasClass)(t, "-expand") && (0, i.elementHasClass)(t, "c-client-role__child") && ((0, i.addClassToElement)(t, "-expand"), (0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(n, "-nodropdown"))
                    })
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.headerMenu = void 0;
        var i = n(1);
        t.headerMenu = function () {
            var e = document.querySelector(".c-header-client__user"),
                t = document.querySelector(".c-header-client__user-dropdown"),
                n = document.querySelector(".c-header-client__link"),
                s = document.querySelector(".c-header-client__notification-bar");
            e && e.addEventListener("click", function () {
                s.style.display = "none", n.classList.remove("-active");
                var i = e.classList;
                i.contains("-active") ? (t.style.display = "none", i.remove("-active")) : (t.style.display = "block", i.add("-active"))
            }), n && n.addEventListener("click", function () {
                t.style.display = "none", e.classList.remove("-active");
                var i = n.classList;
                i.contains("-active") ? (s.style.display = "none", i.remove("-active")) : (s.style.display = "block", i.add("-active"))
            });
            var r = (0, i.getHtmlElmentsByClass)("c-notification-bar__list-item");
            r.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    (0, i.elementHasClass)(e, "-active") ? (0, i.removeClassFromElements)("c-notification-bar__list-item", "-active") : ((0, i.removeClassFromElements)("c-notification-bar__list-item", "-active"), (0, i.addClassToElement)(e, "-active"))
                })
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.addInfoTab = function () {
            var e = window.IndustryXCarousel,
                t = new e("additional-info-carousel", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 2
                    }, {
                        maxWidth: 1250,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 2
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }, window.additionalInfoCarousel = t
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.assetDemo = function () {
            var e = window.IndustryXCarousel,
                t = new e("asset-demo", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !0,
                    leftArrow: '<i class="icon-chevron-left"></i>',
                    rightArrow: '<i class="icon-chevron-right"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 1
                    }, {
                        maxWidth: 1250,
                        items: 1
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.bannerImg = function () {
            var e = window.IndustryXCarousel,
                t = new e("banner-img", {
                    items: 3,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !0,
                    leftArrow: '<i class="icon-chevron-left"></i>',
                    rightArrow: '<i class="icon-chevron-right"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 1
                    }, {
                        maxWidth: 1250,
                        items: 1
                    }, {
                        maxWidth: 600,
                        items: 1
                    }],
                    autoscroll: !0
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.clientLogos = function () {
            var e = window.IndustryXCarousel,
                t = new e("client-logos", {
                    items: 5,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-chevron-left"></i>',
                    rightArrow: '<i class="icon-chevron-right"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 5
                    }, {
                        maxWidth: 1250,
                        items: 5
                    }, {
                        maxWidth: 600,
                        items: 3
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.contactInfo = function () {
            var e = window.IndustryXCarousel,
                t = new e("contact-information", {
                    items: 3,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !0,
                    leftArrow: '<i class="icon-chevron-left"></i>',
                    rightArrow: '<i class="icon-chevron-right"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 3
                    }, {
                        maxWidth: 1250,
                        items: 3
                    }, {
                        maxWidth: 600,
                        items: 2
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.resourceTab = function () {
            for (var e = window.IndustryXCarousel, t = new e("domain-usage-carousel", {
                items: 3,
                firstItemFixed: !1,
                arrows: !0,
                dots: !0,
                leftArrow: '<i class="icon-chevron-left"></i>',
                rightArrow: '<i class="icon-chevron-right"></i>',
                margin: 40,
                responsive: [{
                    maxWidth: 1500,
                    items: 3
                }, {
                    maxWidth: 1250,
                    items: 3
                }, {
                    maxWidth: 600,
                    items: 3
                }]
            }), n = document.querySelectorAll("c-resource-tab__tab"), i = function (e) {
                n[e].addEventListener("click", function () {
                    for (var i = n[e].getAttribute("data-content"), s = document.getElementById(i), r = document.querySelectorAll("c-resource-tab__tab"), a = document.querySelectorAll("c-resource-tab__contents-div"), o = 0; o < r.length; o++) r[o].classList.remove("-active");
                    for (var l = 0; l < r.length; l++) a[l].classList.remove("-active");
                    n[e].classList.add("-active"), s.classList.add("-active"), t.renderCarousel(), techCarousel.renderCarousel(), additionalInfoCarousel.renderCarousel(), securityCarousel.renderCarousel()
                })
            }, s = 0; s < n.length; s++) i(s)
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.securityTab = function () {
            var e = window.IndustryXCarousel,
                t = new e("security-carousel", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 2
                    }, {
                        maxWidth: 1250,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 2
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }, window.securityCarousel = t
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.techTab = function () {
            var e = window.IndustryXCarousel,
                t = new e("technology-carousel", {
                    items: 2,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 2
                    }, {
                        maxWidth: 1250,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 2
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }, window.techCarousel = t
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.modalWindow = function () {
            Element.prototype.matches || (Element.prototype.matches = Element.prototype.msMatchesSelector || Element.prototype.webkitMatchesSelector), Element.prototype.closest || (Element.prototype.closest = function (e) {
                var t = this;
                do {
                    if (t.matches(e)) return t;
                    t = t.parentElement || t.parentNode
                } while (null !== t && 1 === t.nodeType);
                return null
            });
            var e = document.querySelector("body"),
                t = [].slice.call(document.querySelectorAll(".c-modal-btn"));
            t.forEach(function (t) {
                t.onclick = function () {
                    var n = t.getAttribute("data-modal"),
                        i = document.getElementById(n).parentElement;
                    document.getElementById(n).style.display = "grid", i.classList.add("-active"), e.style.overflow = "hidden"
                }
            });
            var n = [].slice.call(document.querySelectorAll(".-close"));
            n.forEach(function (t) {
                t.onclick = function () {
                    var n = t.closest(".-modal");
                    n.style.display = "none", n.parentElement.classList.remove("-active"), e.style.overflow = "auto"
                }
            }), window.onclick = function (t) {
                t.target.classList.contains("-modal") ? (t.target.style.display = "none", t.target.parentElement.classList.remove("-active"), e.style.overflow = "auto") : t.target.classList.contains("-active") && t.target.firstElementChild && t.target.firstElementChild.classList.contains("-modal") && (t.target.classList.remove("-active"), e.style.overflow = "auto")
            }
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.discKick = void 0;
        var i = n(1),
            s = n(2),
            r = n(3),
            a = n(4);
        t.discKick = function () {
            var e = (0, i.getHtmlElmentsByClass)("c-disc-kick__option"),
                t = (0, i.getHtmlElmentsByClass)("c-disc-kick__body-opt");
            e.map(function (n) {
                (0, i.addEventListener)(n, "click", function () {
                    var o = n.getAttribute("data-attr"),
                        l = document.getElementById(o);
                    if (!(0, i.elementHasClass)(l, "-active")) {
                        if (e.map(function (e) {
                            (0, i.removeClassFromElement)(e, "-active")
                        }), t.map(function (e) {
                            (0, i.removeClassFromElement)(e, "-active")
                        }), (0, i.addClassToElement)(n, "-active"), (0, i.addClassToElement)(l, "-active"), "solution-assets" == o && !(0, i.elementHasClass)(n, "-visited")) {
                            (0, s.discData)();
                            var c = l.querySelector(".c-disc-data__data-cards");
                            if (c.childNodes.length > 6)
                                for (; 6 != c.childNodes.length;) c.removeChild(c.childNodes[5]), c.removeChild(c.childNodes[4]);
                            (0, a.discTools)();
                            var d = l.querySelector(".c-disc-tool__tool-cards");
                            if (d.childNodes.length > 6)
                                for (; 6 != d.childNodes.length;) d.removeChild(d.childNodes[5]), d.removeChild(d.childNodes[4])
                        }
                        "solution-tasks" != o || (0, i.elementHasClass)(n, "-visited") || (0, r.discTask)(), (0, i.addClassToElement)(n, "-visited")
                    }
                })
            }), window.onresize = function () {
                e.map(function (e) {
                    (0, i.removeClassFromElement)(e, "-visited")
                })
            }
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.discNav = void 0;
        var i = n(1);
        t.discNav = function () {
            var e = (0, i.getHtmlElmentsByClass)("c-disc-nav__list-item");
            e.map(function (t) {
                (0, i.addEventListener)(t, "click", function () {
                    if (!(0, i.elementHasClass)(t, "-active")) {
                        e.map(function (e) {
                            (0, i.removeClassFromElement)(e, "-active")
                        }), (0, i.addClassToElement)(t, "-active");
                        var n = t.getAttribute("data-href"),
                            s = document.getElementById(n),
                            r = (0, i.getHtmlElmentsByClass)("c-disc-main__body-cont");
                        r.map(function (e) {
                            (0, i.removeClassFromElement)(e, "-active")
                        }), (0, i.addClassToElement)(s, "-active"), window.scrollBy({
                            top: s.getBoundingClientRect().top - 100,
                            left: 0,
                            behavior: "smooth"
                        })
                    }
                })
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.dataAssets = function () {
            var e = window.IndustryXCarousel,
                t = new e("data-assets", {
                    items: 3,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 3
                    }, {
                        maxWidth: 1250,
                        items: 3
                    }, {
                        maxWidth: 1e3,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.data = function () {
            var e = window.IndustryXCarousel,
                t = new e("data", {
                    items: 3,
                    firstItemFixed: !1,
                    arrows: !0,
                    dots: !1,
                    leftArrow: '<i class="icon-arrow-up"></i>',
                    rightArrow: '<i class="icon-arrow-up"></i>',
                    margin: 40,
                    responsive: [{
                        maxWidth: 1500,
                        items: 3
                    }, {
                        maxWidth: 1250,
                        items: 3
                    }, {
                        maxWidth: 1e3,
                        items: 2
                    }, {
                        maxWidth: 600,
                        items: 1
                    }]
                }),
                n = history.pushState;
            history.pushState = function (e) {
                return t.renderCarousel(), n.apply(history, arguments)
            }
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.spendingAssessment = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelector(".-spending"),
                    t = document.querySelector(".-spendingasmt"),
                    n = document.querySelector(".c-select-card");
                (0, i.addEventListener)(e, "click", function () {
                    (0, i.addClassToElement)(t, "-edit-details"), (0, i.addClassToElement)(n, "-active")
                })
            };
        t.spendingAssessment = function () {
            s()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.moredetails = void 0;
        var i = n(1);
        t.moredetails = function () {
            var e = document.querySelector(".c-data-transformation__link"),
                t = document.querySelector(".c-data-transformation");
            (0, i.addEventListener)(e, "click", function () {
                (0, i.elementHasClass)(t, "-hiddenview") ? (0, i.removeClassFromElement)(t, "-hiddenview") : (0, i.addClassToElement)(t, "-hiddenview")
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.viewmore = void 0;
        var i = n(1);
        t.viewmore = function () {
            var e = document.querySelector(".c-spending-assmnt__vm-link"),
                t = document.querySelector(".c-spending-assmnt");
            (0, i.addEventListener)(e, "click", function () {
                (0, i.elementHasClass)(t, "-hiddenview") ? (e.innerText = "View more", (0, i.removeClassFromElement)(t, "-hiddenview")) : ((0, i.addClassToElement)(t, "-hiddenview"), e.innerText = "View less")
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.stickyNav = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelectorAll("c-stickynav__btn")[0],
                    t = (0, i.getHtmlElmentsByClass)("c-stickynav")[0];
                (0, i.addEventListener)(e, "click", function () {
                    (0, i.elementHasClass)(t, "-active") ? (e.innerText = "All Actions", (0, i.removeClassFromElements)("c-stickynav", "-active")) : (e.innerText = "Close", (0, i.removeClassFromElements)("c-stickynav", "-active"), (0, i.addClassToElement)(t, "-active"))
                })
            },
            r = function () {
                var e = (0, i.getHtmlElmentsByClass)("c-stickynav__sublisthead");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.parentElement;
                        (0, i.elementHasClass)(t, "-active") ? (0, i.removeClassFromElements)("c-stickynav__sublistitem", "-active") : ((0, i.removeClassFromElements)("c-stickynav__sublistitem", "-active"), (0, i.addClassToElement)(t, "-active"))
                    })
                })
            },
            a = function () {
                var e = (0, i.getHtmlElmentsByClass)("c-stickynav__subexpand");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.parentElement;
                        (0, i.elementHasClass)(t, "-subactive") ? (0, i.removeClassFromElements)("c-stickynav__subitem", "-subactive") : ((0, i.removeClassFromElements)("c-stickynav__subitem", "-subactive"), (0, i.addClassToElement)(t, "-subactive"))
                    })
                })
            };
        t.stickyNav = function () {
            s(), r(), a()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.deliverytbl = void 0;
        var i = n(1);
        t.deliverytbl = function () {
            var e = (0, i.getQueryHtmlElements)(".c-delivery-table__bodyrow");
            e.map(function (e) {
                (0, i.elementHasClass)(e, "-subrow") || (0, i.addEventListener)(e, "click", function () {
                    for (var t = e.nextElementSibling;
                        (0, i.elementHasClass)(t, "-subrow");)(0, i.elementHasClass)(t, "-subrow") && !(0, i.elementHasClass)(t, "-active") ? ((0, i.addClassToElement)(t, "-active"), (0, i.addClassToElement)(e, "-color")) : (0, i.elementHasClass)(t, "-subrow") && (0, i.elementHasClass)(t, "-active") && ((0, i.removeClassFromElement)(t, "-active"), (0, i.removeClassFromElement)(e, "-color")), t = t.nextElementSibling
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.tooltip = void 0;
        var i = n(1);
        t.tooltip = function e() {
            var e = (0, i.getQueryHtmlElements)(".-tooltip-click");
            document.querySelectorAll(".c-tooltip__wrapper");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.nextElementSibling;
                    (0, i.toggleClassElement)(t, "show")
                })
            })
        }
    }, function (e, t) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        t.workspacetiles = function () {
            var e = document.getElementById("c-workspace-tiles__gridbox");
            if (e)
                for (var t = e.querySelectorAll(".c-radio__input"), n = function (e) {
                    var n = t[e].closest(".c-workspace-tiles__gridbox-items");
                    t[e].onclick = function () {
                        for (var e = document.querySelectorAll(".c-workspace-tiles__gridbox-items"), t = 0; t < e.length; t++) e[t].classList.remove("active");
                        n && n.classList.add("active")
                    }
                }, i = 0; i < t.length; i++) n(i)
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.dashboard = void 0;
        var i = n(1),
            s = function (e) {
                var t = (0, i.getQueryHtmlElements)(e + " .icon-dots");
                t.map(function (t) {
                    (0, i.addEventListener)(t, "click", function () {
                        var n = t.closest(e);
                        (0, i.toggleClassElement)(n, "-active")
                    })
                })
            };
        t.dashboard = function () {
            var e = (0, i.getQueryHtmlElements)(".o-dropdown-btn__btn");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.closest(".o-dropdown-btn");
                    (0, i.toggleClassElement)(t, "-active")
                })
            }), s(".c-admin-card")
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.customSelect = void 0;
        var i = n(1),
            s = function () {
                var e = (0, i.getQueryHtmlElements)(".o-custom-select__selected-text");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.nextElementSibling;
                        (0, i.elementHasClass)(e, "-active") ? ((0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(t, "-active")) : ((0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(t, "-active"))
                    })
                })
            };
        t.customSelect = function () {
            s()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.multiselectElement = void 0;
        var i = n(1);
        t.multiselectElement = function () {
            var e = (0, i.getQueryHtmlElements)(".o-multi-select__arrow");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.parentElement,
                        n = t.nextElementSibling;
                    (0, i.toggleClassElement)(n, "-active"), (0, i.toggleClassElement)(t, "-active"), (0, i.toggleClassElement)(e, "-active")
                })
            })
        }
    }, function (e, t, n) {
        "use strict";

        function i(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.rangeInputOne = t.RangeInputUIOne = void 0;
        var s = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }(),
            r = n(1),
            a = t.RangeInputUIOne = function () {
                function e(t) {
                    i(this, e), this.parent = t, this.mainElement = t.querySelector(".o-range-input-one__wrapper"), this.inverseRight = document.createElement("div"), this.centerRange = document.createElement("div"), this.rightThumb = document.createElement("div"), this.rightSign = document.createElement("div"), this.rightInput = null, this.initProcess(), this.init()
                }
                return s(e, [{
                    key: "addEventListener",
                    value: function (e, t, n) {
                        e && e.addEventListener ? e.addEventListener(t, n, !1) : e && e.attachEvent && e.attachEvent("on" + t, n)
                    }
                }, {
                    key: "addClassToElement",
                    value: function (e, t) {
                        e.classList.add(t)
                    }
                }, {
                    key: "removeClassFromElement",
                    value: function (e, t) {
                        e.classList.remove(t)
                    }
                }, {
                    key: "initProcess",
                    value: function () {
                        var e = "o-range-input-one__range";
                        this.addClassToElement(this.inverseRight, e + "-inverse-right"), this.addClassToElement(this.centerRange, e + "-range"), this.addClassToElement(this.rightThumb, e + "-thumb"), this.addClassToElement(this.rightSign, e + "-sign"), this.rightInput = this.parent.querySelector("." + e + "-right-input")
                    }
                }, {
                    key: "appendElementsToMainElement",
                    value: function () {
                        this.mainElement.prepend(this.inverseRight), this.mainElement.prepend(this.centerRange), this.mainElement.prepend(this.rightSign), this.mainElement.prepend(this.rightThumb)
                    }
                }, {
                    key: "rightRangeSet",
                    value: function () {
                        this.rightInput.value = Math.max(this.rightInput.value);
                        var e = 100 / (parseInt(this.rightInput.max) - parseInt(this.rightInput.min)) * parseInt(this.rightInput.value) - 100 / (parseInt(this.rightInput.max) - parseInt(this.rightInput.min)) * parseInt(this.rightInput.min);
                        this.inverseRight.style.width = 100 - e + "%", this.centerRange.style.right = 100 - e + "%", this.rightThumb.style.left = e + "%", this.rightSign.style.left = e + "%", this.rightSign.innerHTML = "<span>" + this.rightInput.value + "%</span>"
                    }
                }, {
                    key: "onInputRangeSlide",
                    value: function () {
                        var e = this;
                        this.addEventListener(this.rightInput, "input", function () {
                            e.rightRangeSet()
                        })
                    }
                }, {
                    key: "init",
                    value: function () {
                        this.appendElementsToMainElement(), this.rightRangeSet(), this.onInputRangeSlide()
                    }
                }]), e
            }();
        window.RangeInputUIOne = a;
        t.rangeInputOne = function () {
            var e = (0, r.getHtmlElmentsByClass)("o-range-input-one");
            e.map(function (e) {
                new a(e)
            })
        }
    }, function (e, t, n) {
        "use strict";

        function i(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.selectElement = void 0;
        var s = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }(),
            r = n(1),
            a = function () {
                function e(t, n) {
                    i(this, e), "string" == typeof t ? this.selectField = (0, r.getElementById)(t) : this.selectField = t, this.selectField && (this.inputElement = this.selectField.firstElementChild, this.inputOptions = this.selectField.lastElementChild, this.onSelect = n, this.init())
                }
                return s(e, [{
                    key: "showOptions",
                    value: function () {
                        (0, r.addClassToElement)(this.inputElement, "-active"), (0, r.addClassToElement)(this.inputOptions, "-active")
                    }
                }, {
                    key: "hideOptions",
                    value: function () {
                        (0, r.removeClassFromElement)(this.inputElement, "-active"), (0, r.removeClassFromElement)(this.inputOptions, "-active")
                    }
                }, {
                    key: "addValueToInput",
                    value: function (e) {
                        (0, r.addClassToElement)(this.inputElement, "-input"), this.inputElement.innerText = e
                    }
                }, {
                    key: "onElementClick",
                    value: function () {
                        var e = this;
                        (0, r.addEventListener)(this.inputElement, "click", function () {
                            (0, r.elementHasClass)(e.inputElement, "-active") ? e.hideOptions() : e.showOptions()
                        })
                    }
                }, {
                    key: "onOptionsClick",
                    value: function () {
                        var e = this,
                            t = (0, r.getChildElements)(this.inputOptions);
                        t.map(function (t) {
                            (0, r.addEventListener)(t, "click", function () {
                                var n = t.innerText,
                                    i = t.getAttribute("data-value");
                                e.addValueToInput(n), e.onSelect(n, i), e.hideOptions()
                            })
                        })
                    }
                }, {
                    key: "init",
                    value: function () {
                        this.onElementClick(), this.onOptionsClick()
                    }
                }]), e
            }();
        t.selectElement = function () {
            var e = (0, r.getHtmlElmentsByClass)("c-select");
            e.map(function (e) {
                new a(e, function (e, t) { })
            }), window.SelectInput = a
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.existingcc = void 0;
        var i = n(1);
        t.existingcc = function () {
            var e = (0, i.getQueryHtmlElements)(".c-existing-cc .icon-dots");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.closest(".c-existing-cc");
                    (0, i.toggleClassElement)(t, "-active")
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.exploreSearch = void 0;
        var i = n(1),
            s = document.querySelector(".c-search-explore__search-textfield"),
            r = document.querySelector(".c-search-explore__search-text-close"),
            a = document.querySelector(".c-search-explore__search-results"),
            o = document.querySelector(".c-search-explore__filters"),
            l = document.querySelector(".c-search-explore__search-actions-item.-filter"),
            c = document.querySelector(".c-search-explore__search-actions-item.-star-icon"),
            d = document.querySelector(".c-search-explore__modal"),
            u = document.querySelector(".c-save-search__close-icon"),
            m = document.querySelector("body"),
            h = function () {
                (0, i.addClassToElement)(r, "-active"), (0, i.addClassToElement)(a, "-active")
            },
            v = function () {
                (0, i.removeClassFromElement)(r, "-active"), (0, i.removeClassFromElement)(a, "-active")
            },
            f = function () {
                (0, i.addClassToElement)(l, "-active"), (0, i.addClassToElement)(o, "-active")
            },
            p = function () {
                (0, i.removeClassFromElement)(l, "-active"), (0, i.removeClassFromElement)(o, "-active")
            },
            y = function () {
                (0, i.addClassToElement)(d, "-active"), m.style.overflow = "hidden"
            },
            E = function () {
                (0, i.removeClassFromElement)(d, "-active"), m.style.overflow = "visible"
            };
        t.exploreSearch = function () {
            (0, i.addEventListener)(s, "focus", function () {
                h(), p()
            }), (0, i.addEventListener)(r, "click", function () {
                v(), p()
            }), (0, i.addEventListener)(l, "click", function () {
                (0, i.elementHasClass)(l, "-active") ? (v(), p()) : (f(), v())
            }), (0, i.addEventListener)(c, "click", function () {
                y()
            }), (0, i.addEventListener)(u, "click", function () {
                E()
            }), (0, i.addEventListener)(document, "click", function (e) {
                var t = e.target.closest(".c-search-explore__input-bar");
                null === t && (v(), p()), (0, i.elementHasClass)(e.target, "c-search-explore__modal") && E()
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.formActions = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelector(".site-wrap");
                if (e) {
                    var t = e.offsetHeight,
                        n = (document.querySelector(".c-form-list"), document.querySelector(".c-form-tab__filters"));
                    n && (n.style.height = t + "px", document.querySelector(".c-form-tab__filter").addEventListener("click", function () {
                        e.classList.toggle("-active")
                    }))
                }
            },
            r = function () {
                s();
                var e = document.querySelector(".c-form-slider__trigger"),
                    t = document.querySelector(".c-form-slider__content");
                (0, i.addEventListener)(e, "click", function () {
                    s(), (0, i.elementHasClass)(e, "-active") ? ((0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(t, "-active")) : ((0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(t, "-active"))
                }), window.addEventListener("resize", function () {
                    s()
                })
            },
            a = function (e, t, n, s) {
                var r = document.querySelector(e),
                    a = document.querySelector(t),
                    o = document.querySelector(n),
                    l = document.querySelector(s);
                (0, i.addEventListener)(r, "click", function () {
                    (0, i.removeClassFromElement)(o, "-active"), (0, i.removeClassFromElement)(l, "-active"), (0, i.elementHasClass)(r, "-active") ? ((0, i.removeClassFromElement)(r, "-active"), (0, i.removeClassFromElement)(a, "-active")) : ((0, i.addClassToElement)(r, "-active"), (0, i.addClassToElement)(a, "-active"))
                })
            },
            o = function (e, t, n, s) {
                var r = document.querySelector(e),
                    a = document.querySelector(t),
                    o = document.querySelector(n),
                    l = document.querySelector(s);
                (0, i.addEventListener)(o, "click", function () {
                    (0, i.removeClassFromElement)(r, "-active"), (0, i.removeClassFromElement)(a, "-active"), (0, i.elementHasClass)(o, "-active") ? ((0, i.removeClassFromElement)(o, "-active"), (0, i.removeClassFromElement)(l, "-active")) : ((0, i.addClassToElement)(o, "-active"), (0, i.addClassToElement)(l, "-active"))
                })
            };
        t.formActions = function () {
            r(), a(".c-form-tab__filter-trig", ".c-form-tab__filters", ".c-form-tab__search-trig", ".c-form-tab__search-input-div"), o(".c-form-tab__filter-trig", ".c-form-tab__filters", ".c-form-tab__search-trig", ".c-form-tab__search-input-div"), a(".c-workspace-tabs__filter-trig", ".c-workspace-tabs__filters", ".c-workspace-tabs__search-trig", ".c-workspace-tabs__search-input-div"), o(".c-workspace-tabs__filter-trig", ".c-workspace-tabs__filters", ".c-workspace-tabs__search-trig", ".c-workspace-tabs__search-input-div")
        }
    }, function (e, t) {
        "use strict";

        function n(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        var i = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }(),
            s = function () {
                function e(t) {
                    n(this, e), this.headerContainer = document.getElementById(t), this.headerHeight = 0, this.headerSibling = null, this.currentScrollValue = 0, this.className = !0, this.headerContainer && this.init()
                }
                return i(e, [{
                    key: "getCurrentScroll",
                    value: function () {
                        return window.pageYOffset
                    }
                }, {
                    key: "setInitialData",
                    value: function () {
                        this.headerHeight = this.headerContainer.offsetHeight, this.headerContainer.classList.add("header-scroll")
                    }
                }, {
                    key: "addHeaderSibling",
                    value: function () {
                        if (isFromContainter === "true") {
                            this.headerSibling = document.createElement("div"), this.headerContainer.parentElement.insertBefore(this.headerSibling, this.headerContainer.nextSibling), this.headerSibling.classList.add("header-sibling"), this.headerSibling.style.height = this.headerHeight + "px"
                        }
                    }
                }, {
                    key: "animateHeaderOnScroll",
                    value: function () {
                        var e = this.getCurrentScroll();
                        this.className ? this.headerContainer.classList.add("-first") : this.headerContainer.classList.remove("-first"), e >= this.headerHeight ? (this.headerContainer.classList.add("-fixed"), this.headerContainer.style.top = "0px", this.currentScrollValue > e ? (this.headerContainer.classList.add("-show"), this.headerContainer.style.top = "0") : (this.headerContainer.classList.remove("-show"), this.headerContainer.style.top = "0px"), this.className = !1) : (this.className = !0, this.currentScrollValue > e ? this.headerContainer.classList.add("-fixed") : (this.headerContainer.classList.remove("-fixed"), this.headerContainer.style.top = "0")), this.currentScrollValue = e
                    }
                }, {
                    key: "onWindowScroll",
                    value: function () {
                        var e = this;
                        window.addEventListener("scroll", function () {
                            e.animateHeaderOnScroll()
                        })
                    }
                }, {
                    key: "init",
                    value: function () {
                        this.setInitialData(), this.addHeaderSibling(), this.onWindowScroll(), this.animateHeaderOnScroll()
                    }
                }]), e
            }(),
            r = t.headerScroll = function () {
                new s("header-container")
            };
        window.headerScroll = r
    }, function (e, t) {
        "use strict";

        function n(e) {
            if (Array.isArray(e)) {
                for (var t = 0, n = Array(e.length); t < e.length; t++) n[t] = e[t];
                return n
            }
            return Array.from(e)
        }

        function i(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        var s = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }(),
            r = t.IndustryXCarousel = function () {
                function e(t) {
                    var n = this,
                        s = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {};
                    if (i(this, e), this.htmlElement = document.getElementById(t), this.itemsMarginSpace = 0, this.options = this.validateAndUpdateOptions(s), this.width = 0, this.itemWidth = 0, this.mainContainer = null, this.mainWrapper = null, this.wrapperElement = null, this.carouselItems = null, this.totalItemCount = 0, this.currentSlide = 1, this.leftArrow = null, this.rightArrow = null, this.arrowWrapper = null, this.dotsContainer = null, this.currentPage = 0, this.totalPages = void 0, this.slides - null, this.htmlElement && (this.init(), this.renderCarousel(), this.addEventListener(window, "resize", function () {
                        n.renderCarousel()
                    }), this.options.autoscroll)) var r = 3e3,
                        a = function e() {
                            n.currentSlide == n.totalItemCount ? (clearInterval(o), r = 0) : (clearInterval(o), r = 3e3), o = window.setInterval(e, r), n.gotoNextSlide(), n.dotsStatus()
                        },
                        o = window.setInterval(a, r)
                }
                return s(e, [{
                    key: "init",
                    value: function () {
                        var e = this;
                        this.mainContainer = document.createElement("div"), this.wrapperElement = document.createElement("div"), this.mainWrapper = document.createElement("div"), this.leftArrow = document.createElement("div"), this.rightArrow = document.createElement("div"), this.arrowWrapper = document.createElement("div"), this.dotsContainer = document.createElement("div"), this.addEventListener(this.rightArrow, "click", function () {
                            e.gotoNextSlide(), e.appendDisableClassInArrows(), e.dotsStatus()
                        }), this.addEventListener(this.leftArrow, "click", function () {
                            e.gotoPreviousSlide(), e.appendDisableClassInArrows(), e.dotsStatus()
                        })
                    }
                }, {
                    key: "setDataByWindowSize",
                    value: function () {
                        var e = window.innerWidth,
                            t = this.options.responsive;
                        if (t && t.length > 0) {
                            var n = t.find(function (t) {
                                return t.maxWidth <= e
                            });
                            n ? this.options.items = n.items : this.options.items = 1
                        }
                    }
                }, {
                    key: "initCalculation",
                    value: function () {
                        this.width = this.htmlElement.offsetWidth, this.itemWidth = this.width / this.options.items - this.itemsMarginSpace, this.currentSlide = 1
                    }
                }, {
                    key: "getHtmlCollection",
                    value: function (e) {
                        for (var t = [], n = 0; n < e.length; n++) t.push(e[n]);
                        return t
                    }
                }, {
                    key: "addEventListener",
                    value: function (e, t, n) {
                        e && e.addEventListener ? e.addEventListener(t, n, !1) : e && e.attachEvent && e.attachEvent("on" + t, n)
                    }
                }, {
                    key: "addClassToElement",
                    value: function (e, t) {
                        e.classList.add(t)
                    }
                }, {
                    key: "removeClassFromElement",
                    value: function (e, t) {
                        e.classList.remove(t)
                    }
                }, {
                    key: "validateAndUpdateOptions",
                    value: function (e) {
                        var t = e.items,
                            n = void 0 === t ? 1 : t,
                            i = e.firstItemFixed,
                            s = void 0 !== i && i,
                            r = e.arrows,
                            a = void 0 !== r && r,
                            o = e.dots,
                            l = void 0 !== o && o,
                            c = e.margin,
                            d = void 0 === c ? 0 : c;
                        return this.itemsMarginSpace = d, Object.assign(e, {
                            items: n,
                            firstItemFixed: s,
                            arrows: a,
                            dots: l
                        })
                    }
                }, {
                    key: "setCarouselItems",
                    value: function () {
                        this.carouselItems = this.getHtmlCollection(this.htmlElement.querySelectorAll(".carousel-item"))
                    }
                }, {
                    key: "getScrollableIndex",
                    value: function (e, t) {
                        "dot" != t && (e -= 1);
                        var n = this.totalItemCount - this.options.items;
                        return e < 1 ? e = 0 : e > n && (this.options.autoscroll ? (e = 0, this.wrapperElement.style.transition = "none") : e = n), e
                    }
                }, {
                    key: "getScrollTransition",
                    value: function (e, t) {
                        var n = this.getScrollableIndex(e, t);
                        return this.currentSlide = n + 1, this.getItemsWidth() * n
                    }
                }, {
                    key: "goToSlide",
                    value: function (e, t) {
                        this.options.autoscroll && (this.wrapperElement.style.transition = "all 0.5s ease 0s");
                        var n = this.getScrollTransition(e, t);
                        this.currentPage = Math.round(e / this.options.items), this.options.autoscroll && (0 != this.currentPage && (this.currentPage = this.currentPage - 1), this.currentPage > this.totalItemCount - 2 && (this.currentPage = 0)), this.appendDisableClassInArrows(), this.wrapperElement.style.transform = "translate3d(-" + n + "px, 0px, 0px)"
                    }
                }, {
                    key: "getItemsWidth",
                    value: function () {
                        return this.itemWidth + this.itemsMarginSpace
                    }
                }, {
                    key: "appendWrapperOnElements",
                    value: function (e) {
                        var t = this;
                        this.addClassToElement(this.mainContainer, "man-x-container"), this.addClassToElement(this.wrapperElement, "carousel-wrapper"), this.wrapperElement.style.width = this.getItemsWidth() * this.totalItemCount + "px", this.wrapperElement.style.transition = "all 0.5s ease 0s", e.map(function (e) {
                            t.wrapperElement.append(e)
                        }), this.options.autoscroll && (this.clones = this.getHtmlCollection(this.wrapperElement.querySelectorAll(".clone")), this.clones.map(function (e) {
                            e.remove()
                        }), this.slides = this.wrapperElement.querySelectorAll(".carousel-item"), this.firstClone = this.slides[0].cloneNode(!0), this.addClassToElement(this.firstClone, "clone"), this.wrapperElement.append(this.firstClone), this.carouselItems = this.getHtmlCollection(this.wrapperElement.querySelectorAll(".carousel-item")), this.options.firstItemFixed ? this.totalItemCount = this.carouselItems.length - 1 : this.totalItemCount = this.carouselItems.length, this.wrapperElement.style.width = this.getItemsWidth() * this.totalItemCount + "px"), this.mainWrapper.append(this.wrapperElement), this.mainContainer.append(this.mainWrapper), this.htmlElement.append(this.mainContainer)
                    }
                }, {
                    key: "appendWrapper",
                    value: function () {
                        var e = this;
                        this.addClassToElement(this.mainWrapper, "main-x-wrapper"), this.carouselItems.map(function (t) {
                            t.style.width = e.itemWidth + 20 + "px", t.style.marginRight = e.itemsMarginSpace / 4 + "px", t.style.marginLeft = e.itemsMarginSpace / 4 + "px", t.style.float = "left"
                        }), this.options.firstItemFixed ? (this.totalItemCount = this.carouselItems.length - 1, this.mainWrapper.append(this.carouselItems[0]), this.addClassToElement(this.carouselItems[0], "x-first-element"), this.appendWrapperOnElements(this.carouselItems.slice(1, this.carouselItems.length)), this.options.items = this.options.items - 1) : (this.totalItemCount = this.carouselItems.length, this.appendWrapperOnElements(this.carouselItems)), this.totalItemCount > this.options.items ? this.options.arrows = !0 : this.options.arrows = !1
                    }
                }, {
                    key: "gotoPreviousSlide",
                    value: function () {
                        this.goToSlide(this.currentSlide - 1)
                    }
                }, {
                    key: "gotoNextSlide",
                    value: function () {
                        this.goToSlide(this.currentSlide + 1)
                    }
                }, {
                    key: "totalSlides",
                    value: function () {
                        return this.totalItemCount - this.options.items
                    }
                }, {
                    key: "isLastSlide",
                    value: function () {
                        var e = this.totalSlides();
                        return e + 1 === this.currentSlide
                    }
                }, {
                    key: "isFirstSlide",
                    value: function () {
                        return 1 === this.currentSlide
                    }
                }, {
                    key: "appendDisableClassInArrows",
                    value: function () {
                        this.isFirstSlide() ? this.addClassToElement(this.leftArrow, "disabled") : this.removeClassFromElement(this.leftArrow, "disabled"), this.isLastSlide() ? this.addClassToElement(this.rightArrow, "disabled") : this.removeClassFromElement(this.rightArrow, "disabled")
                    }
                }, {
                    key: "appendArrows",
                    value: function () {
                        this.addClassToElement(this.leftArrow, "x-arrow"), this.addClassToElement(this.leftArrow, "x-left-arrow"), this.leftArrow.innerHTML = this.options.leftArrow ? this.options.leftArrow : "left", this.addClassToElement(this.rightArrow, "x-arrow"), this.addClassToElement(this.rightArrow, "x-right-arrow"), this.rightArrow.innerHTML = this.options.leftArrow ? this.options.rightArrow : "right", this.addClassToElement(this.arrowWrapper, "x-arrows"), this.arrowWrapper.append(this.leftArrow), this.arrowWrapper.append(this.rightArrow), this.htmlElement.append(this.arrowWrapper), this.options.arrows ? this.removeClassFromElement(this.arrowWrapper, "-hide") : this.addClassToElement(this.arrowWrapper, "-hide")
                    }
                }, {
                    key: "goToPage",
                    value: function (e, t) {
                        this.currentSlide = e * this.options.items, this.currentPage = e, this.goToSlide(e * this.options.items, t)
                    }
                }, {
                    key: "appendDots",
                    value: function () {
                        var e = this;
                        if (this.options.dots) {
                            this.addClassToElement(this.dotsContainer, "x-dots-container"), this.dotsContainer.innerHTML = null;
                            var t = document.createElement("div");
                            this.addClassToElement(t, "x-dots-wrapper"), this.totalPages = Math.ceil(this.totalItemCount / this.options.items), this.options.autoscroll && (this.totalPages = this.totalPages - 1), this.totalPages > 1 && ([].concat(n(Array(this.totalPages))).map(function (n, i) {
                                var s = document.createElement("span");
                                e.addClassToElement(s, "x-dot-item"), t.append(s), e.addEventListener(s, "click", function () {
                                    e.goToPage(i, "dot"), e.dotsStatus()
                                })
                            }), this.dotsContainer.append(t), this.mainContainer.append(this.dotsContainer))
                        }
                    }
                }, {
                    key: "dotsStatus",
                    value: function () {
                        var e = this;
                        if (this.options.dots && this.dotsContainer.firstElementChild) {
                            var t = this.getHtmlCollection(this.dotsContainer.firstElementChild.children);
                            t.map(function (t, n) {
                                n === e.currentPage ? e.removeClassFromElement(t, "-disabled") : e.addClassToElement(t, "-disabled")
                            })
                        }
                    }
                }, {
                    key: "renderCarousel",
                    value: function () {
                        this.htmlElement && (this.setDataByWindowSize(), this.initCalculation(), this.setCarouselItems(), this.appendWrapper(), this.appendArrows(), this.appendDisableClassInArrows(), this.appendDots(), this.goToSlide(0), this.dotsStatus())
                    }
                }]), e
            }();
        t.xCarousel = function () {
            window.IndustryXCarousel = r
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.logocard = void 0;
        var i = n(1);
        t.logocard = function () {
            var e = (0, i.getQueryHtmlElements)(".c-logo-card .icon-dots");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.closest(".c-logo-card");
                    (0, i.toggleClassElement)(t, "-active")
                })
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.manageClient = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelector(".site-wrap");
                if (e) var t = document.querySelector(".site-wrap").offsetHeight;
                var n = document.querySelector(".c-default-table"),
                    i = document.querySelector(".c-delivery-table"),
                    s = document.querySelector(".c-mc-filter__filters");
                n && (s.style.height = t + "px", document.querySelector(".c-mc-filter__filter-trig").addEventListener("click", function () {
                    e.classList.toggle("-active")
                })), i && (s.style.height = t + "px", document.querySelector(".c-mc-filter__filter-trig").addEventListener("click", function () {
                    e.classList.toggle("-active")
                }))
            },
            r = function () {
                s(), window.addEventListener("resize", function () {
                    s()
                })
            },
            a = function () {
                var e = document.querySelector(".c-mc-filter__filter-trig"),
                    t = document.querySelector(".c-mc-filter__filters"),
                    n = document.querySelector(".c-mc-filter__search-trig"),
                    s = document.querySelector(".c-mc-filter__search-input-div");
                (0, i.addEventListener)(e, "click", function () {
                    (0, i.removeClassFromElement)(n, "-active"), (0, i.removeClassFromElement)(s, "-active"), (0, i.elementHasClass)(e, "-active") ? ((0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(t, "-active")) : ((0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(t, "-active"))
                })
            },
            o = function () {
                var e = document.querySelector(".c-mc-filter__search-icon"),
                    t = document.querySelector(".c-mc-filter__input-field");
                (0, i.addEventListener)(e, "click", function () {
                    var n = e.parentElement;
                    n.classList.contains("-active") ? ((0, i.removeClassFromElement)(n, "-active"), (0, i.removeClassFromElement)(t, "-active")) : ((0, i.addClassToElement)(n, "-active"), (0, i.addClassToElement)(t, "-active"))
                })
            },
            l = function () {
                var e = (0, i.getQueryHtmlElements)(".c-checkbox");
                e.map(function (e) {
                    (0, i.addEventListener)(e, "click", function () {
                        var t = e.parentElement;
                        (0, i.elementHasClass)(t, "-active") ? (0, i.removeClassFromElement)(t, "-active") : (0, i.addClassToElement)(t, "-active")
                    })
                })
            };
        t.manageClient = function () {
            l(), a(), o(), r()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.moreOptions = void 0;
        var i = n(1);
        t.moreOptions = function () {
            var e = (0, i.getQueryHtmlElements)(".-more-options-btn");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.nextElementSibling;
                    (0, i.elementHasClass)(t, "-more-expand") ? ((0, i.removeClassFromElement)(t, "-more-expand"), (0, i.removeClassFromElement)(e, "-active")) : ((0, i.addClassToElement)(t, "-more-expand"), (0, i.addClassToElement)(e, "-active"))
                })
            })
        }
    }, function (e, t, n) {
        "use strict";

        function i(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.multiToggle = void 0;
        var s = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }(),
            r = n(1),
            a = function () {
                function e(t) {
                    var n = this;
                    i(this, e), this.htmlElement = t, this.itemsArray = [], this.itemsWidthArray = [], this.activeElementBar = null, this.setActiveElementBar(), this.setItemsArray(), this.setClickActions(), this.init(), window.addEventListener("resize", function () {
                        n.init()
                    }), document.addEventListener("click", function () {
                        n.init()
                    }), window.addEventListener("load", function () {
                        n.init()
                    }, !1)
                }
                return s(e, [{
                    key: "setActiveElementBar",
                    value: function () {
                        this.activeElementBar = this.htmlElement.querySelector(".o-multi-toggle__select")
                    }
                }, {
                    key: "setItemsArray",
                    value: function () {
                        this.itemsArray = (0, r.htmlCollectionToArray)(this.htmlElement.querySelectorAll(".o-multi-toggle__item"))
                    }
                }, {
                    key: "setActiveClass",
                    value: function (e) {
                        this.itemsArray.map(function (e) {
                            (0, r.removeClassFromElement)(e, "-active")
                        }), (0, r.addClassToElement)(e, "-active")
                    }
                }, {
                    key: "setClickActions",
                    value: function () {
                        var e = this;
                        this.itemsArray.map(function (t) {
                            (0, r.addEventListener)(t, "click", function () {
                                e.init(), e.setActiveClass(t), e.animateOnActive()
                            })
                        })
                    }
                }, {
                    key: "setItemsWidth",
                    value: function () {
                        this.itemsWidthArray = this.itemsArray.map(function (e) {
                            return {
                                width: e.offsetWidth,
                                height: e.offsetHeight
                            }
                        })
                    }
                }, {
                    key: "getLeftValue",
                    value: function (e) {
                        var t = 5;
                        return this.itemsWidthArray.map(function (n, i) {
                            if (i < e) {
                                var s = n.width;
                                t += s + 10
                            }
                        }), t
                    }
                }, {
                    key: "animateOnActive",
                    value: function () {
                        var e = this;
                        this.itemsArray.map(function (t, n) {
                            if ((0, r.elementHasClass)(t, "-active")) {
                                var i = e.itemsWidthArray[n],
                                    s = i.width,
                                    a = i.height,
                                    o = e.getLeftValue(n);
                                e.activeElementBar.style.width = s + "px", e.activeElementBar.style.height = a + "px", e.activeElementBar.style.left = o + "px"
                            }
                        })
                    }
                }, {
                    key: "init",
                    value: function () {
                        this.setItemsWidth(), this.animateOnActive()
                    }
                }]), e
            }(),
            o = t.multiToggle = function () {
                var e = (0, r.getQueryHtmlElements)(".o-multi-toggle");
                e.map(function (e) {
                    new a(e)
                })
            };
        window.multiToggle = o, window.MultiToggle = a
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.savedSearch = void 0;
        var i = n(1),
            s = document.querySelector(".c-saved-search__modal"),
            r = document.querySelector(".c-edit-search__close-icon"),
            a = document.querySelector(".c-edit-search__calcel"),
            o = document.querySelector("body"),
            l = function () {
                (0, i.addClassToElement)(s, "-active"), o.style.overflow = "hidden"
            },
            c = function () {
                (0, i.removeClassFromElement)(s, "-active"), o.style.overflow = "visible"
            };
        t.savedSearch = function () {
            var e = (0, i.getHtmlElmentsByClass)("c-saved-search__edit");
            e.map(function (e) {
                (0, i.addEventListener)(e, "click", function () {
                    l()
                })
            }), (0, i.addEventListener)(r, "click", function () {
                c()
            }), (0, i.addEventListener)(a, "click", function () {
                c()
            })
        }
    }, function (e, t) {
        "use strict";

        function n(e, t) {
            if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
        }
        Object.defineProperty(t, "__esModule", {
            value: !0
        });
        var i = function () {
            function e(e, t) {
                for (var n = 0; n < t.length; n++) {
                    var i = t[n];
                    i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i)
                }
            }
            return function (t, n, i) {
                return n && e(t.prototype, n), i && e(t, i), t
            }
        }();
        HTMLElement.prototype.wrapAll = function (e) {
            var t = e.length ? e[0] : e,
                n = t.parentNode,
                i = t.nextSibling;
            for (this.appendChild(t); e.length;) this.appendChild(e[0]);
            i ? n.insertBefore(this, i) : n.appendChild(this)
        };
        var s = function () {
            function e(t) {
                n(this, e), this.progenitorNode = t, this.options = {
                    scrollSpeedDiv: 2,
                    scrollSpeedStep: 200
                }, this.state = {
                    containerTranslation: 0,
                    wrapperWidth: null
                }, this.setup(), this.bindEvents()
            }
            return i(e, [{
                key: "setup",
                value: function () {
                    this.tabsContainer = this.progenitorNode.querySelector(".c-scrollable-tabs__container"), this.tabsContainer.style.position = "relative", this.tabsContWrapper = document.createElement("div"), this.tabsContWrapper.className = "c-scrollable-tabs__container-wrapper", this.tabsContWrapper.wrapAll(this.tabsContainer), this.tabs = this.tabsContainer.querySelectorAll(".c-scrollable-tabs__element"), this.state.wrapperWidth = this.tabsContWrapper.offsetWidth, this.insertArrows()
                }
            }, {
                key: "bindEvents",
                value: function () {
                    var e = this;
                    this.tabs.forEach(function (t) {
                        t.addEventListener("click", function () {
                            e.selectTab(t)
                        })
                    }), this.tabsContainer.addEventListener("wheel", function (t) {
                        t.preventDefault();
                        var n = t.wheelDelta;
                        e.setContainerTranslation({
                            wheelDelta: n
                        })
                    }), this.arrowLeft.addEventListener("click", function () {
                        e.setContainerTranslation({
                            wheelDelta: e.options.scrollSpeedStep
                        })
                    }), this.arrowRight.addEventListener("click", function () {
                        e.setContainerTranslation({
                            wheelDelta: -e.options.scrollSpeedStep
                        })
                    });
                    var t = new ResizeObserver(function (t) {
                        t.forEach(function (t) {
                            e.init(), e.setContainerTranslation({
                                wheelDelta: 0
                            })
                        })
                    });
                    t.observe(this.tabsContWrapper), t.observe(this.tabsContainer)
                }
            }, {
                key: "init",
                value: function () {
                    this.state.wrapperWidth = this.tabsContWrapper.offsetWidth, this.manageArrows()
                }
            }, {
                key: "insertArrows",
                value: function () {
                    this.arrowLeft = document.createElement("span"),
                        this.arrowLeft.className = "c-scrollable-tabs__arrow", this.arrowLeft.classList.add("icon-chevron-left"), this.arrowLeft.classList.add("-left"), this.arrowRight = document.createElement("span"), this.arrowRight.className = "c-scrollable-tabs__arrow", this.arrowRight.classList.add("icon-chevron-right"), this.arrowRight.classList.add("-right"), this.progenitorNode.prepend(this.arrowLeft), this.progenitorNode.append(this.arrowRight)
                }
            }, {
                key: "manageArrows",
                value: function () {
                    var e = this.tabsContWrapper.offsetWidth < this.tabsContainer.offsetWidth;
                    this.changeArrowsVisibility({
                        left: e,
                        right: e
                    })
                }
            }, {
                key: "changeArrowsVisibility",
                value: function (e) {
                    var t = e.left,
                        n = e.right;
                    return this.tabsContWrapper.offsetWidth >= this.tabsContainer.offsetWidth - 1 ? (void 0 !== t && this.arrowLeft.classList.toggle("-hidden", !0), void (void 0 !== n && this.arrowRight.classList.toggle("-hidden", !0))) : (void 0 !== t && this.arrowLeft.classList.toggle("-hidden", !1), void 0 !== n && this.arrowRight.classList.toggle("-hidden", !1), void 0 !== t && this.arrowLeft.classList.toggle("-disabled", !t), void (void 0 !== n && this.arrowRight.classList.toggle("-disabled", !n)))
                }
            }, {
                key: "selectTab",
                value: function (e) {
                    this.centerToTab(e)
                }
            }, {
                key: "centerToTab",
                value: function (e) {
                    var t = this.tabsContWrapper.offsetWidth,
                        n = e.offsetWidth,
                        i = Math.ceil((t - n) / 2),
                        s = e.offsetLeft,
                        r = s - i;
                    this.setContainerTranslation({
                        value: -r
                    })
                }
            }, {
                key: "setContainerTranslation",
                value: function (e) {
                    var t = e.wheelDelta,
                        n = e.value,
                        i = this.state,
                        s = i.containerTranslation,
                        r = i.wrapperWidth,
                        a = n || s + Math.floor(t / this.options.scrollSpeedDiv),
                        o = Math.min(0, a),
                        l = Math.max(r - this.tabsContainer.offsetWidth, o);
                    this.state.containerTranslation = l, this.tabsContainer.style.transform = "translateX(" + l + "px)", this.changeArrowsVisibility({
                        left: l < 0,
                        right: Math.abs(l) + r < this.tabsContainer.offsetWidth
                    })
                }
            }]), e
        }(),
            r = function () {
                document.querySelectorAll(".c-scrollable-tabs").forEach(function (e) {
                    new s(e)
                })
            };
        t.scrollTabsInit = function () {
            r()
        };
        window.scrollTabsSlide = r
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.searchScroll = void 0;
        var i = n(1),
            s = function () {
                return window.pageYOffset
            },
            r = function () {
                var e = document.querySelector(".c-search-explore__heading");
                if (e) {
                    var t = e.offsetHeight,
                        n = e.offsetTop;
                    return t + n - 30
                }
                return null
            },
            a = function () {
                var e = 188,
                    t = r(),
                    n = s(),
                    a = document.querySelector(".c-search-explore__input-bar"),
                    o = document.querySelector(".c-search-explore__input-placeholder");
                a && o && (t < n ? ((0, i.addClassToElement)(a, "-fixed-bar"), o.style.height = e + "px") : ((0, i.removeClassFromElement)(a, "-fixed-bar"), o.style.height = "0px"))
            };
        t.searchScroll = function () {
            a(), window.addEventListener("scroll", function () {
                a()
            })
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.challenegeList = void 0;
        var i = n(1),
            s = function () {
                var e = document.querySelectorAll("c-showhide-challenge__btn")[0],
                    t = (0, i.getHtmlElmentsByClass)("c-showhide-challenge")[0];
                (0, i.addEventListener)(e, "click", function () {
                    (0, i.elementHasClass)(t, "-active") ? (0, i.removeClassFromElements)("c-showhide-challenge", "-active") : ((0, i.removeClassFromElements)("c-showhide-challenge", "-active"), (0, i.addClassToElement)(t, "-active"))
                })
            };
        t.challenegeList = function () {
            s()
        }
    }, function (e, t, n) {
        "use strict";
        Object.defineProperty(t, "__esModule", {
            value: !0
        }), t.tabAction = void 0;
        var i = n(1),
            s = function () {
                return window.pageYOffset
            },
            r = function () {
                var e = document.querySelector(".c-search-explore");
                if (e) {
                    var t = e.offsetHeight,
                        n = e.offsetTop;
                    return t + n - 105
                }
                return null
            },
            a = function () {
                var e = document.querySelector(".c-create-workspace-header");
                if (e) {
                    var t = e.offsetHeight,
                        n = e.offsetTop;
                    return t + n
                }
                return null
            },
            o = function () {
                var e = 51,
                    t = r(),
                    n = s(),
                    a = document.querySelector(".c-explore-tab__placeholder"),
                    o = document.querySelector(".c-explore-tab");
                o && a && (t < n ? ((0, i.addClassToElement)(o, "-fixed"), a.style.height = e + "px") : ((0, i.removeClassFromElement)(o, "-fixed"), a.style.height = "0px"))
            },
            l = function () {
                var e = 55,
                    t = a(),
                    n = s(),
                    r = document.querySelector(".c-asset-tab__placeholder"),
                    o = document.querySelector(".c-asset-tab");
                o && r && (t < n ? ((0, i.addClassToElement)(o, "-fixed"), r.style.height = e + "px") : ((0, i.removeClassFromElement)(o, "-fixed"), r.style.height = "0px"))
            },
            c = function () {
                var e = document.querySelector(".c-explore-tab__sort-trig");
                (0, i.addEventListener)(e, "click", function () {
                    var t = e.nextElementSibling;
                    (0, i.elementHasClass)(e, "-active") ? ((0, i.removeClassFromElement)(e, "-active"), (0, i.removeClassFromElement)(t, "-active")) : ((0, i.addClassToElement)(e, "-active"), (0, i.addClassToElement)(t, "-active"))
                })
            },
            d = document.querySelector(".c-spending-assess-tab__viewdetails"),
            u = document.querySelector(".c-spending-assess-tab__list"),
            m = function () {
                (0, i.addEventListener)(d, "click", function () {
                    var e = d.parentElement.parentElement;
                    e.classList.contains("-show") ? ((0, i.removeClassFromElement)(e, "-show"), (0, i.removeClassFromElement)(u, "-active")) : ((0, i.addClassToElement)(e, "-show"), (0, i.addClassToElement)(u, "-active"))
                })
            };
        t.tabAction = function () {
            o(), l(), c(), m(), window.addEventListener("scroll", function () {
                o(), l()
            })
        }
    }]);
}

