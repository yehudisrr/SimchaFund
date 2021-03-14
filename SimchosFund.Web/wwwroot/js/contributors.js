$(() => {
    $("#add-contributor").on('click', function () {
        $('#add-contributor-modal').modal();
        $("#initialDepositDiv").show();
    });
    $(".deposit-button").on('click', function () {
        $('#add-deposit-modal').modal();
        const contributorId = $(this).data('contributorid');
        $('#contributor-id').val(contributorId);

        const tr = $(this).parent().parent();
        const name = tr.find('td:eq(1)').text();
        $("#deposit-name").text(name);

    });

    $(".edit-button").on('click', function () {
        const id = $(this).data('id');
        $('#contributorId').val(id);
        const firstName = $(this).data('first-name');
        $('#contributorFirstName').val(firstName);
        const lastName = $(this).data('last-name');
        $('#contributorLastName').val(lastName);
        const cell = $(this).data('cell');
        $('#contributorCellNumber').val(cell);
        const alwaysInclude = $(this).data('always-include');
        $('#contributorAlwaysInclude').prop('checked', alwaysInclude === "True");
        const date = $(this).data('created-date');
        $('#contributorCreatedDate').val(date);
        $("#initialDepositDiv").hide();
        $('#add-contributor-modal').modal();

        const form = $(".add-contributor");
        form.attr('action', '/contributors/edit');
    });
});