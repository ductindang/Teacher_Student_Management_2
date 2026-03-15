
// Display the comment for each student asignment
$(document).ready(function () {

    var table = $('#teacher-table').DataTable();

    $('#teacher-table tbody').on('click', '.view-comment', function () {

        var tr = $(this).closest('tr');
        var row = table.row(tr);
        var comment = $(this).data('comment');

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
            return;
        }

        table.rows().every(function () {
            if (this.child.isShown()) {
                this.child.hide();
                $(this.node()).removeClass('shown');
            }
        });

        row.child(`
                <div class="p-3 bg-light border rounded">
                    <strong>Nhận xét:</strong>
                    <p class="mb-0 mt-2" style="word-break: break-word; white-space: normal;">
                        ${comment}
                    </p>
                </div>
            `).show();

        tr.addClass('shown');
    });

});
