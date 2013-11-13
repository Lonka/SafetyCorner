$(function () {
//    $(window).load(function () {
//        $(window).bind('scroll resize', function () {
//            var $this = $(this);
//            var $this_Top = $this.scrollTop();
//            if ($this_Top > 172) {
//                $('.menu').addClass('menu-fixed');
//                SetMainWidth('menu-fixed');
//                $('.menu-bg').show();
//                SetMainWidth('menu-bg');
//                $('.content').addClass('content-fixed');
//            } else {
//                $('.menu').removeClass('menu-fixed');
//                $('.menu-bg').hide();
//                $('.content').removeClass('content-fixed');
//            }
//        });
//        SetMainWidth('main');
//    });

//    $(window).resize(function () {
//        SetMainWidth('main');
//        SetMainWidth('menu-fixed');
//        SetMainWidth('menu-bg');
//        SetMainWidth('menu');
//    });

    //#region Menu
    var submenu_active = new Array();
    $('.menu-title').mouseenter(function () {
        var menu_Id = $(this).attr("id");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = true;
        var postion = $(this).position();
        $('div[for=' + menu_Id + ']').css({ left: postion.left + 5, top: postion.top + 60 }).fadeIn();
    }).mouseleave(function () {
        var menu_Id = $(this).attr("id");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = false;
        setTimeout(function () { if (submenu_active[id] === false) $('div[for=' + menu_Id + ']').fadeOut(); }, 400);
    });

    $('.sub-menu').mouseenter(function () {
        var menu_Id = $(this).attr("for");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = true;
    }).mouseleave(function () {
        var menu_Id = $(this).attr("for");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = false;
        setTimeout(function () { if (submenu_active[id] === false) $('div[for=' + menu_Id + ']').fadeOut(); }, 400);
    });
    //#endregion

    function SetMainWidth(className) {
        $('.' + className).width($(window).width() - 180);
    }





});


function SetCwinHeight() {
    var iframeid = document.getElementById("content"); //iframe id
    if (document.getElementById) {
        if (iframeid && !window.opera) {
            if (iframeid.contentDocument && iframeid.contentDocument.body.offsetHeight) {
                iframeid.height = iframeid.contentDocument.body.offsetHeight;
            } else if (iframeid.Document && iframeid.Document.body.scrollHeight) {
                iframeid.height = iframeid.Document.body.scrollHeight;
            }
        }
    }
}