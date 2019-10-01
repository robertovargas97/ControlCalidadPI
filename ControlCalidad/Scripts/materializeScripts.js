document.addEventListener('DOMContentLoaded', function () {

    var elems = document.querySelectorAll('select');
    var instances = M.FormSelect.init(elems);

    var elems = document.querySelectorAll('.dropdown-trigger');
    var instances = M.Dropdown.init(elems, { coverTrigger: false, closeOnClick: true });

    var elems = document.querySelectorAll('.datepicker');
    var instances = M.Datepicker.init(elems);

});

