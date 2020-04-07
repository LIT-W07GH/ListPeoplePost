$(() => {
    setTimeout(() => {
        $("#alert").hide('slow');
    }, 2000);

    $("#show-modal").on('click', () => {
        $("#my-modal").modal();
    })
});