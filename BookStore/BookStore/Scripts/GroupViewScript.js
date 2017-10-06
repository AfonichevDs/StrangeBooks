window.onload = function () {

    var options = document.getElementById('selCatgs').childNodes;

    for (var i = 0; i < options.length; i++) {
        $(options[i]).addClass('list-group-item');
    }
}