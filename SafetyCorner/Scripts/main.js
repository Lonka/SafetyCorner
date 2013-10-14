$(function () {
    $(window).load(function () {
        $(window).bind('scroll resize', function () {
            var $this = $(this);
            var $this_Top = $this.scrollTop();
            if ($this_Top > 167) {
                $('.menu').addClass('menu-fixed');
                $('.menu-bg').show();
            } else {
                $('.menu').removeClass('menu-fixed');
                $('.menu-bg').hide();
            }
        });
    });

    var submenu_active = new Array();
    $('.menu-title').mouseenter(function () {
        var menu_Id = $(this).attr("id");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = true;
        var postion  = $(this).position();
        $('div[for='+menu_Id+']').css({left:postion.left+5,top:postion.top+60}).fadeIn();
    }).mouseleave(function () {
        var menu_Id = $(this).attr("id");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = false;
        setTimeout(function () { if (submenu_active[id] === false) $('div[for='+menu_Id+']').fadeOut(); }, 400);
    });

    $('.sub-menu').mouseenter(function () {
        var menu_Id = $(this).attr("for");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = true;
    }).mouseleave(function () {
        var menu_Id = $(this).attr("for");
        var id = menu_Id.replace('menu', '');
        submenu_active[id] = false;
        setTimeout(function () { if (submenu_active[id] === false) $('div[for='+menu_Id+']').fadeOut(); }, 400);
    });

});


function SetCwinHeight()
{
var iframeid=document.getElementById("content"); //iframe id
  if (document.getElementById)
  {   
   if (iframeid && !window.opera)
   {   
    if (iframeid.contentDocument && iframeid.contentDocument.body.offsetHeight)
     {   
       iframeid.height = iframeid.contentDocument.body.offsetHeight;   
     }else if(iframeid.Document && iframeid.Document.body.scrollHeight)
     {   
       iframeid.height = iframeid.Document.body.scrollHeight;   
      }   
    }
   }
}