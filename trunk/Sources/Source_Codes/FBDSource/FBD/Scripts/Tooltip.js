function toolTip(top, left) {
    var el = document.getElementById('foo');
    el.innerHTML = "If you fill in fix values, from values and to values will be ignored!";
    //var top = top * 30 + 300;
    el.style.top = top + "px";
    el.style.left = left + "px";
    el.style.display = "block";
    document.onmouseout = function() {
        el.style.display = "none";
    }
}