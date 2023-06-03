$(document).ready(function () {
    $('#Cart').click(function () {
        $('#offcanvasRight-2').offcanvas('show');
    });
});
$(document).ready(function () {
    $('#Login').click(function () {
        $('#offcanvasScrolling').offcanvas('show');
    });
});
$(document).ready(function () {
    $('#Feature').click(function () {
        $('#feature').offcanvas('show');
    });
});
$(document).ready(function () {
    $('#Create').click(function () {
        $('#offcanvasRight').offcanvas('show');
        $('#offcanvasScrolling').offcanvas('hide');
    });
});
$(document).ready(function () {
    $('#QuenMatKhau').click(function () {
        $('#offcanvasRight-1').offcanvas('show');
        $('#offcanvasScrolling').offcanvas('hide');
    });
});x