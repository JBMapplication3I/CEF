window.blazorExtensions = {
    WriteCookie: function (name, value, days, path) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=" + (path || "/");
    },
    ReadCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i <ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    ReadCookies: function () {
        return document.cookie;
    },
    SetTitle: function (title) {
        document.title = title;
    },
    RedirectPage: function (page) {
        if (page == null || page == undefined) { page = '/'; }
        window.location = page;
    }
}
