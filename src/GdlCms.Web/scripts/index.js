$(document).ready(() => {
    drag2Scroll('communities');
    drag2Scroll('partners');
    startCount();
    const button = document.querySelector("#back2top");
    back2top(button, 200, "all", 300, "ease", 0);
});
function drag2Scroll(id) {
    let ele = document.getElementById(id);
    ele.style.cursor = 'grab';

    let pos = { top: 0, left: 0, x: 0, y: 0 };

    const mouseDownHandler = function(e) {
        e.preventDefault();
        e.stopPropagation();
        ele.style.cursor = 'grabbing';
        ele.style.userSelect = 'none';

        pos = {
            left: ele.scrollLeft,
            top: ele.scrollTop,
            // Get the current mouse position
            x: e.clientX,
            y: e.clientY,
        };

        document.addEventListener('mousemove', mouseMoveHandler);
        document.addEventListener('mouseup', mouseUpHandler);
    };

    const mouseMoveHandler = function(e) {
        // How far the mouse has been moved
        const dx = e.clientX - pos.x;
        const dy = e.clientY - pos.y;

        // Scroll the element
        ele.scrollTop = pos.top - dy;
        ele.scrollLeft = pos.left - dx;
    };

    const mouseUpHandler = function() {
        ele.style.cursor = 'grab';
        ele.style.removeProperty('user-select');

        document.removeEventListener('mousemove', mouseMoveHandler);
        document.removeEventListener('mouseup', mouseUpHandler);
    };

    // Attach the handler
    ele.addEventListener('mousedown', mouseDownHandler);
}

$(window).scroll(function() {
    let fadeInAnchor = $('.fade-in-anchor');
    for(let item of fadeInAnchor) 
    {
        var hT = $(item).offset().top,
        hH = $(item).outerHeight(),
        wH = $(window).height(),
        wS = $(this).scrollTop();
        if (wS > (hT+hH/2-wH)){
            let contents = $(item).find('.fade-in-content');
            if (contents.length > 1) {
                let i = 0;
                for (let content of contents) {
                    i++;
                    setTimeout(function () {
                        $(content).addClass("index-fade-in");
                    }, i * 200);
                }
            } else {
                contents.addClass("index-fade-in");
            }
        }
    }
});

function animateValue(obj, duration) {
    var start = 0;
    var end = parseInt(obj.text().trim());
    var startTimestamp = null;
    var step = (timestamp) => {
      if (!startTimestamp) startTimestamp = timestamp;
      var progress = Math.min((timestamp - startTimestamp) / duration, 1);
      obj.text(Math.floor(progress * (end - start) + start));
      if (progress < 1) {
        window.requestAnimationFrame(step);
      }
    };
    window.requestAnimationFrame(step);
  }
  

function startCount() {
    $('.stats-count').each(function(index) {
        animateValue($(this), 2000);
    });
}

function back2top(selector, offset, prop = 'all', time = '300', effect = 'ease', delay = 0) {
    const WIN_SCROLLED = function () {
        if (document.body.scrollTop > offset || document.documentElement.scrollTop > offset) {
            const STYLES = {
                opacity: '1',
                visibility: 'visible',
                transform: 'translateY(0)',
                transition: `${prop} ${time}ms ${effect} ${delay}ms`
            }
            Object.assign(selector.style, STYLES);
        } else {
            const STYLES = {
                opacity: '0',
                visibility: 'hidden',
                transform: 'translateY(100%)',
                transition: `${prop} ${time}ms ${effect} ${delay}ms`
            }
            Object.assign(selector.style, STYLES);
        }
    };
    const SCROLL_EVT = function () {
        document.documentElement.scrollTo({
            top: 0,
            left: 0,
            behavior: 'smooth'
        });
    };
    const SELECTOR_LISTENER = selector.addEventListener("click", SCROLL_EVT);
    const WINDOW_LISTENER = window.addEventListener('scroll', WIN_SCROLLED);
    return SELECTOR_LISTENER;
    return WINDOW_LISTENER;
}


/* Smooth Scrolling
 * ------------------------------------------------------ */
var clSmoothScroll = function () {

    $('.smoothscroll').on('click', function (e) {
        var target = this.hash,
            $target = $(target);

        e.preventDefault();
        e.stopPropagation();

        $('html, body').stop().animate({
            'scrollTop': $target.offset().top
        }, cfg.scrollDuration, 'swing').promise().done(function () {

            // check if menu is open
            if ($('body').hasClass('menu-is-open')) {
                $('.header-menu-toggle').trigger('click');
            }

            window.location.hash = target;
        });
    });

};

