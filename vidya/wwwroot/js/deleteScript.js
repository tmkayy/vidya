function setupDeleteConfirmation(buttonSelector, controllerName, itemName) {
    $(document).ready(function () {
        $(buttonSelector).click(function () {
            const itemId = $(this).data('id');
            Swal.fire({
                title: `Are you sure you want to delete this ${itemName}?`,
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    // send AJAX request to delete endpoint
                    $.ajax({
                        url: `/${controllerName}/Delete/${itemId}`,
                        type: 'DELETE',
                        success: function (response) {
                            // handle success response
                            Swal.fire(
                                'Deleted!',
                                `The ${itemName} has been deleted.`,
                                'success'
                            ).then(() => {
                                // redirect to the index page
                                window.location.href = `/${controllerName}/Index`;
                            });
                        },
                        error: function (xhr, status, error) {
                            // handle error response
                            Swal.fire(
                                'Error!',
                                `Failed to delete the ${itemName}.`,
                                'error'
                            );
                        }
                    });
                }
            });
        });
    });
}