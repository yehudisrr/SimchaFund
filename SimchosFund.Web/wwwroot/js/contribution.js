$(() => {
    $(function () {
        $(".toggle-btn").change();
    });
    $(".toggle-btn").change(function () {
        if ($(this).prop("checked") == true) {
            const contributionId = $(this).data('contribution-contributor-id');
            $(this).val(contributionId);
        } else {
            $(this).val('0');
        }
     
    });
 });