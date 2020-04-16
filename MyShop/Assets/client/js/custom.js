var cart = {
    init: function () {
        cart.event();
    },
    event: function () {
        $("#btn-continue").on('click', function () {
            window.history.back();
        });
    }
}
cart.init();