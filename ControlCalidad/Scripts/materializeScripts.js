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

        format: 'yyyy-mm-dd',
        showClearBtn: true,
        i18n: {
            done: 'Aceptar',
            cancel: 'Cancelar',
            clear: 'Borrar'
        }


    });
});

function redirectToPage(address) {
    location.href = address;
}
function onSubmit() {
    document.getElementById("loading").classList.remove("hide");
}

//Validation functions for forms

//--------------------------------------Register Validations-------------------------------

function validateEmail(idElement) {

    if (document.getElementById(idElement).value.length < 3) {
        document.getElementById("mailError").innerHTML = "Debe introducir un correo válido";
    }

    else {
        if ((document.getElementById(idElement).value.indexOf("@") == -1) || (document.getElementById(idElement).value.indexOf(".") == -1)) {
            document.getElementById("mailError").innerHTML = "Debe introducir un correo válido";
        }
        else {
            document.getElementById("mailError").innerHTML = "";

        }
    }
}

function validatePassword(password) {
    if (document.getElementById(password).value.length < 4) {
        document.getElementById("passwordError").innerHTML = "La contraseña debe tener 5 caracter como mínimo.";
    }
    else {
        document.getElementById("passwordError").innerHTML = "";
    }
}

function validateConfirmPass() {
    let password = document.getElementById("Password").value;
    let passConfirm = document.getElementById("ConfirmPassword").value;

    if (password != passConfirm) {
        document.getElementById("passwordConfirmError").innerHTML = "Las contraseñas no coinciden.";
    }
    else {
        document.getElementById("passwordConfirmError").innerHTML = "";
    }
}

//----------------------------------------------------------------------------------------------------------------------

//-------------------------------------------------------------------Project Validations--------------------------------
function validateClient(idClient) {
    if (document.getElementById(idClient).value.indexOf("Selecciona el cliente") > -1) {
        document.getElementById("ClientError").innerHTML = "Debe seleccionar un cliente.";
    }
}

function validateProjectName() {
    if (document.getElementById("projectName").value.length < 5) {
        document.getElementById("nameError").innerHTML = "Debe colocar un nombre válido al proyecto(5 caracteres como mínimo).";
    }
    else {
        document.getElementById("nameError").innerHTML = "";
    }
}

function validateDate() {
    if (document.getElementById("fechaInicio").value.length <= 0) {
        document.getElementById("dateErrorMessage").innerHTML = "Debe seleccionar una fecha de inicio para el proyecto.";
    }
    else {
        document.getElementById("dateErrorMessage").innerHTML = "";
    }
}

function validateDuration() {
    if (document.getElementById("avrDuration").value.length <= 0) {
        document.getElementById("avrDurationError").innerHTML = "Debes ingresar una duracion válida.";
    }
    else {
        document.getElementById("avrDurationError").innerHTML = "";
    }
}
//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Employee Validations----------------------------------------------------
function validateEmployeeName(inputtxt) {
    var letters = /^[a-zA-Z\s]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";

    }
}
function validateEmployeeSurname(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
    }
}
function validateEmployeeSurname2(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
    }
}
function validateEmployeeAge(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    var err = document.getElementById("employeeAgeError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "<span class=red-text>Digite caracteres numericos</span>";
    }
    else {
        if (parseInt(inputtxt.value, 10) > 18 && parseInt(inputtxt.value, 10) < 100) {
            err.innerHTML = " "
        } else {
            err.innerHTML = "<span class=red-text>Digite una edad valida</span>";
        }

    }
}
function validateEmployeeEmail(inputtxt) {
    if (inputtxt.value.includes("@") && (inputtxt.value.includes(".com") || inputtxt.value.includes(".net"))) {
        var err = document.getElementById("employeeEmailError");
        err.innerHTML = "";
    } else {
        var err = document.getElementById("employeeEmailError");
        err.innerHTML = "<span class=red-text>Digite un correo valido</span>";
    }
}
function validateEmployeePhoneNumber(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("employeePhoneError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "";
    }
    else {
        err.innerHTML = `<span class=red-text>Digite un valores numericos </span>`;
    }
}
function validateEmployeeID(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("employeeIDError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "";
    }
    else {
        err.innerHTML = `<span class=red-text>Digite un valores numericos </span>`;
    }
}

//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Client Validations----------------------------------------------------
function validateNameClient() {
    if (document.getElementById("nameClient").value.length <= 0) {
        document.getElementById("nameClientError").innerHTML = "Debes ingresar un nombre.";
    }
    else {
        document.getElementById("nameClientError").innerHTML = "";
    }
}

function validateSurnameClient() {
    if (document.getElementById("surnameClient").value.length <= 0) {
        document.getElementById("surnameClientError").innerHTML = "Debes ingresar el primer apellido.";
    }
    else {
        document.getElementById("surnameClientError").innerHTML = "";
    }
}

function validateSecondSurnameClient() {
    if (document.getElementById("secondSurnameClient").value.length <= 0) {
        document.getElementById("secondSurnameClientError").innerHTML = "Debes ingresar el segundo apellido.";
    }
    else {
        document.getElementById("secondSurnameClientError").innerHTML = "";
    }
}

function validateIdClient(inputId) {
        if (document.getElementById("idClient").value.length <= 7) {
            document.getElementById("idClientError").innerHTML = "Debes ingresar una cédula válida.";
        }
        else {
            document.getElementById("idClientError").innerHTML = "";
        }
}
