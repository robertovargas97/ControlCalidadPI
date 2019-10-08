document.addEventListener('DOMContentLoaded', function () {

    var elems = document.querySelectorAll('.parallax');
    var instances = M.Parallax.init(elems);

    var elems = document.querySelectorAll('select');
    var instances = M.FormSelect.init(elems);

    var elems = document.querySelectorAll('.modal');
    var instances = M.Modal.init(elems);

    var elems = document.querySelectorAll('.dropdown-trigger');
    var instances = M.Dropdown.init(elems, { coverTrigger: false, closeOnClick: true });

    var elems = document.querySelectorAll('.datepicker');
    var instances = M.Datepicker.init(elems);

    var elems = document.querySelectorAll('.datepicker');
    var instances = M.Datepicker.init(elems, {

        format : 'yyyy-mm-dd',
        showClearBtn: true,
        i18n: {
            done : 'Aceptar',
            cancel : 'Cancelar',
            clear : 'Borrar'
        }
        

    });
});

function redirectToPage(address) {
    location.href = address;
}

function complete() {
    document.getElementById("complete").classList.remove("hide");
}


function showError() {
    alert("Error");
}

