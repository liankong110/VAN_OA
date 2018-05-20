var TINY = {};

function T$(i) {
    return document.getElementById(i)
}

TINY.box = function() {
    var bigBox, myMask, contBox, ic, iu, iw, ih, ia, f = 0;
    return {
        show: function(contBody, uState, myWidth, myHeight, a, t) {
            if (!f) {
                bigBox = document.createElement('div');
                bigBox.id = 'tinybox';
                myMask = document.createElement('div');
                myMask.id = 'tinymask';
                contBox = document.createElement('div');
                contBox.id = 'tinycontent';
                document.body.appendChild(myMask);
                document.body.appendChild(bigBox);
                bigBox.appendChild(contBox);
                myMask.onclick = TINY.box.hide;
                window.onresize = TINY.box.resize;
                f = 1;
            }
            if (!a && !uState) {
                bigBox.style.width = myWidth ? myWidth + 'px' : 'auto';
                bigBox.style.height = myHeight ? myHeight + 'px' : 'auto';
                bigBox.style.backgroundImage = 'none';
                contBox.innerHTML = contBody;
            } else {
                contBox.style.display = 'none';
                bigBox.style.width = bigBox.style.height = '100px'
            }
            this.mask();
            ic = contBody;
            iu = uState;
            iw = myWidth;
            ih = myHeight;
            ia = a;
            this.alpha(myMask, 1, 80, 3);
            if (t) {
                setTimeout(function() { TINY.box.hide() }, 1000 * t);
            }
        },
        fill: function(contBody, uState, myWidth, myHeight, a) {
            if (uState) {
                bigBox.style.backgroundImage = '';
                var x = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
                x.onreadystatechange = function() {
                    if (x.readyState == 4 && x.status == 200) {
                        TINY.box.psh(x.responseText, myWidth, myHeight, a)
                    }
                };
                x.open('GET', contBody, 1);
                x.send(null)
            } else {
                this.psh(contBody, myWidth, myHeight, a)
            }
        },
        psh: function(contBody, myWidth, myHeight, a) {
            if (a) {
                if (!myWidth || !myHeight) {
                    var x = bigBox.style.width,
					y = bigBox.style.height;
                    contBox.innerHTML = contBody;
                    bigBox.style.width = myWidth ? myWidth + 'px' : '';
                    bigBox.style.height = myHeight ? myHeight + 'px' : '';
                    contBox.style.display = '';
                    myWidth = parseInt(contBox.offsetWidth);
                    myHeight = parseInt(contBox.offsetHeight);
                    contBox.style.display = 'none';
                    bigBox.style.width = x;
                    bigBox.style.height = y;
                } else {
                    contBox.innerHTML = contBody
                }
                this.size(bigBox, myWidth, myHeight, 4)
            } else {
                bigBox.style.backgroundImage = 'none'
            }
        },
        hide: function() {
            TINY.box.alpha(bigBox, -1, 0, 3)
        },
        resize: function() {
            TINY.box.pos();
            TINY.box.mask()
        },
        mask: function() {
            myMask.style.height = TINY.page.theight() + 'px';
            myMask.style.width = '';
            myMask.style.width = TINY.page.twidth() + 'px'
        },
        pos: function() {
            var t = (TINY.page.height() / 2) - (bigBox.offsetHeight / 2);
            t = t < 10 ? 10 : t;
            //bigBox.style.top = (t + TINY.page.top()) + 'px';
            bigBox.style.left = (TINY.page.width() / 2) - (bigBox.offsetWidth / 2) + 'px'
        },

        alpha: function(e, d, a, s) {
            if (d == 1) {
                e.style.opacity = 0;
                e.style.filter = 'alpha(opacity=0)';
                e.style.display = 'block';
                this.pos()
            }
            TINY.box.twalpha(e, a, d, s)
        },
        twalpha: function(e, a, d, s) {
            e.style.opacity = a / 100;
            e.style.filter = 'alpha(opacity=' + a + ')'
            if (d == -1) {
                e.style.display = 'none';
                e == bigBox ? TINY.box.alpha(myMask, -1, 0, 2) : contBox.innerHTML = bigBox.style.backgroundImage = ''
            } else {
                e == myMask ? this.alpha(bigBox, 1, 100, 5) : TINY.box.fill(ic, iu, iw, ih, ia)
            }
        },
        size: function(e, myWidth, myHeight, s) {
            e = typeof e == 'object' ? e : T$(e);
            var ow = e.offsetWidth,
			oh = e.offsetHeight,
			wo = ow - parseInt(e.style.width),
			ho = oh - parseInt(e.style.height);
            var wd = ow - wo > myWidth ? -1 : 1,
			hd = (oh - ho > myHeight) ? -1 : 1;
            TINY.box.twsize(e, myWidth, wo, wd, myHeight, ho, hd, s);
        },
        twsize: function(e, myWidth, wo, wd, myHeight, ho, hd, s) {
            e.style.width = myWidth + "px";
            e.style.height = myHeight + "px";
            this.pos();
            bigBox.style.backgroundImage = 'none';
            contBox.style.display = 'block'
        }
    }
} ();

TINY.page = function() {
    return {
        top: function() {
            return document.body.scrollTop || document.documentElement.scrollTop
        },
        width: function() {
            return self.innerWidth || document.documentElement.clientWidth
        },
        height: function() {
            return self.innerHeight || document.documentElement.clientHeight
        },
        theight: function() {
            var d = document,
			b = d.body,
			e = d.documentElement;
            return Math.max(Math.max(b.scrollHeight, e.scrollHeight), Math.max(b.clientHeight, e.clientHeight))
        },
        twidth: function() {
            var d = document,
			b = d.body,
			e = d.documentElement;
            return Math.max(Math.max(b.scrollWidth, e.scrollWidth), Math.max(b.clientWidth, e.clientWidth))
        }
    }
} ();