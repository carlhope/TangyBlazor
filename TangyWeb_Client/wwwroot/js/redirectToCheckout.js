redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51PiHjmGaqhr8UhGcjPTYBmafZwpSbWh1aMizk7i7E81Dis77hSmRakuLt97v0hNLxCCeMKZXg7XXYky9zXWetJ0u00P2JZlxjd");
    stripe.redirectToCheckout({sessionId: sessionId});
};