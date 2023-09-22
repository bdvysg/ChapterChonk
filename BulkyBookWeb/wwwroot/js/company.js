var dataTable;


$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/company/getall' },

        "columns": [
            { data: 'name' },
            { data: 'region' },
            { data: 'postalCode' },
            { data: 'phoneNumber' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="btn-group w-75" role="group">
                            <a href="/admin/company/upsert?id=${data}" class="btn btn-primary">Edit</a>
                            <a onClick=deleteProduct("/admin/company/delete/${data}") class="btn btn-danger">Delete</a>
                        </div>
                    `
                },
                "width": "15%"
            },
        ]
    });
}

function deleteProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function () {
                    Swal.fire(
                        'Deleted!',
                        'Your book has been deleted.',
                        'success'
                    );
                    dataTable.ajax.reload();
                }
            })

        }
    })
}
