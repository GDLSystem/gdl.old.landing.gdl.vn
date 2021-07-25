$(document).ready(() => {
    drag2Scroll('communities');
    drag2Scroll('partners');
    startCount();
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
        console.log((hT-wH) , wS);
        if (wS > (hT+hH/2-wH)){
            $(item).find('.fade-in-content').addClass("fade-in");
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