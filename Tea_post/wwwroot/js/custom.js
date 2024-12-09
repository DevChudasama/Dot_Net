$(".DeleteSweetAlertBtn").submit(function (e) {
    e.preventDefault(); // Prevent the default form submission
    var form = this; // Keep a reference to the form

    swal({
        title: 'Are you sure?',
        text: 'You want to Delete this Record!',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    })
        .then((willSubmit) => {
            if (willSubmit) {
                // User confirmed, proceed with form submission
                // Directly submit the form bypassing jQuery's submit to prevent infinite loop
                form.submit();

            } else {
                // User canceled, you can show an alert or just silently stop
                /swal('Your form is not submitted!');/
            }
        });
});